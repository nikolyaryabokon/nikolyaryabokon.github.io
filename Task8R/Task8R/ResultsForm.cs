using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Task8R
{
    public partial class ResultsForm : Form
    {
        private string currentPlayer;

        public ResultsForm(string player)
        {
            currentPlayer = player;
            InitializeComponent();
            LoadResults();
        }

        private void LoadResults()
        {
            string file = "results.dat";
            if (!File.Exists(file))
            {
                lstResults.Items.Add("Нет сохранённых результатов.");
                lstResults.Items.Add("Сыграйте несколько партий, чтобы увидеть статистику.");
                return;
            }

            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream fs = new FileStream(file, FileMode.Open))
                {
                    List<GameResult> allResults = (List<GameResult>)bf.Deserialize(fs);
                    List<GameResult> playerResults = allResults.FindAll(r => r.PlayerName == currentPlayer);

                    if (playerResults.Count == 0)
                    {
                        lstResults.Items.Add($"Нет результатов для игрока {currentPlayer}");
                        lstResults.Items.Add("Сыграйте партию, чтобы сохранить результат.");
                    }
                    else
                    {
                        lstResults.Items.Add($"=== Результаты игрока: {currentPlayer} ===");
                        lstResults.Items.Add("");
                        foreach (GameResult res in playerResults)
                        {
                            lstResults.Items.Add(res.ToString());
                            lstResults.Items.Add("---");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lstResults.Items.Add($"Ошибка загрузки: {ex.Message}");
                lstResults.Items.Add("Возможно, файл результатов повреждён.");
            }
        }
    }
}