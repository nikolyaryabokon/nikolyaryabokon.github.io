using System;
using System.Drawing;
using System.Windows.Forms;

namespace Task_3_Ryabokon
{
    public partial class Form1 : Form
    {
        private int radius = 50;
        private int x, y;
        private int dx = 5; // скорость по X
        private Color circleColor = Color.Blue;
        private Timer timer;
        private int minRadius = 10;
        private bool isMovingRight = true;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Resize += Form1_Resize;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;

            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += Timer_Tick;
            timer.Start();

            x = 0;
            y = this.ClientSize.Height / 2 - radius;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            using (Brush brush = new SolidBrush(circleColor))
            {
                e.Graphics.FillEllipse(brush, x, y, radius * 2, radius * 2);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (isMovingRight)
                x += dx;
            else
                x -= dx;

            // Проверка границ
            if (x + radius * 2 >= this.ClientSize.Width)
            {
                isMovingRight = false;
                ChangeColorAndShrink();
            }
            else if (x <= 0)
            {
                isMovingRight = true;
                ChangeColorAndShrink();
            }

            // Остановка при достижении минимального радиуса
            if (radius <= minRadius)
            {
                timer.Stop();
            }

            Invalidate();
        }

        private void ChangeColorAndShrink()
        {
            Random rand = new Random();
            circleColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            radius = Math.Max(minRadius, radius - 5);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Корректировка y, чтобы круг оставался по центру
            y = this.ClientSize.Height / 2 - radius;
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            }
        }


        private void btnSettings_Click(object sender, EventArgs e)
        {
            timer.Stop(); // Останавливаем таймер

            // Правильное создание формы с параметрами
            Form2 settingsForm = new Form2(circleColor, timer.Interval);

            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                circleColor = settingsForm.SelectedColor;
                timer.Interval = settingsForm.SpeedInterval;
                Invalidate();
            }

            timer.Start(); // Запускаем таймер обратно
        }
    }
}