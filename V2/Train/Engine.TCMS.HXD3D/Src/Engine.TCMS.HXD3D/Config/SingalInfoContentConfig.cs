using Excel.Interface;

namespace Engine.TCMS.HXD3D.Config
{
    [ExcelLocation("过程数据-网络控制-信号信息内容配置.xls", "Sheet1")]
    public class SingalInfoContentConfig : ISetValueProvider
    {
        [ExcelField("GroupName")]
        public string GroupName { get; private set; }

        [ExcelField("X")]
        public int X { get; private set; }

        [ExcelField("Y")]
        public int Y { get; private set; }

        [ExcelField("Width")]
        public int Width { get; private set; }

        [ExcelField("Height")]
        public int Height { get; private set; }

        [ExcelField("Content")]
        public string Content { get; private set; }

        [ExcelField("InBoolName")]
        public string InBoolName { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {
        }
    }
}