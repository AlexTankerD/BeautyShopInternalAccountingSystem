﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Models
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }    
        public string StartDate { get; set; }
        public Manufacturer()
        { }
        public Manufacturer(string Name, string StartDate)
        {
            this.Name = Name;
            this.StartDate = StartDate;
        }
    }
}
