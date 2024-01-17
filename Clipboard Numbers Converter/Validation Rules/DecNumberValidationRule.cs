using System.Globalization;
using System.Windows.Controls;

namespace CN_Converter.Validation_Rules
{
    internal class DecNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(long.TryParse((string)value, out _))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Cannot convert the value provided into a DEC value");
            }
        }
    }
}
