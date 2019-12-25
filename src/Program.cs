using System;

namespace MonteCarloCircleCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var radius = (args.Length > 0) ? int.Parse(args[0]) : 20;
            var real = new FormulaCalculator().GetArea(radius);
            var iterations = 1_000_000;

            var x = new MonteCarloTaskCalculator(iterations).GetArea(radius);

            Console.WriteLine($"Result - real: {real} estimated: {x}");
        }
    }
}
