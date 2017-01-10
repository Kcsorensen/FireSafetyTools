using System;
using System.Threading.Tasks;
using FireSafetyTools.Dtos.Tools.FireSafety;
using FireSafetyTools.Models.Tools.FireSafety.DesignFire;
using FireSafetyTools.Services;
using FireSafetyTools.ViewModels.Tools.FireSafety.DesignFire;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

            //designFireViewModel.PhaseTypeId = id;
            //designFireViewModel.UpdateState();

            HttpContext.Session.SetObjectAsJson(SessionNames.DesignFireData, designFireViewModel);

            var phaseFormViewModel = new PhaseFormViewModel {PhaseTypeId = id};

            return View("PhaseForm", phaseFormViewModel); 
        }

        [HttpPost]
        public IActionResult Save(PhaseFormViewModel phaseFormViewModel)
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
                designFireViewModel.UpdatePhase(phaseFormViewModel);
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
    }
}