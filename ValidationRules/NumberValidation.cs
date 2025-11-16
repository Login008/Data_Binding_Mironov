using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Data_Binding_Mironov.ValidationRules
{
    public class NumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string number = value?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(number))
                return new ValidationResult(false, "Введите номер");

            if (number.Length != 10 || !number.All(char.IsDigit))
                return new ValidationResult(false, "Номер должен содержать 10 цифр");

            return ValidationResult.ValidResult;
        }
    }
}
