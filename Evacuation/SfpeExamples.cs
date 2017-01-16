using Evacuation.lib.Models;
using Evacuation.lib.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Evacuation
{
    [TestClass]
    public class SfpeExamples
    {
        [TestMethod]
        public void OfficeBuilding()
        {
            // Arrange
            var viewModel = new EvacuationViewModel();

            var routeName = "Evacaution from 9th floor";

            viewModel.CreateRoute(routeName);

            var room = new Room()
            {
                Name = "9th floor",
                NumberOfPeople = 300,
            };

            viewModel.AddRouteElement(room);

            var corridor = new Corridor()
            {
                Name = "Corridor on 9th Floor",
                NumberOfPeople = 150,
                Width = 2.44,
                Length = 45.7
            };

            var calculator = new Calculator();

            // Act

            // Assert
        }
    }
}
