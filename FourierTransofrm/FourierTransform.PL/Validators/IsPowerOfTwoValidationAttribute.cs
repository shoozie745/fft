using System.ComponentModel.DataAnnotations;

namespace FourierTransform.PL.Validators;

public class IsPowerOfTwoValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        
        if (value is int intPositiveCheck && intPositiveCheck <= 0)
        {
            return new ValidationResult("Длина последовательности должна быть строго положительной");
        }

        if (value is int intPowTwoCheck && Math.Sqrt(intPowTwoCheck) % 1 != 0)
        {
            return  new ValidationResult("Длина последовательности должна быть степенью 2");
        }
        
        return ValidationResult.Success;
    }
}