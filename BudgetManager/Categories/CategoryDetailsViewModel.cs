using System.Windows;
using BudgetManager.Dialogs;
using BudgetManager.Domain;
using BudgetManager.Messages;
using BudgetManager.Services;
using BudgetManager.Validation;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;

namespace BudgetManager.Categories
{
    public class CategoryDetailsViewModel : ValidatableScreen<CategoryDetailsViewModel>, IHandle<CategorySelectedChangedMessage>
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
            var viewModel = new ResultDialogViewModel
            {
                Message = "Wollen Sie die ausgewählte Kategorie wirklich löschen?",
                Title = "Kategorie löschen"
            };

            var dialog = new ResultDialogView
            {
                DataContext = viewModel
            };

            await DialogHost.Show(dialog, "RootDialog");

            if (viewModel.Result == MessageBoxResult.Yes)
            {
                await _categoryService.RemoveAsync(_category);
                PublishCategorySaved(_category, true);
                NewCategory();
            }
        }

        public void Handle(CategorySelectedChangedMessage message)
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
                    await _categoryService.EditAsync(_category);
                }

                PublishCategorySaved(_category, false);
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

        private void PublishCategorySaved(Category category, bool isRemoved)
        {
            var message = new CategorySavedMessage
            {
                Category = category,
                IsRemoved = isRemoved
            };

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}