using System.Numerics;

namespace FourierTransform.FFT;

public class ComplexZeroer
{
    public static void ZeroByPercent(int percent, Complex[] arr)
    {
        if (percent <= 0 || percent >= 100)
            throw new ArgumentException();
        
        var lenght = arr.Length;
        var zeroerCount = lenght * percent / 100;

        var duplicateArray = new Complex[arr.Length];
        arr.CopyTo(duplicateArray,0);
        duplicateArray = duplicateArray.OrderBy(x => x.Magnitude).ToArray();
        var minMagnitude = duplicateArray[zeroerCount].Magnitude;

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i].Magnitude < minMagnitude)
                arr[i] = Complex.Zero;
        }
    }
}