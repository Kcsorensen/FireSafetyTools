using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignFire.Test
{
    [TestClass]
    public class CalculatePhasesTest
    {
        [TestMethod]
        public void GrowthPhaseDurationAndGrowthRate()
        {
            // Arrange
            var state = new State
            {
                LatestXt = 0,
                LatestYq = 0,
                PhasesCount = 0,
                PhaseTypeId = PhaseType.GrowthKnownDurationAndGrowthRate,
                Name = ""
            };

            var viewModel = new PhaseFormViewModel
            {
                Duration = 100,
                GrowthRateFactor = 0.047,
                PhaseTypeId = PhaseType.GrowthKnownDurationAndGrowthRate
            };

            var Result = 470;

            // Act
            var phaseCalculator = new Calculator();

            var growthPhase = phaseCalculator.GeneratePhase(viewModel, state);

            // Assert
            Assert.AreEqual(growthPhase.TargetYq, Result);
        }

        [TestMethod]
        public void GrowthPhaseDurationAndTargetEffect()
        {
            // Arrange
            var state = new State
            {
                LatestXt = 0,
                LatestYq = 0,
                PhasesCount = 0,
                PhaseTypeId = PhaseType.GrowthKnownDurationAndTargetEffect,
                Name = ""
            };

            var viewModel = new PhaseFormViewModel
            {
                Duration = 100,
                TargetYq = 470,
                PhaseTypeId = PhaseType.GrowthKnownDurationAndTargetEffect
            };

            var Result = 0.047;

            // Act
            var phaseCalculator = new Calculator();

            var growthPhase = phaseCalculator.GeneratePhase(viewModel, state);

            // Assert
            Assert.AreEqual(growthPhase.GrowthRateFactor, Result);
        }

        [TestMethod]
        public void GrowthPhaseTargetEffectAndGrowthRate()
        {
            // Arrange
            var state = new State
            {
                LatestXt = 0,
                LatestYq = 0,
                PhasesCount = 0,
                PhaseTypeId = PhaseType.GrowthKnownTargetEffectAndGrowthRate,
                Name = ""
            };

            var viewModel = new PhaseFormViewModel
            {
                GrowthRateFactor = 0.047,
                TargetYq = 470,
                PhaseTypeId = PhaseType.GrowthKnownTargetEffectAndGrowthRate
            };

            var ResultDuration = 100;
            var ResultXt = state.LatestXt + ResultDuration;

            // Act
            var phaseCalculator = new Calculator();

            var growthPhase = phaseCalculator.GeneratePhase(viewModel, state);

            // Assert
            Assert.AreEqual(ResultDuration, growthPhase.Duration);
            Assert.AreEqual(ResultXt, growthPhase.TargetXt);
        }

        [TestMethod]
        public void SteadyPhaseDuration()
        {
            // Arrange
            var state = new State
            {
                LatestXt = 100,
                LatestYq = 525,
                PhasesCount = 0,
                PhaseTypeId = PhaseType.SteadyKnownDuration,
                Name = ""
            };

            var viewModel = new PhaseFormViewModel
            {
                GrowthRateFactor = 0,
                TargetYq = 0,
                Duration = 150,
                PhaseTypeId = PhaseType.SteadyKnownDuration
            };

            var ResultXt = 100 + 150;
            var ResultYq = 525;
            

            // Act
            var phaseCalculator = new Calculator();

            var steadyPhase = phaseCalculator.GeneratePhase(viewModel, state);

            // Assert
            Assert.AreEqual(ResultXt, steadyPhase.TargetXt);
            Assert.AreEqual(ResultYq, steadyPhase.TargetYq);
        }

        [TestMethod]
        public void DecayPhaseDurationAndGrowthRate()
        {
            // Arrange
            var state = new State
            {
                LatestXt = 100,
                LatestYq = 1000,
                PhasesCount = 0,
                PhaseTypeId = PhaseType.DecayKnownDurationAndGrowthRate,
                Name = ""
            };

            var viewModel = new PhaseFormViewModel
            {
                GrowthRateFactor = 0.047,
                Duration = 100,
                PhaseTypeId = PhaseType.DecayKnownDurationAndGrowthRate
            };

            var ResultXt = 100 + 100;
            var ResultYq = 530;


            // Act
            var phaseCalculator = new Calculator();

            var decayPhase = phaseCalculator.GeneratePhase(viewModel, state);

            // Assert
            Assert.AreEqual(ResultXt, decayPhase.TargetXt);
            Assert.AreEqual(ResultYq, decayPhase.TargetYq);
        }

        [TestMethod]
        public void DecayPhaseDurationAndTargetEffect()
        {
            // Arrange
            var state = new State
            {
                LatestXt = 100,
                LatestYq = 1000,
                PhasesCount = 0,
                PhaseTypeId = PhaseType.DecayKnownDurationAndTargetEffect,
                Name = ""
            };

            var viewModel = new PhaseFormViewModel
            {
                Duration = 100,
                TargetYq = 530,
                PhaseTypeId = PhaseType.DecayKnownDurationAndTargetEffect
            };

            var ResultXt = state.LatestXt + viewModel.Duration;
            var ResultGrowthRate = 0.047;


            // Act
            var phaseCalculator = new Calculator();

            var decayPhase = phaseCalculator.GeneratePhase(viewModel, state);

            // Assert
            Assert.AreEqual(ResultXt, decayPhase.TargetXt);
            Assert.AreEqual(ResultGrowthRate, decayPhase.GrowthRateFactor);
        }

        [TestMethod]
        public void DecayPhaseTargetEffectAndGrowthRate()
        {
            // Arrange
            var state = new State
            {
                LatestXt = 100,
                LatestYq = 1000,
                PhasesCount = 0,
                PhaseTypeId = PhaseType.DecayKnownTargetEffectAndGrowthRate,
                Name = ""
            };

            var viewModel = new PhaseFormViewModel
            {
                GrowthRateFactor = 0.047,
                TargetYq = 530,
                PhaseTypeId = PhaseType.DecayKnownTargetEffectAndGrowthRate
            };

            var ResultDuration = 100;
            var ResultXt = state.LatestXt + ResultDuration;

            // Act
            var phaseCalculator = new Calculator();

            var decayPhase = phaseCalculator.GeneratePhase(viewModel, state);

            // Assert
            //Assert.AreEqual(ResultXt, decayPhase.TargetXt);
            Assert.AreEqual(ResultDuration, decayPhase.Duration);
        }
    }
}
