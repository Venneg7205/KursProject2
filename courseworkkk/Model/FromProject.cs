using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace courseworkkk.Model
{
    internal class FromRealEstate : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = int.Parse(value.ToString());
            using (ModelContext db = new ModelContext())
            {
                return db.Project.Where(p => p.id_Project == id).Select(p => p.NameProject + "," + p.PriceProject + "," + p.DeadlineProject + "," + p.StateProject).FirstOrDefault();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}