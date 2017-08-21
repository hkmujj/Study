using System.Xml.Serialization;
using CRH2MMI.AxisTemperature;
using CRH2MMI.AxleTemperature;
using CRH2MMI.BPRescue;
using CRH2MMI.BrakeInfo;
using CRH2MMI.BreakLocked;
using CRH2MMI.CarInfo;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Screen;
using CRH2MMI.Common.View.Train;
using CRH2MMI.CutState;
using CRH2MMI.Deliver;
using CRH2MMI.DoorInfo;
using CRH2MMI.DriveState;
using CRH2MMI.Fault;
using CRH2MMI.Jack;
using CRH2MMI.LightTrans;
using CRH2MMI.Menu;
using CRH2MMI.Notify;
using CRH2MMI.PowerClassify;
using CRH2MMI.PowerVoltage;
using CRH2MMI.Rac;
using CRH2MMI.TelentControl;
using CRH2MMI.Title;
using CRH2MMI.Tow1;
using CRH2MMI.Tow2;
using CRH2MMI.Vigilant;
using CRH2MMI.WorkState;

namespace CRH2MMI.Config.ConfigModel
{
    /// <summary>
    /// 配置数据
    /// </summary>
    public class CRH2ConfigData
    {
        [XmlAttribute]
        public CRH2Type Type { set; get; }

        [XmlElement]
        public EspecialConfig EspecialConfig { get; set; }

        [XmlElement]
        public GlobalInfoConfig GlobalInfoConfig { set; get; }

        [XmlElement]
        public StartViewConfig StartViewConfig { set; get; }

        [XmlElement]
        public FaultConfig FaultConfig { set; get; }

        [XmlElement]
        public TMSPowerConfig TMSPowerConfig { set; get; }

        /// <summary>
        /// TrainView 的配置
        /// </summary>
        [XmlElement]
        public CRH2TrainConfig TrainConfig { set; get; }

        /// <summary>
        /// 主菜单的配置
        /// </summary>
        [XmlElement("MenuView")]
        public MenuConfig MenuConfig { set; get; }

        /// <summary>
        /// 标题栏
        /// </summary>
        [XmlElement]
        public TitleConfig TitleConfig { set; get; }

        /// <summary>
        /// 工况
        /// </summary>
        [XmlElement]
        public WorkStateConfig WorkStateConfig { set; get; }

        /// <summary>
        /// 车辆信息的配置
        /// </summary>
        [XmlElement]
        public CarInfoConfig CarInfoConfig { set; get; }

        /// <summary>
        /// 行驶状态
        /// </summary>
        [XmlElement]
        public DriveStateConfig DriveStateConfig { set; get; }

        /// <summary>
        /// 配电盘信息
        /// </summary>
        [XmlElement]
        public JackConfig JackConfig { set; get; }

        /// <summary>
        /// 出库信息
        /// </summary>
        [XmlElement]
        public DeliverConfig DeliverConfig { set; get; }

        /// <summary>
        /// 切除状态配置
        /// </summary>
        [XmlElement]
        public CutInfoConfig CutInfoConfig { set; get; }

        /// <summary>
        /// 制动信息配置
        /// </summary>
        [XmlElement]
        public BrakeInfoConfig BrakeInfoConfig { set; get; }

        /// <summary>
        /// 牵引变流器 编
        /// </summary>
        [XmlElement]
        public Tow1Config Tow1Config { set; get; }

        /// <summary>
        /// 牵引变流器 车
        /// </summary>
        [XmlElement]
        public TowInfo2Config TowInfo2Config { set; get; }

        /// <summary>
        /// 空转,滑行
        /// </summary>
        [XmlElement]
        public EmputyRollConfig EmputyRollConfig { set; get; }

        /// <summary>
        /// 供电分类
        /// </summary>
        [XmlElement]
        public PowerClassifyConfig PowerClassifyConfig { set; get; }

        /// <summary>
        /// 电源电压
        /// </summary>
        [XmlElement]
        public PowerVoltageConfig PowerVoltageConfig { set; get; }

        /// <summary>
        /// 车门配置
        /// </summary>
        [XmlElement]
        public DoorConfig DoorConfig { set; get; }

        /// <summary>
        /// 光传输状态
        /// </summary>
        [XmlElement()]
        public LightTrainsConfig LightTrainsConfig { set; get; }

        /// <summary>
        /// 轴温切除
        /// </summary>
        [XmlElement]
        public AxisTemperatureConfig AxisTemperatureConfig { set; get; }

        /// <summary>
        /// 抱死切除 
        /// </summary>
        [XmlElement]
        public BrakeLockedConfig BrakeLockedConfig { set; get; }

        /// <summary>
        /// 远程控制切除
        /// </summary>
        [XmlElement]
        public TeleControlConfig TeleControlConfig { set; get; }

        /// <summary>
        /// 警惕
        /// </summary>
        [XmlElement]
        public VigilantConfig VigilantConfig { set; get; }

        /// <summary>
        /// BP 救援
        /// </summary>
        [XmlElement]
        public BPConfig BPRescueConfig { set; get; }

        /// <summary>
        /// 通知状态 
        /// </summary>
        [XmlElement]
        public NotifyConfig NotifyConfig { set; get; }

        /// <summary>
        /// 实时轴温检测
        /// </summary>
        [XmlElement]
        public AxleTemperatureDetailConfig AxleTemperatureDetailConfig { set; get; }

        /// <summary>
        /// 连接头罩信息
        /// </summary>
        [XmlElement]
        public ConnectHoodInfoConfig ConnectHoodInfoConfig { get; set; }

        [XmlElement]
        public GrandTotalPowerConfig GrandTotalPowerConfig { get; set; }

        /// <summary>
        /// 撒砂
        /// </summary>
        [XmlElement]
        public SprinkleSandConfig SprinkleSandConfig { set; get; }
    }

}


