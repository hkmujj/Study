using System;

namespace Mmi.Common.DateTimeInterpreter
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDateTimeInterpreter
    {
        /// <summary>
        /// 解释 float 数组为时间
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        DateTime Interpreter(params float[] rawData);
    }
}