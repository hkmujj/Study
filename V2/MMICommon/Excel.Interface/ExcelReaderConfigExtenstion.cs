using System.Data;
using System.IO;
using CommonUtil.Util;

namespace Excel.Interface
{
    /// <summary>
    /// ExcelReaderConfig extension
    /// </summary>
    public static class ExcelReaderConfigExtenstion
    {
        /// <summary>
        /// 根据配置文件, 适配出指定内容
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static DataSet Adapter(this IExcelReaderConfig config)
        {
            var extens = Path.GetExtension(config.File);
            switch (extens)
            {
                case ".csv" :
                    return ( new CSVAdapter() ).Adapter(config);
                case ".xls" :
                    return ( new ExcelAdapter() ).Adapter(config);
                default :
                    LogMgr.Warn(string.Format("Can not parser file {0}.", config.File));
                    break;
            }

            return new DataSet(config.File);
        }

        /// <summary>
        /// 根据配置文件, 适配出指定内容
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static DataSet Adapter(this ExcelReaderConfig config)
        {
            return ((IExcelReaderConfig) config).Adapter();
        }
    }
}