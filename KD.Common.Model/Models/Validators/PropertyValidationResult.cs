using FluentValidation.Results;

namespace KD.Common.Model.Models.Validators
{
    public class PropertyValidationResult
    {
        public PropertyValidationResult()
        {
            this.Errors = new List<ValidationFailure>();
        }
        public bool IsValid { get; set; }

        public IList<ValidationFailure> Errors { get; set; }
    }
}
