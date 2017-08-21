using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using ES.Facility.PublicModule.Memo;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Wuxi.TMS.维护;

namespace Urban.Wuxi.TMS.运行
{
    /// <summary>
    /// 站点设置
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    internal class StationSet : TMSBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::

        public override string GetInfo()
        {
            return "站点设置";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            LoadStationInfo();

            LoadRoadInfo();

            InitData();
            return true;
        }

        private void LoadRoadInfo()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "线路信息.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                string[] split = cStr.Split('\t');
                if (split.Length == 2)
                {
                    string tmpStations = split[1];
                    string tmpStr = string.Empty;
                    string outTmp = string.Empty;
                    if (StringHelper.findStringByKey(tmpStations, "[", "]", ref tmpStr))
                    {
                        outTmp = tmpStr.Trim();
                    }

                    if (outTmp != string.Empty)
                    {
                        string lineID = split[0];
                        string[] stations = outTmp.Split('-');
                        if (lineID.Trim() == "1号线")
                        {
                            foreach (string st in stations)
                            {
                                //1号线所有站点
                                m_LineID1.Add(st);
                            }
                        }
                        else if (lineID.Trim() == "2号线")
                        {
                            foreach (string st in stations)
                            {
                                //2号线所有站点
                                m_LineID2.Add(st);
                            }
                        }
                    }
                }
            }
        }

        private void LoadStationInfo()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "车站信息.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                string[] split = cStr.Split('\t');
                int stationID;
                if (split.Length == 2 && int.TryParse(split[0], out stationID))
                {
                    m_StationNameID.Add(split[1], stationID);
                }
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                if (nParaB != Convert.ToInt32(nParaC))
                {
                    NeedResetStartAndEndStation(1);
                    ResetInfo(0);
                    ResetInfo(1);
                    ResetInfo(2);
                }
            }
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::

        public override void paint(Graphics dcGs)
        {
            //GetValue();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public override bool mouseDown(Point nPoint)
        {
            int index = 0;
            if (m_IsInLinesSelectView)
            {
                #region :::::::: 线路选择

                for (; index < 13; ++index)
                {
                    if (m_KeyBoardBtns[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                m_KeyIsDown[index] = true;

                #endregion
            }
            else
            {
                #region :::::::: 右边栏按钮

                index = 0;
                for (; index < 8; ++index)
                {
                    if (m_Regions[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                switch (index)
                {
                    case 0: //全自动广播
                        //_buttonIsDown[0] = true;
                        //_buttonIsDown[1] = false;
                        break;
                    case 1: //半自动广播
                        //_buttonIsDown[0] = false;
                        //_buttonIsDown[1] = true;
                        break;
                    case 2: //手动模式
                        m_ButtonIsDown[2] = true;
                        break;
                    case 3: //空
                        m_ButtonIsDown[3] = true;
                        break;
                    case 4: //1号线
                        m_ButtonIsDown[4] = true;
                        break;
                    case 5: //2号线
                        m_ButtonIsDown[5] = true;
                        break;
                    case 6: //路径设置
                        m_ButtonIsDown[6] = true;
                        break;
                    case 7: //返回
                        m_ButtonIsDown[7] = true;
                        break;
                }

                #endregion

                #region :::::::: 光标位置的始发站和终点站以及设定按键

                if (m_MenuID == StationSettingMenuID.Line1 ||
                    m_MenuID == StationSettingMenuID.Line2)
                {
                    index = 8;
                    for (; index < 10; ++index)
                    {
                        if (m_Regions[index].IsVisible(nPoint))
                        {
                            break;
                        }
                    }
                    switch (index)
                    {
                        case 8:
                            m_ButtonIsDown[8] = true;
                            m_ButtonIsDown[9] = false;
                            m_CursorID = 0;
                            break;
                        case 9: //只有始发站不为空才能点终点站
                            if (m_SetStartStationName != string.Empty)
                            {
                                m_ButtonIsDown[8] = false;
                                m_ButtonIsDown[9] = true;
                                m_CursorID = 1;
                            }
                            break;
                    }
                    if (m_Regions[60].IsVisible(nPoint))
                    {
                        if (m_SetisBtnCanRun) m_ButtonIsDown[60] = true;
                    }
                }

                #endregion

                #region  :::::::: 相应线路选择始发站和终点站

                index = 12;
                for (; index < BtnNumbers(m_MenuID) + 12; ++index)
                {
                    if (m_Regions[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                if (index >= 12 && index < BtnNumbers(m_MenuID) + 12)
                {
                    if (m_CursorID == 0)
                    {
                        StationBtnState(index, true);
                        StationName(index - 12, true);
                        m_CursorID = 1; //当选择完成起点站框后自动跳转到终点站框
                    }
                    else if (m_CursorID == 1)
                    {
                        if (m_SetStartStationName != string.Empty)
                        {
                            StationBtnState(index, false);
                            StationName(index - 12, false);
                            m_CursorID = 2;
                            m_SetisBtnCanRun = true;
                        }
                    }
                    else if (m_CursorID == 2)
                    {
                    }
                    else
                    {
                        //当没有选择线路的情况下，按下站点名都会弹起
                        m_ButtonIsDown[index] = true;
                    }
                }

                #endregion
            }

            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            if (m_IsInLinesSelectView)
            {
                #region :::::::: 线路选择

                for (; index < 13; ++index)
                {
                    if (m_KeyBoardBtns[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                m_KeyIsDown[index] = false;
                if (index >= 0 && index < 13)
                {
                    LinesCount(index);
                }

                #endregion
            }
            else
            {
                #region :::::::: 右边栏按钮

                for (; index < 8; ++index)
                {
                    if (m_Regions[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                switch (index)
                {
                    case 2: //手动模式
                        m_ButtonIsDown[2] = false;
                        break;
                    case 3: //空
                        m_ButtonIsDown[3] = false;
                        break;
                    case 4: //1号线
                        m_ButtonIsDown[4] = false;
                        m_MenuID = StationSettingMenuID.Line1;
                        m_CurrentLineID = 1;
                        if (NeedResetStartAndEndStation(1))
                        {
                            ResetInfo(0);
                            ResetInfo(1);
                            ResetInfo(2);
                        }
                        break;
                    case 5: //2号线
                        m_ButtonIsDown[5] = false;
                        m_MenuID = StationSettingMenuID.Line2;
                        m_CurrentLineID = 2;
                        if (NeedResetStartAndEndStation(2))
                        {
                            ResetInfo(0);
                            ResetInfo(1);
                            ResetInfo(2);
                        }
                        break;
                    case 6: //Route选择
                        m_ButtonIsDown[6] = false;
                        m_IsInLinesSelectView = true;
                        ResetInfo(0);
                        ResetInfo(1);
                        ResetInfo(2);
                        break;
                    case 7: //返回
                        m_ButtonIsDown[7] = false;
                        OnPost(CmdType.ChangePage, 11, 0, 0);
                        break;
                }

                #endregion

                #region :::::::: 设定按键

                if (m_MenuID == StationSettingMenuID.Line1 ||
                    m_MenuID == StationSettingMenuID.Line2)
                {
                    if (m_Regions[60].IsVisible(nPoint))
                    {
                        if (m_SetisBtnCanRun)
                        {
                            m_ButtonIsDown[60] = false;
                            OnPost(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, m_SetStartStationId);
                            OnPost(CmdType.SetFloatValue, UIObj.OutFloatList[1], 0, m_SetEndStationID);
                            m_SetisBtnCanRun = false;
                        }
                    }
                }

                #endregion

                #region  :::::::: 相应线路选择始发站和终点站

                index = 12;
                for (; index < BtnNumbers(m_MenuID) + 12; ++index)
                {
                    if (m_Regions[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                if (index >= 12 && index < BtnNumbers(m_MenuID) + 12)
                {
                    if (m_CursorID == 0)
                    {
                    }
                    else if (m_CursorID == 1)
                    {
                    }
                    else if (m_CursorID == 2)
                    {
                    }
                    else
                    {
                        m_ButtonIsDown[index] = false;
                    }
                }

                #endregion
            }

            return base.mouseUp(nPoint);
        }

        #endregion

        #region :::::::::::::::::::::::::::: draw funes ::::::::::::::::::::::::::

        /// <summary>
        /// 线路选择
        /// </summary>
        /// <param name="e">画图对象</param>
        private void DrawTrainLinesSelect(Graphics e)
        {
            m_KeyBoard.DrawKeyboard(e, ref m_KeyIsDown, m_DrawFormat); //????
            e.DrawString("原路径", FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush,
                m_Rects[68], m_RightFormat);
            e.DrawString("新路径", FormatStyle.m_Font12B, FormatStyle.m_WhiteBrush,
                m_Rects[69], m_RightFormat);
            for (int i = 0; i < 2; i++)
            {
                e.FillRectangle(FormatStyle.m_WhiteBrush, m_Rects[70 + i]);
            }
            e.DrawString(m_CurrentLineID.ToString(), FormatStyle.m_Font18B,
                FormatStyle.m_BlackBrush, m_Rects[70], m_LeftFormat);
            e.DrawString(m_SetLineID, FormatStyle.m_Font18B,
                FormatStyle.m_BlackBrush, m_Rects[71], m_LeftFormat);
        }

        /// <summary>
        /// 框架
        /// </summary>
        /// <param name="e"></param>
        private void DrawFrame(Graphics e)
        {
            e.DrawRectangle(FormatStyle.m_WhitePen, m_Rects[0].X, m_Rects[0].Y, m_Rects[0].Width, m_Rects[0].Height);
            e.DrawLine(FormatStyle.m_MediumGreyPen, m_PDrawPoint[0], m_PDrawPoint[1]);
        }

        /// <summary>
        /// 始发站、终点站、选择线路和运行线路
        /// </summary>
        private void DrawStationNamesAndLineID(Graphics e)
        {
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(FormatStyle.m_Str15[i + 8], FormatStyle.m_Font12B,
                    FormatStyle.m_WhiteBrush, m_Rects[59 + i], m_RightFormat);
                e.DrawImage(m_Images[3], m_Rects[63 + i]);
            }

            e.DrawString(m_SetStartStationName, FormatStyle.m_Font12,
                FormatStyle.m_WhiteBrush, m_Rects[63], m_LeftFormat);
            e.DrawString(m_SetEndStationName, FormatStyle.m_Font12,
                FormatStyle.m_WhiteBrush, m_Rects[64], m_LeftFormat);

            switch (m_CursorID)
            {
                case 0:
                    e.DrawImage(m_Images[2], m_Rects[63]);
                    e.DrawString(m_SetStartStationName + Cursor(), FormatStyle.m_Font12B,
                        FormatStyle.m_WhiteBrush, m_Rects[63], m_LeftFormat);
                    break;
                case 1:
                    e.DrawImage(m_Images[2], m_Rects[64]);
                    e.DrawString(m_SetEndStationName + Cursor(), FormatStyle.m_Font12B,
                        FormatStyle.m_WhiteBrush, m_Rects[64], m_LeftFormat);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 填充数值
        /// </summary>
        /// <param name="e"></param>
        private void DrawValue(Graphics e)
        {
            DrawStationsBtn(e);

            //设定按钮
            if (m_MenuID == StationSettingMenuID.Line1 ||
                m_MenuID == StationSettingMenuID.Line2)
            {
                if (m_SetisBtnCanRun)
                {
                    if (m_ButtonIsDown[60])
                        e.DrawImage(m_Images[2], m_Rects[49]);
                    else
                        e.DrawImage(m_Images[0], m_Rects[49]);
                }
                else
                    e.DrawImage(m_Images[4], m_Rects[49]);
                e.DrawString("设 定", FormatStyle.m_Font12B,
                    m_SetisBtnCanRun ? FormatStyle.m_BlackBrush : FormatStyle.m_MediumGreySolidBrush
                    , m_Rects[49], m_DrawFormat);
            }

            //右侧菜单栏
            for (int i = 0; i < 2; i++)
            {
                //暂时做成灰色的
                if (!m_ButtonIsDown[i])
                    e.DrawImage(m_Images[4], m_Rects[51 + i]);
                else
                    e.DrawImage(m_Images[2], m_Rects[51 + i]);
                e.DrawString(FormatStyle.m_Str15[i], FormatStyle.m_Font14,
                    FormatStyle.m_MediumGreySolidBrush, m_Rects[51 + i], m_DrawFormat);
            }
            for (int i = 2; i < 8; i++)
            {
                if (!m_ButtonIsDown[i])
                    e.DrawImage(m_Images[0], m_Rects[51 + i]);
                else
                    e.DrawImage(m_Images[5], m_Rects[51 + i]);
                e.DrawString(FormatStyle.m_Str15[i], FormatStyle.m_Font14,
                    FormatStyle.m_BlackBrush, m_Rects[51 + i], m_DrawFormat);
            }
        }

        /// <summary>
        /// 根据选择的菜单编号绘制
        /// 相应的站点信息
        /// </summary>
        /// <param name="e"></param>


        private void DrawStationsBtn(Graphics e)
        {
            switch (m_MenuID)
            {
                case StationSettingMenuID.AutoBroadcast: //全自动广播
                    break;
                case StationSettingMenuID.SemiAutoBroadcast: //半自动广播
                    break;
                case StationSettingMenuID.Line1: //1号线
                    for (int i = 0; i < m_LineID1.Count; i++)
                    {
                        if (!m_ButtonIsDown[i + 12])
                            e.DrawImage(m_Images[0], m_Rects[i + 1]);
                        else
                            e.DrawImage(m_Images[2], m_Rects[i + 1]);
                        e.DrawString(m_LineID1[i], FormatStyle.m_Font12B,
                            FormatStyle.m_BlackBrush, m_Rects[i + 1], m_DrawFormat);

                    }
                    break;
                case StationSettingMenuID.Line2: //2号线
                    for (int i = 0; i < m_LineID2.Count; i++)
                    {
                        if (!m_ButtonIsDown[i + 12])
                            e.DrawImage(m_Images[0], m_Rects[i + 1]);
                        else
                            e.DrawImage(m_Images[2], m_Rects[i + 1]);
                        e.DrawString(m_LineID2[i], FormatStyle.m_Font12B,
                            FormatStyle.m_BlackBrush, m_Rects[i + 1], m_DrawFormat);
                    }
                    break;
                case StationSettingMenuID.RouteSelect: //Route选择
                    break;
            }
        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            if (m_IsInLinesSelectView)
            {
                DrawTrainLinesSelect(e);
            }
            else
            {
                DrawFrame(e);
                DrawStationNamesAndLineID(e);
                DrawValue(e);
            }
        }

        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::

        private bool LinesSelectRight(string linesIndex)
        {
            if (m_SetLineID == "1" || m_SetLineID == "2")
            {
                m_SetLineRight = true;
            }
            else
                m_SetLineRight = false;

            return m_SetLineRight;
        }

        /// <summary>
        /// 线路计算
        /// </summary>
        /// <param name="index"></param>
        private void LinesCount(int index)
        {
            if (index >= 0 && index < 9)
            {
                m_SetLineID = (index + 1).ToString();
            }
            else if (index == 9)
            {
                m_SetLineID = "0";
            }
            else if (index == 10)
            {
                m_SetLineID = string.Empty;
            }
            else if (index == 11)
            {
                if (LinesSelectRight(m_SetLineID) && m_SetLineID != string.Empty)
                {
                    int ii = Convert.ToInt32(m_SetLineID);
                    OnPost(CmdType.SetFloatValue, UIObj.OutFloatList[2], 0, Convert.ToInt32(m_SetLineID));
                    m_IsInLinesSelectView = false;
                    m_MenuID = StationSettingMenuID.Line1;
                }
                else
                    m_IsShowErrorMsg = true;
            }
            else if (index == 12)
            {
                m_IsInLinesSelectView = false;
                m_MenuID = StationSettingMenuID.Line1;
            }
        }

        /// <summary>
        /// 判断当前点下的站点是起点站还是终点站
        /// 获取相应站点并正确显示
        /// </summary>
        /// <param name="btnId">按键的序号</param>
        /// <param name="isPressStartStation">当前是否选择起点站</param>
        private void StationBtnState(int btnId, bool isPressStartStation)
        {
            int initNumb = 12;
            for (int i = initNumb; i < BtnNumbers(m_MenuID) + initNumb; i++)
            {
                m_ButtonIsDown[i] = false;
            }

            if (isPressStartStation)
            {
                m_StartStationBtnLock = btnId;
                m_ButtonIsDown[m_StartStationBtnLock] = true;

                if (m_EndStationBtnLock != -1)
                {
                    m_ButtonIsDown[m_EndStationBtnLock] = true;
                }
            }
            else
            {
                m_EndStationBtnLock = btnId;
                m_ButtonIsDown[m_EndStationBtnLock] = true;
                if (m_StartStationBtnLock != -1)
                {
                    m_ButtonIsDown[m_StartStationBtnLock] = true;
                }
            }
        }

        /// <summary>
        /// 光标计数器
        /// </summary>
        private int m_Cursorcount = 0;

        /// <summary>
        /// 光标计数器方法
        /// </summary>
        /// <returns></returns>
        private string Cursor()
        {
            m_Cursorcount++;
            if (m_Cursorcount < 4)
            {
                m_Cursorcount++;
                return "|";
            }
            else if (m_Cursorcount >= 4 && m_Cursorcount < 8)
            {
                m_Cursorcount++;
                return "";
            }
            else
            {
                m_Cursorcount = 0;
                return "";
            }
        }

        /// <summary>
        /// 复位信息
        /// </summary>
        /// <param name="typeId">复位类型:0为始发终点站复位;1为始发终点站清空;2为所有按键复位</param>
        private void ResetInfo(int typeId)
        {
            switch (typeId)
            {
                case 0: //始发站终点站复位
                    for (int i = 0; i < 2; i++)
                    {
                        m_ButtonIsDown[8 + i] = false;
                    }
                    m_CursorID = -1;
                    break;
                case 1: //清空始发站和终点站信息
                    m_SetStartStationId = -1;
                    m_SetStartStationName = string.Empty;

                    m_SetEndStationID = -1;
                    m_SetEndStationName = string.Empty;

                    break;
                case 2: //所有按键复位
                    for (int i = 0; i < 49; i++)
                    {
                        m_ButtonIsDown[i + 12] = false;
                    }
                    m_StartStationBtnLock = -1;
                    m_EndStationBtnLock = -1;
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }

        /// <summary>
        /// 判断是否需要重置始发站和终点站
        /// 包括按键框和光标
        /// 根据上一次按的线路与当前线路判断
        /// </summary>
        /// <param name="currentID">当前所在的运行线路界面</param>
        /// <returns></returns>
        private bool NeedResetStartAndEndStation(int currentLineID)
        {
            if (currentLineID == m_LastbtnOfLineID)
            {
                return false;
            }
            else
            {
                m_LastbtnOfLineID = currentLineID;
                return true;
            }
        }

        /// <summary>
        /// 根据menusID判断当前线路
        /// 并返回需要创建的地铁站按钮个数
        /// </summary>
        /// <param name="menusID">线路参数</param>
        /// <returns></returns>
        private int BtnNumbers(StationSettingMenuID menusID)
        {
            switch (menusID)
            {
                case StationSettingMenuID.Line1:
                    return m_LineID1.Count;
                case StationSettingMenuID.Line2:
                    return m_LineID2.Count;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// 获取始发站和终点站名称以及编号
        /// </summary>
        /// <param name="btnIndex">按键的编号</param>
        /// <param name="isStartStation">当前为选择始发站还是终点站</param>
        private void StationName(int btnIndex, bool isStartStation)
        {
            switch (m_MenuID)
            {
                case StationSettingMenuID.Line1:
                    if (isStartStation)
                    {
                        m_SetStartStationName = m_LineID1[btnIndex]; //线路1中的站名赋值
                        m_SetStartStationId = m_StationNameID[m_SetStartStationName]; //所有站中找出站名，将站代表的编号赋值
                    }
                    else
                    {
                        m_SetEndStationName = m_LineID1[btnIndex];
                        if (m_SetEndStationName == m_SetStartStationName)
                        {
                            //终点站和起点站不能相同，相同则将终点站清空重选
                            m_SetEndStationName = string.Empty;
                            m_SetEndStationID = -1;
                        }
                        else
                        {
                            m_SetEndStationName = m_LineID1[btnIndex];
                            m_SetEndStationID = m_StationNameID[m_SetEndStationName];
                        }
                    }
                    break;
                case StationSettingMenuID.Line2:
                    if (isStartStation)
                    {
                        m_SetStartStationName = m_LineID2[btnIndex];
                        m_SetStartStationId = m_StationNameID[m_SetStartStationName];
                    }
                    else
                    {
                        m_SetEndStationName = m_LineID2[btnIndex];
                        if (m_SetEndStationName == m_SetStartStationName)
                        {
                            m_SetEndStationName = string.Empty;
                            m_SetEndStationID = -1;
                        }
                        else
                        {
                            m_SetEndStationName = m_LineID2[btnIndex];
                            m_SetEndStationID = m_StationNameID[m_SetEndStationName];
                        }
                    }
                    break;
            }
        }

        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        /// <summary>
        /// 初始化坐标、数组、热区
        /// </summary>
        private void InitData()
        {
            m_DrawFormat.LineAlignment = StringAlignment.Center;
            m_DrawFormat.Alignment = StringAlignment.Center;

            m_RightFormat.LineAlignment = StringAlignment.Center;
            m_RightFormat.Alignment = StringAlignment.Far;

            m_LeftFormat.LineAlignment = StringAlignment.Center;
            m_LeftFormat.Alignment = StringAlignment.Near;

            m_PDrawPoint = new PointF[200];

            m_Rects = new RectangleF[200];

            m_Images = new Image[30];

            m_ButtonIsDown = new bool[70];

            m_KeyIsDown = new bool[14];

            m_KeyBoardBtns = new List<Region>();

            m_Regions = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::::::::: _rects :::::::::::::::::::::::::::::::::::

            m_Rects[0] = new Rectangle(10, 160, 690, 380);
            //站点
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    m_Rects[1 + i*7 + j] = new Rectangle(25 + j*95, 165 + i*53, 90, 50);
                }
            }
            //站点设置
            for (int i = 0; i < 8; i++)
            {
                m_Rects[51 + i] = new Rectangle(710, 100 + i*56, 89, 54);
            }
            //始发站/终点站
            for (int i = 0; i < 2; i++)
            {
                m_Rects[59 + i] = new Rectangle(30 + i*425, 105, 75, 45);
                m_Rects[61 + i] = new Rectangle(430, 106 + i*23, 90, 20);
                m_Rects[63 + i] = new Rectangle(105 + i*425, 105, 130, 45);
                m_Rects[65 + i] = new Rectangle(530, 106 + i*23, 130, 20);
            }

            //线路选择
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects[68 + i*2 + j] = new Rectangle(100 + i*100, 160 + j*38, 100, 35);
                }
            }

            //MoveScreen
            for (int i = 0; i < m_Rects.Length; i++)
            {
                m_Rects[i].X = (m_Rects[i].X + FormatStyle.ScreenMoveX)*FormatStyle.Scale;
                m_Rects[i].Y = (m_Rects[i].Y + FormatStyle.ScreenMoveY)*FormatStyle.Scale;
                m_Rects[i].Width *= FormatStyle.Scale;
                m_Rects[i].Height *= FormatStyle.Scale;
            }

            #endregion

            #region :::::::::::::::::::::::::::::::: _regions ::::::::::::::::::::::::::::::::::::::

            for (int i = 0; i < 8; i++)
            {
                m_Regions.Add(new Region(m_Rects[51 + i]));
            }
            for (int i = 0; i < 4; i++)
            {
                m_Regions.Add(new Region(m_Rects[63 + i]));
            }
            for (int i = 0; i < 49; i++)
            {
                m_Regions.Add(new Region(m_Rects[1 + i]));
            }

            #endregion

            #region :::::::::::::::::::::::::::::::: point ::::::::::::::::::::::::::::::::::::::

            m_PDrawPoint[0] = new Point(705, 95);
            m_PDrawPoint[1] = new Point(705, 550);
            m_PDrawPoint[2] = new Point(380, 160);

            //MoveScreen
            for (int i = 0; i < m_PDrawPoint.Length; i++)
            {
                m_PDrawPoint[i].X = (m_PDrawPoint[i].X + FormatStyle.ScreenMoveX)*FormatStyle.Scale;
                m_PDrawPoint[i].Y = (m_PDrawPoint[i].Y + FormatStyle.ScreenMoveY)*FormatStyle.Scale;
            }

            #endregion

            m_KeyBoard = new NumbKeyboard(m_PDrawPoint[2]);

            for (int i = 0; i < m_KeyBoard.Rects.Length; i++)
            {
                m_KeyBoardBtns.Add(new Region(m_KeyBoard.Rects[i]));
            }
        }

        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();

        /// <summary>
        /// 新线路
        /// </summary>
        private string m_SetLineID = string.Empty; //_routeLineID

        /// <summary>
        /// 是否有这条新线路
        /// </summary>
        private bool m_SetLineRight = false;

        /// <summary>
        /// 是否要显示错误消息
        /// </summary>
        private bool m_IsShowErrorMsg = false;

        /// <summary>
        /// 是否进入线路选择界面
        /// </summary>
        private bool m_IsInLinesSelectView = false;

        /// <summary>
        /// 光标所在位置
        /// -1表示没有选择站点框
        /// 0表示在起点站框
        /// 1表示在终点站框
        /// 2表示始发站和终点站都已经选择完毕，可以按设定按钮
        /// </summary>
        private int m_CursorID = -1;

        /// <summary>
        /// -1表示未选择起点站，当按下站点后
        /// 将站点代表的按键编号赋给当前变量
        /// </summary>
        private int m_StartStationBtnLock = -1;

        /// <summary>
        /// -1表示未选择终点站，当按下站点后
        /// 将站点代表的按键编号赋给当前变量
        /// </summary>
        private int m_EndStationBtnLock = -1;

        /// <summary>
        /// 判断选站操作是否结束，可以按设定按钮
        /// </summary>
        private bool m_SetisBtnCanRun = false;

        /// <summary>
        /// 上一次按键编号，与始发站终点站有关
        /// </summary>
        private int m_LastbtnOfLineID = 1;

        /// <summary>
        /// 当前线路ID
        /// 默认为1
        /// </summary>
        private int m_CurrentLineID = 1;

        /// <summary>
        /// 选择运行线路
        /// 默认为1
        /// </summary>
        private int m_RouteLineID = 1;

        /// <summary>
        /// 始发站ID
        /// </summary>
        private int m_SetStartStationId = -1;

        /// <summary>
        /// 始发站名称
        /// </summary>
        private string m_SetStartStationName = string.Empty;

        /// <summary>
        /// 终点站ID
        /// </summary>
        private int m_SetEndStationID = -1;

        /// <summary>
        /// 终点站名称
        /// </summary>
        private string m_SetEndStationName = string.Empty;

        //判断当前读取的文本信息，0为空，1为车站信息，2为线路信息
        private int m_ReadInfoId = 0;

        //根据右边按钮选择对应的菜单
        private StationSettingMenuID m_MenuID = StationSettingMenuID.Line1;

        /// <summary>
        /// 所有站点名称
        /// </summary>
        private List<string> m_Stations = new List<string>();

        //1号线站点
        private readonly List<string> m_LineID1 = new List<string>();
        //2号线站点
        private readonly List<string> m_LineID2 = new List<string>();

        //
        private NumbKeyboard m_KeyBoard;

        private List<Region> m_KeyBoardBtns;

        private bool[] m_KeyIsDown;

        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::

        /// <summary>
        /// 坐标集
        /// </summary>
        private PointF[] m_PDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        private RectangleF[] m_Rects;

        /// <summary>
        /// 图片集
        /// </summary>
        private Image[] m_Images;

        /// <summary>
        /// 键是否按下
        /// </summary>
        private bool[] m_ButtonIsDown;

        /// <summary>
        /// 按钮是否可以按
        /// </summary>
        private bool[] m_BtnCanDown;

        /// <summary>
        /// 按键列表
        /// </summary>
        private List<Region> m_Regions;

        private readonly Dictionary<string, int> m_StationNameID = new Dictionary<string, int>();

        #endregion#

        #endregion
    }
}