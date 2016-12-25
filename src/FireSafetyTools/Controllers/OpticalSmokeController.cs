using FireSafetyTools.Models.Tools.FireSafety.OpticalSmoke;
using FireSafetyTools.Services;
using FireSafetyTools.ViewModel.Tools.FireSafety.OpticalSmoke;
using Microsoft.AspNetCore.Mvc;

namespace FireSafetyTools.Controllers
{
    public class OpticalSmokeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetObjectFromJson<SmokeData>(SessionNames.SmokeData) == null)
            {
                HttpContext.Session.SetObjectAsJson(SessionNames.SmokeData, new SmokeData());
            }

            //var viewModel = HttpContext.Session.GetObjectFromJson<SmokeDataViewModel>(SessionNames.SmokeData);

            var smokedata = HttpContext.Session.GetObjectFromJson<SmokeData>(SessionNames.SmokeData);

            return View(smokedata);
        }

        public ActionResult SmokeResult()
        {
            if (HttpContext.Session.GetObjectFromJson<SmokeData>(SessionNames.SmokeData) == null)
            {
                return RedirectToAction("Index");
            }

            //var viewModel = HttpContext.Session.GetObjectFromJson<SmokeDataViewModel>(SessionNames.SmokeData);

            var smokedata = HttpContext.Session.GetObjectFromJson<SmokeData>(SessionNames.SmokeData);

            return View("SmokeResult", smokedata);
        }

        [HttpPost]
        public IActionResult Calculate(SmokeData smokeData)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var smokeUnitConverter = new SmokeUnitConverter();

            var result = smokeUnitConverter.TrimAndCalculate(smokeData.ConvertFromId.ToString(),
                smokeData.ConvertToId.ToString(), smokeData);

            if (result == null)
            {
                return NotFound();
            }

            //var viewModel = HttpContext.Session.GetObjectFromJson<SmokeDataViewModel>(SessionNames.SmokeData);

            //viewModel.UpdateSmokeData(result);

            HttpContext.Session.SetObjectAsJson(SessionNames.SmokeData, result);

            return RedirectToAction("SmokeResult");
        }

        public IActionResult ClearSmokeData()
        {
            //var viewModel = HttpContext.Session.GetObjectFromJson<SmokeDataViewModel>(SessionNames.SmokeData);

            //viewModel.ClearSmokeData();

            var smokeData = new SmokeData();

            HttpContext.Session.SetObjectAsJson(SessionNames.SmokeData, smokeData);

            return RedirectToAction("Index");
        }
    }
}