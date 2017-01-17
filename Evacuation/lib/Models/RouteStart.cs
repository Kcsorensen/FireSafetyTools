namespace Evacuation.lib.Models
{
    public class RouteStart : BaseRouteElement
    {
        public double NumberOfPeople { get; set; }

        public RouteStart()
        {
            base.RouteType = RouteTypes.RouteStartingPoint;
        }
    }
}
