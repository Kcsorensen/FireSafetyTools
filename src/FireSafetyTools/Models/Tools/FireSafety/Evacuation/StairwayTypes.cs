using System.Collections.Generic;

namespace FireSafetyTools.Models.Tools.FireSafety.Evacuation
{
    public class StairwayTypes
    {
        // Stair rise 7.0 inch. Stair tread 11.0 inch// 
        public const int Rise180xTread280 = 0;

        // Stair rise 6.5 inch. Stair tread 12.0 inch
        public const int Rise165xTread305 = 1;

        // Stair rise 6.5 inch. Stair tread 13.0 inch 
        public const int Rise165xTread330 = 2;

        public static readonly List<RouteType> ListOfStairTypes = new List<RouteType>()
        {
            new RouteType() {Id = Rise180xTread280, Name = "Rise 180 mm, Tread 280 mm" },
            new RouteType() {Id = Rise165xTread305, Name = "Rise 165 mm, Tread 305 mm" },
            new RouteType() {Id = Rise165xTread330, Name = "Rise 165 mm, Tread 330 mm" },
        };
    }
}
