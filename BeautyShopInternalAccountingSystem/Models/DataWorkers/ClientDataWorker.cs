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
using System.Collections.ObjectModel;

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
            string pathtocopy = $@"Images\ClientImages\{Path.GetFileName(openFileDialog.FileName)}";
            if (!Directory.Exists(@"Images\ClientImages"))
                Directory.CreateDirectory(@"Images\ClientImages");
            if(!File.Exists(pathtocopy))
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
            Image image = wnd.FindName("ClientImage") as Image;
            image.Source = new BitmapImage(uri);
        }
        public static bool ChangeClientData(Client client, string Username, string Password, string Name, string Surname, string Patronymic, string Birthday,
            string Sex, string Email, string PhoneNumber, string ClientImageDirectory)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Client clientdb = db.Clients.Where(x => (x.Username == Username && Username != client.Username) || 
                (x.PhoneNumber == PhoneNumber && PhoneNumber != client.PhoneNumber) ||
                (x.Email == Email && Email != client.Email)).FirstOrDefault();
                if (clientdb != null)
                    return false;
                else
                {
                    Client newclientdb = db.Clients.Where(x => x == client).FirstOrDefault();
                    newclientdb.Username = Username;
                    newclientdb.Password = Password;
                    newclientdb.Name = Name;
                    newclientdb.Surname = Surname;
                    newclientdb.Patronymic = Patronymic;
                    newclientdb.Birthday = Birthday;
                    newclientdb.Sex = Sex;
                    newclientdb.PhoneNumber = PhoneNumber;
                    newclientdb.Email = Email;
                    newclientdb.ClientImageDirectory = ClientImageDirectory;
                    db.SaveChanges();
                    return true;
                }
            }

        }
        public static bool AddClientTag(Client SelectedClient, string Tag, string TagColor)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                Client client = db.Clients.Where(x => x == SelectedClient).FirstOrDefault();
                client.Tag = Tag;
                client.TagColor = TagColor;
                db.SaveChanges();
                return true;
            }
        }
        public static bool DeleteClientTag(Client SelectedClient)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Client client = db.Clients.Where(x => x == SelectedClient).FirstOrDefault();
                client.Tag = null;
                client.TagColor = null;
                db.SaveChanges();
                return true;
            }
        }
        public static ObservableCollection<Client> GetClients()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return new ObservableCollection<Client>(db.Clients.ToList());
            }
        }
        
    }
}
