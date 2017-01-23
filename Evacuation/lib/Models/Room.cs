﻿
namespace Evacuation.lib.Models
{
    public class Room : BaseRouteElement
    {
        public double Length { get; set; }
        public double NumberOfPeople { get; set; }

        public Room()
        {
            base.RouteType = RouteTypes.Room;
            base.KValue = KValues.WideConcourse;
            base.BoundaryLayerWidth = BoundaryLayerWidths.WideConcourse;
        }
    }
}