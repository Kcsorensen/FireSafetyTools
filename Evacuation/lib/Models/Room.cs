
namespace Evacuation.lib.Models
{
    public class Room : BaseRouteElement
    {
        public Room()
        {
            base.RouteTypeId = RouteTypeHelper.Room;
            base.KValue = KValues.WideConcourse;
            base.BoundaryLayerWidth = BoundaryLayerWidths.WideConcourse;
        }
    }
}
