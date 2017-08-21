using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace KMVT
{
    /// <summary>
    /// 主界面
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class VdObjectes : baseClass
    {
        #region ::::::::::::::::::::: init override 
        public override string GetInfo()
        {
            return "虚拟设备控制类";
        }

       

        public override bool init(ref int nErrorObjectIndex)
        {
            LoadFile();
            return InitData();
        }

        private void LoadFile()
        {
            var file = Path.Combine(RecPath, "..\\config\\操作台元件.txt");
            var allLine = File.ReadAllLines(file, Encoding.Default);
            foreach (var s in allLine)
            {
                AddStringByLine(s);
            }
        }
        //public override bool canSetStringList(int nPara)
        //{
        //    switch (nPara)
        //    {
        //        case 3:
        //            return true;
        //        case 4:
        //            _theObjOrgStaus.Clear();
        //            foreach (var tempInt in _theObjNowStaus.Keys)
        //            {
        //                _theObjOrgStaus.Add(tempInt, _theObjNowStaus[tempInt]);
        //            }
        //            return false;
        //    }
        //    return false;
        //}

        private  void AddStringByLine( string cStr)
        {
            var split = cStr.Split('\t');
            if (split.Length != VdBaseClass.GetUnitCount()) return; //验证参数个数

            //按照配置创建对象
            var tmpVdbc = new VdBaseClass();
            if (!tmpVdbc.InitVdObjectByStrings(ref split, RecPath)) return;

            switch (tmpVdbc.TypeInfo)
            {
                case 1:
                    _theType1List.Add(_theObjParaList.Count);
                    break;
                case 2:
                    _theType2List.Add(_theObjParaList.Count);
                    break;
                default:
                    return;
            }

            //按面板序号分类
            switch (tmpVdbc.MenuID)
            {
                case 0:
                    _theMenu0List.Add(_theObjParaList.Count);
                    break;
                case 1:
                    _theMenu1List.Add(_theObjParaList.Count);
                    break;
                default:
                    return;
            }

            List<int> tmpIndexList;            
            if (!_theObjDict.ContainsKey(tmpVdbc.VdIndex))
            {
                //该控件不存在
                tmpIndexList = new List<int> {_theObjParaList.Count};

                _theObjDict.Add(tmpVdbc.VdIndex, tmpIndexList);

                _theObjList.Add(tmpVdbc.VdIndex);
            }
            else
            {
                tmpIndexList = _theObjDict[tmpVdbc.VdIndex];
                tmpIndexList.Add(_theObjParaList.Count);
            }


            if (tmpVdbc.VdStaus == 1 && tmpVdbc.TypeInfo == 1)
            {
                //将初始状态为1的 放入控件状态中
                if (_theObjNowStaus.ContainsKey(tmpVdbc.VdIndex))
                {
                    _theObjNowStaus[tmpVdbc.VdIndex] = _theObjParaList.Count;
                }
                else
                {
                    _theObjNowStaus.Add(tmpVdbc.VdIndex, _theObjParaList.Count);
                }
            }


            if (!_theObjStausToIndex.ContainsKey(tmpVdbc.GetNameAndStaus()))
            {
                _theObjStausToIndex.Add(tmpVdbc.GetNameAndStaus(), _theObjParaList.Count);
            }

            tmpVdbc.ListIndex = _theObjParaList.Count;

            _theObjParaList.Add(tmpVdbc);
        }

        #endregion

        #region ::::::::::::::::::::: event override
        public override void paint(Graphics dcGs)
        {
            DrawObjStaus(dcGs);
            base.paint(dcGs);
        }

        int _theTempDownForIndex;

        /// <summary>
        /// 当前点击的控件元件号
        /// </summary>
        int _theTempDownObjIndex;

        VdBaseClass _theTempDownObj;
        int _theTempDownObjFindIndex;
        bool _isMouseDowning;
        int _mouseDowningObjIndex;

        public override bool mouseDown(Point nPoint)
        {
            _isMouseDowning = true;
            
            var index = 0;
            for (; index < _regsList.Count; ++index)
            {
                if (_regsList[index].IsVisible(nPoint))
                    break;
            }
            switch (index)
            {
                case 0:
                    _menuIndex = 0;
                    _btnIsDown[0] = true;
                    _btnIsDown[1] = false;
                    break;
                case 1:
                    _menuIndex = 1;
                    _btnIsDown[0] = false;
                    _btnIsDown[1] = true;
                    break;
            }

            //循环 状态1 的控件
            for (_theTempDownForIndex = 0; _theTempDownForIndex < _theObjList.Count; _theTempDownForIndex++)
            {
                _theTempDownObjIndex = _theObjList[_theTempDownForIndex];

                if (!_theObjNowStaus.ContainsKey(_theTempDownObjIndex)) continue;

                if (_theObjNowStaus[_theTempDownObjIndex] >= _theObjParaList.Count ||
                    _theObjNowStaus[_theTempDownObjIndex] < 0)
                    continue;

                _theTempDownObj = _theObjParaList[_theObjNowStaus[_theTempDownObjIndex]];

                //down obj 是否在 返回内
                var tmpRegionA = new Region(_theTempDownObj.VdHotAreaInfoF.VdHotAreaInfo);
                if (tmpRegionA.IsVisible(nPoint) && _theTempDownObj.MenuID == _menuIndex)
                {
                    if (_theObjDict.ContainsKey(_theTempDownObj.VdIndex))
                    {
                        var tempListA = _theObjDict[_theTempDownObj.VdIndex];
                        if (tempListA == null) continue;

                        for (_theTempDownObjFindIndex = 0; _theTempDownObjFindIndex < tempListA.Count; _theTempDownObjFindIndex++)
                        {
                            VdBaseClass theTempDownObjA = _theObjParaList[tempListA[_theTempDownObjFindIndex]];
                            if (theTempDownObjA == null ||
                                theTempDownObjA.VdStaus != _theTempDownObj.VdHotAreaInfoF.VdJumpIndex ||
                                !_theObjNowStaus.ContainsKey(_theTempDownObj.VdIndex)) continue;

                            _theObjNowStaus[_theTempDownObj.VdIndex] = theTempDownObjA.ListIndex; //
                            _mouseDowningObjIndex = theTempDownObjA.VdIndex;
                            break;
                        }
                    }
                    break;
                }

                var tmpRegionB = new Region(_theTempDownObj.VdHotAreaInfoB.VdHotAreaInfo);
                if (!tmpRegionB.IsVisible(nPoint) || _theTempDownObj.MenuID != _menuIndex) continue;
                if (_theObjDict.ContainsKey(_theTempDownObj.VdIndex))
                {
                    var tempListB = _theObjDict[_theTempDownObj.VdIndex];
                    if (tempListB == null) continue;

                    for (_theTempDownObjFindIndex = 0; _theTempDownObjFindIndex < tempListB.Count; _theTempDownObjFindIndex++)
                    {
                        VdBaseClass theTempDownObjB = _theObjParaList[tempListB[_theTempDownObjFindIndex]];
                        if (theTempDownObjB == null ||
                            theTempDownObjB.VdStaus != _theTempDownObj.VdHotAreaInfoB.VdJumpIndex ||
                            !_theObjNowStaus.ContainsKey(_theTempDownObj.VdIndex)) continue;

                        _theObjNowStaus[_theTempDownObj.VdIndex] = theTempDownObjB.ListIndex; //
                        _mouseDowningObjIndex = theTempDownObjB.VdIndex;
                        break;
                    }
                }
                break;
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _isMouseDowning = false;
            return base.mouseUp(nPoint);
        }
        #endregion

        #region ::::::::::::::::::::: ex funes
        int _timeCount;
        private bool TimeTest(int numb)
        {
            if (_timeCount > numb)
            {
                _timeCount = 0;
                return true;
            }
            _timeCount++;
            return false;
        }

        int _theTempObjForIndex;
        int _theTempObjIndex;
        VdBaseClass _theTempObj;

        /// <summary>
        /// 按照控件状态绘制控件
        /// </summary>
        /// <param name="g"></param>
        private void DrawObjStaus(Graphics g)
        {
            g.DrawImage(_imgArr[0], _rectsArr[0]);
            if (_btnIsDown[0])
                g.DrawImage(_imgArr[1], _rectsArr[1]);
            else if (_btnIsDown[1])
                g.DrawImage(_imgArr[2], _rectsArr[1]);

            if (_menuIndex == 1)
            {
                g.FillRectangle(_whiteBrush, _rectsArr[94]);
            }

            #region 发送数据|画图
            for (_theTempObjForIndex = 0; _theTempObjForIndex < _theObjList.Count; _theTempObjForIndex++)
            {
                _theTempObjIndex = _theObjList[_theTempObjForIndex];
                if (!_theObjNowStaus.ContainsKey(_theTempObjIndex)) continue;

                if (_theObjNowStaus[_theTempObjIndex] >= _theObjParaList.Count ||
                    _theObjNowStaus[_theTempObjIndex] < 0)
                    continue;

                _theTempObj = _theObjParaList[_theObjNowStaus[_theTempObjIndex]];
                if (_theTempObj == null) continue;

                if (_theTempObj.TypeInfo == 1)
                {
                    //按元件设置状态
                    SetBoolValueByObj(ref _theTempObj);

                    //倒计时判断
                    if (_theTempObj.VdJumpToIndex > 0 && (--_theTempObj.VdJumpTime) < 0)
                    {
                        //需要跳转
                        _theTempObj.VdJumpTime = 5;      //复原计数值
                        if (_theObjStausToIndex.ContainsKey(_theTempObj.GetNameAndStaus()))
                        {
                            _theObjNowStaus[_theTempObjIndex] = _theObjStausToIndex[_theTempObj.VdIndex
                                + "_" + _theTempObj.VdJumpToIndex];
                        }
                    }

                    if (_theTempObj.CatchTrip.Count > 0 && BoolList[_theTempObj.CatchTrip[0]])
                    {
                        if (TimeTest(1))
                        {
                            _theObjNowStaus[_theTempObjIndex] = _theObjStausToIndex[_theTempObj.VdIndex
                            + "_" + _theTempObj.TripJumpToIndex];
                        }
                    }

                    if (_theTempObj.MenuID == _menuIndex)
                        g.DrawImage(_theTempObj.PicImage, _theTempObj.VdLocPoint);
                }

                #region TypeInfo == 2 暂时不用
                //if (theObjDict.ContainsKey(theTempObj.VdIndex))
                //{
                //    theTempForeachObjList = theObjDict[theTempObj.VdIndex];
                //    for (theTempForeachObjIndex = 0; theTempForeachObjIndex < theTempForeachObjList.Count; theTempForeachObjIndex++)
                //    {
                //        theTempObjB = theObjParaList[theTempForeachObjList[theTempForeachObjIndex]];
                //        if (theTempObjB == null) continue;

                //        if (theTempObjB.TypeInfo == 2 && 
                //            ((isMouseDowning == false && MouseDowningObjIndex == theTempObjB.VdIndex) ||
                //            (MouseDowningObjIndex != theTempObjB.VdIndex)
                //            ))
                //            foreach (int tempIntC in theTempObjB.VdHotAreaInfo_F.VdSendData.Keys)
                //            {
                //                if (this.BoolList.Count > tempIntC &&
                //                    this.BoolList[tempIntC] == theTempObjB.VdHotAreaInfo_F.VdSendData[tempIntC])
                //                {
                //                    g.DrawImage(theTempObjB.PicImage, theTempObjB.VdLocPoint);
                //                }
                //            }
                //    }
                //}
                #endregion

                //框线
                //if (theTempObj.MenuID == menuIndex)
                //{
                //    g.DrawRectangle(new Pen(menuIndex == 0 ? Color.White : Color.Black, 1),
                //                theTempObj.VdHotAreaInfo_F.VdHotAreaInfo.X,
                //                theTempObj.VdHotAreaInfo_F.VdHotAreaInfo.Y,
                //                theTempObj.VdHotAreaInfo_F.VdHotAreaInfo.Width,
                //                theTempObj.VdHotAreaInfo_F.VdHotAreaInfo.Height);
                //    g.DrawRectangle(new Pen(menuIndex == 0 ? Color.White : Color.Black, 1),
                //                    theTempObj.VdHotAreaInfo_B.VdHotAreaInfo.X,
                //                    theTempObj.VdHotAreaInfo_B.VdHotAreaInfo.Y,
                //                    theTempObj.VdHotAreaInfo_B.VdHotAreaInfo.Width,
                //                    theTempObj.VdHotAreaInfo_B.VdHotAreaInfo.Height);
                //}
            }
            #endregion

            //for (int i = 0; i < 54; i++)
            //{
            //    g.DrawRectangle(new Pen(Color.Black), rectsArr[95 + i].X, rectsArr[95 + i].Y, rectsArr[95 + i].Width, rectsArr[95 + i].Height);
            //}
            if (_menuIndex == 0)
            {
                for (var i = 0; i < 90; i++)
                {
                    g.FillRectangle(_whiteBrush, _rectsArr[4 + i]);
                }

                for (var i = 0; i < 45; i++)
                {
                    g.DrawString(_strUp[i], _font10, _blackBrush, _rectsArr[4 + i], _drawFormat);
                    g.DrawString(_strDown[i], _font10, i == 44 ? _redBrush : _blackBrush, _rectsArr[49 + i].X + 10, _rectsArr[49 + i].Y, new StringFormat(StringFormatFlags.DirectionVertical));
                }
            }

            if (_menuIndex == 1)
            {
                for (int i = 0; i < 18; i++)
                {
                    g.DrawString(_strXnUp[i], _font10, _blackBrush, _rectsArr[95 + i], _drawFormat);
                    g.DrawString(_strAa[i], _font10, _blackBrush, _rectsArr[113 + i], _drawFormat);
                    g.DrawString(_strXnDown[i], _font10, _blackBrush, _rectsArr[131 + i], _drawFormat);
                }
            }

        }

        private void SetBoolValueByObj(ref VdBaseClass refVDBC)
        {
            if (refVDBC.VdHotAreaInfoF.VdSendData.Count <= 0) return;

            foreach (int tempInt in refVDBC.VdHotAreaInfoF.VdSendData.Keys)
            {
                append_postCmd(CmdType.SetBoolValue, Math.Abs(tempInt), refVDBC.VdHotAreaInfoF.VdSendData[tempInt] ? 1 : 0, 0);
            }
        }
        #endregion

        #region ::::::::::::::::::::: init funes
        /// <summary>
        /// 初始化坐标、数组、列表、热区
        /// </summary>
        private bool InitData()
        {
            _drawFormat.LineAlignment = StringAlignment.Center;
            _drawFormat.Alignment = StringAlignment.Center;

            _rightFormat.LineAlignment = StringAlignment.Far;
            _rightFormat.Alignment = StringAlignment.Center;

            _leftFormat.LineAlignment = StringAlignment.Near;
            _leftFormat.Alignment = StringAlignment.Center;

            _pDrawArr = new PointF[20];

            _rectsArr = new RectangleF[250];

            _imgArr = new Image[35];

            _btnIsDown = new[] { true, false};

            _btnCanDown = new[] { true, true, true, true, true, true, true, true, true, true };

            _regsList = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                _imgArr[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::::::::: rects :::::::::::::::::::::::::::::::::::
            //背景图
            _rectsArr[0] = new RectangleF(0, 0, 1024, 600);

            //面板按钮条
            _rectsArr[1] = new RectangleF(0, 0, 55, 250);

            //面板按钮
            for (int i = 0; i < 2; i++ )
            {
                _rectsArr[2 + i] = new RectangleF(0, 52 + 98 * i, 55, 94);
            }

            //面板0
            for (int i = 0; i < 24; i++)
            {
                _rectsArr[4 + i] = new RectangleF(61 + i * 40, 0, 38, 28);
                _rectsArr[49 + i] = new RectangleF(61 + i * 40, 138, 38, 115);
            }
            for (int i = 0; i < 21; i++)
            {
                _rectsArr[28 + i] = new RectangleF(61 + i * 40, 270, 38, 30);
                _rectsArr[73 + i] = new RectangleF(61 + i * 40, 408, 38, 115);
            }

            //面板1
            _rectsArr[94] = new RectangleF(60, 10, 900, 325);

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    _rectsArr[95 + i * 9 + j] = new RectangleF(60 + j * 100, 10 + i * 165, 100, 20);
                    _rectsArr[113 + i * 9 + j] = new RectangleF(60 + j * 100, 28 + i * 165, 100, 15);
                    _rectsArr[131 + i * 9 + j] = new RectangleF(60 + j * 100, 130 + i * 165, 100, 35);
                }
            }

            //MoveScreen
            //for (int i = 0; i < rectsArr.Length; i++)
            //{
            //    rectsArr[i].X = (rectsArr[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
            //    rectsArr[i].Y = (rectsArr[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            //    rectsArr[i].Width *= FormatStyle.Scale;
            //    rectsArr[i].Height *= FormatStyle.Scale;
            //}
            #endregion

            #region :::::::::::::::::::::::::::::::: point :::::::::::::::::::::::::::::::::


            for (int i = 0; i < 6; i++)
            {
                //制动缸压力
                _pDrawArr[i * 2] = new PointF(173 + i * 95, 402);
                _pDrawArr[1 + i * 2] = new PointF(173 + i * 95, 431);
            }

            //MoveScreen
            //for (int i = 0; i < pDrawArr.Length; i++)
            //{
            //    pDrawArr[i].X = (pDrawArr[i].X + FormatStyle.ScreenMoveX) * FormatStyle.Scale;
            //    pDrawArr[i].Y = (pDrawArr[i].Y + FormatStyle.ScreenMoveY) * FormatStyle.Scale;
            //}
            #endregion

            #region :::::::::::::::::::::::::::::::: Rect ::::::::::::::::::::::::::::::::::
            for (int i = 0; i < 9; i++)
            {
                _regsList.Add(new Region(_rectsArr[2 + i]));
            }
            #endregion
            return true;
        }
        #endregion

        #region ::::::::::::::::::::: init Vars

        private readonly StringFormat _drawFormat = new StringFormat();
        private readonly StringFormat _rightFormat = new StringFormat();
        private readonly StringFormat _leftFormat = new StringFormat();

        private readonly Font _font10 = new Font("宋体", 10f);

        /// <summary>
        /// 白色
        /// </summary>
        private readonly SolidBrush _whiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));

        /// <summary>
        /// 黑色
        /// </summary>
        private readonly SolidBrush _blackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));

        private readonly SolidBrush _redBrush = new SolidBrush(Color.Red);

        /// <summary>
        /// 存储所有的虚拟设备对象
        /// </summary>
        private readonly List<VdBaseClass> _theObjParaList = new List<VdBaseClass>(150);

        /// <summary>
        /// 控件类型为1的索引表
        /// </summary>
        private readonly List<int> _theType1List = new List<int>(150);

        /// <summary>
        /// 控件类型为2的索引表
        /// </summary>
        private readonly List<int> _theType2List = new List<int>(150);

        /// <summary>
        /// 面板序号为0的索引表
        /// </summary>
        private readonly List<int> _theMenu0List = new List<int>(150);

        /// <summary>
        /// 面板序号为1的索引表
        /// </summary>
        private readonly List<int> _theMenu1List = new List<int>(150);

        /// <summary>
        /// 控件元件号Index队列
        /// </summary>
        private readonly List<int> _theObjList = new List<int>(150);

        /// <summary>
        /// 按控件元件号进行的分类
        /// </summary>
        private readonly Dictionary<int, List<int>> _theObjDict = new Dictionary<int, List<int>>();

        /// <summary>
        /// 存储当前控件状态
        /// </summary>
        private readonly Dictionary<int, int> _theObjNowStaus = new Dictionary<int, int>();

        /// <summary>
        /// 控件初始状态
        /// </summary>
        private readonly Dictionary<int, int> _theObjOrgStaus = new Dictionary<int, int>();

        /// <summary>
        /// 控件编号_状态 对应的 theObjParaList 位置
        /// </summary>
        private readonly Dictionary<string, int> _theObjStausToIndex = new Dictionary<string, int>();

        /// <summary>
        /// 按钮原始参数信息
        /// </summary>
        private readonly List<string> _buttonList = new List<string>();

        /// <summary>
        /// 面板序号
        /// </summary>
        private int _menuIndex;

        private readonly string[] _strUp =
        {
            "=21-\nF101", "=22-\nF101","=27-\nF101",
            "=44-\nF101", "=51-\nF101","=52-\nF101",
            "=52-\nF104", "=52-\nF105","=61-\nF101",
            "=61-\nF102", "=61-\nF103","=73-\nF101",
            "=73-\nF102", "=73-\nF103","=73-\nF104",
            "=81-\nF101", "=81-\nF111","=81-\nF112",
            "=82-\nF101", "=82-\nF102","=82-\nF103",
            "=32-\nF105", "","",
            "=23-\nF101", "=28-\nF101","=28-\nF102",
            "=31-\nF107", "=41-\nF101","=41-\nF102",
            "=41-\nF104", "=42-\nF101","=45-\nF101",
            "=46-\nF101", "=45-\nF102","=46-\nF105",
            "=91-\nF101", "=91-\nF104","=91-\nF105",
            "=91-\nF106", "=91-\nF107","=91-\nF108",
            "=91-\nF109", "=91-\nF110", "" 
        };
        private readonly string[] _strDown =
        {
            "HSCB控制", "列车控制", "停放制动",
            "无线电主机", "外部照明", "司机室内部照明",
            "TC车列车左侧照明", "TC车列车右侧照明", "TC车紧急通风",
            "空调列车控制", "TC车空调控制", "司机室辅助设备",
            "玻璃加热", "轮缘润滑", "刮雨器",
            "车门控制", "左门开", "右门开",
            "门控单元1、3、5", "门控单元2、4、6", "门控单元7、8",
            "永久负载", "", "",
            "VCM", "网关阀", "智能阀",
            "辅助逆变器", "TC车I/O", "Repeater",
            "MMI", "火灾报警", "TC车PIS系统控制器",
            "LCD", "LED显示器", "车地无线设备",
            "ATC列车控制", "CORE MPC", "VIOM1",
            "VIOM2", "VIOM3", "USW1", 
            "USW2", "ATC desk", "B09阀全部切除" };

        private readonly string[] _strXnUp =
        {
            "=34-S102", "=81-S112", "=91-S10",
            "=72-S107", "=22-S120", "=27-S104",
            "=27-S103", "=81-S109", "=81-S110",
            "=31-S104", "=34-S101", "=22-S13",
            "=21-S01", "=22-S08", "=41-S101",
            "=91-S114", "=91-S105", "=72-S101"
        };

        private readonly string[] _strXnDown =
        { 
            "总风压力\n可用旁路", "开关门\n控制切换", "车门使能\n旁路",
            "车钩监视\n旁路", "警惕旁路", "所有制动\n缓解旁路",
            "停放缓解\n旁路", "门零速\n旁路", "门关好\n旁路",
            "充电机\n应急启动", "强迫泵风", "慢行拖动\n模式",
            "BHB切除", "紧急牵引", "MVB复位",
            "ATC复位", "ATC切除", "列车激活" 
        };

        private readonly string[] _strAa =
        {
            "分         合", "硬盘     网络", "分         合",
            "分         合", "分         合", "分         合",
            "分         合", "分         合", "分         合",
            "", "", "慢行     拖动",
            "分         合", "分         合", "",
            "", "分         合", "分         合" 
        };
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        /// <summary>
        /// 坐标集数组
        /// </summary>
        private PointF[] _pDrawArr;

        /// <summary>
        /// 坐标集列表
        /// </summary>
        private List<PointF> _pDrawList;

        /// <summary>
        /// 矩形框集
        /// </summary>
        private RectangleF[] _rectsArr;

        /// <summary>
        /// 矩形框集列表
        /// </summary>
        private List<RectangleF> _rectsList;

        /// <summary>
        /// 按键列表
        /// </summary>
        private List<Region> _regsList;

        /// <summary>
        /// 图片集
        /// </summary>
        private Image[] _imgArr;

        /// <summary>
        /// 键是否按下状态
        /// </summary>
        private bool[] _btnIsDown;

        /// <summary>
        /// 键是否能按下
        /// </summary>
        private bool[] _btnCanDown;

        #endregion
    }
}
