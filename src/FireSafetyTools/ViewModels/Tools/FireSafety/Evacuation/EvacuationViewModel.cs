using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireSafetyTools.Models.Tools.FireSafety.Evacuation;

namespace FireSafetyTools.ViewModels.Tools.FireSafety.Evacuation
{
    public class EvacuationViewModel
    {
        public Dictionary<string, List<Guid>> Routes { get; set; }
        public List<BaseRouteElement> RouteElements { get; set; }

    }
}
