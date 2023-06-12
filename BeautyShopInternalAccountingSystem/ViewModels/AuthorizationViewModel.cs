using BeautyShopInternalAccountingSystem.Models;
using BeautyShopInternalAccountingSystem.Models.DataWorkers;
using BeautyShopInternalAccountingSystem.Models.RegularExpressions;
using BeautyShopInternalAccountingSystem.Models.RelayCommands;
using BeautyShopInternalAccountingSystem.Views;
using BeautyShopInternalAccountingSystem.Views.AuthorizationWindows;
using BeautyShopInternalAccountingSystem.Views.ClientWindows;
using BeautyShopInternalAccountingSystem.Views.EmployeeWindows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BeautyShopInternalAccountingSystem.ViewModels
{
    public class AuthorizationViewModel : INotifyPropertyChanged
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Birthday { get; set; }
        public char Sex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ClientImageDirectory { get; set; }
        public string ConfirmPassword { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public bool ClientBtnIsChecked { get; set; }
        public bool ManagerBtnIsChecked { get; set; }
        public bool EmployeeBtnIsChecked { get; set; }

        #region Команды и методы регистрации пользователя
        private AsyncRelayCommand _registrationcommand;
        public AsyncRelayCommand RegistrationCommand
        {
            get
            {
                return _registrationcommand ?? new AsyncRelayCommand(async(obj) =>
                {
                    Window window = obj as Window;
                    await Task.Run(() => RegistrationCommandMethod(window));
                }
                );
            }
        }
        private void RegistrationCommandMethod(Window window)
        {
            if (!ClientRegexp.IsUsernameValid(Username))
            {
                OpenMessageWindow("Неправильное имя пользователя");
                return;
            }
            if (!ClientRegexp.IsPasswordValid(Password))
            {
                OpenMessageWindow("Неправильный пароль");
                return;
            }
            if (!ClientRegexp.IsNameValid(Name))
            {
                OpenMessageWindow("Неправильное имя");
                return;
            }
            if (!ClientRegexp.IsSurnameValid(Surname))
            {
                OpenMessageWindow("Неправильная фамилия");
                return;
            }
            if (!ClientRegexp.IsPatronymicValid(Patronymic))
            {
                OpenMessageWindow("Неправильное отчество");
                return;
            }
            if (!ClientRegexp.IsBirthdayValid(Birthday))
            {
                OpenMessageWindow("Неправильная дата рождения");
                return;
            }

            if (!ClientRegexp.IsEmailValid(Email))
            {
                OpenMessageWindow("Неправильный Email");
                return;
            }
            if (!ClientRegexp.IsPhoneNumberValid(PhoneNumber))
            {
                OpenMessageWindow("Неправильный номер телефона");
                return;
            }
            if (!ClientRegexp.IsSexValid(Sex))
            {
                OpenMessageWindow("Окно 'пол' не может быть пустым");
                return;
            }
            var reg = AuthorizationDataWorker.Registration(Username, Password, Name, Surname, Patronymic, Birthday,
                       Sex, Email, PhoneNumber, ClientImageDirectory);
            switch (reg)
            {
                case "RegistrationSuccesful":
                    OpenMessageWindow("Регистрация прошла успешно");
                    OpenAuthorizationWindow();
                    CloseWindow(window);
                    return;
                case "RegistrationFail":
                    OpenMessageWindow("Пользователь с таким именем пользователя, Email или номером телефона уже существует");
                    return;
            }
        }
        #endregion

        #region Команды и методы авторизации
        private AsyncRelayCommand _authorizationcommand;
        public AsyncRelayCommand AuthorizationCommand
        {
            get
            {
                return _authorizationcommand ?? new AsyncRelayCommand(async(obj) =>
                {
                    Window window = obj as Window;
                    await Task.Run(() => AuthorizationCommandMethod(window));
                }
                );
            }
        }

        private void AuthorizationCommandMethod(Window window)
        {
            if(ClientBtnIsChecked) 
            {
                Client client = new Client();
                var auth = AuthorizationDataWorker.AuthorizationAsClient(Login, Password, ref client);
                if(auth)
                {
                    OpenMessageWindow("Авторизация прошла успешно");
                    OpenClientWindow(client);
                    CloseWindow(window);
                    return;
                }
                OpenMessageWindow("Неправильный логин или пароль");
                return;
            }
            if (EmployeeBtnIsChecked)
            {
                Employee employee = new Employee();
                var auth = AuthorizationDataWorker.AuthorizationAsEmployee(Login, Password, ref employee);
                if (auth)
                {
                    OpenMessageWindow("Авторизация прошла успешно");
                    OpenEmployeeWindow(employee);
                    CloseWindow(window);
                    return;
                }
                OpenMessageWindow("Неправильный логин или пароль");
                return;
            }
            if (ManagerBtnIsChecked)
            {
                Manager manager = new Manager();
                var auth = AuthorizationDataWorker.AuthorizationAsManager(Login, Password, ref manager);
                if (auth)
                {
                    OpenMessageWindow("Авторизация прошла успешно");
                    CloseWindow(window);
                    return;
                }
                OpenMessageWindow("Неправильный логин или пароль");
                return;
            }
            OpenMessageWindow("Не выбран вид входа");
            return;

        }

        #endregion

        #region Команды и методы смены пароля
        private AsyncRelayCommand _changepasswordcommand;
        public AsyncRelayCommand ChangePasswordCommand
        {
            get
            {
                return _changepasswordcommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    Window window = obj as Window;
                    await Task.Run(() => ChangePasswordCommandMethod(window));
                }
                );
            }
        }
        public void ChangePasswordCommandMethod(Window window)
        {
            if(string.IsNullOrEmpty(Username)) 
            {
                OpenMessageWindow("Введите имя пользователя");
            }
            if(!ClientRegexp.IsNewPasswordValid(Password, ConfirmPassword)) 
            {
                OpenMessageWindow("Пароли должны совпадать");
                return;
            }
            var chgpsw = AuthorizationDataWorker.ChangePassword(Username, Password);
            if (!chgpsw)
            {
                OpenMessageWindow("Пользователя с таким именем не существует");
                return;
            }
            OpenMessageWindow("Пароль успешно изменен");
            OpenAuthorizationWindow();
            CloseWindow(window);
            return;
        }
        #endregion

        #region Команды добавления изображения
        private RelayCommand _addimagecommand;
        public RelayCommand AddImageCommand
        {
            get
            {
                return _addimagecommand ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    AddImage(window);
                    if (string.IsNullOrEmpty(ClientImageDirectory))
                        OpenMessageWindow("Неправильный путь к изображению");

                }
                );

            }
        }
        #endregion

        #region Методы добавления с изображениями
        private void AddImage(Window wnd)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Png files (*.png)|*.png| Jpg files (*.jpg)|*.jpg";
            openFileDialog.ShowDialog();
            if (string.IsNullOrEmpty(openFileDialog.FileName))
                return;
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(openFileDialog.FileName);
            image.EndInit();
            SetImageInWindow(wnd, image.UriSource);
            FileInfo imagepath = new FileInfo(openFileDialog.FileName);
            if (!Directory.Exists(@"Resources\ClientImages"))
                Directory.CreateDirectory(@"Resources\ClientImages");
            imagepath.CopyTo($@"Resources\ClientImages\{Path.GetFileName(openFileDialog.FileName)}", true);
            ClientImageDirectory = $@"Resources\ClientImages\{Path.GetFileName(openFileDialog.FileName)}";
        }
        public void SetImageInWindow(Window wnd, Uri uri)
        {
            Image image = wnd.FindName("ClientImage") as Image;
            image.Source = new BitmapImage(uri);
        }
        #endregion

        #region Команды открытия окон
        private RelayCommand _openregistrationwindowcommand;
        public RelayCommand OpenRegistrationWindowCommand
        {
            get
            {
                
                return _openregistrationwindowcommand ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    OpenRegistrationWindow();
                    wnd.Close();
                });
            }
        }
        private RelayCommand _openauthorizationwindowcommand;
        public RelayCommand OpenAuthorizationWindowCommand
        {
            get
            {

                return _openauthorizationwindowcommand ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    OpenAuthorizationWindow();
                    wnd.Close();
                });
            }
        }
        private RelayCommand _openforgotpasswordwindowcommand;
        public RelayCommand OpenForgotPasswordWindowCommand
        {
            get
            {

                return _openforgotpasswordwindowcommand ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    OpenForgotPasswordWindow();
                    wnd.Close();
                });
            }
        }
        #endregion

        #region Методы открытия окон
        private void OpenRegistrationWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                RegistrationWindow registrationwindow = new RegistrationWindow();
                registrationwindow.Show();
            });
        }
        private void CloseWindow(Window window)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                window.Close();
            });

        }
        private void OpenAuthorizationWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AuthorizationWindow authorizationwindow = new AuthorizationWindow();
                authorizationwindow.Show();
            });
        }
        private void OpenForgotPasswordWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ForgotPasswordWindow authorizationwindow = new ForgotPasswordWindow();
                authorizationwindow.Show();
            });
            
        }
        private void OpenClientWindow(object obj)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ClientWindow clientwindow = new ClientWindow(new ClientViewModel() { Client = obj as Client });
                clientwindow.Show();
            });

        }
        private void OpenEmployeeWindow(object obj)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                EmployeeWindow employeewindow = new EmployeeWindow(new EmployeeViewModel() { Employee = obj as Employee });
                employeewindow.Show();
            });

        }
        private void OpenMessageWindow(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageWindow messagewindow = new MessageWindow(message);
                SetCenterPositionAndOwner(messagewindow);
            });
        }
        private void SetCenterPositionAndOwner(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
