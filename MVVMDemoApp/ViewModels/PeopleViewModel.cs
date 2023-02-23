using CommunityToolkit.Mvvm.Input;
using MVVMDemoApp.Models;
using MVVMDemoApp.Services;
using MVVMDemoApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Networking;
using Windows.UI.Popups;

namespace MVVMDemoApp.ViewModels
{
    public class PeopleViewModel : INotifyPropertyChanged
    {
        public RefreshableObservableCollection<Person> People { get; set; } = new RefreshableObservableCollection<Person>();
        
        public RelayCommand<Person> DeletePersonCommand { get; private set; }
        public RelayCommand<Person> EditPersonCommand { get; private set; }
        public RelayCommand AddPersonCommand { get; private set; }
        public RelayCommand SavePersonCommand { get; private set; }
        public RelayCommand CancelOperationCommand { get; private set; }
        public RelayCommand NavigateToEditPersonViewCommand { get; private set; }
        public RelayCommand NavigateToPeopleViewCommand { get; private set; }
        public RelayCommand NavigateToAddPersonViewCommand { get; private set; }

        public Person PersonToEdit { get; set; }

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

        public PeopleViewModel(PeopleView peopleView, MainPageViewModel shellViewModel)
        {
            this.peopleView = peopleView;
            this.shellViewModel = shellViewModel;
            SavePersonCommand = new RelayCommand(SavePerson, IsPersonValid);
            AddPersonCommand = new RelayCommand(AddPerson, IsPersonValid);
            DeletePersonCommand = new RelayCommand<Person>(DeletePerson);
            EditPersonCommand = new RelayCommand<Person>(EditPerson);
            CancelOperationCommand = new RelayCommand(CancelOperation);
            NavigateToEditPersonViewCommand = new RelayCommand(NavigateToEditPersonView);
            NavigateToPeopleViewCommand = new RelayCommand(NavigateToPeopleView);
            NavigateToAddPersonViewCommand = new RelayCommand(NavigateToAddPersonView);
        }

        private string firstName;
        private string lastName;
        private readonly PeopleView peopleView;
        private readonly MainPageViewModel shellViewModel;

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            AddPersonCommand.NotifyCanExecuteChanged();
            SavePersonCommand.NotifyCanExecuteChanged();
        }

        private bool IsPersonValid()
        {
            return !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName);
        }

        private void AddPerson()
        {
            var person = new Person(FirstName, LastName);
            People.Add(person);
            FirstName = LastName = string.Empty;
            NavigateToPeopleView();
        }

        private void EditPerson(Person personToEdit)
        {
            FirstName = personToEdit.FirstName;
            LastName = personToEdit.LastName;
            PersonToEdit = personToEdit;
            NavigateToEditPersonView();
        }

        private void SavePerson()
        {
            PersonToEdit.FirstName = FirstName;
            PersonToEdit.LastName = LastName;
            People.Refresh();
            FirstName = LastName = string.Empty;
            NavigateToPeopleView();
        }

        private async void DeletePerson(Person person)
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            var dialog = new MessageDialog(resourceLoader.GetString("Confirm deleting person dialog message"), resourceLoader.GetString("Delete person"));
            dialog.Commands.Add(new UICommand(resourceLoader.GetString("Yes"), new UICommandInvokedHandler((cmd) => People.Remove(person))));
            dialog.Commands.Add(new UICommand(resourceLoader.GetString("No")));
            await dialog.ShowAsync();
        }

        private void CancelOperation()
        {
            FirstName = LastName = "";
            NavigateToPeopleView();
        }

        private void NavigateToEditPersonView()
        {
            shellViewModel.CurrentView = new EditPersonView(this);
        }

        private void NavigateToPeopleView()
        {
            shellViewModel.CurrentView = peopleView;
        }

        private void NavigateToAddPersonView()
        {
            shellViewModel.CurrentView = new AddPersonView(this);
        }
    }
}
