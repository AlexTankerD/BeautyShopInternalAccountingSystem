﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
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
        public string Manufacturer { get; set; }
        public string ProductImageDirectory { get; set; } 
        public bool IsActive { get; set; }
        


    }
}
