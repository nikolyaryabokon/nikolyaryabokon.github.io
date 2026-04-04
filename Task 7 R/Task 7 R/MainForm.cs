// File: MainForm.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Task_7_R
{
    public partial class MainForm : Form
    {
        private List<Figure> _figures = new List<Figure>();
        private List<Figure> _clipboard = new List<Figure>();
        private Stroke _currentStroke = new Stroke();
        private StackMemory _undoStack;
        private StackMemory _redoStack;
        private Figure _selectedFigure;
        private SelectionMarker _selectionMarker;
        private Point _lastMouseDown;
        private bool _isDragging = false;
        private bool _isResizing = false;
        private HitMarkerType _resizeHandle = HitMarkerType.None;
        private Point _resizeStartPoint;
        private Rectangle _resizeStartBounds;
        private string _currentFilePath = null;
        private Color _currentFillColor = Color.Transparent;

        // Компоненты формы
        private Panel canvasPanel;
        private PictureBox canvas;
        private MenuStrip menuStrip;
        private ToolStrip toolStrip;
        private StatusStrip statusStrip;
        private ColorDialog colorDialog;
        private ToolStripButton btnCircle, btnEllipse, btnSquare, btnRectangle, btnTrapezoid;
        private ToolStripButton btnSelect, btnFillColor;
        private ToolStripComboBox cmbStrokeWidth;
        private ToolStripButton btnUndo, btnRedo;
        private ToolStripButton btnCopy, btnCut, btnPaste;
        private ToolStripButton btnSave, btnLoad;
        private ToolStripStatusLabel statusLabel;
        private ToolStripStatusLabel positionLabel;

        private enum ToolMode
        {
            Select,
            Circle,
            Ellipse,
            Square,
            Rectangle,
            Trapezoid
        }

        private ToolMode _currentTool = ToolMode.Select;
        private Point _startDrawPoint;

        public MainForm()
        {
            InitializeComponent();
            InitializeStacks();
            InitializeStatusStrip();
            UpdateUI();

            // Подписка на события
            canvas.Paint += Canvas_Paint;
            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseUp += Canvas_MouseUp;
            canvas.MouseClick += Canvas_MouseClick;
            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;
        }

        private void InitializeComponent()
        {
            this.Text = "Векторный редактор - Вариант 1";
            this.Size = new Size(1024, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // MenuStrip
            menuStrip = new MenuStrip();

            var fileMenu = new ToolStripMenuItem("Файл");
            var newItem = new ToolStripMenuItem("Новый", null, NewFile_Click);
            newItem.ShortcutKeys = Keys.Control | Keys.N;
            var openItem = new ToolStripMenuItem("Открыть...", null, LoadFile_Click);
            openItem.ShortcutKeys = Keys.Control | Keys.O;
            var saveItem = new ToolStripMenuItem("Сохранить", null, SaveFile_Click);
            saveItem.ShortcutKeys = Keys.Control | Keys.S;
            var saveAsItem = new ToolStripMenuItem("Сохранить как...", null, SaveAsFile_Click);
            var exitItem = new ToolStripMenuItem("Выход", null, (s, e) => Close());
            exitItem.ShortcutKeys = Keys.Alt | Keys.F4;

            fileMenu.DropDownItems.Add(newItem);
            fileMenu.DropDownItems.Add(openItem);
            fileMenu.DropDownItems.Add(saveItem);
            fileMenu.DropDownItems.Add(saveAsItem);
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add(exitItem);

            var editMenu = new ToolStripMenuItem("Правка");
            var undoItem = new ToolStripMenuItem("Отменить", null, Undo_Click);
            undoItem.ShortcutKeys = Keys.Control | Keys.Z;
            var redoItem = new ToolStripMenuItem("Повторить", null, Redo_Click);
            redoItem.ShortcutKeys = Keys.Control | Keys.Y;
            var cutItem = new ToolStripMenuItem("Вырезать", null, Cut_Click);
            cutItem.ShortcutKeys = Keys.Control | Keys.X;
            var copyItem = new ToolStripMenuItem("Копировать", null, Copy_Click);
            copyItem.ShortcutKeys = Keys.Control | Keys.C;
            var pasteItem = new ToolStripMenuItem("Вставить", null, Paste_Click);
            pasteItem.ShortcutKeys = Keys.Control | Keys.V;
            var deleteItem = new ToolStripMenuItem("Удалить", null, Delete_Click);
            deleteItem.ShortcutKeys = Keys.Delete;

            editMenu.DropDownItems.Add(undoItem);
            editMenu.DropDownItems.Add(redoItem);
            editMenu.DropDownItems.Add(new ToolStripSeparator());
            editMenu.DropDownItems.Add(cutItem);
            editMenu.DropDownItems.Add(copyItem);
            editMenu.DropDownItems.Add(pasteItem);
            editMenu.DropDownItems.Add(new ToolStripSeparator());
            editMenu.DropDownItems.Add(deleteItem);

            var figureMenu = new ToolStripMenuItem("Фигура");
            var strokeColorItem = new ToolStripMenuItem("Цвет контура...", null, ChangeStrokeColor_Click);
            var fillColorItem = new ToolStripMenuItem("Цвет заливки...", null, ChangeFillColor_Click);
            figureMenu.DropDownItems.Add(strokeColorItem);
            figureMenu.DropDownItems.Add(fillColorItem);

            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(editMenu);
            menuStrip.Items.Add(figureMenu);

            // ToolStrip
            toolStrip = new ToolStrip();
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;

            btnSelect = new ToolStripButton("Выбрать");
            btnSelect.ToolTipText = "Выбор и перемещение фигур";
            btnSelect.Click += (s, e) => SetTool(ToolMode.Select);

            toolStrip.Items.Add(new ToolStripSeparator());
            toolStrip.Items.Add(btnSelect);
            toolStrip.Items.Add(new ToolStripSeparator());

            btnCircle = new ToolStripButton("Круг");
            btnCircle.ToolTipText = "Рисование круга";
            btnCircle.Click += (s, e) => SetTool(ToolMode.Circle);

            btnEllipse = new ToolStripButton("Эллипс");
            btnEllipse.ToolTipText = "Рисование эллипса";
            btnEllipse.Click += (s, e) => SetTool(ToolMode.Ellipse);

            btnSquare = new ToolStripButton("Квадрат");
            btnSquare.ToolTipText = "Рисование квадрата";
            btnSquare.Click += (s, e) => SetTool(ToolMode.Square);

            btnRectangle = new ToolStripButton("Прямоугольник");
            btnRectangle.ToolTipText = "Рисование прямоугольника";
            btnRectangle.Click += (s, e) => SetTool(ToolMode.Rectangle);

            btnTrapezoid = new ToolStripButton("Трапеция");
            btnTrapezoid.ToolTipText = "Рисование равнобедренной трапеции";
            btnTrapezoid.Click += (s, e) => SetTool(ToolMode.Trapezoid);

            toolStrip.Items.Add(btnCircle);
            toolStrip.Items.Add(btnEllipse);
            toolStrip.Items.Add(btnSquare);
            toolStrip.Items.Add(btnRectangle);
            toolStrip.Items.Add(btnTrapezoid);
            toolStrip.Items.Add(new ToolStripSeparator());

            // Толщина линии
            toolStrip.Items.Add(new ToolStripLabel("Толщина:"));
            cmbStrokeWidth = new ToolStripComboBox();
            cmbStrokeWidth.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "8", "10" });
            cmbStrokeWidth.SelectedItem = "2";
            cmbStrokeWidth.Size = new Size(50, 25);
            cmbStrokeWidth.SelectedIndexChanged += (s, e) =>
            {
                if (float.TryParse(cmbStrokeWidth.Text, out float width))
                {
                    _currentStroke.Width = width;
                    canvas.Invalidate();
                }
            };
            toolStrip.Items.Add(cmbStrokeWidth);
            toolStrip.Items.Add(new ToolStripSeparator());

            btnFillColor = new ToolStripButton("Заливка");
            btnFillColor.ToolTipText = "Выбрать цвет заливки";
            btnFillColor.Click += ChangeFillColor_Click;
            toolStrip.Items.Add(btnFillColor);
            toolStrip.Items.Add(new ToolStripSeparator());

            btnUndo = new ToolStripButton("Отменить");
            btnUndo.ToolTipText = "Отменить последнее действие";
            btnUndo.Click += Undo_Click;

            btnRedo = new ToolStripButton("Повторить");
            btnRedo.ToolTipText = "Повторить отменённое действие";
            btnRedo.Click += Redo_Click;

            btnCut = new ToolStripButton("Вырезать");
            btnCut.ToolTipText = "Вырезать выделенную фигуру";
            btnCut.Click += Cut_Click;

            btnCopy = new ToolStripButton("Копировать");
            btnCopy.ToolTipText = "Копировать выделенную фигуру";
            btnCopy.Click += Copy_Click;

            btnPaste = new ToolStripButton("Вставить");
            btnPaste.ToolTipText = "Вставить фигуру из буфера обмена";
            btnPaste.Click += Paste_Click;

            btnSave = new ToolStripButton("Сохранить");
            btnSave.ToolTipText = "Сохранить рисунок в файл";
            btnSave.Click += SaveFile_Click;

            btnLoad = new ToolStripButton("Открыть");
            btnLoad.ToolTipText = "Загрузить рисунок из файла";
            btnLoad.Click += LoadFile_Click;

            toolStrip.Items.Add(btnUndo);
            toolStrip.Items.Add(btnRedo);
            toolStrip.Items.Add(new ToolStripSeparator());
            toolStrip.Items.Add(btnCut);
            toolStrip.Items.Add(btnCopy);
            toolStrip.Items.Add(btnPaste);
            toolStrip.Items.Add(new ToolStripSeparator());
            toolStrip.Items.Add(btnSave);
            toolStrip.Items.Add(btnLoad);

            // Canvas Panel
            canvasPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            canvas = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                SizeMode = PictureBoxSizeMode.AutoSize
            };

            canvasPanel.Controls.Add(canvas);

            // StatusStrip
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel("Готов");
            positionLabel = new ToolStripStatusLabel("X: 0, Y: 0");
            statusStrip.Items.Add(statusLabel);
            statusStrip.Items.Add(new ToolStripStatusLabel(" | "));
            statusStrip.Items.Add(positionLabel);

            // ColorDialog
            colorDialog = new ColorDialog();

            // Добавление элементов на форму
            this.Controls.Add(canvasPanel);
            this.Controls.Add(toolStrip);
            this.Controls.Add(menuStrip);
            this.Controls.Add(statusStrip);
            this.MainMenuStrip = menuStrip;

            // Обновление позиции мыши
            canvas.MouseMove += (s, e) =>
            {
                positionLabel.Text = $"X: {e.X}, Y: {e.Y}";
            };
        }

        private void InitializeStacks()
        {
            _undoStack = new StackMemory(20);
            _redoStack = new StackMemory(20);
            SaveState();
        }

        private void InitializeStatusStrip()
        {
            // StatusStrip уже инициализирован в InitializeComponent
            statusLabel.Text = "Готов";
        }

        private void SaveState()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                SaveToStream(ms, _figures);
                _undoStack.Push(ms);
                _redoStack.Clear();
            }
        }

        private void SaveToStream(Stream stream, List<Figure> listToSave = null)
        {
            var formatter = new BinaryFormatter();
            var list = (listToSave ?? _figures).ToList();
            formatter.Serialize(stream, list);
            stream.Position = 0;
        }

        private List<Figure> LoadFromStream(Stream stream)
        {
            try
            {
                var formatter = new BinaryFormatter();
                stream.Position = 0;
                return (List<Figure>)formatter.Deserialize(stream);
            }
            catch (SerializationException e)
            {
                MessageBox.Show($"Ошибка загрузки: {e.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Figure>();
            }
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            if (_undoStack.Count <= 0) return;

            using (MemoryStream ms = new MemoryStream())
            {
                _undoStack.Pop(ms);
                using (MemoryStream currentMs = new MemoryStream())
                {
                    SaveToStream(currentMs, _figures);
                    _redoStack.Push(currentMs);
                }
                _figures = LoadFromStream(ms);
                _selectedFigure = null;
                _selectionMarker = null;
                canvas.Invalidate();
                UpdateUI();
                statusLabel.Text = "Отменено последнее действие";
            }
        }

        private void Redo_Click(object sender, EventArgs e)
        {
            if (_redoStack.Count <= 0) return;

            using (MemoryStream ms = new MemoryStream())
            {
                _redoStack.Pop(ms);
                using (MemoryStream currentMs = new MemoryStream())
                {
                    SaveToStream(currentMs, _figures);
                    _undoStack.Push(currentMs);
                }
                _figures = LoadFromStream(ms);
                _selectedFigure = null;
                _selectionMarker = null;
                canvas.Invalidate();
                UpdateUI();
                statusLabel.Text = "Повторено действие";
            }
        }

        private void SetTool(ToolMode mode)
        {
            _currentTool = mode;
            UpdateToolButtons();
            statusLabel.Text = $"Режим: {mode}";
        }

        private void UpdateToolButtons()
        {
            btnSelect.BackColor = _currentTool == ToolMode.Select ? SystemColors.ActiveCaption : SystemColors.Control;
            btnCircle.BackColor = _currentTool == ToolMode.Circle ? SystemColors.ActiveCaption : SystemColors.Control;
            btnEllipse.BackColor = _currentTool == ToolMode.Ellipse ? SystemColors.ActiveCaption : SystemColors.Control;
            btnSquare.BackColor = _currentTool == ToolMode.Square ? SystemColors.ActiveCaption : SystemColors.Control;
            btnRectangle.BackColor = _currentTool == ToolMode.Rectangle ? SystemColors.ActiveCaption : SystemColors.Control;
            btnTrapezoid.BackColor = _currentTool == ToolMode.Trapezoid ? SystemColors.ActiveCaption : SystemColors.Control;
        }

        private void UpdateUI()
        {
            bool hasSelection = _selectedFigure != null;
            btnCopy.Enabled = hasSelection;
            btnCut.Enabled = hasSelection;
            btnUndo.Enabled = _undoStack.Count > 0;
            btnRedo.Enabled = _redoStack.Count > 0;
            btnPaste.Enabled = _clipboard.Count > 0;

            if (hasSelection && _selectionMarker == null)
                _selectionMarker = new SelectionMarker(_selectedFigure.Bounds);
            else if (hasSelection && _selectionMarker != null)
                _selectionMarker.UpdateBounds(_selectedFigure.Bounds);
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            using (Pen pen = _currentStroke.CreatePen())
            {
                foreach (var figure in _figures)
                {
                    figure.Draw(e.Graphics, pen);
                }
            }

            if (_selectedFigure != null && _selectionMarker != null)
            {
                _selectionMarker.Draw(e.Graphics);
            }
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _lastMouseDown = e.Location;
                canvas.Focus();

                if (_currentTool == ToolMode.Select)
                {
                    // Проверка попадания в маркеры
                    if (_selectedFigure != null && _selectionMarker != null)
                    {
                        _resizeHandle = _selectionMarker.HitTest(e.Location);
                        if (_resizeHandle != HitMarkerType.None)
                        {
                            _isResizing = true;
                            _resizeStartPoint = e.Location;
                            _resizeStartBounds = _selectedFigure.Bounds;
                            statusLabel.Text = "Изменение размера...";
                            return;
                        }
                    }

                    // Поиск фигуры под курсором
                    Figure clickedFigure = null;
                    for (int i = _figures.Count - 1; i >= 0; i--)
                    {
                        if (_figures[i].HitTest(e.Location))
                        {
                            clickedFigure = _figures[i];
                            break;
                        }
                    }

                    if (clickedFigure != null)
                    {
                        _selectedFigure = clickedFigure;
                        _selectedFigure.Selected = true;
                        _selectionMarker = new SelectionMarker(_selectedFigure.Bounds);
                        _isDragging = true;
                        canvas.Invalidate();
                        UpdateUI();
                        statusLabel.Text = "Перемещение фигуры...";
                    }
                    else
                    {
                        _selectedFigure = null;
                        _selectionMarker = null;
                        canvas.Invalidate();
                        UpdateUI();
                    }
                }
                else
                {
                    _startDrawPoint = e.Location;
                    statusLabel.Text = "Рисование...";
                }
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isResizing && _selectedFigure != null)
            {
                int dx = e.X - _resizeStartPoint.X;
                int dy = e.Y - _resizeStartPoint.Y;
                Rectangle newBounds = _resizeStartBounds;

                switch (_resizeHandle)
                {
                    case HitMarkerType.TopLeft:
                        newBounds.X += dx;
                        newBounds.Y += dy;
                        newBounds.Width -= dx;
                        newBounds.Height -= dy;
                        break;
                    case HitMarkerType.Top:
                        newBounds.Y += dy;
                        newBounds.Height -= dy;
                        break;
                    case HitMarkerType.TopRight:
                        newBounds.Y += dy;
                        newBounds.Width += dx;
                        newBounds.Height -= dy;
                        break;
                    case HitMarkerType.Left:
                        newBounds.X += dx;
                        newBounds.Width -= dx;
                        break;
                    case HitMarkerType.Right:
                        newBounds.Width += dx;
                        break;
                    case HitMarkerType.BottomLeft:
                        newBounds.X += dx;
                        newBounds.Width -= dx;
                        newBounds.Height += dy;
                        break;
                    case HitMarkerType.Bottom:
                        newBounds.Height += dy;
                        break;
                    case HitMarkerType.BottomRight:
                        newBounds.Width += dx;
                        newBounds.Height += dy;
                        break;
                }

                // Минимальный размер
                if (newBounds.Width < 10) newBounds.Width = 10;
                if (newBounds.Height < 10) newBounds.Height = 10;

                _selectedFigure.Bounds = newBounds;

                // Для квадрата сохраняем пропорции
                if (_selectedFigure is Square)
                {
                    int size = Math.Min(newBounds.Width, newBounds.Height);
                    _selectedFigure.Bounds = new Rectangle(newBounds.X, newBounds.Y, size, size);
                }

                _selectionMarker?.UpdateBounds(_selectedFigure.Bounds);
                canvas.Invalidate();
                return;
            }

            if (_isDragging && _selectedFigure != null)
            {
                int dx = e.X - _lastMouseDown.X;
                int dy = e.Y - _lastMouseDown.Y;
                _selectedFigure.Move(dx, dy);
                _selectionMarker?.UpdateBounds(_selectedFigure.Bounds);
                _lastMouseDown = e.Location;
                canvas.Invalidate();
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isResizing)
            {
                _isResizing = false;
                _resizeHandle = HitMarkerType.None;
                SaveState();
                UpdateUI();
                statusLabel.Text = "Готов";
                return;
            }

            if (_isDragging)
            {
                _isDragging = false;
                SaveState();
                UpdateUI();
                statusLabel.Text = "Готов";
                return;
            }

            if (_currentTool != ToolMode.Select && _startDrawPoint != Point.Empty)
            {
                Rectangle bounds = new Rectangle(
                    Math.Min(_startDrawPoint.X, e.X),
                    Math.Min(_startDrawPoint.Y, e.Y),
                    Math.Abs(e.X - _startDrawPoint.X),
                    Math.Abs(e.Y - _startDrawPoint.Y)
                );

                if (bounds.Width > 5 && bounds.Height > 5)
                {
                    SaveState();
                    Figure newFigure = null;

                    switch (_currentTool)
                    {
                        case ToolMode.Circle:
                            newFigure = new Circle(bounds);
                            break;
                        case ToolMode.Ellipse:
                            newFigure = new EllipseFigure(bounds);
                            break;
                        case ToolMode.Square:
                            newFigure = new Square(bounds);
                            break;
                        case ToolMode.Rectangle:
                            newFigure = new RectangleFigure(bounds);
                            break;
                        case ToolMode.Trapezoid:
                            newFigure = new IsoscelesTrapezoid(bounds);
                            break;
                    }

                    if (newFigure != null)
                    {
                        newFigure.FillColor = _currentFillColor;
                        _figures.Add(newFigure);
                        _selectedFigure = newFigure;
                        _selectionMarker = new SelectionMarker(newFigure.Bounds);
                        canvas.Invalidate();
                        UpdateUI();
                        statusLabel.Text = $"Добавлена фигура: {_currentTool}";
                    }
                }
                _startDrawPoint = Point.Empty;
            }
        }

        private void Canvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (_currentTool == ToolMode.Select && e.Button == MouseButtons.Right && _selectedFigure != null)
            {
                ContextMenuStrip contextMenu = new ContextMenuStrip();
                contextMenu.Items.Add("Изменить цвет контура", null, (s, ev) => ChangeStrokeColor_Click(s, ev));
                contextMenu.Items.Add("Изменить цвет заливки", null, (s, ev) => ChangeFillColor_Click(s, ev));
                contextMenu.Items.Add(new ToolStripSeparator());
                contextMenu.Items.Add("Копировать", null, (s, ev) => Copy_Click(s, ev));
                contextMenu.Items.Add("Вырезать", null, (s, ev) => Cut_Click(s, ev));
                contextMenu.Items.Add(new ToolStripSeparator());
                contextMenu.Items.Add("Удалить", null, (s, ev) => Delete_Click(s, ev));
                contextMenu.Show(canvas, e.Location);
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (_selectedFigure == null) return;

            int dx = 0, dy = 0;
            int step = e.Shift ? 1 : 5;

            switch (e.KeyCode)
            {
                case Keys.Left:
                    dx = -step;
                    break;
                case Keys.Right:
                    dx = step;
                    break;
                case Keys.Up:
                    dy = -step;
                    break;
                case Keys.Down:
                    dy = step;
                    break;
                case Keys.Delete:
                    Delete_Click(sender, e);
                    e.Handled = true;
                    return;
                default:
                    return;
            }

            if (dx != 0 || dy != 0)
            {
                SaveState();
                _selectedFigure.Move(dx, dy);
                _selectionMarker?.UpdateBounds(_selectedFigure.Bounds);
                canvas.Invalidate();
                UpdateUI();
                statusLabel.Text = $"Сдвиг на ({dx}, {dy})";
                e.Handled = true;
            }
        }

        private void ChangeStrokeColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = _currentStroke.Color;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                _currentStroke.Color = colorDialog.Color;
                canvas.Invalidate();
                statusLabel.Text = $"Цвет контура изменён на {colorDialog.Color.Name}";
            }
        }

        private void ChangeFillColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = _currentFillColor == Color.Transparent ? Color.LightBlue : _currentFillColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                _currentFillColor = colorDialog.Color;

                // Если есть выделенная фигура, применяем заливку к ней
                if (_selectedFigure != null)
                {
                    SaveState();
                    _selectedFigure.FillColor = _currentFillColor;
                    canvas.Invalidate();
                    statusLabel.Text = $"Цвет заливки применён к выделенной фигуре";
                }
                else
                {
                    statusLabel.Text = $"Выбран цвет заливки: {colorDialog.Color.Name} (для новых фигур)";
                }
            }
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            if (_selectedFigure != null)
            {
                _clipboard.Clear();
                _clipboard.Add(_selectedFigure.Clone());
                UpdateUI();
                statusLabel.Text = "Фигура скопирована";
            }
        }

        private void Cut_Click(object sender, EventArgs e)
        {
            if (_selectedFigure != null)
            {
                SaveState();
                _clipboard.Clear();
                _clipboard.Add(_selectedFigure.Clone());
                _figures.Remove(_selectedFigure);
                _selectedFigure = null;
                _selectionMarker = null;
                canvas.Invalidate();
                UpdateUI();
                statusLabel.Text = "Фигура вырезана";
            }
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            if (_clipboard.Count > 0)
            {
                SaveState();
                Figure newFigure = _clipboard[0].Clone();
                // Смещаем вставленную фигуру, чтобы она не перекрывала оригинал
                newFigure.Move(20, 20);
                newFigure.FillColor = _currentFillColor;
                _figures.Add(newFigure);
                _selectedFigure = newFigure;
                _selectionMarker = new SelectionMarker(newFigure.Bounds);
                canvas.Invalidate();
                UpdateUI();
                statusLabel.Text = "Фигура вставлена";
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (_selectedFigure != null)
            {
                SaveState();
                _figures.Remove(_selectedFigure);
                _selectedFigure = null;
                _selectionMarker = null;
                canvas.Invalidate();
                UpdateUI();
                statusLabel.Text = "Фигура удалена";
            }
        }

        private void NewFile_Click(object sender, EventArgs e)
        {
            if (_figures.Count > 0)
            {
                var result = MessageBox.Show("Сохранить изменения перед созданием нового рисунка?",
                    "Новый рисунок", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveFile_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            _figures.Clear();
            _selectedFigure = null;
            _selectionMarker = null;
            _clipboard.Clear();
            _undoStack.Clear();
            _redoStack.Clear();
            _currentFilePath = null;
            SaveState();
            canvas.Invalidate();
            UpdateUI();
            this.Text = "Векторный редактор - Вариант 1";
            statusLabel.Text = "Создан новый рисунок";
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath))
            {
                SaveAsFile_Click(sender, e);
            }
            else
            {
                SaveToFile(_currentFilePath);
            }
        }

        private void SaveAsFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Vector Editor Files (*.vec)|*.vec|All Files (*.*)|*.*";
                sfd.DefaultExt = "vec";
                sfd.FileName = "drawing.vec";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _currentFilePath = sfd.FileName;
                    SaveToFile(_currentFilePath);
                }
            }
        }

        private void SaveToFile(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    SaveToStream(fs, _figures);
                }
                this.Text = $"Векторный редактор - {Path.GetFileName(filePath)}";
                statusLabel.Text = $"Сохранено: {DateTime.Now.ToShortTimeString()}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Vector Editor Files (*.vec)|*.vec|All Files (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open))
                        {
                            var loadedFigures = LoadFromStream(fs);
                            if (loadedFigures != null)
                            {
                                _figures = loadedFigures;
                                _selectedFigure = null;
                                _selectionMarker = null;
                                _clipboard.Clear();
                                _undoStack.Clear();
                                _redoStack.Clear();
                                SaveState();
                                _currentFilePath = ofd.FileName;
                                this.Text = $"Векторный редактор - {Path.GetFileName(_currentFilePath)}";
                                canvas.Invalidate();
                                UpdateUI();
                                statusLabel.Text = $"Загружено: {DateTime.Now.ToShortTimeString()}";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}