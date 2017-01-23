using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FireSafetyTools.ViewModels.Tools.FireSafety.Evacuation
{
    public class CreateRouteViewModel
    {
        [Display(Name = "Number of People")]
        public double NumberOfPeople { get; set; }

        public string Name { get; set; }

        [Display(Name = "Detection Time [s]")]
        public double DetectionTime { get; set; }

        [Display(Name = "Notification Time [s]")]
        public double NotificationTime { get; set; }

        [Display(Name = "Pre-Evacaution Time [s]")]
        public double PreEvacuationTime { get; set; }
    }
}
