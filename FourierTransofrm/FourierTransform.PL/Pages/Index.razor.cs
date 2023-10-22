using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Threading.Channels;
using FourierTransform.FFT;
using FourierTransform.PL.Models;
using FourierTransform.PL.Validators;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace FourierTransform.PL.Pages;

public partial class Index
{
    private bool showComplexPlotComponent = false;
    private bool showRealPlotComponent = false;
    private GraphModel _graphModel;
    private RealGraphModel _realGraphModel;
    private string _exceptionMessage;
    private bool _isExceptionShowing = false;
    CalculateInputsForm model = new CalculateInputsForm();
    private bool success;
    public class CalculateInputsForm
    {
        [IsPowerOfTwoValidation]
        public int SequenceLenght { get; set; }
        public string Expression { get; set; }
        public double? MinValueOnReal { get; set; }
        [GridStepValueValidator]
        public double? GridStep { get; set; }
        public int ZeroingSliderValue { get; set; }
        public bool IsRealFunction { get; set; }

    }

    private void OnValidSubmit(EditContext context)
    {
        _isExceptionShowing = false;
        if (!model.IsRealFunction)
        {
            CallRenderComplexPlot();
            return;
        }

        CallRenderRealPlot();
        
    }

    private void CallRenderRealPlot()
    {
        if (model.MinValueOnReal is null || model.GridStep is null)
            return;
        
        var grid = GridHelper.MakeGrid((double)model.MinValueOnReal, model.SequenceLenght, (double)model.GridStep);
        try
        {
            var functionValues = new List<double>();
            Func<double, double> parsedFunction = MathParser.Parse(model.Expression);
            foreach (var point in grid)
            {
                functionValues.Add(parsedFunction(point));
            }
            var correspondingComplexList = functionValues.Select(x => new Complex(x, 0)).ToArray();
            var fftTransformed = FFT.FFT.FFTTransform(correspondingComplexList);
            if (model.ZeroingSliderValue != 0)
                ComplexZeroer.ZeroByPercent((int)model.ZeroingSliderValue, fftTransformed);
        
            var inverse = FFT.FFT.IFFTTransform(fftTransformed);
            var inverseReal = inverse.Select(x => x.Real).ToArray();
            _realGraphModel = new RealGraphModel()
            {
                FirstSetDoubleValues = functionValues.ToArray(),
                SecondSetDoubleValues = inverseReal,
                GridValues = grid.ToArray()
            };

            showComplexPlotComponent = false;
            showRealPlotComponent = true;
        }
        catch (Exception ex)
        {
            _exceptionMessage = ex.Message;
            _isExceptionShowing = true;
            showRealPlotComponent = false;
            showComplexPlotComponent = false;
        }
        
    }
    

    private void CallRenderComplexPlot()
    {
        try
        {
            var result = ComplexGenerator.GenerateRandomComplexNumbers(model.SequenceLenght);
            var fftTransformed = FFT.FFT.FFTTransform(result);

            if (model.ZeroingSliderValue != 0)
                ComplexZeroer.ZeroByPercent((int)model.ZeroingSliderValue, fftTransformed);
        
            var inverse = FFT.FFT.IFFTTransform(fftTransformed);
            _graphModel = new GraphModel()
            {
                FirstSetComplexValues = result.Select(x => new ComplexValue() { Real = x.Real, Imaginary = x.Imaginary}).ToList(),
                SecondSetComplexValues = inverse.Select(x => new ComplexValue() { Real = x.Real, Imaginary = x.Imaginary}).ToList()
            };
            showRealPlotComponent = false;
            showComplexPlotComponent = true;
        }
        catch (Exception ex)
        {
            _exceptionMessage = ex.Message;
            _isExceptionShowing = true;
            showRealPlotComponent = false;
            showComplexPlotComponent = false;
        }
    }
    
    private void OnSliderInput(int value)
    {
        model.ZeroingSliderValue = value;
    }

}