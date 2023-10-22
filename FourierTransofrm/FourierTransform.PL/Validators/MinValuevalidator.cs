using System.ComponentModel.DataAnnotations;

namespace FourierTransform.PL.Validators;

public class MinValueValidator : ValidationAttribute
{
    public int MaxValue { get; set; }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int intValue && intValue > MaxValue)
        {
            return new ValidationResult("Начальное значение отрезка не может превосходить конечное");
        }

        return ValidationResult.Success;
    }
}