using DictionaryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Task_5_R
{
    public partial class Form1 : Form
    {
        private Slovar currentDictionary;
        private string currentDictPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadDict_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Текстовые файлы|*.txt";
                ofd.Title = "Выберите файл словаря";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    currentDictPath = ofd.FileName;
                    currentDictionary = new Slovar(currentDictPath);
                    lblStatus.Text = $"Загружено слов: {currentDictionary.Count}";
                    RefreshWordList();
                }
            }
        }

        private void RefreshWordList(string prefix = "")
        {
            listBoxWords.Items.Clear();
            var words = currentDictionary.GetWords();
            if (!string.IsNullOrEmpty(prefix))
                words = words.Where(w => w.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)).ToList();
            foreach (var w in words.OrderBy(w => w))
                listBoxWords.Items.Add(w);
        }

        private void btnAddWord_Click(object sender, EventArgs e)
        {
            string word = txtWord.Text.Trim();
            if (string.IsNullOrEmpty(word))
            {
                MessageBox.Show("Введите слово для добавления", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentDictionary.AddWord(word))
            {
                RefreshWordList(txtPrefix.Text.Trim());
                lblStatus.Text = $"Добавлено: {word}";
                txtWord.Clear();
            }
            else
            {
                MessageBox.Show("Слово уже существует в словаре или недопустимо", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRemoveWord_Click(object sender, EventArgs e)
        {
            if (listBoxWords.SelectedItem != null)
            {
                string word = listBoxWords.SelectedItem.ToString();
                if (MessageBox.Show($"Удалить слово \"{word}\"?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (currentDictionary.RemoveWord(word))
                    {
                        RefreshWordList(txtPrefix.Text.Trim());
                        lblStatus.Text = $"Удалено: {word}";
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите слово для удаления", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSaveDict_Click(object sender, EventArgs e)
        {
            if (currentDictionary == null)
            {
                MessageBox.Show("Словарь не загружен", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Текстовые файлы|*.txt";
                sfd.Title = "Сохранить словарь";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    currentDictionary.SaveToFile(sfd.FileName);
                    MessageBox.Show("Словарь сохранён", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCreateCustomDict_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Текстовые файлы|*.txt";
                sfd.FileName = "custom_dict.txt";
                sfd.Title = "Создать новый словарь";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(sfd.FileName, "");
                    currentDictPath = sfd.FileName;
                    currentDictionary = new Slovar(currentDictPath);
                    lblStatus.Text = "Новый пустой словарь создан";
                    RefreshWordList();
                    txtPrefix.Clear();
                }
            }
        }

        private void btnSearchBySyllables_Click(object sender, EventArgs e)
        {
            if (currentDictionary == null)
            {
                MessageBox.Show("Словарь не загружен", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(txtSyllables.Text, out int syllables) && syllables > 0)
            {
                var result = currentDictionary.SearchBySyllableCount(syllables);
                ShowResult(result);
            }
            else
            {
                MessageBox.Show("Введите корректное количество слогов (целое число > 0)", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSearchByLength_Click(object sender, EventArgs e)
        {
            if (currentDictionary == null)
            {
                MessageBox.Show("Словарь не загружен", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(txtLength.Text, out int length) && length > 0)
            {
                var result = currentDictionary.SearchByLength(length);
                ShowResult(result);
            }
            else
            {
                MessageBox.Show("Введите корректную длину слова (целое число > 0)", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowResult(List<string> result)
        {
            if (result.Count == 0)
            {
                MessageBox.Show("Ничего не найдено", "Результат поиска",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                FormResult fr = new FormResult(result);
                fr.ShowDialog();
            }
        }

        private void btnFilterByPrefix_Click(object sender, EventArgs e)
        {
            if (currentDictionary != null)
            {
                string prefix = txtPrefix.Text.Trim();
                RefreshWordList(prefix);
                lblStatus.Text = $"Показаны слова, начинающиеся с \"{prefix}\"";
            }
            else
            {
                MessageBox.Show("Словарь не загружен", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}