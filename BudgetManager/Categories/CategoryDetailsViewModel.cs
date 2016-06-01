using BudgetManager.Domain;
using BudgetManager.Handles;
using BudgetManager.Services;
using Caliburn.Micro;

namespace BudgetManager.Categories
{
    public class CategoryDetailsViewModel : Screen, IHandle<CategoryChangedMessage>
    {
        private readonly ICategoryService _categoryService;
        private readonly IEventAggregator _eventAggregator;
        private Category _category;

        public CategoryDetailsViewModel(IEventAggregator eventAggregator, ICategoryService categoryService)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _categoryService = categoryService;
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
                NotifyOfPropertyChange();
            }
        }
    }
}