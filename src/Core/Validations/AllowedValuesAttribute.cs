using System.ComponentModel.DataAnnotations;

namespace Core.Validations
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AllowedValuesAttribute : ValidationAttribute
    {
        private readonly string[] _AllowedValues;

        public AllowedValuesAttribute(params string[] allowedValues)
        {
            _AllowedValues = allowedValues;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return null;

            if (_AllowedValues.Contains(value as string))
                return ValidationResult.Success;

            return new ValidationResult($"{value} is not an allowed value");
        }
    }
}
