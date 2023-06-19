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
        public static bool AddManufacturer(string ManufacturerName, string ManufacturerStartDate)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                Manufacturer manufacturerdb = db.Manufacturers.Where(x => x.Name == ManufacturerName).FirstOrDefault();
                if (manufacturerdb != null)
                    return false;
                Manufacturer manufacturer = new Manufacturer(ManufacturerName, ManufacturerStartDate);
                db.Manufacturers.Add(manufacturer);
                db.SaveChanges();
                return true;
            }
        }
        public static bool EditManufacturer(Manufacturer SelectedManufacturer,string ManufacturerName, string ManufacturerStartDate)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Manufacturer manufacturerdb = db.Manufacturers.Where(x => x.Name == ManufacturerName).FirstOrDefault();
                if (manufacturerdb != null)
                    return false;
                Manufacturer manufacturer = db.Manufacturers.Where(x => x == SelectedManufacturer).FirstOrDefault();
                manufacturer.Name = ManufacturerName;
                manufacturer.StartDate = ManufacturerStartDate;
                db.SaveChanges();
                return true;
            }
        }
        public static bool DeleteManufacturer(Manufacturer SelectedManufacturer)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Manufacturers.Remove(SelectedManufacturer);
                db.SaveChanges();
                return true;
            }
        }
    }
}
