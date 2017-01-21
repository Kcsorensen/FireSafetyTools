namespace FireSafetyTools.Models.Tools.FireSafety.Evacuation
{
    public class RouteStart : BaseRouteElement
    {
        public int RouteId { get; set; }
        public double NumberOfPeople { get; set; }
        public double DetectionTime { get; set; }
        public double NotificationTime { get; set; }
        public double PreEvacuationTime { get; set; }

        public RouteStart()
        {
            base.RouteType = RouteTypeHelper.RouteStartingPoint;
        }
    }
}
