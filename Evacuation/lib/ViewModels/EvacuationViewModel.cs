using Evacuation.lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Evacuation.lib.ViewModels
{
    public class EvacuationViewModel
    {
        public Dictionary<string, List<BaseRouteElement>> Routes { get; set; }
        public List<BaseRouteElement> RouteElements { get; set; }

        public EvacuationViewModel()
        {
            Initiate();
        }

        public void Initiate()
        {
            Routes = new Dictionary<string, List<BaseRouteElement>>();
            RouteElements = new List<BaseRouteElement>();
        }

        public void AddRouteElement(BaseRouteElement routeelement)
        {
            RouteElements.Add(routeelement);
        }

        public void CreateRoute(string routeName, double numberOfPeople)
        {
            if (Routes.Any(x => x.Key == routeName))
            {
                throw new Exception("Input parameter routeName cannot already exist in the Dictionary, EvacuationViewModel -> CreateRoute");
            }

            Routes.Add(routeName, new List<BaseRouteElement>()
            {
                new RouteStart()
                {
                    Name = routeName,
                    NumberOfPeople = numberOfPeople,
                    TransitionType = TransitionTypes.RouteStartElement
                }
            });
        }

        public void AddRouteElementToRoute(string routeName, BaseRouteElement routeElement)
        {
            if (Routes.Count == 0)
            {
                throw new Exception("Cannot add routeElement to Route, if there is no routes, EvacuationViewModel -> AddRouteElementToRoute");
            }

            // NullExceptionCheck is implemented in Single()
            var route = Routes.Single(x => x.Key == routeName).Value;

            if (route.Count == 1)
            {
                routeElement.TransitionType = TransitionTypes.RouteStartElement;
            }

            if (route.Any(x => x.Guid == routeElement.Guid))
            {
                throw new Exception("Cannot add a RouteElement in a route to the same route, EvacuationViewModel -> AddRouteElementToRoute");
            }

            route.Add(routeElement);
        }

        public List<BaseRouteElement> GetRoute(string routeName)
        {
            if (Routes.Any(x => x.Key != routeName))
            {
                throw new Exception("Cannot find any routes by that name, EvacuationViewModel -> GetRoute");
            }

            var route = Routes.Single(x => x.Key == routeName).Value;

            return route;
        }
    }
}
