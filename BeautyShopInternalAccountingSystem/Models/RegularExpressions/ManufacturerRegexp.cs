using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Models.RegularExpressions
{
    public static class ManufacturerRegexp
    {
        public static bool IsNameValid(string Name)
        {
            if(string.IsNullOrEmpty(Name))
                return false;
            return true;
        }
        public static bool IsStartDateValid(string StartDate) 
        {
            if(string.IsNullOrEmpty (StartDate) || !DateOnly.TryParse(StartDate, out _))
                return false;
            return true;
        }
    }
}
