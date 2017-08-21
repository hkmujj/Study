using System.Drawing;
using System.Runtime.InteropServices;
using CommonControls;
using Excel.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using ShenHuaHaoTMS.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using YunDa.JC.MMI.Common;
using System.IO;
using System.Xml.Serialization;
using MMI.Facility.Interface.Extension;
using YunDa.JC.MMI.Common.Extensions;

namespace ShenHuaHaoTMS
{
    public partial class BlackView : ProjectFormBase, IDisposable, IDataListener
    {
        private ViewManager _viewManager;
        private ICommunicationDataService _dataService;
        private NoBodyVigilantView _nbvv;

        private SH_V405_GroupDiagnose _v405 = null;

        private SH_V501_A_BowState v501;
        private SH_V502_A_BreakerState v502;
        private SH_V503_A_TractionState v503;
        private SH_V504_B_BowState v504;
        private SH_V505_B_BreakerState v505;
        private SH_V506_B_TractionState v506;
        private SH_V507_A_BowState_F v507;
        private SH_V508_A_BreakerState_F v508;
        private SH_V509_A_TractionState_F v509;
        private SH_V510_B_BowState_F v510;
        private SH_V511_B_BreakerState_F v511;
        private SH_V512_B_TractionState_F v512;

        private SH_V801_A_CurrentFalut v801;
        private SH_V802_B_CurrentFalut v802;
        private List<List<FalutInfo>> faluts = new List<List<FalutInfo>>();

        public Int32 LocalTrainType
        {
            set
            {
                if (_localTrainType == value)
                {
                    return;
                }

                _localTrainType = value;

                _v405.SetTrainType(value);
            }
        }

        private Int32 _localTrainType = 0;

        public TrainInfo LocalTrainInfo { get; set; }

        #region 故障
        public delegate void EventHandleFalutChanged(FalutState fs);
        public event EventHandleFalutChanged FalutChangedA
        {
            add { _falutChangedA += value; }
            remove { if (_falutChangedA != null)
            {
                _falutChangedA -= value;
            }
            }
        }
        private EventHandleFalutChanged _falutChangedA = null;

        public FalutState CurrentFalutStateA
        {
            set
            {
                if (_currentFalutStateA == value)
                {
                    return;
                }

                _currentFalutStateA = value;

                if (_falutChangedA != null)
                {
                    _falutChangedA(value);
                }
            }
        }
        private FalutState _currentFalutStateA = null;

        public event EventHandleFalutChanged FalutChangedB
        {
            add { _falutChangedB += value; }
            remove { if (_falutChangedB != null)
            {
                _falutChangedB -= value;
            }
            }
        }
        private EventHandleFalutChanged _falutChangedB = null;

        public FalutState CurrentFalutStateB
        {
            set
            {
                if (_currentFalutStateB == value)
                {
                    return;
                }

                _currentFalutStateB = value;

                if (_falutChangedB != null)
                {
                    _falutChangedB(value);
                }
            }
        }
        private FalutState _currentFalutStateB = null;
        #endregion

        #region 左边
        public delegate void EventHandleInfoChanged(Info info);
        private List<Info> _infosLeft = new List<Info>();
        public event EventHandleInfoChanged LeftInfoChanged
        {
            add { _leftInfoChanged += value; }
            remove { if (_leftInfoChanged != null)
            {
                _leftInfoChanged -= value;
            }
            }
        }
        private EventHandleInfoChanged _leftInfoChanged = null;
        public Info CurrentLeftInfo
        {
            set
            {
                if (_currentLeftInfo == value)
                {
                    return;
                }

                _currentLeftInfo = value;

                if (_leftInfoChanged != null)
                {
                    _leftInfoChanged(value);
                }
            }
        }
        private Info _currentLeftInfo = null;
        #endregion

        #region 中间
        private List<Info> _infosMiddle = new List<Info>();
        public event EventHandleInfoChanged MiddleInfoChanged
        {
            add { _middleInfoChanged += value; }
            remove { if (_middleInfoChanged != null)
            {
                _middleInfoChanged -= value;
            }
            }
        }
        private EventHandleInfoChanged _middleInfoChanged = null;
        public Info CurrentMiddleInfo
        {
            set
            {
                if (_currentMiddleInfo == value)
                {
                    return;
                }

                _currentMiddleInfo = value;

                if (_middleInfoChanged != null)
                {
                    _middleInfoChanged(value);
                }
            }
        }
        private Info _currentMiddleInfo = null;
        #endregion

        public BlackView(SubsystemInitParam initParam, ICommunicationDataService dataService,ICourseService courseService)
            : base(initParam.AppConfig.AppName, initParam.DataPackage)
        {
            AppName = initParam.AppConfig.AppName;
            AppConfig = initParam.AppConfig;

            InitializeComponent();

            initParam.DataPackage.ServiceManager.GetService<IDisposeService>().RegistDisposableObject(this);

            _dataService = dataService;

            //this._viewParent.MouseEnter += (ob, ar) => { OnMouseEnter(ar); };
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            _viewManager = new ViewManager(_viewParent);

            LocalTrainInfo = new TrainInfo();

            using (Stream s = new FileStream(Application.StartupPath + @"\Config\机车配置.xml", FileMode.Open))
            {
                XmlSerializer xs = new XmlSerializer(typeof (TrainInfo));
                LocalTrainInfo = (TrainInfo) xs.Deserialize(s);
            }

            //初始化界面
            SH_V1_MainView v1 = new SH_V1_MainView(1, _viewManager, dataService, this, initParam);
            v1.Show();
            v1.Hide();

            SH_V2_MainData v2 = new SH_V2_MainData(2, _viewManager, dataService, this, initParam);
            SH_V202_A_Temperature v202 = new SH_V202_A_Temperature(202, _viewManager, dataService, this, initParam);
            SH_V203_B_Temperature v203 = new SH_V203_B_Temperature(203, _viewManager, dataService, this, initParam);
            SH_V204_A_AuxiliarySys v204 = new SH_V204_A_AuxiliarySys(204, _viewManager, dataService, this, initParam);
            SH_V205_B_AuxiliarySys v205 = new SH_V205_B_AuxiliarySys(205, _viewManager, dataService, this, initParam);
            SH_V206_A_AxisTemperature v206 = new SH_V206_A_AxisTemperature(206, _viewManager, dataService, this, initParam);
            SH_V207_B_AxisTemperature v207 = new SH_V207_B_AxisTemperature(207, _viewManager, dataService, this, initParam);
            SH_V208_AuxiliaryTest v208 = new SH_V208_AuxiliaryTest(208, _viewManager, dataService, this, initParam);
            SH_V209_ControlSys v209 = new SH_V209_ControlSys(209, _viewManager, dataService, this, initParam);

            SH_V3_Maintain v3 = new SH_V3_Maintain(3, _viewManager, dataService);
            SH_V301_TrainIDSetting v301 = new SH_V301_TrainIDSetting(301, _viewManager, dataService);
            SH_V302_WheelSetting v302 = new SH_V302_WheelSetting(302, _viewManager, dataService);
            SH_V303_TrainAOrB v303 = new SH_V303_TrainAOrB(303, _viewManager, dataService);
            SH_V304_TimeSetting v304 = new SH_V304_TimeSetting(304, _viewManager, dataService);

            SH_V4_Connect v4 = new SH_V4_Connect(4, _viewManager, dataService, this, initParam);
            SH_V401_StateShow v401 = new SH_V401_StateShow(401, _viewManager, dataService, this, initParam);
            SH_V4011_BreakingState v4011 = new SH_V4011_BreakingState(4011, _viewManager, dataService, initParam);
            SH_V4012_VersionInfo v4012 = new SH_V4012_VersionInfo(4012, _viewManager, dataService, initParam);
            SH_V4013_FaultLight v4013 = new SH_V4013_FaultLight(4013, _viewManager, dataService, initParam);
            SH_V403_DisConnect v403 = new SH_V403_DisConnect(403, _viewManager, dataService, this, initParam);
            SH_V404_GroupSetting v404 = new SH_V404_GroupSetting(404, _viewManager, dataService, this, v401, initParam);
            _v405 = new SH_V405_GroupDiagnose(405, _viewManager, dataService,this);

            v501 = new SH_V501_A_BowState(501, _viewManager, dataService, this, initParam);
            v502 = new SH_V502_A_BreakerState(502, _viewManager, dataService, this, initParam);
            v503 = new SH_V503_A_TractionState(503, _viewManager, dataService, this, initParam);
            v504 = new SH_V504_B_BowState(504, _viewManager, dataService, this, initParam);
            v505 = new SH_V505_B_BreakerState(505, _viewManager, dataService, this, initParam);
            v506 = new SH_V506_B_TractionState(506, _viewManager, dataService, this, initParam);
            v507 = new SH_V507_A_BowState_F(507, _viewManager, dataService, this, initParam);
            v508 = new SH_V508_A_BreakerState_F(508, _viewManager, dataService, this, initParam);
            v509 = new SH_V509_A_TractionState_F(509, _viewManager, dataService, this, initParam);
            v510 = new SH_V510_B_BowState_F(510, _viewManager, dataService, this, initParam);
            v511 = new SH_V511_B_BreakerState_F(511, _viewManager, dataService, this, initParam);
            v512 = new SH_V512_B_TractionState_F(512, _viewManager, dataService, this, initParam);

            SH_V6_ParamConfig v6 = new SH_V6_ParamConfig(6, _viewManager, dataService, this, initParam);
            SH_V601_TrianParam v601 = new SH_V601_TrianParam(601, _viewManager, dataService, this, initParam);
            SH_V602_WheelOiling v602 = new SH_V602_WheelOiling(602, _viewManager, dataService, this, initParam);
            SH_V603_Vigilant v603 = new SH_V603_Vigilant(603, _viewManager, dataService, this, initParam);
            SH_V604_TopSwith v604 = new SH_V604_TopSwith(604, _viewManager, dataService, this, initParam);

            SH_V7_TractionData v7 = new SH_V7_TractionData(7, _viewManager, dataService, this, initParam);

            v801 = new SH_V801_A_CurrentFalut(801, _viewManager, dataService, this, initParam);
            v802 = new SH_V802_B_CurrentFalut(802, _viewManager, dataService, this, initParam);

            SH_V803_A_AllFalut v803 = new SH_V803_A_AllFalut(803, _viewManager, dataService, this, initParam);
            SH_V804_A_DestroyFalut v804 = new SH_V804_A_DestroyFalut(804, _viewManager, dataService, this, initParam);
            SH_V805_B_AllFalut v805 = new SH_V805_B_AllFalut(805, _viewManager, dataService, this, initParam);
            SH_V806_B_DestroyFalut v806 = new SH_V806_B_DestroyFalut(806, _viewManager, dataService, this, initParam);

            SH_V807_A_OperateInfo_V0 v807 = new SH_V807_A_OperateInfo_V0(807, _viewManager, dataService, v801, this, initParam);
            SH_V808_A_OperateInfo_V1 v808 = new SH_V808_A_OperateInfo_V1(808, _viewManager, dataService, v801, this, initParam);
            SH_V809_A_OperateInfo_Info v809 = new SH_V809_A_OperateInfo_Info(809, _viewManager, dataService, v801, this, initParam);
            SH_V810_B_OperateInfo_V0 v810 = new SH_V810_B_OperateInfo_V0(810, _viewManager, dataService, v802, this, initParam);
            SH_V811_B_OperateInfo_V1 v811 = new SH_V811_B_OperateInfo_V1(811, _viewManager, dataService, v802, this, initParam);
            SH_V812_B_OperateInfo_Info v812 = new SH_V812_B_OperateInfo_Info(812, _viewManager, dataService, v802, this, initParam);

            SH_V0_BackView v0 = new SH_V0_BackView(0, _viewManager, dataService);

            GlobalParam.Instance.InitParam.RegistDataListener(this);

            if (dataService.ReadService.ReadOnlyBoolDictionary[1300])
            {
                _viewManager.CurrentViewID = 1;
            }
            else
            {
                _viewManager.CurrentViewID = 0;
            }

            
            _nbvv = new NoBodyVigilantView(this){ };
            _nbvv.Show();
            _nbvv.Hide();

            readFile();

            GlobalTimer.Instance.TimersElapsed1000MS += _timer_Elapsed;

        }

        void _timer_Tick(object sender, System.EventArgs e)
        {
            if (!_dataService.ReadService.ReadOnlyBoolDictionary[1300])
            {
                _viewManager.CurrentViewID = 0;
            }
            _dataService.WriteService.ChangeFloat(56, LocalTrainInfo.RealTrainID);

            if (LocalTrainInfo.ControlType == View.V4.ControlType.主控)
            {
                LocalTrainType = 0;
            }
            else
            {
                LocalTrainType = LocalTrainInfo.Number;
            }
            if (LocalTrainInfo.HideCursor == 0)
            {
                Point p = new Point(0, 0);
                GetCursorPos(out p);

                if (p.X < 800 && p.Y < 600)
                {
                    SetCursorPos(850, 600);
                }
            }
        }

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!_dataService.ReadService.ReadOnlyBoolDictionary[1300])
            {
                _viewManager.CurrentViewID = 0;
            }
            _dataService.WriteService.ChangeFloat(56, LocalTrainInfo.RealTrainID);

            if (LocalTrainInfo.ControlType == View.V4.ControlType.主控)
            {
                LocalTrainType = 0;
            }
            else
            {
                LocalTrainType = LocalTrainInfo.Number;
            }
            if (LocalTrainInfo.HideCursor == 0)
            {
                Point p = new Point(0, 0);
                GetCursorPos(out p);

                if (p.X < 800 && p.Y < 600)
                {
                    SetCursorPos(850, 600);
                }
            }
        }

        private bool _isWhileThread = true;

        private void readFile()
        {
            List<LogicInfo> logicInfos = new List<LogicInfo>();
            ExcelAdapter ea = new ExcelAdapter();
            ExcelReaderConfig erc = new ExcelReaderConfig()
            {
                File = Application.StartupPath + @"\Config\牵引封锁数据配置.xls",
                SheetNames = new List<string>() { "A车-受电弓", "B车-受电弓", "A车-主断", "B车-主断", "A车-牵引封锁", "B车-牵引封锁", "从车A-受电弓", "从车B-受电弓", "从车A-主断", "从车B-主断", "从车A-牵引封锁", "从车B-牵引封锁" },
                Coloumns = new List<ColoumnConfig>() { new ColoumnConfig() { Name = "逻辑编号" }, new ColoumnConfig() { Name = "条目描述" } }
            };
            DataSet ds = ea.Adapter(erc);
            v501.SetData(ds);
            v502.SetData(ds);
            v503.SetData(ds);
            v504.SetData(ds);
            v505.SetData(ds);
            v506.SetData(ds);
            v507.SetData(ds);
            v508.SetData(ds);
            v509.SetData(ds);
            v510.SetData(ds);
            v511.SetData(ds);
            v512.SetData(ds);

            readFalutInfo();
            readDiagnoseInfoFile();
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point p);

        [DllImport("user32.dll")]
        public static extern void SetCursorPos(int x,int y);

        private void readDiagnoseInfoFile()
        {
            ExcelAdapter ea = new ExcelAdapter();
            ExcelReaderConfig erc = new ExcelReaderConfig()
            {
                File = Application.StartupPath + @"\Config\编组诊断提示信息.xls",
                SheetNames = new List<string>() { "主车", "从1", "从2", "从3" },
                Coloumns = new List<ColoumnConfig>() { new ColoumnConfig() { Name = "逻辑编号" }, new ColoumnConfig() { Name = "诊断描述" } }
            };
            DataSet ds = ea.Adapter(erc);
            _v405.SetData(ds);
        }

        public bool IsStart20
        {
            set
            {
                if (_isStart20 == value)
                {
                    return;
                }

                _isStart20 = value;

                if (value)
                {
                    _nbvv.Start20New();
                    _nbvv.ShowNew();
                                    }
                else if (!_isStart10)
                {
                    _nbvv.InvokeHide();
                    _nbvv.Stop();
                    _viewManager.OnPait();
                }
            }
        }
        private bool _isStart20 = false;

        public bool IsStart10
        {
            set
            {
                if (_isStart10 == value)
                {
                    return;
                }

                _isStart10 = value;

                if (value)
                {
                    _nbvv.Start10New();
                    _nbvv.ShowNew();
                }
                else if (!_isStart20)
                {
                    _nbvv.InvokeHide();
                    _nbvv.Stop();
                    _viewManager.OnPait();
                }
            }
        }
        private bool _isStart10 = false;

        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> e)
        {
            foreach (var cmd in e.NewValue)
            {
                if (cmd.Key == 1300)
                {
                    if (cmd.Value)
                    {
                        _viewManager.CurrentViewID = 1;
                    }
                    else
                    {
                        LocalTrainInfo.ControlType = View.V4.ControlType.主控;
                        LocalTrainInfo.Direction = View.V4.Direction.上行;
                        LocalTrainInfo.TrainType = View.V4.TrainType.SH8;
                        LocalTrainInfo.Count = 2;
                        LocalTrainInfo.Number = 1;
                        _viewManager.CurrentViewID = 0;
                    }
                }
                else if (cmd.Key == 823)
                {
                    if (_viewManager.CurrentViewID != 801 && _viewManager.CurrentViewID != 802
                        && _viewManager.CurrentViewID != 803 && _viewManager.CurrentViewID != 804
                        && _viewManager.CurrentViewID != 805 && _viewManager.CurrentViewID != 806
                        && _viewManager.CurrentViewID != 807 && _viewManager.CurrentViewID != 808
                        && _viewManager.CurrentViewID != 809 && _viewManager.CurrentViewID != 810
                        && _viewManager.CurrentViewID != 811 && _viewManager.CurrentViewID != 812)
                    {
                        _viewManager.CurrentViewID = 801;
                    }
                }
                else if (cmd.Key == 1313)
                {
                    IsStart10 = cmd.Value;
                }
                else if (cmd.Key == 1314)
                {
                    IsStart20 = cmd.Value;
                }

                if (faluts == null || faluts.Count == 0)
                {
                    continue;
                }

                #region A车故障
                foreach (var item in faluts[0])//A车故障
                {
                    if (item.LogicID == cmd.Key)
                    {
                        String month = DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
                        String day = DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
                        if (cmd.Value)
                        {
                            item.StartTime = month + "-" + day + " " + DateTime.Now.ToString("hh:mm:ss");
                            CurrentFalutStateA = new FalutState()
                            {
                                IsSet = true,
                                CurrentFalut = new FalutInfo() 
                                {
                                     Code=item.Code,
                                     Description=item.Description,
                                     Grade=item.Grade,
                                     Info=item.Info,
                                     LogicID=item.LogicID,
                                     PointOut=item.PointOut,
                                     StartTime=item.StartTime,
                                     TrainID=item.TrainID,
                                     V0=item.V0,
                                     V1=item.V1
                                }
                            };
                        }
                        else
                        {
                            item.EndTime = month + "-" + day + " " + DateTime.Now.ToString("hh:mm:ss");
                            CurrentFalutStateA = new FalutState()
                            {
                                IsSet = false,
                                CurrentFalut = new FalutInfo()
                                {
                                    Code = item.Code,
                                    Description = item.Description,
                                    Grade = item.Grade,
                                    Info = item.Info,
                                    LogicID = item.LogicID,
                                    PointOut = item.PointOut,
                                    EndTime = item.EndTime,
                                    TrainID = item.TrainID,
                                    V0 = item.V0,
                                    V1 = item.V1
                                }
                            };
                        }
                    }
                }
                #endregion

                #region B车故障
                if (faluts.Count == 1)
                {
                    continue;
                }

                foreach (var item in faluts[1])//A车故障
                {
                    if (item.LogicID == cmd.Key)
                    {
                        if (cmd.Value)
                        {
                            item.StartTime = DateTime.Now.ToString("mm-dd hh:mm:ss");
                            CurrentFalutStateB = new FalutState()
                            {
                                IsSet = true,
                                CurrentFalut = new FalutInfo()
                                {
                                    Code = item.Code,
                                    Description = item.Description,
                                    Grade = item.Grade,
                                    Info = item.Info,
                                    LogicID = item.LogicID,
                                    PointOut = item.PointOut,
                                    StartTime = item.StartTime,
                                    TrainID = item.TrainID,
                                    V0 = item.V0,
                                    V1 = item.V1
                                }
                            };
                        }
                        else
                        {
                            item.EndTime = DateTime.Now.ToString("mm-dd hh:mm:ss");
                            CurrentFalutStateB = new FalutState()
                            {
                                IsSet = false,
                                CurrentFalut = new FalutInfo()
                                {
                                    Code = item.Code,
                                    Description = item.Description,
                                    Grade = item.Grade,
                                    Info = item.Info,
                                    LogicID = item.LogicID,
                                    PointOut = item.PointOut,
                                    EndTime = item.EndTime,
                                    TrainID = item.TrainID,
                                    V0 = item.V0,
                                    V1 = item.V1
                                }
                            };
                        }
                    }
                }
                #endregion

                foreach (var item in _infosLeft)
                {
                    if (item.LogicID == cmd.Key)
                    {
                        if (cmd.Value)
                        {
                            CurrentLeftInfo = new Info()
                            {
                                IsSet = true,
                                LogicID = item.LogicID,
                                Description = item.Description
                            };
                        }
                        else
                        {
                            CurrentLeftInfo = new Info()
                            {
                                IsSet = false,
                                LogicID = item.LogicID,
                                Description = item.Description
                            };
                        }
                    }
                }

                foreach (var item in _infosMiddle)
                {
                    if (item.LogicID == cmd.Key)
                    {
                        if (cmd.Value)
                        {
                            CurrentMiddleInfo = new Info()
                            {
                                IsSet = true,
                                LogicID = item.LogicID,
                                Description = item.Description
                            };
                        }
                        else
                        {
                            CurrentMiddleInfo = new Info()
                            {
                                IsSet = false,
                                LogicID = item.LogicID,
                                Description = item.Description
                            };
                        }
                    }
                }
            }
        }

        private void BlackView_FormClosing(object sender, FormClosingEventArgs e)
        {
           // _thread.Abort();
        }

        protected override void OnClosed(EventArgs e)
        {
            //_thread.Abort();
            //base.OnClosed(e);
        }

        private void readFalutInfo()
        {
            List<FalutInfo> falutsA = new List<FalutInfo>();
            List<FalutInfo> falutsB = new List<FalutInfo>();

            ExcelAdapter ea = new ExcelAdapter();
            ExcelReaderConfig erc = new ExcelReaderConfig()
            {
                File = Application.StartupPath + @"\Config\故障信息.xls",
                SheetNames = new List<string>() { "A车故障", "B车故障" },
                Coloumns = new List<ColoumnConfig>() 
                {
                    new ColoumnConfig() { Name = "逻辑编号" }, 
                    new ColoumnConfig() { Name = "代码" }, 
                    new ColoumnConfig() { Name = "车号" },  
                    new ColoumnConfig() { Name = "等级" },
                    new ColoumnConfig() { Name = "故障提示" },
                    new ColoumnConfig() { Name = "故障描述" },
                    new ColoumnConfig() { Name = "V等于0" },
                    new ColoumnConfig() { Name = "V大于0" },
                    new ColoumnConfig() { Name = "信息" }
                }
            };
            DataSet ds = ea.Adapter(erc);
            DataRowCollection drc = ds.Tables["A车故障"].Rows;
            foreach (DataRow item in drc)
            {
                falutsA.Add(new FalutInfo()
                {
                    LogicID = Convert.ToInt32(item[0]),
                    Code = item[1].ToString(),
                    TrainID = Convert.ToInt32(item[2]),
                    Grade = item[3].ToString(),
                    PointOut = (String)item[4],
                    Description = (String)item[5],
                    V0 = (String)item[6],
                    V1 = (String)item[7],
                    Info = (String)item[8]
                });
            }

            drc = ds.Tables["B车故障"].Rows;
            foreach (DataRow item in drc)
            {
                falutsB.Add(new FalutInfo()
                {
                    LogicID = Convert.ToInt32(item[0]),
                    Code = item[1].ToString(),
                    TrainID = Convert.ToInt32(item[2]),
                    Grade = item[3].ToString(),
                    PointOut = (String)item[4],
                    Description = (String)item[5],
                    V0 = (String)item[6],
                    V1 = (String)item[7],
                    Info = (String)item[8]
                });
            }
            faluts= new List<List<FalutInfo>>() { falutsA,falutsB};

            erc = new ExcelReaderConfig()
            {
                File = Application.StartupPath + @"\Config\提示信息.xls",
                SheetNames = new List<string>() {"A车左边", "B车左边", "A车中间", "B车中间" },
                Coloumns = new List<ColoumnConfig>() 
                {
                    new ColoumnConfig() { Name = "逻辑编号" }, 
                    new ColoumnConfig() { Name = "描述" }
                }
            };
            ds = ea.Adapter(erc);
            drc = ds.Tables["A车左边"].Rows;
            foreach (DataRow item in drc)
            {
                _infosLeft.Add(new Info()
                {
                    LogicID = Convert.ToInt32(item[0]),
                    Description = (String)item[1]
                });
            }

            drc = ds.Tables["B车左边"].Rows;
            foreach (DataRow item in drc)
            {
                _infosLeft.Add(new Info()
                {
                    LogicID = Convert.ToInt32(item[0]),
                    Description = (String)item[1]
                });
            }

            drc = ds.Tables["A车中间"].Rows;
            foreach (DataRow item in drc)
            {
                _infosMiddle.Add(new Info()
                {
                    LogicID = Convert.ToInt32(item[0]),
                    Description = (String)item[1]
                });
            }

            drc = ds.Tables["B车中间"].Rows;
            foreach (DataRow item in drc)
            {
                _infosMiddle.Add(new Info()
                {
                    LogicID = Convert.ToInt32(item[0]),
                    Description = (String)item[1]
                });
            }
        }

        public void Dispose()
        {
            GlobalTimer.Instance.TimersElapsed1000MS -= _timer_Elapsed;

            //_thread.Abort();
            if (_nbvv != null)
            {
                _nbvv.Dispose();
            }
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
        }
    }

    public class FalutState
    {
        public bool IsSet { get; set; }

        public FalutInfo CurrentFalut { get; set; }
    }

    public class Info
    {
        public Boolean IsSet { get; set; }
        public Int32 LogicID { get; set; }

        public String Description { get; set; }
    }
}
