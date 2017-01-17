using System;

namespace Evacuation.lib.Models
{
    public class BaseRouteElement
    {
        public string Name { get; set; }
        public Guid Guid { get; private set; }
        public int RouteType { get; set; }
        public double KValue { get; set; }
        public double Fsmax { get; set; }
        public double BoundaryLayerWidth { get; set; }
        public double SpecificFlow { get; set; }
        public double CalculatedFlow { get; set; }
        public double EffectiveWidth { get; set; }
        public double Width { get; set; }
        public double Density { get; set; }
        public int TransitionType { get; set; }
        public double QueueBuildup { get; set; }

        public BaseRouteElement()
        {
            Guid = Guid.NewGuid();
            TransitionType = TransitionTypes.OneFlowInOneFlowOut;
        }
    }
}
