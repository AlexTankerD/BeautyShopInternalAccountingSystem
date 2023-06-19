using BeautyShopInternalAccountingSystem.ViewModels;
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

namespace BeautyShopInternalAccountingSystem.Views.ManagerWindows.ManufacurerWindows
{
    /// <summary>
    /// Логика взаимодействия для ManufacturersWindow.xaml
    /// </summary>
    public partial class ManufacturersWindow : Window
    {
        public static ListBox ListManufacturersBox;
        public ManufacturersWindow(ManagerViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            ListManufacturersBox = ManufacturersListBox;
        }
    }
}
