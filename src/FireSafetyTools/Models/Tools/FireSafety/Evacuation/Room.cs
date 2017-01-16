
namespace FireSafetyTools.Models.Tools.FireSafety.Evacuation
{
    public class Room : BaseRouteElement
    {
        public double Length { get; set; }
        public double Width { get; set; }
        public double Area { get; set; }
        public double MaxDistance { get; set; }
        public double NumberOfPeople { get; set; }

        public Room(int routeType) : base(routeType)
        {
            
        }
    }
}
