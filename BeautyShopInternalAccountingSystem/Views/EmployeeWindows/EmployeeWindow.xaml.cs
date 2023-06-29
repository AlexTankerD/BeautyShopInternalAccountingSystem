using BeautyShopInternalAccountingSystem.ViewModels;
using BeautyShopInternalAccountingSystem.Views.AuthorizationWindows;
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
using System.Windows.Media.Animation;
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
            MainFrame.Navigate(new ServiceOrdersPage(vm));
        }
        private void Menubtn_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 300;
            animation.Duration = TimeSpan.FromSeconds(0.3);
            animation.EasingFunction = new QuadraticEase();
            Menu.Margin = new Thickness(0, 0, 0, 0);
            Menu.BeginAnimation(WidthProperty, animation);
            ApplyEffect();
        }

        private void Menu_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 300;
            animation.To = 0;
            animation.Duration = TimeSpan.FromSeconds(0.3);
            animation.EasingFunction = new QuadraticEase();
            Menu.Margin = new Thickness(-100, 0, 0, 0);
            Menu.BeginAnimation(WidthProperty, animation);
            ClearEffect();
        }
        private void ApplyEffect()
        {
            MainFrame.Opacity = 0.5;
        }
        private void ClearEffect()
        {
            MainFrame.Opacity = 1;
        }
        private void Exitbtn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow wnd = new AuthorizationWindow();
            wnd.Show();
            this.Close();
        }

        private void Closebtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
    

