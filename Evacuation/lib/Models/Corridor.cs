namespace Evacuation.lib.Models
{
    public class Corridor : BaseRouteElement
    {

        public Corridor()
        {
            base.RouteTypeId = RouteTypeHelper.Corridor;
            base.KValue = KValues.Corridor;
            base.Fsmax = FsmaxValues.Corridor;
            base.BoundaryLayerWidth = BoundaryLayerWidths.Corridor;
        }
    }
}
