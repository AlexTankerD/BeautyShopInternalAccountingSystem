using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Models.RegularExpressions
{
    public static class ProductRegexp
    {
        public static bool IsNameValid(string Name)
        {
            if(string.IsNullOrEmpty(Name) || Name.Length > 50) 
                return false;
            return true;
        }
        public static bool IsCategoryValid(string Category)
        {
            if(string.IsNullOrEmpty (Category) || Category.Length > 50)
                return false;
            return true;
        }
        public static bool IsPriceValid(double? Price)
        {
            if(string.IsNullOrEmpty(Price.ToString()) || !double.TryParse(Price.ToString(), out _) || Price < 0 || Price > 10000000000)
                return false;
            return true;
        }
        public static bool IsSpecificationsValid(string Specifications)
        {
            if (string.IsNullOrEmpty(Specifications) || Specifications.Length > 50)
                return false;
            return true;
        }
        public static bool IsDescriptionValid(string Description)
        {
            if(string.IsNullOrEmpty(Description) || Description.Length > 50)
                return false;
            return true;
        }
        public static bool IsWeightValid(double? Weight)
        {
            if(string.IsNullOrEmpty(Weight.ToString()) || !double.TryParse(Weight.ToString(), out _) || Weight > 10000000000) 
                return false;
            return true;
        }
        public static bool IsHeightValid(double? Height)
        {
            if (string.IsNullOrEmpty(Height.ToString()) || !double.TryParse(Height.ToString(), out _) || Height > 10000000000)
                return false;
            return true;
        }
        public static bool IsWidthValid(double? Width)
        {
            if (string.IsNullOrEmpty(Width.ToString()) || !double.TryParse(Width.ToString(), out _) || Width > 10000000000)
                return false;
            return true;
        }
        public static bool IsLengthValid(double? Length)
        {
            if (string.IsNullOrEmpty(Length.ToString()) || !double.TryParse(Length.ToString(), out _) || Length > 10000000000)
                return false;
            return true;
        }
        public static bool IsManufacturerValid(Manufacturer Manufacturer)
        {
            if (Manufacturer == null)
                return false;
            return true;
        }
        public static bool IsProductActiveValid(bool? IsActive)
        {
            if(string.IsNullOrEmpty(IsActive.ToString()) || !bool.TryParse(IsActive.ToString(), out _))
                return false;
            return true;
        }
    }
}
