﻿using CommunityToolkit.Mvvm.Input;
using MVVMDemoApp.Models;
using MVVMDemoApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVMDemoApp.ViewModels
{
    public class AddPersonViewModel : INotifyPropertyChanged
    {
        public RelayCommand AddPersonCommand
        {
            get
            {
                if (_addPersonCommand == null)
                {
                    _addPersonCommand = new RelayCommand(() =>
                    {
                        var person = new Person(FirstName, LastName);
                        PeopleView.Instance.ViewModel.People.Add(person);
                        FirstName = LastName = string.Empty;
                        NavigateToPeopleViewCommand.Execute(null);
                    },
                    () => !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName));
                }
                return _addPersonCommand;
            }
        }
        public RelayCommand NavigateToPeopleViewCommand
        {
            get
            {
                if (_navigateToPeopleViewCommand == null)
                {
                    _navigateToPeopleViewCommand = new RelayCommand(() =>
                    {
                        MainPage.Instance.ViewModel.CurrentView = PeopleView.Instance;
                    });
                }
                return _navigateToPeopleViewCommand;
            }
        }
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

        public event PropertyChangedEventHandler PropertyChanged;

        private RelayCommand _addPersonCommand;
        private RelayCommand _navigateToPeopleViewCommand;
        private string _firstName;
        private string _lastName;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            AddPersonCommand.NotifyCanExecuteChanged();
        }
    }
}