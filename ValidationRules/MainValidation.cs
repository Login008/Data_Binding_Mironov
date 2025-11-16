using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Data_Binding_Mironov.ValidationRules
{
    public class MainValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = value?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(text))
                return new ValidationResult(false, "Поле должно быть заполнено");

            if (text.Length < 2)
                return new ValidationResult(false, "Минимум 2 символа");

            return ValidationResult.ValidResult;
        }
    }
}
