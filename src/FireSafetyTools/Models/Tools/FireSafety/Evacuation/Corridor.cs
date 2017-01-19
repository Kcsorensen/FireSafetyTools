namespace FireSafetyTools.Models.Tools.FireSafety.Evacuation
{
    public class Corridor : BaseRouteElement
    {

        public double Speed { get; set; }
        public double NumberOfPeople { get; set; }
        public bool HasHandrails { get; set; }
        public double Distance { get; set; }
        public double TravelTime { get; set; }

        public Corridor()
        {
            base.RouteType = RouteTypeHelper.Corridor;
            base.KValue = KValues.Corridor;
            base.Fsmax = FsmaxValues.Corridor;
            base.BoundaryLayerWidth = BoundaryLayerWidths.Corridor;
        }
    }
}
