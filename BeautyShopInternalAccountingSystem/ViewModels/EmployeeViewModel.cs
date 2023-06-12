using BeautyShopInternalAccountingSystem.Models;
using BeautyShopInternalAccountingSystem.Models.DataWorkers;
using BeautyShopInternalAccountingSystem.Models.RelayCommands;
using BeautyShopInternalAccountingSystem.Views.ClientWindows;
using BeautyShopInternalAccountingSystem.Views.EmployeeWindows;
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
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        public Employee Employee { get; set; } 
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
                if (SearchText != null)
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
        private RelayCommand _openemployeedatapagecommand;
        public RelayCommand OpenEmployeeDataPageCommand
        {
            get
            {

                return _openemployeedatapagecommand ?? new RelayCommand(obj =>
                {
                    Frame frame = obj as Frame;
                    OpenEmployeeDataPage(frame);
                });
            }
        }
        private void OpenProductsPage(Frame frame)
        {
            frame.Navigate(new ProductsPage(this));
        }
        private void OpenEmployeeDataPage(Frame frame)
        {
            frame.Navigate(new EmployeeDataPage(this));
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
