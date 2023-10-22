using System.Numerics;

namespace FourierTransform.FFT;

public static class ComplexGenerator
{
    private static Random _random = new Random();
    
    public static Complex[] GenerateRandomComplexNumbers(int N)
    {
        Complex[] complexNumbers = new Complex[N];

        for (int i = 0; i < N; i++)
        {
            double realPart = (_random.NextDouble());
            double imaginaryPart = (_random.NextDouble());
            complexNumbers[i] = new Complex(realPart, imaginaryPart);
        }

        return complexNumbers;
    }
}