using System;
using System.ComponentModel.DataAnnotations;
using FireSafetyTools.Models.Tools.FireSafety.DesignFire;

namespace FireSafetyTools.ViewModels.Tools.FireSafety.DesignFire
{
    public class PhaseFormViewModel
    {
        public int PhaseTypeId { get; set; }

        [Display(Name = "Time Duration")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "This value has to be greater then zero")]
        public double Duration { get; set; }

        [Display(Name = "Growth Rate Factor")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "This value has to be greater then zero")]
        public double GrowthRateFactor { get; set; }

        [Display(Name = "Target Effect")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "This value has to be greater then zero")]
        public double TargetYq { get; set; }

        public bool Edit { get; set; }

        public PhaseFormViewModel()
        {
            
        }

        public PhaseFormViewModel(Phase phase)
        {
            PhaseTypeId = phase.PhaseTypeId;
            Duration = phase.Duration;
            GrowthRateFactor = phase.GrowthRateFactor;
            TargetYq = phase.TargetYq;
        }
    }
}
