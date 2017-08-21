using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model
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
        }

        public ItemStateProvider DataStateProvider { get; private set; } //数据 0
        public ItemStateProvider FreqStateProvider { get; private set; } //载频 2
        public ItemStateProvider UpFreqStateProvider { get; private set; } //上行载频
        public ItemStateProvider DownFreqStateProvider { get; private set; } //下行载频
        public ItemStateProvider CTCS3StateProvider { get; private set; } //CTCS3
        public ItemStateProvider CTCS3DStateProvider { get; private set; } //CTCS3D
        public ItemStateProvider OtherStateProvider { get; private set; } //其他 4
        public ItemStateProvider TrainIdStateProvider { get; private set; } //车次号 9
        public ItemStateProvider DriverIdStateProvider { get; private set; } //司机号 8
        public ItemStateProvider TrainDataStateProvider { get; private set; } //列车数据 10
        public EnterableItemStateProvider ShuntingStateProvider { get; private set; } //调车 12
        public EnterableItemStateProvider OnSightStateProvider { get; private set; } //目视 13
        public ItemStateProvider StartStateProvider { get; private set; } //启动 5
        public ItemStateProvider RBCDataStateProvider { get; private set; } //RBC 11
        public EnterableItemStateProvider CabsignalStateProvider { get; private set; } //机信 14
        public ItemStateProvider RelaseStateProvider { get; private set; } //缓解 6
        public ItemStateProvider ControlModelStateProvider { get; private set; } //模式 1
        public ItemStateProvider CTCSStateProvider { get; private set; } //等级 3
        public ItemStateProvider CTCS2StateProvider { get; private set; } //CTCS2
        public ItemStateProvider VigilantStateProvider { get; private set; } //警惕 7

        IItemStateProvider IRegionFStateProvier.DownFreqStateProvider
        {
            get { return this.DownFreqStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.UpFreqStateProvider
        {
            get { return this.UpFreqStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.CTCS2StateProvider
        {
            get { return this.CTCS2StateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.CTCS3StateProvider
        {
            get { return this.CTCS3StateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.CTCS3DStateProvider
        {
            get { return this.CTCS3DStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.ControlModelStateProvider
        {
            get { return this.ControlModelStateProvider; }
        }


        IItemStateProvider IRegionFStateProvier.DataStateProvider
        {
            get { return DataStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.FreqStateProvider
        {
            get { return FreqStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.CTCSStateProvider
        {
            get { return CTCSStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.OtherStateProvider
        {
            get { return OtherStateProvider; }
        }

        IItemStateProvider IRegionFStateProvier.VigilantStateProvider
        {
            get { return this.VigilantStateProvider; }
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
        }
    }
}