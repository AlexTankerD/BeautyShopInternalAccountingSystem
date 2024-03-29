﻿using System;
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

namespace BeautyShopInternalAccountingSystem.Views
{
    /// <summary>
    /// Логика взаимодействия для MessageView.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow(string message)
        {
            InitializeComponent();
            MessageText.Text = message;
        }

        private void okbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Exitbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
