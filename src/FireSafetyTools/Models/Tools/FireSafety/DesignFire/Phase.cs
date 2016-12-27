using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FireSafetyTools.Models.Tools.FireSafety.DesignFire
{
    public class Phase
    {
        public int Id { get; set; }
        public int PhaseTypeId { get; set; }
        public string Name { get; set; }

        [Display(Name = "Time Duration")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "This value has to be greater then zero")]
        public double Duration { get; set; }

        [Display(Name = "Growth Rate Factor")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "This value has to be greater then zero")]
        public double GrowthRateFactor { get; set; }

        [Display(Name = "Target Effect")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "This value has to be greater then zero")]
        public double TargetYq { get; set; }

        public double TargetXt { get; set; }
        public double TotalEnergyReleased { get; set; }
        public double InitialXt { get; set; }
        public double InitialYq { get; set; }
        public List<DataPoint> PhaseDataPoints { get; set; }

        public Phase()
        {

        }

        public Phase(double latestXt, double latestYq, int phaseTypeId)
        {
            InitialXt = latestXt;
            InitialYq = latestYq;
            PhaseTypeId = phaseTypeId;
        }

        public List<DataPoint> GetDataPoints()
        {
            if (PhaseDataPoints == null)
            {
                throw new ArgumentNullException("PhaseDataPoints in Phase cant be null when executing method GetDataPoints");
            }

            return PhaseDataPoints;
        }
    }
}
