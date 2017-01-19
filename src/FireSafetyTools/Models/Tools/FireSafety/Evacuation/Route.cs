using System;

namespace FireSafetyTools.Models.Tools.FireSafety.Evacuation
{
    public class Route
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public double DetectionTime { get; set; }
        public double NotificationTime { get; set; }
        public double PreEvacuationTime { get; set; }

        public Route()
        {
            Guid = Guid.NewGuid();
        }
    }
}
