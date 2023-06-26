using BeautyShopInternalAccountingSystem.Models;
using BeautyShopInternalAccountingSystem.Models.DataWorkers;
using BeautyShopInternalAccountingSystem.Models.RelayCommands;
using BeautyShopInternalAccountingSystem.Views;
using BeautyShopInternalAccountingSystem.Views.AuthorizationWindows;
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
using System.Xaml;

namespace BeautyShopInternalAccountingSystem.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        public Employee Employee { get; set; }

        public EmployeeViewModel(Employee Employee)
        {
            this.Employee = EmployeeDataWorker.GetEmployeeServices(Employee);
            AllServiceOrders = ServiceOrderDataWorker.GetServiceOrdersForEmployee(this.Employee);
        }
        public ServiceOrder SelectedServiceOrder { get; set; }

        private ObservableCollection<ServiceOrder> AllServiceOrders;

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
                    var SearchName = AllServiceOrders.Where(x => x.Client.Name.ToUpper().StartsWith(SearchServiceOrderText.ToUpper()) ||
                    x.Client.Surname.ToUpper().StartsWith(SearchServiceOrderText.ToUpper()) ||
                    x.Client.Patronymic.ToUpper().StartsWith(SearchServiceOrderText.ToUpper()) ||
                    x.Service.Name.ToUpper().StartsWith(SearchServiceOrderText.ToUpper()));
                    return SearchName;
                }
                else { return AllServiceOrders; }

            }
        }
        private AsyncRelayCommand _confirmordercommand;
        public AsyncRelayCommand ConfirmOrderCommand
        {
            get
            {

                return _confirmordercommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    if(SelectedServiceOrder == null)
                    {
                        OpenMessageWindow("Не выбран заказ");
                        return;
                    }
                    await Task.Run(() => ConfirmOrderCommandMethod());
                });
            }
        }
        private void ConfirmOrderCommandMethod()
        {
            var confirmorder = ServiceOrderDataWorker.ConfirmOrder(SelectedServiceOrder, Employee);
            if(confirmorder)
            {
                OpenMessageWindow("Заказ на услугу успешно принят");
                UpdateServiceOrdersPage();
            }
        }
        private AsyncRelayCommand _rejectordercommand;
        public AsyncRelayCommand RejectOrderCommand
        {
            get
            {

                return _rejectordercommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    if (SelectedServiceOrder == null)
                    {
                        OpenMessageWindow("Не выбран заказ");
                        return;
                    }
                    await Task.Run(() => RejectOrderCommandMethod());
                });
            }
        }
        private void RejectOrderCommandMethod()
        {
            var rejectorder = ServiceOrderDataWorker.RejectOrder(SelectedServiceOrder, Employee);
            if (rejectorder)
            {
                OpenMessageWindow("Заказ на услугу успешно отклонен");
                UpdateServiceOrdersPage();
            }
        }
        private void UpdateServiceOrdersPage()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AllServiceOrders = ServiceOrderDataWorker.GetServiceOrdersForEmployee(Employee);
                ServiceOrdersPage.ListServiceOrdersBox.ItemsSource = null;
                ServiceOrdersPage.ListServiceOrdersBox.Items.Clear();
                ServiceOrdersPage.ListServiceOrdersBox.ItemsSource = FilteredServiceOrders;
            });
        }
        private AsyncRelayCommand _openemployeedatapagecommand;
        public AsyncRelayCommand OpenEmployeeDataPageCommand
        {
            get
            {

                return _openemployeedatapagecommand ?? new AsyncRelayCommand(async(obj) =>
                {
                    Frame frame = obj as Frame;
                    await Task.Run(() => OpenEmployeeDataPage(frame));
                });
            }
        } 
        private AsyncRelayCommand _openserviceorderspagecommand;
        public AsyncRelayCommand OpenServiceOrdersPageCommand
        {
            get
            {

                return _openserviceorderspagecommand ?? new AsyncRelayCommand(async (obj) =>
                {
                    Frame frame = obj as Frame;
                    await Task.Run(() => OpenServiceOrdersPage(frame));
                });
            }
        }
        private void OpenServiceOrdersPage(Frame frame)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                frame.Navigate(new ServiceOrdersPage(this));
            });
        }
        private void OpenEmployeeDataPage(Frame frame)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                frame.Navigate(new EmployeeDataPage(this));
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
