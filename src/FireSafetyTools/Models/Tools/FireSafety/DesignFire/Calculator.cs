using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireSafetyTools.ViewModels.Tools.FireSafety.DesignFire;

namespace FireSafetyTools.Models.Tools.FireSafety.DesignFire
{
    public class Calculator
    {
        private readonly int _stepsGrowthPhase;
        private readonly int _stepsSteadyPhase;
        private readonly int _stepsDecayPhase;

        public Calculator()
        {
            // This is the intermediate steps and last step. First is beside these steps.
            // First step is the previous phase last step, which is not applied again in this phase.
            // Only the intermediate steps and the last step in this phase is applied to the PhaseDataPoints.
            // The stepsize is then, stepsize = (duration / intermediate steps)
            _stepsGrowthPhase = 10;
            _stepsSteadyPhase = 0;
            _stepsDecayPhase = 10;
        }

        public Phase GeneratePhase(PhaseFormViewModel phaseFormViewModel, State state)
        {
            var updatedPhase = new Phase();

            updatedPhase.Id = state.PhasesCount + 1;
            updatedPhase.Name = state.Name;
            updatedPhase.InitialXt = state.LatestXt;
            updatedPhase.InitialYq = state.LatestYq;
            updatedPhase.PhaseDataPoints = new List<DataPoint>();

            if (state.PhaseTypeId == PhaseType.GrowthKnownDurationAndGrowthRate)
            {
                updatedPhase.Duration = phaseFormViewModel.Duration;
                updatedPhase.GrowthRateFactor = phaseFormViewModel.GrowthRateFactor;

                updatedPhase.TargetXt = updatedPhase.InitialXt + updatedPhase.Duration;
                updatedPhase.TargetYq = updatedPhase.InitialYq + updatedPhase.GrowthRateFactor * Math.Pow(updatedPhase.Duration, 2);

                var stepsize = updatedPhase.Duration / (_stepsGrowthPhase);

                // Generate Datapoints for chart
                for (int i = 1; i < (_stepsGrowthPhase + 1); i++)
                {
                    var newTime = updatedPhase.InitialXt + stepsize * i;
                    var newEffect = Math.Round((updatedPhase.InitialYq + updatedPhase.GrowthRateFactor * Math.Pow((newTime - updatedPhase.InitialXt), 2)),2) ;

                    var newDataPoint = new DataPoint { Id = updatedPhase.Id, Time = newTime, Effect = newEffect };

                    updatedPhase.PhaseDataPoints.Add(newDataPoint);
                }
            }

            if (state.PhaseTypeId == PhaseType.GrowthKnownDurationAndTargetEffect)
            {
                updatedPhase.Duration = phaseFormViewModel.Duration;
                updatedPhase.TargetYq = phaseFormViewModel.TargetYq;

                updatedPhase.GrowthRateFactor = Math.Round((updatedPhase.TargetYq - updatedPhase.InitialYq) /
                                                Math.Pow(updatedPhase.Duration, 2), 4);
                updatedPhase.TargetXt = updatedPhase.InitialXt + updatedPhase.Duration;

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

            if (state.PhaseTypeId == PhaseType.GrowthKnownTargetEffectAndGrowthRate)
            {
                updatedPhase.TargetYq = phaseFormViewModel.TargetYq;
                updatedPhase.GrowthRateFactor = phaseFormViewModel.GrowthRateFactor;

                updatedPhase.Duration = Math.Round((Math.Sqrt((updatedPhase.TargetYq - updatedPhase.InitialYq) / updatedPhase.GrowthRateFactor)), 1);
                updatedPhase.TargetXt = updatedPhase.InitialXt + updatedPhase.Duration;

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

            updatedPhase.TotalEnergyReleased = Math.Round((((1.0 / 3.0) * updatedPhase.GrowthRateFactor *
                                   Math.Pow(updatedPhase.Duration, 3) +
                                   updatedPhase.InitialYq * updatedPhase.Duration) / 1000.0), 2);

            return updatedPhase;
        }
    }
}
