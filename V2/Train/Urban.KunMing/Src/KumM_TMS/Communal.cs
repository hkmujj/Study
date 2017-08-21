using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.Interface;

using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace KumM_TMS
{
    public class FormatStyle
    {
        private static float m_Scale = 1.0F;
        /// <summary>
        /// 比例尺寸
        /// </summary>
        public static float Scale
        {
            get { return m_Scale; }
        }

        /// <summary>
        /// 屏横向移动像素
        /// </summary>
        public static int ScreenMoveX { get; private set; }

        /// <summary>
        /// 屏纵向移动像素
        /// </summary>
        public static int ScreenMoveY { get; private set; }

        #region :::::::::::::::::::::::::::::: 线条 :::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// 宽1白线
        /// </summary>
        public static readonly Pen WhitePen = new Pen(Color.FromArgb(255, 255, 255), 1 * Scale);

        /// <summary>
        /// 宽1白线
        /// </summary>
        public static readonly Pen WhitePen2 = new Pen(Color.FromArgb(255, 255, 255), 2 * Scale);

        /// <summary>
        /// 宽1黑线
        /// </summary>
        public static readonly Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0), 1 * Scale);

        /// <summary>
        /// 宽3中灰
        /// </summary>
        public static readonly Pen MediumGreyPen = new Pen(Color.FromArgb(150, 150, 150), 3 * Scale);

        /// <summary>
        /// 宽1深灰
        /// </summary>
        public static readonly Pen DarkGreyPen = new Pen(Color.FromArgb(85, 85, 85));
        #endregion

        #region :::::::::::::::::::::::::::::: 颜色 :::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// 白色
        /// </summary>
        public static readonly SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));

        /// <summary>
        /// 黑色
        /// </summary>
        public static readonly SolidBrush BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));

        /// <summary>
        /// 淡绿
        /// </summary>
        public static readonly SolidBrush LightGreenBrush = new SolidBrush(Color.FromArgb(45, 200, 51));

        /// <summary>
        /// 深灰
        /// </summary>
        public static readonly SolidBrush DarkGreyBrush = new SolidBrush(Color.FromArgb(97, 112, 131));

        /// <summary>
        /// 中灰
        /// </summary>
        public static readonly SolidBrush MediumGreySolidBrush = new SolidBrush(Color.FromArgb(150, 150, 150));

        /// <summary>
        /// 50%灰
        /// </summary>
        public static readonly SolidBrush HalfPGreySolidBrush = new SolidBrush(Color.FromArgb(128, 128, 128));

        /// <summary>
        /// 黄色
        /// </summary>
        public static readonly SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 0));

        /// <summary>
        /// 红色
        /// </summary>
        public static readonly SolidBrush RedBrush = new SolidBrush(Color.FromArgb(191, 0, 2));

        /// <summary>
        /// 蓝色
        /// </summary>
        public static readonly SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(101, 127, 162));
        #endregion

        #region :::::::::::::::::::::::::::::: 字体 :::::::::::::::::::::::::::::::::::::::
        public const string StrFont = "宋体";
        public static readonly Font Font8 = new Font(StrFont, 8f * Scale);
        public static readonly Font Font10 = new Font(StrFont, 10f * Scale);
        public static readonly Font Font12 = new Font(StrFont, 12f * Scale);
        public static readonly Font Font14 = new Font(StrFont, 14f * Scale);
        public static readonly Font Font16 = new Font(StrFont, 16f * Scale);
        public static readonly Font Font18 = new Font(StrFont, 18f * Scale);
        public static readonly Font Font20 = new Font(StrFont, 20f * Scale);
        public static readonly Font Font22 = new Font(StrFont, 22f * Scale);
        public static readonly Font Font24 = new Font(StrFont, 24f * Scale);
        public static readonly Font Font26 = new Font(StrFont, 26f * Scale);
        public static readonly Font Font32 = new Font(StrFont, 32f * Scale);
        public static readonly Font Font34 = new Font(StrFont, 34f * Scale);
        public static readonly Font Font38 = new Font(StrFont, 38f * Scale);
        public static readonly Font Font64 = new Font(StrFont, 64f * Scale);

        public static readonly Font Font8B = new Font(StrFont, 8f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font10B = new Font(StrFont, 10f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font12B = new Font(StrFont, 12f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font14B = new Font(StrFont, 14f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font16B = new Font(StrFont, 16f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font18B = new Font(StrFont, 18f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font20B = new Font(StrFont, 20f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font22B = new Font(StrFont, 22f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font24B = new Font(StrFont, 24f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font26B = new Font(StrFont, 26f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font32B = new Font(StrFont, 32f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font34B = new Font(StrFont, 34f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font38B = new Font(StrFont, 38f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static readonly Font Font64B = new Font(StrFont, 64f * Scale, Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        #endregion

        #region :::::::::::::::::::::::::::::: 文字 :::::::::::::::::::::::::::::::::::::::
        public static readonly string[] Str1 = new string[7] { "运行", "车辆状态", "空调状态", "事件", "通讯状态", "帮助", "检修" };
        public static readonly string[] Str2 = new string[11] { "空气压缩机", "牵引系统", "BHB/BLB", "乘客报警", "烟火报警", "客室温度", "1侧门", "2侧门", "制动缸压力", "停放制动", "空气制动" };
        public static readonly string[] Str3 = new string[6] { "0001", "0002", "0003", "0004", "0005", "0006" };
        public static readonly string[] Str4 = new string[24] { "1", "3", "2", "4", "1", "3", "2", "4", "1", "3", "2", "4", "4", "2", "3", "1", "4", "2", "3", "1", "4", "2", "3", "1" };
        public static readonly string[] Str5 = new string[24] { "1", "2", "3", "4", "5", "6", "7", "8", "1", "2", "3", "4", "5", "6", "7", "8", "1", "2", "3", "4", "5", "6", "7", "8" };
        public static readonly string[] Str6 = new string[24] { "8", "7", "6", "5", "4", "3", "2", "1", "8", "7", "6", "5", "4", "3", "2", "1", "8", "7", "6", "5", "4", "3", "2", "1" };
        public static readonly string[] 显示模式 = new string[4] { "ATP", "ATO", "SM", "退行模式" };
        public static readonly string[] 制动模式 = new string[4] { "停放制动", "常用制动", "紧急制动", "保持制动" };
        public static readonly string[] 站点按键 = new string[3] { "紧急广播", "站点设置", "旁路信息" };

        public static readonly string[] Str7 = new string[10] { "VVVF", "主断状态", "中间电压(V)", "电机电流(A)", "SIV", "输出电压(V)", "110V输出(V)", "充电电流(A)", "总风压力", "空簧压力" };
        public static readonly string[] Str8 = new string[7] { "室内温度", "室外温度", "空调目标温度", "空调模式", "温度模式", "压缩机状态", "空调设置" };
        public static readonly string[] Str9 = new string[7] { "-1k", "-2k", "+1k", "+2k", "UIC模式", "预冷关闭", "火灾模式" };
        public static readonly string[] Str10 = new string[24] { "1", "2", "3", "4", "1", "2", "3", "4", "1", "2", "3", "4", "4", "3", "2", "1", "4", "3", "2", "1", "4", "3", "2", "1" };

        public static readonly string[] Str11 = new string[62] {"VCM", "VCM", "REP", "REP", "REP", "REP", "REP", "REP", "DXM1", "DXM2", "ATC", "PIS", "HAVC1", "FDS", "HMI", "ERM", "DIM", "AXM", "HAVC2", "BCU"
        , "HMI", "ERM", "DIM", "AXM", "HAVC2", "BCU", "DXM1", "DXM2", "ATC", "PIS", "HAVC1", "FDS", "DXM1", "DXM2", "DCU", "BCU", "HAVC1", "HAVC2", "DXM1", "DXM2", "DCU", "BCU", "HAVC1", "HAVC2",
        "", "", "", "", "", "", "SIV", "SIV", "DXM1", "DXM2", "DCU", "HAVC1", "HAVC2", "DXM1", "DXM2", "DCU", "HAVC1", "HAVC2" };

        public static readonly string[] Str12 = new string[5] { "NO", "等级", "代码", "故障内容", "时间" };

        public static readonly string[] Str13 = new string[3] { "现存\n故障", "故障\n履历", "操作\n提示" };

        public static readonly string[] Str14 = new string[30] { "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME",
            "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", 
            "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME",
            "NAME", "NAME", "NAME", "NAME", "NAME", "返回", };

        public static readonly string[] Str15 = new string[12] { "全自动\n广播", "半自动\n广播", "1号线", "2号线", "3号线", "6号线", "Route选择", "返回", "始发站:", "终点站:", "Line_ID", "Route_ID" };
        public static readonly string[] Str16 = new string[13] { "旁路投入", "旁路切除", "警惕旁路", "停放制动缓解旁路", "所有制动缓解旁路", "总风压力可用旁路", "ATC旁路", "门零速旁路", "门关好旁路", "车钩监视旁路", "BHB切除", "ATC ZVRD监控", "门能使旁路" };
        public static readonly string[] Str17 = new string[4] { "BHB", "BLB", "BLB", "BHB"};
        public static readonly string[] Str18 = { "人工模式", "ATP模式", "ATO模式", "SM模式", "退行模式","救援模式","慢行模式" };
        public static readonly string[] Str19 = {"停放制动", "制动", "紧急制动", "保持制动", "牵引"};
        

        static FormatStyle()
        {
            ScreenMoveY = 0;
            ScreenMoveX = 0;
        }

        #endregion
    }
}
