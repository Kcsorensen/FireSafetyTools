using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireSafetyTools.ViewModels.Tools.FireSafety.DesignFire;

namespace FireSafetyTools.Models.Tools.FireSafety.DesignFire
{
    public class PhaseCalculator
    {
        private readonly int _stepsGrowthPhase;
        private readonly int _stepsSteadyPhase;
        private readonly int _stepsDecayPhase;

        public PhaseCalculator()
        {
            // This is the intermediate steps and last step. First is beside these steps.
            // First step is the previous phase last step, which is not applied again in this phase.
            // Only the intermediate steps and the last step in this phase is applied to the PhaseDataPoints.
            // The stepsize is then, stepsize = (duration / intermediate steps)
            _stepsGrowthPhase = 10;
            _stepsSteadyPhase = 0;
            _stepsDecayPhase = 10;
        }

        public Phase Calculate(PhaseFormViewModel phaseViewModel, State state)
        {
            var updatedPhase = new Phase();

            if (state.PhaseTypeId == PhaseType.GrowthKnownDurationAndGrowthRate)
            {
                updatedPhase.Duration = phaseViewModel.Duration;
                updatedPhase.GrowthRateFactor = phaseViewModel.GrowthRateFactor;
                updatedPhase.InitialXt = state.LatestXt;
                updatedPhase.InitialYq = state.LatestYq;

                updatedPhase.TargetXt = updatedPhase.InitialXt + updatedPhase.Duration;
                updatedPhase.TargetYq = updatedPhase.InitialYq + updatedPhase.GrowthRateFactor * Math.Pow(updatedPhase.Duration, 2);

                updatedPhase.TotalEnergyReleased = (1.0 / 3.0) * updatedPhase.GrowthRateFactor *
                                                   Math.Round(updatedPhase.Duration, 3) +
                                                   updatedPhase.InitialYq * updatedPhase.Duration;

                updatedPhase.PhaseDataPoints = new List<DataPoint>();

                var stepsize = updatedPhase.Duration / (_stepsGrowthPhase);

                // Generate Datapoints for chart
                for (int i = 1; i < (_stepsGrowthPhase + 1); i++)
                {
                    var newTime = updatedPhase.InitialXt + stepsize * i;
                    var newEffect = updatedPhase.InitialYq + updatedPhase.GrowthRateFactor * Math.Pow((newTime - updatedPhase.InitialXt), 2);

                    var newDataPoint = new DataPoint { Id = updatedPhase.Id, Time = newTime, Effect = newEffect };

                    updatedPhase.PhaseDataPoints.Add(newDataPoint);
                }
            }

            return updatedPhase;
        }
    }
}
