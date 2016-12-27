using System;
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

        public IActionResult New(double latestXt, double latestYq, int id)
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

            var phaseViewModel = new PhaseViewModel(latestXt, latestYq, id);

            return View("PhaseForm", phaseViewModel);
        }

        [HttpPost]
        public IActionResult Save(Phase phase)
        {
            if (!ModelState.IsValid)
            {
                var phaseViewModel = new PhaseViewModel(phase);

                return View("PhaseForm", phaseViewModel);
            }

            if (phase == null)
            {
                return NotFound();
            }

            var phaseCalculator = new PhaseCalculator();

            var updatedPhase = phaseCalculator.Calculate(phase);

            if (updatedPhase == null)
            {
                throw new ArgumentNullException("updatedPhase as a result of phaseCalculator is null. From DesignFireController -> Save");
            }

            var designFireViewModel = HttpContext.Session.GetObjectFromJson<DesignFireViewModel>(SessionNames.DesignFireData);

            designFireViewModel.AddPhase(updatedPhase);

            HttpContext.Session.SetObjectAsJson(SessionNames.DesignFireData, designFireViewModel);

            return RedirectToAction("Index");
        }
    }
}