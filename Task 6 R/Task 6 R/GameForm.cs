using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Task_6_R
{
    public partial class GameForm : Form
    {
        private string selectedTopic;
        private int currentLevel = 1;
        private int currentScore = 0;
        private int questionsPerSession = 5;
        private List<Question> currentQuestions = new List<Question>();
        private int currentQuestionIndex = 0;
        private Question currentQuestion;
        private Timer gameTimer;
        private int timeLeft = 60;

        // UI элементы
        private Label lblTopic;
        private Label lblLevel;
        private Label lblScore;
        private Label lblTimer;
        private Label lblQuestion;
        private PictureBox pictureBox;
        private Panel answerPanel;
        private TextBox txtAnswer;
        private Button btnSubmit;
        private Button btnNext;
        private Label lblResult;
        private Label lblInstruction;

        public GameForm(string topic)
        {
            selectedTopic = topic;
            InitializeComponent();
            SetupUI();
            LoadLevel();
        }

        private void SetupUI()
        {
            this.Text = $"Тестирование: {selectedTopic}";
            this.Size = new Size(800, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            // Верхняя панель информации
            lblTopic = new Label
            {
                Text = $"Тема: {selectedTopic}",
                Location = new Point(20, 20),
                Size = new Size(200, 25),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            lblLevel = new Label
            {
                Text = $"Уровень: {currentLevel}",
                Location = new Point(250, 20),
                Size = new Size(150, 25),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            lblScore = new Label
            {
                Text = $"Баллы: {currentScore}",
                Location = new Point(450, 20),
                Size = new Size(150, 25),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            lblTimer = new Label
            {
                Text = $"Время: {timeLeft} сек",
                Location = new Point(620, 20),
                Size = new Size(100, 25),
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.Red
            };

            // Инструкция
            lblInstruction = new Label
            {
                Location = new Point(20, 60),
                Size = new Size(740, 30),
                Font = new Font("Arial", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Blue
            };

            // Вопрос
            lblQuestion = new Label
            {
                Location = new Point(20, 100),
                Size = new Size(740, 80),
                Font = new Font("Arial", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Картинка
            pictureBox = new PictureBox
            {
                Location = new Point(275, 190),
                Size = new Size(250, 200),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray
            };

            // Панель для ответов
            answerPanel = new Panel
            {
                Location = new Point(20, 410),
                Size = new Size(740, 120),
                AutoScroll = true,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Текстовое поле (для грамматики и окончаний)
            txtAnswer = new TextBox
            {
                Location = new Point(150, 550),
                Size = new Size(300, 30),
                Font = new Font("Arial", 12),
                Visible = false
            };

            // Кнопка отправки
            btnSubmit = new Button
            {
                Text = "Проверить ответ",
                Location = new Point(470, 548),
                Size = new Size(150, 35),
                BackColor = Color.LightGreen,
                Visible = false
            };
            btnSubmit.Click += BtnSubmit_Click;

            // Кнопка далее
            btnNext = new Button
            {
                Text = "Следующий вопрос →",
                Location = new Point(300, 590),
                Size = new Size(200, 35),
                BackColor = Color.LightBlue,
                Visible = false
            };
            btnNext.Click += BtnNext_Click;

            lblResult = new Label
            {
                Location = new Point(20, 550),
                Size = new Size(740, 30),
                Font = new Font("Arial", 11),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Blue
            };

            this.Controls.Add(lblTopic);
            this.Controls.Add(lblLevel);
            this.Controls.Add(lblScore);
            this.Controls.Add(lblTimer);
            this.Controls.Add(lblInstruction);
            this.Controls.Add(lblQuestion);
            this.Controls.Add(pictureBox);
            this.Controls.Add(answerPanel);
            this.Controls.Add(txtAnswer);
            this.Controls.Add(btnSubmit);
            this.Controls.Add(btnNext);
            this.Controls.Add(lblResult);
        }

        private void LoadLevel()
        {
            try
            {
                string xmlPath = System.IO.Path.Combine(Application.StartupPath, "Questions.xml");
                if (!System.IO.File.Exists(xmlPath))
                {
                    MessageBox.Show("Файл Questions.xml не найден!");
                    this.Close();
                    return;
                }

                XDocument doc = XDocument.Load(xmlPath);
                var topicNode = doc.Root.Elements("topic")
                                 .FirstOrDefault(t => (string)t.Attribute("name") == selectedTopic);

                if (topicNode == null)
                {
                    MessageBox.Show("Тема не найдена!");
                    this.Close();
                    return;
                }

                var levelNode = topicNode.Elements("level")
                                        .FirstOrDefault(l => (int)l.Attribute("difficulty") == currentLevel);

                if (levelNode == null)
                {
                    MessageBox.Show($"Уровень {currentLevel} не найден! Вы завершили все уровни!\nИтоговый счёт: {currentScore}");
                    this.Close();
                    return;
                }

                // Загружаем все вопросы уровня
                var allQuestions = new List<Question>();
                foreach (var qNode in levelNode.Elements("question"))
                {
                    Question q = new Question
                    {
                        Text = qNode.Attribute("text")?.Value,
                        Type = qNode.Attribute("type")?.Value,
                        CorrectAnswer = qNode.Attribute("correct")?.Value,
                        ImagePath = qNode.Element("image")?.Attribute("src")?.Value ?? ""
                    };
                    foreach (var aNode in qNode.Elements("answer"))
                    {
                        q.Answers.Add(aNode.Value);
                    }
                    allQuestions.Add(q);
                }

                if (allQuestions.Count == 0)
                {
                    MessageBox.Show("В этом уровне нет вопросов!");
                    this.Close();
                    return;
                }

                // Берём случайные вопросы
                Random rnd = new Random();
                currentQuestions = allQuestions.OrderBy(x => rnd.Next()).Take(questionsPerSession).ToList();

                currentQuestionIndex = 0;
                currentScore = 0;
                UpdateScoreDisplay();

                // Запуск таймера
                timeLeft = 60;
                if (gameTimer != null) gameTimer.Dispose();
                gameTimer = new Timer();
                gameTimer.Interval = 1000;
                gameTimer.Tick += GameTimer_Tick;
                gameTimer.Start();

                LoadQuestion();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки уровня: {ex.Message}");
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            lblTimer.Text = $"Время: {timeLeft} сек";

            if (timeLeft <= 0)
            {
                gameTimer.Stop();
                MessageBox.Show("Время вышло! Игра окончена.");
                ShowLevelResult();
            }
        }

        private void LoadQuestion()
        {
            if (currentQuestionIndex >= currentQuestions.Count)
            {
                gameTimer.Stop();
                ShowLevelResult();
                return;
            }

            currentQuestion = currentQuestions[currentQuestionIndex];
            lblQuestion.Text = currentQuestion.Text;

            // Настройка инструкции в зависимости от типа вопроса
            switch (currentQuestion.Type)
            {
                case "grammar":
                    lblInstruction.Text = "📝 Вставьте пропущенную букву в текстовое поле и нажмите 'Проверить ответ'";
                    lblInstruction.ForeColor = Color.DarkBlue;
                    break;
                case "ending":
                    lblInstruction.Text = "✏️ Вставьте правильное окончание в текстовое поле и нажмите 'Проверить ответ'";
                    lblInstruction.ForeColor = Color.DarkGreen;
                    break;
                case "stress":
                    lblInstruction.Text = "🎯 Нажмите на УДАРНУЮ гласную букву в слове (она выделена в вопросе)";
                    lblInstruction.ForeColor = Color.DarkRed;
                    break;
            }

            // Загрузка изображения
            if (!string.IsNullOrEmpty(currentQuestion.ImagePath) && System.IO.File.Exists(currentQuestion.ImagePath))
            {
                try
                {
                    pictureBox.Image = Image.FromFile(currentQuestion.ImagePath);
                    pictureBox.Visible = true;
                }
                catch
                {
                    pictureBox.Image = null;
                    pictureBox.Visible = false;
                }
            }
            else
            {
                pictureBox.Image = null;
                pictureBox.Visible = false;
            }

            // Очищаем панель ответов
            answerPanel.Controls.Clear();
            txtAnswer.Visible = false;
            btnSubmit.Visible = false;
            btnNext.Visible = false;
            txtAnswer.Text = "";
            txtAnswer.Enabled = true;
            lblResult.Text = "";

            // Настройка интерфейса в зависимости от типа вопроса
            if (currentQuestion.Type == "grammar" || currentQuestion.Type == "ending")
            {
                SetupTextInput();
            }
            else if (currentQuestion.Type == "stress")
            {
                SetupStressInput();
            }
        }

        private void SetupTextInput()
        {
            txtAnswer.Visible = true;
            btnSubmit.Visible = true;
            txtAnswer.Focus();
        }

        private void SetupStressInput()
        {
            // Для ударений создаем кнопки с вариантами ответов (гласные буквы)
            // Вопрос содержит слово с выделенной ударной гласной, например "звОнит"
            // Нужно определить все гласные в слове и сделать их кликабельными

            string word = currentQuestion.Text;
            char[] vowels = { 'а', 'о', 'у', 'ы', 'э', 'я', 'ё', 'ю', 'и', 'е', 'А', 'О', 'У', 'Ы', 'Э', 'Я', 'Ё', 'Ю', 'И', 'Е' };

            // Находим все гласные в слове
            var vowelPositions = new List<int>();
            for (int i = 0; i < word.Length; i++)
            {
                if (vowels.Contains(word[i]))
                {
                    vowelPositions.Add(i);
                }
            }

            if (vowelPositions.Count == 0)
            {
                // Если нет гласных, показываем сообщение
                Label errorLabel = new Label
                {
                    Text = "Ошибка: в слове нет гласных букв!",
                    Location = new Point(10, 10),
                    Size = new Size(700, 30),
                    Font = new Font("Arial", 12),
                    ForeColor = Color.Red
                };
                answerPanel.Controls.Add(errorLabel);
                return;
            }

            // Создаем панель для отображения слова с кнопками на гласных
            int currentX = 10;
            int currentY = 10;

            for (int i = 0; i < word.Length; i++)
            {
                char c = word[i];
                bool isVowel = vowels.Contains(c);

                if (isVowel)
                {
                    // Создаем кнопку для гласной
                    Button vowelBtn = new Button
                    {
                        Text = c.ToString(),
                        Size = new Size(60, 60),
                        Location = new Point(currentX, currentY),
                        Font = new Font("Arial", 18, FontStyle.Bold),
                        BackColor = Color.Yellow,
                        Tag = c.ToString().ToLower(),
                        FlatStyle = FlatStyle.Flat
                    };
                    vowelBtn.Click += (s, e) =>
                    {
                        Button btn = s as Button;
                        CheckStressAnswer(btn.Tag.ToString());
                    };
                    answerPanel.Controls.Add(vowelBtn);
                    currentX += 65;
                }
                else
                {
                    // Создаем Label для согласной
                    Label consLabel = new Label
                    {
                        Text = c.ToString(),
                        Size = new Size(50, 60),
                        Location = new Point(currentX, currentY),
                        Font = new Font("Arial", 18, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter,
                        BackColor = Color.LightGray
                    };
                    answerPanel.Controls.Add(consLabel);
                    currentX += 55;
                }
            }

            // Добавляем пояснение
            Label hintLabel = new Label
            {
                Text = "👆 Нажмите на ударную гласную букву в слове выше",
                Location = new Point(10, 80),
                Size = new Size(700, 30),
                Font = new Font("Arial", 10),
                ForeColor = Color.Gray
            };
            answerPanel.Controls.Add(hintLabel);
        }

        private void CheckStressAnswer(string selectedVowel)
        {
            bool isCorrect = (selectedVowel == currentQuestion.CorrectAnswer.ToLower());

            if (isCorrect)
            {
                currentScore += 20;
                lblResult.Text = "✓ Правильно! +20 баллов";
                lblResult.ForeColor = Color.Green;
            }
            else
            {
                lblResult.Text = $"✗ Неправильно! Правильный ответ: буква '{currentQuestion.CorrectAnswer.ToUpper()}'";
                lblResult.ForeColor = Color.Red;
            }

            UpdateScoreDisplay();

            // Блокируем все кнопки после ответа
            foreach (Control control in answerPanel.Controls)
            {
                if (control is Button)
                {
                    control.Enabled = false;
                }
            }

            btnNext.Visible = true;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAnswer.Text))
            {
                MessageBox.Show("Введите ответ!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string userAnswer = txtAnswer.Text.Trim().ToLower();
            bool isCorrect = (userAnswer == currentQuestion.CorrectAnswer.ToLower());

            if (isCorrect)
            {
                currentScore += 20;
                lblResult.Text = "✓ Правильно! +20 баллов";
                lblResult.ForeColor = Color.Green;
            }
            else
            {
                lblResult.Text = $"✗ Неправильно! Правильный ответ: {currentQuestion.CorrectAnswer}";
                lblResult.ForeColor = Color.Red;
            }

            UpdateScoreDisplay();

            txtAnswer.Enabled = false;
            btnSubmit.Visible = false;
            btnNext.Visible = true;
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            currentQuestionIndex++;
            btnNext.Visible = false;
            LoadQuestion();
        }

        private void UpdateScoreDisplay()
        {
            lblScore.Text = $"Баллы: {currentScore}";
        }

        private void ShowLevelResult()
        {
            gameTimer?.Stop();

            bool passed = currentScore >= 80;
            string message = $"Вы набрали {currentScore} баллов из 100.\n";

            if (passed && currentLevel < 3)
            {
                message += $"Поздравляем! Вы переходите на уровень {currentLevel + 1}.";
                DialogResult result = MessageBox.Show(message, "Результат уровня", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    currentLevel++;
                    currentScore = 0;
                    LoadLevel();
                }
                else
                {
                    this.Close();
                }
            }
            else if (passed && currentLevel == 3)
            {
                message += "Поздравляем! Вы прошли все уровни!";
                MessageBox.Show(message, "Победа!", MessageBoxButtons.OK);
                this.Close();
            }
            else
            {
                message += $"Вы не набрали 80 баллов. Уровень {currentLevel} не пройден.";
                MessageBox.Show(message, "Уровень не пройден", MessageBoxButtons.OK);
                this.Close();
            }
        }
    }
}