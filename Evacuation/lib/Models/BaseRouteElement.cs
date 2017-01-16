using System;

namespace Evacuation.lib.Models
{
    public class BaseRouteElement
    {
        public string Name { get; set; }
        public Guid Guid { get; private set; }
        public int RouteType { get; set; }
        public double KValue { get; set; }

        public BaseRouteElement()
        {
            Guid = new Guid();
        }
    }
}
