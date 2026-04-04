namespace Task_5_R
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

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
            this.btnLoadDict = new System.Windows.Forms.Button();
            this.btnAddWord = new System.Windows.Forms.Button();
            this.btnRemoveWord = new System.Windows.Forms.Button();
            this.btnSaveDict = new System.Windows.Forms.Button();
            this.btnCreateCustomDict = new System.Windows.Forms.Button();
            this.txtWord = new System.Windows.Forms.TextBox();
            this.txtSyllables = new System.Windows.Forms.TextBox();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.btnSearchBySyllables = new System.Windows.Forms.Button();
            this.btnSearchByLength = new System.Windows.Forms.Button();
            this.listBoxWords = new System.Windows.Forms.ListBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.btnFilterByPrefix = new System.Windows.Forms.Button();
            this.groupBoxDictionary = new System.Windows.Forms.GroupBox();
            this.lblWord = new System.Windows.Forms.Label();
            this.groupBoxSearch = new System.Windows.Forms.GroupBox();
            this.lblSyllables = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.groupBoxResults = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBoxDictionary.SuspendLayout();
            this.groupBoxSearch.SuspendLayout();
            this.groupBoxResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadDict
            // 
            this.btnLoadDict.Location = new System.Drawing.Point(11, 27);
            this.btnLoadDict.Name = "btnLoadDict";
            this.btnLoadDict.Size = new System.Drawing.Size(171, 32);
            this.btnLoadDict.TabIndex = 0;
            this.btnLoadDict.Text = "Загрузить словарь";
            this.btnLoadDict.UseVisualStyleBackColor = true;
            this.btnLoadDict.Click += new System.EventHandler(this.btnLoadDict_Click);
            // 
            // btnAddWord
            // 
            this.btnAddWord.Location = new System.Drawing.Point(11, 69);
            this.btnAddWord.Name = "btnAddWord";
            this.btnAddWord.Size = new System.Drawing.Size(171, 32);
            this.btnAddWord.TabIndex = 1;
            this.btnAddWord.Text = "Добавить слово";
            this.btnAddWord.UseVisualStyleBackColor = true;
            this.btnAddWord.Click += new System.EventHandler(this.btnAddWord_Click);
            // 
            // btnRemoveWord
            // 
            this.btnRemoveWord.Location = new System.Drawing.Point(11, 112);
            this.btnRemoveWord.Name = "btnRemoveWord";
            this.btnRemoveWord.Size = new System.Drawing.Size(171, 32);
            this.btnRemoveWord.TabIndex = 2;
            this.btnRemoveWord.Text = "Удалить слово";
            this.btnRemoveWord.UseVisualStyleBackColor = true;
            this.btnRemoveWord.Click += new System.EventHandler(this.btnRemoveWord_Click);
            // 
            // btnSaveDict
            // 
            this.btnSaveDict.Location = new System.Drawing.Point(11, 155);
            this.btnSaveDict.Name = "btnSaveDict";
            this.btnSaveDict.Size = new System.Drawing.Size(171, 32);
            this.btnSaveDict.TabIndex = 3;
            this.btnSaveDict.Text = "Сохранить словарь";
            this.btnSaveDict.UseVisualStyleBackColor = true;
            this.btnSaveDict.Click += new System.EventHandler(this.btnSaveDict_Click);
            // 
            // btnCreateCustomDict
            // 
            this.btnCreateCustomDict.Location = new System.Drawing.Point(11, 197);
            this.btnCreateCustomDict.Name = "btnCreateCustomDict";
            this.btnCreateCustomDict.Size = new System.Drawing.Size(171, 32);
            this.btnCreateCustomDict.TabIndex = 4;
            this.btnCreateCustomDict.Text = "Создать свой словарь";
            this.btnCreateCustomDict.UseVisualStyleBackColor = true;
            this.btnCreateCustomDict.Click += new System.EventHandler(this.btnCreateCustomDict_Click);
            // 
            // txtWord
            // 
            this.txtWord.Location = new System.Drawing.Point(208, 32);
            this.txtWord.Name = "txtWord";
            this.txtWord.Size = new System.Drawing.Size(186, 22);
            this.txtWord.TabIndex = 5;
            // 
            // txtSyllables
            // 
            this.txtSyllables.Location = new System.Drawing.Point(140, 32);
            this.txtSyllables.Name = "txtSyllables";
            this.txtSyllables.Size = new System.Drawing.Size(114, 22);
            this.txtSyllables.TabIndex = 6;
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(140, 75);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(114, 22);
            this.txtLength.TabIndex = 7;
            // 
            // btnSearchBySyllables
            // 
            this.btnSearchBySyllables.Location = new System.Drawing.Point(280, 27);
            this.btnSearchBySyllables.Name = "btnSearchBySyllables";
            this.btnSearchBySyllables.Size = new System.Drawing.Size(171, 32);
            this.btnSearchBySyllables.TabIndex = 8;
            this.btnSearchBySyllables.Text = "Искать по слогам";
            this.btnSearchBySyllables.UseVisualStyleBackColor = true;
            this.btnSearchBySyllables.Click += new System.EventHandler(this.btnSearchBySyllables_Click);
            // 
            // btnSearchByLength
            // 
            this.btnSearchByLength.Location = new System.Drawing.Point(286, 73);
            this.btnSearchByLength.Name = "btnSearchByLength";
            this.btnSearchByLength.Size = new System.Drawing.Size(171, 32);
            this.btnSearchByLength.TabIndex = 9;
            this.btnSearchByLength.Text = "Искать по длине";
            this.btnSearchByLength.UseVisualStyleBackColor = true;
            this.btnSearchByLength.Click += new System.EventHandler(this.btnSearchByLength_Click);
            // 
            // listBoxWords
            // 
            this.listBoxWords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxWords.FormattingEnabled = true;
            this.listBoxWords.ItemHeight = 16;
            this.listBoxWords.Location = new System.Drawing.Point(11, 27);
            this.listBoxWords.Name = "listBoxWords";
            this.listBoxWords.Size = new System.Drawing.Size(434, 340);
            this.listBoxWords.TabIndex = 10;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(11, 459);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(148, 16);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Словарь не загружен";
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(11, 245);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(133, 16);
            this.lblPrefix.TabIndex = 12;
            this.lblPrefix.Text = "Начинается с букв:";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(137, 242);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(228, 22);
            this.txtPrefix.TabIndex = 13;
            // 
            // btnFilterByPrefix
            // 
            this.btnFilterByPrefix.Location = new System.Drawing.Point(377, 240);
            this.btnFilterByPrefix.Name = "btnFilterByPrefix";
            this.btnFilterByPrefix.Size = new System.Drawing.Size(114, 27);
            this.btnFilterByPrefix.TabIndex = 14;
            this.btnFilterByPrefix.Text = "Показать";
            this.btnFilterByPrefix.UseVisualStyleBackColor = true;
            this.btnFilterByPrefix.Click += new System.EventHandler(this.btnFilterByPrefix_Click);
            // 
            // groupBoxDictionary
            // 
            this.groupBoxDictionary.Controls.Add(this.btnLoadDict);
            this.groupBoxDictionary.Controls.Add(this.btnAddWord);
            this.groupBoxDictionary.Controls.Add(this.btnRemoveWord);
            this.groupBoxDictionary.Controls.Add(this.btnSaveDict);
            this.groupBoxDictionary.Controls.Add(this.btnCreateCustomDict);
            this.groupBoxDictionary.Controls.Add(this.txtWord);
            this.groupBoxDictionary.Controls.Add(this.lblWord);
            this.groupBoxDictionary.Location = new System.Drawing.Point(14, 13);
            this.groupBoxDictionary.Name = "groupBoxDictionary";
            this.groupBoxDictionary.Size = new System.Drawing.Size(400, 245);
            this.groupBoxDictionary.TabIndex = 15;
            this.groupBoxDictionary.TabStop = false;
            this.groupBoxDictionary.Text = "Работа со словарём";
            // 
            // lblWord
            // 
            this.lblWord.AutoSize = true;
            this.lblWord.Location = new System.Drawing.Point(34, 32);
            this.lblWord.Name = "lblWord";
            this.lblWord.Size = new System.Drawing.Size(94, 16);
            this.lblWord.TabIndex = 6;
            this.lblWord.Text = "Новое слово:";
            // 
            // groupBoxSearch
            // 
            this.groupBoxSearch.Controls.Add(this.txtSyllables);
            this.groupBoxSearch.Controls.Add(this.txtLength);
            this.groupBoxSearch.Controls.Add(this.button2);
            this.groupBoxSearch.Controls.Add(this.button1);
            this.groupBoxSearch.Controls.Add(this.btnSearchBySyllables);
            this.groupBoxSearch.Controls.Add(this.btnSearchByLength);
            this.groupBoxSearch.Controls.Add(this.lblSyllables);
            this.groupBoxSearch.Controls.Add(this.lblLength);
            this.groupBoxSearch.Location = new System.Drawing.Point(14, 267);
            this.groupBoxSearch.Name = "groupBoxSearch";
            this.groupBoxSearch.Size = new System.Drawing.Size(400, 128);
            this.groupBoxSearch.TabIndex = 16;
            this.groupBoxSearch.TabStop = false;
            this.groupBoxSearch.Text = "Поиск (Вариант 1)";
            // 
            // lblSyllables
            // 
            this.lblSyllables.AutoSize = true;
            this.lblSyllables.Location = new System.Drawing.Point(-2, 35);
            this.lblSyllables.Name = "lblSyllables";
            this.lblSyllables.Size = new System.Drawing.Size(136, 16);
            this.lblSyllables.TabIndex = 10;
            this.lblSyllables.Text = "Количество слогов:";
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(-2, 78);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(122, 16);
            this.lblLength.TabIndex = 11;
            this.lblLength.Text = "Количество букв:";
            // 
            // groupBoxResults
            // 
            this.groupBoxResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxResults.Controls.Add(this.listBoxWords);
            this.groupBoxResults.Controls.Add(this.lblPrefix);
            this.groupBoxResults.Controls.Add(this.txtPrefix);
            this.groupBoxResults.Controls.Add(this.btnFilterByPrefix);
            this.groupBoxResults.Location = new System.Drawing.Point(434, 13);
            this.groupBoxResults.Name = "groupBoxResults";
            this.groupBoxResults.Size = new System.Drawing.Size(457, 384);
            this.groupBoxResults.TabIndex = 17;
            this.groupBoxResults.TabStop = false;
            this.groupBoxResults.Text = "Словарь (алфавитный порядок)";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(260, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 32);
            this.button1.TabIndex = 9;
            this.button1.Text = "Искать по длине";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnSearchByLength_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(260, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 32);
            this.button2.TabIndex = 8;
            this.button2.Text = "Искать по слогам";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnSearchBySyllables_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 501);
            this.Controls.Add(this.groupBoxResults);
            this.Controls.Add(this.groupBoxSearch);
            this.Controls.Add(this.groupBoxDictionary);
            this.Controls.Add(this.lblStatus);
            this.MinimumSize = new System.Drawing.Size(930, 540);
            this.Name = "Form1";
            this.Text = "Task 5 R - Работа со словарём (Вариант 1)";
            this.groupBoxDictionary.ResumeLayout(false);
            this.groupBoxDictionary.PerformLayout();
            this.groupBoxSearch.ResumeLayout(false);
            this.groupBoxSearch.PerformLayout();
            this.groupBoxResults.ResumeLayout(false);
            this.groupBoxResults.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btnLoadDict;
        private System.Windows.Forms.Button btnAddWord;
        private System.Windows.Forms.Button btnRemoveWord;
        private System.Windows.Forms.Button btnSaveDict;
        private System.Windows.Forms.Button btnCreateCustomDict;
        private System.Windows.Forms.TextBox txtWord;
        private System.Windows.Forms.TextBox txtSyllables;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Button btnSearchBySyllables;
        private System.Windows.Forms.Button btnSearchByLength;
        private System.Windows.Forms.ListBox listBoxWords;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Button btnFilterByPrefix;
        private System.Windows.Forms.GroupBox groupBoxDictionary;
        private System.Windows.Forms.GroupBox groupBoxSearch;
        private System.Windows.Forms.GroupBox groupBoxResults;
        private System.Windows.Forms.Label lblSyllables;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblWord;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}