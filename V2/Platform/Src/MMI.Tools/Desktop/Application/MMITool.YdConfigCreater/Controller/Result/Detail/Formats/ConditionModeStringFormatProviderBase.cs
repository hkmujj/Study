using System;
using MMITool.Addin.YdConfigCreater.Model.Condition;

namespace MMITool.Addin.YdConfigCreater.Controller.Result.Detail.Formats
{
    public abstract class ConditionModeStringFormatProviderBase : IFormatProvider, ICustomFormatter
    {
        protected abstract string FortmatTemplate { get; }

        /// <summary>返回一个对象，该对象为指定类型提供格式设置服务。</summary>
        /// <returns>如果 <see cref="T:System.IFormatProvider" /> 实现能够提供该类型的对象，则为 <paramref name="formatType" /> 所指定对象的实例；否则为 null。</returns>
        /// <param name="formatType">一个对象，该对象指定要返回的格式对象的类型。</param>
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }

            return null;
        }

        /// <summary>使用指定的格式和区域性特定格式设置信息将指定对象的值转换为等效的字符串表示形式。</summary>
        /// <returns>
        /// <paramref name="arg" /> 的值的字符串表示形式，按照 <paramref name="format" /> 和 <paramref name="formatProvider" /> 的指定来进行格式设置。</returns>
        /// <param name="format">包含格式规范的格式字符串。</param>
        /// <param name="arg">要设置格式的对象。</param>
        /// <param name="formatProvider">一个对象，它提供有关当前实例的格式信息。</param>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            var model = arg as ConditionModel;
            if (model != null)
            {
                return string.Format(FortmatTemplate, model.DBParam.IP, model.DBParam.Port, model.DBParam.DBName,
                    model.SystemParam.SystemID, model.RunParam.AsyncTcp ? "1" : "0", model.RunParam.AsyncUdp ? "1" : "0",
                    model.RunParam.AsyncMult ? "1" : "0");
            }

            return string.Empty;
        }
    }
}