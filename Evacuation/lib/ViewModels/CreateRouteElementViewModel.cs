
namespace Evacuation.lib.ViewModels
{
    public class CreateRouteElementViewModel
    {
        public string Name { get; set; }
        public int RouteId { get; set; }
        public int RouteTypeId { get; set; }
        public bool HasHandrails { get; set; }
        public int TransitionType { get; set; }
        public int StairwayType { get; set; }

        public double Width { get; set; }

        public double Density { get; set; }

        public double NumberOfPeople { get; set; }

        public double Distance { get; set; }
    }
}
