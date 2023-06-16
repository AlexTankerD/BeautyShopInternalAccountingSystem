using BeautyShopInternalAccountingSystem.Models.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Models.DataWorkers
{
    public static class ManufacturerDataWorker
    {
        public static ObservableCollection<Manufacturer> GetManufacturers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return new ObservableCollection<Manufacturer>(db.Manufacturers.ToList());
            }
        }
    }
}
