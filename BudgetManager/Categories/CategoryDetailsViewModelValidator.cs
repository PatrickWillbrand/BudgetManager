using FluentValidation;

namespace BudgetManager.Categories
{
    public class CategoryDetailsViewModelValidator : AbstractValidator<CategoryDetailsViewModel>
    {
        public CategoryDetailsViewModelValidator()
        {
            RuleFor(category => category.CategoryName).NotEmpty().WithMessage("Geben Sie einen Namen ein.");
            RuleFor(category => category.CategoryName).Length(0, 100).WithMessage("Die Länge der Namens beträgt mehr als 100 Zeichen.");

            RuleFor(category => category.Description).Length(0, 5000).WithMessage("Die Länge der Namens beträgt mehr als 5000 Zeichen.");
        }
    }
}