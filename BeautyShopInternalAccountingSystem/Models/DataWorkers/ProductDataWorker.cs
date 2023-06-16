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
    public class ProductDataWorker
    {
        public static ObservableCollection<Product> GetProducts()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var products = new ObservableCollection<Product>(db.Products.ToList());
                foreach (Product product in products)
                {
                    Manufacturer manufacturer = db.Manufacturers.Where(x => x.Id == product.ManufacturerId).FirstOrDefault();
                    product.ManufacturerName = manufacturer.Name;
                }
                return products;
            }
        }
        public static bool AddProduct(string Name, string Category, double? Price, string Specifications, 
            string Description, double? Weight, double? Height, double? Width, double? Length, int ManufacturerId,
            string ProductImageDirectory, bool? IsActive)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                Product product = new Product(Name, Category, Convert.ToDouble(Price), Specifications, Description, Convert.ToDouble(Weight), Convert.ToDouble(Height),
                Convert.ToDouble(Width), Convert.ToDouble(Length), ManufacturerId, ProductImageDirectory, Convert.ToBoolean(IsActive));
                db.Products.Add(product);
                db.SaveChanges();
                return true;
            }
        }
        public static bool DeleteProduct(Product SelectedProduct)
        {
           using(ApplicationContext db = new ApplicationContext())
           {
                db.Products.Remove(SelectedProduct);
                db.SaveChanges();
                return true;
           }
        }
        public static bool EditProduct(Product SelectedProduct, string Name, string Category, double? Price, string Specifications,
            string Description, double? Weight, double? Height, double? Width, double? Length, int ManufacturerId,
            string ProductImageDirectory, bool? IsActive)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                Product productdb = db.Products.Where(x => x.Name == SelectedProduct.Name && x.Category == SelectedProduct.Category && 
                x.Price == SelectedProduct.Price && x.Specifications == SelectedProduct.Specifications && 
                x.Description == SelectedProduct.Description && x.Weight == SelectedProduct.Weight && x.Height == SelectedProduct.Height &&
                x.Width == SelectedProduct.Width && x.Length == SelectedProduct.Length && x.Manufacturer == SelectedProduct.Manufacturer &&
                x.IsActive == SelectedProduct.IsActive).FirstOrDefault();
                productdb.Name = Name;
                productdb.Category = Category;
                productdb.Price = Convert.ToDouble(Price);
                productdb.Specifications = Specifications;
                productdb.Description = Description;
                productdb.Weight = Convert.ToDouble(Weight);
                productdb.Height = Convert.ToDouble(Height);
                productdb.Width = Convert.ToDouble(Width);
                productdb.Length = Convert.ToDouble(Length);
                productdb.ManufacturerId = ManufacturerId;
                productdb.ProductImageDirectory = ProductImageDirectory;
                productdb.IsActive = Convert.ToBoolean(IsActive);
                db.SaveChanges();

            }
            return true;
        }
        
        public static string AddImage(Window wnd)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Png files (*.png)|*.png| Jpg files (*.jpg)|*.jpg";
            openFileDialog.ShowDialog();
            if (string.IsNullOrEmpty(openFileDialog.FileName))
                return null;
            string pathtocopy = $@"Товары салона красоты\{Path.GetFileName(openFileDialog.FileName)}";
            if (!Directory.Exists(@"Товары салона красоты"))
                Directory.CreateDirectory(@"Товары салона красоты");
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
            Image image = wnd.FindName("ProductImage") as Image;
            image.Source = new BitmapImage(uri);
        }
    }
}
