using BeautyShopInternalAccountingSystem.ViewModels;
using BeautyShopInternalAccountingSystem.Views.ClientWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BeautyShopInternalAccountingSystem.Views.EmployeeWindows
{
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow(EmployeeViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            MainFrame.Navigate(new ProductsPage(vm));
        }
    }
    
}
