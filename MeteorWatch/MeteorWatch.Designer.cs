using LogComponent;
using System;



namespace MeteorWatch
{
    partial class Scatterthon
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scatterthon));
            this.tabRmob = new System.Windows.Forms.TabPage();
            this.btnRecreateRmob = new System.Windows.Forms.Button();
            this.btnNormalise = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioRmobMonth = new System.Windows.Forms.RadioButton();
            this.radioRmobYear = new System.Windows.Forms.RadioButton();
            this.radioRmobAll = new System.Windows.Forms.RadioButton();
            this.lblRecreateRmob = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtWhatIf = new System.Windows.Forms.TextBox();
            this.radioColourByRandom = new System.Windows.Forms.RadioButton();
            this.radioColourByMonth = new System.Windows.Forms.RadioButton();
            this.radioColourByYear = new System.Windows.Forms.RadioButton();
            this.lblNormalise = new System.Windows.Forms.Label();
            this.dtpRMOB = new System.Windows.Forms.DateTimePicker();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblPreviewedMonth = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Colours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnImportColorgram = new System.Windows.Forms.Button();
            this.tabConfig = new System.Windows.Forms.TabPage();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblConfigNotSaved = new System.Windows.Forms.Label();
            this.checkDropHistory = new System.Windows.Forms.CheckBox();
            this.checkGenerateRMOB = new System.Windows.Forms.CheckBox();
            this.txtScreenshotsDelay = new System.Windows.Forms.TextBox();
            this.lblScreenshotsDelay = new System.Windows.Forms.Label();
            this.checkPagination = new System.Windows.Forms.CheckBox();
            this.checkShowFreq = new System.Windows.Forms.CheckBox();
            this.checkShowNoise = new System.Windows.Forms.CheckBox();
            this.checkShowSignal = new System.Windows.Forms.CheckBox();
            this.txtContinuousCaptureSpan = new System.Windows.Forms.TextBox();
            this.lblContinuousCaptureSpan = new System.Windows.Forms.Label();
            this.txtHighResolutionScreenshots = new System.Windows.Forms.TextBox();
            this.lblHighResolutionScreenshotsPath = new System.Windows.Forms.Label();
            this.btnNewStation = new System.Windows.Forms.Button();
            this.chkDefaultStation = new System.Windows.Forms.CheckBox();
            this.cmbStationNames = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdateTopMeteorCount = new System.Windows.Forms.Button();
            this.txtAnnualTopMeteorCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOriginalScreenshotsDirectory = new System.Windows.Forms.TextBox();
            this.txtOriginalLogsDirectory = new System.Windows.Forms.TextBox();
            this.lblScreenshotsPath = new System.Windows.Forms.Label();
            this.lblLogFilePath = new System.Windows.Forms.Label();
            this.btnApplySettings = new System.Windows.Forms.Button();
            this.dlgSaveLogFile = new System.Windows.Forms.SaveFileDialog();
            this.dlgOpenDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgSaveDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTipSync = new System.Windows.Forms.ToolTip(this.components);
            this.btnPrevLog = new System.Windows.Forms.Button();
            this.btnNextLog = new System.Windows.Forms.Button();
            this.btnCopyScreenshot = new System.Windows.Forms.Button();
            this.btnSaveProgress = new System.Windows.Forms.Button();
            this.txtLogIndex = new System.Windows.Forms.TextBox();
            this.btnImageScrollLeft = new System.Windows.Forms.Button();
            this.btnImageScrollRight = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabCleanse = new System.Windows.Forms.TabPage();
            this.innerSplitContainer = new System.Windows.Forms.SplitContainer();
            this.logFileComponent = new LogComponent.LogFileViewer();
            this.lblLogName = new System.Windows.Forms.Label();
            this.lblLogIndexPrompt = new System.Windows.Forms.Label();
            this.lblLogScroll = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.picBoxScreenshot = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.picBoxHighRes1 = new System.Windows.Forms.PictureBox();
            this.picBoxHighRes2 = new System.Windows.Forms.PictureBox();
            this.picBoxHighRes3 = new System.Windows.Forms.PictureBox();
            this.lblScreenshotName = new System.Windows.Forms.Label();
            this.lblScreenshotCount = new System.Windows.Forms.Label();
            this.tabPreview = new System.Windows.Forms.TabPage();
            this.lblCategories = new System.Windows.Forms.Label();
            this.lblTimeUnit = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnView = new System.Windows.Forms.Button();
            this.comboPeriod = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkedListClasses = new System.Windows.Forms.CheckedListBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.TwentyFourHourGrid = new System.Windows.Forms.DataGridView();
            this.Column33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabAnnual = new System.Windows.Forms.TabPage();
            this.tabFilter = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkedListClassesFilter = new System.Windows.Forms.CheckedListBox();
            this.lblCategoriesFilter = new System.Windows.Forms.Label();
            this.btnCoalesce = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioCoalesceMonth = new System.Windows.Forms.RadioButton();
            this.radioCoalesceYear = new System.Windows.Forms.RadioButton();
            this.radioCoalesceAll = new System.Windows.Forms.RadioButton();
            this.lblCoalesceLogs = new System.Windows.Forms.Label();
            this.dtpFilter = new System.Windows.Forms.DateTimePicker();
            this.logFileViewerFilter = new LogComponent.LogFileViewer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutScatterthonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabRmob.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabConfig.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabCleanse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.innerSplitContainer)).BeginInit();
            this.innerSplitContainer.Panel1.SuspendLayout();
            this.innerSplitContainer.Panel2.SuspendLayout();
            this.innerSplitContainer.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxScreenshot)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxHighRes1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxHighRes2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxHighRes3)).BeginInit();
            this.tabPreview.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TwentyFourHourGrid)).BeginInit();
            this.tabFilter.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabRmob
            // 
            this.tabRmob.Controls.Add(this.btnRecreateRmob);
            this.tabRmob.Controls.Add(this.btnNormalise);
            this.tabRmob.Controls.Add(this.groupBox3);
            this.tabRmob.Controls.Add(this.lblRecreateRmob);
            this.tabRmob.Controls.Add(this.groupBox2);
            this.tabRmob.Controls.Add(this.lblNormalise);
            this.tabRmob.Controls.Add(this.dtpRMOB);
            this.tabRmob.Controls.Add(this.btnExport);
            this.tabRmob.Controls.Add(this.lblPreviewedMonth);
            this.tabRmob.Controls.Add(this.dataGridView1);
            this.tabRmob.Controls.Add(this.btnImportColorgram);
            this.tabRmob.Location = new System.Drawing.Point(4, 4);
            this.tabRmob.Name = "tabRmob";
            this.tabRmob.Padding = new System.Windows.Forms.Padding(3);
            this.tabRmob.Size = new System.Drawing.Size(993, 676);
            this.tabRmob.TabIndex = 0;
            this.tabRmob.Text = "RMOB";
            this.tabRmob.UseVisualStyleBackColor = true;
            // 
            // btnRecreateRmob
            // 
            this.btnRecreateRmob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecreateRmob.Location = new System.Drawing.Point(895, 360);
            this.btnRecreateRmob.Name = "btnRecreateRmob";
            this.btnRecreateRmob.Size = new System.Drawing.Size(75, 23);
            this.btnRecreateRmob.TabIndex = 32;
            this.btnRecreateRmob.Text = "Go";
            this.btnRecreateRmob.UseVisualStyleBackColor = true;
            this.btnRecreateRmob.Click += new System.EventHandler(this.btnRecreateRmob_Click);
            // 
            // btnNormalise
            // 
            this.btnNormalise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNormalise.Location = new System.Drawing.Point(895, 192);
            this.btnNormalise.Name = "btnNormalise";
            this.btnNormalise.Size = new System.Drawing.Size(75, 23);
            this.btnNormalise.TabIndex = 31;
            this.btnNormalise.Text = "Go";
            this.btnNormalise.UseVisualStyleBackColor = true;
            this.btnNormalise.Click += new System.EventHandler(this.btnNormalise_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.radioRmobMonth);
            this.groupBox3.Controls.Add(this.radioRmobYear);
            this.groupBox3.Controls.Add(this.radioRmobAll);
            this.groupBox3.Location = new System.Drawing.Point(818, 253);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(151, 100);
            this.groupBox3.TabIndex = 30;
            this.groupBox3.TabStop = false;
            // 
            // radioRmobMonth
            // 
            this.radioRmobMonth.AutoSize = true;
            this.radioRmobMonth.Location = new System.Drawing.Point(11, 68);
            this.radioRmobMonth.Name = "radioRmobMonth";
            this.radioRmobMonth.Size = new System.Drawing.Size(100, 17);
            this.radioRmobMonth.TabIndex = 2;
            this.radioRmobMonth.Text = "Selected Month";
            this.radioRmobMonth.UseVisualStyleBackColor = true;
            // 
            // radioRmobYear
            // 
            this.radioRmobYear.AutoSize = true;
            this.radioRmobYear.Location = new System.Drawing.Point(11, 44);
            this.radioRmobYear.Name = "radioRmobYear";
            this.radioRmobYear.Size = new System.Drawing.Size(92, 17);
            this.radioRmobYear.TabIndex = 1;
            this.radioRmobYear.Text = "Selected Year";
            this.radioRmobYear.UseVisualStyleBackColor = true;
            // 
            // radioRmobAll
            // 
            this.radioRmobAll.AutoSize = true;
            this.radioRmobAll.Checked = true;
            this.radioRmobAll.Location = new System.Drawing.Point(11, 20);
            this.radioRmobAll.Name = "radioRmobAll";
            this.radioRmobAll.Size = new System.Drawing.Size(62, 17);
            this.radioRmobAll.TabIndex = 0;
            this.radioRmobAll.TabStop = true;
            this.radioRmobAll.Text = "All Time";
            this.radioRmobAll.UseVisualStyleBackColor = true;
            // 
            // lblRecreateRmob
            // 
            this.lblRecreateRmob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecreateRmob.AutoSize = true;
            this.lblRecreateRmob.Location = new System.Drawing.Point(818, 240);
            this.lblRecreateRmob.Name = "lblRecreateRmob";
            this.lblRecreateRmob.Size = new System.Drawing.Size(131, 13);
            this.lblRecreateRmob.TabIndex = 29;
            this.lblRecreateRmob.Text = "Recreate RMOB files for...";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtWhatIf);
            this.groupBox2.Controls.Add(this.radioColourByRandom);
            this.groupBox2.Controls.Add(this.radioColourByMonth);
            this.groupBox2.Controls.Add(this.radioColourByYear);
            this.groupBox2.Location = new System.Drawing.Point(818, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(151, 107);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            // 
            // txtWhatIf
            // 
            this.txtWhatIf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtWhatIf.Location = new System.Drawing.Point(77, 75);
            this.txtWhatIf.Name = "txtWhatIf";
            this.txtWhatIf.Size = new System.Drawing.Size(51, 20);
            this.txtWhatIf.TabIndex = 27;
            // 
            // radioColourByRandom
            // 
            this.radioColourByRandom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioColourByRandom.AutoSize = true;
            this.radioColourByRandom.Location = new System.Drawing.Point(11, 76);
            this.radioColourByRandom.Name = "radioColourByRandom";
            this.radioColourByRandom.Size = new System.Drawing.Size(65, 17);
            this.radioColourByRandom.TabIndex = 26;
            this.radioColourByRandom.Text = "Count of";
            this.radioColourByRandom.UseVisualStyleBackColor = true;
            // 
            // radioColourByMonth
            // 
            this.radioColourByMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioColourByMonth.AutoSize = true;
            this.radioColourByMonth.Checked = true;
            this.radioColourByMonth.Location = new System.Drawing.Point(11, 49);
            this.radioColourByMonth.Name = "radioColourByMonth";
            this.radioColourByMonth.Size = new System.Drawing.Size(108, 17);
            this.radioColourByMonth.TabIndex = 25;
            this.radioColourByMonth.TabStop = true;
            this.radioColourByMonth.Text = "Top Month Count";
            this.radioColourByMonth.UseVisualStyleBackColor = true;
            // 
            // radioColourByYear
            // 
            this.radioColourByYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioColourByYear.AutoSize = true;
            this.radioColourByYear.Location = new System.Drawing.Point(11, 20);
            this.radioColourByYear.Name = "radioColourByYear";
            this.radioColourByYear.Size = new System.Drawing.Size(111, 17);
            this.radioColourByYear.TabIndex = 24;
            this.radioColourByYear.Text = "Top Annual Count";
            this.radioColourByYear.UseVisualStyleBackColor = true;
            // 
            // lblNormalise
            // 
            this.lblNormalise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNormalise.AutoSize = true;
            this.lblNormalise.Location = new System.Drawing.Point(818, 65);
            this.lblNormalise.Name = "lblNormalise";
            this.lblNormalise.Size = new System.Drawing.Size(147, 13);
            this.lblNormalise.TabIndex = 27;
            this.lblNormalise.Text = "Normalise Colourgramme for...";
            // 
            // dtpRMOB
            // 
            this.dtpRMOB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpRMOB.Location = new System.Drawing.Point(818, 23);
            this.dtpRMOB.Name = "dtpRMOB";
            this.dtpRMOB.Size = new System.Drawing.Size(151, 20);
            this.dtpRMOB.TabIndex = 26;
            this.dtpRMOB.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(906, 635);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExportRmobFile_Click);
            // 
            // lblPreviewedMonth
            // 
            this.lblPreviewedMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreviewedMonth.AutoSize = true;
            this.lblPreviewedMonth.Location = new System.Drawing.Point(15, 457);
            this.lblPreviewedMonth.Name = "lblPreviewedMonth";
            this.lblPreviewedMonth.Size = new System.Drawing.Size(138, 13);
            this.lblPreviewedMonth.TabIndex = 6;
            this.lblPreviewedMonth.Text = "Displayed month and year...";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16,
            this.Column17,
            this.Column18,
            this.Column19,
            this.Column20,
            this.Column21,
            this.Column22,
            this.Column23,
            this.Column24,
            this.Column25,
            this.Column26,
            this.Column27,
            this.Column28,
            this.Column29,
            this.Column30,
            this.Column31,
            this.Column32,
            this.Colours,
            this.Scale});
            this.dataGridView1.Location = new System.Drawing.Point(18, 10);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 55;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.Size = new System.Drawing.Size(686, 429);
            this.dataGridView1.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.Width = 27;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.Width = 27;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "3";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.Width = 27;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "4";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.Width = 27;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "5";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column5.Width = 27;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "6";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column6.Width = 27;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "7";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column7.Width = 27;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "8";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column8.Width = 27;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "9";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column9.Width = 27;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "10";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column10.Width = 27;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "11";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column11.Width = 27;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "12";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column12.Width = 27;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "13";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column13.Width = 27;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "14";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column14.Width = 27;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "15";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column15.Width = 27;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "16";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column16.Width = 27;
            // 
            // Column17
            // 
            this.Column17.HeaderText = "17";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column17.Width = 27;
            // 
            // Column18
            // 
            this.Column18.HeaderText = "18";
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            this.Column18.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column18.Width = 27;
            // 
            // Column19
            // 
            this.Column19.HeaderText = "19";
            this.Column19.Name = "Column19";
            this.Column19.ReadOnly = true;
            this.Column19.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column19.Width = 27;
            // 
            // Column20
            // 
            this.Column20.HeaderText = "20";
            this.Column20.Name = "Column20";
            this.Column20.ReadOnly = true;
            this.Column20.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column20.Width = 27;
            // 
            // Column21
            // 
            this.Column21.HeaderText = "21";
            this.Column21.Name = "Column21";
            this.Column21.ReadOnly = true;
            this.Column21.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column21.Width = 27;
            // 
            // Column22
            // 
            this.Column22.HeaderText = "22";
            this.Column22.Name = "Column22";
            this.Column22.ReadOnly = true;
            this.Column22.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column22.Width = 27;
            // 
            // Column23
            // 
            this.Column23.HeaderText = "23";
            this.Column23.Name = "Column23";
            this.Column23.ReadOnly = true;
            this.Column23.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column23.Width = 27;
            // 
            // Column24
            // 
            this.Column24.HeaderText = "24";
            this.Column24.Name = "Column24";
            this.Column24.ReadOnly = true;
            this.Column24.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column24.Width = 27;
            // 
            // Column25
            // 
            this.Column25.HeaderText = "25";
            this.Column25.Name = "Column25";
            this.Column25.ReadOnly = true;
            this.Column25.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column25.Width = 27;
            // 
            // Column26
            // 
            this.Column26.HeaderText = "26";
            this.Column26.Name = "Column26";
            this.Column26.ReadOnly = true;
            this.Column26.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column26.Width = 27;
            // 
            // Column27
            // 
            this.Column27.HeaderText = "27";
            this.Column27.Name = "Column27";
            this.Column27.ReadOnly = true;
            this.Column27.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column27.Width = 27;
            // 
            // Column28
            // 
            this.Column28.HeaderText = "28";
            this.Column28.Name = "Column28";
            this.Column28.ReadOnly = true;
            this.Column28.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column28.Width = 27;
            // 
            // Column29
            // 
            this.Column29.HeaderText = "29";
            this.Column29.Name = "Column29";
            this.Column29.ReadOnly = true;
            this.Column29.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column29.Width = 27;
            // 
            // Column30
            // 
            this.Column30.HeaderText = "30";
            this.Column30.Name = "Column30";
            this.Column30.ReadOnly = true;
            this.Column30.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column30.Width = 27;
            // 
            // Column31
            // 
            this.Column31.HeaderText = "31";
            this.Column31.Name = "Column31";
            this.Column31.ReadOnly = true;
            this.Column31.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column31.Width = 20;
            // 
            // Column32
            // 
            this.Column32.HeaderText = "";
            this.Column32.Name = "Column32";
            this.Column32.ReadOnly = true;
            this.Column32.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column32.Width = 17;
            // 
            // Colours
            // 
            this.Colours.HeaderText = "";
            this.Colours.Name = "Colours";
            this.Colours.ReadOnly = true;
            this.Colours.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Colours.Width = 17;
            // 
            // Scale
            // 
            this.Scale.HeaderText = "";
            this.Scale.Name = "Scale";
            this.Scale.ReadOnly = true;
            this.Scale.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Scale.Width = 50;
            // 
            // btnImportColorgram
            // 
            this.btnImportColorgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportColorgram.Location = new System.Drawing.Point(829, 635);
            this.btnImportColorgram.Name = "btnImportColorgram";
            this.btnImportColorgram.Size = new System.Drawing.Size(75, 23);
            this.btnImportColorgram.TabIndex = 7;
            this.btnImportColorgram.Text = "Import";
            this.btnImportColorgram.UseVisualStyleBackColor = true;
            this.btnImportColorgram.Click += new System.EventHandler(this.btnImportColorgram_Click);
            // 
            // tabConfig
            // 
            this.tabConfig.Controls.Add(this.btnSaveConfig);
            this.tabConfig.Controls.Add(this.panel1);
            this.tabConfig.Controls.Add(this.btnApplySettings);
            this.tabConfig.Location = new System.Drawing.Point(4, 4);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfig.Size = new System.Drawing.Size(993, 676);
            this.tabConfig.TabIndex = 3;
            this.tabConfig.Text = "Config";
            this.tabConfig.UseVisualStyleBackColor = true;
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveConfig.Location = new System.Drawing.Point(828, 635);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(75, 23);
            this.btnSaveConfig.TabIndex = 18;
            this.btnSaveConfig.Text = "Save";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.lblConfigNotSaved);
            this.panel1.Controls.Add(this.checkDropHistory);
            this.panel1.Controls.Add(this.checkGenerateRMOB);
            this.panel1.Controls.Add(this.txtScreenshotsDelay);
            this.panel1.Controls.Add(this.lblScreenshotsDelay);
            this.panel1.Controls.Add(this.checkPagination);
            this.panel1.Controls.Add(this.checkShowFreq);
            this.panel1.Controls.Add(this.checkShowNoise);
            this.panel1.Controls.Add(this.checkShowSignal);
            this.panel1.Controls.Add(this.txtContinuousCaptureSpan);
            this.panel1.Controls.Add(this.lblContinuousCaptureSpan);
            this.panel1.Controls.Add(this.txtHighResolutionScreenshots);
            this.panel1.Controls.Add(this.lblHighResolutionScreenshotsPath);
            this.panel1.Controls.Add(this.btnNewStation);
            this.panel1.Controls.Add(this.chkDefaultStation);
            this.panel1.Controls.Add(this.cmbStationNames);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnUpdateTopMeteorCount);
            this.panel1.Controls.Add(this.txtAnnualTopMeteorCount);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtOriginalScreenshotsDirectory);
            this.panel1.Controls.Add(this.txtOriginalLogsDirectory);
            this.panel1.Controls.Add(this.lblScreenshotsPath);
            this.panel1.Controls.Add(this.lblLogFilePath);
            this.panel1.Location = new System.Drawing.Point(18, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(964, 610);
            this.panel1.TabIndex = 17;
            // 
            // lblConfigNotSaved
            // 
            this.lblConfigNotSaved.AutoSize = true;
            this.lblConfigNotSaved.Location = new System.Drawing.Point(37, 289);
            this.lblConfigNotSaved.Name = "lblConfigNotSaved";
            this.lblConfigNotSaved.Size = new System.Drawing.Size(382, 13);
            this.lblConfigNotSaved.TabIndex = 38;
            this.lblConfigNotSaved.Text = "Settings below are not station-specific and will not be saved in configuration fi" +
    "le:";
            // 
            // checkDropHistory
            // 
            this.checkDropHistory.AutoSize = true;
            this.checkDropHistory.Checked = true;
            this.checkDropHistory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkDropHistory.Location = new System.Drawing.Point(216, 417);
            this.checkDropHistory.Name = "checkDropHistory";
            this.checkDropHistory.Size = new System.Drawing.Size(244, 17);
            this.checkDropHistory.TabIndex = 37;
            this.checkDropHistory.Text = "Drop Undo History upon Browsing to Next Log";
            this.toolTipSync.SetToolTip(this.checkDropHistory, "Drop my Undo history on navigating to next log file. \r\nTicking this box will stop" +
        " Scatterthon taking up too much computer memory.");
            this.checkDropHistory.UseVisualStyleBackColor = true;
            // 
            // checkGenerateRMOB
            // 
            this.checkGenerateRMOB.AutoSize = true;
            this.checkGenerateRMOB.Checked = true;
            this.checkGenerateRMOB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkGenerateRMOB.Location = new System.Drawing.Point(216, 392);
            this.checkGenerateRMOB.Name = "checkGenerateRMOB";
            this.checkGenerateRMOB.Size = new System.Drawing.Size(169, 17);
            this.checkGenerateRMOB.TabIndex = 36;
            this.checkGenerateRMOB.Text = "Generate RMOB as I navigate";
            this.toolTipSync.SetToolTip(this.checkGenerateRMOB, "Generate RMOB data on navigating to next log file.\r\nUnticking this option speeds " +
        "up going from file to file.");
            this.checkGenerateRMOB.UseVisualStyleBackColor = true;
            // 
            // txtScreenshotsDelay
            // 
            this.txtScreenshotsDelay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScreenshotsDelay.Location = new System.Drawing.Point(216, 183);
            this.txtScreenshotsDelay.Name = "txtScreenshotsDelay";
            this.txtScreenshotsDelay.Size = new System.Drawing.Size(681, 20);
            this.txtScreenshotsDelay.TabIndex = 35;
            // 
            // lblScreenshotsDelay
            // 
            this.lblScreenshotsDelay.AutoSize = true;
            this.lblScreenshotsDelay.Location = new System.Drawing.Point(130, 190);
            this.lblScreenshotsDelay.Name = "lblScreenshotsDelay";
            this.lblScreenshotsDelay.Size = new System.Drawing.Size(78, 13);
            this.lblScreenshotsDelay.TabIndex = 34;
            this.lblScreenshotsDelay.Text = "Offset Duration";
            // 
            // checkPagination
            // 
            this.checkPagination.AutoSize = true;
            this.checkPagination.Enabled = false;
            this.checkPagination.Location = new System.Drawing.Point(805, 156);
            this.checkPagination.Name = "checkPagination";
            this.checkPagination.Size = new System.Drawing.Size(98, 17);
            this.checkPagination.TabIndex = 33;
            this.checkPagination.Text = "Use Pagination";
            this.checkPagination.UseVisualStyleBackColor = true;
            this.checkPagination.CheckedChanged += new System.EventHandler(this.checkPagination_CheckedChanged);
            // 
            // checkShowFreq
            // 
            this.checkShowFreq.AutoSize = true;
            this.checkShowFreq.Location = new System.Drawing.Point(216, 366);
            this.checkShowFreq.Name = "checkShowFreq";
            this.checkShowFreq.Size = new System.Drawing.Size(115, 17);
            this.checkShowFreq.TabIndex = 32;
            this.checkShowFreq.Text = "Show Freq Column";
            this.checkShowFreq.UseVisualStyleBackColor = true;
            // 
            // checkShowNoise
            // 
            this.checkShowNoise.AutoSize = true;
            this.checkShowNoise.Location = new System.Drawing.Point(216, 342);
            this.checkShowNoise.Name = "checkShowNoise";
            this.checkShowNoise.Size = new System.Drawing.Size(121, 17);
            this.checkShowNoise.TabIndex = 31;
            this.checkShowNoise.Text = "Show Noise Column";
            this.checkShowNoise.UseVisualStyleBackColor = true;
            // 
            // checkShowSignal
            // 
            this.checkShowSignal.AutoSize = true;
            this.checkShowSignal.Location = new System.Drawing.Point(216, 318);
            this.checkShowSignal.Name = "checkShowSignal";
            this.checkShowSignal.Size = new System.Drawing.Size(123, 17);
            this.checkShowSignal.TabIndex = 30;
            this.checkShowSignal.Text = "Show Signal Column";
            this.checkShowSignal.UseVisualStyleBackColor = true;
            // 
            // txtContinuousCaptureSpan
            // 
            this.txtContinuousCaptureSpan.Enabled = false;
            this.txtContinuousCaptureSpan.Location = new System.Drawing.Point(216, 154);
            this.txtContinuousCaptureSpan.Name = "txtContinuousCaptureSpan";
            this.txtContinuousCaptureSpan.Size = new System.Drawing.Size(582, 20);
            this.txtContinuousCaptureSpan.TabIndex = 29;
            // 
            // lblContinuousCaptureSpan
            // 
            this.lblContinuousCaptureSpan.AutoSize = true;
            this.lblContinuousCaptureSpan.Location = new System.Drawing.Point(34, 160);
            this.lblContinuousCaptureSpan.Name = "lblContinuousCaptureSpan";
            this.lblContinuousCaptureSpan.Size = new System.Drawing.Size(174, 13);
            this.lblContinuousCaptureSpan.TabIndex = 28;
            this.lblContinuousCaptureSpan.Text = "Review Screenshot Duration (secs)";
            // 
            // txtHighResolutionScreenshots
            // 
            this.txtHighResolutionScreenshots.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHighResolutionScreenshots.Location = new System.Drawing.Point(216, 126);
            this.txtHighResolutionScreenshots.Name = "txtHighResolutionScreenshots";
            this.txtHighResolutionScreenshots.Size = new System.Drawing.Size(681, 20);
            this.txtHighResolutionScreenshots.TabIndex = 27;
            this.txtHighResolutionScreenshots.DoubleClick += new System.EventHandler(this.txtHighResolutionScreenshots_DoubleClick);
            // 
            // lblHighResolutionScreenshotsPath
            // 
            this.lblHighResolutionScreenshotsPath.AutoSize = true;
            this.lblHighResolutionScreenshotsPath.Location = new System.Drawing.Point(112, 130);
            this.lblHighResolutionScreenshotsPath.Name = "lblHighResolutionScreenshotsPath";
            this.lblHighResolutionScreenshotsPath.Size = new System.Drawing.Size(96, 13);
            this.lblHighResolutionScreenshotsPath.TabIndex = 26;
            this.lblHighResolutionScreenshotsPath.Text = "Highlights directory";
            // 
            // btnNewStation
            // 
            this.btnNewStation.Location = new System.Drawing.Point(216, 25);
            this.btnNewStation.Name = "btnNewStation";
            this.btnNewStation.Size = new System.Drawing.Size(75, 23);
            this.btnNewStation.TabIndex = 25;
            this.btnNewStation.Text = "New...";
            this.btnNewStation.UseVisualStyleBackColor = true;
            this.btnNewStation.Click += new System.EventHandler(this.btnNewStation_Click);
            // 
            // chkDefaultStation
            // 
            this.chkDefaultStation.AutoSize = true;
            this.chkDefaultStation.Checked = true;
            this.chkDefaultStation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDefaultStation.Location = new System.Drawing.Point(440, 28);
            this.chkDefaultStation.Name = "chkDefaultStation";
            this.chkDefaultStation.Size = new System.Drawing.Size(134, 17);
            this.chkDefaultStation.TabIndex = 24;
            this.chkDefaultStation.Text = "Save as default station";
            this.chkDefaultStation.UseVisualStyleBackColor = true;
            // 
            // cmbStationNames
            // 
            this.cmbStationNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStationNames.FormattingEnabled = true;
            this.cmbStationNames.Location = new System.Drawing.Point(303, 26);
            this.cmbStationNames.Name = "cmbStationNames";
            this.cmbStationNames.Size = new System.Drawing.Size(121, 21);
            this.cmbStationNames.TabIndex = 23;
            this.cmbStationNames.SelectedIndexChanged += new System.EventHandler(this.cmbStationNames_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Station Name";
            // 
            // btnUpdateTopMeteorCount
            // 
            this.btnUpdateTopMeteorCount.Location = new System.Drawing.Point(276, 233);
            this.btnUpdateTopMeteorCount.Name = "btnUpdateTopMeteorCount";
            this.btnUpdateTopMeteorCount.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateTopMeteorCount.TabIndex = 20;
            this.btnUpdateTopMeteorCount.Text = "Apply";
            this.btnUpdateTopMeteorCount.UseVisualStyleBackColor = true;
            this.btnUpdateTopMeteorCount.Click += new System.EventHandler(this.btnUpdateTopMeteorCount_Click);
            // 
            // txtAnnualTopMeteorCount
            // 
            this.txtAnnualTopMeteorCount.Location = new System.Drawing.Point(216, 235);
            this.txtAnnualTopMeteorCount.Name = "txtAnnualTopMeteorCount";
            this.txtAnnualTopMeteorCount.Size = new System.Drawing.Size(54, 20);
            this.txtAnnualTopMeteorCount.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(88, 238);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Annual top meteor count";
            // 
            // txtOriginalScreenshotsDirectory
            // 
            this.txtOriginalScreenshotsDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOriginalScreenshotsDirectory.Location = new System.Drawing.Point(216, 98);
            this.txtOriginalScreenshotsDirectory.Name = "txtOriginalScreenshotsDirectory";
            this.txtOriginalScreenshotsDirectory.Size = new System.Drawing.Size(681, 20);
            this.txtOriginalScreenshotsDirectory.TabIndex = 2;
            this.toolTipSync.SetToolTip(this.txtOriginalScreenshotsDirectory, "Double-click to navigate to directory");
            this.txtOriginalScreenshotsDirectory.DoubleClick += new System.EventHandler(this.txtOriginalScreenshotsDirectory_DoubleClick);
            // 
            // txtOriginalLogsDirectory
            // 
            this.txtOriginalLogsDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOriginalLogsDirectory.Location = new System.Drawing.Point(216, 70);
            this.txtOriginalLogsDirectory.Name = "txtOriginalLogsDirectory";
            this.txtOriginalLogsDirectory.Size = new System.Drawing.Size(681, 20);
            this.txtOriginalLogsDirectory.TabIndex = 0;
            this.toolTipSync.SetToolTip(this.txtOriginalLogsDirectory, "Double-click to navigate to directory");
            this.txtOriginalLogsDirectory.DoubleClick += new System.EventHandler(this.txtOriginalLogsDirectory_DoubleClick);
            // 
            // lblScreenshotsPath
            // 
            this.lblScreenshotsPath.AutoSize = true;
            this.lblScreenshotsPath.Location = new System.Drawing.Point(64, 101);
            this.lblScreenshotsPath.Name = "lblScreenshotsPath";
            this.lblScreenshotsPath.Size = new System.Drawing.Size(145, 13);
            this.lblScreenshotsPath.TabIndex = 9;
            this.lblScreenshotsPath.Text = "Original screenshots directory";
            // 
            // lblLogFilePath
            // 
            this.lblLogFilePath.AutoSize = true;
            this.lblLogFilePath.Location = new System.Drawing.Point(86, 73);
            this.lblLogFilePath.Name = "lblLogFilePath";
            this.lblLogFilePath.Size = new System.Drawing.Size(123, 13);
            this.lblLogFilePath.TabIndex = 7;
            this.lblLogFilePath.Text = "Original log files directory";
            // 
            // btnApplySettings
            // 
            this.btnApplySettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplySettings.Location = new System.Drawing.Point(907, 635);
            this.btnApplySettings.Name = "btnApplySettings";
            this.btnApplySettings.Size = new System.Drawing.Size(75, 23);
            this.btnApplySettings.TabIndex = 16;
            this.btnApplySettings.Text = "Reload";
            this.btnApplySettings.UseVisualStyleBackColor = true;
            this.btnApplySettings.Click += new System.EventHandler(this.btnApplySettings_Click);
            // 
            // btnPrevLog
            // 
            this.btnPrevLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevLog.Enabled = false;
            this.btnPrevLog.Location = new System.Drawing.Point(503, 628);
            this.btnPrevLog.Name = "btnPrevLog";
            this.btnPrevLog.Size = new System.Drawing.Size(75, 23);
            this.btnPrevLog.TabIndex = 10;
            this.btnPrevLog.Text = "<";
            this.toolTipSync.SetToolTip(this.btnPrevLog, "Go to previous log file. Save updated log file version.");
            this.btnPrevLog.UseVisualStyleBackColor = true;
            this.btnPrevLog.Click += new System.EventHandler(this.btnPrevLog_Click);
            // 
            // btnNextLog
            // 
            this.btnNextLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextLog.Enabled = false;
            this.btnNextLog.Location = new System.Drawing.Point(582, 628);
            this.btnNextLog.Name = "btnNextLog";
            this.btnNextLog.Size = new System.Drawing.Size(75, 23);
            this.btnNextLog.TabIndex = 11;
            this.btnNextLog.Text = ">";
            this.toolTipSync.SetToolTip(this.btnNextLog, "Go to next log file. Save updated log file version.");
            this.btnNextLog.UseVisualStyleBackColor = true;
            this.btnNextLog.Click += new System.EventHandler(this.btnNextLog_Click);
            // 
            // btnCopyScreenshot
            // 
            this.btnCopyScreenshot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopyScreenshot.Location = new System.Drawing.Point(20, 600);
            this.btnCopyScreenshot.Name = "btnCopyScreenshot";
            this.btnCopyScreenshot.Size = new System.Drawing.Size(75, 23);
            this.btnCopyScreenshot.TabIndex = 7;
            this.btnCopyScreenshot.Text = "Copy";
            this.toolTipSync.SetToolTip(this.btnCopyScreenshot, "Copy current screenshot to specified \'Highlights\' directory.\r\nThis will add it to" +
        " the 3-image scroll ribbon above.");
            this.btnCopyScreenshot.UseVisualStyleBackColor = true;
            this.btnCopyScreenshot.Click += new System.EventHandler(this.btnCopyScreenshot_Click);
            // 
            // btnSaveProgress
            // 
            this.btnSaveProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveProgress.Location = new System.Drawing.Point(20, 627);
            this.btnSaveProgress.Name = "btnSaveProgress";
            this.btnSaveProgress.Size = new System.Drawing.Size(75, 23);
            this.btnSaveProgress.TabIndex = 22;
            this.btnSaveProgress.Text = "Save";
            this.toolTipSync.SetToolTip(this.btnSaveProgress, "Save the cleansed version of your log file.");
            this.btnSaveProgress.UseVisualStyleBackColor = true;
            this.btnSaveProgress.Click += new System.EventHandler(this.btnSaveProgress_Click);
            // 
            // txtLogIndex
            // 
            this.txtLogIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogIndex.Location = new System.Drawing.Point(423, 630);
            this.txtLogIndex.Name = "txtLogIndex";
            this.txtLogIndex.Size = new System.Drawing.Size(73, 20);
            this.txtLogIndex.TabIndex = 19;
            this.toolTipSync.SetToolTip(this.txtLogIndex, "Enter the index of log file to go to, then press Enter on your keyboard.");
            this.txtLogIndex.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLogIndex_KeyPress);
            // 
            // btnImageScrollLeft
            // 
            this.btnImageScrollLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnImageScrollLeft.Location = new System.Drawing.Point(3, 3);
            this.btnImageScrollLeft.Name = "btnImageScrollLeft";
            this.btnImageScrollLeft.Size = new System.Drawing.Size(16, 129);
            this.btnImageScrollLeft.TabIndex = 5;
            this.btnImageScrollLeft.Text = "<";
            this.toolTipSync.SetToolTip(this.btnImageScrollLeft, "Navigate towards the first image in the \"Highlights\" directory.");
            this.btnImageScrollLeft.UseVisualStyleBackColor = true;
            this.btnImageScrollLeft.Click += new System.EventHandler(this.btnImageScrollLeft_Click);
            // 
            // btnImageScrollRight
            // 
            this.btnImageScrollRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnImageScrollRight.Location = new System.Drawing.Point(616, 3);
            this.btnImageScrollRight.Name = "btnImageScrollRight";
            this.btnImageScrollRight.Size = new System.Drawing.Size(17, 129);
            this.btnImageScrollRight.TabIndex = 6;
            this.btnImageScrollRight.Text = ">";
            this.toolTipSync.SetToolTip(this.btnImageScrollRight, "Navigate towards the last image in the \"Highlights\" directory");
            this.btnImageScrollRight.UseVisualStyleBackColor = true;
            this.btnImageScrollRight.Click += new System.EventHandler(this.btnImageScrollRight_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "rmob*.txt";
            // 
            // tabMain
            // 
            this.tabMain.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabMain.Controls.Add(this.tabCleanse);
            this.tabMain.Controls.Add(this.tabPreview);
            this.tabMain.Controls.Add(this.tabRmob);
            this.tabMain.Controls.Add(this.tabAnnual);
            this.tabMain.Controls.Add(this.tabConfig);
            this.tabMain.Controls.Add(this.tabFilter);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 24);
            this.tabMain.Multiline = true;
            this.tabMain.Name = "tabMain";
            this.tabMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1020, 684);
            this.tabMain.TabIndex = 1;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabCleanse
            // 
            this.tabCleanse.Controls.Add(this.innerSplitContainer);
            this.tabCleanse.Location = new System.Drawing.Point(4, 4);
            this.tabCleanse.Name = "tabCleanse";
            this.tabCleanse.Padding = new System.Windows.Forms.Padding(3);
            this.tabCleanse.Size = new System.Drawing.Size(993, 676);
            this.tabCleanse.TabIndex = 0;
            this.tabCleanse.Text = "Cleanse";
            this.tabCleanse.UseVisualStyleBackColor = true;
            // 
            // innerSplitContainer
            // 
            this.innerSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.innerSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.innerSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.innerSplitContainer.Name = "innerSplitContainer";
            // 
            // innerSplitContainer.Panel1
            // 
            this.innerSplitContainer.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.innerSplitContainer.Panel1.Controls.Add(this.logFileComponent);
            this.innerSplitContainer.Panel1.Controls.Add(this.lblLogName);
            this.innerSplitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // innerSplitContainer.Panel2
            // 
            this.innerSplitContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.innerSplitContainer.Panel2.Controls.Add(this.lblLogIndexPrompt);
            this.innerSplitContainer.Panel2.Controls.Add(this.btnSaveProgress);
            this.innerSplitContainer.Panel2.Controls.Add(this.txtLogIndex);
            this.innerSplitContainer.Panel2.Controls.Add(this.lblLogScroll);
            this.innerSplitContainer.Panel2.Controls.Add(this.btnPrevLog);
            this.innerSplitContainer.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.innerSplitContainer.Panel2.Controls.Add(this.btnNextLog);
            this.innerSplitContainer.Panel2.Controls.Add(this.btnCopyScreenshot);
            this.innerSplitContainer.Panel2.Controls.Add(this.lblScreenshotName);
            this.innerSplitContainer.Panel2.Controls.Add(this.lblScreenshotCount);
            this.innerSplitContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.innerSplitContainer.Panel2MinSize = 652;
            this.innerSplitContainer.Size = new System.Drawing.Size(987, 670);
            this.innerSplitContainer.SplitterDistance = 309;
            this.innerSplitContainer.TabIndex = 0;
            // 
            // logFileComponent
            // 
            this.logFileComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logFileComponent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.logFileComponent.Location = new System.Drawing.Point(17, 29);
            this.logFileComponent.LogFileContent = new string[0];
            this.logFileComponent.Name = "logFileComponent";
            this.logFileComponent.Size = new System.Drawing.Size(279, 638);
            this.logFileComponent.TabIndex = 1;
            // 
            // lblLogName
            // 
            this.lblLogName.AutoSize = true;
            this.lblLogName.Location = new System.Drawing.Point(14, 13);
            this.lblLogName.Name = "lblLogName";
            this.lblLogName.Size = new System.Drawing.Size(370, 13);
            this.lblLogName.TabIndex = 4;
            this.lblLogName.Text = "LOG: Double-click \'Original log files directory\' in Config tab to set logs\' folde" +
    "r...";
            // 
            // lblLogIndexPrompt
            // 
            this.lblLogIndexPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLogIndexPrompt.AutoSize = true;
            this.lblLogIndexPrompt.Location = new System.Drawing.Point(266, 634);
            this.lblLogIndexPrompt.Name = "lblLogIndexPrompt";
            this.lblLogIndexPrompt.Size = new System.Drawing.Size(152, 13);
            this.lblLogIndexPrompt.TabIndex = 23;
            this.lblLogIndexPrompt.Text = "Go directly to log file at index #";
            // 
            // lblLogScroll
            // 
            this.lblLogScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLogScroll.AutoSize = true;
            this.lblLogScroll.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogScroll.Location = new System.Drawing.Point(101, 603);
            this.lblLogScroll.Name = "lblLogScroll";
            this.lblLogScroll.Size = new System.Drawing.Size(358, 16);
            this.lblLogScroll.TabIndex = 17;
            this.lblLogScroll.Text = "Click on < or > buttons to scroll through log files...";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.picBoxScreenshot, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(20, 29);
            this.tableLayoutPanel2.MinimumSize = new System.Drawing.Size(450, 300);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(642, 564);
            this.tableLayoutPanel2.TabIndex = 16;
            // 
            // picBoxScreenshot
            // 
            this.picBoxScreenshot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBoxScreenshot.Image = ((System.Drawing.Image)(resources.GetObject("picBoxScreenshot.Image")));
            this.picBoxScreenshot.Location = new System.Drawing.Point(3, 3);
            this.picBoxScreenshot.Name = "picBoxScreenshot";
            this.picBoxScreenshot.Size = new System.Drawing.Size(636, 417);
            this.picBoxScreenshot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxScreenshot.TabIndex = 0;
            this.picBoxScreenshot.TabStop = false;
            this.picBoxScreenshot.DoubleClick += new System.EventHandler(this.picBoxScreenshot_DoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.LightGray;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.Controls.Add(this.picBoxHighRes1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.picBoxHighRes2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.picBoxHighRes3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnImageScrollLeft, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnImageScrollRight, 4, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 426);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(636, 135);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // picBoxHighRes1
            // 
            this.picBoxHighRes1.BackColor = System.Drawing.Color.Silver;
            this.picBoxHighRes1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBoxHighRes1.Location = new System.Drawing.Point(25, 3);
            this.picBoxHighRes1.Name = "picBoxHighRes1";
            this.picBoxHighRes1.Size = new System.Drawing.Size(191, 129);
            this.picBoxHighRes1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxHighRes1.TabIndex = 2;
            this.picBoxHighRes1.TabStop = false;
            this.picBoxHighRes1.DoubleClick += new System.EventHandler(this.picBoxHighRes1_DoubleClick);
            // 
            // picBoxHighRes2
            // 
            this.picBoxHighRes2.BackColor = System.Drawing.Color.Silver;
            this.picBoxHighRes2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBoxHighRes2.Location = new System.Drawing.Point(222, 3);
            this.picBoxHighRes2.Name = "picBoxHighRes2";
            this.picBoxHighRes2.Size = new System.Drawing.Size(191, 129);
            this.picBoxHighRes2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxHighRes2.TabIndex = 3;
            this.picBoxHighRes2.TabStop = false;
            this.picBoxHighRes2.DoubleClick += new System.EventHandler(this.picBoxHighRes2_DoubleClick);
            // 
            // picBoxHighRes3
            // 
            this.picBoxHighRes3.BackColor = System.Drawing.Color.Silver;
            this.picBoxHighRes3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBoxHighRes3.Location = new System.Drawing.Point(419, 3);
            this.picBoxHighRes3.Name = "picBoxHighRes3";
            this.picBoxHighRes3.Size = new System.Drawing.Size(191, 129);
            this.picBoxHighRes3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxHighRes3.TabIndex = 4;
            this.picBoxHighRes3.TabStop = false;
            this.picBoxHighRes3.DoubleClick += new System.EventHandler(this.picBoxHighRes3_DoubleClick);
            // 
            // lblScreenshotName
            // 
            this.lblScreenshotName.AutoSize = true;
            this.lblScreenshotName.Location = new System.Drawing.Point(19, 13);
            this.lblScreenshotName.Name = "lblScreenshotName";
            this.lblScreenshotName.Size = new System.Drawing.Size(482, 13);
            this.lblScreenshotName.TabIndex = 5;
            this.lblScreenshotName.Text = "SCREENSHOT: Double-click \'Original screenshots directory\' in Config tab to set sc" +
    "reenshots\' folder...";
            // 
            // lblScreenshotCount
            // 
            this.lblScreenshotCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblScreenshotCount.AutoSize = true;
            this.lblScreenshotCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScreenshotCount.Location = new System.Drawing.Point(178, 641);
            this.lblScreenshotCount.Name = "lblScreenshotCount";
            this.lblScreenshotCount.Size = new System.Drawing.Size(15, 13);
            this.lblScreenshotCount.TabIndex = 4;
            this.lblScreenshotCount.Text = "  ";
            // 
            // tabPreview
            // 
            this.tabPreview.Controls.Add(this.lblCategories);
            this.tabPreview.Controls.Add(this.lblTimeUnit);
            this.tabPreview.Controls.Add(this.dateTimePicker1);
            this.tabPreview.Controls.Add(this.btnView);
            this.tabPreview.Controls.Add(this.comboPeriod);
            this.tabPreview.Controls.Add(this.groupBox1);
            this.tabPreview.Controls.Add(this.dataGridView2);
            this.tabPreview.Controls.Add(this.TwentyFourHourGrid);
            this.tabPreview.Location = new System.Drawing.Point(4, 4);
            this.tabPreview.Name = "tabPreview";
            this.tabPreview.Size = new System.Drawing.Size(993, 676);
            this.tabPreview.TabIndex = 5;
            this.tabPreview.Text = "Preview";
            this.tabPreview.UseVisualStyleBackColor = true;
            // 
            // lblCategories
            // 
            this.lblCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCategories.AutoSize = true;
            this.lblCategories.Location = new System.Drawing.Point(818, 126);
            this.lblCategories.Name = "lblCategories";
            this.lblCategories.Size = new System.Drawing.Size(152, 13);
            this.lblCategories.TabIndex = 19;
            this.lblCategories.Text = "Check boxes with categories...";
            // 
            // lblTimeUnit
            // 
            this.lblTimeUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTimeUnit.AutoSize = true;
            this.lblTimeUnit.Location = new System.Drawing.Point(818, 65);
            this.lblTimeUnit.Name = "lblTimeUnit";
            this.lblTimeUnit.Size = new System.Drawing.Size(138, 13);
            this.lblTimeUnit.TabIndex = 18;
            this.lblTimeUnit.Text = "Select Unit of Time (Hour)...";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.Location = new System.Drawing.Point(818, 23);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(151, 20);
            this.dateTimePicker1.TabIndex = 16;
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.Enabled = false;
            this.btnView.Location = new System.Drawing.Point(894, 309);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 15;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // comboPeriod
            // 
            this.comboPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboPeriod.FormattingEnabled = true;
            this.comboPeriod.Items.AddRange(new object[] {
            "1",
            "0.5",
            "0.25"});
            this.comboPeriod.Location = new System.Drawing.Point(818, 82);
            this.comboPeriod.Name = "comboPeriod";
            this.comboPeriod.Size = new System.Drawing.Size(151, 21);
            this.comboPeriod.TabIndex = 14;
            this.comboPeriod.Text = "1";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkedListClasses);
            this.groupBox1.Location = new System.Drawing.Point(818, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(151, 160);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // checkedListClasses
            // 
            this.checkedListClasses.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListClasses.FormattingEnabled = true;
            this.checkedListClasses.Items.AddRange(new object[] {
            "  Aircraft",
            "  Interference",
            "  Meteor trail",
            "  Moon bounce",
            "  Head echo",
            "  Remove",
            "  Satellite",
            "  Query"});
            this.checkedListClasses.Location = new System.Drawing.Point(11, 15);
            this.checkedListClasses.Margin = new System.Windows.Forms.Padding(30);
            this.checkedListClasses.Name = "checkedListClasses";
            this.checkedListClasses.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkedListClasses.Size = new System.Drawing.Size(125, 135);
            this.checkedListClasses.TabIndex = 12;
            this.checkedListClasses.SelectedValueChanged += new System.EventHandler(this.checkedListClasses_SelectedValueChanged);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView2.Location = new System.Drawing.Point(89, 23);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView2.Size = new System.Drawing.Size(702, 507);
            this.dataGridView2.TabIndex = 17;
            // 
            // TwentyFourHourGrid
            // 
            this.TwentyFourHourGrid.AllowUserToAddRows = false;
            this.TwentyFourHourGrid.AllowUserToDeleteRows = false;
            this.TwentyFourHourGrid.AllowUserToResizeColumns = false;
            this.TwentyFourHourGrid.AllowUserToResizeRows = false;
            this.TwentyFourHourGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.TwentyFourHourGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column33});
            this.TwentyFourHourGrid.Location = new System.Drawing.Point(34, 23);
            this.TwentyFourHourGrid.Name = "TwentyFourHourGrid";
            this.TwentyFourHourGrid.ReadOnly = true;
            this.TwentyFourHourGrid.RowHeadersWidth = 54;
            this.TwentyFourHourGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.TwentyFourHourGrid.Size = new System.Drawing.Size(59, 507);
            this.TwentyFourHourGrid.TabIndex = 0;
            // 
            // Column33
            // 
            this.Column33.HeaderText = "";
            this.Column33.Name = "Column33";
            this.Column33.ReadOnly = true;
            // 
            // tabAnnual
            // 
            this.tabAnnual.Location = new System.Drawing.Point(4, 4);
            this.tabAnnual.Name = "tabAnnual";
            this.tabAnnual.Padding = new System.Windows.Forms.Padding(3);
            this.tabAnnual.Size = new System.Drawing.Size(993, 676);
            this.tabAnnual.TabIndex = 4;
            this.tabAnnual.Text = "Annual";
            this.tabAnnual.UseVisualStyleBackColor = true;
            // 
            // tabFilter
            // 
            this.tabFilter.Controls.Add(this.groupBox5);
            this.tabFilter.Controls.Add(this.lblCategoriesFilter);
            this.tabFilter.Controls.Add(this.btnCoalesce);
            this.tabFilter.Controls.Add(this.groupBox4);
            this.tabFilter.Controls.Add(this.lblCoalesceLogs);
            this.tabFilter.Controls.Add(this.dtpFilter);
            this.tabFilter.Controls.Add(this.logFileViewerFilter);
            this.tabFilter.Location = new System.Drawing.Point(4, 4);
            this.tabFilter.Name = "tabFilter";
            this.tabFilter.Padding = new System.Windows.Forms.Padding(3);
            this.tabFilter.Size = new System.Drawing.Size(993, 676);
            this.tabFilter.TabIndex = 6;
            this.tabFilter.Text = "Filter";
            this.tabFilter.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.checkedListClassesFilter);
            this.groupBox5.Location = new System.Drawing.Point(819, 204);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(151, 160);
            this.groupBox5.TabIndex = 33;
            this.groupBox5.TabStop = false;
            // 
            // checkedListClassesFilter
            // 
            this.checkedListClassesFilter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListClassesFilter.FormattingEnabled = true;
            this.checkedListClassesFilter.Items.AddRange(new object[] {
            "  Aircraft",
            "  Interference",
            "  Meteor trail",
            "  Moon bounce",
            "  Head echo",
            "  Remove",
            "  Satellite",
            "  Query"});
            this.checkedListClassesFilter.Location = new System.Drawing.Point(10, 15);
            this.checkedListClassesFilter.Margin = new System.Windows.Forms.Padding(30);
            this.checkedListClassesFilter.Name = "checkedListClassesFilter";
            this.checkedListClassesFilter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkedListClassesFilter.Size = new System.Drawing.Size(125, 135);
            this.checkedListClassesFilter.TabIndex = 12;
            // 
            // lblCategoriesFilter
            // 
            this.lblCategoriesFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCategoriesFilter.AutoSize = true;
            this.lblCategoriesFilter.Location = new System.Drawing.Point(822, 190);
            this.lblCategoriesFilter.Name = "lblCategoriesFilter";
            this.lblCategoriesFilter.Size = new System.Drawing.Size(91, 13);
            this.lblCategoriesFilter.TabIndex = 34;
            this.lblCategoriesFilter.Text = "With Categories...";
            // 
            // btnCoalesce
            // 
            this.btnCoalesce.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCoalesce.Location = new System.Drawing.Point(895, 374);
            this.btnCoalesce.Name = "btnCoalesce";
            this.btnCoalesce.Size = new System.Drawing.Size(75, 23);
            this.btnCoalesce.TabIndex = 32;
            this.btnCoalesce.Text = "Go";
            this.btnCoalesce.UseVisualStyleBackColor = true;
            this.btnCoalesce.Click += new System.EventHandler(this.btnCoalesce_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.radioCoalesceMonth);
            this.groupBox4.Controls.Add(this.radioCoalesceYear);
            this.groupBox4.Controls.Add(this.radioCoalesceAll);
            this.groupBox4.Location = new System.Drawing.Point(818, 78);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(151, 100);
            this.groupBox4.TabIndex = 31;
            this.groupBox4.TabStop = false;
            // 
            // radioCoalesceMonth
            // 
            this.radioCoalesceMonth.AutoSize = true;
            this.radioCoalesceMonth.Location = new System.Drawing.Point(11, 68);
            this.radioCoalesceMonth.Name = "radioCoalesceMonth";
            this.radioCoalesceMonth.Size = new System.Drawing.Size(100, 17);
            this.radioCoalesceMonth.TabIndex = 2;
            this.radioCoalesceMonth.Text = "Selected Month";
            this.radioCoalesceMonth.UseVisualStyleBackColor = true;
            // 
            // radioCoalesceYear
            // 
            this.radioCoalesceYear.AutoSize = true;
            this.radioCoalesceYear.Location = new System.Drawing.Point(11, 44);
            this.radioCoalesceYear.Name = "radioCoalesceYear";
            this.radioCoalesceYear.Size = new System.Drawing.Size(92, 17);
            this.radioCoalesceYear.TabIndex = 1;
            this.radioCoalesceYear.Text = "Selected Year";
            this.radioCoalesceYear.UseVisualStyleBackColor = true;
            // 
            // radioCoalesceAll
            // 
            this.radioCoalesceAll.AutoSize = true;
            this.radioCoalesceAll.Checked = true;
            this.radioCoalesceAll.Location = new System.Drawing.Point(11, 20);
            this.radioCoalesceAll.Name = "radioCoalesceAll";
            this.radioCoalesceAll.Size = new System.Drawing.Size(62, 17);
            this.radioCoalesceAll.TabIndex = 0;
            this.radioCoalesceAll.TabStop = true;
            this.radioCoalesceAll.Text = "All Time";
            this.radioCoalesceAll.UseVisualStyleBackColor = true;
            // 
            // lblCoalesceLogs
            // 
            this.lblCoalesceLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCoalesceLogs.AutoSize = true;
            this.lblCoalesceLogs.Location = new System.Drawing.Point(818, 65);
            this.lblCoalesceLogs.Name = "lblCoalesceLogs";
            this.lblCoalesceLogs.Size = new System.Drawing.Size(120, 13);
            this.lblCoalesceLogs.TabIndex = 2;
            this.lblCoalesceLogs.Text = "Coalesce Log Files for...";
            // 
            // dtpFilter
            // 
            this.dtpFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFilter.Location = new System.Drawing.Point(818, 23);
            this.dtpFilter.Name = "dtpFilter";
            this.dtpFilter.Size = new System.Drawing.Size(151, 20);
            this.dtpFilter.TabIndex = 1;
            // 
            // logFileViewerFilter
            // 
            this.logFileViewerFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logFileViewerFilter.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.logFileViewerFilter.Location = new System.Drawing.Point(17, 23);
            this.logFileViewerFilter.LogFileContent = new string[0];
            this.logFileViewerFilter.Name = "logFileViewerFilter";
            this.logFileViewerFilter.Size = new System.Drawing.Size(780, 644);
            this.logFileViewerFilter.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1020, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutScatterthonToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // aboutScatterthonToolStripMenuItem
            // 
            this.aboutScatterthonToolStripMenuItem.Name = "aboutScatterthonToolStripMenuItem";
            this.aboutScatterthonToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.aboutScatterthonToolStripMenuItem.Text = "About Scatterthon";
            this.aboutScatterthonToolStripMenuItem.Click += new System.EventHandler(this.aboutScatterthonToolStripMenuItem_Click);
            // 
            // Scatterthon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 708);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1036, 583);
            this.Name = "Scatterthon";
            this.Text = "Scatterthon";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Scatterthon_FormClosing);
            this.Load += new System.EventHandler(this.Scatterthon_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Scatterthon_KeyDown);
            this.tabRmob.ResumeLayout(false);
            this.tabRmob.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabConfig.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabCleanse.ResumeLayout(false);
            this.innerSplitContainer.Panel1.ResumeLayout(false);
            this.innerSplitContainer.Panel1.PerformLayout();
            this.innerSplitContainer.Panel2.ResumeLayout(false);
            this.innerSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.innerSplitContainer)).EndInit();
            this.innerSplitContainer.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxScreenshot)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxHighRes1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxHighRes2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxHighRes3)).EndInit();
            this.tabPreview.ResumeLayout(false);
            this.tabPreview.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TwentyFourHourGrid)).EndInit();
            this.tabFilter.ResumeLayout(false);
            this.tabFilter.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer innerSplitContainer;
        private System.Windows.Forms.SaveFileDialog dlgSaveLogFile;
        private System.Windows.Forms.PictureBox picBoxScreenshot;
        private System.Windows.Forms.FolderBrowserDialog dlgOpenDirectory;
        private System.Windows.Forms.FolderBrowserDialog dlgSaveDirectory;
        private System.Windows.Forms.Label lblLogName;
        private System.Windows.Forms.Label lblScreenshotName;
        private System.Windows.Forms.Label lblScreenshotCount;
        private System.Windows.Forms.ToolTip toolTipSync;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabPage tabRmob;
        private System.Windows.Forms.Button btnImportColorgram;
        private System.Windows.Forms.TabPage tabConfig;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox txtOriginalScreenshotsDirectory;
        public System.Windows.Forms.TextBox txtOriginalLogsDirectory;
        private System.Windows.Forms.Label lblScreenshotsPath;
        private System.Windows.Forms.Label lblLogFilePath;
        private System.Windows.Forms.Button btnApplySettings;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column22;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column23;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column24;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column25;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column26;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column27;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column28;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column29;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column30;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column31;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column32;
        private System.Windows.Forms.DataGridViewTextBoxColumn Colours;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scale;
        public System.Windows.Forms.TextBox txtAnnualTopMeteorCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label lblPreviewedMonth;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnUpdateTopMeteorCount;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabCleanse;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutScatterthonToolStripMenuItem;
        private LogFileViewer logFileComponent;
        private System.Windows.Forms.TabPage tabAnnual;
        private System.Windows.Forms.TabPage tabPreview;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.CheckedListBox checkedListClasses;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.ComboBox comboPeriod;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridView TwentyFourHourGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column33;
        private System.Windows.Forms.ComboBox cmbStationNames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkDefaultStation;
        private System.Windows.Forms.Button btnNewStation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox picBoxHighRes1;
        private System.Windows.Forms.PictureBox picBoxHighRes2;
        private System.Windows.Forms.PictureBox picBoxHighRes3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtContinuousCaptureSpan;
        private System.Windows.Forms.Label lblContinuousCaptureSpan;
        private System.Windows.Forms.TextBox txtHighResolutionScreenshots;
        private System.Windows.Forms.Label lblHighResolutionScreenshotsPath;
        private System.Windows.Forms.Button btnPrevLog;
        private System.Windows.Forms.Button btnNextLog;
        private System.Windows.Forms.Button btnCopyScreenshot;
        private System.Windows.Forms.Label lblLogScroll;
        private System.Windows.Forms.Label lblCategories;
        private System.Windows.Forms.Label lblTimeUnit;
        private System.Windows.Forms.CheckBox checkShowFreq;
        private System.Windows.Forms.CheckBox checkShowNoise;
        private System.Windows.Forms.CheckBox checkShowSignal;
        private System.Windows.Forms.CheckBox checkPagination;
        private System.Windows.Forms.Button btnImageScrollLeft;
        private System.Windows.Forms.Button btnImageScrollRight;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.TextBox txtScreenshotsDelay;
        private System.Windows.Forms.Label lblScreenshotsDelay;
        private System.Windows.Forms.TextBox txtLogIndex;
        private System.Windows.Forms.Button btnSaveProgress;
        private System.Windows.Forms.CheckBox checkDropHistory;
        private System.Windows.Forms.CheckBox checkGenerateRMOB;
        private System.Windows.Forms.DateTimePicker dtpRMOB;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtWhatIf;
        private System.Windows.Forms.RadioButton radioColourByRandom;
        private System.Windows.Forms.RadioButton radioColourByMonth;
        private System.Windows.Forms.RadioButton radioColourByYear;
        private System.Windows.Forms.Label lblNormalise;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioRmobMonth;
        private System.Windows.Forms.RadioButton radioRmobYear;
        private System.Windows.Forms.RadioButton radioRmobAll;
        private System.Windows.Forms.Label lblRecreateRmob;
        private System.Windows.Forms.Button btnRecreateRmob;
        private System.Windows.Forms.Button btnNormalise;
        private System.Windows.Forms.Label lblLogIndexPrompt;
        private System.Windows.Forms.TabPage tabFilter;
        private System.Windows.Forms.Button btnCoalesce;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioCoalesceMonth;
        private System.Windows.Forms.RadioButton radioCoalesceYear;
        private System.Windows.Forms.RadioButton radioCoalesceAll;
        private System.Windows.Forms.Label lblCoalesceLogs;
        private System.Windows.Forms.DateTimePicker dtpFilter;
        private LogFileViewer logFileViewerFilter;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckedListBox checkedListClassesFilter;
        private System.Windows.Forms.Label lblCategoriesFilter;
        private System.Windows.Forms.Label lblConfigNotSaved;
    }
}

