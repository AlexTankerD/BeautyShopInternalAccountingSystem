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
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Duration { get; set; }
        public string ServiceImageDirectory { get; set; }
        public List<Employee> Employees { get; set; }
        public Service()
        {

        }
        public Service(string name, string description, double price, double discount, int duration, string serviceImageDirectory)
        {
            Name = name;
            Description = description;
            Price = price;
            Discount = discount;
            Duration = duration;
            ServiceImageDirectory = serviceImageDirectory;
        }
    }
}
