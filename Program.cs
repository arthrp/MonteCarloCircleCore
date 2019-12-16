using System;

namespace MonteCarloCircleCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var radius = (args.Length > 0) ? int.Parse(args[0]) : 20;
            var real = new FormulaCalculator().GetArea(radius);
            var x = new MonteCarloPrimitiveThreadedCalculator().GetArea(radius);

            Console.WriteLine($"Result - real: {real} estimated: {x}");
        }
    }
}
