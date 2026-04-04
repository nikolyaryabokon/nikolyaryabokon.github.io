using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Task8R
{
    public partial class Form1 : Form
    {
        private string currentPlayer = "";
        private string difficulty = "Средний";
        private Color mapColor = Color.SandyBrown;

        private Dictionary<string, List<string>> maps;
        private Dictionary<string, Dictionary<Tuple<string, string>, int>> travelTimes;
        private Dictionary<string, List<List<string>>> optimalRoutes;

        private List<string> currentPoints;
        private Dictionary<Tuple<string, string>, int> currentTimes;
        private List<List<string>> currentOptimalRoutes;

        public Form1()
        {
            InitializeComponent();
            InitializeGameData();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowAuth();
        }

        private void ShowAuth()
        {
            Form authForm = new Form();
            authForm.Text = "Авторизация";
            authForm.Size = new Size(320, 160);
            authForm.StartPosition = FormStartPosition.CenterParent;
            authForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            authForm.MaximizeBox = false;
            authForm.MinimizeBox = false;

            Label lblName = new Label();
            lblName.Text = "Введите ваше имя:";
            lblName.Location = new Point(20, 20);
            lblName.Size = new Size(150, 25);
            lblName.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);

            TextBox txtName = new TextBox();
            txtName.Location = new Point(20, 50);
            txtName.Size = new Size(260, 25);
            txtName.Font = new Font("Microsoft Sans Serif", 10);

            Button btnOk = new Button();
            btnOk.Text = "Начать игру";
            btnOk.Location = new Point(20, 85);
            btnOk.Size = new Size(120, 35);
            btnOk.BackColor = Color.LightGreen;
            btnOk.FlatStyle = FlatStyle.Flat;
            btnOk.Cursor = Cursors.Hand;

            string playerName = "";
            btnOk.Click += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Пожалуйста, введите ваше имя!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                playerName = txtName.Text.Trim();
                authForm.DialogResult = DialogResult.OK;
                authForm.Close();
            };

            authForm.Controls.AddRange(new Control[] { lblName, txtName, btnOk });

            if (authForm.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(playerName))
            {
                currentPlayer = playerName;
                this.Text = $"Спортивное ориентирование - Игрок: {currentPlayer}";
                LoadMap();
            }
            else
            {
                Application.Exit();
            }
        }

        private void InitializeGameData()
        {
            maps = new Dictionary<string, List<string>>
            {
                ["Лёгкий"] = new List<string> { "А", "Б", "В" },
                ["Средний"] = new List<string> { "А", "Б", "В", "Г" },
                ["Сложный"] = new List<string> { "А", "Б", "В", "Г", "Д" }
            };

            travelTimes = new Dictionary<string, Dictionary<Tuple<string, string>, int>>();
            optimalRoutes = new Dictionary<string, List<List<string>>>();
            Random rand = new Random();

            foreach (var diff in maps.Keys)
            {
                var points = maps[diff];
                var times = new Dictionary<Tuple<string, string>, int>();

                for (int i = 0; i < points.Count; i++)
                {
                    for (int j = i + 1; j < points.Count; j++)
                    {
                        int t = rand.Next(5, 21);
                        times[Tuple.Create(points[i], points[j])] = t;
                        times[Tuple.Create(points[j], points[i])] = t;
                    }
                }
                travelTimes[diff] = times;

                var routes = new List<List<string>>();
                int bestTime = int.MaxValue;

                foreach (var perm in GetPermutations(points, points.Count))
                {
                    var route = perm.ToList();
                    route.Add(points[0]);
                    int totalTime = 0;
                    for (int i = 0; i < route.Count - 1; i++)
                    {
                      //  totalTime += times[Tuple.Create(route[i], route[i + 1])];
                    }
                    if (totalTime < bestTime)
                    {
                        bestTime = totalTime;
                        routes.Clear();
                        routes.Add(route);
                    }
                    else if (totalTime == bestTime)
                    {
                        routes.Add(route);
                    }
                }
                optimalRoutes[diff] = routes;
            }
        }

        private IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        private void LoadMap()
        {
            currentPoints = maps[difficulty];
            currentTimes = travelTimes[difficulty];
            currentOptimalRoutes = optimalRoutes[difficulty];
            DrawMap();
        }

        private void DrawMap()
        {
            if (mapPanel == null) return;

            mapPanel.Controls.Clear();
            mapPanel.Refresh();

            int radius = 25;
            int centerX = mapPanel.Width / 2;
            int centerY = mapPanel.Height / 2;
            int radiusBig = 120;

            var angles = new Dictionary<string, double>();
            for (int i = 0; i < currentPoints.Count; i++)
            {
                double angle = 2 * Math.PI * i / currentPoints.Count;
                angles[currentPoints[i]] = angle;
                int x = centerX + (int)(radiusBig * Math.Cos(angle)) - radius;
                int y = centerY + (int)(radiusBig * Math.Sin(angle)) - radius;

                Button btn = new Button
                {
                    Text = currentPoints[i],
                    Location = new Point(x, y),
                    Size = new Size(radius * 2, radius * 2),
                    BackColor = mapColor,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    Enabled = false
                };
                mapPanel.Controls.Add(btn);
            }

            using (Graphics g = mapPanel.CreateGraphics())
            {
                g.Clear(Color.White);
                foreach (var p1 in currentPoints)
                {
                    foreach (var p2 in currentPoints)
                    {
                        if (string.Compare(p1, p2) < 0)
                        {
                            int x1 = centerX + (int)(radiusBig * Math.Cos(angles[p1]));
                            int y1 = centerY + (int)(radiusBig * Math.Sin(angles[p1]));
                            int x2 = centerX + (int)(radiusBig * Math.Cos(angles[p2]));
                            int y2 = centerY + (int)(radiusBig * Math.Sin(angles[p2]));
                            g.DrawLine(Pens.Black, x1, y1, x2, y2);

                            int mx = (x1 + x2) / 2;
                            int my = (y1 + y2) / 2;
                            int time = currentTimes[Tuple.Create(p1, p2)];
                            g.DrawString(time.ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Red, mx - 10, my - 10);
                        }
                    }
                }
            }
        }

        private void BtnCheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoute.Text))
            {
                MessageBox.Show("Введите маршрут!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string[] parts = txtRoute.Text.Trim().ToUpper().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> input = new List<string>(parts);

            if (input.Count < 2)
            {
                MessageBox.Show("Введите корректный маршрут!");
                return;
            }

            if (input[0] != input[input.Count - 1])
            {
                MessageBox.Show("Маршрут должен начинаться и заканчиваться в одной точке!");
                return;
            }

            bool valid = true;
            int totalTime = 0;
            for (int i = 0; i < input.Count - 1; i++)
            {
                var key = Tuple.Create(input[i], input[i + 1]);
                if (!currentTimes.ContainsKey(key))
                {
                    valid = false;
                    break;
                }
                totalTime += currentTimes[key];
            }

            if (!valid)
            {
                lblResult.Text = "❌ Неверный маршрут: некоторые точки не связаны!";
                lblResult.ForeColor = Color.Red;
                return;
            }

            bool isOptimal = currentOptimalRoutes.Any(route => route.SequenceEqual(input));
            string resultText = isOptimal ? "✅ Поздравляем! Маршрут оптимальный!" : "❌ Маршрут не оптимальный. Попробуйте снова!";
            lblResult.Text = $"{resultText}\nОбщее время: {totalTime} мин";
            lblResult.ForeColor = isOptimal ? Color.Green : Color.Orange;

            SaveResult(currentPlayer, difficulty, string.Join("-", input), totalTime, isOptimal);
        }

        private void SaveResult(string player, string diff, string route, int time, bool optimal)
        {
            List<string> results = new List<string>();
            string file = "results.txt";
            if (File.Exists(file))
            {
                results = File.ReadAllLines(file).ToList();
            }
            results.Add($"{DateTime.Now:dd.MM.yyyy HH:mm}|{player}|{diff}|{route}|{time}|{(optimal ? "Да" : "Нет")}");
            File.WriteAllLines(file, results);
        }

        private void ShowResults()
        {
            string file = "results.txt";
            if (!File.Exists(file))
            {
                MessageBox.Show("Нет сохранённых результатов.", "Результаты", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var results = File.ReadAllLines(file);
            var playerResults = results.Where(r => r.Split('|')[1] == currentPlayer).ToList();

            if (playerResults.Count == 0)
            {
                MessageBox.Show($"Нет результатов для игрока {currentPlayer}", "Результаты", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string message = $"=== Результаты игрока: {currentPlayer} ===\n\n";
            foreach (var res in playerResults)
            {
                var parts = res.Split('|');
                message += $"Дата: {parts[0]}\nСложность: {parts[2]}\nМаршрут: {parts[3]}\nВремя: {parts[4]} мин\nОптимальный: {parts[5]}\n\n---\n";
            }

            MessageBox.Show(message, "Результаты", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ChangeDifficulty(string newDifficulty)
        {
            difficulty = newDifficulty;
            LoadMap();
        }

        public void ChangeColor()
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mapColor = dialog.Color;
                DrawMap();
            }
        }
    }
}