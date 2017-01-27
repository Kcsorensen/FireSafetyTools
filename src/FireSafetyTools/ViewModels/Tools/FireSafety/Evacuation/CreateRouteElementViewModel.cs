
using System.ComponentModel.DataAnnotations;

namespace FireSafetyTools.ViewModels.Tools.FireSafety.Evacuation
{
    public class CreateRouteElementViewModel
    {
        public string Name { get; set; }
        public int RouteId { get; set; }
        public int RouteTypeId { get; set; }
        public bool HasHandrails { get; set; }
        public int TransitionType { get; set; }
        public int StairwayType { get; set; }

        [Display(Name = "Width [m]")]
        public double Width { get; set; }

        [Display(Name = "Density [people/m²]")]
        public double Density { get; set; }

        [Display(Name = "Number of People")]
        public double NumberOfPeople { get; set; }

        [Display(Name = "Distance [m]")]
        public double Distance { get; set; }
    }
}
