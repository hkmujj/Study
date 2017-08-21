using System;
using System.Drawing;

namespace Urban.Wuxi.TMS
{
    public class FormatStyle
    {
        private static readonly float m_Scale = 1.0f;
        /// <summary>
        /// 比例尺寸
        /// </summary>
        public static float Scale { get { return m_Scale; } }

        private static readonly int m_ScreenMoveX = 0;
        /// <summary>
        /// 屏横向移动像素
        /// </summary>
        public static int ScreenMoveX { get { return m_ScreenMoveX; } }

        private static readonly int m_ScreenMoveY = 0;
        /// <summary>
        /// 屏纵向移动像素
        /// </summary>
        public static int ScreenMoveY { get { return m_ScreenMoveY; } }


        public static StringFormat m_Cneter = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        #region :::::::::::::::::::::::::::::: 线条 :::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// 宽1白线
        /// </summary>
        public static Pen m_WhitePen = new Pen(Color.FromArgb(255, 255, 255), 1 * m_Scale);

        /// <summary>
        /// 宽1黑线
        /// </summary>
        public static Pen m_BlackPen = new Pen(Color.FromArgb(0, 0, 0), 1 * m_Scale);

        /// <summary>
        /// 宽3中灰
        /// </summary>
        public static Pen m_MediumGreyPen = new Pen(Color.FromArgb(150, 150, 150), 3 * m_Scale);

        /// <summary>
        /// 宽1深灰
        /// </summary>
        public static Pen m_DarkGreyPen = new Pen(Color.FromArgb(85, 85, 85));
        #endregion

        #region :::::::::::::::::::::::::::::: 颜色 :::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// 白色
        /// </summary>
        public static SolidBrush m_WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));

        /// <summary>
        /// 黑色
        /// </summary>
        public static SolidBrush m_BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));

        /// <summary>
        /// 淡绿
        /// </summary>
        public static SolidBrush m_LightGreenBrush = new SolidBrush(Color.FromArgb(45, 200, 51));

        /// <summary>
        /// 深灰
        /// </summary>
        public static SolidBrush m_DarkGreyBrush = new SolidBrush(Color.FromArgb(97, 112, 131));

        /// <summary>
        /// 中灰
        /// </summary>
        public static SolidBrush m_MediumGreySolidBrush = new SolidBrush(Color.FromArgb(150, 150, 150));

        /// <summary>
        /// 50%灰
        /// </summary>
        public static SolidBrush m_HalfPGreySolidBrush = new SolidBrush(Color.FromArgb(128, 128, 128));

        /// <summary>
        /// 黄色
        /// </summary>
        public static SolidBrush m_YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 0));

        /// <summary>
        /// 桔黄色
        /// </summary>
        public static SolidBrush m_OrangeBrush = new SolidBrush(Color.Orange);

        /// <summary>
        /// 红色
        /// </summary>
        public static SolidBrush m_RedBrush = new SolidBrush(Color.FromArgb(191, 0, 2));

        /// <summary>
        /// 蓝色
        /// </summary>
        public static SolidBrush m_BlueBrush = new SolidBrush(Color.FromArgb(101, 127, 162));
        /// <summary>
        /// 浅灰
        /// </summary>
        public static SolidBrush m_GreyBrush = new SolidBrush(Color.FromArgb(85, 85, 85));
        /// <summary>
        /// 绿色
        /// </summary>
        public static SolidBrush m_GreenBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
        /// <summary>
        /// 白灰
        /// </summary>
        public static SolidBrush m_BgrBursh = new SolidBrush(Color.FromArgb(170, 170, 170));
        /// <summary>
        ///淡蓝色
        /// </summary>
        public static SolidBrush m_ThinBlue = new SolidBrush(Color.FromArgb(0, 109, 189));
        #endregion

        #region :::::::::::::::::::::::::::::: 字体 :::::::::::::::::::::::::::::::::::::::
        public const String StrFont = "宋体";
        public static Font m_Font8 = new Font(StrFont, 8f * m_Scale);
        public static Font m_Font10 = new Font(StrFont, 10f * m_Scale);
        public static Font m_Font12 = new Font(StrFont, 12f * m_Scale);
        public static Font m_Font14 = new Font(StrFont, 14f * m_Scale);
        public static Font m_Font16 = new Font(StrFont, 16f * m_Scale);
        public static Font m_Font18 = new Font(StrFont, 18f * m_Scale);
        public static Font m_Font20 = new Font(StrFont, 20f * m_Scale);
        public static Font m_Font22 = new Font(StrFont, 22f * m_Scale);
        public static Font m_Font24 = new Font(StrFont, 24f * m_Scale);
        public static Font m_Font26 = new Font(StrFont, 26f * m_Scale);
        public static Font m_Font32 = new Font(StrFont, 32f * m_Scale);
        public static Font m_Font34 = new Font(StrFont, 34f * m_Scale);
        public static Font m_Font38 = new Font(StrFont, 38f * m_Scale);
        public static Font m_Font64 = new Font(StrFont, 64f * m_Scale);

        public static Font m_Font8B = new Font(StrFont, 8f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font10B = new Font(StrFont, 10f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font12B = new Font(StrFont, 12f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font14B = new Font(StrFont, 14f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font16B = new Font(StrFont, 16f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font18B = new Font(StrFont, 18f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font20B = new Font(StrFont, 20f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font22B = new Font(StrFont, 22f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font24B = new Font(StrFont, 24f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font26B = new Font(StrFont, 26f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font32B = new Font(StrFont, 32f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font34B = new Font(StrFont, 34f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font38B = new Font(StrFont, 38f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font m_Font64B = new Font(StrFont, 64f * m_Scale, m_Scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        #endregion

        #region :::::::::::::::::::::::::::::: 文字 :::::::::::::::::::::::::::::::::::::::
        public static String[] m_Str1 = new String[7] { "运行", "车辆状态", "空调状态", "事件", "通讯状态", "帮助", "检修" };
        public static String[] m_Str2 = new String[10] { "空气压缩机", "牵引系统", "乘客报警", "烟火报警", "BHB", "BLB", "停放制动", "空气制动", "客室温度", "电制动可用性" };
        public static String[] m_Str3 = new String[6] { "TC1", "M11", "M21", "M22", "M12", "TC2" };
        public static String[] m_Str4 = new String[24] { "1", "3", "2", "4", "1", "3", "2", "4", "1", "3", "2", "4", "4", "2", "3", "1", "4", "2", "3", "1", "4", "2", "3", "1" };
        public static String[] m_Str5 = new String[24] { "1", "2", "3", "4", "5", "6", "7", "8", "1", "2", "3", "4", "5", "6", "7", "8", "1", "2", "3", "4", "5", "6", "7", "8" };
        public static String[] m_Str6 = new String[24] { "8", "7", "6", "5", "4", "3", "2", "1", "8", "7", "6", "5", "4", "3", "2", "1", "8", "7", "6", "5", "4", "3", "2", "1" };
        public static String[] m_显示模式 = new String[4] { "ATP", "ATO", "SM", "退行模式" };
        public static String[] m_制动模式 = new String[4] { "停放制动", "常用制动", "紧急制动", "保持制动" };
        public static String[] m_站点按键 = new String[9] { "", "", "", "", "", "紧急广播", "站点设置", "", "" };

        public static String[] m_Str7 = new String[10] { "主断状态", "中间电压(V)", "电机电流(A)", "SIV", "380V输出(V)", "110V输出(V)", "充电电流(A)", "总风压力", "空簧压力", "制动缸压力" };
        public static String[] m_Str8 = new String[9] { "室内温度", "室外温度", "空调目标温度", "空调状态(机组1)", "空调状态(机组2)", "空调模式(机组1)", "空调模式(机组2)", "空调机组减载", "压缩机状态" };
        public static String[] m_Str9 = new String[9] { "22", "24", "26", "28", "通风", "UIC模式", "关闭预冷或预热", "", "火灾模式" };
        public static String[] m_Str10 = new String[24] { "1", "2", "3", "4", "1", "2", "3", "4", "1", "2", "3", "4", "4", "3", "2", "1", "4", "3", "2", "1", "4", "3", "2", "1" };

        public static String[] m_Str11 = new String[64] { "REP", "REP", "REP", "REP", "REP", "REP",
                                                "VCM", "DIM", "SIV", "HMI", "AXM", "PIS", "ATC", "ERM",
                                                "DXM1", "DXM2", "BCU", "HVAC1", "HVAC2", "EDCU1", "EDCU2",
                                                "DXM1", "DCU", "HVAC1", "HVAC2", "EDCU1", "EDCU2",
                                                "DXM1", "DXM2", "DCU", "BCU", "HVAC1", "HVAC2", "EDCU1", "EDCU2",
                                                "DXM1", "DXM2", "DCU", "BCU", "HVAC1", "HVAC2", "EDCU1", "EDCU2",
                                                "DXM1", "DCU", "HVAC1", "HVAC2", "EDCU1", "EDCU2",
                                                "DXM1", "DXM2", "BCU", "HVAC1", "HVAC2", "EDCU1", "EDCU2",
                                                "VCM(M)", "DIM", "SIV", "HMI", "AXM", "PIS", "ATC", "ERM",};

        public static String[] m_Str12 = new String[5] { "NO", "等级", "代码", "故障内容", "时间" };

        public static String[] m_Str13 = new String[3] { "现存\n故障", "故障\n履历", "操作\n提示" };

        public static String[] m_Str14 = new String[30] { "列车延误", "列车清客", "区间清客", "越站", "稳定乘客情绪", "预留", "预留", "预留",
            "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", 
            "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME", "NAME",
            "NAME", "NAME", "NAME", "NAME", "NAME", "返回", };

        public static String[] m_Str15 = { "全自动\n广播", "半自动\n广播", "手动\n模式", "", "1号线", "2号线", "路径\n设置", "返回", "始发站", "终点站" };
        public static String[] m_Str16 = new String[13] { "旁路投入", "旁路切除", "警惕旁路", "停放制动缓解旁路", "所有制动缓解旁路", "总风压力可用旁路", "ATC旁路", "门零速旁路", "门关好旁路", "车钩监视旁路", "BHB切除", "ATC ZVRD监控", "门能使旁路" };
        public static String[] m_Str17 = new String[4] { "BHB", "BLB", "BLB", "BHB" };
        public static String[] m_Str18 = { "人工模式", "ATP模式", "ATO模式", "ATB模式", "退行模式", "紧急牵引模式", "救援模式", "慢行模式", "RMF模式", "RMP模式", "未知" };
        public static String[] m_Str19 = { "停放制动", "常用制动", "紧急制动", "保持制动", "牵引" };
        public static String[] m_Str20 = { "紧急\n制动", "牵引\n封锁", "限速", "请转紧\n急牵引", "其他\n信息", "", " 返回" };
        public static String[] m_Str21 = { "紧急制动条件提示", "牵引封锁条件提示", "限速条件提示", "以下条件触发时建议转入紧急牵引模式", "其他信息提示" };
        public static String[] m_Str22 = { "紧急制动", "牵引封锁", "请转紧急牵引", "限速", "其他" };
        public static String[] m_Str23 = { "1B", "2B", "3B", "4B" };
        public static String[] m_Str24 = { "1A", "2A", "3A", "4A" };
        public static String[] m_Str25 = { "4B", "3B", "2B", "1B" };
        public static String[] m_Str26 = { "4A", "3A", "2A", "1A" };
        public static String[] m_Str27 = { "司机室占用" };
        public static String[] m_Str28 = { "1", "2", "1", "2", "1", "2", "1", "2", "1", "2", "1", "2" };
        public static String[] m_Str29 = { "TC1火灾重启", "M11火灾重启", "M21火灾重启", "M12火灾重启", "M22火灾重启", "TC2火灾重启", "整车火灾重启", "返回" };
        public static String[] m_Str30 = new String[6] { "TC1", "M11", "M21", "M22", "M12", "TC2" };

        #endregion
    }
}
