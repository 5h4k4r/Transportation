using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Core.Validations
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AllowedValuesAttribute : ValidationAttribute
    {
        private readonly string[] _allowedValues;

        public AllowedValuesAttribute(params string[] allowedValues)
        {
            _allowedValues = allowedValues;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return null;

            if (_allowedValues.Contains(value as string))
                return ValidationResult.Success;

            string allowedValuesAsString = string.Empty;

            foreach (var allowedValue in _allowedValues)
            {
                allowedValuesAsString += $"{allowedValue} ";
            }
            return new ValidationResult($"{value} is not an allowed value, Allowed Values are: {allowedValuesAsString}" );
        }
    }
}
