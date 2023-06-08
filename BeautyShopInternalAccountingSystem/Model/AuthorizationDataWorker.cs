using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Model
{
    public static class AuthorizationDataWorker
    {
        public static string Authorization(string Username, string Password, bool IsClientbtnChecked, bool IsEmployeebtnChecked, bool IsManagerbtnChecked)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                if(IsClientbtnChecked)
                {
                    Client clientdb = db.Clients.Where(x => x.Username == Username || x.Email == Username || 
                    x.PhoneNumber == Username && x.Password == Password).FirstOrDefault();
                    if (clientdb != null)
                        return "ClientLoginSuccesful";
                    else
                        return "LoginFail";
                }
                if (IsEmployeebtnChecked)
                {
                    Employee employeedb = db.Employees.Where(x => x.Username == Username || x.Email == Username ||
                    x.PhoneNumber == Username && x.Password == Password).FirstOrDefault();
                    if (employeedb != null)
                        return "EmployeeLoginSuccesful";
                    else
                        return "LoginFail";
                }
                if (IsManagerbtnChecked)
                {
                    Manager managerdb = db.Manager.Where(x => x.Username == Username && x.Password == Password).FirstOrDefault();
                    if (managerdb != null)
                        return "ManagerLoginSuccesful";
                    else
                        return "LoginFail";
                }
            }
            return "TheButtonIsNotSelected";
        }
        public static string Registration(string Username, string Password, string Name, string Surname, string Patronymic, string Birthday,
            char Sex, string Email, string PhoneNumber, string? ClientImageDirectory)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                Client clientdb = db.Clients.Where(x => x.Username == Username || x.PhoneNumber == PhoneNumber || 
                x.Email == Email).FirstOrDefault();
                if (clientdb != null)
                    return "RegistrationFail";
                else
                {
                    Client newclient = new Client(Username, Password, Name, Surname, Patronymic, Birthday, Sex, 
                        Email, PhoneNumber, ClientImageDirectory);
                    db.Clients.Add(newclient);
                    db.SaveChanges();
                     return "RegistrationSuccesful";
                }
            }
            
        }
    }
}
