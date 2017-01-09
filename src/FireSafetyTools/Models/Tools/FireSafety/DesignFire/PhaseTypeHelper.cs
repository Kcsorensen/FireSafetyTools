using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireSafetyTools.Models.Tools.FireSafety.DesignFire
{
    public class PhaseTypeHelper
    {
        public const int GrowthKnownDurationAndGrowthRate = 0;
        public const int GrowthKnownDurationAndTargetEffect = 1;
        public const int GrowthKnownTargetEffectAndGrowthRate = 2;
        public const int SteadyKnownDuration = 3;
        public const int DecayKnownDurationAndGrowthRate = 4;
        public const int DecayKnownDurationAndTargetEffect = 5;
        public const int DecayKnownTargetEffectAndGrowthRate = 6;

        public static readonly List<PhaseType> ListOfPhaseTypes = new List<PhaseType>()
        {
            new PhaseType {Id = 0, Name = "Growth (duration, growth rate)"},
            new PhaseType {Id = 1, Name = "Growth (duration, target effect)"},
            new PhaseType {Id = 2, Name = "Growth (target effect, growth rate)"},
            new PhaseType {Id = 3, Name = "Steady (duration)"},
            new PhaseType {Id = 4, Name = "Decay (duration, growth rate)"},
            new PhaseType {Id = 5, Name = "Decay (duration, target effect)"},
            new PhaseType {Id = 6, Name = "Decay (target effect, growth rate)"},
        };

        public static string GetPhaseTypeName(int id)
        {
            if (id > 6)
            {
                throw new IndexOutOfRangeException("Id in GetPhaseTypeName cannot be larger then 6");
            }

            var name = ListOfPhaseTypes.Single(x => x.Id == id).Name;

            return name;
        }
    }
}
