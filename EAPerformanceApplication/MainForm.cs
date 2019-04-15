using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutonomousVehicleLibrary;
using AutonomousVehicleLibrary.Maps;
using AutonomousVehicleLibrary.Navigation;
using EAPerformanceApplication.AddedClasses;
using EAPerformanceApplication.AddedClasses.Optimization;
using EAPerformanceApplication.AddedClasses.Optimization.EventArgsClasses;
using MathematicsLibrary;
using MathematicsLibrary.Interpolation;
using ObjectSerializerLibrary;
using PlotLibrary;
using StochasticOptimizationLibrary;

namespace EAPerformanceApplication
{
    public partial class MainForm : Form
    {
        #region Constants
        // Constants used when generating trajectories (splines) from the map.
        private const double INFORMATION_COMPRESSION = 8; // <=> effectively no compression.
        private const double METRIC_STEP = 0.1;
        #endregion

        private string defaultPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "..\\..\\..\\Data");
        private string batchPath;
        private MetricMap metricMap;
        private MetricPath metricPath;
        private PiecewiseLinearSpeedProfileOptimizationSettings optimizationSettings;
        private PiecewiseLinearSpeedProfileOptimization speedProfileOptimizer;
        private OptimizableStructure speedProfileToBeEvaluated;
        private List<OptimizableStructure> bestIndividualsList = null;
        private string currentBestSpeedProfileInfo = "";
        private string generationEvaluatedInfor = "";
        private string batchRunGenerationEvaluatedInfo = "";
        private string batchRunIndividualEvaluatedInfo = "";
        private string batchRunInfo = "";
        private Thread batchRunThread;
        private string roadFileName;

        public MainForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            defaultPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "..\\..\\..\\Data");
            batchPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "..\\..\\..\\Results"); 
            optimizationSettings = new PiecewiseLinearSpeedProfileOptimizationSettings();
            optimizationSettings.SetDefault();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            openFileDialog.InitialDirectory = Path.GetFullPath(defaultPath + "\\Roads");

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                roadFileName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                metricMap = (MetricMap)ObjectXmlSerializer.ObtainSerializedObject(openFileDialog.FileName, typeof(MetricMap));
                metricMap.GenerateTrajectories(INFORMATION_COMPRESSION, METRIC_STEP);
                GenerateMetricPath();
            }
            //batchPath += "\\BatchRunResult" + "_" + roadFileName + ".txt";
        }

        private void PlotSpeedProfile(Plot2DPanel speedProfilePlot2DPanel, PiecewiseLinearSpeedProfile speedProfile, string selectedItem)
        {
            if(selectedItem == "Batch run") {  }
            else if (speedProfilePlot2DPanel.HorizontalAxisLabel != "") { speedProfilePlot2DPanel.RemoveSeries(selectedItem); }

            Color lineColor = Color.Navy;
            Color pointColor = Color.Blue;

            if (selectedItem == "Best individual") { lineColor = Color.DarkRed; pointColor = Color.DarkRed; }
            else if (selectedItem == "Selected individual") { lineColor = Color.DarkGreen; pointColor = Color.DarkGreen; }

            List<double> horizentalAxisData = new List<double>();
            List<double> verticalAxisData = new List<double>();

            double xMin = 0;
            double xMax = speedProfile.ConnectionPointsList.Last()[1];

            double entryDistance = 0;
            double exitDistance = 0;
            // It is assumed that there are only one SegmentSpeedProfile
            for (int i = 0; i < speedProfile.ConnectionPointsList.Count; i++)
            {
                exitDistance = speedProfile.ConnectionPointsList[i][1];
                for (double u = 0; u <= 1; u += 0.1)
                {
                    double x = entryDistance + u * (exitDistance - entryDistance);
                    double speed = speedProfile.SegmentLinearSpeedProfileList[0].LinearBezierSplineList[i].GetPoint(u).CoordinateList[0];

                    horizentalAxisData.Add(x);
                    verticalAxisData.Add(speed);
                }
                entryDistance = exitDistance;
            }

            DataSeries speedProfileDataSeries = new DataSeries();
            speedProfileDataSeries.Generate(selectedItem, horizentalAxisData, verticalAxisData);
            speedProfileDataSeries.SetLineColor(lineColor);
            speedProfileDataSeries.SetPointColor(pointColor);
            speedProfileDataSeries.ConnectRange(xMin, xMax);
            speedProfileDataSeries.SetPointVisibilityState(false);
            speedProfileDataSeries.SetLineWidth(0.0035);

            double horizentalDataRange = horizentalAxisData.Max() - horizentalAxisData.Min();
            double verticalDataRange = optimizationSettings.MaximumSpeedValue - optimizationSettings.MinimumSpeedValue;

            speedProfilePlot2DPanel.MajorHorizontalTickMarkSpacing = horizentalDataRange / 10;
            speedProfilePlot2DPanel.MajorVerticalTickMarkSpacing = verticalDataRange / 4;
            speedProfilePlot2DPanel.AxisColor = Color.Black;
            speedProfilePlot2DPanel.VerticalAxisVisible = true;
            speedProfilePlot2DPanel.HorizontalAxisVisible = true;
            speedProfilePlot2DPanel.HorizontalAxisMarkingsVisible = true;
            speedProfilePlot2DPanel.GridVisible = true;
            speedProfilePlot2DPanel.SetHorizontalPlotRange(horizentalAxisData.Min(), horizentalAxisData.Max());
            speedProfilePlot2DPanel.SetVerticalPlotRange(optimizationSettings.MinimumSpeedValue, optimizationSettings.MaximumSpeedValue);
            // Hardcoded range for the speed profile, need to be fixed.
            speedProfilePlot2DPanel.HorizontalAxisLabel = "X";
            speedProfilePlot2DPanel.HorizontalAxisLabelFontSize = 14;
            speedProfilePlot2DPanel.VerticalAxisLabel = "Speed";
            speedProfilePlot2DPanel.VerticalAxisLabelFontSize = 14;
            speedProfilePlot2DPanel.HorizontalAxisMarkingsFormatString = "0";
            speedProfilePlot2DPanel.VerticalAxisMarkingsFormatString = "0.0";
            speedProfilePlot2DPanel.AddDataSeries(speedProfileDataSeries);
            //speedProfilePlot2DPanel.AddDataSeries(vehicleSpeedDataSeries);
        }

        private void PlotRoadProfile(Plot2DPanel roadProfilePlot2DPanel)
        {
            roadProfilePlot2DPanel.Clear();
            string selectedItem = "Road profile";

            List<double> horizentalAxisData = new List<double>();
            List<double> verticalAxisData = new List<double>();

            double xMin = metricMap.RoadPointList[0].X;
            double xMax = metricMap.RoadPointList.Last().X;
            // It is assumed that there are only one SegmentSpeedProfile
            for (int i = 0; i < metricMap.MapSegmentList[0].NaturalCubicSplineList.Count; i++)
            {
                for (double u = 0; u <= 1; u += 0.1)
                {
                    double x = metricMap.MapSegmentList[0].NaturalCubicSplineList[i].GetPoint(u).CoordinateList[0];
                    double z = metricMap.MapSegmentList[0].NaturalCubicSplineList[i].GetPoint(u).CoordinateList[2];

                    horizentalAxisData.Add(x);
                    verticalAxisData.Add(z);
                }
            }

            double finalX = metricMap.MapSegmentList[0].NaturalCubicSplineList.Last().GetPoint(1).CoordinateList[0];
            double finalZ = metricMap.MapSegmentList[0].NaturalCubicSplineList.Last().GetPoint(1).CoordinateList[2];

            horizentalAxisData.Add(finalX);
            verticalAxisData.Add(finalZ);

            DataSeries roadProfileDataSeries = new DataSeries();
            roadProfileDataSeries.Generate(selectedItem, horizentalAxisData, verticalAxisData);
            roadProfileDataSeries.SetLineColor(Color.Navy);
            roadProfileDataSeries.SetPointColor(Color.Blue);
            roadProfileDataSeries.ConnectRange(xMin, xMax);
            roadProfileDataSeries.SetPointVisibilityState(false);
            roadProfileDataSeries.SetLineWidth(0.0035);

            double horizentalDataRange = horizentalAxisData.Max() - horizentalAxisData.Min();
            double verticalDataRange = verticalAxisData.Max() - verticalAxisData.Min();

            roadProfilePlot2DPanel.MajorHorizontalTickMarkSpacing = horizentalDataRange / 10;
            roadProfilePlot2DPanel.MajorVerticalTickMarkSpacing = verticalDataRange / 4;
            roadProfilePlot2DPanel.AxisColor = Color.Black;
            roadProfilePlot2DPanel.VerticalAxisVisible = true;
            roadProfilePlot2DPanel.HorizontalAxisVisible = true;
            roadProfilePlot2DPanel.HorizontalAxisMarkingsVisible = true;
            roadProfilePlot2DPanel.GridVisible = true;
            roadProfilePlot2DPanel.SetHorizontalPlotRange(horizentalAxisData.Min(), horizentalAxisData.Max());
            roadProfilePlot2DPanel.SetVerticalPlotRange(verticalAxisData.Min(), verticalAxisData.Max()); // Hardcoded range for the speed profile, need to be fixed.
            roadProfilePlot2DPanel.HorizontalAxisLabel = "X";
            roadProfilePlot2DPanel.HorizontalAxisLabelFontSize = 14;
            roadProfilePlot2DPanel.VerticalAxisLabel = "Elevation";
            roadProfilePlot2DPanel.VerticalAxisLabelFontSize = 14;
            roadProfilePlot2DPanel.HorizontalAxisMarkingsFormatString = "0";
            roadProfilePlot2DPanel.VerticalAxisMarkingsFormatString = "0";
            roadProfilePlot2DPanel.AddDataSeries(roadProfileDataSeries);
        }

        private void setOptimizationParameterButton_Click(object sender, EventArgs e)
        {
            OptimizationParameterSettingsForm settingsForm = new OptimizationParameterSettingsForm();
            settingsForm.SetOptimizationSettings(optimizationSettings);
            settingsForm.Show();
        }

        private void GenerateMetricPath()
        {
            // Generate path:
            TopologicalPathGenerator topologicalPathGenerator = new Dijkstra();
            int topologicalStartPointID = 0;
            int topologicalEndPointID = metricMap.RoadPointList.Count - 1;
            TopologicalPath topologicalPath = topologicalPathGenerator.GenerateShortestPath(metricMap, topologicalStartPointID, topologicalEndPointID);
            metricPath = new MetricPath();
            metricPath.Generate(topologicalPath, metricMap);
            metricPath.GenerateMetricPathPointList(0.001, 0.5, 0.01, 0.1); // Ugly MW ToDo: Parameterize
            metricPath.ComputeSegmentLengths(0.1);
        }

        private void startBatchRunButton_Click(object sender, EventArgs e)
        {
            startBatchRunButton.Enabled = false;
            batchRunProgressListBox.Enabled = false;

            batchRunInfo += "%% Optimization method: " + Enum.Parse(typeof(OptimizationMethod), OptimizationMethod.GA.ToString()) + "\r\n";
            batchRunInfo += "%% Number of generations: " + optimizationSettings.NumberOfGenerations.ToString("0") + "\r\n";
            batchRunInfo += "%% Population size: " + optimizationSettings.PopulationSize.ToString("0") + "\r\n";
            batchRunInfo += "%% Tournament selection rate: " + optimizationSettings.TournamentSelectionParameter.ToString("0.00") + "\r\n";
            batchRunInfo += "%% Tournament size: " + optimizationSettings.TournamentSize.ToString("0") + "\r\n";
            batchRunInfo += "%% Crossover probability: " + optimizationSettings.CrossoverProbability.ToString("0.00") + "\r\n";
            batchRunInfo += "%% Relative mutation rate: " + optimizationSettings.RelativeMutationProbability.ToString("0.00") + "\r\n";
            batchRunInfo += "%% Creep mutation rate: " + optimizationSettings.CreepMutationRate.ToString("0.00") + "\r\n";
            batchRunInfo += "%% Road file name " + roadFileName + "\r\n";
            batchRunInfo += "%% RunID, BestFitness(=1/fc), AverageFitness, [variables], IndexOfTheVariableVector, NumberOfEvaluatedIndividuals \r\n";

            PlotRoadProfile(roadPlot2DPanel);
            batchRunProgressListBox.Items.Clear();
            bestIndividualsList = new List<OptimizableStructure>();

            OptimizationMethod optimizationMethod = OptimizationMethod.GA;

            int numberOfBatchRuns = int.Parse(numberOfBatchRunsTextBox.Text);
            batchRunThread = new Thread(new ThreadStart(() => BatchRunLoop(numberOfBatchRuns, optimizationSettings, optimizationMethod)));
            batchRunThread.Start();
        }

        private void BatchRunLoop(int numberOfBatchRuns, PiecewiseLinearSpeedProfileOptimizationSettings optimizationSettings, OptimizationMethod optimizationMethod)
        {
            for (int iRun = 0; iRun < numberOfBatchRuns; iRun++)
            {
                speedProfileOptimizer = new PiecewiseLinearSpeedProfileOptimization();
                speedProfileOptimizer.OptimizationMethod = optimizationMethod;
                speedProfileOptimizer.IndividualEvaluated += new EventHandler<EvaluatedIndividualEventArgs>(ThreadSafeHandleBatchRunIndividualEvaluated);

                speedProfileOptimizer.RunSynchronous(optimizationSettings, new Random(), optimizationSettings.OptimizationTime, metricMap, metricPath);

                double bestScore = speedProfileOptimizer.BestScore;
                int numberOfEvaluatedIndividuals = speedProfileOptimizer.NumberOfEvaluatedIndividuals;
                OptimizableStructure optimizedProfile = speedProfileOptimizer.OptimizedSpeedProfile.Copy();
                bestIndividualsList.Add(optimizedProfile);
                double averageScore = speedProfileOptimizer.AverageFitness;

                List<double> speedSequence = new List<double>();
                List<int> speedSequenceIndexList = new List<int>();

                for (int i = 0; i < optimizationSettings.NumberOfSpeedPoints + 1; i++)
                {
                    int speedIndex = ((IntParameter)optimizedProfile.ParameterList[i]).ParameterValue;
                    speedSequenceIndexList.Add(speedIndex);
                    speedSequence.Add(speedProfileOptimizer.PossibleSpeedList[speedIndex]);
                }
                long speedProfileIndex = GetIndexFromValues(speedSequenceIndexList, speedProfileOptimizer.PossibleSpeedList.Count);

                batchRunInfo += iRun.ToString("0").PadLeft(3) + " " +
                                bestScore.ToString("0.000000").PadLeft(11) + " " +
                                averageScore.ToString("0.000000").PadLeft(11) + " [";
                for (int ii = 0; ii < speedSequenceIndexList.Count - 1; ii++) { batchRunInfo += speedSequenceIndexList[ii].ToString("0") + " "; }
                batchRunInfo += speedSequenceIndexList[speedSequenceIndexList.Count - 1].ToString("0") + "] " +
                                speedProfileIndex.ToString("0").PadLeft(13) + " " +
                                numberOfEvaluatedIndividuals.ToString("0").PadLeft(6) + "\r\n";

                ThreadSafeShowResult(numberOfEvaluatedIndividuals, bestScore, averageScore, iRun, optimizedProfile);
            }

            if (InvokeRequired) { this.BeginInvoke(new MethodInvoker(() => startBatchRunButton.Enabled = true)); }
            else { startBatchRunButton.Enabled = true; }
            if (InvokeRequired) { this.BeginInvoke(new MethodInvoker(() => batchRunProgressListBox.Enabled = true)); }
            else { batchRunProgressListBox.Enabled = true; }
            string path = batchPath + "\\BatchRunResult" + "_" + roadFileName + ".txt";
            System.IO.File.WriteAllText(path, batchRunInfo);            
        }

        private void ThreadSafeHandleBatchRunIndividualEvaluated(object sender, EvaluatedIndividualEventArgs e)
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => HandleBatchRunIndividualEvaluated(e))); }
            else { HandleBatchRunIndividualEvaluated(e); }
        }

        private void HandleBatchRunIndividualEvaluated(EvaluatedIndividualEventArgs e)
        {
            if (e.AverageSpeed > 0)
            {
                batchRunIndividualEvaluatedInfo += "Average individual: ";
                batchRunIndividualEvaluatedInfo += "[";
                for (int ii = 0; ii < e.AverageIndividualSpeedIndex.Count - 1; ii++)
                {
                    batchRunIndividualEvaluatedInfo += e.AverageIndividualSpeedIndex[ii].ToString("0") + " ";
                }
                batchRunIndividualEvaluatedInfo += e.AverageIndividualSpeedIndex[e.AverageIndividualSpeedIndex.Count - 1].ToString("0") + "]" + ", ";
                batchRunIndividualEvaluatedInfo += "Index: " + e.SpeedProfileIndex.ToString("0").PadLeft(12) + ", ";
                batchRunIndividualEvaluatedInfo += "Average speed: " + (e.AverageSpeed * 3.6).ToString("0.0").PadLeft(4) + ", ";
                batchRunIndividualEvaluatedInfo += "Fitness: " + e.Fitness.ToString("0.00000").PadLeft(9) + ", ";
                batchRunIndividualEvaluatedInfo += "================================= \r\n";
            }
            else
            {
                batchRunIndividualEvaluatedInfo += e.Fitness.ToString("0.00000").PadLeft(9) + ", ";
                batchRunIndividualEvaluatedInfo += "[";
                for (int ii = 0; ii < e.AverageIndividualSpeedIndex.Count - 1; ii++)
                {
                    batchRunIndividualEvaluatedInfo += e.AverageIndividualSpeedIndex[ii].ToString("0") + " ";
                }
                batchRunIndividualEvaluatedInfo += e.AverageIndividualSpeedIndex[e.AverageIndividualSpeedIndex.Count - 1].ToString("0") + "]" + ", ";
                batchRunIndividualEvaluatedInfo += e.SpeedProfileIndex.ToString("0").PadLeft(12) + "\r\n";
            }
            
        }

        private void ThreadSafeShowResult(int numberOfEvaluatedIndividuals, double bestScore, double averageScore, int iRun, OptimizableStructure optimizedProfile)
        {
            if (InvokeRequired) { Invoke(new MethodInvoker(() => ShowBatchResult(numberOfEvaluatedIndividuals, bestScore, averageScore, iRun, optimizedProfile))); }
            else { ShowBatchResult(numberOfEvaluatedIndividuals, bestScore, averageScore, iRun, optimizedProfile); }
        }

        private void ShowBatchResult(int numberOfEvaluatedIndividuals, double bestScore, double averageScore, int iRun, OptimizableStructure optimizedProfile)
        {
            batchRunProgressListBox.Items.Insert(0, "Run ID: " + iRun.ToString("0") + " Number of evaluated individuals: " +
            numberOfEvaluatedIndividuals.ToString().PadLeft(7) + " Best fitness: " +
            bestScore.ToString("0.000000"));

            List<double> speedSequence = new List<double>();

            for (int i = 0; i < optimizationSettings.NumberOfSpeedPoints + 1; i++)
            {
                int speedIndex = ((IntParameter)optimizedProfile.ParameterList[i]).ParameterValue;
                speedSequence.Add(speedProfileOptimizer.PossibleSpeedList[speedIndex]);
            }

            PiecewiseLinearSpeedProfile tempProfile = new PiecewiseLinearSpeedProfile(metricPath, optimizationSettings.NumberOfSpeedPoints);
            tempProfile.Generate(metricPath, speedSequence);
            PlotSpeedProfile(speedProfilePlot2DPanel, tempProfile, "Batch run");
            
            
        }

        private void PlotData(Plot2DPanel plotPanel, List<double> horizontalAxisData, List<double> verticalAxisData)
        {
            Color lineColor = Color.Navy;
            Color pointColor = Color.Blue;
            plotPanel.Clear();
            DataSeries speedProfileDataSeries = new DataSeries();
            speedProfileDataSeries.Generate("Logged data", horizontalAxisData, verticalAxisData);
            speedProfileDataSeries.SetLineColor(lineColor);
            speedProfileDataSeries.SetPointColor(pointColor);
            speedProfileDataSeries.ConnectRange(horizontalAxisData.Min(), horizontalAxisData.Max());
            speedProfileDataSeries.SetPointVisibilityState(false);
            speedProfileDataSeries.SetLineWidth(0.0035);

            double horizentalDataRange = horizontalAxisData.Max() - horizontalAxisData.Min();
            double verticalDataRange = optimizationSettings.MaximumSpeedValue - optimizationSettings.MinimumSpeedValue;

            plotPanel.MajorHorizontalTickMarkSpacing = horizentalDataRange / 10;
            plotPanel.MajorVerticalTickMarkSpacing = verticalDataRange / 4;
            plotPanel.AxisColor = Color.Black;
            plotPanel.VerticalAxisVisible = true;
            plotPanel.HorizontalAxisVisible = true;
            plotPanel.HorizontalAxisMarkingsVisible = true;
            plotPanel.GridVisible = true;
            plotPanel.SetHorizontalPlotRange(horizontalAxisData.Min(), horizontalAxisData.Max());
            plotPanel.SetVerticalPlotRange(optimizationSettings.MinimumSpeedValue, optimizationSettings.MaximumSpeedValue);
            // Hardcoded range for the speed profile, need to be fixed.
            plotPanel.HorizontalAxisLabel = "X";
            plotPanel.HorizontalAxisLabelFontSize = 14;
            plotPanel.VerticalAxisLabel = "Speed";
            plotPanel.VerticalAxisLabelFontSize = 14;
            plotPanel.HorizontalAxisMarkingsFormatString = "0";
            plotPanel.VerticalAxisMarkingsFormatString = "0.0";
            plotPanel.AddDataSeries(speedProfileDataSeries);
        }

        private long GetIndexFromValues(List<int> valueList, int numberOfLevels)
        {
            long convertedSpeedIndex = 0;
            int numberOfSpeedLevels = numberOfLevels;

            for (int ii = 0; ii < valueList.Count; ii++)
            {
                convertedSpeedIndex += valueList[ii] * (long)Math.Pow(numberOfSpeedLevels, ii);
            }

            return convertedSpeedIndex;
        }
        
        public List<int> GetValuesFromIndex(long index, int numberOfPoints, int numberOfLevels)
        {
            List<int> valueList = new List<int>();
            for (int ii = 0; ii < numberOfPoints; ii++) { valueList.Add(0); }
            long tempNumber = index;
            int counter = 0;
            while (tempNumber > 0)
            {
                long remainder = tempNumber % numberOfLevels;
                tempNumber /= numberOfLevels;
                valueList[counter] = (int)remainder;
                counter++;
            }
            return valueList;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
