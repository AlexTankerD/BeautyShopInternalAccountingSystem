﻿using BeautyShopInternalAccountingSystem.Models;
using BeautyShopInternalAccountingSystem.Models.DataWorkers;
using BeautyShopInternalAccountingSystem.Models.RelayCommands;
using BeautyShopInternalAccountingSystem.Views.AuthorizationWindows;
using BeautyShopInternalAccountingSystem.Views.ClientWindows;
using BeautyShopInternalAccountingSystem.Views.EmployeeWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BeautyShopInternalAccountingSystem.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        public Employee Employee { get; set; } 
        public Product SelectedProduct { get; set; }
        
        
        private AsyncRelayCommand _openemployeedatapagecommand;
        public AsyncRelayCommand OpenEmployeeDataPageCommand
        {
            get
            {

                return _openemployeedatapagecommand ?? new AsyncRelayCommand(async(obj) =>
                {
                    Frame frame = obj as Frame;
                    await Task.Run(() => OpenEmployeeDataPage(frame));
                });
            }
        }
        private void OpenEmployeeDataPage(Frame frame)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                frame.Navigate(new EmployeeDataPage(this));
            });
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
