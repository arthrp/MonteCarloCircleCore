using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

public class MonteCarloTaskCalculator : BaseCircleAreaCalculator, ICircleAreaCalculator
{
    public MonteCarloTaskCalculator(int iterations) : base(iterations) {}

    public double GetArea(int radius)
    {
        int iterations = _iterations;
        int rectangleArea = (radius*2) * (radius*2);
        const int taskCount = 4;
        int pointsPerTask = iterations / taskCount;
        var taskArr = new List<Task<int>>();

        for(int i = 0; i < taskCount; i++)
        {
            var task = Task<int>.Factory.StartNew(() => GetPointsInCircle(pointsPerTask, radius));
            taskArr.Add(task);
        }

        //Task is execulted when Result is read (and calling thread blocked until Result is available)
        int totalPointsInCircle = taskArr.Sum(x => x.Result);

        double ratio = ((double)totalPointsInCircle / (double)iterations);
        Console.WriteLine($"{ratio}");
        return ratio * rectangleArea;
    }

    private int GetPointsInCircle(int pointsPerTask, int radius)
    {
        var r = new Random();
        int pointsInCircle = 0;
        double circleCenterX = (double)radius;
        double circleCenterY = (double)radius;

        for(int i = 0; i < pointsPerTask; i++)
        {
            var x = r.Next(0, radius);
            var y = r.Next(0, radius);

            var isInCircle = IsPointInCircle((double)x, (double)y, circleCenterX, circleCenterY, (double)radius);
            if(isInCircle)
               pointsInCircle++;
        }

        return pointsInCircle;
    }

    private bool IsPointInCircle(double x, double y, double circleCenterX, double circleCenterY, double radius)
    {
        return Math.Pow((x - circleCenterX), 2) + Math.Pow((y - circleCenterY),2) < Math.Pow(radius,2);    
    }
}