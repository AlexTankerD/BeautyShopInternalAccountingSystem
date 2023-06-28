﻿using BeautyShopInternalAccountingSystem.ViewModels;
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

namespace BeautyShopInternalAccountingSystem.Views.ManagerWindows.ServiceWindows
{
    /// <summary>
    /// Логика взаимодействия для EditServiceWindow.xaml
    /// </summary>
    public partial class EditServiceWindow : Window
    {
        public EditServiceWindow(ManagerViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
        private void Exitbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
