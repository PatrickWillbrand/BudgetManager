using Caliburn.Micro;

namespace BudgetManager.Categories
{
    public class CategoryWorkspaceViewModel : Conductor<object>.Collection.AllActive
    {
        private CategoryDetailsViewModel _detailsViewModel;
        private CategoryMasterViewModel _masterViewModel;

        public Screen Details
        {
            get
            {
                if (_detailsViewModel == null)
                {
                    _detailsViewModel = IoC.Get<CategoryDetailsViewModel>();
                }

                return _detailsViewModel;
            }
        }

        public Screen Master
        {
            get
            {
                if (_masterViewModel == null)
                {
                    _masterViewModel = IoC.Get<CategoryMasterViewModel>();
                }

                return _masterViewModel;
            }
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}