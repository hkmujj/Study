using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.Resources.ImageKeys;
using Motor.ATP._300T.共用;

namespace Motor.ATP._300T.主屏
{
    public class AreaEContent
    {
        private readonly ATPMainScreen m_ATPMainScreen;

        public AreaEContent(ATPMainScreen atpMainScreen)
        {
            m_ATPMainScreen = atpMainScreen;
        }


        private readonly Image[] m_GsmImages =
        {
            ComImages.GSM_灰,
            ComImages.GSM_2,
            ComImages.GSM_3,

        };

        private readonly Image[] m_RbcImages =
        {
            ComImages.未与RBC相连,
            ComImages.正在与RBC相连,
            ComImages.已经与RBC相连,
        };

        public void DrawAreaEDateTime(Graphics g)
        {
            /*
                         * 在此区域分三行显示，
                         * 第一行显示文本“时间日期”，
                         * 第二行显示时间，显示格式是“小时:分:秒”，如11:20:23；
                         * 第三行显示日期，显示格式是“年-月-日”，如07-11-13。
                         * 字体为幼圆大小为14磅（推荐），颜色为白色。
                         */

            g.DrawString(
                "时间日期\n" + m_ATPMainScreen.m_CurrentTime.ToLongTimeString() + "\n" +
                m_ATPMainScreen.m_CurrentTime.ToString("yy-MM-dd"),
                FontsItems.Font12YouR, SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[58],
                FontsItems.TheAlignment(FontRelated.居中));
        }

        /// <summary>
        /// E区，监控信息
        /// </summary>
        public void DrawAreaE(Graphics g)
        {

            #region ::::::::::: E1区，备用系统状态

            /*
             * 当C3控车时，显示C2系统的状态；
             * 当C2主控时，显示C3系统的状态。
             * 显示的状态包括正常和故障两种状态。
             */

            #endregion

            #region ::::::::::: E3区，监督司机动作信息（预留）

            /*
             * 当系统监督司机的活动，对司机发出警告或施加制动时显示图标符号，否则的话没有任何显示。
             * 
             * 当对司机发出警告时，显示闪烁图标（并触发声音），闪烁频率为1 Hz。
             * 司机执行一项操作解除了监督告警状态后，此符号消失。
             * 如果司机没有做出回应，监督功能将施加制动，并显示红色符号。
             * 制动的同时，在C9区显示一个制动图标。
             * 在制动期间，当司机执行一项操作，满足了司机作业监督功能，此告警图标将消除。
             */

            if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inb司机安全设备告警)] &&
                m_ATPMainScreen.CurrentTime.Second%2 == 0)
            {
                g.DrawImage(ComImages.司机安全设备告警, m_ATPMainScreen.m_RectsList[12]);
            }
            else if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inb司机安全设备制动)])
            {
                g.DrawImage(ComImages.司机安全设备制动, m_ATPMainScreen.m_RectsList[12]);
            }

            #endregion

            #region ::::::::::: E4区，紧急信号

            /*
             * 当车载设备收到紧急信号后，显示带有闪烁边框的紧急信号图标，闪烁频率为1 Hz，并伴有声音S7。
             * 当超出紧急信号条件时，该图标不再显示。
             */
            if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inb紧急消息)])
            {
                g.DrawImage(ComImages.紧急消息, m_ATPMainScreen.m_RectsList[13]);
            }
            else if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inb没有无线通信)])
            {
                g.DrawImage(ComImages.没有无线通信, m_ATPMainScreen.m_RectsList[13]);
            }

            #endregion

            #region ::::::::::: E5区，机控/人控表示

            /*
             * 在机控优先的情况下，显示“机控”，在人控优先的情况下，显示 “人控”。
             * 
             * 图标大小为54×30，字体为幼圆大小为14磅（推荐），颜色为白色。
             */
            if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inb机控)])
            {
                g.DrawString("机控", FontsItems.Font14YouR,
                    SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[14],
                    FontsItems.TheAlignment(FontRelated.居中));
            }
            else if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inb人控)])
            {
                g.DrawString("人控", FontsItems.Font14YouR,
                    SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[14],
                    FontsItems.TheAlignment(FontRelated.居中));
            }

            #endregion

            #region ::::::::::: E6- E10区，车站名称

            DrawStationName(g);

            #endregion


            #region E13区 车门侧信息

            var openLocation = (OpenDoorLocation) m_ATPMainScreen.GetInFloatValue(InFloatKeys.开门侧);
            switch (openLocation)
            {
                case OpenDoorLocation.Left:
                    g.DrawImage(OpenDoorImages.Open_Left, m_ATPMainScreen.m_RectsList[50]);
                    break;
                case OpenDoorLocation.Right:
                    g.DrawImage(OpenDoorImages.Open_Right, m_ATPMainScreen.m_RectsList[50]);
                    break;
            }

            #endregion


            #region ::::::::::: E16a区，车次号

            /*
             * 上面显示文本“车次号”，下面显示实际的车次号。
             * 车次号的编码方式执行《关于公布铁路列车车次编码方案的通知》（铁运[2005] 72号）。
             * 字体为幼圆大小为14磅（推荐），颜色为白色。
             * 车次号最多显示8位字符或者数字。
             */
            //if (FloatList[UIObj.OutFloatList[2]] != 0 || FloatList[UIObj.OutFloatList[3]] != 0)
            //{
            g.DrawString("车次号\r\n\r\n\r\n", FontsItems.Font12DefB,
                SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[53], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString("\r\n\r\n" + m_ATPMainScreen.OpenFunBtnCtcs300T.TrainId, FontsItems.Font12DefB,
                SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[53], FontsItems.TheAlignment(FontRelated.居中));
            //}

            #endregion

            #region ::::::::::: E16b区，GSM-R网络状态和与RBC连接状态

            /*
             * E16b1区，显示GSM-R的网络状态。
             * 当有GSM-R网络时，显示网络图标，没有GSM-R网络时，则不显示网络图标。
             * 图标大小为32×32，在此区居中显示。
             */



            for (var i = 0; i < 3; i++)
            {
                if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GSMRIndexs[i]])
                {
                    g.DrawImage(m_GsmImages[i], m_ATPMainScreen.m_RectsList[54]);
                }
            }


            /* E16b2区，显示RBC的连接状态。
            * 表示与RBC未连接、正在连接或已经连接的状态。
            * 图标大小为32×32，在此区居中显示。
            */
            for (var i = 0; i < 3; i++)
            {
                if (m_ATPMainScreen.BoolList[m_ATPMainScreen.RbcIndexs[i]])
                {
                    g.DrawImage(m_RbcImages[i], m_ATPMainScreen.m_RectsList[55]);
                }
            }

            #endregion

            #region ::::::::::: E16c/E16d区，缩放键图标

            /*
             * E16c区显示放大键图标，E16d区显示缩小键图标。
             * 图标大小为57×40。
             */
            m_ATPMainScreen.m_TheGlasses[0] = ComImages.放大暗; //放大
            m_ATPMainScreen.m_TheGlasses[1] = ComImages.缩小暗; //缩小
            if (m_ATPMainScreen.m_ShowPlanningArea) //完全和引导模式
            {
                switch (m_ATPMainScreen.m_RangeScaleMode)
                {
                    case 0:
                        m_ATPMainScreen.m_TheGlasses[0] = ComImages.放大亮;
                        m_ATPMainScreen.m_TheGlasses[1] = ComImages.缩小暗;
                        break;
                    case 1:
                        m_ATPMainScreen.m_TheGlasses[0] = ComImages.放大亮;
                        m_ATPMainScreen.m_TheGlasses[1] = ComImages.缩小亮;
                        break;
                    case 2:
                        m_ATPMainScreen.m_TheGlasses[0] = ComImages.放大暗;
                        m_ATPMainScreen.m_TheGlasses[1] = ComImages.缩小亮;
                        break;
                }
            }

            for (var i = 0; i < 2; i++)
            {
                g.DrawImage(m_ATPMainScreen.m_TheGlasses[i], m_ATPMainScreen.m_RectsList[56 + i]);
            }

            #endregion

            #region ::::::::::: E17 区，日期和时间

            DrawAreaEDateTime(g);

            #endregion

            #region ::::::::::: E19-E22区，文本信息

            /*
             * 此区域范围内最多能显示4行文本，可滚动显示信息，
             * 所显示信息的最前端有时间标记（小时:分钟:秒），应避免显示信息的长度超过1行的情况。
             * 文本信息按时间先后顺序分类显示，最新信息显示在E19处，白色显示，伴有声音提示。
             * 有新文本出现后，原来的文本变为灰色。
             * 
             * 对于需要司机确认的信息，显示带有闪烁的黄色边框，边框的宽度为1个像素，闪烁频率为1 Hz。
             * 司机进行确认后，文本变为灰色，黄色边框消失。
             * 根据主机的要求，选择保留或删除文本。
             * 
             * DMI保留最近至少50条文本信息，文本信息字体为幼圆大小为14磅（推荐），时间字体为Arial大小为10磅（推荐）。
             */

            #endregion

            #region ::::::::::: E23区，公里标

            /*
             * 在E23中心位置显示公里标。
             * 显示格式为“Kx + y”，其中字母“K”后显示公里数，“+”后显示米数。
             * 字体为Arail大小为16磅（推荐），颜色为白色。x单位为公里，y单位为米。
             */
            if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inb公里标显示)])
            {
                var meter = (int) m_ATPMainScreen.GetInFloatValue(InFloatKeys.公里标);
                g.DrawString(string.Format("K{0} + {1}", meter/1000, meter%1000),
                    FontsItems.Font14YouB,
                    SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[41],
                    FontsItems.TheAlignment(FontRelated.居中));
            }

            #endregion

            #region ::::::::::: E24-E25区，文本信息滚动箭头

            /*
             * E24区显示向上滚动箭头，E25区显示向下滚动箭头。
             * 当文本信息小于等于4条时，滚动箭头均显示灰色，表示不能滚动显示；
             * 当文本信息大于4条时，根据显示情况，能够滚动方向的箭头显示白色，不能滚动方向的箭头显示灰色。
             */

            #endregion

        }

        private void DrawStationName(Graphics g)
        {
             /*
             * 车站名称以汉字方式显示，最多显示6个汉字，汉字采用GB18030编码方式。
             * 字体为幼圆大小为16磅（推荐），颜色为白色。
             */
            var stationId =
                BitConverter.ToInt32(
                    BitConverter.GetBytes(m_ATPMainScreen.GetInFloatValue(InFloatKeys.车站名)), 0);

            var stationName = GetStationName(stationId);
            if (!string.IsNullOrWhiteSpace(stationName))
            {
                g.DrawString(stationName,
                    FontsItems.Font16YouB, SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[47],
                    FontsItems.TheAlignment(FontRelated.居中));
            }
        }

        private string GetStationName(int index)
        {
            if (m_ATPMainScreen.StationNameProviderService.StationDictionary != null && m_ATPMainScreen.StationNameProviderService.StationDictionary.ContainsKey(index))
            {
                return m_ATPMainScreen.StationNameProviderService.StationDictionary[index].Name;
            }

            if (m_ATPMainScreen.m_StationsDict.ContainsKey(index))
            {
                return m_ATPMainScreen.m_StationsDict[index];
            }

            return string.Empty;
        }
    }
}