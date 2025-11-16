using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Data_Binding_Mironov.ValidationRules
{
    public class DateValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string dateStr = value?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(dateStr))
                return new ValidationResult(false, "Введите дату");

            if (!DateTime.TryParse(dateStr, out DateTime date))
                return new ValidationResult(false, "Неверный формат даты");

            if (date > DateTime.Now)
                return new ValidationResult(false, "Дата не может быть будущей");

            return ValidationResult.ValidResult;
        }
    }
}
