using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireSafetyTools.Models.Tools.FireSafety.OpticalSmoke
{
    public class SmokeUnitConverter
    {
        public SmokeData Calculate(string fromUnitIndex, string toUnitIndex, SmokeData data)
        {
            if (data == null)
            {
                return null;
            }

            double resultS0 = 0;
            double resultS = 0;
            double resultD0 = 0;
            double resultYs = 0;

            if (fromUnitIndex == toUnitIndex)
            {
                return data;
            }

            #region Calculate To Smoke Potential Argos

            if (fromUnitIndex == SmokeUnit.SootYield && toUnitIndex == SmokeUnit.SmokePotentialArgos)
            {
                resultS0 = data.Pod * data.Ys * (10.0 / Math.Log(10.0) * (data.Hair / data.Hmat) * data.Rho0);
            }

            if (fromUnitIndex == SmokeUnit.SmokeProduction && toUnitIndex == SmokeUnit.SmokePotentialArgos)
            {
                resultS0 = (data.S / data.RateOfHeatRelease) * data.Hair * data.Rho0;
            }

            if (fromUnitIndex == SmokeUnit.SmokePotentialFuel && toUnitIndex == SmokeUnit.SmokePotentialArgos)
            {
                resultS0 = ((1000.0 * data.Hair * data.D010Log * data.Rho0) / data.Hmat);
            }

            #endregion

            #region Calculate to Smoke Potential Fuel

            if (fromUnitIndex == SmokeUnit.SootYield && toUnitIndex == SmokeUnit.SmokePotentialFuel)
            {
                resultD0 = (10.0 * data.Pod * data.Ys) / (1000.0 * Math.Log(10.0));
            }

            if (fromUnitIndex == SmokeUnit.SmokeProduction && toUnitIndex == SmokeUnit.SmokePotentialFuel)
            {
                resultD0 = (data.S * data.Hmat) / (1000.0 * data.RateOfHeatRelease);
            }

            if (fromUnitIndex == SmokeUnit.SmokePotentialArgos && toUnitIndex == SmokeUnit.SmokePotentialFuel)
            {
                resultD0 = (data.S0 * data.Hmat) / (1000.0 * data.Rho0 * data.Hair);
            }

            #endregion

            #region Calculate to Smoke Yield

            if (fromUnitIndex == SmokeUnit.SmokePotentialFuel && toUnitIndex == SmokeUnit.SootYield)
            {
                resultYs = (1000.0 * data.D010Log * Math.Log(10.0)) / (10.0 * data.Pod);
            }

            if (fromUnitIndex == SmokeUnit.SmokeProduction && toUnitIndex == SmokeUnit.SootYield)
            {
                resultYs = (data.S * Math.Log(10.0) * data.Hmat) / (10.0 * data.RateOfHeatRelease * data.Pod);
            }

            if (fromUnitIndex == SmokeUnit.SmokePotentialArgos && toUnitIndex == SmokeUnit.SootYield)
            {
                resultYs = (data.S0 * Math.Log(10.0) * data.Hmat) / (10.0 * data.Pod * data.Hair * data.Rho0);
            }

            #endregion

            #region Calculate to Smoke Production

            if (fromUnitIndex == SmokeUnit.SmokePotentialFuel && toUnitIndex == SmokeUnit.SmokeProduction)
            {
                resultS = (1000.0 * data.D010Log * data.RateOfHeatRelease) / data.Hmat;
            }

            if (fromUnitIndex == SmokeUnit.SootYield && toUnitIndex == SmokeUnit.SmokeProduction)
            {
                resultS = (data.Ys * 10.0 * data.RateOfHeatRelease * data.Pod) / (Math.Log(10.0) * data.Hmat);
            }

            if (fromUnitIndex == SmokeUnit.SmokePotentialArgos && toUnitIndex == SmokeUnit.SmokeProduction)
            {
                resultS = (data.S0 * data.RateOfHeatRelease) / (data.Hair * data.Rho0);
            }

            #endregion

            if (resultS0 > 0.0)
            {
                data.S0 = Math.Round(resultS0, 1);
            }

            if (resultS > 0.0)
            {
                data.S = Math.Round(resultS, 2);
            }

            if (resultD0 > 0.0)
            {
                data.D010Log = Math.Round(resultD0, 3);
            }

            if (resultYs > 0.0)
            {
                data.Ys = Math.Round(resultYs, 5);
            }

            return data;
        }

        public SmokeData TrimIrrelevantUnits(string fromUnitIndex, string toUnitIndex, SmokeData data)
        {
            if (data == null)
            {
                return null;
            }

            SmokeData newSmokeData = null;

            if (toUnitIndex == SmokeUnit.SmokePotentialArgos)
            {
                if (fromUnitIndex == SmokeUnit.SootYield)
                {
                    newSmokeData = new SmokeData
                    {
                        S0 = 0.0,
                        S = 0.0,
                        D010Log = 0.0,
                        RateOfHeatRelease = 0.0,

                        Ys = data.Ys,
                        Pod = data.Pod,
                        Hmat = data.Hmat,
                        Hair = data.Hair,
                        Rho0 = data.Rho0
                    };
                }

                if (fromUnitIndex == SmokeUnit.SmokeProduction)
                {
                    newSmokeData = new SmokeData
                    {
                        S0 = 0.0,
                        Ys = 0.0,
                        D010Log = 0.0,
                        Pod = 0.0,
                        Hmat = 0.0,

                        S = data.S,
                        RateOfHeatRelease = data.RateOfHeatRelease,
                        Hair = data.Hair,
                        Rho0 = data.Rho0
                    };
                }

                if (fromUnitIndex == SmokeUnit.SmokePotentialFuel)
                {
                    newSmokeData = new SmokeData
                    {
                        S0 = 0.0,
                        S = 0.0,
                        Ys = 0.0,
                        Pod = 0.0,
                        RateOfHeatRelease = 0.0,

                        D010Log = data.D010Log,
                        Hmat = data.Hmat,
                        Hair = data.Hair,
                        Rho0 = data.Rho0
                    };
                }
            }

            if (toUnitIndex == SmokeUnit.SmokePotentialFuel)
            {
                if (fromUnitIndex == SmokeUnit.SootYield)
                {
                    newSmokeData = new SmokeData
                    {
                        S0 = 0.0,
                        S = 0.0,
                        D010Log = 0.0,
                        Hmat = 0.0,
                        Hair = 0.0,
                        Rho0 = 0.0,
                        RateOfHeatRelease = 0.0,

                        Ys = data.Ys,
                        Pod = data.Pod
                    };
                }

                if (fromUnitIndex == SmokeUnit.SmokeProduction)
                {
                    newSmokeData = new SmokeData
                    {
                        S0 = 0.0,
                        Ys = 0.0,
                        D010Log = 0.0,
                        Pod = 0.0,
                        Hair = 0.0,
                        Rho0 = 0.0,

                        S = data.S,
                        Hmat = data.Hmat,
                        RateOfHeatRelease = data.RateOfHeatRelease
                    };
                }

                if (fromUnitIndex == SmokeUnit.SmokePotentialArgos)
                {
                    newSmokeData = new SmokeData
                    {
                        S = 0.0,
                        Ys = 0.0,
                        D010Log = 0.0,
                        Pod = 0.0,
                        RateOfHeatRelease = 0.0,

                        S0 = data.S0,
                        Hmat = data.Hmat,
                        Hair = data.Hair,
                        Rho0 = data.Rho0
                    };
                }
            }

            if (toUnitIndex == SmokeUnit.SootYield)
            {
                if (fromUnitIndex == SmokeUnit.SmokePotentialFuel)
                {
                    newSmokeData = new SmokeData
                    {
                        Ys = 0.0,
                        S = 0.0,
                        S0 = 0.0,
                        RateOfHeatRelease = 0.0,
                        Hair = 0.0,
                        Rho0 = 0.0,
                        Hmat = 0.0,

                        D010Log = data.D010Log,
                        Pod = data.Pod
                    };
                }

                if (fromUnitIndex == SmokeUnit.SmokeProduction)
                {
                    newSmokeData = new SmokeData
                    {
                        Ys = 0.0,
                        D010Log = 0.0,
                        S0 = 0.0,
                        Hair = 0.0,
                        Rho0 = 0.0,

                        S = data.S,
                        Pod = data.Pod,
                        RateOfHeatRelease = data.RateOfHeatRelease,
                        Hmat = data.Hmat
                    };
                }

                if (fromUnitIndex == SmokeUnit.SmokePotentialArgos)
                {
                    newSmokeData = new SmokeData
                    {
                        Ys = 0.0,
                        D010Log = 0.0,
                        S = 0.0,
                        RateOfHeatRelease = 0.0,

                        S0 = data.S0,
                        Pod = data.Pod,
                        Hair = data.Hair,
                        Rho0 = data.Rho0,
                        Hmat = data.Hmat
                    };
                }
            }

            if (toUnitIndex == SmokeUnit.SmokeProduction)
            {
                if (fromUnitIndex == SmokeUnit.SmokePotentialFuel)
                {
                    newSmokeData = new SmokeData
                    {
                        Ys = 0.0,
                        S = 0.0,
                        S0 = 0.0,
                        Pod = 0.0,
                        Hair = 0.0,
                        Rho0 = 0.0,

                        D010Log = data.D010Log,
                        RateOfHeatRelease = data.RateOfHeatRelease,
                        Hmat = data.Hmat
                    };
                }

                if (fromUnitIndex == SmokeUnit.SootYield)
                {
                    newSmokeData = new SmokeData
                    {
                        D010Log = 0.0,
                        S = 0.0,
                        S0 = 0.0,
                        Hair = 0.0,
                        Rho0 = 0.0,

                        Ys = data.Ys,
                        RateOfHeatRelease = data.RateOfHeatRelease,
                        Hmat = data.Hmat,
                        Pod = data.Pod
                    };
                }

                if (fromUnitIndex == SmokeUnit.SmokePotentialArgos)
                {
                    newSmokeData = new SmokeData
                    {
                        D010Log = 0.0,
                        S = 0.0,
                        Ys = 0.0,
                        Hmat = 0.0,
                        Pod = 0.0,

                        S0 = data.S0,
                        RateOfHeatRelease = data.RateOfHeatRelease,
                        Hair = data.Hair,
                        Rho0 = data.Rho0
                    };
                }
            }

            return newSmokeData;
        }

        public SmokeData TrimAndCalculate(string fromUnitIndex, string toUnitIndex, SmokeData data)
        {
            if (data == null)
            {
                return null;
            }

            SmokeData trimmedSmokeData = TrimIrrelevantUnits(fromUnitIndex, toUnitIndex, data);

            SmokeData result = Calculate(fromUnitIndex, toUnitIndex, trimmedSmokeData);

            return result;
        }
    }
}
