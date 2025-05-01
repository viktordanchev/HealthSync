using System.ComponentModel.DataAnnotations;

namespace Core.Attributes
{
    public class TimeAfterAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public TimeAfterAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var currentValue = (TimeOnly?)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            var comparisonValue = (TimeOnly?)property.GetValue(validationContext.ObjectInstance);
            comparisonValue = comparisonValue!.Value.AddHours(1);

            if (currentValue < comparisonValue)
            {
                return new ValidationResult(string.Empty);
            }

            return ValidationResult.Success;
        }
    }
}
