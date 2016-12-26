using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace JsonSerializerTest
{
    [TestClass]
    public class JsonSerializing
    {
        [TestMethod]
        public void SimpleListToJson()
        {
            // Arrange
            List<SelectListItem> SelectListItems = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "Smoke Potential Argos", Value = "0"},
                new SelectListItem() {Text = "Smoke Potential Fuel", Value = "1"},
                new SelectListItem() {Text = "Smoke Production", Value = "2"},
                new SelectListItem() {Text = "Soot yield", Value = "3"}
            };

            // Act

            var serialize = JsonConvert.SerializeObject(SelectListItems);

            var deserialize = JsonConvert.DeserializeObject<List<SelectListItem>>(serialize);

            // Assert

            Assert.AreEqual(SelectListItems.Count, deserialize.Count);


        }

        [TestMethod]
        public void ListInClassToJson()
        {
            // Arrange
            var classWithList = new SimpleSmokeData();

            // Act

            var serialize = JsonConvert.SerializeObject(classWithList);

            var deserialize = JsonConvert.DeserializeObject<SimpleSmokeData>(serialize);

            // Assert

            Assert.AreEqual(classWithList.SelectListItems.Count, deserialize.SelectListItems.Count);
        }

        [TestMethod]
        public void RealListInClassToJson()
        {
            // Arrange
            var smokeDataViewModel = new SmokeDataViewModel();
            smokeDataViewModel.Initiate();

            var smokeData = new SmokeData
            {
                RateOfHeatRelease = 500.0,
                Hmat = 14000.0
            };

            smokeDataViewModel.UpdateSmokeData(smokeData);

            // Act
            var serialize = JsonConvert.SerializeObject(smokeDataViewModel);

            var deserialize = JsonConvert.DeserializeObject<SmokeDataViewModel>(serialize);

            // Assert,
            Assert.AreEqual(smokeDataViewModel.SelectListItems.Count, deserialize.SelectListItems.Count);
            Assert.AreEqual(smokeDataViewModel._smokeData.RateOfHeatRelease, deserialize._smokeData.RateOfHeatRelease);
            Assert.AreEqual(smokeDataViewModel._smokeData.Hmat, deserialize._smokeData.Hmat);
        }
    }
}
