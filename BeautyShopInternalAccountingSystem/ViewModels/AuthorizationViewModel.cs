using BeautyShopInternalAccountingSystem.Model;
using BeautyShopInternalAccountingSystem.View;
using BeautyShopInternalAccountingSystem.View.AuthorizationWindows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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
        public DateOnly Birthday { get; set; }
        public char Sex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ClientImageDirectory { get; set; }
        public string ConfirmPassword { get; set; }

        public bool ClientBtnIsChecked { get; set; }
        public bool ManagerBtnIsChecked { get; set; }
        public bool EmployeeBtnIsChecked { get; set; }
        #region Команды и методы добавления изображения
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
            imagepath.CopyTo($"Resources\\ClientImages\\{Path.GetFileNameWithoutExtension(openFileDialog.FileName)}", true);
            ClientImageDirectory = $"Resources\\ClientImages\\{Path.GetFileNameWithoutExtension(openFileDialog.FileName)}";
        }
        public void SetImageInWindow(Window wnd, Uri uri)
        {
            Image image = wnd.FindName("ClientImage") as Image;
            image.Source = new BitmapImage(uri);
        }
        #endregion

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
        private void OpenRegistrationWindow()
        {
            RegistrationWindow registrationwindow = new RegistrationWindow();
            registrationwindow.Show();
            
        }
        private void OpenAuthorizationWindow()
        {
            AuthorizationWindow authorizationwindow = new AuthorizationWindow();
            authorizationwindow.Show();
        }
        private void OpenForgotPasswordWindow()
        {
            ForgotPasswordWindow authorizationwindow = new ForgotPasswordWindow();
            authorizationwindow.Show();
        }
        private void OpenMessageWindow(string message)
        {
            MessageWindow messagewindow = new MessageWindow(message);
            SetCenterPositionAndOwner(messagewindow);
        }
        private void SetCenterPositionAndOwner(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
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
