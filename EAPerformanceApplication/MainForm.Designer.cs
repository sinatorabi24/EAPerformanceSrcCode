namespace EAPerformanceApplication
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.singleRunOptimizationTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.singleRunOptimizationEvaluatedIndListBox = new System.Windows.Forms.ListBox();
            this.singleRunOptimizationProgressListBox = new System.Windows.Forms.ListBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.selectedSpeedProfileStatusLabel = new System.Windows.Forms.ToolStripLabel();
            this.evaluateSpeedProfileButton = new System.Windows.Forms.ToolStripButton();
            this.fuelConsumptionLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.optimizationMethodComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.setOptimizationParameterButton = new System.Windows.Forms.ToolStripButton();
            this.initializeButton = new System.Windows.Forms.ToolStripButton();
            this.startOptimizationButton = new System.Windows.Forms.ToolStripButton();
            this.stopOptimizationButton = new System.Windows.Forms.ToolStripButton();
            this.batchRunOptimizationTabPage = new System.Windows.Forms.TabPage();
            this.batchRunProgressListBox = new System.Windows.Forms.ListBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.batchRunOptimizationMethodLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.numberOfBatchRunsTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.startBatchRunButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.speedProfilePlot2DPanel = new PlotLibrary.Plot2DPanel();
            this.roadPlot2DPanel = new PlotLibrary.Plot2DPanel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.logDataButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.singleRunOptimizationTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.batchRunOptimizationTabPage.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1370, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roadToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // roadToolStripMenuItem
            // 
            this.roadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem});
            this.roadToolStripMenuItem.Name = "roadToolStripMenuItem";
            this.roadToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.roadToolStripMenuItem.Text = "Road";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1370, 485);
            this.splitContainer1.SplitterDistance = 625;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.singleRunOptimizationTabPage);
            this.tabControl1.Controls.Add(this.batchRunOptimizationTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(625, 485);
            this.tabControl1.TabIndex = 0;
            // 
            // singleRunOptimizationTabPage
            // 
            this.singleRunOptimizationTabPage.Controls.Add(this.splitContainer3);
            this.singleRunOptimizationTabPage.Controls.Add(this.toolStrip2);
            this.singleRunOptimizationTabPage.Controls.Add(this.toolStrip1);
            this.singleRunOptimizationTabPage.Location = new System.Drawing.Point(4, 22);
            this.singleRunOptimizationTabPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.singleRunOptimizationTabPage.Name = "singleRunOptimizationTabPage";
            this.singleRunOptimizationTabPage.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.singleRunOptimizationTabPage.Size = new System.Drawing.Size(617, 459);
            this.singleRunOptimizationTabPage.TabIndex = 0;
            this.singleRunOptimizationTabPage.Text = "Single run";
            this.singleRunOptimizationTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(2, 52);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.singleRunOptimizationEvaluatedIndListBox);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.singleRunOptimizationProgressListBox);
            this.splitContainer3.Size = new System.Drawing.Size(613, 405);
            this.splitContainer3.SplitterDistance = 145;
            this.splitContainer3.SplitterWidth = 3;
            this.splitContainer3.TabIndex = 2;
            // 
            // singleRunOptimizationEvaluatedIndListBox
            // 
            this.singleRunOptimizationEvaluatedIndListBox.BackColor = System.Drawing.SystemColors.InfoText;
            this.singleRunOptimizationEvaluatedIndListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.singleRunOptimizationEvaluatedIndListBox.ForeColor = System.Drawing.Color.Lime;
            this.singleRunOptimizationEvaluatedIndListBox.FormattingEnabled = true;
            this.singleRunOptimizationEvaluatedIndListBox.Location = new System.Drawing.Point(0, 0);
            this.singleRunOptimizationEvaluatedIndListBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.singleRunOptimizationEvaluatedIndListBox.Name = "singleRunOptimizationEvaluatedIndListBox";
            this.singleRunOptimizationEvaluatedIndListBox.Size = new System.Drawing.Size(145, 405);
            this.singleRunOptimizationEvaluatedIndListBox.TabIndex = 0;
            // 
            // singleRunOptimizationProgressListBox
            // 
            this.singleRunOptimizationProgressListBox.BackColor = System.Drawing.SystemColors.InfoText;
            this.singleRunOptimizationProgressListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.singleRunOptimizationProgressListBox.ForeColor = System.Drawing.Color.Lime;
            this.singleRunOptimizationProgressListBox.FormattingEnabled = true;
            this.singleRunOptimizationProgressListBox.Location = new System.Drawing.Point(0, 0);
            this.singleRunOptimizationProgressListBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.singleRunOptimizationProgressListBox.Name = "singleRunOptimizationProgressListBox";
            this.singleRunOptimizationProgressListBox.Size = new System.Drawing.Size(465, 405);
            this.singleRunOptimizationProgressListBox.TabIndex = 0;
            this.singleRunOptimizationProgressListBox.SelectedIndexChanged += new System.EventHandler(this.singleRunOptimizationProgressListBox_SelectedIndexChanged);
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectedSpeedProfileStatusLabel,
            this.evaluateSpeedProfileButton,
            this.fuelConsumptionLabel});
            this.toolStrip2.Location = new System.Drawing.Point(2, 27);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(613, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // selectedSpeedProfileStatusLabel
            // 
            this.selectedSpeedProfileStatusLabel.Enabled = false;
            this.selectedSpeedProfileStatusLabel.Name = "selectedSpeedProfileStatusLabel";
            this.selectedSpeedProfileStatusLabel.Size = new System.Drawing.Size(154, 22);
            this.selectedSpeedProfileStatusLabel.Text = "No speed profile is selected.";
            // 
            // evaluateSpeedProfileButton
            // 
            this.evaluateSpeedProfileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.evaluateSpeedProfileButton.Enabled = false;
            this.evaluateSpeedProfileButton.Image = ((System.Drawing.Image)(resources.GetObject("evaluateSpeedProfileButton.Image")));
            this.evaluateSpeedProfileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.evaluateSpeedProfileButton.Name = "evaluateSpeedProfileButton";
            this.evaluateSpeedProfileButton.Size = new System.Drawing.Size(55, 22);
            this.evaluateSpeedProfileButton.Text = "Evaluate";
            this.evaluateSpeedProfileButton.Click += new System.EventHandler(this.evaluateSpeedProfileButton_Click);
            // 
            // fuelConsumptionLabel
            // 
            this.fuelConsumptionLabel.Name = "fuelConsumptionLabel";
            this.fuelConsumptionLabel.Size = new System.Drawing.Size(131, 22);
            this.fuelConsumptionLabel.Text = "Fuel consumption: N/A";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.optimizationMethodComboBox,
            this.setOptimizationParameterButton,
            this.logDataButton,
            this.initializeButton,
            this.startOptimizationButton,
            this.stopOptimizationButton});
            this.toolStrip1.Location = new System.Drawing.Point(2, 2);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(613, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(127, 22);
            this.toolStripLabel1.Text = "Optimization method: ";
            // 
            // optimizationMethodComboBox
            // 
            this.optimizationMethodComboBox.Items.AddRange(new object[] {
            "GA",
            "RMHC"});
            this.optimizationMethodComboBox.Name = "optimizationMethodComboBox";
            this.optimizationMethodComboBox.Size = new System.Drawing.Size(75, 25);
            // 
            // setOptimizationParameterButton
            // 
            this.setOptimizationParameterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.setOptimizationParameterButton.Image = ((System.Drawing.Image)(resources.GetObject("setOptimizationParameterButton.Image")));
            this.setOptimizationParameterButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.setOptimizationParameterButton.Name = "setOptimizationParameterButton";
            this.setOptimizationParameterButton.Size = new System.Drawing.Size(154, 22);
            this.setOptimizationParameterButton.Text = "Set optimization parameter";
            this.setOptimizationParameterButton.Click += new System.EventHandler(this.setOptimizationParameterButton_Click);
            // 
            // initializeButton
            // 
            this.initializeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.initializeButton.Image = ((System.Drawing.Image)(resources.GetObject("initializeButton.Image")));
            this.initializeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.initializeButton.Name = "initializeButton";
            this.initializeButton.Size = new System.Drawing.Size(54, 22);
            this.initializeButton.Text = "Initialize";
            this.initializeButton.Click += new System.EventHandler(this.initializeButton_Click);
            // 
            // startOptimizationButton
            // 
            this.startOptimizationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.startOptimizationButton.Enabled = false;
            this.startOptimizationButton.Image = ((System.Drawing.Image)(resources.GetObject("startOptimizationButton.Image")));
            this.startOptimizationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startOptimizationButton.Name = "startOptimizationButton";
            this.startOptimizationButton.Size = new System.Drawing.Size(35, 22);
            this.startOptimizationButton.Text = "Start";
            this.startOptimizationButton.Click += new System.EventHandler(this.startOptimizationButton_Click);
            // 
            // stopOptimizationButton
            // 
            this.stopOptimizationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stopOptimizationButton.Enabled = false;
            this.stopOptimizationButton.Image = ((System.Drawing.Image)(resources.GetObject("stopOptimizationButton.Image")));
            this.stopOptimizationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopOptimizationButton.Name = "stopOptimizationButton";
            this.stopOptimizationButton.Size = new System.Drawing.Size(35, 22);
            this.stopOptimizationButton.Text = "Stop";
            this.stopOptimizationButton.Click += new System.EventHandler(this.stopOptimizationButton_Click);
            // 
            // batchRunOptimizationTabPage
            // 
            this.batchRunOptimizationTabPage.Controls.Add(this.batchRunProgressListBox);
            this.batchRunOptimizationTabPage.Controls.Add(this.toolStrip3);
            this.batchRunOptimizationTabPage.Location = new System.Drawing.Point(4, 22);
            this.batchRunOptimizationTabPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.batchRunOptimizationTabPage.Name = "batchRunOptimizationTabPage";
            this.batchRunOptimizationTabPage.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.batchRunOptimizationTabPage.Size = new System.Drawing.Size(618, 460);
            this.batchRunOptimizationTabPage.TabIndex = 1;
            this.batchRunOptimizationTabPage.Text = "Batch run";
            this.batchRunOptimizationTabPage.UseVisualStyleBackColor = true;
            // 
            // batchRunProgressListBox
            // 
            this.batchRunProgressListBox.BackColor = System.Drawing.SystemColors.InfoText;
            this.batchRunProgressListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.batchRunProgressListBox.ForeColor = System.Drawing.Color.Lime;
            this.batchRunProgressListBox.FormattingEnabled = true;
            this.batchRunProgressListBox.Location = new System.Drawing.Point(2, 27);
            this.batchRunProgressListBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.batchRunProgressListBox.Name = "batchRunProgressListBox";
            this.batchRunProgressListBox.Size = new System.Drawing.Size(614, 431);
            this.batchRunProgressListBox.TabIndex = 1;
            // 
            // toolStrip3
            // 
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.batchRunOptimizationMethodLabel,
            this.toolStripLabel2,
            this.numberOfBatchRunsTextBox,
            this.startBatchRunButton});
            this.toolStrip3.Location = new System.Drawing.Point(2, 2);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(614, 25);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // batchRunOptimizationMethodLabel
            // 
            this.batchRunOptimizationMethodLabel.Name = "batchRunOptimizationMethodLabel";
            this.batchRunOptimizationMethodLabel.Size = new System.Drawing.Size(149, 22);
            this.batchRunOptimizationMethodLabel.Text = "Optimization method: N/A";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(97, 22);
            this.toolStripLabel2.Text = "Number of runs: ";
            // 
            // numberOfBatchRunsTextBox
            // 
            this.numberOfBatchRunsTextBox.Name = "numberOfBatchRunsTextBox";
            this.numberOfBatchRunsTextBox.Size = new System.Drawing.Size(38, 25);
            this.numberOfBatchRunsTextBox.Text = "100";
            // 
            // startBatchRunButton
            // 
            this.startBatchRunButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.startBatchRunButton.Image = ((System.Drawing.Image)(resources.GetObject("startBatchRunButton.Image")));
            this.startBatchRunButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startBatchRunButton.Name = "startBatchRunButton";
            this.startBatchRunButton.Size = new System.Drawing.Size(89, 22);
            this.startBatchRunButton.Text = "Start batch run";
            this.startBatchRunButton.Click += new System.EventHandler(this.startBatchRunButton_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.speedProfilePlot2DPanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.roadPlot2DPanel);
            this.splitContainer2.Size = new System.Drawing.Size(742, 485);
            this.splitContainer2.SplitterDistance = 209;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 0;
            // 
            // speedProfilePlot2DPanel
            // 
            this.speedProfilePlot2DPanel.AxisColor = System.Drawing.Color.Black;
            this.speedProfilePlot2DPanel.AxisMarkingsColor = System.Drawing.Color.Black;
            this.speedProfilePlot2DPanel.AxisThickness = 1F;
            this.speedProfilePlot2DPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.speedProfilePlot2DPanel.BottomFrameHeight = 45F;
            this.speedProfilePlot2DPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.speedProfilePlot2DPanel.Font = new System.Drawing.Font("Times New Roman", 7.8F);
            this.speedProfilePlot2DPanel.FrameBackColor = System.Drawing.Color.White;
            this.speedProfilePlot2DPanel.GridColor = System.Drawing.Color.DarkGray;
            this.speedProfilePlot2DPanel.GridLineThickness = 1F;
            this.speedProfilePlot2DPanel.GridVisible = false;
            this.speedProfilePlot2DPanel.HorizontalAxisLabel = "";
            this.speedProfilePlot2DPanel.HorizontalAxisLabelFontSize = 7.8F;
            this.speedProfilePlot2DPanel.HorizontalAxisLabelVisible = true;
            this.speedProfilePlot2DPanel.HorizontalAxisMarkingsFormatString = "0.000";
            this.speedProfilePlot2DPanel.HorizontalAxisMarkingsVisible = true;
            this.speedProfilePlot2DPanel.HorizontalAxisVisible = false;
            this.speedProfilePlot2DPanel.LeftFrameWidth = 70F;
            this.speedProfilePlot2DPanel.Location = new System.Drawing.Point(0, 0);
            this.speedProfilePlot2DPanel.MajorHorizontalTickMarkSpacing = 0D;
            this.speedProfilePlot2DPanel.MajorHorizontalTickMarksVisible = true;
            this.speedProfilePlot2DPanel.MajorVerticalTickMarkSpacing = 0D;
            this.speedProfilePlot2DPanel.MajorVerticalTickMarksVisible = true;
            this.speedProfilePlot2DPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.speedProfilePlot2DPanel.Name = "speedProfilePlot2DPanel";
            this.speedProfilePlot2DPanel.PlotBackColor = System.Drawing.Color.White;
            this.speedProfilePlot2DPanel.RightFrameWidth = 45F;
            this.speedProfilePlot2DPanel.Size = new System.Drawing.Size(742, 209);
            this.speedProfilePlot2DPanel.TabIndex = 0;
            this.speedProfilePlot2DPanel.TickMarkColor = System.Drawing.Color.Black;
            this.speedProfilePlot2DPanel.TopFrameHeight = 45F;
            this.speedProfilePlot2DPanel.VerticalAxisLabel = "";
            this.speedProfilePlot2DPanel.VerticalAxisLabelFontSize = 7.8F;
            this.speedProfilePlot2DPanel.VerticalAxisLabelVisible = true;
            this.speedProfilePlot2DPanel.VerticalAxisMarkingsFormatString = "0.000";
            this.speedProfilePlot2DPanel.VerticalAxisMarkingsVisible = true;
            this.speedProfilePlot2DPanel.VerticalAxisVisible = false;
            // 
            // roadPlot2DPanel
            // 
            this.roadPlot2DPanel.AxisColor = System.Drawing.Color.Black;
            this.roadPlot2DPanel.AxisMarkingsColor = System.Drawing.Color.Black;
            this.roadPlot2DPanel.AxisThickness = 1F;
            this.roadPlot2DPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.roadPlot2DPanel.BottomFrameHeight = 45F;
            this.roadPlot2DPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roadPlot2DPanel.Font = new System.Drawing.Font("Times New Roman", 7.8F);
            this.roadPlot2DPanel.FrameBackColor = System.Drawing.Color.White;
            this.roadPlot2DPanel.GridColor = System.Drawing.Color.DarkGray;
            this.roadPlot2DPanel.GridLineThickness = 1F;
            this.roadPlot2DPanel.GridVisible = false;
            this.roadPlot2DPanel.HorizontalAxisLabel = "";
            this.roadPlot2DPanel.HorizontalAxisLabelFontSize = 7.8F;
            this.roadPlot2DPanel.HorizontalAxisLabelVisible = true;
            this.roadPlot2DPanel.HorizontalAxisMarkingsFormatString = "0.000";
            this.roadPlot2DPanel.HorizontalAxisMarkingsVisible = true;
            this.roadPlot2DPanel.HorizontalAxisVisible = false;
            this.roadPlot2DPanel.LeftFrameWidth = 70F;
            this.roadPlot2DPanel.Location = new System.Drawing.Point(0, 0);
            this.roadPlot2DPanel.MajorHorizontalTickMarkSpacing = 0D;
            this.roadPlot2DPanel.MajorHorizontalTickMarksVisible = true;
            this.roadPlot2DPanel.MajorVerticalTickMarkSpacing = 0D;
            this.roadPlot2DPanel.MajorVerticalTickMarksVisible = true;
            this.roadPlot2DPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.roadPlot2DPanel.Name = "roadPlot2DPanel";
            this.roadPlot2DPanel.PlotBackColor = System.Drawing.Color.White;
            this.roadPlot2DPanel.RightFrameWidth = 45F;
            this.roadPlot2DPanel.Size = new System.Drawing.Size(742, 273);
            this.roadPlot2DPanel.TabIndex = 0;
            this.roadPlot2DPanel.TickMarkColor = System.Drawing.Color.Black;
            this.roadPlot2DPanel.TopFrameHeight = 45F;
            this.roadPlot2DPanel.VerticalAxisLabel = "";
            this.roadPlot2DPanel.VerticalAxisLabelFontSize = 7.8F;
            this.roadPlot2DPanel.VerticalAxisLabelVisible = true;
            this.roadPlot2DPanel.VerticalAxisMarkingsFormatString = "0.000";
            this.roadPlot2DPanel.VerticalAxisMarkingsVisible = true;
            this.roadPlot2DPanel.VerticalAxisVisible = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // logDataButton
            // 
            this.logDataButton.CheckOnClick = true;
            this.logDataButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.logDataButton.Image = ((System.Drawing.Image)(resources.GetObject("logDataButton.Image")));
            this.logDataButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.logDataButton.Name = "logDataButton";
            this.logDataButton.Size = new System.Drawing.Size(57, 22);
            this.logDataButton.Text = "Log data";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 509);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "Main form";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.singleRunOptimizationTabPage.ResumeLayout(false);
            this.singleRunOptimizationTabPage.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.batchRunOptimizationTabPage.ResumeLayout(false);
            this.batchRunOptimizationTabPage.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem roadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private PlotLibrary.Plot2DPanel roadPlot2DPanel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton initializeButton;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private PlotLibrary.Plot2DPanel speedProfilePlot2DPanel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage singleRunOptimizationTabPage;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListBox singleRunOptimizationEvaluatedIndListBox;
        private System.Windows.Forms.ListBox singleRunOptimizationProgressListBox;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.TabPage batchRunOptimizationTabPage;
        private System.Windows.Forms.ListBox batchRunProgressListBox;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton setOptimizationParameterButton;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox optimizationMethodComboBox;
        private System.Windows.Forms.ToolStripLabel batchRunOptimizationMethodLabel;
        private System.Windows.Forms.ToolStripButton startOptimizationButton;
        private System.Windows.Forms.ToolStripButton stopOptimizationButton;
        private System.Windows.Forms.ToolStripLabel selectedSpeedProfileStatusLabel;
        private System.Windows.Forms.ToolStripButton evaluateSpeedProfileButton;
        private System.Windows.Forms.ToolStripLabel fuelConsumptionLabel;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox numberOfBatchRunsTextBox;
        private System.Windows.Forms.ToolStripButton startBatchRunButton;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton logDataButton;
    }
}

