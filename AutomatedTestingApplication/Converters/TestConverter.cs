using AutomatedTestingApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AutomatedTestingApplication.Converters
{
    class TestConverter : IValueConverter
    {
      int id;

      public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
         id = (int)value;

         using (DataContext db = new DataContext())
            return $"{db.Test.Find(id).Name}";
      }

      public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
         return id;
      }
   }
}
