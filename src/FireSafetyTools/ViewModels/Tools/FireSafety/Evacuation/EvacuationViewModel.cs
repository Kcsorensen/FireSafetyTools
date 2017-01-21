using System;
using System.Collections.Generic;
using System.Linq;
using FireSafetyTools.Models.Tools.FireSafety.Evacuation;

namespace FireSafetyTools.ViewModels.Tools.FireSafety.Evacuation
{
    public class EvacuationViewModel
    {
        public Dictionary<Guid, List<BaseRouteElement>> Routes { get; set; }
        public List<BaseRouteElement> RouteElements { get; set; }

        public EvacuationViewModel()
        {
            Initiate();
        }

        public void Initiate()
        {
            Routes = new Dictionary<Guid, List<BaseRouteElement>>();
            RouteElements = new List<BaseRouteElement>();
        }

        public void AddRouteElement(BaseRouteElement routeelement)
        {
            RouteElements.Add(routeelement);
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
                    RouteId = routeId,
                    RouteElementId = 0,
                    Name = viewModel.Name,
                    NumberOfPeople = viewModel.NumberOfPeople,
                    TransitionType = TransitionTypes.RouteStartElement,
                    DetectionTime = viewModel.DetectionTime,
                    NotificationTime = viewModel.NotificationTime,
                    PreEvacuationTime = viewModel.PreEvacuationTime
                }
            };

            Routes.Add(Guid.NewGuid(), listOfRouteElements);
        }

        public void AddRouteElementToRoute(Guid guid, BaseRouteElement routeElement)
        {
            if (Routes.Count == 0)
            {
                throw new Exception("Cannot add routeElement to Route, if there is no routes, EvacuationViewModel -> AddRouteElementToRoute");
            }

            // NullExceptionCheck is implemented in Single()
            var route = Routes.Single(x => x.Key == guid).Value;

            if (route.Count == 1)
            {
                routeElement.TransitionType = TransitionTypes.FirstRouteElement;
            }

            if (route.Any(x => x.Guid == routeElement.Guid))
            {
                throw new Exception("Cannot add a RouteElement in a route to the same route, EvacuationViewModel -> AddRouteElementToRoute");
            }

            route.Add(routeElement);
        }

        public List<BaseRouteElement> GetRoute(Guid guid)
        {
            if (Routes.Any(x => x.Key != guid))
            {
                throw new Exception("Cannot find any routes by that Guid, EvacuationViewModel -> GetRoute");
            }

            var route = Routes.Single(x => x.Key == guid).Value;

            return route;
        }

        public void ClearRoutes()
        {
            
        }
    }
}
