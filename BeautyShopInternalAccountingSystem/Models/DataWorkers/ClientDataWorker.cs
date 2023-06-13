using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;

namespace BeautyShopInternalAccountingSystem.Models.DataWorkers
{
    public static class ClientDataWorker
    {
        public static string AddImage(Window wnd)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Png files (*.png)|*.png| Jpg files (*.jpg)|*.jpg";
            openFileDialog.ShowDialog();
            if (string.IsNullOrEmpty(openFileDialog.FileName)) 
                return null;
            string pathtocopy = $@"Resources\ClientImages\{Path.GetFileName(openFileDialog.FileName)}";
            if (!Directory.Exists(@"Resources\ClientImages"))
                Directory.CreateDirectory(@"Resources\ClientImages");
            try
            {
                FileInfo imagepath = new FileInfo(openFileDialog.FileName);
                imagepath.CopyTo(pathtocopy, true);
            }
            catch (Exception ex)
            {
                return null;
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
            Image image = wnd.FindName("ClientImage") as Image;
            image.Source = new BitmapImage(uri);
        }
    }
}
