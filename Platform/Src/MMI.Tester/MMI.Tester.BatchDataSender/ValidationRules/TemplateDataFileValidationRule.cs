using System.Globalization;
using System.IO;
using System.Windows.Controls;

namespace MMI.Tester.BatchDataSender.ValidationRules
{
    public class TemplateDataFileValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var file = (string) value;
            if (string.IsNullOrEmpty(file))
            {
                return new ValidationResult(false, "Value is null.");
            }
            if (!File.Exists(file))
            {
                return new ValidationResult(false, "File is not exist.");
            }
            var ext = Path.GetExtension(file);
            if (!ext.Contains("xls") && !ext.Contains("csv"))
            {
                return new ValidationResult(false, "File is not .xls or .csv type");
            }

            return new ValidationResult(true, string.Empty);
        }
    }
}