using System;
using MMITool.Addin.YdConfigCreater.Model.Condition;

namespace MMITool.Addin.YdConfigCreater.Extension
{
    public static class ConditionModelExtension 
    {
        /// <summary>
        /// 格式化输出 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public static string ToString(this ConditionModel model, IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "{0}", model);
        }
    }
}