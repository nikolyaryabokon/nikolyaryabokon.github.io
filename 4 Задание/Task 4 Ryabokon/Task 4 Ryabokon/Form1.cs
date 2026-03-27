using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Task_4_Ryabokon
{
    public partial class Form1 : Form
    {
        private ArrayParticipant participants;

        public Form1()
        {
            InitializeComponent();
            participants = new ArrayParticipant();
        }

        // Добавление участника
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    MessageBox.Show("Введите фамилию участника!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string lastName = txtLastName.Text.Trim();
                double distance = (double)numDistance.Value;
                double time = (double)numTime.Value;
                int misses = (int)numMisses.Value;

                Participant participant = new Participant(lastName, distance, time, misses);
                participants.Add(participant);

                UpdateDataGridView();
                ClearInputFields();

                MessageBox.Show("Участник успешно добавлен!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обновление таблицы
        private void UpdateDataGridView()
        {
            dgvParticipants.Rows.Clear();

            for (int i = 0; i < participants.Count; i++)
            {
                Participant p = participants[i];
                dgvParticipants.Rows.Add(
                    p.LastName,
                    p.Distance,
                    p.Time,
                    p.Misses,
                    p.TotalTime
                );
            }
        }

        // Очистка полей ввода
        private void ClearInputFields()
        {
            txtLastName.Clear();
            numDistance.Value = 0;
            numTime.Value = 0;
            numMisses.Value = 0;
            txtLastName.Focus();
        }

        // Поиск лучшего результата
        private void btnFindBest_Click(object sender, EventArgs e)
        {
            if (participants.Count == 0)
            {
                MessageBox.Show("Нет данных для поиска!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Participant best = participants.GetBestParticipant();
            MessageBox.Show($"Лучший результат:\n\nФамилия: {best.LastName}\n" +
                $"Дистанция: {best.Distance} м\nВремя: {best.Time} сек\n" +
                $"Промахи: {best.Misses}\nИтоговое время: {best.TotalTime:F2} сек",
                "Лучший участник", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Сохранение в файл
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (participants.Count == 0)
            {
                MessageBox.Show("Нет данных для сохранения!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Title = "Сохранить результаты";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    participants.SaveToFile(saveFileDialog.FileName);
                    MessageBox.Show("Данные успешно сохранены!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Загрузка из файла
        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileDialog.Title = "Загрузить результаты";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    participants.LoadFromFile(openFileDialog.FileName);
                    UpdateDataGridView();
                    UpdateChart();
                    MessageBox.Show("Данные успешно загружены!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Обновление диаграммы при переключении на вкладку
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageChart)
            {
                UpdateChart();
            }
        }

        // Построение столбчатой диаграммы
        private void UpdateChart()
        {
            if (participants.Count == 0)
            {
                chartResults.Series.Clear();
                chartResults.Titles.Clear();
                chartResults.Titles.Add("Нет данных для отображения");
                return;
            }

            chartResults.Series.Clear();
            chartResults.Titles.Clear();

            // Настройка диаграммы
            chartResults.BackColor = Color.Gray;
            chartResults.BackSecondaryColor = Color.WhiteSmoke;
            chartResults.BackGradientStyle = GradientStyle.DiagonalRight;
            chartResults.BorderlineDashStyle = ChartDashStyle.Solid;
            chartResults.BorderlineColor = Color.Gray;

            // Настройка области диаграммы
            if (chartResults.ChartAreas.Count == 0)
                chartResults.ChartAreas.Add(new ChartArea());
            chartResults.ChartAreas[0].BackColor = Color.White;
            chartResults.ChartAreas[0].AxisX.Title = "Участники";
            chartResults.ChartAreas[0].AxisY.Title = "Итоговое время (сек)";

            // Добавление заголовка
            chartResults.Titles.Add("Сравнение участников по итоговому времени");
            chartResults.Titles[0].Font = new Font("Arial", 16, FontStyle.Bold);

            // Добавление серии
            Series series = new Series("Итоговое время");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.SteelBlue;

            // Заполнение данными
            for (int i = 0; i < participants.Count; i++)
            {
                series.Points.AddXY(participants[i].LastName, participants[i].TotalTime);
            }

            chartResults.Series.Add(series);
        }

        // Подписка на событие изменения вкладки
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
        }
    }

    // Класс участника
    public class Participant
    {
        private string lastName;
        private double distance;
        private double time;
        private int misses;

        public Participant(string lastName, double distance, double time, int misses)
        {
            this.lastName = lastName;
            this.distance = distance;
            this.time = time;
            this.misses = misses;
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public double Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public double Time
        {
            get { return time; }
            set { time = value; }
        }

        public int Misses
        {
            get { return misses; }
            set { misses = value; }
        }

        // Итоговое время с учетом штрафных минут (1 минута = 60 сек за промах)
        public double TotalTime
        {
            get { return time + (misses * 60); }
        }
    }

    // Класс для работы с массивом участников
    public class ArrayParticipant
    {
        private List<Participant> participants;

        public ArrayParticipant()
        {
            participants = new List<Participant>();
        }

        public ArrayParticipant(string fileName)
        {
            participants = new List<Participant>();
            LoadFromFile(fileName);
        }

        public void Add(Participant p)
        {
            participants.Add(p);
        }

        public int Count
        {
            get { return participants.Count; }
        }

        public Participant this[int index]
        {
            get
            {
                if (index >= 0 && index < participants.Count)
                    return participants[index];
                return null;
            }
        }

        // Поиск участника с лучшим результатом (наименьшее итоговое время)
        public Participant GetBestParticipant()
        {
            if (participants.Count == 0)
                throw new Exception("Нет участников для поиска!");

            Participant best = participants[0];
            for (int i = 1; i < participants.Count; i++)
            {
                if (participants[i].TotalTime < best.TotalTime)
                    best = participants[i];
            }
            return best;
        }

        // Сохранение в файл
        public void SaveToFile(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("Биатлонные соревнования");
                writer.WriteLine($"Количество участников: {participants.Count}");
                writer.WriteLine("Фамилия;Дистанция(м);Время(сек);Промахи;ИтоговоеВремя(сек)");

                foreach (Participant p in participants)
                {
                    writer.WriteLine($"{p.LastName};{p.Distance};{p.Time};{p.Misses};{p.TotalTime:F2}");
                }
            }
        }

        // Загрузка из файла
        public void LoadFromFile(string fileName)
        {
            participants.Clear();

            using (StreamReader reader = new StreamReader(fileName))
            {
                // Пропускаем заголовок
                string line = reader.ReadLine();
                line = reader.ReadLine();
                line = reader.ReadLine();

                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(';');
                    if (parts.Length >= 4)
                    {
                        string lastName = parts[0];
                        double distance = double.Parse(parts[1]);
                        double time = double.Parse(parts[2]);
                        int misses = int.Parse(parts[3]);

                        participants.Add(new Participant(lastName, distance, time, misses));
                    }
                }
            }
        }
    }
}