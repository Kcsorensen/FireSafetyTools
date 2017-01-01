using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DesignFire.Test
{
    [TestClass]
    public class ListToJsonTest
    {
        [TestMethod]
        public void ListToJsonString()
        {
            // Arrange

            var List = new List<DataPoint>()
            {
                new DataPoint() {Time = 0, Effect = 0},
                new DataPoint() {Time = 10, Effect = 5},
                new DataPoint() {Time = 20, Effect = 15},
                new DataPoint() {Time = 30, Effect = 35},
            };

            // Act
            var JsonString = JsonConvert.SerializeObject(List);

            JArray JsonObject = JArray.Parse(JsonString);

            // Assert
        }
    }
}
