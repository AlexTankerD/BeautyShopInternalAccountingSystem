﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Model
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateOnly Birthday { get; set; }
        public char Sex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public double SellaryRatio { get; set; }
        public string PassportNumber { get; set; }
        public string EmployeeImageDirectory { get; set; }
    }
}
