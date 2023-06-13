using System.Collections.Generic;

namespace CompanyCoreLib
{
    public class Analytics
    {
        public List<DateTime> PopularMonths(List<DateTime> purchaseDates)
        {
            Dictionary<DateTime, int> monthCounts = new Dictionary<DateTime, int>();

            foreach (DateTime date in purchaseDates)
            {
                DateTime month = new DateTime(date.Year, date.Month, 1);
                if (monthCounts.ContainsKey(month))
                {
                    monthCounts[month]++;
                }
                else
                {
                    monthCounts.Add(month, 1);
                }
            }

            var sortedMonths = monthCounts.OrderByDescending(x => x.Value).ThenBy(x => x.Key);

            List<DateTime> popularMonths = new List<DateTime>();

            foreach (KeyValuePair<DateTime, int> pair in sortedMonths)
            {
                popularMonths.Add(new DateTime(pair.Key.Year, pair.Key.Month, 1));
            }

            return popularMonths;
        }
    }
    
}