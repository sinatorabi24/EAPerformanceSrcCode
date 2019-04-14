namespace EAPerformanceApplication
{
    partial class OptimizationParameterSettingsForm
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
            this.optimizationSettingsPropertyPanel = new CustomUserControlsLibrary.PropertyPanels.PropertyPanel();
            this.SuspendLayout();
            // 
            // optimizationSettingsPropertyPanel
            // 
            this.optimizationSettingsPropertyPanel.BackColor = System.Drawing.Color.DimGray;
            this.optimizationSettingsPropertyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optimizationSettingsPropertyPanel.Location = new System.Drawing.Point(0, 0);
            this.optimizationSettingsPropertyPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optimizationSettingsPropertyPanel.Name = "optimizationSettingsPropertyPanel";
            this.optimizationSettingsPropertyPanel.Size = new System.Drawing.Size(685, 710);
            this.optimizationSettingsPropertyPanel.TabIndex = 0;
            // 
            // OptimizationParameterSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 710);
            this.Controls.Add(this.optimizationSettingsPropertyPanel);
            this.Name = "OptimizationParameterSettingsForm";
            this.Text = "OptimizationParameterSettingsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomUserControlsLibrary.PropertyPanels.PropertyPanel optimizationSettingsPropertyPanel;
    }
}