using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Task8R
{
    public partial class AuthForm : Form
    {
        public string PlayerName { get; private set; }

        public AuthForm()
        {
            InitializeComponent();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Пожалуйста, введите ваше имя!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PlayerName = txtName.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}