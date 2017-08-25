using System.Collections.Generic;

namespace CommonUtil.Excel
{
    public interface IExcelAdapter
    {
        /// <summary>
        /// 数据转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> Adapter<T>(IExcelConfig config) where T : IExcelValueProvide;
    }
}
