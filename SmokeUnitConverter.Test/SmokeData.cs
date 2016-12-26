
namespace SmokeUnitConverter.Test
{
    public class SmokeData
    {
        public double D010Log { get; set; }

        public double S { get; set; }

        public double S0 { get; set; }

        public double Ys { get; set; }

        public double RateOfHeatRelease { get; set; }

        public double Pod { get; set; }

        public double Hair { get; set; }

        public double Hmat { get; set; }

        public double Rho0 { get; set; }

        public byte ConvertFromId { get; set; }
        public byte ConvertToId { get; set; }

        public SmokeData()
        {
            Pod = 8700;
            Hair = 3000;
            Rho0 = 1.205;
            ConvertFromId = 0;
            ConvertToId = 3;
        }
    }
}
