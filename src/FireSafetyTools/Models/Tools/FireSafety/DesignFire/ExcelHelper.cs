using System;
using System.Drawing;
using System.IO;
using System.Linq;
using FireSafetyTools.ViewModels.Tools.FireSafety.DesignFire;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace FireSafetyTools.Models.Tools.FireSafety.DesignFire
{
    public class ExcelHelper
    {
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
                    cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                //Add values to worksheetPhases
                int phaseCounter = 1;

                foreach (var phase in designFireViewModel.Phases)
                {
                    worksheetPhases.Cells[1 + phaseCounter, 1].Value = phase.Id;
                    worksheetPhases.Cells[1 + phaseCounter, 2].Value = phase.Name;
                    worksheetPhases.Cells[1 + phaseCounter, 3].Value = Math.Round(phase.Duration, 2);
                    worksheetPhases.Cells[1 + phaseCounter, 4].Value = Math.Round(phase.GrowthRateFactor, 5);
                    worksheetPhases.Cells[1 + phaseCounter, 5].Value = Math.Round(phase.TargetYq, 2);
                    worksheetPhases.Cells[1 + phaseCounter, 6].Value = Math.Round(phase.TotalEnergyReleased, 2);

                    phaseCounter += 1;
                }

                // Two rows below the list of phases, add a the sum of Energy released 
                worksheetPhases.Cells[3 + phaseCounter, 2].Value = "Sum of Energy Released";
                worksheetPhases.Cells[3 + phaseCounter, 6].Value = Math.Round(designFireViewModel.Phases.Sum(x => x.TotalEnergyReleased), 2);

                #endregion

                #region worksheetGraph

                //First add the headers
                worksheetGraph.Cells[1, 1].Value = "Time";
                worksheetGraph.Cells[1, 2].Value = "Effect";

                // Make all cells in first row bold
                using (var cells = worksheetGraph.Cells[1, 1, 1, 2])
                {
                    cells.Style.Font.Bold = true;
                    cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
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
                    cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
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

                // Styling the worksheet
                worksheetPhases.Cells[1, 1, 1, 6].AutoFitColumns();
                worksheetGraph.Cells[1, 1, 1, 2].AutoFitColumns();
                worksheetPyrosim.Cells[1, 1, 1, 2].AutoFitColumns();
                worksheetPhases.Column(2).Width = 29;

                return new MemoryStream(package.GetAsByteArray());
            }
        }
    }
}
