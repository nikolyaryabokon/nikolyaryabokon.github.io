using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Task_6_R
{
    public partial class MainForm : Form
    {
        private List<string> topics = new List<string>();
        private FlowLayoutPanel flowPanel;
        private Button btnAdmin;
        private Label lblTitle;

        public MainForm()
        {
            InitializeComponent();
            LoadTopics();
            SetupUI();
        }

        private void LoadTopics()
        {
            try
            {
                XDocument doc = XDocument.Load("Questions.xml");
                topics = doc.Root.Elements("topic")
                            .Select(t => t.Attribute("name")?.Value)
                            .Where(name => name != null)
                            .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки XML: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Создаём темы по умолчанию, если файла нет
                topics = new List<string> { "Грамматика", "Ударения", "Окончания" };
            }
        }

        private void SetupUI()
        {
            this.Text = "Изучение русского языка - Главное меню";
            this.Size = new Size(500, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightBlue;

            lblTitle = new Label
            {
                Text = "Выберите тему для тестирования",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(50, 30),
                Size = new Size(400, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            flowPanel = new FlowLayoutPanel
            {
                Location = new Point(50, 100),
                Size = new Size(400, 200),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };

            foreach (string topic in topics)
            {
                Button btn = new Button
                {
                    Text = topic,
                    Size = new Size(380, 50),
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    BackColor = Color.White,
                    Tag = topic,
                    Margin = new Padding(0, 0, 0, 10)
                };
                btn.Click += TopicButton_Click;
                flowPanel.Controls.Add(btn);
            }

            btnAdmin = new Button
            {
                Text = "Панель администратора",
                Size = new Size(180, 40),
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.Gold,
                Location = new Point(50, 330)
            };
            btnAdmin.Click += BtnAdmin_Click;

            Button btnExit = new Button
            {
                Text = "Выход",
                Size = new Size(180, 40),
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.LightCoral,
                Location = new Point(270, 330)
            };
            btnExit.Click += (s, e) => Application.Exit();

            this.Controls.Add(lblTitle);
            this.Controls.Add(flowPanel);
            this.Controls.Add(btnAdmin);
            this.Controls.Add(btnExit);
        }

        private void TopicButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string selectedTopic = btn.Tag.ToString();
            GameForm gameForm = new GameForm(selectedTopic);
            gameForm.ShowDialog();
        }

        private void BtnAdmin_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();
            adminForm.ShowDialog();
            // Обновляем список тем после закрытия админки
            LoadTopics();
            flowPanel.Controls.Clear();
            foreach (string topic in topics)
            {
                Button btn = new Button
                {
                    Text = topic,
                    Size = new Size(380, 50),
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    BackColor = Color.White,
                    Tag = topic,
                    Margin = new Padding(0, 0, 0, 10)
                };
                btn.Click += TopicButton_Click;
                flowPanel.Controls.Add(btn);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}