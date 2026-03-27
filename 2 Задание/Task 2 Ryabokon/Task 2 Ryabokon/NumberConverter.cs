using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_Ryabokon
{
    /// <summary>
    /// Класс для работы с числами в системах счисления 3, 5 и 7
    /// </summary>
    public static class NumberConverter
    {

        public static bool IsValidNumber(string number, int baseSystem)
        {
            if (string.IsNullOrWhiteSpace(number))
                return false;

            // Проверка на допустимую систему счисления
            if (baseSystem != 3 && baseSystem != 5 && baseSystem != 7)
                throw new ArgumentException("Поддерживаются только системы счисления: 3, 5, 7");

            // Проверка каждого символа
            foreach (char c in number)
            {
                // Проверяем, что символ - цифра
                if (!char.IsDigit(c))
                    return false;

                int digit = int.Parse(c.ToString());
                if (digit >= baseSystem)
                    return false;
            }

            return true;
        }

        public static long ToDecimal(string number, int baseSystem)
        {
            if (!IsValidNumber(number, baseSystem))
                throw new ArgumentException($"Число {number} не является корректным в системе счисления {baseSystem}");

            long result = 0;
            int power = 0;

            // Проходим по цифрам справа налево
            for (int i = number.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(number[i].ToString());
                result += digit * (long)Math.Pow(baseSystem, power);
                power++;
            }

            return result;
        }

        public static string FromDecimal(long decimalNumber, int baseSystem)
        {
            if (decimalNumber < 0)
                throw new ArgumentException("Число должно быть неотрицательным");

            if (baseSystem != 3 && baseSystem != 5 && baseSystem != 7)
                throw new ArgumentException("Поддерживаются только системы счисления: 3, 5, 7");

            if (decimalNumber == 0)
                return "0";

            string result = "";
            long number = decimalNumber;

            while (number > 0)
            {
                long remainder = number % baseSystem;
                result = remainder.ToString() + result;
                number /= baseSystem;
            }

            return result;
        }

        public static string Add(string num1, string num2, int baseSystem)
        {
            // Переводим оба числа в десятичную систему
            long dec1 = ToDecimal(num1, baseSystem);
            long dec2 = ToDecimal(num2, baseSystem);

            // Складываем в десятичной
            long sum = dec1 + dec2;

            // Переводим результат обратно
            return FromDecimal(sum, baseSystem);
        }

        public static string Subtract(string num1, string num2, int baseSystem)
        {
            // Переводим оба числа в десятичную систему
            long dec1 = ToDecimal(num1, baseSystem);
            long dec2 = ToDecimal(num2, baseSystem);

            if (dec1 < dec2)
                throw new InvalidOperationException("Результат вычитания отрицательный (не поддерживается для натуральных чисел)");

            // Вычитаем в десятичной
            long difference = dec1 - dec2;

            // Переводим результат обратно
            return FromDecimal(difference, baseSystem);
        }
    }
}