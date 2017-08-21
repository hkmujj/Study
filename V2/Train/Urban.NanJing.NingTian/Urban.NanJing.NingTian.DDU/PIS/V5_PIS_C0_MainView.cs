#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-25
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图5-PIS-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using ES.Facility.PublicModule.Memo;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.NanJing.NingTian.DDU.Common;

namespace Urban.NanJing.NingTian.DDU.PIS
{
    /// <summary>
    /// 功能描述：站台信息
    /// 创建人：唐林
    /// 创建时间：2014-07-25
    /// </summary>
    public class Station
    {
        /// <summary>
        /// 读取或设置站台编号属性
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 读取或设置站台名称属性
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 功能描述：视图5-PIS-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-25
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V5_PIS_C0_MainView : baseClass
    {
        #region 枚举
        /// <summary>
        /// PIS模式
        /// </summary>
        public enum PISMode
        {
            /// <summary>
            /// 手动模式
            /// </summary>
            Hand = 0,
            /// <summary>
            /// 半自动模式
            /// </summary>
            HalfAuto = 1,
            /// <summary>
            /// 自动模式
            /// </summary>
            Auto = 2
        }

        /// <summary>
        /// 区间号
        /// </summary>
        public enum PISInterval
        {
            /// <summary>
            /// 上行
            /// </summary>
            Front = 0,
            /// <summary>
            /// 下行
            /// </summary>
            Back = 1,
        }

        /// <summary>
        /// 站台设置
        /// </summary>
        public enum StationSet
        {
            /// <summary>
            /// 第一站
            /// </summary>
            Start = 0,
            /// <summary>
            /// 终点站
            /// </summary>
            Destination = 1,
            /// <summary>
            /// 下一站
            /// </summary>
            Next = 2
        }

        /// <summary>
        /// 自复按钮
        /// </summary>
        public enum ButtonFlag
        {
            /// <summary>
            /// 确认按钮
            /// </summary>
            OK = 0,
            /// <summary>
            /// 挑站
            /// </summary>
            JumpStation = 1,
            /// <summary>
            /// 预报站
            /// </summary>
            ForecastStation = 2,
            /// <summary>
            /// 到站广播
            /// </summary>
            ArriveBoard = 3,
            /// <summary>
            /// 紧急广播
            /// </summary>
            UrgencyBoard = 4,
            /// <summary>
            /// 出发确认
            /// </summary>
            LeaveOK = 5,
            /// <summary>
            /// 向上选择
            /// </summary>
            Up = 6,
            /// <summary>
            /// 向下选择
            /// </summary>
            Down = 7
        }
        #endregion

        #region 对外接口
        /// <summary>
        /// 当前站台列表（用于同步当前上下行）
        /// </summary>
        public static List<Station> CurrentListStation = new List<Station>();
        #endregion

        #region 稀有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        private int _readInfoId = 0;//读取文本标志
        private List<List<Station>> _list_Stations = new List<List<Station>>();//站点列表
        private bool _isFirstSet = true;//是否第一次设置
        private List<TextBox> _textBoxs_Station_F_T = new List<TextBox>();//文本输入框列表

        private List<Button> _btns_Mode = new List<Button>();//模式按钮列表
        public static PISMode _currentMode = PISMode.Hand;//当前PIS模式

        private List<Button> _btns_Interval = new List<Button>();//区间号按钮列表
        public static PISInterval _currentInterval = PISInterval.Front;//当前区间号

        private List<Button> _btns_StationSetting = new List<Button>();//站点设置按钮列表
        private StationSet _currentStationSetting = StationSet.Start;//当前设置站点

        private List<Button> _btns = new List<Button>();//功能按钮
        private ListBox<Station> _listBox;

        private List<TextBox> _textBoxs = new List<TextBox>();//文本框列表


        private int _currentStartStationID = 0;
        private int _currentEndStationID = 0;
        private int _currentNextStationID = 0;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "PIS（站点设置）试图-站点设置";
        }

        /// <summary>
        /// 初始化函数：导入图片、创建控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            LoadLineInfo();

            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            //模式按钮列表
            string[] strs_1 = new string[] { "手动", "半自动", "自动" };
            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button(
                    strs_1[i],
                    new RectangleF(21, 150 + 66 * i, 138, 64),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES() { Font = new Font("宋体", 18), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SF_CC },
                        Background = _resource_Image[1],
                        DownImage = _resource_Image[2]
                    },
                    false
                    );
                btn.ClickEvent += btn_Mode_ClickEvent;
                _btns_Mode.Add(btn);
            }
            _btns_Mode[0].IsReplication = false;
            //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 4800, 1, 0);
            //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1] + 4800, 0, 0);
            //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2] + 4800, 0, 0);

            //模式按钮列表
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    "",
                    new RectangleF(182, 150 + 109 * i, 138, 87),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES() { Font = new Font("宋体", 18), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SF_CC },
                        Background = _resource_Image[3],
                        DownImage = _resource_Image[4],
                        DisableImage = _resource_Image[7]
                    },
                    false
                    );
                btn.ClickEvent += btn_Interval_ClickEvent;
                _btns_Interval.Add(btn);
            }
            _btns_Interval[0].IsReplication = false;
            //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[3] + 4800, 1, 0);
            //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[4] + 4800, 0, 0);

            string[] strs_2 = new string[] { "起始站", "终点站", "下一站" };
            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button(
                     strs_2[i],
                     new RectangleF(510, 148 + 59 * i, 139, 56),
                     i,
                     new ButtonStyle()
                     {
                         FontStyle = new FontStyle_ES() { Font = new Font("宋体", 18), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SF_CC },
                         Background = _resource_Image[5],
                         DownImage = _resource_Image[6],
                         DisableImage = _resource_Image[7]
                     },
                     false
                     );
                btn.ClickEvent += btn_StationSetting_ClickEvent;
                _btns_StationSetting.Add(btn);
            }
            _btns_StationSetting[0].IsReplication = false;

            //功能键
            string[] strs_3 = new string[] { "确认", "跳站", "预报站", "到站广播", "紧急广播", "出发确认" };
            for (int i = 0; i < 6; i++)
            {
                Button btn = new Button(
                     strs_3[i],
                     new RectangleF(510 + (i / 3) * 142, 148 + 59 * (i % 3 + 3), 139, 56),
                     i,
                     new ButtonStyle()
                     {
                         FontStyle = new FontStyle_ES() { Font = new Font("宋体", 18), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SF_CC },
                         Background = _resource_Image[5],
                         DownImage = _resource_Image[6],
                         DisableImage = _resource_Image[7]
                     }
                     );
                btn.ClickEvent += btn_ClickEvent;
                btn.MouseDownEvent += btn_MouseDownEvent;
                _btns.Add(btn);
            }
            _btns[1].Enable = false;

            //上下选择站台按钮
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                     "",
                     new RectangleF(269, 362 + 85 * i, 64, 64),
                     i + 6,
                     new ButtonStyle()
                     {
                         FontStyle = new FontStyle_ES() { Font = new Font("宋体", 18), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SF_CC },
                         Background = _resource_Image[8 + 0 + 3 * i],
                         DownImage = _resource_Image[8 + 1 + 3 * i],
                         DisableImage = _resource_Image[8 + 2 + 3 * i]
                     }
                     );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            //站台列表
            _listBox = new ListBox<Station>(new RectangleF(345, 115, 161, 413), CurrentListStation,
                new ListBoxHeader()
                {
                    Text = "站台名称",
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("黑体", 11),
                    DataFont = new Font("宋体", 11, FontStyle.Bold),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 25,
                    Width = 161,
                    TProperty = "Name",
                    SF_Data = FontInfo.SF_CC,
                    SF_Header = FontInfo.SF_CC
                }
                );
            _listBox.RowCount = 17;
            _listBox.BackgroundBrush = Brushes.White;
            _listBox.GridBrush = Brushes.Black;
            _listBox.HideHeader = true;

            //文本框（显示选择的站台）
            for (int i = 0; i < 3; i++)
            {
                TextBox textBox = new TextBox(
                    new RectangleF(651, 150 + 59 * i, 140, 52),
                    new TextBoxStyle()
                    {
                        FontStyle = new FontStyle_ES() { Font = new Font("宋体", 15, FontStyle.Bold), StringFormat = FontInfo.SF_CC, TextBrush = (SolidBrush)Brushes.Black },
                        Background = _resource_Image[14]
                    },
                    i);
                _textBoxs.Add(textBox);
            }

            return true;
        }

        private void LoadLineInfo()
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
                        List<string> list = (outTmp.Split('-')).ToList();
                        List<Station> list_ = new List<Station>();
                        for (int i = 0; i < list.Count; i++)
                        {
                            list_.Add(new Station() {ID = i, Name = list[i]});
                        }
                        _list_Stations.Add(list_);
                    }
                }
            }
        }

        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            _btns_Mode.ForEach(a => a.MouseDown(nPoint));
            _btns_Interval.ForEach(a => a.MouseDown(nPoint));
            _btns_StationSetting.ForEach(a => a.MouseDown(nPoint));
            _btns.ForEach(a => a.MouseDown(nPoint));
            _listBox.MouseDown(nPoint);
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btns_Mode.ForEach(a => a.MouseUp(nPoint));
            _btns_Interval.ForEach(a => a.MouseUp(nPoint));
            _btns_StationSetting.ForEach(a => a.MouseUp(nPoint));
            _btns.ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 右侧菜单按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_Mode_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if ((int)_currentMode == e.Message)
            {
                _btns_Mode.Find(a => a.ID == e.Message).IsReplication = false;
                return;
            }

            _btns_Mode.Find(a => a.ID == e.Message).IsReplication = false;
            _btns_Mode.Find(a => a.ID == (int)_currentMode).IsReplication = true;

            switch ((PISMode)e.Message)
            {
                case PISMode.Hand://手动模式按钮
                    _currentMode = PISMode.Hand;

                    _btns_Interval.ForEach(a => a.Enable = true);
                    _btns_Interval[(int)_currentInterval].IsReplication = false;

                    _btns_StationSetting.ForEach(a => a.Enable = true);
                    _btns.ForEach(a => a.Enable = true);
                    _btns.Find(a => ((ButtonFlag)a.ID) == ButtonFlag.JumpStation).Enable = false;
                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 4800, 1, 0);
                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1] + 4800, 0, 0);
                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2] + 4800, 0, 0);
                    break;
                case PISMode.HalfAuto://半自动
                    _currentMode = PISMode.HalfAuto;

                    _btns_Interval.ForEach(a => a.Enable = true);
                    _btns_Interval[(int)_currentInterval].IsReplication = false;

                    _btns_StationSetting.ForEach(a => a.Enable = true);
                    _btns_StationSetting.Find(a => ((StationSet)a.ID) == StationSet.Next).Enable = false;
                    _btns.ForEach(a => a.Enable = true);
                    _btns.FindAll(a => ((ButtonFlag)a.ID) == ButtonFlag.ArriveBoard || ((ButtonFlag)a.ID) == ButtonFlag.ForecastStation).ForEach(b => b.Enable = false);
                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 4800, 0, 0);
                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1] + 4800, 1, 0);
                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2] + 4800, 0, 0);
                    break;
                case PISMode.Auto://自动
                    _currentMode = PISMode.Auto;
                    _btns_Interval.ForEach(a => a.Enable = false);
                    _btns_StationSetting.ForEach(a => a.Enable = false);
                    _btns.ForEach(a => a.Enable = false);
                    _btns.FindAll(a => ((ButtonFlag)a.ID) == ButtonFlag.UrgencyBoard || ((ButtonFlag)a.ID) == ButtonFlag.LeaveOK).ForEach(b => b.Enable = true);
                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 4800, 0, 0);
                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1] + 4800, 0, 0);
                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2] + 4800, 1, 0);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 右侧菜单按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_Interval_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if ((int)_currentInterval == e.Message)
            {
                _btns_Interval.Find(a => a.ID == e.Message).IsReplication = false;
                return;
            }

            _btns_Interval.Find(a => a.ID == e.Message).IsReplication = false;
            _btns_Interval.Find(a => a.ID == (int)_currentInterval).IsReplication = true;

            switch ((PISInterval)e.Message)
            {
                case PISInterval.Front://线路1
                    _currentInterval = PISInterval.Front;
                    //this._listBox.DataList = this._list_Stations[0];
                    CurrentListStation.Clear();
                    _list_Stations[0].ForEach(a => CurrentListStation.Add(a));

                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[3] + 4800, 1, 0);
                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[4] + 4800, 0, 0);
                    break;
                case PISInterval.Back://线路2
                    _currentInterval = PISInterval.Back;
                    //this._listBox.DataList = this._list_Stations[1];
                    CurrentListStation.Clear();
                    _list_Stations[1].ForEach(a => CurrentListStation.Add(a));

                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[3] + 4800, 0, 0);
                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[4] + 4800, 1, 0);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 站台选择菜单按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_StationSetting_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if ((int)_currentStationSetting == e.Message)
            {
                _btns_StationSetting.Find(a => a.ID == e.Message).IsReplication = false;
                return;
            }

            _btns_StationSetting.Find(a => a.ID == e.Message).IsReplication = false;
            _btns_StationSetting.Find(a => a.ID == (int)_currentStationSetting).IsReplication = true;

            switch ((StationSet)e.Message)
            {
                case StationSet.Start://起始站
                    _currentStationSetting = StationSet.Start;
                    ////根据选择的站台选择功能键获取到目前有效的TextBox，直接赋值其text属性
                    //TextBox tb = this._textBoxs.Find(a => a.ID == this._btns_StationSetting.FindIndex(b => b.IsReplication == false));
                    //if (tb != null)
                    //{
                    //    Station s = this._list_Stations[(int)_currentInterval].Find(a => a.Name == tb.Text);
                    //    if (s != null)
                    //    {
                    //        this._listBox.SelectedIndex = s.ID;
                    //    }
                    //    else

                    //        tb.Text = this._listBox.SelectedItem.Name;
                    //}
                    break;
                case StationSet.Destination://终点站
                    _currentStationSetting = StationSet.Destination;

                    ////根据选择的站台选择功能键获取到目前有效的TextBox，直接赋值其text属性
                    //TextBox tb1 = this._textBoxs.Find(a => a.ID == this._btns_StationSetting.FindIndex(b => b.IsReplication == false));
                    //if (tb1 != null)
                    //{
                    //    Station s = this._list_Stations[(int)_currentInterval].Find(a => a.Name == tb1.Text);
                    //    if (s != null)
                    //    {
                    //        this._listBox.SelectedIndex = s.ID;
                    //    }
                    //    else

                    //        tb1.Text = this._listBox.SelectedItem.Name;
                    //}

                    break;
                case StationSet.Next://下一站
                    _currentStationSetting = StationSet.Next;

                    ////根据选择的站台选择功能键获取到目前有效的TextBox，直接赋值其text属性
                    //TextBox tb2 = this._textBoxs.Find(a => a.ID == this._btns_StationSetting.FindIndex(b => b.IsReplication == false));
                    //if (tb2 != null)
                    //{
                    //    Station s = this._list_Stations[(int)_currentInterval].Find(a => a.Name == tb2.Text);
                    //    if (s != null)
                    //    {
                    //        this._listBox.SelectedIndex = s.ID;
                    //    }
                    //    else

                    //        tb2.Text = this._listBox.SelectedItem.Name;
                    //}

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 自复按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch ((ButtonFlag)e.Message)
            {
                case ButtonFlag.Up://向上选择
                    _listBox.LastItem();
                    break;
                case ButtonFlag.Down://向下选择
                    _listBox.NextItem();
                    break;
                case ButtonFlag.OK://确认按钮
                    int startStationID = 0, endStationID = 0, nextStationID = 0;
                    if (_list_Stations[0].Find(a => a.Name == _textBoxs[0].Text) !=
                        null)
                    {
                        startStationID =
                            _list_Stations[0].Find(a => a.Name == _textBoxs[0].Text).ID + 1;
                    }

                    if (_list_Stations[0].Find(a => a.Name == _textBoxs[1].Text) !=
                        null)
                    {
                        endStationID =
                            _list_Stations[0].Find(a => a.Name == _textBoxs[1].Text).ID + 1;
                    }

                    if (_list_Stations[0].Find(a => a.Name == _textBoxs[2].Text) !=
                        null)
                    {
                        nextStationID =
                            _list_Stations[0].Find(a => a.Name == _textBoxs[2].Text).ID + 1;
                    }
                    append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0] + 600, 0, startStationID);
                    append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[1] + 600, 0, endStationID);
                    append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[2] + 600, 0, nextStationID);
                    //向逻辑发生站台信息
                    break;
                case ButtonFlag.ForecastStation://预报站
                    break;
                case ButtonFlag.ArriveBoard://到站广播
                    break;
                case ButtonFlag.UrgencyBoard://紧急广播
                    break;
                case ButtonFlag.LeaveOK://出发确认
                    int startStationID1 = 0, endStationID1 = 0, nextStationID1 = 0;

                    if (_list_Stations[0].Find(a => a.Name == _textBoxs[0].Text) !=
                        null)
                    {
                        startStationID1 =
                            _list_Stations[0].Find(a => a.Name == _textBoxs[0].Text).ID + 1;
                    }

                    if (_list_Stations[0].Find(a => a.Name == _textBoxs[1].Text) !=
                        null)
                    {
                        endStationID1 =
                            _list_Stations[0].Find(a => a.Name == _textBoxs[1].Text).ID + 1;
                    }

                    if (_list_Stations[0].Find(a => a.Name == _textBoxs[2].Text) !=
                        null)
                    {
                        nextStationID1 =
                            _list_Stations[0].Find(a => a.Name == _textBoxs[2].Text).ID + 1;
                    }

                    if (_currentInterval == PISInterval.Front)
                    {
                        if (nextStationID1 <= _list_Stations[0].Count - 1)
                        {
                            nextStationID1++;
                            if (_currentStationSetting == StationSet.Next)
                            {
                                _listBox.NextItem();
                            }
                            else
                                _textBoxs[2].Text = _list_Stations[0][nextStationID1].Name;
                        }
                        else nextStationID1 = 0;
                    }
                    else
                    {
                        if (nextStationID1 >0 )
                        {
                            nextStationID1--;
                            if (_currentStationSetting == StationSet.Next)
                            {
                                _listBox.NextItem();
                            }
                            else
                                _textBoxs[2].Text = _list_Stations[0][nextStationID1].Name;
                        }
                        else nextStationID1 = 0;
                    }

                    append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0] + 600, 0, startStationID1);
                    append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[1] + 600, 0, endStationID1);
                    append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[2] + 600, 0, nextStationID1);
                    break;
                case ButtonFlag.JumpStation:
                    break;
                default:
                    break;
            }
            if (e.Message < 6)
                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[5 + e.Message] + 4800, 0, 0);
        }

        void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[5 + e.Message] + 4800, 1, 0);
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="g"></param>
        public override void paint(Graphics g)
        {
            if (_isFirstSet)
            {
                _list_Stations[0].ForEach(a => CurrentListStation.Add(a));
                _listBox.SelectedIndex = 0;

                if (_list_Stations.Count == 2)
                {
                    _btns_Interval[0].Text = _list_Stations[0][0].Name + "\n至" + _list_Stations[0][_list_Stations[0].Count - 1].Name;
                    _btns_Interval[1].Text = _list_Stations[1][0].Name + "至\n" + _list_Stations[1][_list_Stations[1].Count - 1].Name;
                }

                _textBoxs.ForEach(a => a.Text = _list_Stations[0][0].Name);

                _isFirstSet = false;
            }

            paint_Frame(g);

            if (_currentMode != PISMode.Auto)
            {
                _btns[(int)ButtonFlag.Up].Enable = !_listBox.IsFirsItem;
                _btns[(int)ButtonFlag.Down].Enable = !_listBox.IsFinalItem;
            }
            else
            {
                _btns[(int)ButtonFlag.Up].Enable = false;
                _btns[(int)ButtonFlag.Down].Enable = false;
            }

            _btns_Mode.ForEach(a => a.Paint(g));
            _btns_Interval.ForEach(a => a.Paint(g));
            _btns_StationSetting.ForEach(a => a.Paint(g));
            _btns.ForEach(a => a.Paint(g));

            _listBox.Paint(g);
            _listBox.Paint_PageInfo(g, new RectangleF(510, 503, 32, 25));

            //根据选择的站台选择功能键获取到目前有效的TextBox，直接赋值其text属性
            TextBox tb = _textBoxs.Find(a => a.ID == _btns_StationSetting.FindIndex(b => b.IsReplication == false));
            if (tb != null)
            {
                tb.Text = _listBox.SelectedItem.Name;
                //Station s = this._list_Stations[(int)_currentInterval].Find(a => a.Name == tb.Text);
                //if (s != null)
                //{
                //    //this._listBox.SelectedIndex = s.ID;
                //}
                //else
                //    tb.Text = this._listBox.SelectedItem.Name;
            }
            _textBoxs.ForEach(a => a.Paint(g));

            base.paint(g);
        }

        /// <summary>
        /// 绘制线框
        /// </summary>
        /// <param name="g"></param>
        private void paint_Frame(Graphics g)
        {
            g.DrawImage(_resource_Image[0], new Point(0, 108));
        }
        #endregion
    }
}
