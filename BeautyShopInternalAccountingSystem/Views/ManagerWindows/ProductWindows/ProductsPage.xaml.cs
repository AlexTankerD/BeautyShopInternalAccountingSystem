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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeautyShopInternalAccountingSystem.Views.ManagerWindows.ProductWindows
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : ModernWpf.Controls.Page
    {
        public static ListBox ListProductsBox;
        public ProductsPage(ManagerViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            ListProductsBox = ProductsListBox;
        }
    }
}
