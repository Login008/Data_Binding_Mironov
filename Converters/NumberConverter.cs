using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Data_Binding_Mironov.Converters
{
    public class NumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return "Пусто";

            string phone = value.ToString().Trim();

            if (phone.Length != 10)
                return "Неверный формат";

            if (!phone.All(char.IsDigit))
                return "Все символы должны быть цифрами";

            return $"+7 ({phone.Substring(0, 3)}) {phone.Substring(3, 3)}-{phone.Substring(6, 2)}-{phone.Substring(8, 2)}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
