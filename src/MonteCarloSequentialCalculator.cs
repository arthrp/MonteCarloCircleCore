using System;
using System.Diagnostics;

namespace MonteCarloCircleCore
{
    public class MonteCarloSequentialCalculator : BaseCircleAreaCalculator, ICircleAreaCalculator
    {
        private readonly Random _random = new Random();
        public MonteCarloSequentialCalculator(int iterations) : base(iterations) {}

        public double GetArea(int radius)
        {
            int iterations = _iterations;
            int pointsInCircle = 0;
            int area = (radius*2) * (radius*2);
            double circleCenterX = (double)radius;
            double circleCenterY = (double)radius;

            for(int i = 0; i<iterations; i++)
            {
                var x = _random.Next(0, radius);
                var y = _random.Next(0, radius);

                var isInCircle = IsPointInCircle((double)x, (double)y, circleCenterX, circleCenterY, (double)radius);
                if(isInCircle)
                    pointsInCircle++;
            }

            Console.WriteLine($"{pointsInCircle}");
            
            double ratio = ((double)pointsInCircle / (double)iterations);


            Console.WriteLine($"{ratio}");
            return ratio * area;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        private bool IsPointInCircle(double x, double y, double circleCenterX, double circleCenterY, double radius)
        {
            return Math.Pow((x - circleCenterX), 2) + Math.Pow((y - circleCenterY),2) < Math.Pow(radius,2);
        }
    }
}