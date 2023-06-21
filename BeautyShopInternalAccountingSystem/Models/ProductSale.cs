using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Models
{
    public class ProductSale
    {
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public string DateOfSale { get; set; }
    }
}
