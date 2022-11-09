using FluentValidation;

namespace Forum.Services.AuthenticationServices.Models
{
    public class SignUpViewModel
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignUpViewModelValidator : AbstractValidator<SignUpViewModel>
    {
        public SignUpViewModelValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .NotNull()
                .Length(3, 63);

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .Length(3, 63);

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .Length(3, 63);
        }
    }
}