using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyShopInternalAccountingSystem.Models.Data;

namespace BeautyShopInternalAccountingSystem.Models.DataWorkers
{
    public static class AuthorizationDataWorker
    {
        public static bool AuthorizationAsClient(string Login, string Password, ref Client client)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Client clientdb = db.Clients.Where(x => (x.Username == Login || x.Email == Login ||
                x.PhoneNumber == Login) && x.Password == Password).FirstOrDefault();
                if (clientdb != null)
                {
                    client = clientdb;
                    return true;
                }
                else
                    return false;
            }
        }
        public static bool AuthorizationAsEmployee(string Login, string Password, ref Employee employee)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Employee employeedb = db.Employees.Where(x => (x.Username == Login || x.Email == Login ||
                    x.PhoneNumber == Login) && x.Password == Password).FirstOrDefault();
                if (employeedb != null)
                {
                    employee = employeedb;
                    return true;
                }
                else
                    return false;
            }
        }
        public static bool AuthorizationAsManager(string Login, string Password)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Manager managerdb = db.Managers.Where(x => (x.Username == Login) && x.Password == Password).FirstOrDefault();
                if (managerdb != null)
                {
                    return true;
                }
                else
                    return false;
            }
        }
        public static bool Registration(string Username, string Password, string Name, string Surname, string Patronymic, string Birthday,
            string Sex, string Email, string PhoneNumber, string? ClientImageDirectory)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Client clientdb = db.Clients.Where(x => x.Username == Username || x.PhoneNumber == PhoneNumber ||
                x.Email == Email).FirstOrDefault();
                if (clientdb != null)
                    return false;
                else
                {
                    Client newclient = new Client(Username, Password, Name, Surname, Patronymic, Birthday, Sex,
                        Email, PhoneNumber, ClientImageDirectory);
                    db.Clients.Add(newclient);
                    db.SaveChanges();
                    return true;
                }
            }

        }
        public static bool ChangePassword(string Username, string NewPassword)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Client clientdb = db.Clients.Where(x => x.Username == Username).FirstOrDefault();
                if (clientdb == null)
                    return false;
                clientdb.Password = NewPassword;
                db.SaveChanges();
                return true;
            }
        }
    }
}
