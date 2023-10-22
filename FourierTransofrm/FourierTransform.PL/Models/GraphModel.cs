using System.Numerics;
using MudBlazor;

namespace FourierTransform.PL.Models;

public class GraphModel
{
    public List<ComplexValue> FirstSetComplexValues { get; set; }
    public List<ComplexValue> SecondSetComplexValues { get; set; }
}

public class ComplexValue
{
    public double Real { get; set; }
    public double Imaginary { get; set; }
}