using MVVMDemoApp.ViewModels;
using MVVMDemoApp.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MVVMDemoApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel ViewModel { get; set; }

        private static MainPage _instance;
        public static MainPage Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new MainPage();
                }
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        public MainPage()
        {
            ViewModel = new MainPageViewModel();
            InitializeComponent();
            Instance = this;
        }
    }
}
