using CommunityToolkit.Mvvm.Input;
using MVVMDemoApp.Commands;
using MVVMDemoApp.Models;
using MVVMDemoApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        public ICommand NavigateToAddPersonViewCommand { get; } = new NavigateToAddPersonViewCommand();
        
        public RelayCommand<Person> DeletePersonCommand
        {
            get
            {
                if (_deletePersonCommand is null)
                {
                    _deletePersonCommand = new RelayCommand<Person>(async (person) =>
                    {
                        var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
                        var dialog = new MessageDialog(resourceLoader.GetString("Confirm deleting person dialog message"), resourceLoader.GetString("Delete person"));
                        dialog.Commands.Add(new UICommand(resourceLoader.GetString("Yes"), new UICommandInvokedHandler((cmd) => People.Remove(person))));
                        dialog.Commands.Add(new UICommand(resourceLoader.GetString("No")));
                        await dialog.ShowAsync();
                    });
                }
                return _deletePersonCommand;
            }
        }
        public RelayCommand<Person> EditPersonCommand
        {
            get
            {
                if (_editPersonCommand is null)
                {
                    _editPersonCommand = new RelayCommand<Person>((person) =>
                    {
                        var editPersonView = new EditPersonView(person);
                        MainPage.Instance.ViewModel.CurrentView = editPersonView;
                    });
                }
                return _editPersonCommand;
            }
        }

        private RelayCommand<Person> _deletePersonCommand;
        private RelayCommand<Person> _editPersonCommand;

        public PeopleViewModel()
        {
            People.Add(new Person("John", "Smith"));
        }
    }
}
