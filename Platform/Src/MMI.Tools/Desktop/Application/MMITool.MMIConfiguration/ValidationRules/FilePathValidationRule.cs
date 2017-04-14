using System.Globalization;
using System.IO;
using System.Windows.Controls;

namespace MMITool.Addin.MMIConfiguration.ValidationRules
{
    /// <summary>
    /// 验证文件是否符合规范
    /// </summary>
    public class FilePathValidationRule:ValidationRule
    {
        /// <summary>
        /// 重写验证事件
        /// </summary>
        /// <param name="value">传进来的对象</param>
        /// <param name="cultureInfo">区域信息</param>
        /// <returns>返回验证结果</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stValu = (string) value;
            if (string.IsNullOrEmpty(stValu))
            {
                return new ValidationResult(true, null);
            }
            return File.Exists((string)value) ? new ValidationResult(true, null) : new ValidationResult(false, "文件不存在!");
        }
    }
}
