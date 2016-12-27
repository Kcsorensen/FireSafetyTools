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
        public string ChartDataJsonString { get; set; }
        public int PhaseTypeId { get; set; }
        public double LatestXt { get; set; }
        public double LatestYq { get; set; }

        public void AddPhase(Phase newPhase)
        {
            newPhase.Id = Phases.Count + 1;
            newPhase.Name = PhaseTypes.Single(d => d.Id == newPhase.PhaseTypeId).Name;

            Phases.Add(newPhase);

            this.ClearChartData();

            foreach (var phase in Phases)
            {
                ChartDataPoints.AddRange(phase.GetDataPoints());
            }

            ChartDataJsonString = JsonConvert.SerializeObject(ChartDataPoints);
        }

        public void Initiate()
        {
            LatestXt = 0.0;
            LatestYq = 0.0;

            Phases = new List<Phase>();

            PhaseTypes = new List<PhaseType>
            {
                new PhaseType {Id = 0, Name = "Growth (duration, growth rate)"},
                new PhaseType {Id = 1, Name = "Growth (target effect, growth rate)"},
                new PhaseType {Id = 2, Name = "Growth (target effect, duration)"},
                new PhaseType {Id = 3, Name = "Steady (duration)"},
                new PhaseType {Id = 4, Name = "Decay (duration, growth rate)"},
                new PhaseType {Id = 5, Name = "Decay (target effect, growth rate)"},
                new PhaseType {Id = 6, Name = "Decay (target effect, duration)"},
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
    }
}
