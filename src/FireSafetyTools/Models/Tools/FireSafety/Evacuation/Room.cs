
namespace FireSafetyTools.Models.Tools.FireSafety.Evacuation
{
    public class Room : BaseRouteElement
    {
        public double NumberOfPeople { get; set; }

        public Room()
        {
            base.RouteType = RouteTypeHelper.Room;
            base.KValue = KValues.WideConcourse;
            base.BoundaryLayerWidth = BoundaryLayerWidths.WideConcourse;
        }
    }
}
