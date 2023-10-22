using System.Linq.Expressions;
using System.Linq.Dynamic;
namespace FourierTransform.FFT;

public static class MathParser
{
    public static Func<double, double> Parse(string expression)
    {
        // Create a parameter for the input variable (x)

        string modifiedExpression = expression.Replace("x", "p");

        // Create a parameter expression for the input variable
        ParameterExpression parameter = Expression.Parameter(typeof(double), "p");

        // Parse the expression and replace 'p' with the input parameter
        Expression parsedExpression = System.Linq.Dynamic.Core.DynamicExpressionParser
            .ParseLambda(new[] { parameter }, null, modifiedExpression)
            .Body;

        // Create a lambda function that takes 'p' as input and evaluates the parsed expression
        return Expression.Lambda<Func<double, double>>(parsedExpression, parameter).Compile();
    }
}