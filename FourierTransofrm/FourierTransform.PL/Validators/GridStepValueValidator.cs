using System.ComponentModel.DataAnnotations;

namespace FourierTransform.PL.Validators;

public class GridStepValueValidator : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is double doubleValue && doubleValue <= 0)
        {
            return new ValidationResult("Шаг сетки не может быть отрицательным");
        }

        return ValidationResult.Success;
    }
}