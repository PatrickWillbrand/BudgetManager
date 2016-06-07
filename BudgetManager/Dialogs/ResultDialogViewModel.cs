using System.Windows;
using System.Windows.Input;
using BudgetManager.MVVM;
using MaterialDesignThemes.Wpf;

namespace BudgetManager.Dialogs
{
    public class ResultDialogViewModel : MessageDialogViewModel
    {
        private ICommand _noCommand;
        private MessageBoxResult _result;
        private ICommand _yesCommand;

        public ICommand NoCommand
        {
            get
            {
                if (_noCommand == null)
                {
                    _noCommand = new RelayCommand(() => No());
                }

                return _noCommand;
            }
        }

        public MessageBoxResult Result
        {
            get { return _result; }
            set
            {
                _result = value;
                NotifyOfPropertyChange(() => Result);
            }
        }

        public ICommand YesCommand
        {
            get
            {
                if (_yesCommand == null)
                {
                    _yesCommand = new RelayCommand(() => Yes());
                }

                return _yesCommand;
            }
        }

        public void No()
        {
            Result = MessageBoxResult.No;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public void Yes()
        {
            Result = MessageBoxResult.Yes;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}