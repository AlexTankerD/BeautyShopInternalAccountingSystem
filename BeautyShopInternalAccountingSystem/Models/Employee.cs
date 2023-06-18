using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Birthday { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public double SellaryRatio { get; set; }
        public string? EmployeeImageDirectory { get; set; }
        public string PassportData { get; set; }
        public List<Service> Services { get; set; }
        public Employee() { }
        public Employee(string Username, string Password, string Name, string Surname, string Patronymic, string Birthday,
            string Sex, string Email, string PhoneNumber, string Position, double SellaryRatio, string PassportData, List<Service> Services, string? EmployeeImageDirectory)
        {
            this.Username = Username;
            this.Password = Password;
            this.Name = Name;
            this.Surname = Surname;
            this.Patronymic = Patronymic;
            this.Birthday = Birthday;
            this.Sex = Sex;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.Position = Position;
            this.SellaryRatio = SellaryRatio;
            this.PassportData = PassportData;
            this.Services = Services;
            this.EmployeeImageDirectory = EmployeeImageDirectory;
        }
    }
}
