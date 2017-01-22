using System;
using System.Collections.Generic;
using System.Linq;
using FireSafetyTools.Models.Tools.FireSafety.Evacuation;

namespace FireSafetyTools.ViewModels.Tools.FireSafety.Evacuation
{
    public class EvacuationViewModel
    {
        public Dictionary<int, List<BaseRouteElement>> Routes { get; set; }

        public EvacuationViewModel()
        {
            Routes = new Dictionary<int, List<BaseRouteElement>>();

            StartupExample();
        }

        private void StartupExample()
        {
            var routeViewModel = new CreateRouteViewModel()
            {
                Name = "Evacuation"
            };

            CreateRoute(routeViewModel);
        }

        public void CreateRoute(CreateRouteViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new NullReferenceException("Input parameter CreateRouteViewModel cannot be null, EvacuationViewModel -> CreateRoute");
            }

            int routeId = (Routes.Count == 0) ? Routes.Count : Routes.Count + 1;

            var listOfRouteElements = new List<BaseRouteElement>()
            {
                new RouteStart()
                {
                    RouteElementId = 0,
                    Name = viewModel.Name,
                    NumberOfPeople = viewModel.NumberOfPeople,
                    TransitionType = TransitionTypes.RouteStartElement,
                    DetectionTime = viewModel.DetectionTime,
                    NotificationTime = viewModel.NotificationTime,
                    PreEvacuationTime = viewModel.PreEvacuationTime
                }
            };

            Routes.Add(routeId, listOfRouteElements);
        }

        public void AddRouteElementToRoute(CreateRouteElementViewModel viewModel)
        {
            if (Routes.Count == 0)
            {
                throw new Exception("Cannot add routeElement to Route, if there is no routes, EvacuationViewModel -> AddRouteElementToRoute");
            }

            // NullExceptionCheck is implemented in Single()
            var route = Routes.Single(x => x.Key == viewModel.RouteId).Value;

            // Default value
            int transitionType = TransitionTypes.OneFlowInOneFlowOut;

            if (route.Count == 1)
            {
                transitionType = TransitionTypes.FirstRouteElement;
            }

            if (viewModel.RouteTypeId == RouteTypeHelper.Corridor)
            {
                var routeElement = new Corridor()
                {
                    Name = viewModel.Name,
                    Width = viewModel.Width,
                    Density = viewModel.Density,
                    NumberOfPeople = viewModel.NumberOfPeople,
                    Distance = viewModel.Distance,
                    TransitionType = transitionType,
                    RouteElementId = route.Count
                };

                route.Add(routeElement);
            }

            //if (route.Any(x => x.Guid == routeElement.Guid))
            //{
            //    throw new Exception("Cannot add a RouteElement in a route to the same route, EvacuationViewModel -> AddRouteElementToRoute");
            //}

            //route.Add(routeElement);
        }

        public List<BaseRouteElement> GetRoute(int routeId)
        {
            if (Routes.Any(x => x.Key != routeId))
            {
                throw new Exception("Cannot find any routes by that Guid, EvacuationViewModel -> GetRoute");
            }

            var route = Routes.Single(x => x.Key == routeId).Value;

            return route;
        }

        public void ClearRoutes()
        {
            Routes.Clear();
        }
    }
}
