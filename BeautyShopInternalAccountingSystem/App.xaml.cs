using BeautyShopInternalAccountingSystem.ViewModels;
using BeautyShopInternalAccountingSystem.Views.AuthorizationWindows;
using BeautyShopInternalAccountingSystem.Views.ClientWindows;
using BeautyShopInternalAccountingSystem.Views.EmployeeWindows;
using BeautyShopInternalAccountingSystem.Views.ManagerWindows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BeautyShopInternalAccountingSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {

            AuthorizationWindow window = new AuthorizationWindow();
            window.Show();
                
        }
    }
}
