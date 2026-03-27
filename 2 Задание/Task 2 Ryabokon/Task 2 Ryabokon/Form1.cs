using System;
using System.Windows.Forms;
using Task_2_Ryabokon;

namespace Task_2_Ryabokon

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Настройка ComboBox для выбора системы счисления
            cmbBaseSystem.Items.Add("Троичная (3)");
            cmbBaseSystem.Items.Add("Пятеричная (5)");
            cmbBaseSystem.Items.Add("Семеричная (7)");
            cmbBaseSystem.SelectedIndex = 0;

            // Настройка ComboBox для выбора операции
            cmbOperation.Items.Add("Сложение");
            cmbOperation.Items.Add("Вычитание");
            cmbOperation.SelectedIndex = 0;

            // Изначально скрываем второе число для вычитания
            lblNum2.Visible = true;
            txtNum2.Visible = true;
        }

        private void cmbBaseSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Очищаем поля при смене системы счисления
            txtNum1.Clear();
            txtNum2.Clear();
            txtResult.Clear();
            UpdateExample();
        }

        private void cmbOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Для вычитания показываем оба поля, для сложения - тоже оба (оба числа нужны)
            lblNum2.Visible = true;
            txtNum2.Visible = true;
            txtResult.Clear();
        }

        private void UpdateExample()
        {
            int baseSystem = GetBaseSystem();
            string example = "";

            switch (baseSystem)
            {
                case 3:
                    example = "Пример: 2101 + 12 = 2120";
                    break;
                case 5:
                    example = "Пример: 432 + 34 = 1021";
                    break;
                case 7:
                    example = "Пример: 654 + 123 = 1110";
                    break;
            }

            lblExample.Text = example;
        }

        private int GetBaseSystem()
        {
            switch (cmbBaseSystem.SelectedIndex)
            {
                case 0: return 3;
                case 1: return 5;
                case 2: return 7;
                default: return 3;
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                int baseSystem = GetBaseSystem();
                string num1 = txtNum1.Text.Trim();
                string num2 = txtNum2.Text.Trim();
                string result = "";

                // Проверка на пустые поля
                if (string.IsNullOrEmpty(num1) || string.IsNullOrEmpty(num2))
                {
                    MessageBox.Show("Введите оба числа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Проверка корректности первого числа
                if (!NumberConverter.IsValidNumber(num1, baseSystem))
                {
                    MessageBox.Show($"Число {num1} не является корректным в {GetBaseSystemName(baseSystem)} системе счисления",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Проверка корректности второго числа
                if (!NumberConverter.IsValidNumber(num2, baseSystem))
                {
                    MessageBox.Show($"Число {num2} не является корректным в {GetBaseSystemName(baseSystem)} системе счисления",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Выполнение операции
                if (cmbOperation.SelectedIndex == 0) // Сложение
                {
                    result = NumberConverter.Add(num1, num2, baseSystem);
                }
                else // Вычитание
                {
                    result = NumberConverter.Subtract(num1, num2, baseSystem);
                }

                txtResult.Text = result;

                // Показываем также десятичные эквиваленты для проверки
                long dec1 = NumberConverter.ToDecimal(num1, baseSystem);
                long dec2 = NumberConverter.ToDecimal(num2, baseSystem);
                long decResult = NumberConverter.ToDecimal(result, baseSystem);

                lblDecimalInfo.Text = $"Десятичные: {dec1} {(cmbOperation.SelectedIndex == 0 ? "+" : "-")} {dec2} = {decResult}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetBaseSystemName(int baseSystem)
        {
            switch (baseSystem)
            {
                case 3: return "троичной";
                case 5: return "пятеричной";
                case 7: return "семеричной";
                default: return "";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtNum1.Clear();
            txtNum2.Clear();
            txtResult.Clear();
            lblDecimalInfo.Text = "";
        }
    }
}