using System.Drawing;

namespace ES.Facility.Common
{
    /// <summary>
    /// 功能描述：字体样式库
    /// 创建人：唐林
    /// 创建时间：2014-07-15
    /// </summary>
    public class FontStyles
    {
        /// <summary>
        /// 宋体-12号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyleEs m_FsSong12CcB = new FontStyleEs()
        {
            Font = new Font("宋体", 12),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-11号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyleEs m_FsSong11CcB = new FontStyleEs()
        {
            Font = new Font("宋体", 11),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-13号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyleEs m_FsSong13CcB = new FontStyleEs()
        {
            Font = new Font("宋体", 13),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-10.5号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyleEs m_FsSong105CcB = new FontStyleEs()
        {
            Font = new Font("宋体", 10.5f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-10号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyleEs m_FsSong10CcB = new FontStyleEs()
        {
            Font = new Font("宋体", 10f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-10号-垂直居中-水平居中-蓝色
        /// </summary>
        public static FontStyleEs m_FsSong10CcBlue = new FontStyleEs()
        {
            Font = new Font("宋体", 10f, FontStyle.Bold),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Blue)
        };

        /// <summary>
        /// 宋体-10号-垂直居中-水平居中-白色
        /// </summary>
        public static FontStyleEs m_FsSong10CcW = new FontStyleEs()
        {
            Font = new Font("宋体", 10f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.White)
        };

        /// <summary>
        /// 宋体-10号-垂直居中-水平靠左-黑色
        /// </summary>
        public static FontStyleEs m_FsSong10LcB = new FontStyleEs()
        {
            Font = new Font("宋体", 10f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-10.5号-垂直居中-水平靠左-白色
        /// </summary>
        public static FontStyleEs m_FsSong105LcW = new FontStyleEs()
        {
            Font = new Font("宋体", 10.5f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.White)
        };

        /// <summary>
        /// 宋体-20号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyleEs m_FsSong20CcB = new FontStyleEs()
        {
            Font = new Font("宋体", 20f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };
    }
}
