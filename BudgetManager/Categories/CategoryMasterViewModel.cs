using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BudgetManager.Domain;
using BudgetManager.Messages;
using BudgetManager.Services;
using Caliburn.Micro;

namespace BudgetManager.Categories
{
    public class CategoryMasterViewModel : Screen, IHandle<CategorySavedMessage>
    {
        private readonly ICategoryService _categoryService;
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<CategoryListItemViewModel> _categories;
        private CategoryListItemViewModel _selectedItem;

        public CategoryMasterViewModel(IEventAggregator eventAggregator, ICategoryService categoryService)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _categoryService = categoryService;
        }

        public ObservableCollection<CategoryListItemViewModel> Categories
        {
            get { return _categories; }
            private set
            {
                _categories = value;
                NotifyOfPropertyChange(() => Categories);
            }
        }

        public CategoryListItemViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
                PublishCategoryChanged(SelectedItem);
            }
        }

        public void AddNewCategory()
        {
            SelectedItem = null;
        }

        public void Handle(CategorySavedMessage message)
        {
            if (Categories.Any(x => x.Id == message.Category.Id))
            {
                CategoryListItemViewModel listItem = Categories.FirstOrDefault(x => x.Id == message.Category.Id);
                listItem.CategoryName = message.Category.Name;
                listItem.Description = message.Category.Description;
            }
            else
            {
                Categories.Add(new CategoryListItemViewModel(message.Category));
            }
        }

        protected async override void OnViewLoaded(object view)
        {
            IEnumerable<Category> categories = await _categoryService.GetAll();
            Categories = new ObservableCollection<CategoryListItemViewModel>();

            foreach (var item in categories)
            {
                Categories.Add(new CategoryListItemViewModel(item));
            }

            base.OnViewLoaded(view);
        }

        private void PublishCategoryChanged(CategoryListItemViewModel viewModel)
        {
            var message = new CategoryChangedMessage();
            if (viewModel != null)
            {
                message.NewCategory = viewModel.Category;
            }
            else
            {
                // Um eine neue Kategorie anzulegen übergeben wir null.
                message.NewCategory = null;
            }

            _eventAggregator.PublishOnUIThread(message);
        }
    }
}