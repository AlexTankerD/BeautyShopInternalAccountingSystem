using BeautyShopInternalAccountingSystem.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Models.DataWorkers
{
    public static class ServiceOrderDataWorker
    {
        public static ObservableCollection<ServiceOrder> GetServiceOrdersForClient(Client Client)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return new ObservableCollection<ServiceOrder>(db.ServiceOrders.Include(x => x.Service).Include(x => x.Client).Where(x=> x.Client == Client && x.Status == null).ToList());
            }
        }
        public static ObservableCollection<ServiceOrder> GetServiceOrders()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return new ObservableCollection<ServiceOrder>(db.ServiceOrders.Include(x => x.Service).Include(x => x.Client).Include(x => x.Employee).ToList());
            }
        }
        public static ObservableCollection<ServiceOrder> GetServiceOrdersForEmployee(Employee employee)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var list = new ObservableCollection<ServiceOrder>(db.ServiceOrders.Include(x => x.Service).Include(x => x.Client)
                    .Include(x => x.Service.Employees).Where(x => x.Employee != employee && x.Service.Employees.Any(x => x == employee) && x.Status != "Confirmed").ToList());
                return list;
            }

        }
        public static bool OrderService(Client Client, Service SelectedService, string StartDate, string Comment)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                var orderdb = db.ServiceOrders.Where(x => x.Service == SelectedService && x.StartDate == StartDate).FirstOrDefault();
                if (orderdb != null)
                    return false;
                else
                {
                    db.Clients.Entry(Client).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                    db.Services.Entry(SelectedService).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                    ServiceOrder order = new ServiceOrder(Client, SelectedService, StartDate, Comment);
                    db.ServiceOrders.Add(order);
                    db.SaveChanges();
                    return true;
                }
            }
        }
        public static bool ConfirmOrder(ServiceOrder ServiceOrder, Employee Employee)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Employees.Entry(Employee).State = EntityState.Unchanged;
                ServiceOrder serviceorder = db.ServiceOrders.Where(x => x == ServiceOrder).FirstOrDefault();
                serviceorder.Employee = Employee;
                serviceorder.Status = "Confirmed";
                db.SaveChanges();
                return true;
            }
        }
        public static bool RejectOrder(ServiceOrder ServiceOrder, Employee Employee)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Employees.Entry(Employee).State = EntityState.Unchanged;
                ServiceOrder serviceorder = db.ServiceOrders.Where(x => x == ServiceOrder).FirstOrDefault();
                serviceorder.Employee = Employee;
                serviceorder.Status = "Rejected";
                db.SaveChanges();
                return true;
            }
        }
        public static bool DeleteServiceOrder(ServiceOrder SelectedServiceOrder)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.ServiceOrders.Remove(SelectedServiceOrder);
                db.SaveChanges();
                return true;
            }
        }
    }
}
