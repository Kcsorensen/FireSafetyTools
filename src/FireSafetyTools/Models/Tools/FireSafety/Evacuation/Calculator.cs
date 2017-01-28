using System;
using System.Collections.Generic;
using System.Linq;

namespace FireSafetyTools.Models.Tools.FireSafety.Evacuation
{
    public class Calculator
    {
        private readonly double _a = 0.266;
        private readonly double _dmax = 1 / (2 * 0.266);
        private double _previousEffectiveWidth;
        private double _previousSpecificFlow;
        private double _previousCalculatedFlow;
        private double _previousFirstPersonTime;
        private double _previousLastPersonTime;

        public List<BaseRouteElement> CalculateRoute(List<BaseRouteElement> route)
        {
            if (route == null)
            {
                throw new ArgumentNullException("Input parameter route cannot be null, Calculator -> CalculateRoute");
            }

            // Create a placeholder for the updated routes and populate with the first item from input parameter, RouteStart
            var updatedRoute = new List<BaseRouteElement>() { route.First() };
             
            // Skal ligges i foreach for routes, så den resettes hver gang der kikkes på en ny route 
            _previousEffectiveWidth = 0.0;
            _previousSpecificFlow = 0.0;
            _previousCalculatedFlow = 0.0;
            _previousFirstPersonTime = 0.0;
            _previousLastPersonTime = 0.0;

            foreach (var routeElement in route)
            {
                // Corridor
                if (routeElement.RouteTypeId == RouteTypeHelper.Corridor)
                {
                    // Cast routeElement to Corridor
                    var element = ((Corridor)routeElement);

                    // Determine Effective Width
                    element.EffectiveWidth = element.Width - (2 * element.BoundaryLayerWidth);

                    // Check Transition Type
                    if (element.TransitionType == TransitionTypes.FirstRouteElement)
                    {
                        if (element.Density < 0.001)
                        {
                            throw new Exception("The Density cannot be zero for the First RouteElement, Calculator -> CalculateRoute");
                        }

                        // Determine Specific Flow
                        element.SpecificFlow = (element.KValue - _a * element.KValue * element.Density) * element.Density;
                    }

                    // Check Transition Type
                    if (element.TransitionType != TransitionTypes.FirstRouteElement)
                    {
                        // Determine Specific Flow
                        element.SpecificFlow = GetSpecificFlow(element);

                        // Determine Density
                        var a = _a * element.KValue;
                        var b = -element.KValue;
                        var c = element.SpecificFlow;

                        var roots = QuadraticEquationSolver(a, b, c);

                        if (roots.Item1 <= _dmax)
                        {
                            element.Density = roots.Item1;
                        }

                        if (roots.Item2 <= _dmax)
                        {
                            element.Density = roots.Item2;
                        }

                        if (element.Density < 0.001)
                        {
                            throw new Exception("The Density cannot be zero after the QuadraticEquationSolver, Calculator -> CalculateRoute");
                        }
                    }

                    // Check if calculated specific flow exceed maximum specific flow
                    if (element.SpecificFlow > element.Fsmax)
                    {
                        element.SpecificFlow = element.Fsmax;
                    }

                    // Determine Speed
                    element.Speed = element.KValue - _a * element.KValue * element.Density;

                    // Determine Calculated Flow
                    element.CalculatedFlow = element.SpecificFlow * element.EffectiveWidth;

                    // Determine Travel Time
                    element.TravelTime = element.Distance / element.Speed;

                    // Time for Passage
                    element.TimeForPassage = element.NumberOfPeople / element.CalculatedFlow;

                    // Determine Queue Buildup
                    element.QueueBuildup = _previousCalculatedFlow - element.CalculatedFlow;

                    // First Person Time
                    element.FirstPersonTime = _previousFirstPersonTime + element.TravelTime;

                    // Last Person Time
                    element.LastPersonTime = (element.QueueBuildup <= 0) ? 
                        _previousLastPersonTime +  element.TravelTime :
                        _previousLastPersonTime + element.TimeForPassage + element.TravelTime;

                    // Update previous Specific Flow, Effective Width and Calculated Flow
                    _previousEffectiveWidth = element.EffectiveWidth;
                    _previousSpecificFlow = element.SpecificFlow;
                    _previousCalculatedFlow = element.CalculatedFlow;
                    _previousFirstPersonTime = element.FirstPersonTime;
                    _previousLastPersonTime = element.LastPersonTime;

                    updatedRoute.Add(element);
                }

                // Door
                if (routeElement.RouteTypeId == RouteTypeHelper.Door)
                {
                    // Cast routeElement to Door
                    var element = ((Door)routeElement);

                    // Determine Effective Width
                    element.EffectiveWidth = element.Width - (2 * element.BoundaryLayerWidth);

                    // Check Transition Type
                    if (routeElement.TransitionType == TransitionTypes.FirstRouteElement)
                    {
                        if (element.Density < 0.001)
                        {
                            throw new Exception("The Density cannot be zero after the QuadraticEquationSolver, Calculator -> CalculateRoute");
                        }

                        // Determine Specific Flow
                        element.SpecificFlow = (element.KValue - _a * element.KValue * element.Density) * element.Density;
                    }

                    // Check Transition Type
                    if (routeElement.TransitionType != TransitionTypes.FirstRouteElement)
                    {
                        // Determine Specific Flow
                        element.SpecificFlow = GetSpecificFlow(element);
                    }

                    // Check if calculated specific flow exceed maximum specific flow
                    if (element.SpecificFlow > element.Fsmax)
                    {
                        element.SpecificFlow = element.Fsmax;
                    }

                    // Determine Calculated Flow
                    element.CalculatedFlow = element.SpecificFlow * element.EffectiveWidth;

                    // Time for Passage
                    element.TimeForPassage = element.NumberOfPeople / element.CalculatedFlow;

                    // Determine Queue Buildup
                    element.QueueBuildup = _previousCalculatedFlow - element.CalculatedFlow;

                    // First Person Time
                    element.FirstPersonTime = _previousFirstPersonTime;

                    // Last Person Time
                    element.LastPersonTime = (element.QueueBuildup <= 0) ?
                        _previousLastPersonTime :
                        _previousLastPersonTime + element.TimeForPassage;

                    // Update previous Specific Flow, Effective Width and Calculated Flow
                    _previousEffectiveWidth = element.EffectiveWidth;
                    _previousSpecificFlow = element.SpecificFlow;
                    _previousCalculatedFlow = element.CalculatedFlow;
                    _previousFirstPersonTime = element.FirstPersonTime;
                    _previousLastPersonTime = element.LastPersonTime;

                    updatedRoute.Add(element);
                }

                // Stairway
                if (routeElement.RouteTypeId == RouteTypeHelper.Stairway)
                {
                    // Cast routeElement to Stairway
                    var element = ((Stairway)routeElement);

                    // Determine Effective Width
                    element.EffectiveWidth = element.Width - (2 * element.BoundaryLayerWidth);

                    // Check Transition Type
                    if (routeElement.TransitionType == TransitionTypes.FirstRouteElement)
                    {
                        if (element.Density < 0.001)
                        {
                            throw new Exception("The Density cannot be zero after the QuadraticEquationSolver, Calculator -> CalculateRoute");
                        }

                        // Determine Specific Flow
                        element.SpecificFlow = (element.KValue - _a * element.KValue * element.Density) * element.Density;
                    }

                    // Check Transition Type
                    if (routeElement.TransitionType != TransitionTypes.FirstRouteElement)
                    {
                        // Determine Specific Flow
                        element.SpecificFlow = GetSpecificFlow(element);

                        // Determine Density
                        var a = _a * element.KValue;
                        var b = -element.KValue;
                        var c = element.SpecificFlow;

                        var roots = QuadraticEquationSolver(a, b, c);

                        if (roots.Item1 <= _dmax)
                        {
                            element.Density = roots.Item1;
                        }

                        if (roots.Item2 <= _dmax)
                        {
                            element.Density = roots.Item2;
                        }

                        if (element.Density < 0.001)
                        {
                            throw new Exception("The Density cannot be zero after the QuadraticEquationSolver, Calculator -> CalculateRoute");
                        }
                    }

                    // Check if calculated specific flow exceed maximum specific flow
                    if (element.SpecificFlow > element.Fsmax)
                    {
                        element.SpecificFlow = element.Fsmax;
                    }

                    // Determine Speed
                    element.Speed = element.KValue - _a * element.KValue * element.Density;

                    // Determine Calculated Flow
                    element.CalculatedFlow = element.SpecificFlow * element.EffectiveWidth;

                    // Determine Travel Time
                    element.TravelTime = element.Distance / element.Speed;

                    // Time for Passage
                    element.TimeForPassage = element.NumberOfPeople / element.CalculatedFlow;

                    // Determine Queue Buildup
                    element.QueueBuildup = _previousCalculatedFlow - element.CalculatedFlow;

                    // First Person Time
                    element.FirstPersonTime = _previousFirstPersonTime + element.TravelTime;

                    // Last Person Time
                    element.LastPersonTime = (element.QueueBuildup <= 0) ?
                        _previousLastPersonTime + element.TravelTime :
                        _previousLastPersonTime + element.TimeForPassage + element.TravelTime;

                    // Update previous Specific Flow, Effective Width and Calculated Flow
                    _previousEffectiveWidth = element.EffectiveWidth;
                    _previousSpecificFlow = element.SpecificFlow;
                    _previousCalculatedFlow = element.CalculatedFlow;
                    _previousFirstPersonTime = element.FirstPersonTime;
                    _previousLastPersonTime = element.LastPersonTime;

                    updatedRoute.Add(element);
                }
            }

            return updatedRoute;
        }

        private double GetSpecificFlow(BaseRouteElement routeElement)
        {
            double specificFlow = 0;

            if (routeElement.TransitionType == TransitionTypes.OneFlowInOneFlowOut)
            {
                if (Math.Abs(routeElement.EffectiveWidth) < 0.001)
                {
                    throw new Exception("Effective Flow cannot be zero when calculating Specific Flow, Calculator -> GetSpecificFlow");
                }

                specificFlow = (_previousSpecificFlow * _previousEffectiveWidth) / routeElement.EffectiveWidth;
            }

            return specificFlow;
        }

        /// <summary>
        /// The input parameters is the constants in ax²+bx+c=0. Output is a Tuple with x(+) and x(-)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private Tuple<double, double> QuadraticEquationSolver(double a, double b, double c)
        {
            var preRoot = b * b - 4 * a * c;

            if (preRoot < 0)
            {
                throw new Exception("The preRoot cannot be less then zero, Calculator -> QuadraticEquationSolver");
            }

            var x1 = (-b + Math.Sqrt(Math.Pow(b, 2) - 4 * a * c)) / (2 * a);
            var x2 = (-b - Math.Sqrt(Math.Pow(b, 2) - 4 * a * c)) / (2 * a);

            var results = new Tuple<double, double>(x1, x2);

            return results;
        }
    }
}
