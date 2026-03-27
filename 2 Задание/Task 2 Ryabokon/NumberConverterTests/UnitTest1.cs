using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Task_2_Ryabokon;


namespace NumberConverterTests
{
    [TestClass]
    public class NumberConverterTests
    {
        // Тесты для IsValidNumber
        [TestMethod]
        public void IsValidNumber_ValidTernaryNumber_ReturnsTrue()
        {
            // Arrange
            string number = "2101";
            int baseSystem = 3;

            // Act
            bool result = NumberConverter.IsValidNumber(number, baseSystem);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidNumber_InvalidTernaryNumber_ReturnsFalse()
        {
            // Arrange
            string number = "2103"; // Цифра 3 недопустима в троичной системе
            int baseSystem = 3;

            // Act
            bool result = NumberConverter.IsValidNumber(number, baseSystem);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidNumber_ValidBase5Number_ReturnsTrue()
        {
            // Arrange
            string number = "4321";
            int baseSystem = 5;

            // Act
            bool result = NumberConverter.IsValidNumber(number, baseSystem);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidNumber_ValidBase7Number_ReturnsTrue()
        {
            // Arrange
            string number = "6543";
            int baseSystem = 7;

            // Act
            bool result = NumberConverter.IsValidNumber(number, baseSystem);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidNumber_EmptyString_ReturnsFalse()
        {
            // Arrange
            string number = "";
            int baseSystem = 3;

            // Act
            bool result = NumberConverter.IsValidNumber(number, baseSystem);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidNumber_InvalidBaseSystem_ThrowsArgumentException()
        {
            // Arrange
            string number = "123";
            int baseSystem = 4; // Неподдерживаемая система

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => NumberConverter.IsValidNumber(number, baseSystem));
        }

        // Тесты для ToDecimal
        [TestMethod]
        public void ToDecimal_TernaryNumber_ReturnsCorrectDecimal()
        {
            // Arrange
            string number = "2101"; // 2*3^3 + 1*3^2 + 0*3^1 + 1*3^0 = 54 + 9 + 0 + 1 = 64
            int baseSystem = 3;
            long expected = 64;

            // Act
            long actual = NumberConverter.ToDecimal(number, baseSystem);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToDecimal_Base5Number_ReturnsCorrectDecimal()
        {
            // Arrange
            string number = "432"; // 4*5^2 + 3*5^1 + 2*5^0 = 100 + 15 + 2 = 117
            int baseSystem = 5;
            long expected = 117;

            // Act
            long actual = NumberConverter.ToDecimal(number, baseSystem);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToDecimal_Base7Number_ReturnsCorrectDecimal()
        {
            // Arrange
            string number = "165"; // 1*7^2 + 6*7^1 + 5*7^0 = 49 + 42 + 5 = 96
            int baseSystem = 7;
            long expected = 96;

            // Act
            long actual = NumberConverter.ToDecimal(number, baseSystem);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToDecimal_InvalidNumber_ThrowsArgumentException()
        {
            // Arrange
            string number = "123";
            int baseSystem = 3; // В троичной системе цифра 3 недопустима

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => NumberConverter.ToDecimal(number, baseSystem));
        }

        // Тесты для FromDecimal
        [TestMethod]
        public void FromDecimal_ToTernary_ReturnsCorrectString()
        {
            // Arrange
            long decimalNumber = 64;
            int baseSystem = 3;
            string expected = "2101";

            // Act
            string actual = NumberConverter.FromDecimal(decimalNumber, baseSystem);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FromDecimal_ToBase5_ReturnsCorrectString()
        {
            // Arrange
            long decimalNumber = 117;
            int baseSystem = 5;
            string expected = "432";

            // Act
            string actual = NumberConverter.FromDecimal(decimalNumber, baseSystem);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FromDecimal_ToBase7_ReturnsCorrectString()
        {
            // Arrange
            long decimalNumber = 96;
            int baseSystem = 7;
            string expected = "165";

            // Act
            string actual = NumberConverter.FromDecimal(decimalNumber, baseSystem);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FromDecimal_Zero_ReturnsZero()
        {
            // Arrange
            long decimalNumber = 0;
            int baseSystem = 3;
            string expected = "0";

            // Act
            string actual = NumberConverter.FromDecimal(decimalNumber, baseSystem);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FromDecimal_NegativeNumber_ThrowsArgumentException()
        {
            // Arrange
            long decimalNumber = -5;
            int baseSystem = 3;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => NumberConverter.FromDecimal(decimalNumber, baseSystem));
        }

        // Тесты для сложения
        [TestMethod]
        public void Add_TwoTernaryNumbers_ReturnsCorrectSum()
        {
            // Arrange
            string num1 = "12"; // 1*3 + 2 = 5 в десятичной
            string num2 = "21"; // 2*3 + 1 = 7 в десятичной
            int baseSystem = 3;
            string expected = "110"; // 5+7=12 в десятичной = 1*9 + 1*3 + 0 = 110 в троичной

            // Act
            string actual = NumberConverter.Add(num1, num2, baseSystem);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Add_TwoBase5Numbers_ReturnsCorrectSum()
        {
            // Arrange
            string num1 = "34"; // 3*5 + 4 = 19 в десятичной
            string num2 = "12"; // 1*5 + 2 = 7 в десятичной
            int baseSystem = 5;
            string expected = "101"; // 19+7=26 в десятичной = 1*25 + 0*5 + 1 = 101 в пятеричной

            // Act
            string actual = NumberConverter.Add(num1, num2, baseSystem);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Add_ZeroAndNumber_ReturnsNumber()
        {
            // Arrange
            string num1 = "0";
            string num2 = "123"; // в семеричной
            int baseSystem = 7;

            // Act
            string actual = NumberConverter.Add(num1, num2, baseSystem);

            // Assert
            Assert.AreEqual("123", actual);
        }

        // Тесты для вычитания
        [TestMethod]
        public void Subtract_TwoTernaryNumbers_ReturnsCorrectDifference()
        {
            // Arrange
            string num1 = "110"; // 12 в десятичной
            string num2 = "12";  // 5 в десятичной
            int baseSystem = 3;
            string expected = "21"; // 12-5=7 в десятичной = 21 в троичной

            // Act
            string actual = NumberConverter.Subtract(num1, num2, baseSystem);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Subtract_ResultNegative_ThrowsInvalidOperationException()
        {
            // Arrange
            string num1 = "12"; // 5 в десятичной
            string num2 = "110"; // 12 в десятичной
            int baseSystem = 3;

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => NumberConverter.Subtract(num1, num2, baseSystem));
        }
    }
}