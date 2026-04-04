using System.Drawing;
using System.Windows.Forms;

namespace Task8R
{
    partial class AuthForm
    {
        private TextBox txtName;
        private Button btnOk;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text = "Авторизация";
            this.Size = new Size(320, 160);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lblName = new Label
            {
                Text = "Введите ваше имя:",
                Location = new Point(20, 20),
                Size = new Size(150, 25),
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold)
            };

            txtName = new TextBox
            {
                Location = new Point(20, 50),
                Size = new Size(260, 25),
                Font = new Font("Microsoft Sans Serif", 10)
            };

            btnOk = new Button
            {
                Text = "Начать игру",
                Location = new Point(20, 85),
                Size = new Size(120, 35),
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            btnOk.Click += BtnOk_Click;

            this.Controls.AddRange(new Control[] { lblName, txtName, btnOk });

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}