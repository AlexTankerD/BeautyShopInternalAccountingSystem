﻿using BeautyShopInternalAccountingSystem.Models.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;

namespace BeautyShopInternalAccountingSystem.Models.DataWorkers
{
    public class EmployeeDataWorker
    {
        public static string AddImage(Window wnd)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Png files (*.png)|*.png| Jpg files (*.jpg)|*.jpg";
            openFileDialog.ShowDialog();
            if (string.IsNullOrEmpty(openFileDialog.FileName))
                return null;
            string pathtocopy = $@"Images\EmployeeImages\{Path.GetFileName(openFileDialog.FileName)}";
            if (!Directory.Exists(@"Images\EmployeeImages"))
                Directory.CreateDirectory(@"Images\EmployeeImages");
            if (!File.Exists(pathtocopy))
            {
                FileInfo imagepath = new FileInfo(openFileDialog.FileName);
                imagepath.CopyTo(pathtocopy);
            }
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(openFileDialog.FileName);
            image.EndInit();
            SetImageInWindow(wnd, image.UriSource);
            return pathtocopy;
        }
        private static void SetImageInWindow(Window wnd, Uri uri)
        {
            Image image = wnd.FindName("EmployeeImage") as Image;
            image.Source = new BitmapImage(uri);
        }
        public static bool AddEmployee(string Username, string Password, string Name, string Surname, string Patronymic, string Birthday,
           string Sex, string Email, string PhoneNumber, string Position, double? SellaryRatio, string PassportData, List<Service> Services, string? EmployeeImageDirectory)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Employee employeedb = db.Employees.Where(x => x.Username == Username || x.PhoneNumber == PhoneNumber ||
                x.Email == Email || x.PassportData == PassportData).FirstOrDefault();
                if (employeedb != null)
                    return false;
                else
                {
                    Employee employee = new Employee(Username, Password, Name, Surname, Patronymic, Birthday, Sex,
                        Email, PhoneNumber, Position, Convert.ToDouble(SellaryRatio), PassportData, Services, EmployeeImageDirectory);
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return true;
                }
            }

        }
        public static ObservableCollection<Employee> GetEmployees()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return new ObservableCollection<Employee>(db.Employees.ToList());
            }
        }
    }
}
