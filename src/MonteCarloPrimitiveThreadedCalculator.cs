using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

//Error-prone messy old-style threading
public class MonteCarloPrimitiveThreadedCalculator : ICircleAreaCalculator
{
    public double GetArea(int radius)
    {
        const int iterations = 100_000;
        var pointsInCircleBag = new ConcurrentBag<int>();
        const int threadCount = 4;
        var threads = new List<Thread>();

        int area = (radius*2) * (radius*2);
        double circleCenterX = (double)radius;
        double circleCenterY = (double)radius;

        for(int i = 0; i < threadCount; i++)
        {
            var threadIters = iterations / threadCount;
            var thread = new Thread(() => {
                var r = new Random();
                var localPointsInCircle = 0;
                for(int i = 0; i<threadIters; i++)
                {
                    var x = r.Next(0, radius);
                    var y = r.Next(0, radius);

                    var isInCircle = IsPointInCircle((double)x, (double)y, circleCenterX, circleCenterY, (double)radius);
                    if(isInCircle)
                        localPointsInCircle++;
                }

                pointsInCircleBag.Add(localPointsInCircle);
            });

            threads.Add(thread);
        }

        threads.ForEach(t => t.Start());
        threads.ForEach(t => t.Join());

        var totalPointsInCircle = 0;

        foreach(var points in pointsInCircleBag)
        {
            Console.WriteLine($"Got {points} points");
            totalPointsInCircle += points;
        }
        double ratio = ((double)totalPointsInCircle / (double)iterations);
        Console.WriteLine($"{ratio}");
        return ratio * area;
    }

    private bool IsPointInCircle(double x, double y, double circleCenterX, double circleCenterY, double radius)
    {
        return Math.Pow((x - circleCenterX), 2) + Math.Pow((y - circleCenterY),2) < Math.Pow(radius,2);    
    }
}