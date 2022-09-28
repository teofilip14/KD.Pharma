using FluentValidation;

namespace KD.Common.Model.Models.Validators
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        public BaseValidator()
        {
            CascadeMode = CascadeMode.Continue;
        }

        public PropertyValidationResult ValidateProperty(T instancetoValidate, string propertyName)
        {
            var validationResult = Validate(instancetoValidate);

            return new PropertyValidationResult()
            {
                IsValid = validationResult.IsValid,
                Errors = validationResult.Errors.Where(error => error.PropertyName == propertyName).ToList()
            };
        }
    }
}
