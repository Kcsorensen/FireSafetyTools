using System;
using FireSafetyTools.Models.Tools.FireSafety.OpticalSmoke;
using FireSafetyTools.Services.Tools.FireSafety.OpticalSmoke;
using Microsoft.AspNetCore.Mvc;

namespace FireSafetyTools.Controllers
{
    public class OpticalSmokeController : Controller
    {
        private IUnitConverterContext _context;

        public OpticalSmokeController(IUnitConverterContext unitConverterConvext)
        {
            _context = unitConverterConvext;
        }

        public IActionResult Index()
        {
            var smokedata = _context.GetsSmokeData();

            return View(smokedata);
        }

        public ActionResult SmokeResult()
        {
            var smokedata = _context.GetsSmokeData();

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

            _context.UpdateSmokeData(result);

            return RedirectToAction("SmokeResult");
        }

        public IActionResult ClearSmokeData()
        {
            _context.ClearSmokeData();

            return RedirectToAction("Index");
        }
    }
}