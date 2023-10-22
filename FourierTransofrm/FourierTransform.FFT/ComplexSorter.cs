using System.Numerics;

namespace FourierTransform.FFT;

public class ComplexSorter
{
    public static void SortByNorm(Complex[] complexArray)
    {
        if (complexArray == null || complexArray.Length == 0)
            return;

        Array.Sort(complexArray, new ComplexComparer());
    }
}