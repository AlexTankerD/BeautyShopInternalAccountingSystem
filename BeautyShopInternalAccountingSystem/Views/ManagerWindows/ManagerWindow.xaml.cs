using BeautyShopInternalAccountingSystem.ViewModels;
using BeautyShopInternalAccountingSystem.Views.ManagerWindows.ProductWindows;
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

namespace BeautyShopInternalAccountingSystem.Views.ManagerWindows
{
    /// <summary>
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow(ManagerViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            MainFrame.Navigate(new ProductsPage(vm));
        }
    }
}
