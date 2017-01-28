using Evacuation.lib.Models;
using Evacuation.lib.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Evacuation
{
    [TestClass]
    public class SfpeExamples
    {
        [TestMethod]
        public void OfficeBuilding()
        {
            // Arrange
            var resultCorridorCalculatedFlow = 2.4672;
            var resultDoorCalculatedFlow = 0.793;
            var resultStairwayDensity = 1.471;

            var viewModel = new EvacuationViewModel();
            viewModel.Initiate();

            var routeViewModel = new CreateRouteViewModel()
            {
                Name = "Evacaution from 9th floor",
                NumberOfPeople = 150
            };

            viewModel.CreateRoute(routeViewModel);

            var corridorViewModel = new CreateRouteElementViewModel()
            {
                RouteId = viewModel.Routes.First().Key,
                RouteTypeId = RouteTypeHelper.Corridor,
                Name = "Corridor on 9th Floor",
                NumberOfPeople = 150,
                Width = 2.44,
                Distance = (15.2 + 45.7) / 2,
                Density = 150 / (45.7 * 2.44)
            };

            viewModel.AddRouteElementToRoute(corridorViewModel);

            var doorViewModel = new CreateRouteElementViewModel()
            {
                RouteId = viewModel.Routes.First().Key,
                RouteTypeId = RouteTypeHelper.Door,
                Name = "Door on 9th Floor",
                NumberOfPeople = 150,
                Width = 0.91
            };

            viewModel.AddRouteElementToRoute(doorViewModel);

            var stairwayViewModel = new CreateRouteElementViewModel()
            {
                RouteId = viewModel.Routes.First().Key,
                RouteTypeId = RouteTypeHelper.Stairway,
                Name = "Stairway on 9th Floor",
                NumberOfPeople = 150,
                Width = 1.12,
                Distance = 11.6,
                StairwayType = StairwayTypes.Rise180xTread280
            };

            viewModel.AddRouteElementToRoute(stairwayViewModel);

            // Act
            var calculator = new Calculator();

            var updatedRoute = calculator.CalculateRoute(viewModel.GetRoute(viewModel.Routes.First().Key));

            var updatedCorridor = ((Corridor)updatedRoute.Single(x => x.RouteTypeId == RouteTypeHelper.Corridor));
            var updatedDoor = ((Door)updatedRoute.Single(x => x.RouteTypeId == RouteTypeHelper.Door));
            var updatedStairway = ((Stairway)updatedRoute.Single(x => x.RouteTypeId == RouteTypeHelper.Stairway));

            // Assert

            var actualResultCorridor = Math.Round(updatedCorridor.CalculatedFlow, 4);
            var actualResultDoor = Math.Round(updatedDoor.CalculatedFlow, 3);
            var actualResultStairway = Math.Round(updatedStairway.Density, 3);

            Assert.AreEqual(resultCorridorCalculatedFlow, actualResultCorridor);
            Assert.AreEqual(resultDoorCalculatedFlow, actualResultDoor);
            Assert.AreEqual(resultStairwayDensity, actualResultStairway);

        }
    }
}
