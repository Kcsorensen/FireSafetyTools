namespace FireSafetyTools.Models.Tools.FireSafety.Evacuation
{
    public class RouteStart : BaseRouteElement
    {
        public double NumberOfPeople { get; set; }

        public RouteStart()
        {
            base.RouteType = RouteTypeHelper.RouteStartingPoint;
        }
    }
}
