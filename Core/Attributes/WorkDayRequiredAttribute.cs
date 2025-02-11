using System.ComponentModel.DataAnnotations;

namespace Core.Attributes
{
    public class WorkDayRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance;

            //if (model.IsWorkDay && value == null)
            //{
            //    return new ValidationResult(string.Empty);
            //}

            return ValidationResult.Success;
        }
    }
}
