using System.ComponentModel.DataAnnotations;

namespace Core.Attributes
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
            var dependentValue = validationContext.ObjectType.GetProperty(_dependentProperty)?.GetValue(validationContext.ObjectInstance);
            
            if (string.IsNullOrEmpty(dependentValue?.ToString()) || string.IsNullOrEmpty(value?.ToString()))
            {
                return new ValidationResult(string.Empty);
            }

            return ValidationResult.Success;
        }
    }
}
