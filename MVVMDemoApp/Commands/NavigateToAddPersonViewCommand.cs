using MVVMDemoApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMDemoApp.Commands
{
    public class NavigateToAddPersonViewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MainPage.Instance.ViewModel.CurrentView = AddPersonView.Instance;
        }
    }
}
