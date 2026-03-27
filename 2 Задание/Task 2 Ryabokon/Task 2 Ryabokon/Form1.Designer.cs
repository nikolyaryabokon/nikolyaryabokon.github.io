namespace Task_2_Ryabokon
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbBaseSystem;
        private System.Windows.Forms.ComboBox cmbOperation;
        private System.Windows.Forms.TextBox txtNum1;
        private System.Windows.Forms.TextBox txtNum2;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label lblBaseSystem;
        private System.Windows.Forms.Label lblOperation;
        private System.Windows.Forms.Label lblNum1;
        private System.Windows.Forms.Label lblNum2;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblExample;
        private System.Windows.Forms.Label lblDecimalInfo;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnClear;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbBaseSystem = new System.Windows.Forms.ComboBox();
            this.cmbOperation = new System.Windows.Forms.ComboBox();
            this.txtNum1 = new System.Windows.Forms.TextBox();
            this.txtNum2 = new System.Windows.Forms.TextBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.lblBaseSystem = new System.Windows.Forms.Label();
            this.lblOperation = new System.Windows.Forms.Label();
            this.lblNum1 = new System.Windows.Forms.Label();
            this.lblNum2 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblExample = new System.Windows.Forms.Label();
            this.lblDecimalInfo = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Form1
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Text = "Калькулятор систем счисления (3, 5, 7)";
            this.Load += new System.EventHandler(this.Form1_Load);

            // lblBaseSystem
            this.lblBaseSystem.Text = "Система счисления:";
            this.lblBaseSystem.Location = new System.Drawing.Point(30, 30);
            this.lblBaseSystem.Size = new System.Drawing.Size(120, 23);

            // cmbBaseSystem
            this.cmbBaseSystem.Location = new System.Drawing.Point(160, 27);
            this.cmbBaseSystem.Size = new System.Drawing.Size(150, 28);
            this.cmbBaseSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaseSystem.SelectedIndexChanged += new System.EventHandler(this.cmbBaseSystem_SelectedIndexChanged);

            // lblOperation
            this.lblOperation.Text = "Операция:";
            this.lblOperation.Location = new System.Drawing.Point(30, 70);
            this.lblOperation.Size = new System.Drawing.Size(120, 23);

            // cmbOperation
            this.cmbOperation.Location = new System.Drawing.Point(160, 67);
            this.cmbOperation.Size = new System.Drawing.Size(150, 28);
            this.cmbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperation.SelectedIndexChanged += new System.EventHandler(this.cmbOperation_SelectedIndexChanged);

            // lblNum1
            this.lblNum1.Text = "Первое число:";
            this.lblNum1.Location = new System.Drawing.Point(30, 110);
            this.lblNum1.Size = new System.Drawing.Size(120, 23);

            // txtNum1
            this.txtNum1.Location = new System.Drawing.Point(160, 107);
            this.txtNum1.Size = new System.Drawing.Size(150, 26);

            // lblNum2
            this.lblNum2.Text = "Второе число:";
            this.lblNum2.Location = new System.Drawing.Point(30, 150);
            this.lblNum2.Size = new System.Drawing.Size(120, 23);

            // txtNum2
            this.txtNum2.Location = new System.Drawing.Point(160, 147);
            this.txtNum2.Size = new System.Drawing.Size(150, 26);

            // btnCalculate
            this.btnCalculate.Text = "Вычислить";
            this.btnCalculate.Location = new System.Drawing.Point(160, 190);
            this.btnCalculate.Size = new System.Drawing.Size(150, 30);
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);

            // lblResult
            this.lblResult.Text = "Результат:";
            this.lblResult.Location = new System.Drawing.Point(30, 240);
            this.lblResult.Size = new System.Drawing.Size(120, 23);

            // txtResult
            this.txtResult.Location = new System.Drawing.Point(160, 237);
            this.txtResult.Size = new System.Drawing.Size(150, 26);
            this.txtResult.ReadOnly = true;

            // lblDecimalInfo
            this.lblDecimalInfo.Location = new System.Drawing.Point(30, 270);
            this.lblDecimalInfo.Size = new System.Drawing.Size(400, 23);
            this.lblDecimalInfo.Text = "";

            // lblExample
            this.lblExample.Location = new System.Drawing.Point(30, 300);
            this.lblExample.Size = new System.Drawing.Size(400, 23);
            this.lblExample.Text = "Пример: 2101 + 12 = 2120";

            // btnClear
            this.btnClear.Text = "Очистить";
            this.btnClear.Location = new System.Drawing.Point(320, 190);
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            this.Controls.Add(this.lblBaseSystem);
            this.Controls.Add(this.cmbBaseSystem);
            this.Controls.Add(this.lblOperation);
            this.Controls.Add(this.cmbOperation);
            this.Controls.Add(this.lblNum1);
            this.Controls.Add(this.txtNum1);
            this.Controls.Add(this.lblNum2);
            this.Controls.Add(this.txtNum2);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.lblDecimalInfo);
            this.Controls.Add(this.lblExample);
            this.Controls.Add(this.btnClear);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}