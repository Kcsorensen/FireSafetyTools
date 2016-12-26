using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmokeUnitConverter.Test
{
    [TestClass]
    public class SmokeUnitConverterTest
    {
        private readonly SmokeData _defaultSmokeData = new SmokeData()
        {
            RateOfHeatRelease = 1000,
            Hmat = 14000,
            Hair = 3000,
            Pod = 8700,
            Rho0 = 1.205
        };

        [TestMethod]
        public void ConvertFromD0()
        {
            // Arrange
            double ExpectedS = 71.43;
            double ExpectedS0 = 258.2;
            double ExpectedYs = 0.02647;

            var smokeDataWithD0 = _defaultSmokeData;
            smokeDataWithD0.D010Log = 1.0;

            // Act
            SmokeData actualS0 = new SmokeUnitConverter()
                .TrimAndCalculate(SmokeUnit.SmokePotentialFuel, SmokeUnit.SmokePotentialArgos, smokeDataWithD0);

            SmokeData actualYs = new SmokeUnitConverter()
                .TrimAndCalculate(SmokeUnit.SmokePotentialFuel, SmokeUnit.SootYield, smokeDataWithD0);

            SmokeData actualS = new SmokeUnitConverter()
                .TrimAndCalculate(SmokeUnit.SmokePotentialFuel, SmokeUnit.SmokeProduction, smokeDataWithD0);

            // Assert
            Assert.AreEqual(ExpectedS0, Math.Round(actualS0.S0, 2));
            Assert.AreEqual(ExpectedYs, Math.Round(actualYs.Ys, 5));
            Assert.AreEqual(ExpectedS, Math.Round(actualS.S, 2));
        }

        [TestMethod]
        public void ConvertFromS0()
        {
            // Arrange
            double ExpectedS = 27.66;
            double ExpectedD0 = 0.39;
            double ExpectedYs = 0.01025;

            var smokeDataWithD0 = _defaultSmokeData;
            smokeDataWithD0.S0 = 100.0;

            // Act
            SmokeData actualD0 = new SmokeUnitConverter()
                .TrimAndCalculate(SmokeUnit.SmokePotentialArgos, SmokeUnit.SmokePotentialFuel, smokeDataWithD0);

            SmokeData actualYs = new SmokeUnitConverter()
                .TrimAndCalculate(SmokeUnit.SmokePotentialArgos, SmokeUnit.SootYield, smokeDataWithD0);

            SmokeData actualS = new SmokeUnitConverter()
                .TrimAndCalculate(SmokeUnit.SmokePotentialArgos, SmokeUnit.SmokeProduction, smokeDataWithD0);

            // Assert
            Assert.AreEqual(ExpectedD0, Math.Round(actualD0.D010Log, 2));
            Assert.AreEqual(ExpectedYs, Math.Round(actualYs.Ys, 5));
            Assert.AreEqual(ExpectedS, Math.Round(actualS.S, 2));
        }

        [TestMethod]
        public void ConvertFromS()
        {
            // Arrange
            double ExpectedS0 = 180.8;
            double ExpectedD0 = 0.70;
            double ExpectedYs = 0.01853;

            var smokeDataWithD0 = _defaultSmokeData;
            smokeDataWithD0.S = 50.0;

            // Act
            SmokeData actualD0 = new SmokeUnitConverter()
                .TrimAndCalculate(SmokeUnit.SmokeProduction, SmokeUnit.SmokePotentialFuel, smokeDataWithD0);

            SmokeData actualYs = new SmokeUnitConverter()
                .TrimAndCalculate(SmokeUnit.SmokeProduction, SmokeUnit.SootYield, smokeDataWithD0);

            SmokeData actualS0 = new SmokeUnitConverter()
                .TrimAndCalculate(SmokeUnit.SmokeProduction, SmokeUnit.SmokePotentialArgos, smokeDataWithD0);

            // Assert
            Assert.AreEqual(ExpectedD0, Math.Round(actualD0.D010Log, 2));
            Assert.AreEqual(ExpectedYs, Math.Round(actualYs.Ys, 5));
            Assert.AreEqual(ExpectedS0, Math.Round(actualS0.S0, 2));
        }

        [TestMethod]
        public void ConvertFromYs()
        {
            // Arrange
            double ExpectedS0 = 377.6;
            double ExpectedD0 = 1.46;
            double ExpectedS = 104.44;

            var smokeDataWithD0 = _defaultSmokeData;
            smokeDataWithD0.Ys = 0.0387;

            // Act
            SmokeData actualD0 = new SmokeUnitConverter()
                .TrimAndCalculate(SmokeUnit.SootYield, SmokeUnit.SmokePotentialFuel, smokeDataWithD0);

            SmokeData actualS = new SmokeUnitConverter()
                .TrimAndCalculate(SmokeUnit.SootYield, SmokeUnit.SmokeProduction, smokeDataWithD0);

            SmokeData actualS0 = new SmokeUnitConverter()
                .TrimAndCalculate(SmokeUnit.SootYield, SmokeUnit.SmokePotentialArgos, smokeDataWithD0);

            // Assert
            Assert.AreEqual(ExpectedD0, Math.Round(actualD0.D010Log, 2));
            Assert.AreEqual(ExpectedS, Math.Round(actualS.S, 2));
            Assert.AreEqual(ExpectedS0, Math.Round(actualS0.S0, 2));
        }
    }
}
