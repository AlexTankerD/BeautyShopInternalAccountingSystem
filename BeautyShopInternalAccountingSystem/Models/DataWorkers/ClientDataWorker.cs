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
using BeautyShopInternalAccountingSystem.Models.Data;

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
        public static bool ChangeClientData(Client client, string Username, string Password, string Name, string Surname, string Patronymic, string Birthday,
            string Sex, string Email, string PhoneNumber, string ClientImageDirectory)
        {
            string clientdefaultimagepath = @"Resources\ClientImages\user.png";
            using (ApplicationContext db = new ApplicationContext())
            {
                Client clientdb = db.Clients.Where(x => (x.Username == Username && Username != client.Username) || 
                (x.PhoneNumber == PhoneNumber && PhoneNumber != client.PhoneNumber) ||
                (x.Email == Email && Email != client.Email)).FirstOrDefault();
                if (clientdb != null)
                    return false;
                else
                {
                    Client newclientdb = db.Clients.Where(x => x.Username == client.Username && x.Name == client.Name && 
                    x.Surname == client.Surname && x.Patronymic == client.Patronymic && 
                    x.Birthday == client.Birthday && x.Sex == client.Sex && 
                    x.PhoneNumber == client.PhoneNumber && x.Email == client.Email).FirstOrDefault();
                    newclientdb.Username = Username ?? client.Username;
                    newclientdb.Password = Password ?? client.Password;
                    newclientdb.Name = Name ?? client.Name;
                    newclientdb.Surname = Surname ?? client.Surname;
                    newclientdb.Patronymic = Patronymic ?? client.Patronymic;
                    newclientdb.Birthday = Birthday ?? client.Birthday;
                    newclientdb.Sex = Sex ?? client.Sex;
                    newclientdb.PhoneNumber = PhoneNumber ?? client.PhoneNumber;
                    newclientdb.Email = Email ?? client.Email;
                    newclientdb.ClientImageDirectory = ClientImageDirectory;
                    db.SaveChanges();
                    return true;
                }
            }

        }
    }
}
