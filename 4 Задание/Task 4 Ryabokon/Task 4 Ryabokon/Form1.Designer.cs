namespace Task_4_Ryabokon
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageInput = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.numMisses = new System.Windows.Forms.NumericUpDown();
            this.numTime = new System.Windows.Forms.NumericUpDown();
            this.numDistance = new System.Windows.Forms.NumericUpDown();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageTable = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnFindBest = new System.Windows.Forms.Button();
            this.dgvParticipants = new System.Windows.Forms.DataGridView();
            this.tabPageChart = new System.Windows.Forms.TabPage();
            this.chartResults = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ColumnLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMisses = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTotalTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPageInput.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMisses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDistance)).BeginInit();
            this.tabPageTable.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParticipants)).BeginInit();
            this.tabPageChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartResults)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageInput);
            this.tabControl1.Controls.Add(this.tabPageTable);
            this.tabControl1.Controls.Add(this.tabPageChart);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(884, 561);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageInput
            // 
            this.tabPageInput.Controls.Add(this.groupBox1);
            this.tabPageInput.Location = new System.Drawing.Point(4, 22);
            this.tabPageInput.Name = "tabPageInput";
            this.tabPageInput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInput.Size = new System.Drawing.Size(876, 535);
            this.tabPageInput.TabIndex = 0;
            this.tabPageInput.Text = "Ввод данных";
            this.tabPageInput.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.numMisses);
            this.groupBox1.Controls.Add(this.numTime);
            this.groupBox1.Controls.Add(this.numDistance);
            this.groupBox1.Controls.Add(this.txtLastName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(20, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 250);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Данные участника";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(150, 200);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // numMisses
            // 
            this.numMisses.Location = new System.Drawing.Point(150, 160);
            this.numMisses.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numMisses.Name = "numMisses";
            this.numMisses.Size = new System.Drawing.Size(100, 20);
            this.numMisses.TabIndex = 7;
            // 
            // numTime
            // 
            this.numTime.Location = new System.Drawing.Point(150, 120);
            this.numTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numTime.Name = "numTime";
            this.numTime.Size = new System.Drawing.Size(100, 20);
            this.numTime.TabIndex = 6;
            // 
            // numDistance
            // 
            this.numDistance.Location = new System.Drawing.Point(150, 80);
            this.numDistance.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numDistance.Name = "numDistance";
            this.numDistance.Size = new System.Drawing.Size(100, 20);
            this.numDistance.TabIndex = 5;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(150, 40);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(150, 20);
            this.txtLastName.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Количество промахов:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Время (сек):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Дистанция (м):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Фамилия:";
            // 
            // tabPageTable
            // 
            this.tabPageTable.Controls.Add(this.groupBox2);
            this.tabPageTable.Location = new System.Drawing.Point(4, 22);
            this.tabPageTable.Name = "tabPageTable";
            this.tabPageTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTable.Size = new System.Drawing.Size(876, 535);
            this.tabPageTable.TabIndex = 1;
            this.tabPageTable.Text = "Результаты";
            this.tabPageTable.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLoad);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnFindBest);
            this.groupBox2.Controls.Add(this.dgvParticipants);
            this.groupBox2.Location = new System.Drawing.Point(10, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(850, 510);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Таблица результатов";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(750, 470);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(90, 30);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Загрузить";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(650, 470);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnFindBest
            // 
            this.btnFindBest.Location = new System.Drawing.Point(10, 470);
            this.btnFindBest.Name = "btnFindBest";
            this.btnFindBest.Size = new System.Drawing.Size(150, 30);
            this.btnFindBest.TabIndex = 1;
            this.btnFindBest.Text = "Найти лучший результат";
            this.btnFindBest.UseVisualStyleBackColor = true;
            this.btnFindBest.Click += new System.EventHandler(this.btnFindBest_Click);
            // 
            // dgvParticipants
            // 
            this.dgvParticipants.AllowUserToAddRows = false;
            this.dgvParticipants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParticipants.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnLastName,
            this.ColumnDistance,
            this.ColumnTime,
            this.ColumnMisses,
            this.ColumnTotalTime});
            this.dgvParticipants.Location = new System.Drawing.Point(10, 20);
            this.dgvParticipants.Name = "dgvParticipants";
            this.dgvParticipants.Size = new System.Drawing.Size(830, 440);
            this.dgvParticipants.TabIndex = 0;
            // 
            // tabPageChart
            // 
            this.tabPageChart.Controls.Add(this.chartResults);
            this.tabPageChart.Location = new System.Drawing.Point(4, 22);
            this.tabPageChart.Name = "tabPageChart";
            this.tabPageChart.Size = new System.Drawing.Size(876, 535);
            this.tabPageChart.TabIndex = 2;
            this.tabPageChart.Text = "Диаграмма";
            this.tabPageChart.UseVisualStyleBackColor = true;
            // 
            // chartResults
            // 
            chartArea1.Name = "ChartArea1";
            this.chartResults.ChartAreas.Add(chartArea1);
            this.chartResults.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartResults.Legends.Add(legend1);
            this.chartResults.Location = new System.Drawing.Point(0, 0);
            this.chartResults.Name = "chartResults";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartResults.Series.Add(series1);
            this.chartResults.Size = new System.Drawing.Size(876, 535);
            this.chartResults.TabIndex = 0;
            this.chartResults.Text = "chart1";
            // 
            // ColumnLastName
            // 
            this.ColumnLastName.HeaderText = "Фамилия";
            this.ColumnLastName.Name = "ColumnLastName";
            this.ColumnLastName.Width = 120;
            // 
            // ColumnDistance
            // 
            this.ColumnDistance.HeaderText = "Дистанция (м)";
            this.ColumnDistance.Name = "ColumnDistance";
            // 
            // ColumnTime
            // 
            this.ColumnTime.HeaderText = "Время (сек)";
            this.ColumnTime.Name = "ColumnTime";
            // 
            // ColumnMisses
            // 
            this.ColumnMisses.HeaderText = "Промахи";
            this.ColumnMisses.Name = "ColumnMisses";
            // 
            // ColumnTotalTime
            // 
            this.ColumnTotalTime.HeaderText = "Итоговое время (сек)";
            this.ColumnTotalTime.Name = "ColumnTotalTime";
            this.ColumnTotalTime.Width = 150;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Биатлонные соревнования";
            this.tabControl1.ResumeLayout(false);
            this.tabPageInput.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMisses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDistance)).EndInit();
            this.tabPageTable.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParticipants)).EndInit();
            this.tabPageChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageInput;
        private System.Windows.Forms.TabPage tabPageTable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.NumericUpDown numMisses;
        private System.Windows.Forms.NumericUpDown numTime;
        private System.Windows.Forms.NumericUpDown numDistance;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvParticipants;
        private System.Windows.Forms.Button btnFindBest;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabPage tabPageChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMisses;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTotalTime;
    }
}