using System.ComponentModel.DataAnnotations;

namespace RestAPI.Attributes
{
    public class ValidateAttribute : ValidationAttribute
    {
        private readonly string _dependentProperty;

        public ValidateAttribute(string dependentProperty)
        {
            _dependentProperty = dependentProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dependentProperty = validationContext.ObjectType.GetProperty(_dependentProperty);
            if (dependentProperty == null)
            {
                return new ValidationResult($"Unknown property: {_dependentProperty}");
            }

            var dependentValue = dependentProperty.GetValue(validationContext.ObjectInstance);
            if (dependentValue != null && !string.IsNullOrEmpty(dependentValue.ToString()))
            {
                if (value == null || string.IsNullOrEmpty(value.ToString()))
                {
                    return new ValidationResult("ErrorMessage" ?? $"{validationContext.DisplayName} is required.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
