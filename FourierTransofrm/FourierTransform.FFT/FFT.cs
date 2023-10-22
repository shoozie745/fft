using System.Numerics;

namespace FourierTransform.FFT;

public class FFT
{
    public static Complex[] CooleyTukeyFFT(Complex[] x)
    {
        int n = x.Length;

        // Base case: if the input size is 1, return the input
        if (n == 1)
            return new Complex[] { x[0] };

        // Split the input into even and odd parts
        Complex[] xEven = new Complex[n / 2];
        Complex[] xOdd = new Complex[n / 2];
        for (int i = 0; i < n / 2; i++)
        {
            xEven[i] = x[2 * i];
            xOdd[i] = x[2 * i + 1];
        }

        // Recursive FFT on even and odd parts
        Complex[] XEven = CooleyTukeyFFT(xEven);
        Complex[] XOdd = CooleyTukeyFFT(xOdd);

        // Combine the results
        Complex[] X = new Complex[n];
        for (int k = 0; k < n / 2; k++)
        {
            Complex twiddleFactor = Complex.FromPolarCoordinates(1, -2 * Math.PI * k / n) * XOdd[k];
            X[k] = XEven[k] + twiddleFactor;
            X[k + n / 2] = XEven[k] - twiddleFactor;
        }

        return X;
    }
    public static Complex[] CooleyTukeyIFFT(Complex[] X)
    {
        int n = X.Length;

        // Take the conjugate of the input
        for (int i = 0; i < n; i++)
            X[i] = Complex.Conjugate(X[i]);

        // Compute FFT of conjugate input
        Complex[] result = FFT.CooleyTukeyFFT(X);

        // Take the conjugate of the output and divide by n
        for (int i = 0; i < n; i++)
            result[i] = Complex.Conjugate(result[i]) / n;

        return result;
    }

    // Wrapper function for the IFFT
    public static Complex[] IFFTTransform(Complex[] X)
    {
        int n = X.Length;

        // Ensure input size is a power of 2
        if (!IsPowerOfTwo(n))
        {
            throw new ArgumentException("Input size must be a power of 2");
        }

        // Perform the Cooley-Tukey IFFT
        return CooleyTukeyIFFT(X);
    }

    // Wrapper function for the FFT
    public static Complex[] FFTTransform(Complex[] x)
    {
        int n = x.Length;

        // Ensure input size is a power of 2
        if (!IsPowerOfTwo(n))
        {
            throw new ArgumentException("Input size must be a power of 2");
        }

        // Perform the Cooley-Tukey FFT
        return CooleyTukeyFFT(x);
    }

    // Check if a number is a power of 2
    private static bool IsPowerOfTwo(int x)
    {
        return (x & (x - 1)) == 0 && x != 0;
    }

    // Print the complex array
    public static void PrintComplexArray(Complex[] arr)
    {
        foreach (var element in arr)
        {
            Console.WriteLine(element);
        }
    }
    
}