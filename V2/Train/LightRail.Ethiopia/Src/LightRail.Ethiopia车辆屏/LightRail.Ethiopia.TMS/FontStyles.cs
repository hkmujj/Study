using System.Drawing;

namespace LightRail.Ethiopia.TMS
{
    /// <summary>
    /// 功能描述：字体样式库
    /// 创建人：唐林
    /// 创建时间：2014-07-15
    /// </summary>
    public class FontStyles
    {
        /// <summary>
        /// 宋体-11号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyle_ES FS_Song_11_CC_B = new FontStyle_ES()
        {
            Font = new Font("宋体", 11),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-13号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyle_ES FS_Song_13_CC_B = new FontStyle_ES()
        {
            Font = new Font("宋体", 13),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-10.5号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyle_ES FS_Song_105_CC_B = new FontStyle_ES()
        {
            Font = new Font("宋体", 10.5f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-10号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyle_ES FS_Song_10_CC_B = new FontStyle_ES()
        {
            Font = new Font("宋体", 10f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-10号-垂直居中-水平居中-白色
        /// </summary>
        public static FontStyle_ES FS_Song_10_CC_W = new FontStyle_ES()
        {
            Font = new Font("宋体", 10f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.White)
        };

        /// <summary>
        /// 宋体-10号-垂直居中-水平靠左-黑色
        /// </summary>
        public static FontStyle_ES FS_Song_10_LC_B = new FontStyle_ES()
        {
            Font = new Font("宋体", 10f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-10.5号-垂直居中-水平靠左-白色
        /// </summary>
        public static FontStyle_ES FS__Song_105_LC_W = new FontStyle_ES()
        {
            Font = new Font("宋体", 10.5f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.White)
        };
    }
}
