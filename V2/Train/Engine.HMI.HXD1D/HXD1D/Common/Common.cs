using System;
using System.Drawing;

namespace HXD1D.Common
{
    class FormatStyle
    {
        private const float _scale = 1.0f;

        /// <summary>
        /// 比例尺寸
        /// </summary>
        public static float Scale { get { return _scale; } }

        private const int _screenMoveX = 0;

        /// <summary>
        /// 屏横向移动像素
        /// </summary>
        public static int ScreenMoveX { get { return _screenMoveX; } }

        private static int _screenMoveY = 0;
        /// <summary>
        /// 屏纵向移动像素
        /// </summary>
        public static int ScreenMoveY { get { return _screenMoveY; } }

        #region :::::::::::::::::::::::::::::: 线条 :::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// 宽1白线
        /// </summary>
        public static Pen WhitePen = new Pen(Color.FromArgb(255, 255, 255), 1 * _scale);
        /// <summary>
        /// 宽2白线
        /// </summary>
        public static Pen WhitePen1 = new Pen(Color.FromArgb(255, 255, 255), 2 * _scale);
        /// <summary>
        /// 宽3白线
        /// </summary>
        public static Pen WhitePen2 = new Pen(Color.FromArgb(255, 255, 255), 3 * _scale);


        /// <summary>
        /// 宽1黑线
        /// </summary>
        public static Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0), 1 * _scale);
        public static Pen BlackPen1 = new Pen(Color.FromArgb(0, 0, 0), 2 * _scale);
        public static Pen BlackPen2 = new Pen(Color.FromArgb(0, 0, 0), 3 * _scale);

        /// <summary>
        /// 宽3中灰
        /// </summary>
        public static Pen MediumGreyPen = new Pen(Color.FromArgb(150, 150, 150), 3 * _scale);

        /// <summary>
        /// 宽1深灰
        /// </summary>
        public static Pen DarkGreyPen = new Pen(Color.FromArgb(85, 85, 85));
       /// <summary>
       /// 红1
       /// </summary>
        public static Pen RedPen = new Pen(Color.FromArgb(191, 0, 2));
        /// <summary>
        /// 红3
        /// </summary>
        public static Pen RedPen3 = new Pen(Color.FromArgb(191, 0, 2),3 * _scale);
        /// <summary>
        /// 蓝1
        /// </summary>
        public static Pen BluePen = new Pen(Color.FromArgb(0, 0, 255));
        /// <summary>
        /// 黄1
        /// </summary>
        public static Pen YelloPen = new Pen(Color.FromArgb(255, 255, 0));


        #endregion

        #region :::::::::::::::::::::::::::::: 颜色 :::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// 白色
        /// </summary>
        public static SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));

        /// <summary>
        /// 黑色
        /// </summary>
        public static SolidBrush BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));

        /// <summary>
        /// 淡绿
        /// </summary>
        public static SolidBrush LightGreenBrush = new SolidBrush(Color.FromArgb(45, 200, 51));

        /// <summary>
        /// 深灰
        /// </summary>
        public static SolidBrush DarkGreyBrush = new SolidBrush(Color.FromArgb(97, 112, 131));

        /// <summary>
        /// 中灰
        /// </summary>
        public static SolidBrush MediumGreySolidBrush = new SolidBrush(Color.FromArgb(150, 150, 150));

        /// <summary>
        /// 50%灰
        /// </summary>
        public static SolidBrush HalfPGreySolidBrush = new SolidBrush(Color.FromArgb(128, 128, 128));

        /// <summary>
        /// 黄色
        /// </summary>
        public static SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 0));

        /// <summary>
        /// 桔黄色
        /// </summary>
        public static SolidBrush OrangeBrush = new SolidBrush(Color.Orange);

        /// <summary>
        /// 红色
        /// </summary>
        public static SolidBrush RedBrush = new SolidBrush(Color.FromArgb(191, 0, 2));

        /// <summary>
        /// 蓝色
        /// </summary>
        public static SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(0, 0, 255));
        /// <summary>
        /// 浅灰
        /// </summary>
        public static SolidBrush GreyBrush = new SolidBrush(Color.FromArgb(85, 85, 85));
        /// <summary>
        /// 绿色
        /// </summary>
        public static SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
        /// <summary>
        /// 白灰
        /// </summary>
        public static SolidBrush BgrBursh=new SolidBrush(Color.FromArgb(170,170,170));
        /// <summary>
        ///淡蓝色
        /// </summary>
        public static SolidBrush ThinBlue = new SolidBrush(Color.FromArgb(0, 109, 189));
        /// <summary>
        /// 灰
        /// </summary>
        public static SolidBrush GreyBrushsBrush = new SolidBrush(Color.FromArgb(210, 210, 210));
        /// <summary>
        /// 浅灰
        /// </summary>
        public static SolidBrush GreyBrus= new SolidBrush(Color.FromArgb(143, 148, 144));
        #endregion

        #region :::::::::::::::::::::::::::::: 字体 :::::::::::::::::::::::::::::::::::::::
        public const String strFont = "宋体";
        public static Font Font8 = new Font(strFont, 8f * _scale);
        public static Font Font10 = new Font(strFont, 10f * _scale);
        public static Font Font12 = new Font(strFont, 12f * _scale);
        public static Font Font14 = new Font(strFont, 14f * _scale);
        public static Font Font16 = new Font(strFont, 16f * _scale);
        public static Font Font18 = new Font(strFont, 18f * _scale);
        public static Font Font20 = new Font(strFont, 20f * _scale);
        public static Font Font22 = new Font(strFont, 22f * _scale);
        public static Font Font24 = new Font(strFont, 24f * _scale);
        public static Font Font26 = new Font(strFont, 26f * _scale);
        public static Font Font32 = new Font(strFont, 32f * _scale);
        public static Font Font34 = new Font(strFont, 34f * _scale);
        public static Font Font38 = new Font(strFont, 38f * _scale);
        public static Font Font64 = new Font(strFont, 64f * _scale);

        public static Font Font8B = new Font(strFont, 8f * _scale, _scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font Font10B = new Font(strFont, 10f * _scale, _scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font Font12B = new Font(strFont, 12f * _scale, _scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font Font14B = new Font(strFont, 14f * _scale, _scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font Font16B = new Font(strFont, 16f * _scale, _scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font Font18B = new Font(strFont, 18f * _scale, _scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font Font20B = new Font(strFont, 20f * _scale, _scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font Font22B = new Font(strFont, 22f * _scale, _scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font Font24B = new Font(strFont, 24f * _scale, _scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font Font26B = new Font(strFont, 26f * _scale, _scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font Font32B = new Font(strFont, 32f * _scale, _scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font Font34B = new Font(strFont, 34f * _scale, _scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font Font38B = new Font(strFont, 38f * _scale, _scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        public static Font Font64B = new Font(strFont, 64f * _scale, _scale >= 1 ? FontStyle.Bold : FontStyle.Regular);
        #endregion

        #region :::::::::::::::::::::::::::::: 文字 :::::::::::::::::::::::::::::::::::::::

        public static String[] str1 ={ "主要\n数据","控制\n设置","机车\n设置","运行\n条件","维护\n测试","检修\n模式"," 主\n界面" };
        public static String[] str2 = { "网压", "原边电流", "控制电压", "本机" };
        public static string[] str3 = { "级位", "设定速度", "级", "km/h", "调整设定速度", "列供1电压", "列供2电压",
                                          "V", "V",  "A", "A", "电流", "电流", "故障信息", "提示信息" ,"+1km", "-1km"};
        public static String[] str4 = { "牵引\n数据", "机车\n配置", "控制\n系统", "辅助\n系统", "列车\n供电", "状态\n信息","温度\n检测", " 主\n界面", };
        public static string[] str5 = { "受电弓1", "受电弓2", "车顶隔离开关1", "车顶隔离开关2", "主断", "主断分断次数", "VCM1", "VCM2", "机车模式", "机车节点", " 司机室" , "电空电流", "MVB A线状态", "MVB B线状态 ", "WTB A线状态", "WTB B线状态 "};
        public static string[] str6 = { "正常", "故障", "断开" };
        public static string[] str7 = { "30", "25", "20", "15", "10", "5", "0", "600", "400", "200", "0", "120", "100", "80", "60", "40", "20", "0", "100",
                                          "80", "60", "40", "20", "0" ,"[KV]", "[A]", "[V]", "[%]"};
        public static string[] str8 = { "V", "HZ", "A" };
        public static string[] str9 = { "DXM11", "DIM21", "AXM12","DIM22", "DXM13", "AXM23" ,"ETS", "BCU", "GVM", "VCM2",  "IDU1", "TCU1", "ERM", "VCM1", "TCU2", "IDU2", "DXM31", "DXM32", "DXM33",
                                          "DXM34", "DXM35", "DXM36", "DXM37", "DXM38" ,"DXM39"};
        public static string[] str10 = { "CCU", "line_A", "line_B", "line_A", "cab1", "cab1", "line_B", "MIO"};
        public static string[] str11 = { "类别", "车号", "代码", "故障内容", "故障发生时间" };
        public static string[] str12 = { "维护\n检修", "信息", "", "", "", "", "", "上一\n条", "下一\n条"};
        public static string[] str13 = { "供电允许", "接地隔离1", "供电1申请", "列供钥匙1", "集控隔离","接地隔离2","供电2申请","列供钥匙2",
                                           "供电电压","供电半电压","供电电流","交流输入电流","交流输入电压" ,"供电电压","供电半电压","供电电流","交流输入电流","交流输入电压" };
        public static string[] str14 = { "V", "V", "A", "A", "V", "V", "V", "A", "A", "V" };
        public static string[] str15 = { "KM13", "交流接触器", "11FU", "MQS1", "隔离闸刀11", "MQS2", "隔离闸刀12", "MQS3", "隔离闸刀21", 
                                           "MQS4", "隔离闸刀22", "KM14", "交流接触器", "12FU", "快熔", "快熔"};
        public static string[] str16 =
        {
            "电机1温度", "电机2温度", "电机3温度", "主变压器1冷却油温度", "油流1状态", "主变流器1冷却水温度", "主变流器1柜体温度", "主变流器1进水压力", "主变流器1出水压力", "主变流器1水压力差" ,
        
       "主变流器1中间直流电压","电机4温度","电机5温度","电机6温度", "主变压器2冷却油温度", "油流2状态", "主变流器2冷却水温度", "主变流器2柜体温度", "主变流器2进水压力",

        "主变流器2出水压力", "主变流器2水压力差","主变流器2中间直流电压" };
        public  static  string[] str17={"正常","异常"};
        public static string[] str18 = {"载重\n设置", "联挂\n设置", "运行\n模式", "调度\n设置"};
        public static string[] str19 = { "选择完成后，请按确认键结束","列车重量","(<=25000)t","使用值" ,"列车类型\n1=客车","使用值","列车铀重\n1=21t","使用值","t","t"};
        public static string[] str20 = { "选择完成后，请按确认键结束","机车联挂速度范围\n(1-3Km/h)","使用值","设置值","Km/h","Km/h"};
        public static string[] str21 = { "选择完成后，请按确认键结束", "模式选择", "正常模式", "应急模式" };
        public static string[] str22 = { "默认值","设定值","(根据实际情况设定范围：20%-50%)"};
        public static string[] str23 = { "选择完成后，请按确认键结束", "自动过分相设置", "投入", "切除","过分相G1-G2点距离","当前使用值"};
        public static string[] str24 = { "升弓\n条件", "主断\n条件", "牵引\n条件"};
        public static string[] str25 = { "辅机\n测试", "库内\n动车", "淋雨\n模式", "库内分\n相测试", "顶轮\n模式" };
        public static string[] str26 = { "选择完成后，请按确认键结束", "模式选择", "取消", "激活" };
        public static string[] str27 = { "返回", "后退\n[<]", "前进\n[>]", "确认" };
        public static string[] str28 = { "版本信息", "历史故障", "检修设置", "I/O\n信息", "主\n界面" };
        public static string[] str29 = { "机车\n编号", "操作\n端", "轮径\n设置", "轮缘\n润滑", "时间\n设置","密码\n设置" ,"检修\n模式"};
        public static string[] str30 = { "进入维护界面必需通过身份验证","请输入密码:"};
        public static string[] str31 = { "无操作段", "司机室1被占用", "司机室2被占用" };
        public static string[] str32 = { "主", "从", "断开" };
        public static string[] str33 = { "正常操作", "库内动车操作", "辅机测试操作", "定速模式操作" };
     
     
        #endregion 
        #region 字体对齐方式
        public static StringFormat drawFormat = new StringFormat();
        public static StringFormat RightFormat = new StringFormat(){Alignment =  StringAlignment.Far,LineAlignment = StringAlignment.Far};

        public static StringFormat LeftFormat = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
        public static StringFormat MFormat = new StringFormat(){Alignment =  StringAlignment.Center,LineAlignment = StringAlignment.Center};
        
     
        #endregion

    }   
}

    

