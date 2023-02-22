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
            get { return _firstName; }
            set { _firstName = value; RaisePropertyChanged(); }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; RaisePropertyChanged(); }
        }

        public EditPersonViewModel()
        {
            SavePersonCommand = new RelayCommand(() =>
            {
                var people = PeopleView.Instance.ViewModel.People;
                _personToEdit.FirstName = FirstName;
                _personToEdit.LastName = LastName;
                FirstName = LastName = string.Empty;
                var indexInList = people.IndexOf(_personToEdit);
                if (indexInList == -1)
                {
                    people.Add(_personToEdit);
                }
                else
                {
                    people[indexInList] = _personToEdit;
                }
                NavigateToPeopleViewCommand.Execute(null);
            },
            canExecute: () => !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName));
        }

        private string _firstName;
        private string _lastName;
        private readonly Person _personToEdit;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            SavePersonCommand.NotifyCanExecuteChanged();
        }

        public EditPersonViewModel(Person personToEdit)
        {
            _personToEdit = personToEdit;
            FirstName = personToEdit.FirstName;
            LastName = personToEdit.LastName;
        }
    }
}
