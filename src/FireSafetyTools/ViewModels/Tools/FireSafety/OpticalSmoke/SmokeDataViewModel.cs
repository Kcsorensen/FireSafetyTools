using System.Collections.Generic;
using FireSafetyTools.Models.Tools.FireSafety.OpticalSmoke;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;


namespace FireSafetyTools.ViewModels.Tools.FireSafety.OpticalSmoke
{

    public class SmokeDataViewModel
    {
        public SmokeData _smokeData { get; set; }

        public List<SelectListItem> SelectListItems { get; set; }
        
        public SmokeData GetsSmokeData()
        {
            return _smokeData;
        }

        public void UpdateSmokeData(SmokeData smokeData)
        {
            _smokeData = smokeData;
        }

        public void ClearSmokeData()
        {
            _smokeData = new SmokeData();
        }

        public void Initiate()
        {
            _smokeData = new SmokeData();

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
