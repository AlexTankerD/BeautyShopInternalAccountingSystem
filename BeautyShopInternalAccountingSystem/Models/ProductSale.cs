using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Models
{
    public class ProductSale
    {
        [Key]
        public int Id { get; set; }
        public Client Client { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Count { get; set; }
        public string DateOfSale { get; set; }
        public ProductSale() { }
        public ProductSale(Client Client, int ProductId, int Count, string DateTime) 
        {
            this.ProductId = ProductId;
            this.Client = Client;
            this.Count = Count;
            this.DateOfSale = DateTime;
        }
    }
}
