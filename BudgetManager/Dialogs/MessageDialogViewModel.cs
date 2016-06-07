using Caliburn.Micro;

namespace BudgetManager.Dialogs
{
    public class MessageDialogViewModel : Screen
    {
        private string _message;
        private string _title;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }
    }
}