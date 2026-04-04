using System.Drawing;
using System.Windows.Forms;

namespace Task8R
{
    partial class ResultsForm
    {
        private ListBox lstResults;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text = "Результаты игрока";
            this.Size = new Size(600, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimumSize = new Size(500, 400);

            lstResults = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 10),
                IntegralHeight = false
            };

            this.Controls.Add(lstResults);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}