using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EAPerformanceApplication.AddedClasses.Optimization;

namespace EAPerformanceApplication
{
    public partial class OptimizationParameterSettingsForm : Form
    {
        private PiecewiseLinearSpeedProfileOptimizationSettings optimizationSettings;

        public OptimizationParameterSettingsForm()
        {
            InitializeComponent();
        }

        public void SetOptimizationSettings(PiecewiseLinearSpeedProfileOptimizationSettings optimizationSettings)
        {
            this.optimizationSettings = optimizationSettings;
            optimizationSettingsPropertyPanel.SetObject(this.optimizationSettings);
        }
    }
}
