using BeautyShopInternalAccountingSystem.Models;
using BeautyShopInternalAccountingSystem.Models.DataWorkers;
using BeautyShopInternalAccountingSystem.Models.RelayCommands;
using BeautyShopInternalAccountingSystem.Views.ClientWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
        public Product SelectedProduct { get; set; }
        public ObservableCollection<Product> AllProducts = ProductsDataWorker.GetProducts();
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
                OnPropertyChanged("FilteredProducts");
            }
        }
        public IEnumerable<Product> FilteredProducts
        {
            get
            { 
                if(SearchText != null)
                {
                    var SearchName = AllProducts.Where(x => x.Name.ToUpper().StartsWith(SearchText.ToUpper()));
                    return SearchName;
                }
                else { return AllProducts; }
                
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
