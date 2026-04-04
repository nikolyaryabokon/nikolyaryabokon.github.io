using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Task_5_R
{
    public partial class FormResult : Form
    {
        private List<string> words;

        public FormResult(List<string> result)
        {
            InitializeComponent();
            words = result;
            foreach (var w in result)
                listBoxResult.Items.Add(w);
            lblCount.Text = $"Найдено: {result.Count}";
        }

        private void btnSaveResult_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Текстовые файлы|*.txt";
                sfd.Title = "Сохранить результаты поиска";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllLines(sfd.FileName, words);
                    MessageBox.Show("Результаты успешно сохранены!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}