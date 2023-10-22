using System.Numerics;

namespace FourierTransform.FFT;

public class ComplexComparer: IComparer<Complex>
{
    public int Compare(Complex x, Complex y)
    {
        double normX = x.Magnitude;
        double normY = y.Magnitude;

        if (normX < normY)
            return -1;
        else if (normX > normY)
            return 1;
        else
            return 0;
    }
}