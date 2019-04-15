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
            this.batchRunOptimizationTabPage = new System.Windows.Forms.TabPage();
            this.batchRunProgressListBox = new System.Windows.Forms.ListBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.numberOfBatchRunsTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.startBatchRunButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.speedProfilePlot2DPanel = new PlotLibrary.Plot2DPanel();
            this.roadPlot2DPanel = new PlotLibrary.Plot2DPanel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.setOptimizationParameterButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
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
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
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
            this.tabControl1.Controls.Add(this.batchRunOptimizationTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(625, 485);
            this.tabControl1.TabIndex = 0;
            // 
            // batchRunOptimizationTabPage
            // 
            this.batchRunOptimizationTabPage.Controls.Add(this.batchRunProgressListBox);
            this.batchRunOptimizationTabPage.Controls.Add(this.toolStrip3);
            this.batchRunOptimizationTabPage.Location = new System.Drawing.Point(4, 22);
            this.batchRunOptimizationTabPage.Margin = new System.Windows.Forms.Padding(2);
            this.batchRunOptimizationTabPage.Name = "batchRunOptimizationTabPage";
            this.batchRunOptimizationTabPage.Padding = new System.Windows.Forms.Padding(2);
            this.batchRunOptimizationTabPage.Size = new System.Drawing.Size(617, 459);
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
            this.batchRunProgressListBox.Margin = new System.Windows.Forms.Padding(2);
            this.batchRunProgressListBox.Name = "batchRunProgressListBox";
            this.batchRunProgressListBox.Size = new System.Drawing.Size(613, 430);
            this.batchRunProgressListBox.TabIndex = 1;
            // 
            // toolStrip3
            // 
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setOptimizationParameterButton,
            this.toolStripLabel2,
            this.numberOfBatchRunsTextBox,
            this.startBatchRunButton});
            this.toolStrip3.Location = new System.Drawing.Point(2, 2);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(613, 25);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
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
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(2);
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
            this.speedProfilePlot2DPanel.Margin = new System.Windows.Forms.Padding(2);
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
            this.roadPlot2DPanel.Margin = new System.Windows.Forms.Padding(2);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 509);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Main form";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
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
        private System.Windows.Forms.SplitContainer splitContainer2;
        private PlotLibrary.Plot2DPanel speedProfilePlot2DPanel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage batchRunOptimizationTabPage;
        private System.Windows.Forms.ListBox batchRunProgressListBox;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox numberOfBatchRunsTextBox;
        private System.Windows.Forms.ToolStripButton startBatchRunButton;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton setOptimizationParameterButton;
    }
}

