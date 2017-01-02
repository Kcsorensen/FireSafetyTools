using System;
using System.Collections.Generic;

namespace DesignFire.Test.lib
{
    public class Phase
    {
        public int Id { get; set; }
        public int PhaseTypeId { get; set; }
        public string Name { get; set; }

        public double Duration { get; set; }

        public double GrowthRateFactor { get; set; }

        public double TargetYq { get; set; }
        public double TargetXt { get; set; }
        public double TotalEnergyReleased { get; set; }
        public double InitialXt { get; set; }
        public double InitialYq { get; set; }
        public List<DataPoint> PhaseDataPoints { get; set; }

        public Phase()
        {

        }

        public List<DataPoint> GetDataPoints()
        {
            if (PhaseDataPoints == null)
            {
                throw new ArgumentNullException("PhaseDataPoints in Phase cannot be null when executing method GetDataPoints");
            }

            return PhaseDataPoints;
        }
    }
}
