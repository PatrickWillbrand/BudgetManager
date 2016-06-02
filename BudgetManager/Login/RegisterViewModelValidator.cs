using FluentValidation;

namespace BudgetManager.Login
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(register => register.FirstName).NotEmpty().WithMessage("Geben Sie einen Vornamen ein.");
            RuleFor(register => register.FirstName).Length(0, 100).WithMessage("Die Länge der Vornamens beträgt mehr als 100 Zeichen.");

            RuleFor(register => register.LastName).NotEmpty().WithMessage("Geben Sie einen Nachnamen ein.");
            RuleFor(register => register.LastName).Length(0, 100).WithMessage("Die Länge der Nachnames beträgt mehr als 100 Zeichen.");

            RuleFor(register => register.UserName).NotEmpty().WithMessage("Geben Sie einen Benutzernamen ein.");
            RuleFor(register => register.UserName).Length(0, 100).WithMessage("Die Länge der Benutzernamens beträgt mehr als 100 Zeichen.");

            RuleFor(register => register.Password).NotEmpty().WithMessage("Geben Sie ein Passwort ein.");

            RuleFor(register => register.RepeatPassword).NotEmpty().WithMessage("Bitte wiederholen Sie das angegebene Passwort.");
            RuleFor(register => register.RepeatPassword)
                .Must((viewModel, repeatPassword) => IsRepeatPasswordValid(viewModel, repeatPassword))
                .WithMessage("Das Passwort und die Wiederholung stimmen nicht überein.");
        }

        private bool IsRepeatPasswordValid(RegisterViewModel viewModel, string repeatPassword)
        {
            return viewModel.Password == repeatPassword;
        }
    }
}