using FireSafetyTools.Models.Tools.FireSafety.OpticalSmoke;

namespace FireSafetyTools.ViewModel.Tools.FireSafety.OpticalSmoke
{
    public class SmokeDataViewModel
    {
        public SmokeData _smokeData { get; set; }

        public SmokeDataViewModel()
        {
            _smokeData = new SmokeData();
        }

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
    }
}
