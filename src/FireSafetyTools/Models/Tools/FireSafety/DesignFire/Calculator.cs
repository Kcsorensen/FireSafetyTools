﻿using System;
using System.Collections.Generic;
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
            _stepsSteadyPhase = 1;
            _stepsDecayPhase = 10;
        }

        public Phase GeneratePhase(PhaseFormViewModel phaseFormViewModel, State state)
        {
            var updatedPhase = new Phase();

            updatedPhase.Id = state.PhasesCount + 1;
            updatedPhase.PhaseTypeId = phaseFormViewModel.PhaseTypeId;
            updatedPhase.Name = PhaseTypeHelper.GetPhaseTypeName(updatedPhase.PhaseTypeId);
            updatedPhase.InitialXt = state.LatestXt;
            updatedPhase.InitialYq = state.LatestYq;
            updatedPhase.PhaseDataPoints = new List<DataPoint>();

            #region Growth Phase

            if (updatedPhase.PhaseTypeId == PhaseTypeHelper.GrowthKnownDurationAndGrowthRate)
            {
                updatedPhase.Duration = phaseFormViewModel.Duration;
                updatedPhase.GrowthRateFactor = phaseFormViewModel.GrowthRateFactor;

                updatedPhase.TargetXt = updatedPhase.InitialXt + updatedPhase.Duration;
                updatedPhase.TargetYq = updatedPhase.InitialYq +
                                        updatedPhase.GrowthRateFactor * Math.Pow(updatedPhase.Duration, 2);

                var stepsize = updatedPhase.Duration / (_stepsGrowthPhase);

                // Generate Datapoints for chart
                for (int i = 1; i < (_stepsGrowthPhase + 1); i++)
                {
                    var newTime = updatedPhase.InitialXt + stepsize * i;
                    var newEffect =
                        Math.Round(
                        (updatedPhase.InitialYq +
                         updatedPhase.GrowthRateFactor * Math.Pow((newTime - updatedPhase.InitialXt), 2)), 2);

                    var newDataPoint = new DataPoint { Id = updatedPhase.Id, Time = newTime, Effect = newEffect };

                    updatedPhase.PhaseDataPoints.Add(newDataPoint);
                }

                updatedPhase.TotalEnergyReleased = Math.Round((((1.0 / 3.0) * updatedPhase.GrowthRateFactor *
                                                                Math.Pow(updatedPhase.Duration, 3) +
                                                                updatedPhase.InitialYq * updatedPhase.Duration) / 1000.0), 2);
            }

            if (updatedPhase.PhaseTypeId == PhaseTypeHelper.GrowthKnownDurationAndTargetEffect)
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
                    var newEffect =
                        Math.Round(
                        (updatedPhase.InitialYq +
                         updatedPhase.GrowthRateFactor * Math.Pow((newTime - updatedPhase.InitialXt), 2)), 2);

                    var newDataPoint = new DataPoint { Id = updatedPhase.Id, Time = newTime, Effect = newEffect };

                    updatedPhase.PhaseDataPoints.Add(newDataPoint);
                }

                updatedPhase.TotalEnergyReleased = Math.Round((((1.0 / 3.0) * updatedPhase.GrowthRateFactor *
                                                                Math.Pow(updatedPhase.Duration, 3) +
                                                                updatedPhase.InitialYq * updatedPhase.Duration) / 1000.0), 2);
            }

            if (updatedPhase.PhaseTypeId == PhaseTypeHelper.GrowthKnownTargetEffectAndGrowthRate)
            {
                updatedPhase.TargetYq = phaseFormViewModel.TargetYq;
                updatedPhase.GrowthRateFactor = phaseFormViewModel.GrowthRateFactor;

                updatedPhase.Duration =
                    Math.Round(
                        (Math.Sqrt((updatedPhase.TargetYq - updatedPhase.InitialYq) / updatedPhase.GrowthRateFactor)), 1);
                updatedPhase.TargetXt = updatedPhase.InitialXt + updatedPhase.Duration;

                var stepsize = updatedPhase.Duration / (_stepsGrowthPhase);

                // Generate Datapoints for chart
                for (int i = 1; i < (_stepsGrowthPhase + 1); i++)
                {
                    var newTime = updatedPhase.InitialXt + stepsize * i;
                    var newEffect =
                        Math.Round(
                        (updatedPhase.InitialYq +
                         updatedPhase.GrowthRateFactor * Math.Pow((newTime - updatedPhase.InitialXt), 2)), 2);

                    var newDataPoint = new DataPoint { Id = updatedPhase.Id, Time = newTime, Effect = newEffect };

                    updatedPhase.PhaseDataPoints.Add(newDataPoint);
                }

                updatedPhase.TotalEnergyReleased = Math.Round((((1.0 / 3.0) * updatedPhase.GrowthRateFactor *
                                                                Math.Pow(updatedPhase.Duration, 3) +
                                                                updatedPhase.InitialYq * updatedPhase.Duration) / 1000.0), 2);
            }

            #endregion

            #region Steady Phase

            if (updatedPhase.PhaseTypeId == PhaseTypeHelper.SteadyKnownDuration)
            {
                updatedPhase.Duration = phaseFormViewModel.Duration;

                updatedPhase.GrowthRateFactor = 0;
                updatedPhase.TargetXt = updatedPhase.InitialXt + updatedPhase.Duration;
                updatedPhase.TargetYq = updatedPhase.InitialYq;

                var stepsize = updatedPhase.Duration / (_stepsSteadyPhase);

                // Generate Datapoints for chart
                for (int i = 1; i < (_stepsSteadyPhase + 1); i++)
                {
                    var newTime = updatedPhase.InitialXt + stepsize * i;
                    var newEffect =
                        Math.Round(
                        (updatedPhase.InitialYq +
                         updatedPhase.GrowthRateFactor * Math.Pow((newTime - updatedPhase.InitialXt), 2)), 2);

                    var newDataPoint = new DataPoint { Id = updatedPhase.Id, Time = newTime, Effect = newEffect };

                    updatedPhase.PhaseDataPoints.Add(newDataPoint);
                }

                updatedPhase.TotalEnergyReleased = Math.Round(((updatedPhase.InitialYq * updatedPhase.Duration) / 1000.0), 2);
            }

            #endregion

            #region Decay Phase

            if (updatedPhase.PhaseTypeId == PhaseTypeHelper.DecayKnownDurationAndGrowthRate)
            {
                updatedPhase.Duration = phaseFormViewModel.Duration;
                updatedPhase.GrowthRateFactor = phaseFormViewModel.GrowthRateFactor;

                updatedPhase.TargetXt = updatedPhase.InitialXt + updatedPhase.Duration;
                updatedPhase.TargetYq = updatedPhase.InitialYq -
                                        updatedPhase.GrowthRateFactor * Math.Pow(updatedPhase.Duration, 2);

                var stepsize = updatedPhase.Duration / (_stepsDecayPhase);

                // Generate Datapoints for chart
                for (int i = 1; i < (_stepsDecayPhase + 1); i++)
                {
                    var newTime = updatedPhase.InitialXt + stepsize * i;
                    var newEffect =
                        Math.Round((updatedPhase.InitialYq -
                                    updatedPhase.GrowthRateFactor *
                                    (Math.Pow(updatedPhase.Duration, 2) -
                                     Math.Pow((updatedPhase.Duration - (newTime - updatedPhase.InitialXt)), 2))), 2);

                    var newDataPoint = new DataPoint { Id = updatedPhase.Id, Time = newTime, Effect = newEffect };

                    updatedPhase.PhaseDataPoints.Add(newDataPoint);
                }

                updatedPhase.TotalEnergyReleased = Math.Round((((1.0 / 3.0) * updatedPhase.GrowthRateFactor *
                                                                Math.Pow(updatedPhase.Duration, 3) +
                                                                updatedPhase.TargetYq * updatedPhase.Duration) / 1000.0), 2);
            }

            if (updatedPhase.PhaseTypeId == PhaseTypeHelper.DecayKnownDurationAndTargetEffect)
            {
                updatedPhase.Duration = phaseFormViewModel.Duration;
                updatedPhase.TargetYq = phaseFormViewModel.TargetYq;

                updatedPhase.GrowthRateFactor = Math.Round((updatedPhase.InitialYq - updatedPhase.TargetYq) /
                                                           Math.Pow(updatedPhase.Duration, 2), 4);
                updatedPhase.TargetXt = updatedPhase.InitialXt + updatedPhase.Duration;

                var stepsize = updatedPhase.Duration / (_stepsDecayPhase);

                // Generate Datapoints for chart
                for (int i = 1; i < (_stepsDecayPhase + 1); i++)
                {
                    var newTime = updatedPhase.InitialXt + stepsize * i;
                    var newEffect =
                        Math.Round((updatedPhase.InitialYq -
                                    updatedPhase.GrowthRateFactor *
                                    (Math.Pow(updatedPhase.Duration, 2) -
                                     Math.Pow((updatedPhase.Duration - (newTime - updatedPhase.InitialXt)), 2))), 2);

                    var newDataPoint = new DataPoint { Id = updatedPhase.Id, Time = newTime, Effect = newEffect };

                    updatedPhase.PhaseDataPoints.Add(newDataPoint);
                }

                updatedPhase.TotalEnergyReleased = Math.Round((((1.0 / 3.0) * updatedPhase.GrowthRateFactor *
                                                                Math.Pow(updatedPhase.Duration, 3) +
                                                                updatedPhase.TargetYq * updatedPhase.Duration) / 1000.0), 2);
            }

            if (updatedPhase.PhaseTypeId == PhaseTypeHelper.DecayKnownTargetEffectAndGrowthRate)
            {
                updatedPhase.TargetYq = phaseFormViewModel.TargetYq;
                updatedPhase.GrowthRateFactor = phaseFormViewModel.GrowthRateFactor;

                updatedPhase.Duration =
                    Math.Round(
                        (Math.Sqrt((updatedPhase.InitialYq - updatedPhase.TargetYq) / updatedPhase.GrowthRateFactor)), 1);
                updatedPhase.TargetXt = updatedPhase.InitialXt + updatedPhase.Duration;

                var stepsize = updatedPhase.Duration / (_stepsDecayPhase);

                // Generate Datapoints for chart
                for (int i = 1; i < (_stepsDecayPhase + 1); i++)
                {
                    var newTime = updatedPhase.InitialXt + stepsize * i;
                    var newEffect =
                        Math.Round((updatedPhase.InitialYq -
                                    updatedPhase.GrowthRateFactor *
                                    (Math.Pow(updatedPhase.Duration, 2) -
                                     Math.Pow((updatedPhase.Duration - (newTime - updatedPhase.InitialXt)), 2))), 2);

                    var newDataPoint = new DataPoint { Id = updatedPhase.Id, Time = newTime, Effect = newEffect };

                    updatedPhase.PhaseDataPoints.Add(newDataPoint);
                }

                updatedPhase.TotalEnergyReleased = Math.Round((((1.0 / 3.0) * updatedPhase.GrowthRateFactor *
                                                                Math.Pow(updatedPhase.Duration, 3) +
                                                                updatedPhase.TargetYq * updatedPhase.Duration) / 1000.0), 2);
            }

            #endregion

            return updatedPhase;
        }

        public async Task<List<Phase>> UpdatePhasesAsync(List<Phase> phases)
        {
            if (phases == null)
            {
                throw new NullReferenceException("input Phases cannot be null in Calculator -> UpdatePhases");
            }

            var newState = new State();

            var newPhases = new List<Phase>();

            await Task.Run((() =>
            {
                foreach (var phase in phases)
                {
                    var newPhaseFormViewModel = new PhaseFormViewModel()
                    {
                        Duration = phase.Duration,
                        GrowthRateFactor = phase.GrowthRateFactor,
                        TargetYq = phase.TargetYq,
                        PhaseTypeId = phase.PhaseTypeId

                    };

                    var newPhase = GeneratePhase(newPhaseFormViewModel, newState);

                    newState.LatestXt = newPhase.TargetXt;
                    newState.LatestYq = newPhase.TargetYq;
                    newState.PhasesCount += 1;

                    newPhases.Add(newPhase);
                }

            }));

            return newPhases;
        }
    }
}
