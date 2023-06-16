using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Category { get; set; }
        public double Price { get; set; }
        public string? Specifications { get; set; }
        public string?  Description { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public double? Length { get; set; }
        public int ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }
        [NotMapped]
        public string ManufacturerName { get; set; }
        public string ProductImageDirectory { get; set; } 
        public bool IsActive { get; set; }
        public Product()
        {

        }
        public Product(string Name, string Category, double Price, string Specifications, 
            string Description, double Weight, double Height, double Width, double Length, 
            int ManufacturerId, string ProductImageDirectory, bool IsActive)
        {
            this.Name = Name;
            this.Category = Category;
            this.Price = Price;
            this.Specifications = Specifications;
            this.Description = Description;
            this.Weight = Weight;
            this.Height = Height;
            this.Width = Width;
            this.Length = Length;
            this.ManufacturerId = ManufacturerId;
            this.ProductImageDirectory = ProductImageDirectory;
            this.IsActive = IsActive;

        }


    }
}
