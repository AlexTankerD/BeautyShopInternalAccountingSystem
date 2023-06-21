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
        public static ObservableCollection<ServiceOrder> GetServiceOrders()
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                return new ObservableCollection<ServiceOrder>(db.ServiceOrders.Include(x => x.Service).Include(x => x.Client).ToList());
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
