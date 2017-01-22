using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireSafetyTools.Models.Tools.FireSafety.Evacuation
{
    public class RouteTypeHelper
    {
        public const int RouteStartingPoint = 0;
        public const int Corridor = 1;
        public const int Door = 2;
        public const int WideConcourse = 3;
        public const int Room = 4;
        public const int Stairway = 5;

        public static readonly List<RouteType> ListOfRouteTypes = new List<RouteType>()
        {
            new RouteType() {Id = Room, Name = "Room" },
            new RouteType() {Id = WideConcourse, Name = "Wide Concourse" },
            new RouteType() {Id = Corridor, Name = "Corridor" },
            new RouteType() {Id = Stairway, Name = "Stairway" },
            new RouteType() {Id = Door, Name = "Door" },
        };

        public static string GetRouteTypeName(int routeTypeId)
        {
            if (routeTypeId < 0 || routeTypeId > ListOfRouteTypes.Count)
            {
                throw new IndexOutOfRangeException("Input parameter routeTypeId cannot be less then zero or larger than (ListOfRouteTypes.Count + 1), RouteTypeHelper -> GetRouteTypeName");
            }

            var routeTypeName = ListOfRouteTypes.Single(x => x.Id == routeTypeId).Name;

            return routeTypeName;
        }
    }
}
