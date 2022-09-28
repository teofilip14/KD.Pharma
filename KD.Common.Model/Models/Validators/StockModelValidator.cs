using FluentValidation;

namespace KD.Common.Model.Models.Validators
{
    public class StockModelValidator: BaseValidator <StockModel>
    {
        public StockModelValidator()
        {
            RuleFor(s => s.Quantity).NotEmpty();
            RuleFor(s => s.Product).NotEmpty();
        }
    }
}
