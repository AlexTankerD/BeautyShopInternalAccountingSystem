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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeautyShopInternalAccountingSystem.Views.ManagerWindows.StatisticsWindows
{
    /// <summary>
    /// Логика взаимодействия для ProductsStatisticsPage.xaml
    /// </summary>
    public partial class ProductsStatisticsPage : ModernWpf.Controls.Page
    {
        public ProductsStatisticsPage(ManagerViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
