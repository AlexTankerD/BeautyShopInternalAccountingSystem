using BeautyShopInternalAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem
{
    public class EmployeeServices
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
