using Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Library.Views;
using Library.Services;
using Library.Controls;

namespace Library.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        #region -- Поля  --
        private ApplicationDbContext _db = null!;
        private SignInWindow _view = null!;

        private string _login = null!;
        private string _password = null!;
        #endregion

        #region -- Свойства --
        public string Login
        {
            get => _login;
            set => Set(ref _login, value, nameof(Login));
        }
        public string Password
        {
            get => _password;
            set => Set(ref _password, value, nameof(Password));
        }
        #endregion

        #region -- Сервисы --
        public UserService UserService { get; set; } = null!;
        #endregion


        public SignInViewModel(SignInWindow signInWindow)
        {
            _db = new ApplicationDbContext();
            _view = signInWindow;

            UserService = new(_db);
        }


        #region -- Методы --
        private bool SignIn()
        {
            var isExist = false;
            try
            {
                isExist = _db.Users.Any(c => c.Login == Login && c.Password == Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return isExist;
        }

        private bool PropertiesIsNull()
            => (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Login)) ? true : false;

        private void OpenMainWindow()
        {
            Password = _view.PasswordBox.Password;
            if (!PropertiesIsNull())
            {
                if (SignIn())
                {
                    var MainWindow = new MainWindow(_db, UserService.GetUser(Login, Password));
                    var CurrentWindow = Application.Current.MainWindow;
                    MainWindow.Show();
                    Application.Current.MainWindow = MainWindow;
                    CurrentWindow.Close();
                }
                else
                {
                    MessageBox.Show("Учётная запись не найдена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    Login = null!;
                    Password = null!;
                    _view.PasswordBox.Password = null;
                }
            }
            else
                MessageBox.Show("Все поля должны быть заполнены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion


        public ICommand AuthorizationButton => new Command(x => OpenMainWindow());
    }
}
