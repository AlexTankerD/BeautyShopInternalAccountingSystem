using BeautyShopInternalAccountingSystem.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Models.DataWorkers
{
    public static class ProductSaleDataWorker
    {
        public static ObservableCollection<ProductSale> GetProductSales()
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                return new ObservableCollection<ProductSale>(db.ProductsSale.Include(x => x.Product).Include(x => x.Client).ToList());
            }
        }
        public static bool AddProductSale(ObservableCollection<Product> Products, Client Client)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                db.Clients.Entry(Client).State = EntityState.Unchanged;
                var dublicates = Products.GroupBy(x => x)
                    .Select(x => new
                    {
                        Product = x.Key,
                        Count = x.Count(),

                    }).ToList();
                foreach (var product in dublicates)
                {
                    db.Products.Entry(product.Product).State = EntityState.Unchanged;
                    db.ProductsSale.Add(new ProductSale(Client, product.Product.Id, product.Count, DateTime.Now.ToString()));
                    db.SaveChanges();
                }
                return true;
            }
        }
    }
}
