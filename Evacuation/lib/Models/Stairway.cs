namespace Evacuation.lib.Models
{
    public class Stairway : BaseRouteElement
    {
        public int StairwayType { get; set; }
        public double Density { get; set; }
        public double Speed { get; set; }
        public double NumberOfPeople { get; set; }
        public bool HasHandrails { get; set; }
        public double Length { get; set; }

        public Stairway(int stairwayType)
        {
            StairwayType = stairwayType;
            base.RouteType = RouteTypes.Stairway;
            base.BoundaryLayerWidth = BoundaryLayerWidths.Stairway;

            if (StairwayType == StairwayTypes.Rise180xTread280)
            {
                base.KValue = KValues.Stairway180x280;
                Fsmax = FsmaxValues.Stair180x280;
            }
            else if (StairwayType == StairwayTypes.Rise165xTread305)
            {
                base.KValue = KValues.Stairway165x305;
                base.Fsmax = FsmaxValues.Stair165x305;
            }
            else
            {
                base.KValue = KValues.Stairway165x330;
                base.Fsmax = FsmaxValues.Stair165x330;
            }
        }
    }
}
