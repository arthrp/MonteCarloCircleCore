public abstract class BaseCircleAreaCalculator
{
    protected readonly int _iterations;
    public BaseCircleAreaCalculator(int iterations)
    {
        _iterations = iterations;
    }

    public abstract double GetArea(int radius);
}