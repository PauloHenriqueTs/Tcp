using Amr.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Amr.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }

        private string _email;
        private string _password;
        public ICommand LoginCommand { get; set; }
        private NavigationService navigation { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(login, e => true);
        }

        public string email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(email));
            }
        }

        public string password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(password));
            }
        }

        private void login(object o)
        {
            Uri uri = new Uri("MainWindow.xaml", UriKind.Relative);
        }
    }
}