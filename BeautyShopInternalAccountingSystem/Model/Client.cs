using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Model
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Birthday { get; set; }
        public char Sex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? ClientImageDirectory { get; set; }
        public Client()
        { }
        public Client(string Username, string Password, string Name, string Surname, string Patronymic, string Birthday,
            char Sex, string Email, string PhoneNumber, string? ClientImageDirectory)
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
            this.ClientImageDirectory = ClientImageDirectory;
        }

    }
}
