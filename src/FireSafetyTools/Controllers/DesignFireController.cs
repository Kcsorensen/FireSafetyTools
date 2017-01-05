using System;
using System.Threading.Tasks;
using FireSafetyTools.Dtos.Tools.FireSafety;
using FireSafetyTools.Models.Tools.FireSafety.DesignFire;
using FireSafetyTools.Services;
using FireSafetyTools.ViewModels.Tools.FireSafety.DesignFire;
using Microsoft.AspNetCore.Mvc;

namespace FireSafetyTools.Controllers
{
    public class DesignFireController : Controller
    {
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

            designFireViewModel.PhaseTypeId = id;
            designFireViewModel.UpdateState();

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

            if (phaseFormViewModel == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData) == null)
            {
                return RedirectToAction("Index");
            }

            var designFireViewModel = HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData);

            var phaseCalculator = new Calculator();

            var updatedPhase = await phaseCalculator.GeneratePhaseAsync(phaseFormViewModel, designFireViewModel.State);

            if (updatedPhase == null)
            {
                throw new ArgumentNullException("UpdatedPhase as a result of phaseCalculator is null. From DesignFireController -> Save");
            }

            designFireViewModel.AddPhase(updatedPhase);

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
        public IActionResult DeletePhase(int id)
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

            designFireViewModel.DeletePhase(id);

            HttpContext.Session.SetObjectAsJson(SessionNames.DesignFireData, designFireViewModel);

            return RedirectToAction("Index");
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
    }
}