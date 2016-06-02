using BudgetManager.Domain;
using BudgetManager.Handles;
using BudgetManager.Services;
using BudgetManager.Validation;
using Caliburn.Micro;

namespace BudgetManager.Categories
{
    public class CategoryDetailsViewModel : ValidatableScreen<CategoryDetailsViewModel>, IHandle<CategoryChangedMessage>
    {
        private readonly ICategoryService _categoryService;
        private readonly IEventAggregator _eventAggregator;
        private Category _category;

        public CategoryDetailsViewModel(IEventAggregator eventAggregator, ICategoryService categoryService)
            : base(new CategoryDetailsViewModelValidator())
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _categoryService = categoryService;
        }

        public string Description
        {
            get { return _category.Description; }
            set
            {
                _category.Description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }

        public bool IsEnabled
        {
            get { return _category != null; }
        }

        public string Name
        {
            get { return _category.Name; }
            set
            {
                _category.Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public void Handle(CategoryChangedMessage message)
        {
            // Wenn die Nachricht eine null übergibt, dann legen wir eine neue Kategorie an.
            if (message.NewCategory == null)
            {
                _category = new Category();
            }
            else
            {
                _category = message.NewCategory;
            }

            NotifyOfPropertyChange(string.Empty);
            NotifyOfPropertyChange(() => IsEnabled);
        }
    }
}