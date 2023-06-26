using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Models
{
    public class ServiceOrder
    {
        [Key]
        public int Id { get; set; }
        public Client Client { get; set; }
        public Service Service { get; set; }
        public Employee? Employee { get; set; }
        public string StartDate { get; set; }
        public string? Comment { get; set; }
        public string? ServiceOrderImage { get; set; }
        public string? Status { get; set; }
        public ServiceOrder() { }
        public ServiceOrder(Client Client, Service Service, string StartDate, string Comment)
        {
            this.Service = Service;
            this.Client = Client;
            this.StartDate = StartDate;
            this.Comment = Comment;
        }
    }
}
