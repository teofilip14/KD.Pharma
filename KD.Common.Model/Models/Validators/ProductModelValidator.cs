using FluentValidation;

namespace KD.Common.Model.Models.Validators
{
    public class ProductModelValidator : BaseValidator<ProductModel>
    {
        public ProductModelValidator()
        {
            RuleFor(s=>s.Name).NotEmpty();
            RuleFor(s=>s.Price).NotEmpty();
            RuleFor(s => s.Producer).NotEmpty();
        }
    }
}