using System.Globalization;
using System.Windows.Controls;

namespace TimeCraft_WPF_App.Validators
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string str && !string.IsNullOrEmpty(str))
            {
                return ValidationResult.ValidResult;
            }
            else if (value is DateTime selectedDate && selectedDate != default)
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Field is required.");
            }
        }
    }
}
