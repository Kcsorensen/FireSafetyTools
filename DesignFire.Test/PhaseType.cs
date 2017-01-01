using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesignFire.Test
{
    public class PhaseType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public const int GrowthKnownDurationAndGrowthRate = 0;
        public const int GrowthKnownDurationAndTargetEffect = 1;
        public const int GrowthKnownTargetEffectAndGrowthRate = 2;
        public const int SteadyKnownDuration = 3;
        public const int DecayKnownDurationAndGrowthRate = 4;
        public const int DecayKnownDurationAndTargetEffect = 5;
        public const int DecayKnownTargetEffectAndGrowthRate = 6;
    }
}
