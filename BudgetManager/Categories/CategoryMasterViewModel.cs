using System.Collections.ObjectModel;
using BudgetManager.Domain;
using BudgetManager.Handles;
using BudgetManager.Services;
using Caliburn.Micro;

namespace BudgetManager.Categories
{
    public class CategoryMasterViewModel : Screen
    {
        private readonly ICategoryService _categoryService;
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<Category> _categories;
        private Category _selectedItem;

        public CategoryMasterViewModel(IEventAggregator eventAggregator, ICategoryService categoryService)
        {
            _eventAggregator = eventAggregator;
            _categoryService = categoryService;
        }

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            private set
            {
                _categories = value;
                NotifyOfPropertyChange(() => Categories);
            }
        }

        public Category SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
                PublishCategoryChanged(_selectedItem);
            }
        }

        public void AddNewCategory()
        {
            // Null übergeben um eine neue Kategorie anzulegen.
            PublishCategoryChanged(null);
        }

        protected async override void OnViewLoaded(object view)
        {
            Categories = new ObservableCollection<Category>(await _categoryService.GetAll());

            base.OnViewLoaded(view);
        }

        private void PublishCategoryChanged(Category category)
        {
            CategoryChangedMessage message = new CategoryChangedMessage
            {
                NewCategory = category
            };

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}