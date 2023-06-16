﻿using BeautyShopInternalAccountingSystem.Models;
using BeautyShopInternalAccountingSystem.Models.DataWorkers;
using BeautyShopInternalAccountingSystem.Models.RegularExpressions;
using BeautyShopInternalAccountingSystem.Models.RelayCommands;
using BeautyShopInternalAccountingSystem.Views;
using BeautyShopInternalAccountingSystem.Views.ManagerWindows;
using BeautyShopInternalAccountingSystem.Views.ManagerWindows.ClientWindows;
using BeautyShopInternalAccountingSystem.Views.ManagerWindows.EmployeeWindows;
using BeautyShopInternalAccountingSystem.Views.ManagerWindows.ProductWindows;
using BeautyShopInternalAccountingSystem.Views.ManagerWindows.ServiceWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BeautyShopInternalAccountingSystem.ViewModels
{
    public class ManagerViewModel : INotifyPropertyChanged
    {
        public Manager Manager { get; set; }

        #region Поля товара
        public string ProductName { get; set; }
        public string? ProductCategory { get; set; }
        public double? ProductPrice { get; set; }
        public string? ProductSpecifications { get; set; }
        public string? ProductDescription { get; set; }
        public double? ProductWeight { get; set; }
        public double? ProductHeight { get; set; }
        public double? ProductWidth { get; set; }
        public double? ProductLength { get; set; }
        public Manufacturer ProductManufacturer { get; set; }
        public string ProductImageDirectory { get; set; }
        public bool? ProductIsActive { get; set; }
        #endregion

        #region Поля сервиса
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public double? ServicePrice { get; set; }
        public double? ServiceDiscount { get; set; }
        public int? ServiceDuration { get; set; }
        public string ServiceImageDirectory { get; set; }
        #endregion

        #region Поля работника
        public string EmployeeUsername { get; set; }
        public string EmployeePassword { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeePatronymic { get; set; }
        public string EmployeeBirthday { get; set; }
        public char EmployeeSex { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePhoneNumber { get; set; }
        public string EmployeePosition { get; set; }
        public double EmployeeSellaryRatio { get; set; }
        public string? EmployeeImageDirectory { get; set; }
        public string EmployeePassportData { get; set; }
        public List<Service> EmployeeServices { get; set; }
        #endregion

        #region Все товары, услуги, работники, клиенты
        private ObservableCollection<Product> _allproducts = ProductDataWorker.GetProducts();
        public ObservableCollection<Product> AllProducts
        {
            get { return _allproducts; }
            set
            {
                _allproducts = value;
                OnPropertyChanged("AllProducts");
            }
        }
        public ObservableCollection<Service> _allservices = ServiceDataWorker.GetServices();
        public ObservableCollection<Service> AllServices
        {
            get { return _allservices; }
            set
            {
                _allservices = value;
                OnPropertyChanged("AllServices");
            }
        }
        public ObservableCollection<Client> AllClients = ClientDataWorker.GetClients();
        public ObservableCollection<Employee> AllEmployees = EmployeeDataWorker.GetEmployees();
        public ObservableCollection<Manufacturer> _allmanufacturers = ManufacturerDataWorker.GetManufacturers();
        public ObservableCollection<Manufacturer> AllManufacturers
        {
            get { return _allmanufacturers; }
            set 
            { 
                _allmanufacturers = value;
                OnPropertyChanged("AllManufacturers");
            }
        }
        #endregion

        #region Выбранные товары, услуги, работники, клиенты
        public Product SelectedProduct { get; set; }
        public Service SelectedService { get; set; }
        #endregion

        #region Поиск товаров, услуг, работников, клиентов
        private string _searchproductstext;
        public string SearchProductsText
        {
            get
            {
                return _searchproductstext;
            }
            set
            {
                _searchproductstext = value;
                OnPropertyChanged("FilteredProducts");
            }
        }
        public IEnumerable<Product> FilteredProducts
        {
            get
            {
                if (SearchProductsText != null)
                {
                    var SearchName = AllProducts.Where(x => x.Name.ToUpper().StartsWith(SearchProductsText.ToUpper()) ||
                    x.Description.ToUpper().StartsWith(SearchProductsText.ToUpper()));
                    return SearchName;
                }
                else { return AllProducts; }

            }
        }
        private string _searchservicestext;
        public string SearchServicesText
        {
            get
            {
                return _searchservicestext;
            }
            set
            {
                _searchservicestext = value;
                OnPropertyChanged("FilteredServices");
            }
        }
        public IEnumerable<Service> FilteredServices
        {
            get
            {
                if (SearchServicesText != null)
                {
                    var SearchName = AllServices.Where(x => x.Name.ToUpper().StartsWith(SearchServicesText.ToUpper()));
                    return SearchName;
                }
                else { return AllServices; }

            }
        }
        private string _searchemployeestext;
        public string SearchEmployeesText
        {
            get
            {
                return _searchemployeestext;
            }
            set
            {
                _searchemployeestext = value;
                OnPropertyChanged("FilteredEmployees");
            }
        }
        public IEnumerable<Employee> FilteredEmployees
        {
            get
            {
                if (SearchEmployeesText != null)
                {
                    var SearchName = AllEmployees.Where(x => x.Name.ToUpper().StartsWith(SearchProductsText.ToUpper()));
                    return SearchName;
                }
                else { return AllEmployees; }

            }
        }
        private string _searchclientstext;
        public string SearchClientsText
        {
            get
            {
                return _searchclientstext;
            }
            set
            {
                _searchclientstext = value;
                OnPropertyChanged("FilteredClients");
            }
        }
        public IEnumerable<Client> FilteredClients
        {
            get
            {
                if (SearchClientsText != null)
                {
                    var SearchName = AllClients.Where(x => x.Name.ToUpper().StartsWith(SearchProductsText.ToUpper()));
                    return SearchName;
                }
                else { return AllClients; }

            }
        }
        #endregion

        #region Команды добавления, редактирования и удаления продуктов
        private AsyncRelayCommand _addproductcommand;
        public AsyncRelayCommand AddProductCommand
        {
            get 
            {
                return _addproductcommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    Window wnd = obj as Window;
                    if (!ProductRegexp.IsNameValid(ProductName))
                    {
                        OpenMessageWindow("Неправильное название товара");
                        return;
                    }
                    if (!ProductRegexp.IsCategoryValid(ProductCategory))
                    {
                        OpenMessageWindow("Неправильная категория продукта");
                        return;
                    }
                    if (!ProductRegexp.IsPriceValid(ProductPrice))
                    {
                        OpenMessageWindow("Неправильная цена продукта");
                        return;
                    }
                    if (!ProductRegexp.IsSpecificationsValid(ProductSpecifications))
                    {
                        OpenMessageWindow("Неправильные характеристики продукта");
                        return;
                    }
                    if (!ProductRegexp.IsDescriptionValid(ProductDescription))
                    {
                        OpenMessageWindow("Неправильное описание продукта");
                        return;
                    }
                    if (!ProductRegexp.IsWeightValid(ProductWeight))
                    {
                        OpenMessageWindow("Неправильный вес продукта");
                        return;
                    }
                    if (!ProductRegexp.IsHeightValid(ProductHeight))
                    {
                        OpenMessageWindow("Неправильная высота продукта");
                        return;
                    }
                    if (!ProductRegexp.IsWidthValid(ProductWidth))
                    {
                        OpenMessageWindow("Неправильная ширина продукта");
                        return;
                    }
                    if (!ProductRegexp.IsLengthValid(ProductLength))
                    {
                        OpenMessageWindow("Неправильная длина продукта");
                        return;
                    }
                    if (!ProductRegexp.IsManufacturerValid(ProductManufacturer))
                    {
                        OpenMessageWindow("Неправильный производитель продукта");
                        return;
                    }
                    if (!ProductRegexp.IsProductActiveValid(ProductIsActive))
                    {
                        OpenMessageWindow("Окно активности не должно быть пустым");
                        return;
                    }
                    if (ProductImageDirectory == null)
                        ProductImageDirectory = @"Товары салона красоты\productdefaultimage.png";
                    var addproduct = ProductDataWorker.AddProduct(ProductName, ProductCategory, ProductPrice, 
                        ProductSpecifications, ProductDescription, ProductWeight, ProductHeight, ProductWidth, 
                        ProductLength, ProductManufacturer.Id, ProductImageDirectory, ProductIsActive);
                    if(addproduct)
                    {
                        OpenMessageWindow("Продукт успешно добавлен");
                        UpdateProductPage();
                        SetNullProductValueProperties();
                        CloseWindow(wnd);
                    }

                });
            }
        }
        private AsyncRelayCommand _editproductcommand;
        public AsyncRelayCommand EditProductCommand
        {
            get
            {
                return _editproductcommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    Window wnd = obj as Window;
                    if (!ProductRegexp.IsNameValid(ProductName))
                    {
                        OpenMessageWindow("Неправильное название товара");
                        return;
                    }
                    if (!ProductRegexp.IsCategoryValid(ProductCategory))
                    {
                        OpenMessageWindow("Неправильная категория продукта");
                        return;
                    }
                    if (!ProductRegexp.IsPriceValid(ProductPrice))
                    {
                        OpenMessageWindow("Неправильная цена продукта");
                        return;
                    }
                    if (!ProductRegexp.IsSpecificationsValid(ProductSpecifications))
                    {
                        OpenMessageWindow("Неправильные характеристики продукта");
                        return;
                    }
                    if (!ProductRegexp.IsDescriptionValid(ProductDescription))
                    {
                        OpenMessageWindow("Неправильное описание продукта");
                        return;
                    }
                    if (!ProductRegexp.IsWeightValid(ProductWeight))
                    {
                        OpenMessageWindow("Неправильный вес продукта");
                        return;
                    }
                    if (!ProductRegexp.IsHeightValid(ProductHeight))
                    {
                        OpenMessageWindow("Неправильная высота продукта");
                        return;
                    }
                    if (!ProductRegexp.IsWidthValid(ProductWidth))
                    {
                        OpenMessageWindow("Неправильная ширина продукта");
                        return;
                    }
                    if (!ProductRegexp.IsLengthValid(ProductLength))
                    {
                        OpenMessageWindow("Неправильная длина продукта");
                        return;
                    }
                    if (!ProductRegexp.IsManufacturerValid(ProductManufacturer))
                    {
                        OpenMessageWindow("Неправильный производитель продукта");
                        return;
                    }
                    if (!ProductRegexp.IsProductActiveValid(ProductIsActive))
                    {
                        OpenMessageWindow("Окно активности не должно быть пустым");
                        return;
                    }
                    if (ProductImageDirectory == null)
                        ProductImageDirectory = @"Товары салона красоты\productdefaultimage.png";
                    var editproduct = ProductDataWorker.EditProduct(SelectedProduct, ProductName, ProductCategory, ProductPrice,
                        ProductSpecifications, ProductDescription, ProductWeight, ProductHeight, ProductWidth,
                        ProductLength, ProductManufacturer.Id, ProductImageDirectory, ProductIsActive);
                    if (editproduct)
                    {
                        OpenMessageWindow("Продукт успешно изменен");
                        UpdateProductPage();
                        SetNullProductValueProperties();
                        CloseWindow(wnd);
                    }

                });
            }
        }
        private AsyncRelayCommand _deleteproductcommand;
        public AsyncRelayCommand DeleteProductCommand
        {
            get
            {
                return _deleteproductcommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    if(SelectedProduct == null)
                    {
                        OpenMessageWindow("Не выбран продукт");
                        return;
                    }
                    ProductDataWorker.DeleteProduct(SelectedProduct);
                    OpenMessageWindow("Продукт успешно удален");
                    UpdateProductPage();
                    

                });
            }
        }

        private void SetNullProductValueProperties()
        {
            ProductName = null;
            ProductCategory = null;
            ProductPrice = null;
            ProductSpecifications = null;
            ProductDescription = null;
            ProductWeight = null;
            ProductHeight = null;
            ProductWidth = null;
            ProductLength = null;
            ProductManufacturer = null;
            ProductImageDirectory = null;
            ProductIsActive = null;
        }
        private void UpdateProductPage()
        {
            AllProducts = ProductDataWorker.GetProducts();
            ProductsPage.ListProductsBox.ItemsSource = null;
            ProductsPage.ListProductsBox.Items.Clear();
            ProductsPage.ListProductsBox.ItemsSource = AllProducts;
        }
        #endregion

        #region Команды добавления, редактирования и удаления услуг
        private AsyncRelayCommand _addservicecommand;
        public AsyncRelayCommand AddServiceCommand
        {
            get
            {
                return _addservicecommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    Window wnd = obj as Window;
                    if (!ServiceRegexp.IsNameValid(ServiceName))
                    {
                        OpenMessageWindow("Неправильное название услуги");
                        return;
                    }
                    if (!ServiceRegexp.IsDescriptionValid(ServiceDescription))
                    {
                        OpenMessageWindow("Неправильное описание услуги");
                        return;
                    }
                    if (!ServiceRegexp.IsPriceValid(ServicePrice))
                    {
                        OpenMessageWindow("Неправильная цена услуги");
                        return;
                    }
                    if (!ServiceRegexp.IsDiscountValid(ServiceDiscount))
                    {
                        OpenMessageWindow("Неправильная скидка");
                        return;
                    }
                    if (!ServiceRegexp.IsDurationValid(ServiceDuration))
                    {
                        OpenMessageWindow("Неправильная продолжительность услуги");
                        return;
                    }
                    if (ServiceImageDirectory == null)
                        ServiceImageDirectory = @"Images\ServiceImages\servicedefaultimage.png";
                    var addservice = ServiceDataWorker.AddService(ServiceName, ServiceDescription, 
                        ServicePrice, ServiceDiscount, ServiceDuration, ServiceImageDirectory);
                    if (addservice)
                    {
                        OpenMessageWindow("Услуга успешно добавлен");
                        UpdateServicePage();
                        SetNullServiceValueProperties();
                        CloseWindow(wnd);
                    }

                });
            }
        }
        private AsyncRelayCommand _editservicecommand;
        public AsyncRelayCommand EditServiceCommand
        {
            get
            {
                return _editservicecommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    Window wnd = obj as Window;
                    if (!ServiceRegexp.IsNameValid(ServiceName))
                    {
                        OpenMessageWindow("Неправильное название услуги");
                        return;
                    }
                    if (!ServiceRegexp.IsDescriptionValid(ServiceDescription))
                    {
                        OpenMessageWindow("Неправильное описание услуги");
                        return;
                    }
                    if (!ServiceRegexp.IsPriceValid(ServicePrice))
                    {
                        OpenMessageWindow("Неправильная цена услуги");
                        return;
                    }
                    if (!ServiceRegexp.IsDiscountValid(ServiceDiscount))
                    {
                        OpenMessageWindow("Неправильная скидка");
                        return;
                    }
                    if (!ServiceRegexp.IsDurationValid(ServiceDuration))
                    {
                        OpenMessageWindow("Неправильная продолжительность услуги");
                        return;
                    }
                    if (ServiceImageDirectory == null)
                        ServiceImageDirectory = @"Images\ServiceImages\servicedefaultimage.png";
                    var editservice = ServiceDataWorker.EditService(SelectedService, ServiceName, ServiceDescription,
                        ServicePrice, ServiceDiscount, ServiceDuration, ServiceImageDirectory);
                    if (editservice)
                    {
                        OpenMessageWindow("Услуга успешно изменена");
                        UpdateServicePage();
                        SetNullServiceValueProperties();
                        CloseWindow(wnd);
                    }

                });
            }
        }
        private AsyncRelayCommand _deleteservicecommand;
        public AsyncRelayCommand DeleteServiceCommand
        {
            get
            {
                return _deleteservicecommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    if (SelectedService == null)
                    {
                        OpenMessageWindow("Не выбрана услуга");
                        return;
                    }
                    ServiceDataWorker.DeleteService(SelectedService);
                    OpenMessageWindow("Услуга успешно удалена");
                    UpdateServicePage();


                });
            }
        }
        private void SetNullServiceValueProperties()
        {
            ServiceName = null;
            ServiceDescription = null;
            ServicePrice = null;
            ServiceDiscount = null;
            ServiceDuration = null;
            ServiceImageDirectory = null;
            
        }
        private void UpdateServicePage()
        {
            AllServices = ServiceDataWorker.GetServices();
            ServicesPage.ListServicesBox.ItemsSource = null;
            ServicesPage.ListServicesBox.Items.Clear();
            ServicesPage.ListServicesBox.ItemsSource = AllServices;
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

                return _openservicespagecommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    Frame frame = obj as Frame;
                    await Task.Run(() => OpenServicesPage(frame));
                });
            }
        }
        private AsyncRelayCommand _openclientspagecommand;
        public AsyncRelayCommand OpenClientsPageCommand
        {
            get
            {

                return _openclientspagecommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    Frame frame = obj as Frame;
                    await Task.Run(() => OpenClientsPage(frame));
                });
            }
        }
        private AsyncRelayCommand _openemployeespagecommand;
        public AsyncRelayCommand OpenEmployeesPageCommand
        {
            get
            {

                return _openemployeespagecommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    Frame frame = obj as Frame;
                    await Task.Run(() => OpenEmployeesPage(frame));
                });
            }
        }
        private AsyncRelayCommand _openaddproductwindowcommand;
        public AsyncRelayCommand OpenAddProductWindowCommand
        {
            get
            {

                return _openaddproductwindowcommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    await Task.Run(() => OpenAddProductWindow());
                });
            }
        }
        private AsyncRelayCommand _openeditproductwindowcommand;
        public AsyncRelayCommand OpenEditProductWindowCommand
        {
            get
            {

                return _openeditproductwindowcommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    if (SelectedProduct == null)
                    {
                        OpenMessageWindow("Не выбран продукт");
                        return;
                    }
                    await Task.Run(() => OpenEditProductWindow());
                });
            }
        }
        private AsyncRelayCommand _openaddservicewindowcommand;
        public AsyncRelayCommand OpenAddServiceWindowCommand
        {
            get
            {

                return _openaddservicewindowcommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    await Task.Run(() => OpenAddServiceWindow());
                });
            }
        }
        private AsyncRelayCommand _openeditservicewindowcommand;
        public AsyncRelayCommand OpenEditServiceWindowCommand
        {
            get
            {

                return _openeditservicewindowcommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    if (SelectedService == null)
                    {
                        OpenMessageWindow("Не выбрана услуга");
                        return;
                    }
                    await Task.Run(() => OpenEditServiceWindow());
                });
            }
        }
        #endregion

        #region Команды добавления изображения
        private RelayCommand _addproductimagecommand;
        public RelayCommand AddProductImageCommand
        {
            get
            {
                return _addproductimagecommand ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    ProductImageDirectory = ProductDataWorker.AddImage(window);
                    if (string.IsNullOrEmpty(ProductImageDirectory))
                        OpenMessageWindow("Неправильный путь к изображению");

                }
                );

            }
        }
        private RelayCommand _addserviceimagecommand;
        public RelayCommand AddServiceImageCommand
        {
            get
            {
                return _addserviceimagecommand ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    ServiceImageDirectory = ServiceDataWorker.AddImage(window);
                    if (string.IsNullOrEmpty(ServiceImageDirectory))
                        OpenMessageWindow("Неправильный путь к изображению");

                }
                );

            }
        }
        #endregion

        #region Методы открытия окон и страниц
        private void OpenMessageWindow(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageWindow messagewindow = new MessageWindow(message);
                SetCenterPositionAndOwner(messagewindow);
            });
        }
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
        private void OpenEmployeesPage(Frame frame)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                frame.Navigate(new EmployeesPage(this));
            });
        }
        private void OpenClientsPage(Frame frame)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                frame.Navigate(new ClientsPage(this));
            });
        }
        private void OpenAddProductWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AddProductWindow wnd = new AddProductWindow(this);
                wnd.Show();
            });
        }
        private void OpenEditProductWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                EditProductWindow wnd = new EditProductWindow(this);
                wnd.Show();
            });
        }
        private void OpenAddServiceWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AddServiceWindow wnd = new AddServiceWindow(this);
                wnd.Show();
            });
        }
        private void OpenEditServiceWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                EditServiceWindow wnd = new EditServiceWindow(this);
                wnd.Show();
            });
        }
        private void SetCenterPositionAndOwner(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
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