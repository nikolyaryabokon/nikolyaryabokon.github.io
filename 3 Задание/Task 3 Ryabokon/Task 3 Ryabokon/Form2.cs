using System;
using System.Drawing;
using System.Windows.Forms;

namespace Task_3_Ryabokon
{
    public partial class Form2 : Form
    {
        public Color SelectedColor { get; private set; }
        public int SpeedInterval { get; private set; }

        public Form2(Color currentColor, int currentSpeed)
        {
            InitializeComponent(); // Это должно быть первой строкой в конструкторе

            SelectedColor = currentColor;
            SpeedInterval = currentSpeed;

            // Проверяем, что trackBarSpeed не null перед использованием
            if (trackBarSpeed != null)
            {
                trackBarSpeed.Minimum = 1;
                trackBarSpeed.Maximum = 100;
                trackBarSpeed.Value = currentSpeed;
            }

            // Проверяем, что btnColor не null перед использованием
            if (btnColor != null)
            {
                btnColor.BackColor = currentColor;
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = SelectedColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedColor = colorDialog.Color;
                btnColor.BackColor = SelectedColor;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SpeedInterval = trackBarSpeed.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}