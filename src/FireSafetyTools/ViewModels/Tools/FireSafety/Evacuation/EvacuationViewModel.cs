using System;
using System.Collections.Generic;
using System.Linq;
using FireSafetyTools.Models.Tools.FireSafety.Evacuation;

namespace FireSafetyTools.ViewModels.Tools.FireSafety.Evacuation
{
    public class EvacuationViewModel
    {
        public Dictionary<int, List<BaseRouteElement>> Routes { get; set; }

        public void Initiate() 
        {
            Routes = new Dictionary<int, List<BaseRouteElement>>();

            var routeViewModel = new CreateRouteViewModel()
            {
                Name = "Evacuation from 9th floor"
            };

            CreateRoute(routeViewModel);

            var corridor = new CreateRouteElementViewModel()
            {
                RouteId = Routes.First().Key,
                RouteTypeId = RouteTypeHelper.Corridor,
                Name = "9th floor Corridor",
                Width = 2.44,
                Distance = (15.2 + 45.7) / 2,
                NumberOfPeople = 150,
                Density = 150 / (2.44 * 45.7)
            };

            AddRouteElementToRoute(corridor);

            var door = new CreateRouteElementViewModel()
            {
                RouteId = Routes.First().Key,
                RouteTypeId = RouteTypeHelper.Door,
                Name = "9th floor Door",
                Width = 0.91,
                NumberOfPeople = 150,
            };

            AddRouteElementToRoute(door);

            var stairway = new CreateRouteElementViewModel()
            {
                RouteId = Routes.First().Key,
                RouteTypeId = RouteTypeHelper.Stairway,
                Name = "9th floor Stairway",
                Width = 1.12,
                Distance = 11.6,
                NumberOfPeople = 150,
                StairwayType = StairwayTypes.Rise180xTread280
            };

            AddRouteElementToRoute(stairway);
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

            if (viewModel.RouteTypeId == RouteTypeHelper.Door)
            {
                var routeElement = new Door()
                {
                    Name = viewModel.Name,
                    Width = viewModel.Width,
                    Density = viewModel.Density,
                    NumberOfPeople = viewModel.NumberOfPeople,
                    TransitionType = transitionType,
                    RouteElementId = route.Count
                };

                route.Add(routeElement);
            }

            if (viewModel.RouteTypeId == RouteTypeHelper.Stairway)
            {
                var routeElement = new Stairway(viewModel.StairwayType)
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

            if (viewModel.RouteTypeId == RouteTypeHelper.WideConcourse)
            {
                var routeElement = new Room()
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

        public void CalculateRoutes()
        {
            var calculator = new Calculator();

            foreach (var route in Routes)
            {
                var updatedRouteElements = calculator.CalculateRoute(route.Value);

                route.Value.Clear();

                route.Value.AddRange(updatedRouteElements);
            }
        }

        public void ClearRoutes()
        {
            Routes.Clear();
        }
    }
}
