using FluentValidation;

namespace Forum.Services.AuthenticationServices.Models
{
    public class SignInViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class SignInViewModelValidator : AbstractValidator<SignInViewModel>
    {
        public SignInViewModelValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .NotNull()
                .Length(3, 63);

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .Length(3, 63);
        }
    }
}