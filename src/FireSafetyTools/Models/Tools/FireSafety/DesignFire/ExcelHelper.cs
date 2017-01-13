using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FireSafetyTools.ViewModels.Tools.FireSafety.DesignFire;
using OfficeOpenXml;

namespace FireSafetyTools.Models.Tools.FireSafety.DesignFire
{
    public class ExcelHelper
    {
        public void SaveExcelResult(string filePathAndName, DesignFireViewModel designFireViewModel)
        {
            FileInfo file = new FileInfo(Path.Combine(filePathAndName));

            if (designFireViewModel.Phases == null)
            {
                throw new NullReferenceException("input phases in ExcelHelper cannot be null");
            }

            using (ExcelPackage package = new ExcelPackage(file))
            {
                // add new worksheets to the empty workbook
                ExcelWorksheet worksheetPhases = package.Workbook.Worksheets.Add("Phases");
                ExcelWorksheet worksheetGraph = package.Workbook.Worksheets.Add("Graph data");
                ExcelWorksheet worksheetPyrosim = package.Workbook.Worksheets.Add("Pyrosim data");

                #region worksheetPhases

                //First add the headers
                worksheetPhases.Cells[1, 1].Value = "#";
                worksheetPhases.Cells[1, 2].Value = "Phase";
                worksheetPhases.Cells[1, 3].Value = "Duration [s]";
                worksheetPhases.Cells[1, 4].Value = "Growth Rate [kW/s²]";
                worksheetPhases.Cells[1, 5].Value = "Max Effect [kW]";
                worksheetPhases.Cells[1, 6].Value = "Total Energy [MJ]";

                // Make all cells in first row bold
                using (var cells = worksheetPhases.Cells[1, 1, 1, 6])
                {
                    cells.Style.Font.Bold = true;
                }
                
                //Add values to worksheetPhases
                int phaseCounter = 1;

                foreach (var phase in designFireViewModel.Phases)
                {
                    worksheetPhases.Cells[1 + phaseCounter, 1].Value = phase.Id;
                    worksheetPhases.Cells[1 + phaseCounter, 2].Value = phase.Name;
                    worksheetPhases.Cells[1 + phaseCounter, 3].Value = phase.Duration;
                    worksheetPhases.Cells[1 + phaseCounter, 4].Value = phase.GrowthRateFactor;
                    worksheetPhases.Cells[1 + phaseCounter, 5].Value = phase.TargetYq;
                    worksheetPhases.Cells[1 + phaseCounter, 6].Value = phase.TotalEnergyReleased;

                    phaseCounter += 1;
                }

                // Two rows below the list of phases, add a the sum of Energy released 
                worksheetPhases.Cells[3 + phaseCounter, 2].Value = "Sum of Energy Released";
                worksheetPhases.Cells[3 + phaseCounter, 6].Value = designFireViewModel.Phases.Sum(x => x.TotalEnergyReleased);

                #endregion

                #region worksheetGraph

                //First add the headers
                worksheetGraph.Cells[1, 1].Value = "Time";
                worksheetGraph.Cells[1, 2].Value = "Effect";

                // Make all cells in first row bold
                using (var cells = worksheetGraph.Cells[1, 1, 1, 2])
                {
                    cells.Style.Font.Bold = true;
                }

                //Add values to worksheetGraph
                int graphCounter = 1;

                foreach (var phase in designFireViewModel.Phases)
                {
                    if (phase.GetDataPoints() == null)
                    {
                        throw new NullReferenceException("The DataPoints in the Phase cannot be null -> worksheetGraph");
                    }

                    foreach (var dataPoint in phase.GetDataPoints())
                    {
                        worksheetGraph.Cells[1 + graphCounter, 1].Value = dataPoint.Time;
                        worksheetGraph.Cells[1 + graphCounter, 2].Value = dataPoint.Effect;

                        graphCounter += 1;
                    }
                }

                #endregion

                #region worksheetPyrosim

                //First add the headers
                worksheetPyrosim.Cells[1, 1].Value = "Time";
                worksheetPyrosim.Cells[1, 2].Value = "Fraction";

                // Make all cells in first row bold
                using (var cells = worksheetPyrosim.Cells[1, 1, 1, 2])
                {
                    cells.Style.Font.Bold = true;
                }

                //Add values to worksheetPhases
                int pyrosimCounter = 1;

                var maxEffect = designFireViewModel.ChartDataPoints.Max(x => x.Effect);
                
                foreach (var phase in designFireViewModel.Phases)
                {
                    if (phase.GetDataPoints() == null)
                    {
                        throw new NullReferenceException("The DataPoints in the Phase cannot be null -> worksheetPyrosim");
                    }
                    
                    foreach (var dataPoint in phase.GetDataPoints())
                    {
                        var time = Math.Round(dataPoint.Time, 2);
                        var fraction = Math.Round((dataPoint.Effect / maxEffect), 2);

                        worksheetPyrosim.Cells[1 + pyrosimCounter, 1].Value = time;
                        worksheetPyrosim.Cells[1 + pyrosimCounter, 2].Value = fraction;

                        pyrosimCounter += 1;
                    }
                }

                #endregion

                package.Save(); //Save the workbook.
            }
        }

        public Stream SaveExcelResultStream(DesignFireViewModel designFireViewModel)
        {
            if (designFireViewModel.Phases == null)
            {
                throw new NullReferenceException("input phases in ExcelHelper cannot be null");
            }

            using (ExcelPackage package = new ExcelPackage())
            {
                // add new worksheets to the empty workbook
                ExcelWorksheet worksheetPhases = package.Workbook.Worksheets.Add("Phases");
                ExcelWorksheet worksheetGraph = package.Workbook.Worksheets.Add("Graph data");
                ExcelWorksheet worksheetPyrosim = package.Workbook.Worksheets.Add("Pyrosim data");

                #region worksheetPhases

                //First add the headers
                worksheetPhases.Cells[1, 1].Value = "#";
                worksheetPhases.Cells[1, 2].Value = "Phase";
                worksheetPhases.Cells[1, 3].Value = "Duration [s]";
                worksheetPhases.Cells[1, 4].Value = "Growth Rate [kW/s²]";
                worksheetPhases.Cells[1, 5].Value = "Max Effect [kW]";
                worksheetPhases.Cells[1, 6].Value = "Total Energy [MJ]";

                // Make all cells in first row bold
                using (var cells = worksheetPhases.Cells[1, 1, 1, 6])
                {
                    cells.Style.Font.Bold = true;
                }

                //Add values to worksheetPhases
                int phaseCounter = 1;

                foreach (var phase in designFireViewModel.Phases)
                {
                    worksheetPhases.Cells[1 + phaseCounter, 1].Value = phase.Id;
                    worksheetPhases.Cells[1 + phaseCounter, 2].Value = phase.Name;
                    worksheetPhases.Cells[1 + phaseCounter, 3].Value = phase.Duration;
                    worksheetPhases.Cells[1 + phaseCounter, 4].Value = phase.GrowthRateFactor;
                    worksheetPhases.Cells[1 + phaseCounter, 5].Value = phase.TargetYq;
                    worksheetPhases.Cells[1 + phaseCounter, 6].Value = phase.TotalEnergyReleased;

                    phaseCounter += 1;
                }

                // Two rows below the list of phases, add a the sum of Energy released 
                worksheetPhases.Cells[3 + phaseCounter, 2].Value = "Sum of Energy Released";
                worksheetPhases.Cells[3 + phaseCounter, 6].Value = designFireViewModel.Phases.Sum(x => x.TotalEnergyReleased);

                #endregion

                #region worksheetGraph

                //First add the headers
                worksheetGraph.Cells[1, 1].Value = "Time";
                worksheetGraph.Cells[1, 2].Value = "Effect";

                // Make all cells in first row bold
                using (var cells = worksheetGraph.Cells[1, 1, 1, 2])
                {
                    cells.Style.Font.Bold = true;
                }

                //Add values to worksheetGraph
                int graphCounter = 1;

                foreach (var phase in designFireViewModel.Phases)
                {
                    if (phase.GetDataPoints() == null)
                    {
                        throw new NullReferenceException("The DataPoints in the Phase cannot be null -> worksheetGraph");
                    }

                    foreach (var dataPoint in phase.GetDataPoints())
                    {
                        worksheetGraph.Cells[1 + graphCounter, 1].Value = dataPoint.Time;
                        worksheetGraph.Cells[1 + graphCounter, 2].Value = dataPoint.Effect;

                        graphCounter += 1;
                    }
                }

                #endregion

                #region worksheetPyrosim

                //First add the headers
                worksheetPyrosim.Cells[1, 1].Value = "Time";
                worksheetPyrosim.Cells[1, 2].Value = "Fraction";

                // Make all cells in first row bold
                using (var cells = worksheetPyrosim.Cells[1, 1, 1, 2])
                {
                    cells.Style.Font.Bold = true;
                }

                //Add values to worksheetPhases
                int pyrosimCounter = 1;

                var maxEffect = designFireViewModel.ChartDataPoints.Max(x => x.Effect);

                foreach (var phase in designFireViewModel.Phases)
                {
                    if (phase.GetDataPoints() == null)
                    {
                        throw new NullReferenceException("The DataPoints in the Phase cannot be null -> worksheetPyrosim");
                    }

                    foreach (var dataPoint in phase.GetDataPoints())
                    {
                        var time = Math.Round(dataPoint.Time, 2);
                        var fraction = Math.Round((dataPoint.Effect / maxEffect), 2);

                        worksheetPyrosim.Cells[1 + pyrosimCounter, 1].Value = time;
                        worksheetPyrosim.Cells[1 + pyrosimCounter, 2].Value = fraction;

                        pyrosimCounter += 1;
                    }
                }

                #endregion

                return new MemoryStream(package.GetAsByteArray());
            }
        }
    }
}
