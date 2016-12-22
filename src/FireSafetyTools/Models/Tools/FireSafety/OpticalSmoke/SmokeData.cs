using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FireSafetyTools.Models.Tools.FireSafety.OpticalSmoke
{
    public class SmokeData
    {
        [Required(ErrorMessage = "This value is required")]
        [Display(Name = " ")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "This value has to be greater then zero")]
        public double D010Log { get; set; }

        [Required]
        [Display(Name = " ")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "This value has to be greater then zero")]
        public double S { get; set; }

        [Required(ErrorMessage = "This value is required")]
        [Display(Name = " ")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "This value has to be greater then zero")]
        public double S0 { get; set; }

        [Required(ErrorMessage = "This value is required")]
        [Display(Name = " ")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "This value has to be greater then zero")]
        public double Ys { get; set; }

        [Required(ErrorMessage = "This value is required")]
        [Display(Name = " ")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "This value has to be greater then zero")]
        public double RateOfHeatRelease { get; set; }

        [Required(ErrorMessage = "This value is required")]
        [Display(Name = " ")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "This value has to be greater then zero")]
        public double Pod { get; set; }

        [Required(ErrorMessage = "This value is required")]
        [Display(Name = " ")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "This value has to be greater then zero")]
        public double Hair { get; set; }

        [Required(ErrorMessage = "This value is required")]
        [Display(Name = " ")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "This value has to be greater then zero")]
        public double Hmat { get; set; }

        [Required(ErrorMessage = "This value is required")]
        [Display(Name = " ")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "This value has to be greater then zero")]
        public double Rho0 { get; set; }

        public byte ConvertFromId { get; set; }
        public byte ConvertToId { get; set; }

        public IEnumerable<SelectListItem> SelectListItems { get; set; }

        public SmokeData()
        {
            Pod = 8700;
            Hair = 3000;
            Rho0 = 1.205;
            ConvertFromId = 0;
            ConvertToId = 3;

            SelectListItems = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "Smoke Potential Argos", Value = "0"},
                new SelectListItem() {Text = "Smoke Potential Fuel", Value = "1"},
                new SelectListItem() {Text = "Smoke Production", Value = "2"},
                new SelectListItem() {Text = "Soot yield", Value = "3"}
            };
        }
    }
}
