using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Models.RegularExpressions
{
    public static class ServiceRegexp
    {
        public static bool IsNameValid(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length > 50)
                return false;
            return true;
        }
        public static bool IsDescriptionValid(string description)
        {
            if (string.IsNullOrEmpty(description) || description.Length > 50)
                return false;
            return true;
        }
        public static bool IsPriceValid(double? Price)
        {
            if (string.IsNullOrEmpty(Price.ToString()) || !double.TryParse(Price.ToString(), out _) || Price > 10000000000)
                return false;
            return true;
        }
        public static bool IsDiscountValid(double? discount)
        {
            if (string.IsNullOrEmpty(discount.ToString()) || !double.TryParse(discount.ToString(), out _) || discount > 100 || discount < 0)
                return false;
            return true;
        }
        public static bool IsDurationValid(int? duration)
        {
            if(string.IsNullOrEmpty(duration.ToString()) || !int.TryParse(duration.ToString(), out _) || duration > 10000000000)
                return false;
            return true;
        }
    }
}
