using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireSafetyTools.Models.Tools.FireSafety.OpticalSmoke;

namespace FireSafetyTools.Services.Tools.FireSafety.OpticalSmoke
{
    public interface IUnitConverterContext
    {
        SmokeData GetsSmokeData();

        void UpdateSmokeData(SmokeData smokeData);

        void ClearSmokeData();
    }
}
