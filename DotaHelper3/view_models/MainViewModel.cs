using DotaHelper3.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DotaHelper3.view_models
{
    class MainViewModel : ObservableObject, INotifyPropertyChanged
    {
        public RelayCommand AboutViewCommand { get; set; }
        public RelayCommand HelperCommand { get; set; }
        public RelayCommand SettingsCommand { get; set; }


        public WelcomeViewModel WelcomeVM { get; set; }
        public HelperViewModel HelperVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            WelcomeVM = new WelcomeViewModel();
            HelperVM = new HelperViewModel();
            SettingsVM = new SettingsViewModel();

            CurrentView = WelcomeVM;

            AboutViewCommand = new RelayCommand(o =>
            {
                CurrentView = WelcomeVM;
            });

            HelperCommand = new RelayCommand(o =>
            {
                CurrentView = HelperVM;
            });

            SettingsCommand = new RelayCommand(o =>
            {
                CurrentView = SettingsVM;
            });
        }
    }
}
