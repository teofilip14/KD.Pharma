using FluentValidation;

namespace KD.Common.Model.Models.Validators
{
    public class UserModelValidator : BaseValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(s => s.UserId).NotNull()
                .WithMessage("UserId must not be empty.");

            RuleFor(s => s.Username).NotEmpty()
                .WithMessage("Username must not be empty.")
                .MinimumLength(3)
                .WithMessage("Minimum lenght of 3.")
                .MaximumLength(60)
                .WithMessage("Maximum lenght of 60");

            RuleFor(s => s.Password).NotEmpty()
                .WithMessage("Password must not be empty.")
                .MinimumLength(3)
                .WithMessage("Minimum lenght of 3.")
                .MaximumLength(60)
                .WithMessage("Maximum lenght of 60");

            RuleFor(s => s.IsAdmin).NotEmpty()
                .WithMessage("IsAdmin must not be empty");
        }
    }
}
