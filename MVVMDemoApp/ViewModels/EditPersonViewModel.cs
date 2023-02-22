using CommunityToolkit.Mvvm.Input;
using MVVMDemoApp.Commands;
using MVVMDemoApp.Models;
using MVVMDemoApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMDemoApp.ViewModels
{
    public class EditPersonViewModel : INotifyPropertyChanged
    {
        public RelayCommand SavePersonCommand { get; private set; }
        public ICommand NavigateToPeopleViewCommand { get; set; } = new NavigateToPeopleViewCommand();

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

        private string firstName;
        private string lastName;
        private readonly Person personToEdit;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            SavePersonCommand.NotifyCanExecuteChanged();
        }

        public EditPersonViewModel(Person personToEdit)
        {
            SavePersonCommand = new RelayCommand(() =>
            {
                personToEdit.FirstName = FirstName;
                personToEdit.LastName = LastName;
                PeopleView.Instance.ViewModel.People.Refresh();
                FirstName = LastName = string.Empty;
                NavigateToPeopleViewCommand.Execute(null);
            },
            canExecute: () => !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName));

            this.personToEdit = personToEdit;
            FirstName = personToEdit.FirstName;
            LastName = personToEdit.LastName;
        }
    }
}
