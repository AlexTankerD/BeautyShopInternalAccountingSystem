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
        #region Поля клиента
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
        #endregion

        #region Поля оформления услуги
        public string StartDate { get; set; }
        public string Comment { get; set; }
        #endregion

        #region Выбранные услуги, товары и заказы
        public Service SelectedService { get; set; }
        public Product SelectedProduct { get; set; }
        public ServiceOrder SelectedServiceOrder { get; set; }
        #endregion

        #region Все услуги и продукты
        public ObservableCollection<Service> AllServices = ServiceDataWorker.GetServicesForClient();
        public ObservableCollection<Product> AllProducts = ProductDataWorker.GetProducts();
        public ObservableCollection<ServiceOrder> AllServiceOrders = ServiceOrderDataWorker.GetServiceOrders();
        #endregion

        #region Поиск услуг, товаров, заказов 
        private string _searchservicetext;
        public string SearchServiceText
        {
            get
            {
                return _searchservicetext;
            }
            set
            { 
                _searchproducttext = value;
                OnPropertyChanged("FilteredServices");
            }
        }
        public IEnumerable<Service> FilteredServices
        {
            get
            { 
                if(SearchServiceText != null)
                {
                    var SearchName = AllServices.Where(x => x.Name.ToUpper().StartsWith(SearchServiceText.ToUpper()));
                    return SearchName;
                }
                else { return AllServices; }
                
            }
        }
        private string _searchproducttext;
        public string SearchProductText
        {
            get
            {
                return _searchproducttext;
            }
            set
            {
                _searchproducttext = value;
                OnPropertyChanged("FilteredProducts");
            }
        }
        public IEnumerable<Product> FilteredProducts
        {
            get
            {
                if (SearchProductText != null)
                {
                    var SearchName = AllProducts.Where(x => x.Name.ToUpper().StartsWith(SearchProductText.ToUpper()));
                    return SearchName;
                }
                else { return AllProducts; }

            }
        }
        private string _searchserviceordertext;
        public string SearchServiceOrderText
        {
            get
            {
                return _searchserviceordertext;
            }
            set
            {
                _searchserviceordertext = value;
                OnPropertyChanged("FilteredServiceOrders");
            }
        }
        public IEnumerable<ServiceOrder> FilteredServiceOrders
        {
            get
            {
                if (SearchServiceOrderText != null)
                {
                    var SearchName = AllServiceOrders.Where(x => x.Service.Name.ToUpper().StartsWith(SearchServiceOrderText.ToUpper()) || 
                    x.StartDate.ToUpper().StartsWith(SearchServiceOrderText.ToUpper()));
                    return SearchName;
                }
                else { return AllServiceOrders; }

            }
        }
        #endregion

        #region Изменение данных клиента
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
        #endregion

        #region Оформление заказа на услугу
        private AsyncRelayCommand _orderservicecommand;
        public AsyncRelayCommand OrderServiceCommand
        {
            get
            {
                return _orderservicecommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    Window window = obj as Window;
                    await Task.Run(() => OrderServiceCommandMethod(window));
                }
                );
            }
        }

        private void OrderServiceCommandMethod(Window window)
        {
            if (!ServiceOrderRegexp.IsStartDateValid(StartDate))
            {
                OpenMessageWindow("Неправильная дата начала услуги");
                return;
            }
            if (!ServiceOrderRegexp.IsCommentValid(Comment))
            {
                OpenMessageWindow("Длина комментария не может превышать 50 символов");
                return;
            }
            var order = ServiceOrderDataWorker.OrderService(Client, SelectedService, StartDate, Comment);
            if (order)
            {
                OpenMessageWindow("Запись на услугу успешно оформлена");
                CloseWindow(window);
                SetNullServiceOrderValueProperties();
                UpdateClientServiceOrdersPage();
                return;
            }
            else
            {
                OpenMessageWindow("Пользователь с таким именем пользователя, Email или номером телефона уже существует");
                return;
            }
        }
        private void SetNullServiceOrderValueProperties()
        {
            StartDate = null;
            Comment = null;
        }
        private AsyncRelayCommand _deleteclientserviceordercommand;
        public AsyncRelayCommand DeleteClientServiceOrderCommand
        {
            get
            {
                return _deleteclientserviceordercommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    if (SelectedServiceOrder == null)
                    {
                        OpenMessageWindow("Не выбрана запись для удаления");
                        return;
                    }
                    await Task.Run(() => DeleteClientServiceOrderCommandMethod());
                }
                );
            }
        }

        private void DeleteClientServiceOrderCommandMethod()
        {
            var deleteorder = ServiceOrderDataWorker.DeleteServiceOrder(SelectedServiceOrder);
            if (deleteorder)
            {
                OpenMessageWindow("Запись на услугу успешно удалена");
                UpdateClientServiceOrdersPage();
                return;
            }
        }
        public void UpdateClientServiceOrdersPage()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AllServiceOrders = ServiceOrderDataWorker.GetServiceOrders();
                if (ClientServiceOrdersPage.ListServiceOrdersBox == null)
                    return;
                ClientServiceOrdersPage.ListServiceOrdersBox.ItemsSource = null;
                ClientServiceOrdersPage.ListServiceOrdersBox.Items.Clear();
                ClientServiceOrdersPage.ListServiceOrdersBox.ItemsSource = FilteredServiceOrders;
            });
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
                    ClientImageDirectory = ClientDataWorker.AddImage(window);
                    if (string.IsNullOrEmpty(ClientImageDirectory))
                        OpenMessageWindow("Неправильный путь к изображению");

                }
                );

            }
        }
        #endregion

        #region Команды открытия окон и страниц
        private AsyncRelayCommand _openproductspagecommand;
        public AsyncRelayCommand OpenProductsPageCommand
        {
            get
            {

                return _openproductspagecommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    Frame frame = obj as Frame;
                    await Task.Run(() => OpenProductsPage(frame));
                });
            }
        }
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
        private AsyncRelayCommand _openserviceorderwindowcommand;
        public AsyncRelayCommand OpenServiceOrderWindowCommand
        {
            get
            {

                return _openserviceorderwindowcommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    if(SelectedService == null)
                    {
                        OpenMessageWindow("Не выбрана услуга");
                        return;
                    }
                    await Task.Run(() => OpenServiceOrderWindow());
                });
            }
        }
        private AsyncRelayCommand _openclientserviceorderspagecommand;
        public AsyncRelayCommand OpenClientServiceOrdersPageCommand
        {
            get
            {

                return _openclientserviceorderspagecommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    Frame frame = obj as Frame;
                    await Task.Run(() => OpenClientServiceOrdersPage(frame));
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
        private void OpenProductsPage(Frame frame)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                frame.Navigate(new ProductsPage(this));
            });
        }
        private void OpenServicesPage(Frame frame)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                frame.Navigate(new ServicesPage(this));
            });
        }
        private void OpenServiceOrderWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ServiceOrderWindow wnd = new ServiceOrderWindow(this);
                wnd.ShowDialog();
            });
        }
        private void OpenClientServiceOrdersPage(Frame frame)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                frame.Navigate(new ClientServiceOrdersPage(this));
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
                changeclientdatawindow.ShowDialog();
            });
        }
        private void OpenMessageWindow(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageWindow messagewindow = new MessageWindow(message);
                messagewindow.ShowDialog();
            });
        }
        private void CloseWindow(Window window)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                window.Close();
            });

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
