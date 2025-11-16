using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Data_Binding_Mironov.Converters
{
    public class AppointmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableCollection<HistoryAppointment> dates)
            {
                if (dates.Count > 0)
                {
                    var lastDate = dates.Last();
                    DateTime.TryParse(lastDate.Date, culture, DateTimeStyles.None, out DateTime lastDate1);
                    var daysPassed = (DateTime.Now - lastDate1).Days;
                    return $"Прошло {daysPassed} дней с момента последнего приёма";
                }
            }

            return "Первый прием";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
