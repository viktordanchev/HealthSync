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
            currentValue = currentValue!.Value.AddHours(1); 

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            var comparisonValue = (TimeOnly?)property.GetValue(validationContext.ObjectInstance);

            if (currentValue < comparisonValue)
            {
                return new ValidationResult(string.Empty);
            }

            return ValidationResult.Success;
        }
    }
}
