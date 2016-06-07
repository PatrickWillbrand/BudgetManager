using System;
using BudgetManager.Domain;
using Caliburn.Micro;

namespace BudgetManager.Categories
{
    public class CategoryListItemViewModel : PropertyChangedBase
    {
        private Category _category;
        private bool _isSelected;

        public CategoryListItemViewModel(Category category)
        {
            _category = category;
        }

        public Category Category
        {
            get { return _category; }
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

        public Guid Id
        {
            get { return _category.Id; }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }
    }
}