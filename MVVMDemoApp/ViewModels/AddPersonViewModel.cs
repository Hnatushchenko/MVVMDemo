using CommunityToolkit.Mvvm.Input;
using MVVMDemoApp.Commands;
using MVVMDemoApp.Models;
using MVVMDemoApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMDemoApp.ViewModels
{
    public class AddPersonViewModel : INotifyPropertyChanged
    {
        public RelayCommand AddPersonCommand { get; private set; }
        public ICommand NavigateToPeopleViewCommand { get; private set; } = new NavigateToPeopleViewCommand();
        
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; RaisePropertyChanged(); }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; RaisePropertyChanged(); }
        }

        public AddPersonViewModel()
        {
            AddPersonCommand = new RelayCommand(() =>
            {
                var person = new Person(FirstName, LastName);
                PeopleView.Instance.ViewModel.People.Add(person);
                FirstName = LastName = string.Empty;
                NavigateToPeopleViewCommand.Execute(null);
            },
            canExecute: () => !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string firstName;
        private string lastName;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            AddPersonCommand.NotifyCanExecuteChanged();
        }
    }
}
