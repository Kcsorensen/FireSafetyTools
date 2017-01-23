using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireSafetyTools.Models.Tools.FireSafety.DesignFire;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace FireSafetyTools.ViewModels.Tools.FireSafety.DesignFire
{
    public class DesignFireViewModel
    {
        public List<Phase> Phases { get; set; }
        public List<PhaseType> PhaseTypes { get; set; }
        public List<DataPoint> ChartDataPoints { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public State State { get; set; }

        public void Initiate()
        {
            State = new State();

            Phases = new List<Phase>();

            PhaseTypes = PhaseTypeHelper.ListOfPhaseTypes;

            ChartDataPoints = new List<DataPoint>
            {
                new DataPoint {Id = 0, Time = 0.0, Effect = 0.0}
            };

            // Initially build we a growth, steady and decay phase for testing
            var growthFormViewModel = new PhaseFormViewModel()
            {
                Duration = 240,
                TargetYq = 1550,
                PhaseTypeId = PhaseTypeHelper.GrowthKnownDurationAndTargetEffect
            };

            AddPhase(growthFormViewModel);

            var steadyFormViewModel = new PhaseFormViewModel()
            {
                Duration = 120,
                PhaseTypeId = PhaseTypeHelper.SteadyKnownDuration
            };

            AddPhase(steadyFormViewModel);

            var decayFormViewModel = new PhaseFormViewModel()
            {
                Duration = 1000,
                TargetYq = 0,
                PhaseTypeId = PhaseTypeHelper.DecayKnownDurationAndTargetEffect
            };

            AddPhase(decayFormViewModel);
        }

        private void UpdateState(double targetXt, double targetYq)
        {
            State.LatestXt = targetXt;
            State.LatestYq = targetYq;
            State.PhasesCount = Phases.Count;
        }
        
        private void UpdateChartData()
        {
            ChartDataPoints.Clear();
            ChartDataPoints.Add(new DataPoint { Id = 0, Time = 0.0, Effect = 0.0 });

            foreach (var phase in Phases)
            {
                ChartDataPoints.AddRange(phase.GetDataPoints());
            }

            var xAxisArray = ChartDataPoints.Select(x => x.Time).ToArray();
            var yAxisArray = ChartDataPoints.Select(x => x.Effect).ToArray();

            XAxis = JsonConvert.SerializeObject(xAxisArray);
            YAxis = JsonConvert.SerializeObject(yAxisArray);
        }

        public void ClearPhases()
        {
            State.LatestXt = 0.0;
            State.LatestYq = 0.0;
            Phases.Clear();
            ChartDataPoints.Clear();
            ChartDataPoints.Add(new DataPoint { Id = 0, Time = 0.0, Effect = 0.0 });
            var xAxisArray = ChartDataPoints.Select(x => x.Time).ToArray();
            var yAxisArray = ChartDataPoints.Select(x => x.Effect).ToArray();

            XAxis = JsonConvert.SerializeObject(xAxisArray);
            YAxis = JsonConvert.SerializeObject(yAxisArray);
        }

        public void AddPhase(PhaseFormViewModel phaseFormViewModel)
        {
            if (phaseFormViewModel == null)
            {
                throw new NullReferenceException("The input PhaseFormViewModel in AddPhase cannot be null");
            }

            var calculator = new Calculator();

            var newPhase = calculator.GeneratePhase(phaseFormViewModel, State);

            if (newPhase == null)
            {
                throw new ArgumentNullException("newPhase as a result of calculator is null. From DesignFireViewModel -> AddPhase");
            }

            Phases.Add(newPhase);

            UpdateState(newPhase.TargetXt, newPhase.TargetYq);

            // Update ChartDataPoints
            UpdateChartData();
        }

        public async Task UpdatePhaseAsync(PhaseFormViewModel phaseFormViewModel)
        {
            if (phaseFormViewModel == null)
            {
                throw new NullReferenceException("The input PhaseFormViewModel in UpdatePhase cannot be null");
            }

            // Update Phases
            var calculator = new Calculator();

            Phases.Single(x => x.Id == phaseFormViewModel.PhaseId).Duration = phaseFormViewModel.Duration;
            Phases.Single(x => x.Id == phaseFormViewModel.PhaseId).GrowthRateFactor = phaseFormViewModel.GrowthRateFactor;
            Phases.Single(x => x.Id == phaseFormViewModel.PhaseId).TargetYq = phaseFormViewModel.TargetYq;

            Phases = await calculator.UpdatePhasesAsync(Phases);

            var lastPhase = Phases.Last();

            UpdateState(lastPhase.TargetXt, lastPhase.TargetYq);

            // Update ChartDataPoints
            UpdateChartData();
        }
       
        public async Task DeletePhaseAsync(int id)
        {
            if (id == 0)
            {
                throw new ArgumentOutOfRangeException("Cannot delete phase with id = 0");
            }

            // Remove Phase in Phases
            var selectedPhase = Phases.Single(x => x.Id == id);

            Phases.Remove(selectedPhase);

            // Update Phases and State
            var calculator = new Calculator();

            Phases = await calculator.UpdatePhasesAsync(Phases);

            var lastPhase = Phases.Last();

            State.LatestXt = lastPhase.TargetXt;
            State.LatestYq = lastPhase.TargetYq;
            State.PhasesCount = Phases.Count();

            // Update ChartDataPoints
            UpdateChartData();
        }

        public string GetPyrosimExportData()
        {
            if (ChartDataPoints == null)
            {
                return "";
            }

            string stringResult = "";

            var maxEffect = ChartDataPoints.Max(a => a.Effect);

            foreach (var dataPoint in ChartDataPoints)
            {
                var time = Math.Round(dataPoint.Time, 2); 
                var fraction = Math.Round((dataPoint.Effect / maxEffect), 2);

                // Culture er med for at sikre at decimalseperator er punktum, så Pyrosim får dataen i rigtig format.
                stringResult += string.Format(new System.Globalization.CultureInfo("en-US"), "{0}\t{1}" + Environment.NewLine, time, fraction);
            }

            return stringResult;
        }
    }
}
