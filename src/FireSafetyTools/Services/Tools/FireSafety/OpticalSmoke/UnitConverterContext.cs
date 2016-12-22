using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FireSafetyTools.Models.Tools.FireSafety.OpticalSmoke;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FireSafetyTools.Services.Tools.FireSafety.OpticalSmoke
{
    public class UnitConverterContext : IUnitConverterContext
    {
        public SmokeData SmokeData { get; set; }


        public UnitConverterContext()
        {
            SmokeData = new SmokeData();
        }

        public SmokeData GetsSmokeData()
        {
            return SmokeData;
        }

        public void UpdateSmokeData(SmokeData smokeData)
        {
            SmokeData = smokeData;
        }

        public void ClearSmokeData()
        {
            SmokeData = new SmokeData();
        }
    }
}
