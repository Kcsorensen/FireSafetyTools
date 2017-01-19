using FireSafetyTools.Models.Tools.FireSafety.Evacuation;
using FireSafetyTools.Services;
using FireSafetyTools.ViewModels.Tools.FireSafety.Evacuation;
using Microsoft.AspNetCore.Mvc;

namespace FireSafetyTools.Controllers
{
    public class EvacuationController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetObjectFromJson<EvacuationViewModel>(SessionNames.EvacuationData) == null)
            {
                var designFireViewModel = new EvacuationViewModel();
                designFireViewModel.Initiate();

                HttpContext.Session.SetObjectAsJson(SessionNames.EvacuationData, designFireViewModel);
            }

            var viewModel = HttpContext.Session.GetObjectFromJson<EvacuationViewModel>(SessionNames.EvacuationData);

            return View(viewModel);
        }

        public IActionResult CreateRoute()
        {
            var viewModel = new CreateRouteViewModel();

            return View(viewModel);
        }

        public IActionResult CreateNewRouteElement()
        {
            return View();
        }

        public IActionResult ClearTable()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveRoute()
        {
            return RedirectToAction("Index");
        }
    }
}