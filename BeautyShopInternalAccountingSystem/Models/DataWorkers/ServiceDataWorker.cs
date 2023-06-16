using BeautyShopInternalAccountingSystem.Models.Data;
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
    public static class ServiceDataWorker
    {
        public static ObservableCollection<Service> GetServices()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return new ObservableCollection<Service>(db.Services.ToList());
            }
        }
        public static bool AddService(string Name, string Description, double? Price, double? Discount, 
            int? Duration, string ServiceImageDirectory)
        {
            using (ApplicationContext db = new ApplicationContext()) 
            {
                Service service = new Service(Name, Description, Convert.ToDouble(Price), Convert.ToDouble(Discount), Convert.ToInt32(Duration), ServiceImageDirectory);
                db.Services.Add(service);
                db.SaveChanges();
                return true;
            }
        }
        public static bool EditService(Service SelectedService, string Name, string Description, double? Price, double? Discount,
            int? Duration, string ServiceImageDirectory)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Service Servicedb = db.Services.Where(x => x.Name == SelectedService.Name && x.Description == SelectedService.Description &&
                x.Price == SelectedService.Price && x.Price == SelectedService.Price &&
                x.Discount == SelectedService.Discount && x.Duration == SelectedService.Duration).FirstOrDefault();
                Servicedb.Name = Name;
                Servicedb.Description = Description;
                Servicedb.Price = Convert.ToDouble(Price);
                Servicedb.Discount = Convert.ToDouble(Discount);
                Servicedb.Duration = Convert.ToInt32(Duration);
                Servicedb.ServiceImageDirectory = ServiceImageDirectory;
                db.SaveChanges();

            }
            return true;
        }
        public static bool DeleteService(Service SelectedService) 
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Services.Remove(SelectedService);
                db.SaveChanges();
                return true;
            }
        }
        public static string AddImage(Window wnd)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Png files (*.png)|*.png| Jpg files (*.jpg)|*.jpg";
            openFileDialog.ShowDialog();
            if (string.IsNullOrEmpty(openFileDialog.FileName))
                return null;
            string pathtocopy = $@"Images\ServiceImages\{Path.GetFileName(openFileDialog.FileName)}";
            if (!Directory.Exists(@"Images\ServiceImages"))
                Directory.CreateDirectory(@"Images\ServiceImages");
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
            Image image = wnd.FindName("ServiceImage") as Image;
            image.Source = new BitmapImage(uri);
        }
    }
}
