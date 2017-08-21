#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-8-21
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：公共组件-第一个组件-标题
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CommonUtil.Util;
using ES.Facility.Common.Control;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Service;
using Urban.HeFei.TMS.Common;
using Urban.HeFei.TMS.Help;
using Urban.HeFei.TMS.Resource;

namespace Urban.HeFei.TMS.CommonPart
{
    /// <summary>
    /// 功能描述：公共组件-第一个组件-标题
    /// 创建人：lih
    /// 创建时间：2015-8-4
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class VcC0Title : baseClass
    {
        #region 私有变量

        private int m_ReadTxtID = 0; //读取文本标志
        private List<Image> m_ResourceImage = new List<Image>(); //图片资源
        //private SortedDictionary<Int32, String> _stationList = new SortedDictionary<int, string>();//车站列表
        private List<Label> m_Labels = new List<Label>(); //组件上的文本框列表
        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f); //画线黑色画笔
        private List<Point> m_StartPoints = new List<Point>(); //起点坐标
        private List<Point> m_EndPoints = new List<Point>(); //终点坐标
        private SolidBrush m_BlackSolidBrush = new SolidBrush(Color.Black); //黑色

        private IStringFloatConverter m_StringFloatConverter = new StringFloatConverter();

        public IProjectIndexDescriptionConfig IndexDescription { private set; get; }

        private string[] m_ModelStrs; //模式字符串
        private Font m_ModelFont;
        private Rectangle m_ModelRectangle;
        //标题信息
        private string[] m_TitleInfoStrs;
        private Font m_CurrentTitleFont;
        private Rectangle m_CurrentTitleRect;

        private DateTime m_NowDate; //时间信息
        private Font[] m_TimeFonts;
        private Rectangle[] m_TimeRectangles;
        private string m_HourMintueStr;
        private string m_YearMonthDayStr;

        //站台相关信息
        private string[] m_StationInfoStrs; //站台信息
        private Font m_StationFont; //站台信息显示字体
        private Rectangle[] m_StationRectangle; //初始化需要绘制的rectangle,避免在绘图函数中反复生成销毁
        private string m_NextStationStr; //下一站名
        private string m_EndStationStr; //终点站名

        //车次号
        private Font[] m_TrainNumberFonts;
        private Rectangle[] m_TrainNumberRectangles;

        //速度网压
        private Font[] m_SpeedPressureFonts;
        private Rectangle[] m_SpeedPressureRects;

        private string m_TrainNoStr; //车次号
        private string m_TrainVoltage; //网压
        private string m_TrainSpeed; //速度
        private int m_NextStationIndex = 0, m_EndStationIndex = 0; //下一站终点站Index

        #endregion

        #region 框架初始化函数

        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共视图-标题栏";
        }

        /// <summary>
        /// 获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        //public override string GetTypeName()
        //{
        //    return "VC_C0_Title";
        //}

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            var indexKey = new CommunicationDataKey(AppConfig);
            IndexDescription =
                DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(indexKey);

            if (IndexDescription == null)
            {
                AppLog.Error("Can not GetProjectIndexDescriptionConfig where indexKey={0}", indexKey);
            }

            InitUserDefinedData();

            UIObj.ParaList.ForEach(a =>
            {
                using (var fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            return true;
        }

        /// <summary>
        /// 初始化自定义数据
        /// </summary>
        private void InitUserDefinedData()
        {
            var start1 = new Point(1, 88);
            var end1 = new Point(800, 88);
            var start2 = new Point(150, 1);
            var end2 = new Point(150, 88);
            var start3 = new Point(150, 44);
            var end3 = new Point(250, 44);
            var start4 = new Point(250, 1);
            var end4 = new Point(250, 88);
            var start5 = new Point(350, 1);
            var end5 = new Point(350, 88);
            var start6 = new Point(578, 1);
            var end6 = new Point(578, 88);
            var start7 = new Point(350, 44);
            var end7 = new Point(578, 44);
            var start8 = new Point(690, 1);
            var end8 = new Point(690, 88);
            m_StartPoints.Add(start1);
            m_EndPoints.Add(end1);
            m_StartPoints.Add(start2);
            m_EndPoints.Add(end2);
            m_StartPoints.Add(start3);
            m_EndPoints.Add(end3);
            m_StartPoints.Add(start4);
            m_EndPoints.Add(end4);
            m_StartPoints.Add(start5);
            m_EndPoints.Add(end5);
            m_StartPoints.Add(start6);
            m_EndPoints.Add(end6);
            m_StartPoints.Add(start7);
            m_EndPoints.Add(end7);
            m_StartPoints.Add(start8);
            m_EndPoints.Add(end8);

            m_ModelStrs = new string[] { "AM", "WM", "PM", "RM", "OFF", "?" };
            m_ModelFont = new Font("Arial", 22, FontStyle.Regular);
            m_ModelRectangle = new Rectangle(150, 1, 100, 44);

            m_TitleInfoStrs = new string[]
            {
                "主界面", "牵引", "制动", "辅助", "空调", "旁路", "乘客信息", "特殊信息", "事件警告", "故障", "历史",
                "维护", "列车信息", "软件版本", "接口检查", "能耗信息", "加速度测试", "制动自检", "密码设置", "亮度调节"
            };


            m_CurrentTitleFont = new Font("宋体", 18, FontStyle.Regular);
            m_CurrentTitleRect = new Rectangle(25, 1, 102, 85);


            m_StationInfoStrs = new string[] { "下一站:", "终点站:", "----" };
            m_StationFont = new Font("宋体", 16, FontStyle.Regular);
            m_StationRectangle = new Rectangle[4];
            m_StationRectangle[0] = new Rectangle(350, 1, 84, 44);
            m_StationRectangle[1] = new Rectangle(350, 44, 84, 44);
            m_StationRectangle[2] = new Rectangle(434, 1, 144, 44);
            m_StationRectangle[3] = new Rectangle(434, 44, 144, 44);

            m_TimeFonts = new Font[2];
            m_TimeFonts[0] = new Font("Arial", 22, FontStyle.Regular);
            m_TimeFonts[1] = new Font("Arial", 16, FontStyle.Regular);
            m_TimeRectangles = new Rectangle[2];
            m_TimeRectangles[0] = new Rectangle(680, 1, 120, 44);
            m_TimeRectangles[1] = new Rectangle(680, 44, 120, 44);

            m_TrainNumberFonts = new Font[2];
            m_TrainNumberFonts[0] = new Font("Arial", 12, FontStyle.Regular);
            m_TrainNumberFonts[1] = new Font("Arial", 22, FontStyle.Regular);
            m_TrainNumberRectangles = new Rectangle[2];
            m_TrainNumberRectangles[0] = new Rectangle(578, 1, 112, 44);
            m_TrainNumberRectangles[1] = new Rectangle(578, 44, 112, 44);

            m_SpeedPressureFonts = new Font[4];
            m_SpeedPressureFonts[0] = new Font("Arial", 22, FontStyle.Regular);
            m_SpeedPressureFonts[1] = new Font("Arial", 44, FontStyle.Regular);
            m_SpeedPressureFonts[2] = new Font("Arial", 16, FontStyle.Regular);
            m_SpeedPressureFonts[3] = new Font("Arial", 34, FontStyle.Regular);

            m_SpeedPressureRects = new Rectangle[3];
            m_SpeedPressureRects[0] = new Rectangle(150, 44, 100, 44);
            m_SpeedPressureRects[1] = new Rectangle(250, 1, 100, 64);
            m_SpeedPressureRects[2] = new Rectangle(250, 65, 100, 20);

            OnGetStationInfo();

        }

        public override bool mouseDown(Point point)
        {

            if (m_CurrentTitleRect.Contains(point))
            {
                HelpView.IsDisplay = true;
            }

            return true;
        }

        /// <summary>
        /// 获取当期视图值
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                var viewName = ((ViewState)nParaB).ToString();
            }
        }


        /// <summary>
        /// 获取文本信息：车站信息
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="cStr"></param>
        private void OnGetStationInfo()
        {
            var path = AppConfig.AppPaths.ConfigDirectory;
            var stationFileName = Path.Combine(path, "车站信息.xml");
            var sts = DataSerialization.DeserializeFromXmlFile<StationInfos>(stationFileName);
            if (sts == null)
            {
                return;
            }
            foreach (var temp in sts.StationInfoList)
            {
                CommonStatus.StationList.Add(temp.Index, temp.Name);
            }
        }

        #endregion

        #region 绘制界面

        /// <summary>
        /// 重载函数：绘制界面
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            paint_Frame(dcGs);
            paint_CurInterfaceInfo(dcGs);
            paint_Mode(dcGs);
            paint_StationInfo(dcGs);
            paint_TrainNumber(dcGs);
            paint_Speed_Pressure(dcGs);
            paint_TimeInfo(dcGs);
            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制站台信息
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_StationInfo(Graphics dcGs)
        {

            dcGs.DrawString(m_StationInfoStrs[0], m_StationFont, m_BlackSolidBrush, m_StationRectangle[0],
                FontInfo.SfCc);

            dcGs.DrawString(m_StationInfoStrs[1], m_StationFont, m_BlackSolidBrush, m_StationRectangle[1],
                FontInfo.SfCc);

            m_NextStationIndex = (int)FloatList[UIObj.InFloatList[2]];
            m_EndStationIndex = (int)FloatList[UIObj.InFloatList[3]];

            m_NextStationStr = GetStation(m_NextStationIndex) ?? m_StationInfoStrs[2];

            m_EndStationStr = GetStation(m_EndStationIndex) ?? m_StationInfoStrs[2];

            dcGs.DrawString(m_NextStationStr, m_StationFont, m_BlackSolidBrush, m_StationRectangle[2], FontInfo.SfCc);

            dcGs.DrawString(m_EndStationStr, m_StationFont, m_BlackSolidBrush, m_StationRectangle[3], FontInfo.SfCc);
        }

        /// <summary>
        /// 线框绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            for (var i = 0; i < m_StartPoints.Count && i < m_EndPoints.Count; i++)
            {
                dcGs.DrawLine(m_BlackLinePen, m_StartPoints[i], m_EndPoints[i]);
            }
        }


        /// <summary>
        /// 绘制当前界面标题信息
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_CurInterfaceInfo(Graphics dcGs)
        {
            //Brush[] brushs = new Brush[] { 
            //    new SolidBrush(Color.FromArgb(0, 255, 0)),
            //    new SolidBrush(Color.FromArgb(253,153,7)), 
            //    new SolidBrush(Color.FromArgb(255,0,5)),
            //    new SolidBrush(Color.FromArgb(178,178,178)) 
            //};

            //for (int i = 0; i < 4; i++)
            //{
            //    if (BoolList[UIObj.InBoolList[2] + i])
            //    {
            //        ((LabelStyle)this._labels[0].Style).BackgroundBrush = brushs[i];
            //        return;
            //    }
            //}
            //((LabelStyle)this._labels[0].Style).BackgroundBrush = null;


            //for (int i = 0; i < _titleInfoStrs.Length; i++)
            //{
            //    if(...)
            //}
            dcGs.DrawImage(m_ResourceImage[0], m_CurrentTitleRect);
            //dcGs.DrawString(CommonStatus.m_CurrentInterfaceName, m_CurrentTitleFont, m_BlackSolidBrush,
            //    m_CurrentTitleRect, FontInfo.m_SfCc);

        }

        /// <summary>
        /// 模式绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Mode(Graphics dcGs)
        {
            for (var i = 0; i < m_ModelStrs.Length; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    dcGs.DrawString(m_ModelStrs[i], m_ModelFont, m_BlackSolidBrush, m_ModelRectangle, FontInfo.SfCc);
                    return;
                }
            }
            dcGs.DrawString(m_ModelStrs[5], m_ModelFont, m_BlackSolidBrush, m_ModelRectangle, FontInfo.SfCc);
        }

        /// <summary>
        /// 时间信息绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_TimeInfo(Graphics dcGs)
        {
            m_NowDate = DateTime.Now;
            m_HourMintueStr = m_NowDate.ToString("HH:mm"); //时分
            dcGs.DrawString(m_HourMintueStr, m_TimeFonts[0], m_BlackSolidBrush, m_TimeRectangles[0], FontInfo.SfCb);

            m_YearMonthDayStr = m_NowDate.ToString("yy-MM-dd"); //年月日
            dcGs.DrawString(m_YearMonthDayStr, m_TimeFonts[1], m_BlackSolidBrush, m_TimeRectangles[1], FontInfo.SfCt);
        }

        /// <summary>
        /// 车次号绘制
        /// </summary>
        /// <param name="g"></param>
        public void paint_TrainNumber(Graphics g)
        {
            g.DrawString("车次号", m_TrainNumberFonts[0], m_BlackSolidBrush, m_TrainNumberRectangles[0], FontInfo.SfCb);

            m_TrainNoStr =
                new string(m_StringFloatConverter.ConvertFloatArray(new[]
                {
                    FloatList[IndexDescription.InFloatDescriptionDictionary[InFloatKeys.主界面_车次号0]],
                    FloatList[IndexDescription.InFloatDescriptionDictionary[InFloatKeys.主界面_车次号1]]
                }));

            g.DrawString(m_TrainNoStr, m_TrainNumberFonts[1], m_BlackSolidBrush, m_TrainNumberRectangles[1],
                FontInfo.SfCt);
        }


        /// <summary>
        /// 绘制：速度与网压
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Speed_Pressure(Graphics dcGs)
        {
            //网压
            m_TrainVoltage = FloatList[UIObj.InFloatList[0]].ToString("0V");
            dcGs.DrawString(m_TrainVoltage, m_SpeedPressureFonts[0], m_BlackSolidBrush, m_SpeedPressureRects[0],
                FontInfo.SfCc);


            m_TrainSpeed = FloatList[UIObj.InFloatList[1]].ToString("0");
            //速度
            if (m_TrainSpeed.Length >= 3)
            {
                dcGs.DrawString(m_TrainSpeed, m_SpeedPressureFonts[3], m_BlackSolidBrush, m_SpeedPressureRects[1],
                    FontInfo.SfCc);
            }
            else
            {
                dcGs.DrawString(m_TrainSpeed, m_SpeedPressureFonts[1], m_BlackSolidBrush, m_SpeedPressureRects[1],
                    FontInfo.SfCc);
            }


            //单位
            dcGs.DrawString("km/h", m_SpeedPressureFonts[2], m_BlackSolidBrush, m_SpeedPressureRects[2], FontInfo.SfCc);
        }

        /// <summary>
        /// 获取站台信息
        /// </summary>
        /// <param name="stationNum"></param>
        /// <returns></returns>
        private string GetStation(int stationNum)
        {
            var stationName = string.Empty;
            CommonStatus.StationList.TryGetValue(stationNum, out stationName);
            return stationName;
        }

        #endregion
    }
}
