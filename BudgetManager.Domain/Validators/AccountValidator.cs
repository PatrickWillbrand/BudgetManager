using FluentValidation;

namespace BudgetManager.Domain.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(a => a.FirstName).NotEmpty().WithMessage("Geben Sie einen Vornamen ein.");
            RuleFor(a => a.FirstName).Length(0, 100).WithMessage("Die Länge der Vornamens beträgt mehr als 100 Zeichen");

            RuleFor(a => a.LastName).NotEmpty().WithMessage("Geben Sie einen Nachnamen ein.");
            RuleFor(a => a.LastName).Length(0, 100).WithMessage("Die Länge der Nachnames beträgt mehr als 100 Zeichen");

            RuleFor(a => a.UserName).NotEmpty().WithMessage("Geben Sie einen Benutzernamen ein.");
            RuleFor(a => a.UserName).Length(0, 100).WithMessage("Die Länge der Benutzernamens beträgt mehr als 100 Zeichen");

            RuleFor(a => a.Password).NotEmpty();
        }
    }
}