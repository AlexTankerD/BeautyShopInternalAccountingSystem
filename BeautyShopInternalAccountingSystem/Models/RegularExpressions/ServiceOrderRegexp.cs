using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Models.RegularExpressions
{
    public static class ServiceOrderRegexp
    {
        public static bool IsStartDateValid(string StartDate)
        {
            if (string.IsNullOrEmpty(StartDate) || !DateTime.TryParse(StartDate, out _))
                return false;
            return true;
        }
        public static bool IsCommentValid(string Comment)
        {
            if (Comment.Length > 50)
                return false;
            return true;
        }
    }
}
