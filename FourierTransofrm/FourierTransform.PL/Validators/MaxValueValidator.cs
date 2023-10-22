using System.ComponentModel.DataAnnotations;

namespace FourierTransform.PL.Validators;

public class MaxValueValidator : ValidationAttribute
{
    public int MinValue { get; set; }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int intValue && intValue > MinValue)
        {
            return new ValidationResult("Конечное значение отрезка не может быть меньше начального");
        }

        return ValidationResult.Success;
    }
}