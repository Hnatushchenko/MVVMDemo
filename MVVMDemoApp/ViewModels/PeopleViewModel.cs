using CommunityToolkit.Mvvm.Input;
using MVVMDemoApp.Models;
using MVVMDemoApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace MVVMDemoApp.ViewModels
{
    public class PeopleViewModel
    {
        public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();

        public RelayCommand NavigateToAddPersonViewCommand
        {
            get
            {
                if (_navigateToAddPersonViewCommand is null)
                {
                    _navigateToAddPersonViewCommand = new RelayCommand(() =>
                    {
                        MainPage.Instance.ViewModel.CurrentView = AddPersonView.Instance;
                    });
                }
                return _navigateToAddPersonViewCommand;
            }
        }
        public RelayCommand<Person> DeletePersonCommand
        {
            get
            {
                if (_deletePersonCommand is null)
                {
                    _deletePersonCommand = new RelayCommand<Person>(async (person) =>
                    {
                        var dialog = new MessageDialog("Are you sure you want to delete this person?", "Delete Person");
                        dialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler((cmd) => People.Remove(person))));
                        dialog.Commands.Add(new UICommand("No"));
                        await dialog.ShowAsync();
                    });
                }
                return _deletePersonCommand;
            }
        }

        private RelayCommand _navigateToAddPersonViewCommand;
        private RelayCommand<Person> _deletePersonCommand;
        
        public PeopleViewModel()
        {
            People.Add(new Person("John", "Smith"));
        }
    }
}
