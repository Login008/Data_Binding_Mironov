using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Data_Binding_Mironov.Converters
{
    public class IsOldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
                if (DateTime.TryParse(value as string, culture, DateTimeStyles.None, out DateTime result))
                {
                    if (result.Date <= DateTime.Today && result.Date.ToString() != "")
                    {
                        int age = DateTime.Now.Year - result.Year;
                        if (DateTime.Now.DayOfYear < result.DayOfYear)
                            age--;

                        return age >= 18 ? "Совершеннолетний" : "Несовершеннолетний";
                    }
                }

            return "Неверный формат записи";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
