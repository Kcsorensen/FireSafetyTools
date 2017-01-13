using System.IO;
using System.Threading.Tasks;
using FireSafetyTools.Dtos.Tools.FireSafety;
using FireSafetyTools.Models.Tools.FireSafety.DesignFire;
using FireSafetyTools.Services;
using FireSafetyTools.ViewModels.Tools.FireSafety.DesignFire;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;

namespace FireSafetyTools.Controllers
{
    public class DesignFireController : Controller
    {
        public DesignFireController()
        {
            string sFileName = @"d:\\demo.xlsx";
            
            FileInfo file = new FileInfo(Path.Combine(sFileName));

            if (file.Exists)
            {
                file.Delete();
            }

            using (ExcelPackage package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Employee");
                //First add the headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Gender";
                worksheet.Cells[1, 4].Value = "Salary (in $)";

                //Add values
                worksheet.Cells["A2"].Value = 1000;
                worksheet.Cells["B2"].Value = "Jon";
                worksheet.Cells["C2"].Value = "M";
                worksheet.Cells["D2"].Value = 5000;

                worksheet.Cells["A3"].Value = 1001;
                worksheet.Cells["B3"].Value = "Graham";
                worksheet.Cells["C3"].Value = "M";
                worksheet.Cells["D3"].Value = 10000;

                worksheet.Cells["A4"].Value = 1002;
                worksheet.Cells["B4"].Value = "Jenny";
                worksheet.Cells["C4"].Value = "F";
                worksheet.Cells["D4"].Value = 5000;

                package.Save(); //Save the workbook.

            }
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData) == null)
            {
                var designFireViewModel = new DesignFireViewModel();
                designFireViewModel.Initiate();

                HttpContext.Session.SetObjectAsJson(SessionNames.DesignFireData, designFireViewModel);
            }

            var viewModel = HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData);

            return View(viewModel);
        }
        
        // id is phaseTypeId
        public IActionResult New(int id)
        {
            if (HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData) == null)
            {
                return RedirectToAction("Index");
            }

            var designFireViewModel = HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData);

            if (id > designFireViewModel.PhaseTypes.Count || id < 0)
            {
                return NotFound();
            }

            HttpContext.Session.SetObjectAsJson(SessionNames.DesignFireData, designFireViewModel);

            var phaseFormViewModel = new PhaseFormViewModel {PhaseTypeId = id};

            return View("PhaseForm", phaseFormViewModel); 
        }

        [HttpPost]
        public async Task<IActionResult> Save(PhaseFormViewModel phaseFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new PhaseFormViewModel();

                return View("PhaseForm", viewModel);
            }

            if (HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData) == null)
            {
                return RedirectToAction("Index");
            }

            var designFireViewModel = HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData);

            // If creating a new Phase
            if (phaseFormViewModel.PhaseId == 0)
            {
                designFireViewModel.AddPhase(phaseFormViewModel);
            }

            // If editing a existing phase
            if (phaseFormViewModel.PhaseId > 0)
            {
                await designFireViewModel.UpdatePhaseAsync(phaseFormViewModel);
            }

            HttpContext.Session.SetObjectAsJson(SessionNames.DesignFireData, designFireViewModel);

            return RedirectToAction("Index"); 
        }

        public IActionResult ClearTable()
        {
            if (HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData) == null)
            {
                return RedirectToAction("Index");
            }

            var designFireViewModel = HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData);

            designFireViewModel.ClearPhases();

            HttpContext.Session.SetObjectAsJson(SessionNames.DesignFireData, designFireViewModel);

            return View("Index", designFireViewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePhase(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            if (HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData) == null)
            {
                return RedirectToAction("Index");
            }

            var designFireViewModel = HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData);

            await designFireViewModel.DeletePhaseAsync(id);

            HttpContext.Session.SetObjectAsJson(SessionNames.DesignFireData, designFireViewModel);

            return RedirectToAction("Index");
        }

        public IActionResult EditPhase(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            if (HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData) == null)
            {
                return RedirectToAction("Index");
            }

            var designFireViewModel = HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData);

            var selectedPhase = designFireViewModel.Phases.Single(x => x.Id == id);

            var phaseFormViewModel = new PhaseFormViewModel(selectedPhase) { PhaseId = id };

            return View("EditForm", phaseFormViewModel);
        }

        public IActionResult GetChartData()
        {
            if (HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData) == null)
            {
                var designFireViewModel = new DesignFireViewModel();
                designFireViewModel.Initiate();

                HttpContext.Session.SetObjectAsJson(SessionNames.DesignFireData, designFireViewModel);
            }

            var viewModel = HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData);

            var chartDataDto = new ChartDataDto()
            {
                XAxis = viewModel.XAxis,
                YAxis = viewModel.YAxis
            };

            return Json(chartDataDto);
        }

        public IActionResult GetPyrosimExportData()
        {
            if (HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData) == null)
            {
                var designFireViewModel = new DesignFireViewModel();
                designFireViewModel.Initiate();

                HttpContext.Session.SetObjectAsJson(SessionNames.DesignFireData, designFireViewModel);
            }

            var viewModel = HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData);

            var resultString = viewModel.GetPyrosimExportData();

            return Content(resultString); 
        }
    }
}