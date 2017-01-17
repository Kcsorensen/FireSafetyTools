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

            var routeName = "Evacaution from 9th floor";
            var initialNumberOfPeople = 150;

            viewModel.CreateRoute(routeName, initialNumberOfPeople);

            var corridor = new Corridor()
            {
                Name = "Corridor on 9th Floor",
                NumberOfPeople = 150,
                Width = 2.44,
                Length = 45.7
            };

            viewModel.AddRouteElementToRoute(routeName, corridor);

            var door = new Door()
            {
                Name = "Door on 9th Floor",
                NumberOfPeople = 150,
                Width = 0.91
            };

            viewModel.AddRouteElementToRoute(routeName, door);

            var stairway = new Stairway(StairwayTypes.Rise180xTread280)
            {
                Name = "Stairway on 9th Floor",
                NumberOfPeople = 150,
                Width = 1.12,
                Length = 11.6,
            };

            viewModel.AddRouteElementToRoute(routeName, stairway);

            // Act

            var calculator = new Calculator();

            var updatedRoute = calculator.CalculateRoute(viewModel.GetRoute(routeName));

            var updatedCorridor = ((Corridor)updatedRoute.Single(x => x.RouteType == RouteTypes.Corridor));
            var updatedDoor = ((Door)updatedRoute.Single(x => x.RouteType == RouteTypes.Door));
            var updatedStairway = ((Stairway)updatedRoute.Single(x => x.RouteType == RouteTypes.Stairway));

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
