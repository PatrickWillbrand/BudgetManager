using BudgetManager.Domain;
using BudgetManager.Messages;
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
        private bool _isNew;

        public CategoryDetailsViewModel(IEventAggregator eventAggregator, ICategoryService categoryService)
            : base(new CategoryDetailsViewModelValidator())
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _categoryService = categoryService;

            NewCategory();
        }

        public bool CanDeleteAsync
        {
            get { return !_isNew; }
        }

        public string CategoryName
        {
            get { return _category.Name; }
            set
            {
                _category.Name = value;
                NotifyOfPropertyChange(() => CategoryName);
            }
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

        public async void DeleteAsync()
        {
        }

        public void Handle(CategoryChangedMessage message)
        {
            // Wenn die Nachricht eine null übergibt, dann legen wir eine neue Kategorie an.
            if (message.NewCategory == null)
            {
                NewCategory();
            }
            else
            {
                EditCategory(message.NewCategory);
            }

            NotifyOfPropertyChange(string.Empty);
            NotifyOfPropertyChange(() => CanDeleteAsync);
        }

        public async void SaveAsync()
        {
            if (IsValid)
            {
                if (_isNew)
                {
                    await _categoryService.AddAsync(_category);
                }
                else
                {

                }

                PublishCategorySaved(_category);
                DisplayName = _category.Name;
            }
        }

        private void EditCategory(Category category)
        {
            _category = category;
            _isNew = false;

            DisplayName = _category.Name;
        }

        private void NewCategory()
        {
            _category = new Category();
            _isNew = true;

            DisplayName = "Neue Kategorie anlegen";
        }

        private void PublishCategorySaved(Category category)
        {
            var message = new CategorySavedMessage()
            {
                Category = category
            };

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}