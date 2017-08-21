using System.Drawing;

namespace ES.Facility.Common
{
    /// <summary>
    /// 功能描述：字体样式库
    /// 创建人：lih
    /// 创建时间：2014-07-15
    /// </summary>
    public class FontStyles
    {
        /// <summary>
        /// 宋体-12号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyleEs FsSong12CcB = new FontStyleEs()
        {
            Font = new Font("宋体", 12),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-11号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyleEs FsSong11CcB = new FontStyleEs()
        {
            Font = new Font("宋体", 11),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-13号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyleEs FsSong13CcB = new FontStyleEs()
        {
            Font = new Font("宋体", 13),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-14号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyleEs FsSong14CcB = new FontStyleEs()
        {
            Font = new Font("宋体", 14),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-10.5号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyleEs FsSong105CcB = new FontStyleEs()
        {
            Font = new Font("宋体", 10.5f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-10号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyleEs FsSong10CcB = new FontStyleEs()
        {
            Font = new Font("宋体", 10f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-10号-垂直居中-水平居中-蓝色
        /// </summary>
        public static FontStyleEs FsSong10CcBlue = new FontStyleEs()
        {
            Font = new Font("宋体", 10f, FontStyle.Bold),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Blue)
        };

        /// <summary>
        /// 宋体-10号-垂直居中-水平居中-白色
        /// </summary>
        public static FontStyleEs FsSong10CcW = new FontStyleEs()
        {
            Font = new Font("宋体", 10f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.White)
        };

        /// <summary>
        /// 宋体-14号-垂直居中-水平居中-白色
        /// </summary>
        public static FontStyleEs FsSong14CcW = new FontStyleEs()
        {
            Font = new Font("宋体", 14f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.White)
        };

        /// <summary>
        /// 宋体-10号-垂直居中-水平靠左-黑色
        /// </summary>
        public static FontStyleEs FsSong10LcB = new FontStyleEs()
        {
            Font = new Font("宋体", 10f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-10.5号-垂直居中-水平靠左-白色
        /// </summary>
        public static FontStyleEs FsSong105LcW = new FontStyleEs()
        {
            Font = new Font("宋体", 10.5f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.White)
        };

        /// <summary>
        /// 宋体-20号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyleEs FsSong20CcB = new FontStyleEs()
        {
            Font = new Font("宋体", 20f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-16号-垂直居中-水平居中-黑色
        /// </summary>
        public static FontStyleEs FsSong16CcB = new FontStyleEs()
        {
            Font = new Font("宋体", 16f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.Black)
        };
      
        /// <summary>
        /// 宋体-16号-垂直居中-水平居中-白色
        /// </summary>
        public static FontStyleEs FsSong16CcWhite = new FontStyleEs()
        {
            Font = new Font("宋体", 16f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.White)
        };
        /// <summary>
        /// 宋体-14号-垂直居中-水平居中-白色
        /// </summary>
        public static FontStyleEs FsSong14CcWhite = new FontStyleEs()
        {
            Font = new Font("宋体", 14f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.White)
        };
        /// <summary>
        /// 宋体-14号-垂直居中-水平居中-白色
        /// </summary>
        public static FontStyleEs FsSong14CcWAndB = new FontStyleEs()
        {
            Font = new Font("宋体", 14f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.White),
            TextUpBrush=new SolidBrush(Color.White),
            TextDownBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-20号-垂直居中-水平居中-白色
        /// </summary>
        public static FontStyleEs FsSong20CcWAndB = new FontStyleEs()
        {
            Font = new Font("宋体", 20f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.White),
            TextUpBrush = new SolidBrush(Color.White),
            TextDownBrush = new SolidBrush(Color.Black)
        };

        /// <summary>
        /// 宋体-13号-垂直居中-水平居中-白色
        /// </summary>
        public static FontStyleEs FsSong13CcWAndB = new FontStyleEs()
        {
            Font = new Font("宋体", 13f),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = new SolidBrush(Color.White),
            TextUpBrush = new SolidBrush(Color.White),
            TextDownBrush = new SolidBrush(Color.Black)
        };
    }
}
