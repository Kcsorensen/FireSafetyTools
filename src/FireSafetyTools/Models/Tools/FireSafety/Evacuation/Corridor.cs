using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireSafetyTools.Models.Tools.FireSafety.Evacuation
{
    public class Corridor : BaseRouteElement
    {
        public double Density { get; set; }
        public double Speed { get; set; }
        public double NumberOfPeople { get; set; }
        public double SpecificFlow { get; set; }
        public bool HasHandrails { get; set; }
        public double CalculatedFlow { get; set; }

        public Corridor(int routeType) : base(routeType)
        {
            
        }
    }
}
