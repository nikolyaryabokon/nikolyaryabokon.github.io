using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Task_1_Ryabokon
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            buttonCompute.Enabled = false; // Изначально кнопка заблокирована
        }

        private void ComputeCh()
        {
            try
            {
                // Получение данных из полей ввода
                double x = Convert.ToDouble(textBoxX.Text);
                double eps = Convert.ToDouble(textBoxEps.Text);

                // Проверка корректности точности
                if (eps <= 0 || eps >= 1)
                {
                    MessageBox.Show("Точность должна быть в интервале (0, 1)",
                        "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

             
                double sum = 0;          
                double term = 1.0;         
                int n = 0;                 
                int count = 0;            

                while (Math.Abs(term) >= eps)
                {
                    sum += term;              // Добавляем текущий член к сумме
                    count++;                   // Увеличиваем счетчик

                    // Вычисляем следующий член ряда по рекуррентной формуле
                    // a(n+1) = a(n) * x^2 / ((2n+1)*(2n+2))
                    term *= (x * x) / ((2 * n + 1) * (2 * n + 2));
                    n++;                        // Переходим к следующему номеру члена

                    // Защита от бесконечного цикла (максимум 1000 итераций)
                    if (count > 1000)
                    {
                        MessageBox.Show("Достигнут лимит итераций. Возможно, ряд расходится.",
                            "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }


                if (count <= 1000)
                {
                    sum += term;
                    count++;
                }

                // Вычисление значения встроенной функцией для сравнения
                double mathCosh = Math.Cosh(x);

                // Вывод результатов
                labelResult.Text =
                                   $"Вычисленные значения:\n" +
                                   $"  Сумма ряда S(x) = {sum:F10}\n" +
                                   $"  Math.Cosh({x:F3}) = {mathCosh:F10}\n" +
                                   $"  Абсолютная погрешность = {Math.Abs(mathCosh - sum):E10}\n" +
                                   $"  Относительная погрешность = {Math.Abs((mathCosh - sum) / mathCosh):E10}\n\n" +
                                   $"  Количество просуммированных членов: {count}";
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка формата данных. Проверьте правильность ввода чисел.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Число слишком большое или слишком маленькое.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Необработанная ошибка: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void textBoxX_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем управляющие клавиши (backspace, delete и т.д.)
            if (char.IsControl(e.KeyChar))
                return;

            // Разрешаем цифры
            if (char.IsDigit(e.KeyChar))
                return;

            // Разрешаем запятую (десятичный разделитель)
            if (e.KeyChar == ',')
            {
                // Проверяем, что запятая еще не введена
                System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;
                if (textBox.Text.Contains(','))
                {
                    e.Handled = true; // Запрещаем вторую запятую
                }
                return;
            }

            // Разрешаем минус (только если это первый символ)
            if (e.KeyChar == '-')
            {
                System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;
                if (textBox.Text.Length > 0 || textBox.SelectionStart > 0)
                {
                    e.Handled = true; // Минус только в начале
                }
                return;
            }

            // Все остальные символы запрещены
            e.Handled = true;
        }

      
        private void textBoxEps_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем управляющие клавиши
            if (char.IsControl(e.KeyChar))
                return;

            // Разрешаем цифры
            if (char.IsDigit(e.KeyChar))
                return;

            // Разрешаем запятую (десятичный разделитель)
            if (e.KeyChar == ',')
            {
                System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;
                if (textBox.Text.Contains(','))
                {
                    e.Handled = true; // Запрещаем вторую запятую
                }
                return;
            }

            e.Handled = true;
        }


        private void TextBox_TextChanged(object sender, EventArgs e)
        {

            buttonCompute.Enabled = !string.IsNullOrWhiteSpace(textBoxX.Text) &&
                                    !string.IsNullOrWhiteSpace(textBoxEps.Text);
        }


        private void buttonCompute_Click(object sender, EventArgs e)
        {
            ComputeCh();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // Устанавливаем начальные подсказки
            textBoxX.Text = "";
            textBoxEps.Text = "";
            labelResult.Text = "Введите данные и нажмите 'Вычислить'...";
        }

        private void labelResult_Click(object sender, EventArgs e)
        {

        }

        private void groupBoxInput_Enter(object sender, EventArgs e)
        {

        }

        private void groupBoxOutput_Enter(object sender, EventArgs e)
        {

        }

        private void labelX_Click(object sender, EventArgs e)
        {

        }
    }
}