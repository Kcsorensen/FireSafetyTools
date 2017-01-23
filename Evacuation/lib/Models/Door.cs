namespace Evacuation.lib.Models
{
    public class Door : BaseRouteElement
    {
        public double NumberOfPeople { get; set; }

        public Door()
        {
            base.RouteType = RouteTypes.Door;
            base.KValue = KValues.Door;
            base.BoundaryLayerWidth = BoundaryLayerWidths.Door;
            base.Fsmax = FsmaxValues.Door;
        }
    }
}
