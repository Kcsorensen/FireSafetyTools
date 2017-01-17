namespace Evacuation.lib.Models
{
    public class Corridor : BaseRouteElement
    {
        public double Density { get; set; }
        public double Speed { get; set; }
        public double NumberOfPeople { get; set; }
        public bool HasHandrails { get; set; }
        public double Length { get; set; }

        public Corridor()
        {
            base.RouteType = RouteTypes.Corridor;
            base.KValue = KValues.Corridor;
            base.Fsmax = FsmaxValues.Corridor;
            base.BoundaryLayerWidth = BoundaryLayerWidths.Corridor;
        }
    }
}
