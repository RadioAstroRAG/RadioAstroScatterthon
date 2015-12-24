namespace LogComponent
{
    partial class LogFileViewer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnApplyClassification = new System.Windows.Forms.Button();
            this.comboClasses = new System.Windows.Forms.ComboBox();
            this.btnMerge = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btnSplit = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUndo
            // 
            this.btnUndo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUndo.Enabled = false;
            this.btnUndo.Location = new System.Drawing.Point(5, 424);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(75, 23);
            this.btnUndo.TabIndex = 0;
            this.btnUndo.TabStop = false;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnApplyClassification
            // 
            this.btnApplyClassification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApplyClassification.Enabled = false;
            this.btnApplyClassification.Location = new System.Drawing.Point(203, 424);
            this.btnApplyClassification.Name = "btnApplyClassification";
            this.btnApplyClassification.Size = new System.Drawing.Size(75, 23);
            this.btnApplyClassification.TabIndex = 5;
            this.btnApplyClassification.Text = "Apply";
            this.btnApplyClassification.UseVisualStyleBackColor = true;
            this.btnApplyClassification.Click += new System.EventHandler(this.btnApplyClassification_Click);
            // 
            // comboClasses
            // 
            this.comboClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboClasses.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboClasses.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboClasses.FormattingEnabled = true;
            this.comboClasses.Items.AddRange(new object[] {
            "1 - Aircraft",
            "2 - Head echo",
            "3 - Interference",
            "4 - Meteor trail",
            "5 - Moon bounce",
            "6 - Remove",
            "7 - Satellite",
            "8 - Query"});
            this.comboClasses.Location = new System.Drawing.Point(84, 425);
            this.comboClasses.Name = "comboClasses";
            this.comboClasses.Size = new System.Drawing.Size(115, 21);
            this.comboClasses.TabIndex = 4;
            this.comboClasses.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // btnMerge
            // 
            this.btnMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMerge.Enabled = false;
            this.btnMerge.Location = new System.Drawing.Point(5, 396);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(75, 23);
            this.btnMerge.TabIndex = 1;
            this.btnMerge.Text = "Merge";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Location = new System.Drawing.Point(84, 398);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(115, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // btnSplit
            // 
            this.btnSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSplit.Enabled = false;
            this.btnSplit.Location = new System.Drawing.Point(203, 397);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(75, 23);
            this.btnSplit.TabIndex = 3;
            this.btnSplit.Text = "Split";
            this.btnSplit.UseVisualStyleBackColor = true;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(3, 3);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(641, 383);
            this.dgv.TabIndex = 6;
            this.dgv.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);
            // 
            // LogFileViewer
            // 
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnApplyClassification);
            this.Controls.Add(this.comboClasses);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btnSplit);
            this.Name = "LogFileViewer";
            this.Size = new System.Drawing.Size(647, 464);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LogFileViewer_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnApplyClassification;
        private System.Windows.Forms.ComboBox comboClasses;
        public System.Windows.Forms.Button btnMerge;
        public System.Windows.Forms.NumericUpDown numericUpDown1;
        public System.Windows.Forms.Button btnSplit;
        private System.Windows.Forms.DataGridView dgv;

    }
}
