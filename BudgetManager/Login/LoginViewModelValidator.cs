using FluentValidation;

namespace BudgetManager.Login
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(login => login.UserName).NotEmpty().WithMessage("Geben Sie einen Benutzernamen ein.");
            RuleFor(login => login.UserName).Length(0, 100).WithMessage("Die Länge der Benutzernamens beträgt mehr als 100 Zeichen.");

            RuleFor(login => login.Password).NotEmpty().WithMessage("Geben Sie ein Passwort ein.");
        }
    }
}