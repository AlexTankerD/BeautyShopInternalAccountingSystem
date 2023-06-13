using BeautyShopInternalAccountingSystem.Models;
using BeautyShopInternalAccountingSystem.Models.DataWorkers;
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
        public char Sex { get; set; }
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
  
        
        private RelayCommand _openservicespagecommand;
        public RelayCommand OpenServicesPageCommand
        {
            get
            {

                return _openservicespagecommand ?? new RelayCommand(obj =>
                {
                    Frame frame = obj as Frame;
                    OpenServicesPage(frame);
                });
            }
        }
        private RelayCommand _openclientdatapagecommand;
        public RelayCommand OpenClientDataPageCommand
        {
            get
            {

                return _openclientdatapagecommand ?? new RelayCommand(obj =>
                {
                    Frame frame = obj as Frame;
                    OpenClientDataPage(frame);
                });
            }
        }
        private RelayCommand _openchangeclientdatawindowcommand;
        public RelayCommand OpenChangeClientDataWindowCommand
        {
            get
            {

                return _openchangeclientdatawindowcommand ?? new RelayCommand(obj =>
                {
                    OpenChangeClientDataWindow();
                });
            }
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
                    Client.ClientImageDirectory = null;
                    ClientImageDirectory = ClientDataWorker.AddImage(window);
                    if (string.IsNullOrEmpty(ClientImageDirectory))
                        OpenMessageWindow("Неправильный путь к изображению");

                }
                );

            }
        }
        #endregion


        private void OpenServicesPage(Frame frame)
        {
            frame.Navigate(new ServicesPage(this));
        }
        private void OpenClientDataPage(Frame frame)
        {
            frame.Navigate(new ClientDataPage(this));
        }
        private void OpenChangeClientDataWindow()
        {
            ChangeClientDataWindow changeclientdatawindow = new ChangeClientDataWindow(this);
            changeclientdatawindow.Show();
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
