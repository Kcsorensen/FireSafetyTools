using System.Collections.Generic;
using System.Linq;
using DesignFire.Test.lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignFire.Test
{
    [TestClass]
    public class CreateDataPointsForChartTest
    {
        [TestMethod]
        public void OneOfEachPhase()
        {
            // Arrange
            var resultGrowthTargetYq = 470.00;
            var resultGrowhtDataPointCount = 10;
            var resultGrowthTotalEnergy = 15.67;
            var resultSteadyTargetXt = 200;
            var resultSteadyTargetYq = 470.00;
            var resultSteadyDataPointCount = 1;
            var resultSteadyTotalEnergy = 47.00;
            var resultDecayTargetXt = 300;
            var resultDecayTargetYq = 0;
            var resultDecayDataPointCount = 10;
            var resultDecayTotalEnergy = 15.67;
            var resultCompleteListOfDataPoints = new List<DataPoint>()
            {
                new DataPoint() {Id = 1, Time = 0, Effect = 0},
                new DataPoint() {Id = 1, Time = 10, Effect = 4.70},
                new DataPoint() {Id = 1, Time = 20, Effect = 18.80},
                new DataPoint() {Id = 1, Time = 30, Effect = 42.30},
                new DataPoint() {Id = 1, Time = 40, Effect = 75.20},
                new DataPoint() {Id = 1, Time = 50, Effect = 117.50},
                new DataPoint() {Id = 1, Time = 60, Effect = 169.20},
                new DataPoint() {Id = 1, Time = 70, Effect = 230.30},
                new DataPoint() {Id = 1, Time = 80, Effect = 300.80},
                new DataPoint() {Id = 1, Time = 90, Effect = 380.70},
                new DataPoint() {Id = 1, Time = 100, Effect = 470.00},
                new DataPoint() {Id = 1, Time = 200, Effect = 470.00},
                new DataPoint() {Id = 1, Time = 210, Effect = 380.70},
                new DataPoint() {Id = 1, Time = 220, Effect = 300.80},
                new DataPoint() {Id = 1, Time = 230, Effect = 230.30},
                new DataPoint() {Id = 1, Time = 240, Effect = 169.20},
                new DataPoint() {Id = 1, Time = 250, Effect = 117.50},
                new DataPoint() {Id = 1, Time = 260, Effect = 75.20},
                new DataPoint() {Id = 1, Time = 270, Effect = 42.30},
                new DataPoint() {Id = 1, Time = 280, Effect = 18.80},
                new DataPoint() {Id = 1, Time = 290, Effect = 4.70},
                new DataPoint() {Id = 1, Time = 300, Effect = 0},
            };

            var stateGrowth = new State()
            {
                LatestXt = 0,
                LatestYq = 0,
                PhaseTypeId = PhaseType.GrowthKnownDurationAndGrowthRate
            };

            var growthFormViewModel = new PhaseFormViewModel()
            {
                Duration = 100,
                GrowthRateFactor = 0.047
            };

            var chartDatapoints = new List<DataPoint>()
            {
                new DataPoint() {Id = 1, Time = 0, Effect = 0}
            };

            var calculator = new Calculator();

            // Act

            var growthPhase = calculator.GeneratePhase(growthFormViewModel, stateGrowth);

            chartDatapoints.AddRange(growthPhase.GetDataPoints());

            var stateSteady = new State()
            {
                LatestXt = growthPhase.TargetXt,
                LatestYq = growthPhase.TargetYq,
                PhaseTypeId = PhaseType.SteadyKnownDuration
            };

            var steadyFormViewModel = new PhaseFormViewModel()
            {
                Duration = 100,
            };

            var steadyPhase = calculator.GeneratePhase(steadyFormViewModel, stateSteady);

            chartDatapoints.AddRange(steadyPhase.GetDataPoints());

            var stateDecay = new State()
            {
                LatestXt = steadyPhase.TargetXt,
                LatestYq = steadyPhase.TargetYq,
                PhaseTypeId = PhaseType.DecayKnownDurationAndGrowthRate
            };

            var decayFormViewModel = new PhaseFormViewModel()
            {
                Duration = 100,
                GrowthRateFactor = 0.047
            };

            var decayPhase = calculator.GeneratePhase(decayFormViewModel, stateDecay);

            chartDatapoints.AddRange(decayPhase.GetDataPoints());

            // Assert
            Assert.AreEqual(growthPhase.TargetYq, resultGrowthTargetYq);
            Assert.AreEqual(growthPhase.PhaseDataPoints.Count, resultGrowhtDataPointCount);
            Assert.AreEqual(growthPhase.TotalEnergyReleased, resultGrowthTotalEnergy);
            Assert.AreEqual(steadyPhase.TargetXt, resultSteadyTargetXt);
            Assert.AreEqual(steadyPhase.TargetYq, resultSteadyTargetYq);
            Assert.AreEqual(steadyPhase.PhaseDataPoints.Count, resultSteadyDataPointCount);
            Assert.AreEqual(steadyPhase.TotalEnergyReleased, resultSteadyTotalEnergy);
            Assert.AreEqual(decayPhase.TargetXt, resultDecayTargetXt);
            Assert.AreEqual(decayPhase.TargetYq, resultDecayTargetYq);
            Assert.AreEqual(decayPhase.PhaseDataPoints.Count, resultDecayDataPointCount);
            Assert.AreEqual(decayPhase.TotalEnergyReleased, resultDecayTotalEnergy);

            foreach (var actualDataPoint in chartDatapoints)
            {
                var time = actualDataPoint.Time;

                Assert.IsTrue(resultCompleteListOfDataPoints.Any(x => x.Time == time));

                var expectedDataPoint = resultCompleteListOfDataPoints.Single(x => x.Time == time);

                Assert.AreEqual(actualDataPoint.Effect, expectedDataPoint.Effect);
            }
        }
    }
}
