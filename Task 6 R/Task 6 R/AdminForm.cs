using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Task_6_R
{
    public partial class AdminForm : Form
    {
        private TextBox txtTopic;
        private ComboBox cmbLevel;
        private TextBox txtQuestion;
        private ComboBox cmbType;
        private TextBox txtCorrect;
        private TextBox txtImagePath;
        private Button btnSelectImage;
        private Panel answersPanel;
        private Button btnAddAnswer;
        private Button btnSave;
        private ListBox lstAnswers;
        private string selectedImagePath = "";

        public AdminForm()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Панель администратора - Добавление вопросов";
            this.Size = new Size(600, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightGray;

            int y = 20;
            int labelW = 150;
            int controlW = 350;

            // Тема
            Label lblTopic = new Label { Text = "Название темы:", Location = new Point(20, y), Size = new Size(labelW, 25) };
            txtTopic = new TextBox { Location = new Point(180, y), Size = new Size(controlW, 25) };
            y += 35;

            // Уровень сложности
            Label lblLevel = new Label { Text = "Уровень (1-3):", Location = new Point(20, y), Size = new Size(labelW, 25) };
            cmbLevel = new ComboBox { Location = new Point(180, y), Size = new Size(controlW, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbLevel.Items.AddRange(new object[] { 1, 2, 3 });
            cmbLevel.SelectedIndex = 0;
            y += 35;

            // Текст вопроса
            Label lblQuestion = new Label { Text = "Текст вопроса:", Location = new Point(20, y), Size = new Size(labelW, 25) };
            txtQuestion = new TextBox { Location = new Point(180, y), Size = new Size(controlW, 60), Multiline = true };
            y += 75;

            // Тип вопроса
            Label lblType = new Label { Text = "Тип вопроса:", Location = new Point(20, y), Size = new Size(labelW, 25) };
            cmbType = new ComboBox { Location = new Point(180, y), Size = new Size(controlW, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbType.Items.AddRange(new object[] { "grammar", "stress", "ending" });
            cmbType.SelectedIndex = 0;
            y += 35;

            // Правильный ответ
            Label lblCorrect = new Label { Text = "Правильный ответ:", Location = new Point(20, y), Size = new Size(labelW, 25) };
            txtCorrect = new TextBox { Location = new Point(180, y), Size = new Size(controlW, 25) };
            y += 35;

            // Изображение
            Label lblImage = new Label { Text = "Изображение:", Location = new Point(20, y), Size = new Size(labelW, 25) };
            txtImagePath = new TextBox { Location = new Point(180, y), Size = new Size(controlW - 100, 25), ReadOnly = true };
            btnSelectImage = new Button { Text = "Выбрать", Location = new Point(180 + controlW - 90, y), Size = new Size(80, 25) };
            btnSelectImage.Click += BtnSelectImage_Click;
            y += 35;

            // Варианты ответов
            Label lblAnswers = new Label { Text = "Варианты ответов:", Location = new Point(20, y), Size = new Size(labelW, 25) };
            y += 30;

            lstAnswers = new ListBox { Location = new Point(20, y), Size = new Size(300, 100) };

            TextBox txtNewAnswer = new TextBox { Location = new Point(330, y), Size = new Size(150, 25) };
            btnAddAnswer = new Button { Text = "Добавить", Location = new Point(490, y), Size = new Size(80, 25) };
            btnAddAnswer.Click += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(txtNewAnswer.Text))
                {
                    lstAnswers.Items.Add(txtNewAnswer.Text);
                    txtNewAnswer.Clear();
                }
            };
            y += 110;

            // Кнопка удаления ответа
            Button btnRemoveAnswer = new Button { Text = "Удалить выбранный", Location = new Point(20, y), Size = new Size(150, 30) };
            btnRemoveAnswer.Click += (s, e) =>
            {
                if (lstAnswers.SelectedItem != null)
                    lstAnswers.Items.Remove(lstAnswers.SelectedItem);
            };
            y += 45;

            // Кнопка сохранения
            btnSave = new Button
            {
                Text = "Сохранить вопрос",
                Location = new Point(200, y),
                Size = new Size(200, 40),
                BackColor = Color.LightGreen,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            btnSave.Click += BtnSave_Click;

            this.Controls.Add(lblTopic);
            this.Controls.Add(txtTopic);
            this.Controls.Add(lblLevel);
            this.Controls.Add(cmbLevel);
            this.Controls.Add(lblQuestion);
            this.Controls.Add(txtQuestion);
            this.Controls.Add(lblType);
            this.Controls.Add(cmbType);
            this.Controls.Add(lblCorrect);
            this.Controls.Add(txtCorrect);
            this.Controls.Add(lblImage);
            this.Controls.Add(txtImagePath);
            this.Controls.Add(btnSelectImage);
            this.Controls.Add(lblAnswers);
            this.Controls.Add(lstAnswers);
            this.Controls.Add(txtNewAnswer);
            this.Controls.Add(btnAddAnswer);
            this.Controls.Add(btnRemoveAnswer);
            this.Controls.Add(btnSave);
        }

        private void BtnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            ofd.InitialDirectory = Application.StartupPath + "\\Images";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = ofd.FileName;
                txtImagePath.Text = selectedImagePath;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTopic.Text))
                {
                    MessageBox.Show("Введите название темы!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtQuestion.Text))
                {
                    MessageBox.Show("Введите текст вопроса!");
                    return;
                }
                if (lstAnswers.Items.Count == 0)
                {
                    MessageBox.Show("Добавьте хотя бы один вариант ответа!");
                    return;
                }

                string xmlPath = "Questions.xml";
                XDocument doc;

                if (System.IO.File.Exists(xmlPath))
                {
                    doc = XDocument.Load(xmlPath);
                }
                else
                {
                    doc = new XDocument(new XElement("russian_language"));
                }

                // Поиск или создание темы
                var topicNode = doc.Root.Elements("topic")
                                   .FirstOrDefault(t => (string)t.Attribute("name") == txtTopic.Text);

                if (topicNode == null)
                {
                    topicNode = new XElement("topic", new XAttribute("name", txtTopic.Text));
                    doc.Root.Add(topicNode);
                }

                // Поиск или создание уровня
                int level = (int)cmbLevel.SelectedItem;
                var levelNode = topicNode.Elements("level")
                                        .FirstOrDefault(l => (int)l.Attribute("difficulty") == level);

                if (levelNode == null)
                {
                    levelNode = new XElement("level", new XAttribute("difficulty", level));
                    topicNode.Add(levelNode);
                }

                // Создание вопроса
                XElement questionNode = new XElement("question",
                    new XAttribute("text", txtQuestion.Text),
                    new XAttribute("type", cmbType.SelectedItem.ToString()),
                    new XAttribute("correct", txtCorrect.Text),
                    new XElement("image", new XAttribute("src", selectedImagePath))
                );

                foreach (string answer in lstAnswers.Items)
                {
                    questionNode.Add(new XElement("answer", answer));
                }

                levelNode.Add(questionNode);
                doc.Save(xmlPath);

                MessageBox.Show("Вопрос успешно сохранён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Очистка полей
                txtQuestion.Clear();
                txtCorrect.Clear();
                txtImagePath.Clear();
                lstAnswers.Items.Clear();
                selectedImagePath = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }
    }
}