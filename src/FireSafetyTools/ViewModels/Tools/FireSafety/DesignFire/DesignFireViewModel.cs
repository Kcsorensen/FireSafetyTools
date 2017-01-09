using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireSafetyTools.Models.Tools.FireSafety.DesignFire;
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
        public int PhaseTypeId { get; set; }
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

            var calculator = new Calculator();

            var growthFormViewModel = new PhaseFormViewModel()
            {
                Duration = 100,
                GrowthRateFactor = 0.047,
                PhaseTypeId = PhaseTypeHelper.GrowthKnownDurationAndGrowthRate
            };

            var growthPhase = calculator.GeneratePhase(growthFormViewModel, State);

            AddPhase(growthPhase);

            UpdateState();

            var steadyFormViewModel = new PhaseFormViewModel()
            {
                Duration = 100,
                PhaseTypeId = PhaseTypeHelper.SteadyKnownDuration
            };

            var steadyPhase = calculator.GeneratePhase(steadyFormViewModel, State);

            AddPhase(steadyPhase);

            UpdateState();

            var decayFormViewModel = new PhaseFormViewModel()
            {
                Duration = 100,
                GrowthRateFactor = 0.047,
                PhaseTypeId = PhaseTypeHelper.DecayKnownDurationAndGrowthRate
            };

            var decayPhase = calculator.GeneratePhase(decayFormViewModel, State);

            AddPhase(decayPhase);

        }

        private void ClearChartData()
        {
            ChartDataPoints.Clear();
            ChartDataPoints.Add(new DataPoint { Id = 0, Time = 0.0, Effect = 0.0 });
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

        public void AddPhase(Phase newPhase)
        {
            State.LatestXt = newPhase.TargetXt;
            State.LatestYq = newPhase.TargetYq;

            Phases.Add(newPhase);

            this.ClearChartData();

            foreach (var phase in Phases)
            {
                ChartDataPoints.AddRange(phase.GetDataPoints());
            }

            var xAxisArray = ChartDataPoints.Select(x => x.Time).ToArray();
            var yAxisArray = ChartDataPoints.Select(x => x.Effect).ToArray();

            XAxis = JsonConvert.SerializeObject(xAxisArray);
            YAxis = JsonConvert.SerializeObject(yAxisArray);

        }

        private void UpdatePhases()
        {
            
        }

        public void UpdateState()
        {
            State.PhasesCount = Phases.Count;
        }

        public void DeletePhase(int id)
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

            var updatedPhases = calculator.UpdatePhases(Phases);

            Phases = updatedPhases;

            var lastPhase = Phases.Last();

            State.LatestXt = lastPhase.TargetXt;
            State.LatestYq = lastPhase.TargetYq;
            State.PhasesCount = Phases.Count();

            // Update ChartDataPoints

            this.ClearChartData();

            foreach (var phase in Phases)
            {
                ChartDataPoints.AddRange(phase.GetDataPoints());
            }

            var xAxisArray = ChartDataPoints.Select(x => x.Time).ToArray();
            var yAxisArray = ChartDataPoints.Select(x => x.Effect).ToArray();

            XAxis = JsonConvert.SerializeObject(xAxisArray);
            YAxis = JsonConvert.SerializeObject(yAxisArray);
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

            var updatedPhases = await calculator.UpdatePhasesAsync(Phases);

            Phases = updatedPhases;

            var lastPhase = Phases.Last();

            State.LatestXt = lastPhase.TargetXt;
            State.LatestYq = lastPhase.TargetYq;
            State.PhasesCount = Phases.Count(); 

            // Update ChartDataPoints

            this.ClearChartData();

            foreach (var phase in Phases)
            {
                ChartDataPoints.AddRange(phase.GetDataPoints());
            }

            var xAxisArray = ChartDataPoints.Select(x => x.Time).ToArray();
            var yAxisArray = ChartDataPoints.Select(x => x.Effect).ToArray();

            XAxis = JsonConvert.SerializeObject(xAxisArray);
            YAxis = JsonConvert.SerializeObject(yAxisArray);
        }
    }
}
