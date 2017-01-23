﻿namespace FireSafetyTools.Models.Tools.FireSafety.Evacuation
{
    public class Door : BaseRouteElement
    {
        public double NumberOfPeople { get; set; }

        public Door()
        {
            base.RouteType = RouteTypeHelper.Door;
            base.KValue = KValues.Door;
            base.BoundaryLayerWidth = BoundaryLayerWidths.Door;
            base.Fsmax = FsmaxValues.Door;
        }
    }
}