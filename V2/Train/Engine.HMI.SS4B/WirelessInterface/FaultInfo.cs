using Excel.Interface;
using System.Drawing;

namespace SS4B_TMS.WirelessInterface
{
    [ExcelLocation("编组诊断信息.xls", "Sheet1")]
    public class FaultInfo : ISetValueProvider
    {
        [ExcelField("Index")]
        public int Index { get; set; }

        [ExcelField("FaultInfo")]
        public string Title { get; set; }

        [ExcelField("Info")]
        public string Info { get; set; }

        [ExcelField("Logic")]
        public string LogicName { get; set; }

        [ExcelField("默认背景色")]
        public Color DefaultBack { get; set; }

        [ExcelField("默认字体颜色")]
        public Color DefaultFon { get; set; }

        [ExcelField("默认边框颜色")]
        public Color DefaultFram { get; set; }

        [ExcelField("故障背景色")]
        public Color FaultBack { get; set; }

        [ExcelField("故障字体颜色")]
        public Color FaultFon { get; set; }

        [ExcelField("故障边框颜色")]
        public Color FaultFram { get; set; }

        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Index":
                    Index = value.ConvertToInt();
                    break;

                case "Title":
                    Title = value;
                    break;

                case "Info":
                    Info = value;
                    break;

                case "LogicName":
                    LogicName = value;
                    break;

                case "DefaultBack":
                    DefaultBack = value.ConvertToColor();
                    break;

                case "DefaultFon":
                    DefaultFon = value.ConvertToColor();
                    break;

                case "DefaultFram":
                    DefaultFram = value.ConvertToColor();
                    break;

                case "FaultBack":
                    FaultBack = value.ConvertToColor();
                    break;

                case "FaultFon":
                    FaultFon = value.ConvertToColor();
                    break;

                case "FaultFram":
                    FaultFram = value.ConvertToColor();
                    break;
            }
        }
    }

    public static class ColorExtend
    {
        /// <summary>
        /// 转换为System.Drawing.Color类型
        /// </summary>
        /// <param name="value">数据格式为[R,G,B]样式的字符串</param>
        /// <returns></returns>
        public static Color ConvertToColor(this string value)
        {
            var slip = value.Split(',');
            var color = new Color();
            if (slip.Length == 3)
            {
                color = Color.FromArgb(255, slip[0].ConvertToInt(), slip[1].ConvertToInt(), slip[2].ConvertToInt());
            }
            return color;
        }

        public static int ConvertToInt(this string value)
        {
            int resout = int.MinValue;
            double tmp = 0;
            if (double.TryParse(value, out tmp))
            {
                resout = (int)tmp;
            }
            return resout;
        }
    }
}