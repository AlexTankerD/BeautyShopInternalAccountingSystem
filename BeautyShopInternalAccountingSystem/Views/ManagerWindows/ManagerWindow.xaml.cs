using BeautyShopInternalAccountingSystem.ViewModels;
using BeautyShopInternalAccountingSystem.Views.AuthorizationWindows;
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
using System.Windows.Media.Animation;
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

        private void Menubtn_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 300;
            animation.Duration = TimeSpan.FromSeconds(0.3);
            animation.EasingFunction = new QuadraticEase();
            Menu.Margin = new Thickness(0,0,0,0);
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

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2) 
            {
                if(IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1200;
                    this.Height = 720;
                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;

                }
            }
        }
    }
}
