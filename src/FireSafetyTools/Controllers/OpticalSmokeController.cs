using FireSafetyTools.Models.Tools.FireSafety.OpticalSmoke;
using FireSafetyTools.Services;
using FireSafetyTools.ViewModels.Tools.FireSafety.OpticalSmoke;
using Microsoft.AspNetCore.Mvc;

namespace FireSafetyTools.Controllers
{
    public class OpticalSmokeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetObjectFromJson<SmokeDataViewModel>(SessionNames.SmokeData) == null)
            {
                var smokeDataViewModel = new SmokeDataViewModel();

                smokeDataViewModel.Initiate();

                HttpContext.Session.SetObjectAsJson(SessionNames.SmokeData, smokeDataViewModel);
            }

            var viewModel = HttpContext.Session.GetObjectFromJson<SmokeDataViewModel>(SessionNames.SmokeData);

            return View(viewModel);
        }

        public ActionResult SmokeResult()
        {
            if (HttpContext.Session.GetObjectFromJson<SmokeDataViewModel>(SessionNames.SmokeData) == null)
            {
                return RedirectToAction("Index");
            }

            var viewModel = HttpContext.Session.GetObjectFromJson<SmokeDataViewModel>(SessionNames.SmokeData);

            var smokeData = viewModel.GetsSmokeData();

            return View(smokeData);
        }

        [HttpPost]
        public IActionResult Calculate(SmokeData _smokeData)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var smokeUnitConverter = new SmokeUnitConverter();

            var result = smokeUnitConverter.TrimAndCalculate(_smokeData.ConvertFromId.ToString(),
                _smokeData.ConvertToId.ToString(), _smokeData);

            if (result == null)
            {
                return NotFound();
            }

            var viewModel = HttpContext.Session.GetObjectFromJson<SmokeDataViewModel>(SessionNames.SmokeData);

            viewModel.UpdateSmokeData(result);

            HttpContext.Session.SetObjectAsJson(SessionNames.SmokeData, viewModel);

            return RedirectToAction("SmokeResult");
        }

        public IActionResult ClearSmokeData()
        {
            var viewModel = HttpContext.Session.GetObjectFromJson<SmokeDataViewModel>(SessionNames.SmokeData);

            viewModel.ClearSmokeData();

            HttpContext.Session.SetObjectAsJson(SessionNames.SmokeData, viewModel);

            return RedirectToAction("Index");
        }
    }
}