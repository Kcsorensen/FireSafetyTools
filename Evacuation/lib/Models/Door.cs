namespace Evacuation.lib.Models
{
    public class Door : BaseRouteElement
    {
        public Door()
        {
            base.RouteTypeId = RouteTypeHelper.Door;
            base.KValue = KValues.Door;
            base.BoundaryLayerWidth = BoundaryLayerWidths.Door;
            base.Fsmax = FsmaxValues.Door;
        }
    }
}
