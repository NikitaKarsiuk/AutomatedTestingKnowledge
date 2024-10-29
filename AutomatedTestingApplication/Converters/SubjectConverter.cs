using AutomatedTestingApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AutomatedTestingApplication.Converters
{
    class SubjectConverter : IValueConverter
   {
      int id;

      public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
         if (value != null)
         {
            id = (int)value;

            using (DataContext db = new DataContext())
               return $"{db.Subject.Find(id).Name}";
         }
         return null;
      }

      public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
      {
         return id;
      }
   }
}
