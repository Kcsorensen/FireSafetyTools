using System.Collections.Generic;
using System.Linq;
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

        public void Initiate()
        {
            State = new State();

            Phases = new List<Phase>();

            PhaseTypes = new List<PhaseType>
            {
                new PhaseType {Id = 0, Name = "Growth (duration, growth rate)"},
                new PhaseType {Id = 1, Name = "Growth (duration, target effect)"},
                new PhaseType {Id = 2, Name = "Growth (target effect, growth rate)"},
                new PhaseType {Id = 3, Name = "Steady (duration)"},
                new PhaseType {Id = 4, Name = "Decay (duration, growth rate)"},
                new PhaseType {Id = 5, Name = "Decay (duration, target effect)"},
                new PhaseType {Id = 6, Name = "Decay (target effect, growth rate)"},
            };

            ChartDataPoints = new List<DataPoint>
            {
                new DataPoint {Id = 0, Time = 0.0, Effect = 0.0}
            };
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

        public void UpdateState()
        {
            State.PhasesCount = Phases.Count;
            State.Name = PhaseTypes.Single(d => d.Id == PhaseTypeId).Name;
            State.PhaseTypeId = this.PhaseTypeId;
        }
    }
}
