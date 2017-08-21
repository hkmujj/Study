using System.IO;
using Excel.Interface;

namespace Motor.ATP._200H.ConfigModel
{
    public class ATP200HConfig
    {
        public const string FileName = "ATP200HConfig.xml";


        public ExcelReaderConfig InBoolConfig { set; get; }

        public ExcelReaderConfig OutBoolConfig { set; get; }

        public ExcelReaderConfig InFloatConfig { set; get; }

        public ExcelReaderConfig OutFloatConfig { set; get; }

        internal void Correct(string configPath)
        {
            if (InBoolConfig != null)
            {
                InBoolConfig.File = Path.Combine(configPath, InBoolConfig.File);
            }
        }
    }
}