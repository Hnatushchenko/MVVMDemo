using CommunityToolkit.Mvvm.Input;
using MVVMDemoApp.Models;
using MVVMDemoApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVMDemoApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private object currentView;
        public object CurrentView
        {
            get { return currentView; }
            set { currentView = value; RaisePropertyChanged(); }
        }

        public MainPageViewModel()
        {
            CurrentView = new PeopleView(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
