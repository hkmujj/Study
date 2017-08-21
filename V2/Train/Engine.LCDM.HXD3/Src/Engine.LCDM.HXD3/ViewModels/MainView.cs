using Engine.LCDM.HXD3.Config;
using Engine.LCDM.HXD3.Resource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using Engine.LCDM.HXD3.Common;
using Excel.Interface;
using MMI.Facility.Interface.Data;

namespace Engine.LCDM.HXD3.ViewModels
{
    [Export(typeof(MainViewModel))]
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            var tmp = ExcelParser.Parser<InfoUnit>(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory);
            InfoUnits = new ObservableCollection<InfoUnit>(tmp);
            InfoUnitDic=new Dictionary<int, bool>();
            foreach (InfoUnit temp in InfoUnits)
            {
                InfoUnitDic.Add(temp.Number,false);
            }
        }
        private float m_FLow;
        public float Flow
        {
            get
            {
                return m_FLow;
            }
            set
            {
                if (m_FLow == value)
                {
                    return;
                }
                m_FLow = value;
                RaisePropertyChanged(() => Flow);
            }
        }
        private float m_BalanceReservior;
        public int BalanceReservior
        {
            get
            {
                return (int)m_BalanceReservior;
            }
            set
            {
                if (m_BalanceReservior == value)
                {
                    return;
                }
                m_BalanceReservior = value;
                RaisePropertyChanged(() => BalanceReservior);
            }
        }
        private float m_TrainPipe;
        public int TrainPipe
        {
            get
            {
                return (int)m_TrainPipe;
            }
            set
            {
                if (m_TrainPipe == value)
                    return;
                m_TrainPipe = value;
                RaisePropertyChanged(() => TrainPipe);
            }
        }
        private float m_MainReservior;
        public int MainReservior
        {
            get
            {
                return (int)m_MainReservior;
            }
            set
            {
                if (value == m_MainReservior)
                    return;
                m_MainReservior = value;
                RaisePropertyChanged(() => MainReservior);
            }
        }
        private float m_BrakeReservior;
        public int BrakeReservior
        {
            get
            {
                return (int)m_BrakeReservior;
            }
            set
            {
                if (value == m_BrakeReservior)
                    return;
                m_BrakeReservior = value;
                RaisePropertyChanged(() => BrakeReservior);
            }
        }

        public string TrainFalut
        {
            get { return m_TrainFalut; }
            set
            {
                if (value == m_TrainFalut)
                {
                    return;
                }
                m_TrainFalut = value;
                RaisePropertyChanged(() => TrainFalut);
            }
        }
        private string m_TrainFalut = "100";
        public bool ReleaseStyle
        {
            get { return m_ReleaseStyle; }
            set 
            {
                if (value == m_ReleaseStyle)
                    return;
                m_ReleaseStyle = value;
                RaisePropertyChanged(()=>ReleaseStyle);
            }
        }
        private bool m_ReleaseStyle;

        public bool CutOrPut
        {
            get { return m_CutOrPut; }
            set
            {
                if (value == m_CutOrPut)
                    return;
                m_CutOrPut = value;
                RaisePropertyChanged(() => CutOrPut);
            }
        }
        private bool m_CutOrPut;
        public string TrainInfo
        {
            get { return m_TrainInfo; }
            set
            {
                if (value == m_TrainInfo)
                {
                    return;
                }
                m_TrainInfo = value;
                RaisePropertyChanged(() => TrainInfo);
            }
        }
        private string m_TrainInfo = "";

        public float SpeedOneWhite
        {
            get { return m_SpeedOneWhite; }
            set
            {
                if (value.Equals(m_SpeedOneWhite)) return;
                m_SpeedOneWhite = value;
                RaisePropertyChanged(() => SpeedOneWhite);
            }
        }
        private float m_SpeedOneWhite = -60.5f;
        public float SpeedTwoWhite
        {
            get { return m_SpeedTwoWhite; }
            set
            {
                if (value.Equals(m_SpeedTwoWhite)) return;
                m_SpeedTwoWhite = value;
                RaisePropertyChanged(() => SpeedTwoWhite);
            }
        }
        private float m_SpeedTwoWhite = -60.5f;

        public float SpeedOneRed
        {
            get { return m_SpeedOneRed; }
            set
            {
                if (value.Equals(m_SpeedOneRed)) return;
                m_SpeedOneRed = value;
                RaisePropertyChanged(() => SpeedOneRed);
            }
        }
        private float m_SpeedOneRed = -60.5f;

        public float SpeedTwoRed
        {
            get { return m_SpeedTwoRed; }
            set
            {
                if (value.Equals(m_SpeedTwoRed)) return;
                m_SpeedTwoRed = value;
                RaisePropertyChanged(() => SpeedTwoRed);
            }
        }
        private float m_SpeedTwoRed = -60.5f;

        public int InfoNumber
        {
            get { return m_InfoNumber; }
            set
            {
                if (value == m_InfoNumber)
                {
                    return;
                }
                m_InfoNumber = value;
                RaisePropertyChanged(() => InfoNumber);
            }
        }
        private int m_InfoNumber;


        //总风缸变白
        public Visibility TotalWhite
        {
            get { return m_TotalWhite; }
            set
            {
                if (value == m_TotalWhite)
                {
                    return;
                }
                m_TotalWhite = value;
                RaisePropertyChanged(() => TotalWhite);
            }
        }
        private Visibility m_TotalWhite=Visibility.Hidden;
        //总风缸变白
        private bool TurnWhite;
        //总风缸闪白
        public bool TotalFlash
        {
            get { return m_TotalFlash; }
            set
            {
                if (value == m_TotalFlash)
                {
                    return;
                }
                m_TotalFlash = value;
                RaisePropertyChanged(() => TotalFlash);
            }
        }
        private bool m_TotalFlash;

        //MIPU失电
        public Visibility MipuVisibility
        {
            get { return m_MipuVisibility; }
            set
            {
                if (value == m_MipuVisibility)
                {
                    return;
                }
                m_MipuVisibility = value;
                RaisePropertyChanged(() => MipuVisibility);
            }
        }
        private Visibility m_MipuVisibility = Visibility.Hidden;

        //EPCU失电且MIPU不失电
        public Visibility EpcuNotMipmVisibility
        {
            get { return m_EpcuNotMipuVisibility; }
            set
            {
                if (value == m_EpcuNotMipuVisibility)
                {
                    return;
                }
                m_EpcuNotMipuVisibility = value;
                RaisePropertyChanged(() => EpcuNotMipmVisibility);
            }
        }
        private Visibility m_EpcuNotMipuVisibility = Visibility.Hidden;
        //EPCU失电
        public Visibility EpcuVisibility
        {
            get { return m_EpcuVisibility; }
            set
            {
                if (value == m_EpcuVisibility)
                {
                    return;
                }
                m_EpcuVisibility = value;
                RaisePropertyChanged(() => EpcuVisibility);
            }
        }
        private Visibility m_EpcuVisibility=Visibility.Hidden; 

        //EPCU失电或MIPU失电
        public Visibility NorEmVisibility
        {
            get { return m_NorEmVisibility; }
            set
            {
                if (value == m_NorEmVisibility)
                {
                    return;
                }
                m_NorEmVisibility = value;
                RaisePropertyChanged(() => NorEmVisibility);
            }
        }
        private Visibility m_NorEmVisibility = Visibility.Visible;

        //惩罚制动
        public Visibility PunishBrakeVisibility
        {
            get { return m_PunishBrakeVisibility; }
            set
            {
                if (value == m_PunishBrakeVisibility)
                {
                    return;
                }
                m_PunishBrakeVisibility = value;
                RaisePropertyChanged(() => PunishBrakeVisibility);
            }
        }
        private Visibility m_PunishBrakeVisibility = Visibility.Hidden;
        //动力切除
        public Visibility MotiveCutVisibility
        {
            get { return m_MotiveCutVisibility; }
            set
            {
                if (value == m_MotiveCutVisibility)
                {
                    return;
                }
                m_MotiveCutVisibility = value;
                RaisePropertyChanged(() => MotiveCutVisibility);
            }
        }
        private Visibility m_MotiveCutVisibility = Visibility.Hidden;
        //动力制动
        public Visibility MotiveBrakeVisibility
        {
            get { return m_MotiveBrakeVisibility; }
            set
            {
                if (value == m_MotiveBrakeVisibility)
                {
                    return;
                }
                m_MotiveBrakeVisibility = value;
                RaisePropertyChanged(() => MotiveBrakeVisibility);
            }
        }
        private Visibility m_MotiveBrakeVisibility = Visibility.Hidden;
        //电控缓解
        public Visibility PowerControlRelease
        {
            get { return m_PowerControlRelease; }
            set 
            {
                if (value == m_PowerControlRelease)
                    return;
                m_PowerControlRelease = value;
                RaisePropertyChanged(() => PowerControlRelease);
            }
        }
        private Visibility m_PowerControlRelease = Visibility.Hidden;
        //电控制动
        public Visibility PowerControlBrake
        {
            get { return m_PowerControlBrake; }
            set 
            {
                if (m_PowerControlBrake == value)
                    return;
                m_PowerControlBrake = value;
                RaisePropertyChanged(()=>PowerControlBrake);
            }
        }
        private Visibility m_PowerControlBrake = Visibility.Hidden;
        public Visibility PowerControlNeutral
        {
            get { return m_PowerControlNeutral; }
            set 
            {
                if (m_PowerControlNeutral == value)
                    return;
                m_PowerControlNeutral = value;
                RaisePropertyChanged(()=>PowerControlNeutral);
            }
        }
        private Visibility m_PowerControlNeutral = Visibility.Hidden;

        private readonly int[] m_BoolIndex = new int[]
        {
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.本机],                //0
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.补机],
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.单机],
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.亮屏标志],          //3
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.动力切除],
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.惩罚制动],
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.总风缸闪白],
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.总风缸变白],
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.亮度加],           //8
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.亮度减],
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.亮度自适应],
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.EPCU失电],       //11
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.MIPM失电],
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.动力制动],       //13
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.电控缓解],
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.电控制动],
            GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.电控中立]
        };

        private ObservableCollection<InfoUnit> m_InfoUnits;
        private InfoUnit m_Unit;
        

        public override void DataChanged(object sender, MMI.Facility.Interface.Data.CommunicationDataChangedArgs<float> args)
        {
            var indexOne = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.流量];
            if (args.NewValue.ContainsKey(indexOne))
            {
                Flow = args.NewValue[indexOne];
                if (NorEmVisibility == Visibility.Hidden)
                    Flow = 0;
            }
            var indexTwo = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.均衡风缸];
            if (args.NewValue.ContainsKey(indexTwo))
            {
                BalanceReservior = (int)args.NewValue[indexTwo];
                SpeedOneRed = args.NewValue[indexTwo] * 301f / 1400f - 60.5f;
            }
            var indexThree = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.列车管];
            if (args.NewValue.ContainsKey(indexThree))
            {
                TrainPipe = (int)args.NewValue[indexThree];
                SpeedOneWhite = args.NewValue[indexThree] * 301f / 1400f - 60.5f;
            }
            var indexFour = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.制动缸1];
            if (args.NewValue.ContainsKey(indexFour))
            {
                MainReservior = (int)args.NewValue[indexFour];
                SpeedTwoRed = args.NewValue[indexFour] * 301f / 1400f - 60.5f;
            }
            var indexFive = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.制动缸2];
            if (args.NewValue.ContainsKey(indexFive))
            {
                BrakeReservior = (int)args.NewValue[indexFive];
                SpeedTwoWhite = args.NewValue[indexFive] * 301f / 1400f - 60.5f;
            }
            base.DataChanged(sender, args);
        }

        public override void DataChanged(object sender, CommunicationDataChangedArgs<bool> args)
        {
            if (InfoUnitDic.Any(w => args.NewValue.ContainsKey(w.Key)))
            {
                foreach (var temp in InfoUnits)
                {
                    if (args.NewValue.ContainsKey(temp.Number))
                        InfoUnitDic[temp.Number] = args.NewValue[temp.Number];
                }
                Unit = InfoUnits.FirstOrDefault(f => InfoUnitDic[f.Number]);
            }

            if (args.NewValue.ContainsKey(m_BoolIndex[0]))
            {
                if (args.NewValue[m_BoolIndex[0]])
                    TrainFalut = "100";
            }
            if (args.NewValue.ContainsKey(m_BoolIndex[1]))
            {
                if (args.NewValue[m_BoolIndex[1]])
                    TrainFalut = "010";
            }
            if (args.NewValue.ContainsKey(m_BoolIndex[2]))
            {
                if (args.NewValue[m_BoolIndex[2]])
                    TrainFalut = "001";
            }
            if (args.NewValue.ContainsKey(m_BoolIndex[4]))
            {
                MotiveCutVisibility = args.NewValue[m_BoolIndex[4]] ? Visibility.Visible : Visibility.Hidden;
            }
            if (args.NewValue.ContainsKey(m_BoolIndex[5]))
            {
                PunishBrakeVisibility = args.NewValue[m_BoolIndex[5]] ? Visibility.Visible : Visibility.Hidden;
            }

            if (args.NewValue.ContainsKey(m_BoolIndex[13]))
            {
                MotiveBrakeVisibility = args.NewValue[m_BoolIndex[13]] ? Visibility.Visible : Visibility.Hidden;
            }
            if (args.NewValue.ContainsKey(m_BoolIndex[14]))
            {
                PowerControlRelease = args.NewValue[m_BoolIndex[14]] ? Visibility.Visible : Visibility.Hidden;
            }
            if (args.NewValue.ContainsKey(m_BoolIndex[15]))
            {
                PowerControlBrake = args.NewValue[m_BoolIndex[15]] ? Visibility.Visible : Visibility.Hidden;
            }
            if (args.NewValue.ContainsKey(m_BoolIndex[16]))
            {
                PowerControlNeutral = args.NewValue[m_BoolIndex[16]] ? Visibility.Visible : Visibility.Hidden;
            }
            if (args.NewValue.ContainsKey(m_BoolIndex[7]))
            {
                if (args.NewValue[m_BoolIndex[7]])
                    TotalWhite = Visibility.Visible;
                else
                {
                    if (!TotalFlash)
                        TotalWhite = Visibility.Hidden;
                }
                TurnWhite = args.NewValue[m_BoolIndex[7]];
            }
            if (args.NewValue.ContainsKey(m_BoolIndex[6]))
            {
                if (args.NewValue[m_BoolIndex[6]])
                    TotalWhite = Visibility.Visible;
                else
                {
                    if(!TurnWhite)
                        TotalWhite=Visibility.Hidden;
                }
                TotalFlash = args.NewValue[m_BoolIndex[6]];
            }
        }

        public ObservableCollection<InfoUnit> InfoUnits
        {
            get { return m_InfoUnits; }
            set
            {
                if (Equals(value, m_InfoUnits))
                {
                    return;
                }
                m_InfoUnits = value;
                RaisePropertyChanged(() => InfoUnits);
            }
        }

        public LCDMViewModel ViewModel { get; set; }
        public InfoUnit Unit
        {
            get { return m_Unit; }
            set
            {
                if (Equals(value, m_Unit))
                {
                    return;
                }
                m_Unit = value;
                RaisePropertyChanged(() => Unit);
            }
        }

        private Dictionary<int, bool> InfoUnitDic;
    }
}
