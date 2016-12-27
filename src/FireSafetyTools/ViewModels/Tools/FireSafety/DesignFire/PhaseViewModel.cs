using System;
using FireSafetyTools.Models.Tools.FireSafety.DesignFire;

namespace FireSafetyTools.ViewModels.Tools.FireSafety.DesignFire
{
    public class PhaseViewModel
    {
        public Phase Phase { get; private set; }

        public PhaseViewModel()
        {
            
        }

        public PhaseViewModel(Phase phase)
        {
            if (phase == null)
            {
                throw new ArgumentNullException("phase", "Phase in PhaseViewModel cannot be null");
            }

            Phase = phase;
        }

        public PhaseViewModel(double latestXt, double latestYq, int phaseTypeId)
        {
            Phase = new Phase(latestXt, latestYq, phaseTypeId);
        }
    }
}
