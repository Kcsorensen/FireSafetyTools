namespace Evacuation.lib.Models
{
    public class RouteStart : BaseRouteElement
    {
        public double DetectionTime { get; set; }
        public double NotificationTime { get; set; }
        public double PreEvacuationTime { get; set; }

        public RouteStart()
        {
            base.RouteTypeId = RouteTypeHelper.RouteStartingPoint;
        }
    }
}
