using BeautyShopInternalAccountingSystem.Models;
using BeautyShopInternalAccountingSystem.Models.DataWorkers;
using BeautyShopInternalAccountingSystem.Models.RelayCommands;
using BeautyShopInternalAccountingSystem.Views.ClientWindows;
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
    public class ClientViewModel : INotifyPropertyChanged
    {
        public Client Client { get; set; }
        private ObservableCollection<Product> _allproducts = ProductsDataWorker.GetProducts();
        public ObservableCollection<Product> AllProducts
        {
            get { return _allproducts; }
            set
            {
                _allproducts = value;
                OnPropertyChanged("AllProducts");
            }
        }
        private RelayCommand _openproductspagecommand;
        public RelayCommand OpenProductsPageCommand
        {
            get
            {

                return _openproductspagecommand ?? new RelayCommand(obj =>
                {
                    Frame frame = obj as Frame;
                    OpenProductsPage(frame);
                });
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
        private void OpenProductsPage(Frame frame)
        {
            frame.Navigate(new ProductsPage(this));
        }
        private void OpenServicesPage(Frame frame)
        {
            frame.Navigate(new ServicesPage(this));
        }
        private void OpenClientDataPage(Frame frame)
        {
            frame.Navigate(new ClientDataPage(this));
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
