using System.Linq;
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
                var evacuationViewModel = new EvacuationViewModel();
                evacuationViewModel.Initiate();

                HttpContext.Session.SetObjectAsJson(SessionNames.EvacuationData, evacuationViewModel);
            }

            var viewModel = HttpContext.Session.GetObjectFromJson<EvacuationViewModel>(SessionNames.EvacuationData);

            return View(viewModel);
        }

        public IActionResult CreateRoute()
        {
            var viewModel = new CreateRouteViewModel();

            return View(viewModel);
        }

        public IActionResult CreateNewRouteElement(int routeTypeId, int routeId)
        {
            if (routeTypeId == 0)
            {
                return NotFound();
            }

            var viewModel = new CreateRouteElementViewModel()
            {
                RouteId = routeId,
                RouteTypeId = routeTypeId,
            };

            return View(viewModel);
        }

        public IActionResult ClearTable()
        {
            if (HttpContext.Session.GetObjectFromJson<EvacuationViewModel>(SessionNames.EvacuationData) == null)
            {
                return RedirectToAction("Index");
            }

            var evacuationViewModel = HttpContext.Session.GetObjectFromJson<EvacuationViewModel>(SessionNames.EvacuationData);

            evacuationViewModel.ClearRoutes();

            HttpContext.Session.SetObjectAsJson(SessionNames.EvacuationData, evacuationViewModel);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveRoute(CreateRouteViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            if (HttpContext.Session.GetObjectFromJson<EvacuationViewModel>(SessionNames.EvacuationData) == null)
            {
                return RedirectToAction("Index");
            }

            var evacuationViewModel = HttpContext.Session.GetObjectFromJson<EvacuationViewModel>(SessionNames.EvacuationData);

            evacuationViewModel.CreateRoute(viewModel);

            HttpContext.Session.SetObjectAsJson(SessionNames.EvacuationData, evacuationViewModel);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveRouteElement(CreateRouteElementViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            if (HttpContext.Session.GetObjectFromJson<EvacuationViewModel>(SessionNames.EvacuationData) == null)
            {
                return RedirectToAction("Index");
            }

            var evacuationViewModel = HttpContext.Session.GetObjectFromJson<EvacuationViewModel>(SessionNames.EvacuationData);

            evacuationViewModel.AddRouteElementToRoute(viewModel);

            HttpContext.Session.SetObjectAsJson(SessionNames.EvacuationData, evacuationViewModel);

            return RedirectToAction("Index");
        }

        public IActionResult EditRoute(int routeId)
        {
            return View("Index");
        }

        public IActionResult EditRouteElement(int routeId, int routeElementId)
        {
            return View("Index");
        }

        public IActionResult CalculateRoutes()
        {
            if (HttpContext.Session.GetObjectFromJson<EvacuationViewModel>(SessionNames.EvacuationData) == null)
            {
                return RedirectToAction("Index");
            }

            var evacuationViewModel = HttpContext.Session.GetObjectFromJson<EvacuationViewModel>(SessionNames.EvacuationData);

            evacuationViewModel.CalculateRoutes();

            HttpContext.Session.SetObjectAsJson(SessionNames.EvacuationData, evacuationViewModel);

            return View("EvacuationResult", evacuationViewModel);
        }
    }
}