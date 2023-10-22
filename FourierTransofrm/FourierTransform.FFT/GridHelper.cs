namespace FourierTransform.FFT;

public static class GridHelper
{
    public static List<double> MakeGrid(double minValue, int pointsAmount, double step)
    {
        if (step <= 0)
            throw new ArgumentException("Step must be a positive number.");

        List<double> grid = new List<double>();
        grid.Add(minValue);
        for (int i = 1; i <= pointsAmount -1 ; i++)
        {
            grid.Add(grid[i-1] + step);
        }

        return grid;
    }
}