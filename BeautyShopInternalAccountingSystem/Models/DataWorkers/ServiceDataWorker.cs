using BeautyShopInternalAccountingSystem.Models.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Models.DataWorkers
{
    public class ServiceDataWorker
    {
        public static ObservableCollection<Service> GetServices()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return new ObservableCollection<Service>(db.Services.ToList());
            }
        }
    }
}
