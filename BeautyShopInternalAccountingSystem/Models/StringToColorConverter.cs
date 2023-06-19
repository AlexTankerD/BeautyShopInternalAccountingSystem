using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace BeautyShopInternalAccountingSystem.Models
{
    internal class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value == null)
            {
                return (Brush)System.ComponentModel.TypeDescriptor
        .GetConverter(typeof(Brush)).ConvertFromInvariantString("Black");
            }
            StringBuilder builder = new StringBuilder(value.ToString());
            builder.Replace("System.Windows.Media.Color ", "");
            return (Brush)System.ComponentModel.TypeDescriptor
        .GetConverter(typeof(Brush)).ConvertFromInvariantString(builder.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
