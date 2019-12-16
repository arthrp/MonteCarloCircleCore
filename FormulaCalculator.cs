using System;

public class FormulaCalculator : ICircleAreaCalculator
{
    public double GetArea(int radius)
    {
        return Math.PI * radius * radius;
    }
}