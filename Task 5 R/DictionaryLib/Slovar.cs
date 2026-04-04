using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DictionaryLib
{
    public class Slovar
    {
        private List<string> list = new List<string>();
        private string filename;
        private int count;

        public Slovar(string filename)
        {
            this.filename = filename;
            OpenFile();
            count = list.Count;
        }

        public int Count => count;
        public List<string> GetWords() => new List<string>(list);

        public void OpenFile()
        {
            try
            {
                list.Clear();
                using (StreamReader f = new StreamReader(filename, Encoding.UTF8))
                {
                    while (!f.EndOfStream)
                    {
                        string word = f.ReadLine()?.Trim();
                        if (!string.IsNullOrEmpty(word) && !list.Contains(word))
                            list.Add(word);
                    }
                }
            }
            catch
            {
                throw new Exception("Ошибка доступа к файлу!");
            }
        }

        public bool AddWord(string word)
        {
            word = word.Trim();
            if (string.IsNullOrEmpty(word) || list.Contains(word))
                return false;
            list.Add(word);
            count = list.Count;
            return true;
        }

        public bool RemoveWord(string word)
        {
            word = word.Trim();
            if (list.Remove(word))
            {
                count = list.Count;
                return true;
            }
            return false;
        }

        public void SaveToFile(string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
                foreach (var w in list)
                    sw.WriteLine(w);
        }

        public List<string> FuzzySearch(string pattern, int maxDistance = 3)
        {
            var result = new List<string>();
            foreach (var word in list)
                if (LevenshteinDistance(word, pattern) <= maxDistance)
                    result.Add(word);
            return result;
        }

        private int LevenshteinDistance(string s, string t)
        {
            int n = s.Length, m = t.Length;
            int[,] d = new int[n + 1, m + 1];
            if (n == 0) return m;
            if (m == 0) return n;
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= m; j++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            return d[n, m];
        }

        // Вариант 1: поиск по количеству слогов
        public List<string> SearchBySyllableCount(int syllableCount)
        {
            var result = new List<string>();
            foreach (var word in list)
                if (CountSyllables(word) == syllableCount)
                    result.Add(word);
            return result;
        }

        private int CountSyllables(string word)
        {
            word = word.ToLower();
            int count = 0;
            bool lastWasVowel = false;
            foreach (char c in word)
            {
                if ("аеёиоуыэюяaeiouy".Contains(c))
                {
                    if (!lastWasVowel)
                        count++;
                    lastWasVowel = true;
                }
                else
                    lastWasVowel = false;
            }
            return Math.Max(count, 1);
        }

        // Вариант 1: поиск по длине слова
        public List<string> SearchByLength(int length)
        {
            var result = new List<string>();
            foreach (var word in list)
                if (word.Length == length)
                    result.Add(word);
            return result;
        }
    }
}