using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.PublicUI;
using MMI.Facility.Interface.Data;

using ES.Facility.PublicModule.Memo;

namespace KumM_TMS.运行
{
    /// <summary>
    /// 站点设置
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class StationSet : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "站点设置";
        }

     

        public override bool init(ref int nErrorObjectIndex)
        {
            LoadFile();
            InitData();
            return true;
        }

        private void LoadFile()
        {
            var file = Path.Combine(RecPath, "..\\config\\车站信息.txt");

            foreach (var line in File.ReadAllLines(file,Encoding.Default))
            {
                LoadStation(line);
            }
            file = Path.Combine(RecPath, "..\\config\\线路信息.txt");

            foreach (var line in File.ReadAllLines(file, Encoding.Default))
            {
                LoadLineData(line);
            }
        }

        void LoadStation(string sPara)
        {
            string[] split = sPara.Split('\t');
            int stationID;
            if (split.Length == 2 && int.TryParse(split[0], out stationID))
            {
                _stationNameID.Add(split[1], stationID);
            }
        }

        void LoadLineData(string sPara)
        {
            string[] split = sPara.Split('\t');
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
                        _lineID1.Add(st);
                    }
                }
                else if (lineID.Trim() == "2号线")
                {
                    foreach (string st in stations)
                    {
                        _lineID2.Add(st);
                    }
                }
                else if (lineID.Trim() == "3号线")
                {
                    foreach (string st in stations)
                    {
                        _lineID3.Add(st);
                    }
                }
                else if (lineID.Trim() == "6号线")
                {
                    foreach (string st in stations)
                    {
                        _lineID6.Add(st);
                    }
                }
            }
        } 

        private Dictionary<string, int> _stationNameID = new Dictionary<string, int>();
     

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
            if (isInLinesSelectView)
            {
                #region :::::::: 线路选择
                for (; index < 13; ++index)
                {
                    if (keyBoardBtns[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                keyIsDown[index] = true;
                #endregion
            }
            else
            {
                #region :::::::: 右边栏按钮
                index = 0;
                for (; index < 8; ++index)
                {
                    if (Rect[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                switch (index)
                {
                    case 0://全自动广播
                        buttonIsDown[0] = true;
                        buttonIsDown[1] = false;
                        break;
                    case 1://半自动广播
                        buttonIsDown[0] = false;
                        buttonIsDown[1] = true;
                        break;
                    case 2://1号线
                        buttonIsDown[2] = true;
                        break;
                    case 3://2号线
                        buttonIsDown[3] = true;
                        break;
                    case 4://3号线
                        buttonIsDown[4] = true;
                        break;
                    case 5://6号线
                        buttonIsDown[5] = true;
                        break;
                    case 6://Route选择
                        buttonIsDown[6] = true;
                        break;
                    case 7://返回
                        buttonIsDown[7] = true;
                        break;
                }
                #endregion

                #region :::::::: 光标位置的始发站和终点站以及设定按键
                if (_menuID == StationSettingMenuID.Line1 ||
                    _menuID == StationSettingMenuID.Line2 ||
                    _menuID == StationSettingMenuID.Line3 ||
                    _menuID == StationSettingMenuID.Line6)
                {
                    index = 8;
                    for (; index < 10; ++index)
                    {
                        if (Rect[index].IsVisible(nPoint))
                        {
                            break;
                        }
                    }
                    switch (index)
                    {
                        case 8:
                            buttonIsDown[8] = true;
                            buttonIsDown[9] = false;
                            cursorID = 0;
                            break;
                        case 9://只有始发站不为空才能点终点站
                            if (setStartStationName != string.Empty)
                            {
                                buttonIsDown[8] = false;
                                buttonIsDown[9] = true;
                                cursorID = 1;
                            }
                            break;
                    }
                    if (Rect[60].IsVisible(nPoint))
                    {
                        if (setBtnCanRun) buttonIsDown[60] = true;
                    }
                }
                #endregion

                #region  :::::::: 相应线路选择始发站和终点站
                index = 12;
                for (; index < BtnNumbers(_menuID) + 12; ++index)
                {
                    if (Rect[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                if (index >= 12 && index < BtnNumbers(_menuID) + 12)
                {
                    buttonIsDown[index] = true;     //选站按键不锁定

                    if (cursorID == 0)
                    {
                        StationBtnState(index, true);
                        StationName(index - 12, true);
                        //cursorID = 1;       //当选择完成起点站框后自动跳转到终点站框
                    }
                    else if (cursorID == 1)
                    {
                        if (setStartStationName != string.Empty)
                        {
                            StationBtnState(index, false);
                            StationName(index - 12, false);
                            cursorID = 2;
                            setBtnCanRun = true;
                        }
                    }
                    else if (cursorID == 2)
                    {
                    }
                    else
                    {
                        //当没有选择线路的情况下，按下站点名都会弹起
                        buttonIsDown[index] = true;
                    }
                }
                #endregion
            }

            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            int index = 0;
            if (isInLinesSelectView)
            {
                #region :::::::: 线路选择
                for (; index < 13; ++index)
                {
                    if (keyBoardBtns[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                keyIsDown[index] = false;
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
                    if (Rect[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                switch (index)
                {
                    case 2://1号线
                        buttonIsDown[2] = false;
                        _menuID = StationSettingMenuID.Line1;
                        currentLineID = 1;
                        if (NeedResetStartAndEndStation(1))
                        {
                            ResetInfo(0);
                            ResetInfo(1);
                            ResetInfo(2);
                        }
                        break;
                    case 3://2号线
                        buttonIsDown[3] = false;
                        _menuID = StationSettingMenuID.Line2;
                        currentLineID = 2;
                        if (NeedResetStartAndEndStation(2))
                        {
                            ResetInfo(0);
                            ResetInfo(1);
                            ResetInfo(2);
                        }
                        break;
                    case 4://3号线
                        buttonIsDown[4] = false;
                        _menuID = StationSettingMenuID.Line3;
                        currentLineID = 3;
                        if (NeedResetStartAndEndStation(3))
                        {
                            ResetInfo(0);
                            ResetInfo(1);
                            ResetInfo(2);
                        }
                        break;
                    case 5://6号线
                        buttonIsDown[5] = false;
                        _menuID = StationSettingMenuID.Line6;
                        currentLineID = 6;
                        if (NeedResetStartAndEndStation(6))
                        {
                            ResetInfo(0);
                            ResetInfo(1);
                            ResetInfo(2);
                        }
                        break;
                    case 6://Route选择
                        buttonIsDown[6] = false;
                        isInLinesSelectView = true;
                        ResetInfo(0);
                        ResetInfo(1);
                        ResetInfo(2);
                        break;
                    case 7://返回
                        buttonIsDown[7] = false;
                        append_postCmd(CmdType.ChangePage, 11, 0, 0);
                        break;
                }
                #endregion

                #region :::::::: 设定按键
                if (_menuID == StationSettingMenuID.Line1 ||
                    _menuID == StationSettingMenuID.Line2 ||
                    _menuID == StationSettingMenuID.Line3 ||
                    _menuID == StationSettingMenuID.Line6)
                {
                    if (Rect[60].IsVisible(nPoint))
                    {
                        if (setBtnCanRun)
                        {
                            buttonIsDown[60] = false;
                            append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, setStartStationID);
                            append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[1], 0, setEndStationID);
                            setBtnCanRun = false;
                        }
                    }
                }
                #endregion

                #region  :::::::: 相应线路选择始发站和终点站
                index = 12;
                for (; index < BtnNumbers(_menuID) + 12; ++index)
                {
                    if (Rect[index].IsVisible(nPoint))
                    {
                        break;
                    }
                }
                if (index >= 12 && index < BtnNumbers(_menuID) + 12)
                {
                    buttonIsDown[index] = false;        //选站按钮不锁定

                    if (cursorID == 0)
                    {
                    }
                    else if (cursorID == 1)
                    {
                    }
                    else if (cursorID == 2)
                    {
                    }
                    else
                    {
                    }
                }
                #endregion
            }

            return base.mouseUp(nPoint);
        }
        #endregion

        #region :::::::::::::::::::::::::::: draw funes ::::::::::::::::::::::::::
        /// <summary>
        /// 框架
        /// </summary>
        /// <param name="e"></param>
        private void DrawFrame(Graphics e)
        {
            //for (int i = 0; i < 65; i++)
            //{
            //    e.DrawRectangle(FormatStyle.WhitePen, rects[i].X, rects[i].Y, rects[i].Width, rects[i].Height);
            //}
            e.DrawRectangle(FormatStyle.WhitePen, rects[0].X, rects[0].Y, rects[0].Width, rects[0].Height);
            //
            e.DrawLine(FormatStyle.MediumGreyPen, pDrawPoint[0], pDrawPoint[1]);
        }

        /// <summary>
        /// 填充数值
        /// </summary>
        /// <param name="e"></param>
        private void DrawValue(Graphics e)
        {
            DrawStationsBtn(e);

            //设定
            if (_menuID == StationSettingMenuID.Line1 ||
                _menuID == StationSettingMenuID.Line2 ||
                _menuID == StationSettingMenuID.Line3 ||
                _menuID == StationSettingMenuID.Line6)
            {
                if (setBtnCanRun)
                {
                    if (buttonIsDown[60])
                        e.DrawImage(Img[2], rects[49]);
                    else
                        e.DrawImage(Img[0], rects[49]);
                }
                else
                    e.DrawImage(Img[4], rects[49]);
                e.DrawString("设 定", FormatStyle.Font12B,
                    setBtnCanRun ? FormatStyle.BlackBrush : FormatStyle.MediumGreySolidBrush
                    , rects[49], drawFormat);
            }

            //菜单
            for (int i = 0; i < 2; i++)
            {
                if (!buttonIsDown[i])
                    e.DrawImage(Img[4], rects[51 + i]);
                else
                    e.DrawImage(Img[2], rects[51 + i]);
                e.DrawString(FormatStyle.Str15[i], FormatStyle.Font14,
                    FormatStyle.MediumGreySolidBrush, rects[51 + i], drawFormat);
            }
            for (int i = 2; i < 8; i++)
            {
                if (!buttonIsDown[i])
                    e.DrawImage(Img[0], rects[51 + i]);
                else
                    e.DrawImage(Img[2], rects[51 + i]);
                e.DrawString(FormatStyle.Str15[i], FormatStyle.Font14,
                    FormatStyle.BlackBrush, rects[51 + i], drawFormat);
            }
        }


        /// <summary>
        /// 根据选择的菜单编号绘制
        /// 相应的站点信息
        /// </summary>
        /// <param name="e"></param>
        private void DrawStationsBtn(Graphics e)
        {
            switch (_menuID)
            {
                case StationSettingMenuID.AutoBroadcast://全自动广播
                    break;
                case StationSettingMenuID.semiAutoBroadcast://半自动广播
                    break;
                case StationSettingMenuID.Line1://1号线
                    for (int i = 0; i < _lineID1.Count; i++)
                    {
                        if (!buttonIsDown[i + 12])
                            e.DrawImage(Img[0], rects[i + 1]);
                        else
                            e.DrawImage(Img[2], rects[i + 1]);
                        e.DrawString(_lineID1[i], FormatStyle.Font12B,
                            FormatStyle.BlackBrush, rects[i + 1], drawFormat);
                    }
                    break;
                case StationSettingMenuID.Line2://2号线
                    for (int i = 0; i < _lineID2.Count; i++)
                    {
                        if (!buttonIsDown[i + 12])
                            e.DrawImage(Img[0], rects[i + 1]);
                        else
                            e.DrawImage(Img[2], rects[i + 1]);
                        e.DrawString(_lineID2[i], FormatStyle.Font12B,
                            FormatStyle.BlackBrush, rects[i + 1], drawFormat);
                    }
                    break;
                case StationSettingMenuID.Line3://3号线
                    for (int i = 0; i < _lineID3.Count; i++)
                    {
                        if (!buttonIsDown[i + 12])
                            e.DrawImage(Img[0], rects[i + 1]);
                        else
                            e.DrawImage(Img[2], rects[i + 1]);
                        e.DrawString(_lineID3[i], FormatStyle.Font12B,
                            FormatStyle.BlackBrush, rects[i + 1], drawFormat);
                    }
                    break;
                case StationSettingMenuID.Line6://6号线
                    for (int i = 0; i < _lineID6.Count; i++)
                    {
                        if (!buttonIsDown[i + 12])
                            e.DrawImage(Img[0], rects[i + 1]);
                        else
                            e.DrawImage(Img[2], rects[i + 1]);
                        e.DrawString(_lineID6[i], FormatStyle.Font12B,
                            FormatStyle.BlackBrush, rects[i + 1], drawFormat);
                    }
                    break;
                case StationSettingMenuID.RouteSelect://Route选择
                    break;
            }
        }

        /// <summary>
        /// 始发站、终点站、选择线路和运行线路
        /// </summary>
        private void DrawStationNamesAndLineID(Graphics e)
        {
            for (int i = 0; i < 4; i++)
            {
                e.DrawString(FormatStyle.Str15[i + 8], FormatStyle.Font12B,
                    FormatStyle.WhiteBrush, rects[59 + i], RightFormat);
                e.DrawImage(Img[3], rects[63 + i]);
            }

            e.DrawString(setStartStationName, FormatStyle.Font12,
                FormatStyle.WhiteBrush, rects[63], LeftFormat);
            e.DrawString(setEndStationName, FormatStyle.Font12,
                FormatStyle.WhiteBrush, rects[64], LeftFormat);
            e.DrawString(currentLineID.ToString(), FormatStyle.Font12,
                FormatStyle.WhiteBrush, rects[65], drawFormat);
            e.DrawString(Convert.ToInt32(FloatList[UIObj.InFloatList[0]]).ToString(),
                FormatStyle.Font12, FormatStyle.WhiteBrush, rects[66], drawFormat);

            switch (cursorID)
            {
                case 0:
                    e.DrawImage(Img[2], rects[63]);
                    e.DrawString(setStartStationName + Cursor(), FormatStyle.Font12B,
                        FormatStyle.WhiteBrush, rects[63], LeftFormat);
                    break;
                case 1:
                    e.DrawImage(Img[2], rects[64]);
                    e.DrawString(setEndStationName + Cursor(), FormatStyle.Font12B,
                        FormatStyle.WhiteBrush, rects[64], LeftFormat);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            if (isInLinesSelectView)
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

        /// <summary>
        /// 线路选择
        /// </summary>
        /// <param name="e"></param>
        private void DrawTrainLinesSelect(Graphics e)
        {
            _keyBoard.DrawKeyboard(e, ref keyIsDown, drawFormat);
            e.DrawString("原线路", FormatStyle.Font12B, FormatStyle.WhiteBrush,
                rects[68], RightFormat);
            e.DrawString("新线路", FormatStyle.Font12B, FormatStyle.WhiteBrush,
                rects[69], RightFormat);
            for (int i = 0; i < 2; i++)
            {
                e.FillRectangle(FormatStyle.WhiteBrush, rects[70 + i]);
            }
            e.DrawString(currentLineID.ToString(), FormatStyle.Font18B,
                FormatStyle.BlackBrush, rects[70], LeftFormat);
            e.DrawString(_setLineID, FormatStyle.Font18B,
                FormatStyle.BlackBrush, rects[71], LeftFormat);
        }
        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
        private bool LinesSelectRight(string linesIndex)
        {
            if (_setLineID == "1" || _setLineID == "2" ||
                _setLineID == "3" || _setLineID == "6")
            {
                _setLineRight = true;
            }
            else
                _setLineRight = false;

            return _setLineRight;
        }

        /// <summary>
        /// 线路计算
        /// </summary>
        /// <param name="index"></param>
        private void LinesCount(int index)
        {
            if (index >= 0 && index < 9)
            {
                _setLineID = (index + 1).ToString();
            }
            else if (index == 9)
            {
                _setLineID = "0";
            }
            else if (index == 10)
            {
                _setLineID = string.Empty;
            }
            else if (index == 11)
            {
                if (LinesSelectRight(_setLineID) && _setLineID != string.Empty)
                {
                    int ii = Convert.ToInt32(_setLineID);
                    append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[2], 0, Convert.ToInt32(_setLineID));
                }
                else
                    isShowErrorMsg = true;

                isInLinesSelectView = false;
                _menuID = StationSettingMenuID.Line1;
            }
            else if (index == 12)
            {
                isInLinesSelectView = false;
                _menuID = StationSettingMenuID.Line1;
            }
        }

        /// <summary>
        /// 判断当前点下的站点是起点站还是终点站
        /// 获取相应站点并正确显示
        /// </summary>
        /// <param name="BtnId">按键的序号</param>
        /// <param name="isPressStartStation">当前是否选择起点站</param>
        private void StationBtnState(int BtnId, bool isPressStartStation)
        {
            int initNumb = 12;
            for (int i = initNumb; i < BtnNumbers(_menuID) + initNumb; i++)
            {
                buttonIsDown[i] = false;
            }

            if (isPressStartStation)
            {
                startStationBtnLock = BtnId;
                buttonIsDown[startStationBtnLock] = true;

                //if (endStationBtnLock != -1)
                //{
                //    buttonIsDown[endStationBtnLock] = true;
                //}
            }
            else
            {
                endStationBtnLock = BtnId;
                buttonIsDown[endStationBtnLock] = true;
                //if (startStationBtnLock != -1)
                //{
                //    buttonIsDown[startStationBtnLock] = true;
                //}
            }
        }

        /// <summary>
        /// 光标计数器
        /// </summary>
        int cursorcount = 0;

        /// <summary>
        /// 光标计数器方法
        /// </summary>
        /// <returns></returns>
        private string Cursor()
        {
            cursorcount++;
            if (cursorcount < 4)
            {
                cursorcount++;
                return "|";
            }
            else if (cursorcount >= 4 && cursorcount < 8)
            {
                cursorcount++;
                return "";
            }
            else
            {
                cursorcount = 0;
                return "";
            }
        }

        /// <summary>
        /// 复位信息
        /// </summary>
        /// <param name="typeId">复位类型</param>
        private void ResetInfo(int typeId)
        {
            switch (typeId)
            {
                case 0://始发站终点站复位
                    for (int i = 0; i < 2; i++)
                    {
                        buttonIsDown[8 + i] = false;
                    }
                    cursorID = -1;
                    break;
                case 1://清空始发站和终点站信息
                    setStartStationID = -1;
                    setStartStationName = string.Empty;

                    setEndStationID = -1;
                    setEndStationName = string.Empty;

                    break;
                case 2://所有按键复位
                    for (int i = 0; i < 49; i++)
                    {
                        buttonIsDown[i + 12] = false;
                    }
                    startStationBtnLock = -1;
                    endStationBtnLock = -1;
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
            if (currentLineID == lastbtnOfLineID)
            {
                return false;
            }
            else
            {
                lastbtnOfLineID = currentLineID;
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
                    return _lineID1.Count;
                case StationSettingMenuID.Line2:
                    return _lineID2.Count;
                case StationSettingMenuID.Line3:
                    return _lineID3.Count;
                case StationSettingMenuID.Line6:
                    return _lineID6.Count;
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
            switch (_menuID)
            {
                case StationSettingMenuID.Line1:
                    if (isStartStation)
                    {
                        setStartStationName = _lineID1[btnIndex];               //线路1中的站名赋值
                        setStartStationID = _stationNameID[setStartStationName];//所有站中找出站名，将站代表的编号赋值
                    }
                    else
                    {
                        setEndStationName = _lineID1[btnIndex];
                        if (setEndStationName == setStartStationName)
                        {
                            //终点站和起点站不能相同，相同则将终点站清空重选
                            setEndStationName = string.Empty;
                            setEndStationID = -1;
                        }
                        else
                        {
                            setEndStationName = _lineID1[btnIndex];
                            setEndStationID = _stationNameID[setEndStationName];
                        }
                    }
                    break;
                case StationSettingMenuID.Line2:
                    if (isStartStation)
                    {
                        setStartStationName = _lineID2[btnIndex];
                        setStartStationID = _stationNameID[setStartStationName];
                    }
                    else
                    {
                        setEndStationName = _lineID2[btnIndex];
                        if (setEndStationName == setStartStationName)
                        {
                            setEndStationName = string.Empty;
                            setEndStationID = -1;
                        }
                        else
                        {
                            setEndStationName = _lineID2[btnIndex];
                            setEndStationID = _stationNameID[setEndStationName];
                        }
                    }
                    break;
                case StationSettingMenuID.Line3:
                    if (isStartStation)
                    {
                        setStartStationName = _lineID3[btnIndex];
                        setStartStationID = _stationNameID[setStartStationName];
                    }
                    else
                    {
                        setEndStationName = _lineID3[btnIndex];
                        if (setEndStationName == setStartStationName)
                        {
                            setEndStationName = string.Empty;
                            setEndStationID = -1;
                        }
                        else
                        {
                            setEndStationName = _lineID3[btnIndex];
                            setEndStationID = _stationNameID[setEndStationName];
                        }
                    }
                    break;
                case StationSettingMenuID.Line6:
                    if (isStartStation)
                    {
                        setStartStationName = _lineID6[btnIndex];
                        setStartStationID = _stationNameID[setStartStationName];
                    }
                    else
                    {
                        setEndStationName = _lineID6[btnIndex];
                        if (setEndStationName == setStartStationName)
                        {
                            setEndStationName = string.Empty;
                            setEndStationID = -1;
                        }
                        else
                        {
                            setEndStationName = _lineID6[btnIndex];
                            setEndStationID = _stationNameID[setEndStationName];
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
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;

            RightFormat.LineAlignment = StringAlignment.Center;
            RightFormat.Alignment = StringAlignment.Far;

            LeftFormat.LineAlignment = StringAlignment.Center;
            LeftFormat.Alignment = StringAlignment.Near;

            pDrawPoint = new PointF[200];

            rects = new RectangleF[200];

            Img = new Image[30];

            buttonIsDown = new bool[70];

            keyIsDown = new bool[14];

            keyBoardBtns = new List<Region>();

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::::::::: rects :::::::::::::::::::::::::::::::::::
            rects[0] = new Rectangle(10, 160, 690, 380);
            //站点
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    rects[1 + i * 7 + j] = new Rectangle(25 + j * 95, 165 + i * 53, 90, 50);
                }
            }
            //站点设置
            for (int i = 0; i < 8; i++)
            {
                rects[51 + i] = new Rectangle(710, 100 + i * 56, 89, 54);
            }
            //始发站/终点站
            for (int i = 0; i < 2; i++)
            {
                rects[59 + i] = new Rectangle(30 + i * 205, 105, 75, 45);
                rects[61 + i] = new Rectangle(430, 106 + i * 23, 90, 20);
                rects[63 + i] = new Rectangle(105 + i * 205, 105, 130, 45);
                rects[65 + i] = new Rectangle(530, 106 + i * 23, 130, 20);
            }

            //线路选择
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    rects[68 + i * 2 + j] = new Rectangle(100 + i * 100, 160 + j * 38, 100, 35);
                }
            }

            //MoveScreen
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i].X = (rects[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                rects[i].Y = (rects[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
                rects[i].Width *= FormatStyle.Scale;
                rects[i].Height *= FormatStyle.Scale;
            }
            #endregion

            #region :::::::::::::::::::::::::::::::: Rect ::::::::::::::::::::::::::::::::::::::
            for (int i = 0; i < 8; i++)
            {
                Rect.Add(new Region(rects[51 + i]));
            }
            for (int i = 0; i < 4; i++)
            {
                Rect.Add(new Region(rects[63 + i]));
            }
            for (int i = 0; i < 49; i++)
            {
                Rect.Add(new Region(rects[1 + i]));
            }
            #endregion

            #region :::::::::::::::::::::::::::::::: point ::::::::::::::::::::::::::::::::::::::
            pDrawPoint[0] = new Point(705, 95);
            pDrawPoint[1] = new Point(705, 550);
            pDrawPoint[2] = new Point(380, 160);

            //MoveScreen
            for (int i = 0; i < pDrawPoint.Length; i++)
            {
                pDrawPoint[i].X = (pDrawPoint[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
                pDrawPoint[i].Y = (pDrawPoint[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            }
            #endregion

            _keyBoard = new 维护.NumbKeyboard(pDrawPoint[2]);

            for (int i = 0; i < _keyBoard.Rects.Length; i++)
            {
                keyBoardBtns.Add(new Region(_keyBoard.Rects[i]));
            }
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        /// <summary>
        /// 新线路
        /// </summary>
        private string _setLineID = string.Empty;//routeLineID

        /// <summary>
        /// 是否有这条新线路
        /// </summary>
        private bool _setLineRight = false;

        /// <summary>
        /// 是否要显示错误消息
        /// </summary>
        private bool isShowErrorMsg = false;

        /// <summary>
        /// 是否进入线路选择界面
        /// </summary>
        private bool isInLinesSelectView = false;

        /// <summary>
        /// 光标所在位置
        /// -1表示没有选择站点框
        /// 0表示在起点站框
        /// 1表示在终点站框
        /// 2表示始发站和终点站都已经选择完毕，可以按设定按钮
        /// </summary>
        private int cursorID = -1;

        /// <summary>
        /// -1表示未选择起点站，当按下站点后
        /// 将站点代表的按键编号赋给当前变量
        /// </summary>
        private int startStationBtnLock = -1;

        /// <summary>
        /// -1表示未选择终点站，当按下站点后
        /// 将站点代表的按键编号赋给当前变量
        /// </summary>
        private int endStationBtnLock = -1;

        /// <summary>
        /// 判断选站操作是否结束，可以按设定按钮
        /// </summary>
        bool setBtnCanRun = false;

        /// <summary>
        /// 上一次按键编号，与始发站终点站有关
        /// </summary>
        private int lastbtnOfLineID = 1;

        /// <summary>
        /// 当前线路ID
        /// 默认为1
        /// </summary>
        private int currentLineID = 1;

        /// <summary>
        /// 选择运行线路
        /// 默认为1
        /// </summary>
        private int routeLineID = 1;

        /// <summary>
        /// 始发站ID
        /// </summary>
        private int setStartStationID = -1;

        /// <summary>
        /// 始发站名称
        /// </summary>
        private string setStartStationName = string.Empty;

        /// <summary>
        /// 终点站ID
        /// </summary>
        private int setEndStationID = -1;

        /// <summary>
        /// 终点站名称
        /// </summary>
        private string setEndStationName = string.Empty;

        //判断当前读取的文本信息，0为空，1为车站信息，2为线路信息
        int readInfoId = 0;

        //根据右边按钮选择对应的菜单
        StationSettingMenuID _menuID = StationSettingMenuID.Line1;

        /// <summary>
        /// 所有站点名称
        /// </summary>
        private List<string> Stations = new List<string>();

        //1号线站点
        private List<string> _lineID1 = new List<string>();
        //2号线站点
        private List<string> _lineID2 = new List<string>();
        //3号线站点
        private List<string> _lineID3 = new List<string>();
        //6号线站点
        private List<string> _lineID6 = new List<string>();

        //
        private 维护.NumbKeyboard _keyBoard;

        private List<Region> keyBoardBtns;

        private bool[] keyIsDown;
        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::
        /// <summary>
        /// 坐标集
        /// </summary>
        internal PointF[] pDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        internal RectangleF[] rects;

        /// <summary>
        /// 图片集
        /// </summary>
        internal Image[] Img;

        /// <summary>
        /// 键是否按下
        /// </summary>
        internal bool[] buttonIsDown;

        /// <summary>
        /// 按钮是否可以按
        /// </summary>
        private bool[] btnCanDown;

        /// <summary>
        /// 按键列表
        /// </summary>
        internal List<Region> Rect;
        #endregion#
        #endregion
    }

    /// <summary>
    /// 站点设置右排选择菜单
    /// </summary>
     public enum StationSettingMenuID : int
    {
        AutoBroadcast,
        semiAutoBroadcast,
        Line1,
        Line2,
        Line3,
        Line6,
        RouteSelect,
        Back
    }

}
