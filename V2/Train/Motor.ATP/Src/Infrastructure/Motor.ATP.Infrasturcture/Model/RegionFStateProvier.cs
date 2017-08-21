using System;
using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model
{
    public class RegionFStateProvier : ATPPartialBase, IRegionFStateProvier
    {
        private bool m_Visible;

        public RegionFStateProvier(ATPDomain parent)
            : base(parent)
        {
            Visible = true;
            DataStateProvider = new ItemStateProvider();
            FreqStateProvider = new ItemStateProvider();
            CTCSStateProvider = new ItemStateProvider();
            OtherStateProvider = new ItemStateProvider();
            TrainIdStateProvider = new ItemStateProvider();
            DriverIdStateProvider = new ItemStateProvider();
            TrainDataStateProvider = new ItemStateProvider();
            EightTrucksStateProvider = new ItemStateProvider();
            SixteenTrucksStateProvider = new ItemStateProvider();
            ShuntingStateProvider = new EnterableItemStateProvider();
            OnSightStateProvider = new EnterableItemStateProvider();
            StartStateProvider = new ItemStateProvider();
            RBCDataStateProvider = new ItemStateProvider();
            CabsignalStateProvider = new EnterableItemStateProvider();
            RelaseStateProvider = new ItemStateProvider();
            ControlModelStateProvider = new ItemStateProvider();
            CTCS2StateProvider = new ItemStateProvider();
            CTCS3StateProvider = new ItemStateProvider();
            CTCS3DStateProvider = new ItemStateProvider(false);
            UpFreqStateProvider = new ItemStateProvider();
            DownFreqStateProvider = new ItemStateProvider();
            VigilantStateProvider = new ItemStateProvider();
            RunBrakeTestStateProvider = new ItemStateProvider();
            SkipRunBrakeTestStateProvider = new ItemStateProvider();
            RBCIDStateProvider = new ItemStateProvider();
            TelNumberStateProvider = new ItemStateProvider();
            NetNumberStateProvider = new ItemStateProvider(false);
            DMITestStateProvider = new ItemStateProvider();
            RunNormalBrakeTestStateProvider = new ItemStateProvider();
            RunEmergencyBrakeTestStateProvider = new ItemStateProvider();
            QuitTestStateProvider = new ItemStateProvider();
            CTCS0StateProvider = new EnterableItemStateProvider();
        }

        public ItemStateProvider DataStateProvider { get; private set; } //数据 0
        public ItemStateProvider FreqStateProvider { get; private set; } //载频 2
        public ItemStateProvider UpFreqStateProvider { get; private set; } //上行载频
        public ItemStateProvider DownFreqStateProvider { get; private set; } //下行载频
        /// <summary>
        /// C0 
        /// </summary>
        public ItemStateProvider CTCS0StateProvider { get; private set; } 
        public ItemStateProvider CTCS3StateProvider { get; private set; } //CTCS3
        public ItemStateProvider CTCS3DStateProvider { get; private set; } //CTCS3D
        public ItemStateProvider OtherStateProvider { get; private set; } //其他 4
        public ItemStateProvider TrainIdStateProvider { get; private set; } //车次号 9
        public ItemStateProvider DriverIdStateProvider { get; private set; } //司机号 8
        public ItemStateProvider TrainDataStateProvider { get; private set; } //列车数据 10

        public ItemStateProvider EightTrucksStateProvider { get; private set; }//8辆

        public ItemStateProvider SixteenTrucksStateProvider { get; private set; }//16辆

        public ItemStateProvider NetNumberStateProvider { get; private set; }

        /// <summary>
        /// 网络号码
        /// </summary>
        IItemStateProvider IRegionFStateProvier.NetNumberStateProvider
        {
            get { return NetNumberStateProvider; }
        }

        public EnterableItemStateProvider ShuntingStateProvider { get; private set; } //调车 12
        public EnterableItemStateProvider OnSightStateProvider { get; private set; } //目视 13
        public ItemStateProvider StartStateProvider { get; private set; } //启动 5
        public ItemStateProvider RBCDataStateProvider { get; private set; }

        public ItemStateProvider RBCIDStateProvider { private set; get; }

        /// <summary>
        /// RBC数据ID
        /// </summary>
        IItemStateProvider IRegionFStateProvier.RBCIDStateProvider
        {
            get { return RBCIDStateProvider; }
        }

        public ItemStateProvider TelNumberStateProvider { get; private set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        IItemStateProvider IRegionFStateProvier.TelNumberStateProvider
        {
            get { return TelNumberStateProvider; }
        }

        public EnterableItemStateProvider CabsignalStateProvider { get; private set; } //机信 14
        public ItemStateProvider RelaseStateProvider { get; private set; }

        /// <summary>
        /// 执行制动测试
        /// </summary>
        public ItemStateProvider RunBrakeTestStateProvider { get; private set; }


        public ItemStateProvider RunNormalBrakeTestStateProvider { get; private set; }

        /// <summary>
        /// 常用制动
        /// </summary>
        IItemStateProvider IRegionFStateProvier.RunNormalBrakeTestStateProvider
        {
            get { return RunNormalBrakeTestStateProvider; }
        }

        public ItemStateProvider RunEmergencyBrakeTestStateProvider { get; private set; }

        /// <summary>
        /// 紧急制动
        /// </summary>
        IItemStateProvider IRegionFStateProvier.RunEmergencyBrakeTestStateProvider
        {
            get { return RunEmergencyBrakeTestStateProvider; }
        }

        public ItemStateProvider DMITestStateProvider { get; private set; }

        /// <summary>
        /// 退出制动测试
        /// </summary>
        public ItemStateProvider QuitTestStateProvider { get; private set; }

        IItemStateProvider IRegionFStateProvier.QuitTestStateProvider { get { return QuitTestStateProvider; } }

        /// <summary>
        /// DMI测试
        /// </summary>
        IItemStateProvider IRegionFStateProvier.DMITestStateProvider
        {
            get { return DMITestStateProvider; }
        }

        /// <summary>
        /// 略过制动测试
        /// </summary>
        public ItemStateProvider SkipRunBrakeTestStateProvider { get; private set; }

        //缓解 6
        public ItemStateProvider ControlModelStateProvider { get; private set; } //模式 1

        /// <summary>
        /// 进入等级选择
        /// </summary>
        public ItemStateProvider CTCSStateProvider { get; private set; } 
        public ItemStateProvider CTCS2StateProvider { get; private set; } //CTCS2
        public ItemStateProvider VigilantStateProvider { get; private set; }

        //警惕 7

        IItemStateProvider IRegionFStateProvier.EightTrucksStateProvider
        {
            get { return EightTrucksStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.SixteenTrucksStateProvider
        {
            get { return SixteenTrucksStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.DownFreqStateProvider
        {
            get { return DownFreqStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.UpFreqStateProvider
        {
            get { return UpFreqStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.CTCSStateProvider
        {
            get { return CTCSStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.CTCS0StateProvider
        {
            get { return CTCS0StateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.CTCS2StateProvider
        {
            get { return CTCS2StateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.CTCS3StateProvider
        {
            get { return CTCS3StateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.CTCS3DStateProvider
        {
            get { return CTCS3DStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.ControlModelStateProvider
        {
            get { return ControlModelStateProvider; }
        }


        IItemStateProvider IRegionFStateProvier.DataStateProvider
        {
            get { return DataStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.FreqStateProvider
        {
            get { return FreqStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.OtherStateProvider
        {
            get { return OtherStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.VigilantStateProvider
        {
            get { return VigilantStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.DriverIdStateProvider
        {
            get { return DriverIdStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.TrainIdStateProvider
        {
            get { return TrainIdStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.TrainDataStateProvider
        {
            get { return TrainDataStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.RBCDataStateProvider
        {
            get { return RBCDataStateProvider; }
        }

        IEnterableItemStateProvider IRegionFStateProvier.ShuntingStateProvider
        {
            get { return ShuntingStateProvider; }
        }

        IEnterableItemStateProvider IRegionFStateProvier.OnSightStateProvider
        {
            get { return OnSightStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.StartStateProvider
        {
            get { return StartStateProvider; }
        }

        IEnterableItemStateProvider IRegionFStateProvier.CabsignalStateProvider
        {
            get { return CabsignalStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.RelaseStateProvider
        {
            get { return RelaseStateProvider; }
        }

        /// <summary>
        /// 执行制动测试
        /// </summary>
        IItemStateProvider IRegionFStateProvier.RunBrakeTestStateProvider
        {
            get { return RunBrakeTestStateProvider; }
        }

        /// <summary>
        /// 略过制动测试
        /// </summary>
        IItemStateProvider IRegionFStateProvier.SkipRunBrakeTestStateProvider
        {
            get { return SkipRunBrakeTestStateProvider; }
        }

        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (value == m_Visible)
                {
                    return;
                }

                m_Visible = value;
                RaisePropertyChanged(() => Visible);
            }
        }

        bool IVisibility.Visible
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void SetAllVisible(bool visible)
        {
            DataStateProvider.Visible = visible;
            FreqStateProvider.Visible = visible;
            CTCSStateProvider.Visible = visible;
            OtherStateProvider.Visible = visible;
            TrainIdStateProvider.Visible = visible;
            DriverIdStateProvider.Visible = visible;
            TrainDataStateProvider.Visible = visible;
            ShuntingStateProvider.Visible = visible;
            OnSightStateProvider.Visible = visible;
            StartStateProvider.Visible = visible;
            RBCDataStateProvider.Visible = visible;
            CabsignalStateProvider.Visible = visible;
            RelaseStateProvider.Visible = visible;
            ControlModelStateProvider.Visible = visible;
            CTCS2StateProvider.Visible = visible;
            CTCS3StateProvider.Visible = visible;
            CTCS3DStateProvider.Visible = visible;
            UpFreqStateProvider.Visible = visible;
            DownFreqStateProvider.Visible = visible;
            VigilantStateProvider.Visible = visible;
            RunBrakeTestStateProvider.Visible = visible;
            SkipRunBrakeTestStateProvider.Visible = visible;
            RBCIDStateProvider.Visible = visible;
            TelNumberStateProvider.Visible = visible;
            NetNumberStateProvider.Visible = visible;
            RunNormalBrakeTestStateProvider.Visible = visible;
            RunEmergencyBrakeTestStateProvider.Visible = visible;
            DMITestStateProvider.Visible = visible;
            QuitTestStateProvider.Visible = visible;
            EightTrucksStateProvider.Visible = visible;
            SixteenTrucksStateProvider.Visible = visible;
        }
    }
}