using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireSafetyTools.Models.Tools.FireSafety.Evacuation
{
    public class BaseRouteElement
    {
        public string Name { get; set; }
        public Guid Guid { get; private set; }
        public int RouteType { get; set; }

        public BaseRouteElement(int routeType)
        {
            RouteType = routeType;
            Guid = new Guid();
        }
    }
}
