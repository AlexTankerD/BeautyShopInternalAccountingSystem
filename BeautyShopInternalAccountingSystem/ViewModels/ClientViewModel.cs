using BeautyShopInternalAccountingSystem.Models;
using BeautyShopInternalAccountingSystem.Models.Data;
using BeautyShopInternalAccountingSystem.Models.DataWorkers;
using BeautyShopInternalAccountingSystem.Models.RegularExpressions;
using BeautyShopInternalAccountingSystem.Models.RelayCommands;
using BeautyShopInternalAccountingSystem.Views;
using BeautyShopInternalAccountingSystem.Views.AuthorizationWindows;
using BeautyShopInternalAccountingSystem.Views.ClientWindows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BeautyShopInternalAccountingSystem.ViewModels
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Birthday { get; set; }
        public string? Sex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ClientImageDirectory { get; set; }
        public string Password { get; set; }
        private Client _client;
        public Client Client
        {
            get { return _client; }
            set { _client = value; OnPropertyChanged(nameof(Client)); }
        }
        public Service SelectedService { get; set; }
        public ObservableCollection<Service> AllServices = ServiceDataWorker.GetServices();
        private string _searchtext;
        public string SearchText
        {
            get
            {
                return _searchtext;
            }
            set
            { 
                _searchtext = value;
                OnPropertyChanged("FilteredServices");
            }
        }
        public IEnumerable<Service> FilteredServices
        {
            get
            { 
                if(SearchText != null)
                {
                    var SearchName = AllServices.Where(x => x.Name.ToUpper().StartsWith(SearchText.ToUpper()));
                    return SearchName;
                }
                else { return AllServices; }
                
            }
        }
        private AsyncRelayCommand _changeclientdatacommand;
        public AsyncRelayCommand ChangeClientDataCommand
        {
            get
            {
                return _changeclientdatacommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    Window window = obj as Window;
                    await Task.Run(() => ChangeClientDataCommandMethod(window));
                }
                );
            }
        }

        private void ChangeClientDataCommandMethod(Window window)
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
            if (ClientImageDirectory == null)
                ClientImageDirectory = @"Images\ClientImages\user.png";
            var reg = ClientDataWorker.ChangeClientData(Client, Username, Password, Name, Surname, Patronymic, Birthday,
                       Sex, Email, PhoneNumber, ClientImageDirectory);
            if (reg)
            {
                OpenMessageWindow("Данные успешно изменены");
                using(ApplicationContext db = new ApplicationContext())
                {
                    Client = db.Clients.Where(x => x.Username == Username && x.Name == Name &&
                    x.Surname == Surname && x.Patronymic == Patronymic &&
                    x.Birthday == Birthday && x.Sex == Sex &&
                    x.PhoneNumber == PhoneNumber && x.Email == Email).FirstOrDefault();
                }
                CloseWindow(window);
                SetNullValueProperties();
                return;
            }
            else
            {
                OpenMessageWindow("Пользователь с таким именем пользователя, Email или номером телефона уже существует");
                return;
            }
        }
        private void SetNullValueProperties()
        {
            Username = null;
            Password = null;
            Name = null;
            Surname = null;
            Patronymic = null;
            Birthday = null;
            Sex = null;
            Email = null;
            PhoneNumber = null;
            ClientImageDirectory = null;
        }

        #region Команды добавления изображения
        private RelayCommand _addimagecommand;
        public RelayCommand AddImageCommand
        {
            get
            {
                return _addimagecommand ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    ClientImageDirectory = ClientDataWorker.AddImage(window);
                    if (string.IsNullOrEmpty(ClientImageDirectory))
                        OpenMessageWindow("Неправильный путь к изображению");

                }
                );

            }
        }
        #endregion

        #region Команды открытия окон и страниц
        private AsyncRelayCommand _openservicespagecommand;
        public AsyncRelayCommand OpenServicesPageCommand
        {
            get
            {

                return _openservicespagecommand ?? new AsyncRelayCommand(async(obj) =>
                {
                    Frame frame = obj as Frame;
                    await Task.Run(() => OpenServicesPage(frame));
                });
            }
        }
        private AsyncRelayCommand _openclientdatapagecommand;
        public AsyncRelayCommand OpenClientDataPageCommand
        {
            get
            {

                return _openclientdatapagecommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    Frame frame = obj as Frame;
                    await Task.Run(() => OpenClientDataPage(frame));
                });
            }
        }
        private AsyncRelayCommand _openchangeclientdatawindowcommand;
        public AsyncRelayCommand OpenChangeClientDataWindowCommand
        {
            get
            {

                return _openchangeclientdatawindowcommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    await Task.Run(() =>OpenChangeClientDataWindow());
                });
            }
        }
        #endregion

        #region Методы открытия окон и страниц
        private void OpenServicesPage(Frame frame)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                frame.Navigate(new ServicesPage(this));
            });
        }
        private void OpenClientDataPage(Frame frame)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                frame.Navigate(new ClientDataPage(this));
            });
        }
        private void OpenChangeClientDataWindow()
        { 
            Application.Current.Dispatcher.Invoke(() =>
            {
                ChangeClientDataWindow changeclientdatawindow = new ChangeClientDataWindow(this);
                changeclientdatawindow.Show();
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
        private void CloseWindow(Window window)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                window.Close();
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
