namespace Task_1_Ryabokon { 
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            buttonCompute = new Button();
            labelResult = new Label();
            groupBoxOutput = new GroupBox();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            textBoxEps = new TextBox();
            labelEps = new Label();
            textBoxX = new TextBox();
            labelX = new Label();
            groupBoxInput = new GroupBox();
            pictureBox1 = new PictureBox();
            groupBoxOutput.SuspendLayout();
            statusStrip.SuspendLayout();
            groupBoxInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // buttonCompute
            // 
            buttonCompute.BackColor = Color.LightSteelBlue;
            buttonCompute.FlatStyle = FlatStyle.Flat;
            buttonCompute.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            buttonCompute.Location = new Point(221, 268);
            buttonCompute.Margin = new Padding(4, 5, 4, 5);
            buttonCompute.Name = "buttonCompute";
            buttonCompute.Size = new Size(160, 54);
            buttonCompute.TabIndex = 4;
            buttonCompute.Text = "Вычислить";
            buttonCompute.UseVisualStyleBackColor = false;
            buttonCompute.Click += buttonCompute_Click;
            // 
            // labelResult
            // 
            labelResult.BackColor = SystemColors.ControlLightLight;
            labelResult.BorderStyle = BorderStyle.FixedSingle;
            labelResult.Font = new Font("Consolas", 10F);
            labelResult.Location = new Point(17, 17);
            labelResult.Margin = new Padding(4, 0, 4, 0);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(573, 284);
            labelResult.TabIndex = 5;
            labelResult.TextAlign = ContentAlignment.MiddleLeft;
            labelResult.Click += labelResult_Click;
            // 
            // groupBoxOutput
            // 
            groupBoxOutput.Controls.Add(labelResult);
            groupBoxOutput.Font = new Font("Microsoft Sans Serif", 9F);
            groupBoxOutput.Location = new Point(16, 356);
            groupBoxOutput.Margin = new Padding(4, 5, 4, 5);
            groupBoxOutput.Name = "groupBoxOutput";
            groupBoxOutput.Padding = new Padding(4, 5, 4, 5);
            groupBoxOutput.Size = new Size(613, 321);
            groupBoxOutput.TabIndex = 7;
            groupBoxOutput.TabStop = false;
            groupBoxOutput.Text = "Результаты вычислений";
            groupBoxOutput.Enter += groupBoxOutput_Enter;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel });
            statusStrip.Location = new Point(0, 700);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 19, 0);
            statusStrip.Size = new Size(645, 26);
            statusStrip.TabIndex = 8;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(248, 20);
            toolStripStatusLabel.Text = "Вариант 1: Разложение ch(x) в ряд";
            // 
            // textBoxEps
            // 
            textBoxEps.Font = new Font("Microsoft Sans Serif", 10F);
            textBoxEps.Location = new Point(477, 210);
            textBoxEps.Margin = new Padding(4, 5, 4, 5);
            textBoxEps.MaxLength = 15;
            textBoxEps.Name = "textBoxEps";
            textBoxEps.Size = new Size(93, 26);
            textBoxEps.TabIndex = 3;
            textBoxEps.TextAlign = HorizontalAlignment.Right;
            textBoxEps.TextChanged += TextBox_TextChanged;
            textBoxEps.KeyPress += textBoxEps_KeyPress;
            // 
            // labelEps
            // 
            labelEps.AutoSize = true;
            labelEps.Font = new Font("Microsoft Sans Serif", 10F);
            labelEps.Location = new Point(350, 216);
            labelEps.Margin = new Padding(4, 0, 4, 0);
            labelEps.Name = "labelEps";
            labelEps.Size = new Size(119, 20);
            labelEps.TabIndex = 2;
            labelEps.Text = "Точность (ε):";
            // 
            // textBoxX
            // 
            textBoxX.Font = new Font("Microsoft Sans Serif", 10F);
            textBoxX.Location = new Point(129, 216);
            textBoxX.Margin = new Padding(4, 5, 4, 5);
            textBoxX.MaxLength = 15;
            textBoxX.Name = "textBoxX";
            textBoxX.Size = new Size(93, 26);
            textBoxX.TabIndex = 1;
            textBoxX.TextAlign = HorizontalAlignment.Right;
            textBoxX.TextChanged += TextBox_TextChanged;
            textBoxX.KeyPress += textBoxX_KeyPress;
            // 
            // labelX
            // 
            labelX.AutoSize = true;
            labelX.Font = new Font("Microsoft Sans Serif", 10F);
            labelX.Location = new Point(8, 219);
            labelX.Margin = new Padding(4, 0, 4, 0);
            labelX.Name = "labelX";
            labelX.Size = new Size(104, 20);
            labelX.TabIndex = 0;
            labelX.Text = "Введите X:";
            labelX.Click += labelX_Click;
            // 
            // groupBoxInput
            // 
            groupBoxInput.Controls.Add(pictureBox1);
            groupBoxInput.Controls.Add(labelX);
            groupBoxInput.Controls.Add(textBoxX);
            groupBoxInput.Controls.Add(labelEps);
            groupBoxInput.Controls.Add(buttonCompute);
            groupBoxInput.Controls.Add(textBoxEps);
            groupBoxInput.Font = new Font("Microsoft Sans Serif", 9F);
            groupBoxInput.Location = new Point(33, 14);
            groupBoxInput.Margin = new Padding(4, 5, 4, 5);
            groupBoxInput.Name = "groupBoxInput";
            groupBoxInput.Padding = new Padding(4, 5, 4, 5);
            groupBoxInput.Size = new Size(596, 332);
            groupBoxInput.TabIndex = 6;
            groupBoxInput.TabStop = false;
            groupBoxInput.Text = "Входные данные";
            groupBoxInput.Enter += groupBoxInput_Enter;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(8, 25);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(565, 156);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(645, 726);
            Controls.Add(statusStrip);
            Controls.Add(groupBoxOutput);
            Controls.Add(groupBoxInput);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Вычисление функции с помощью разложения в ряд (Вариант 1)";
            Load += Form1_Load;
            groupBoxOutput.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            groupBoxInput.ResumeLayout(false);
            groupBoxInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button buttonCompute;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.GroupBox groupBoxOutput;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private TextBox textBoxEps;
        private Label labelEps;
        private TextBox textBoxX;
        private Label labelX;
        private GroupBox groupBoxInput;
        private PictureBox pictureBox1;
    }
}