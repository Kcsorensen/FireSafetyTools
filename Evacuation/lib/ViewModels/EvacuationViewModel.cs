using Evacuation.lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Evacuation.lib.ViewModels
{
    public class EvacuationViewModel
    {
        public Dictionary<string, List<Guid>> Routes { get; set; }
        public List<BaseRouteElement> RouteElements { get; set; }

        public EvacuationViewModel()
        {
            Initiate();
        }

        public void Initiate()
        {
            Routes = new Dictionary<string, List<Guid>>();
            RouteElements = new List<BaseRouteElement>();
        }

        public void AddRouteElement(BaseRouteElement routeelement)
        {
            RouteElements.Add(routeelement);
        }

        public void CreateRoute(string routeName)
        {
            if (Routes.Any(x => x.Key == routeName))
            {
                throw new Exception("Input parameter routeName cannot already exist in the Dictionary, EvacuationViewModel -> CreateRoute");
            }

            Routes.Add(routeName, new List<Guid>());
        }

        public void AddRouteElementToRoute(string routeName, BaseRouteElement thisRouteElement, BaseRouteElement toRouteElement)
        {
            if (Routes.Count == 0)
            {
                throw new Exception("Cannot add routeElement to Route, if there is no routes");
            }

            // NullExceptionCheck is implemented in Single()
            var route = Routes.Single(x => x.Key == routeName);

            if (route.Value.Any(x => x == thisRouteElement.Guid) && route.Value.Any(x => x == toRouteElement.Guid))
            {
                throw new Exception("Cannot add a RouteElement in a route to a another RouteElement in the same route");
            }

            if (route.Value.Any(x => x != toRouteElement.Guid))
            {
                route.Value.Add(toRouteElement.Guid);
                route.Value.Add(thisRouteElement.Guid);
            }
            else
            {
                route.Value.Add(thisRouteElement.Guid);
            }
        }
    }
}
