using Microsoft.VisualStudio.TestTools.UnitTesting;
using DictionaryLib;
using System.IO;
using System.Text;

namespace Task_5_R_Tests
{
    [TestClass]
    public class SlovarTests
    {
        private string testFile = "test_dict.txt";
        private Slovar slovar;

        // Этот метод выполняется перед каждым тестом
        [TestInitialize]
        public void Setup()
        {
            // Создаём тестовый файл со словами
            string[] testWords = { "кот", "собака", "дом", "машина", "кот" };
            File.WriteAllLines(testFile, testWords, Encoding.UTF8);

            // Загружаем словарь
            slovar = new Slovar(testFile);
        }

        // Этот метод выполняется после каждого теста
        [TestCleanup]
        public void Cleanup()
        {
            // Удаляем тестовый файл
            if (File.Exists(testFile))
                File.Delete(testFile);
        }

        // Тест 1: Проверка загрузки словаря (дубликаты не добавляются)
        [TestMethod]
        public void TestDictionaryLoad_NoDuplicates()
        {
            // Assert - проверяем, что загружено 4 уникальных слова (кот, собака, дом, машина)
            Assert.AreEqual(4, slovar.Count, "Должно быть загружено 4 уникальных слова");
        }

        // Тест 2: Добавление нового слова
        [TestMethod]
        public void TestAddWord_NewWord_Success()
        {
            // Act - добавляем новое слово
            bool result = slovar.AddWord("новое");

            // Assert - проверяем результат
            Assert.IsTrue(result, "Новое слово должно добавиться");
            Assert.AreEqual(5, slovar.Count, "Количество слов должно увеличиться на 1");
        }

        // Тест 3: Добавление существующего слова
        [TestMethod]
        public void TestAddWord_ExistingWord_Fails()
        {
            // Act - пытаемся добавить существующее слово
            bool result = slovar.AddWord("кот");

            // Assert - проверяем, что слово не добавилось
            Assert.IsFalse(result, "Существующее слово не должно добавляться");
            Assert.AreEqual(4, slovar.Count, "Количество слов не должно измениться");
        }

        // Тест 4: Удаление существующего слова
        [TestMethod]
        public void TestRemoveWord_ExistingWord_Success()
        {
            // Act - удаляем слово
            bool result = slovar.RemoveWord("кот");

            // Assert - проверяем результат
            Assert.IsTrue(result, "Существующее слово должно удалиться");
            Assert.AreEqual(3, slovar.Count, "Количество слов должно уменьшиться на 1");
        }

        // Тест 5: Удаление несуществующего слова
        [TestMethod]
        public void TestRemoveWord_NonExistingWord_Fails()
        {
            // Act - пытаемся удалить несуществующее слово
            bool result = slovar.RemoveWord("несуществующее");

            // Assert - проверяем, что ничего не удалилось
            Assert.IsFalse(result, "Несуществующее слово не должно удаляться");
            Assert.AreEqual(4, slovar.Count, "Количество слов не должно измениться");
        }

        // Тест 6: Поиск слов по количеству слогов
        [TestMethod]
        public void TestSearchBySyllableCount_ReturnsCorrectWords()
        {
            // Act - ищем слова с 1 слогом
            var result = slovar.SearchBySyllableCount(1);

            // Assert - проверяем результат
            Assert.AreEqual(2, result.Count, "Должно быть 2 слова с 1 слогом");
            Assert.IsTrue(result.Contains("кот"), "Слово 'кот' должно быть в результате");
            Assert.IsTrue(result.Contains("дом"), "Слово 'дом' должно быть в результате");
        }

        // Тест 7: Поиск слов по длине
        [TestMethod]
        public void TestSearchByLength_ReturnsCorrectWords()
        {
            // Act - ищем слова длиной 3 буквы
            var result = slovar.SearchByLength(3);

            // Assert - проверяем результат
            Assert.AreEqual(2, result.Count, "Должно быть 2 слова длиной 3 буквы");
            Assert.IsTrue(result.Contains("кот"), "Слово 'кот' должно быть в результате");
            Assert.IsTrue(result.Contains("дом"), "Слово 'дом' должно быть в результате");
        }

        // Тест 8: Поиск слов по длине - нет результатов
        [TestMethod]
        public void TestSearchByLength_NoResults_ReturnsEmptyList()
        {
            // Act - ищем слова длиной 10 букв (таких нет)
            var result = slovar.SearchByLength(10);

            // Assert - проверяем, что результат пустой
            Assert.AreEqual(0, result.Count, "Должен вернуться пустой список");
        }

        // Тест 9: Поиск по слогам - нет результатов
        [TestMethod]
        public void TestSearchBySyllableCount_NoResults_ReturnsEmptyList()
        {
            // Act - ищем слова с 5 слогами (таких нет)
            var result = slovar.SearchBySyllableCount(5);

            // Assert - проверяем, что результат пустой
            Assert.AreEqual(0, result.Count, "Должен вернуться пустой список");
        }

        // Тест 10: Нечёткий поиск (расстояние Левенштейна)
        [TestMethod]
        public void TestFuzzySearch_ReturnsSimilarWords()
        {
            // Act - ищем похожие на "ко" (расстояние 1)
            var result = slovar.FuzzySearch("ко", 1);

            // Assert - проверяем результат
            Assert.IsTrue(result.Contains("кот"), "Слово 'кот' должно быть найдено");
        }
    }
}