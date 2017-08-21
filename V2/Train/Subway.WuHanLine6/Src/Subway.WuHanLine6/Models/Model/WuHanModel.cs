using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Subway.WuHanLine6.Constance;
using Subway.WuHanLine6.Interfaces;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    ///     武汉地铁Model
    /// </summary>
    [Export(ClassExportName.WuHanModel, typeof(IModels))]
    public class WuHanModel : ModelBase
    {
        private IStateInterface m_CurrentStateInterface;
        private Visibility m_MMIBlack;

        /// <summary>
        ///     构造函数
        /// </summary>
        public WuHanModel()
        {
            MMIBlack = Visibility.Hidden;
        }

        /// <summary>
        ///     标题
        /// </summary>
        [Import]
        public TitleModel TitleModel { get; private set; }

        /// <summary>
        ///     车
        /// </summary>
        [Import]
        public CarModel CarModel { get; private set; }

        /// <summary>
        ///     运行
        /// </summary>
        [Import]
        public RunModel RunModel { get; private set; }

        /// <summary>
        ///     旁路
        /// </summary>
        [Import]
        public BypassModel BypassModel { get; private set; }

        /// <summary>
        ///     制动状态
        /// </summary>
        [Import]
        public BrakeStatusModel BrakeStatusModel { get; private set; }

        /// <summary>
        ///     牵引状态
        /// </summary>
        [Import]
        public TractionStatusModel TractionStatusModel { get; private set; }

        /// <summary>
        ///     辅助状态
        /// </summary>
        [Import]
        public AssistStatusModel AssistStatusModel { get; private set; }

        /// <summary>
        ///     空调状态
        /// </summary>
        [Import]
        public AirConditionModel AirConditionModel { get; private set; }

        /// <summary>
        ///     牵引封锁
        /// </summary>
        [Import]
        public TractionLocakModel TractionLocakModel { get; private set; }

        /// <summary>
        ///     紧急广播
        /// </summary>
        [Import]
        public EmergencyBordercastModel EmergencyBordercastModel { get; private set; }

        /// <summary>
        ///     烟火报警
        /// </summary>
        [Import]
        public SmokeModel SmokeModel { get; private set; }

        /// <summary>
        ///     紧急对讲
        /// </summary>
        [Import]
        public EmergencyTalkModel EmergencyTalkModel { get; private set; }

        /// <summary>
        ///     网络拓扑
        /// </summary>
        [Import]
        public NetWorkModel NetWorkModel { get; private set; }

        /// <summary>
        /// 故障M
        /// </summary>
        [Import]
        public FaultInfoModel FaultInfoModel { get; private set; }
        /// <summary>
        /// 亮度调节
        /// </summary>
        [Import]
        public LightSettingViewModel LightSettingViewModel { get; private set; }

        /// <summary>
        /// 牵引辅助切除复位
        /// </summary>
        [Import]
        public TractionAssistCutModel TractionAssistCutModel { get; private set; }

        /// <summary>
        /// 制动自检
        /// </summary>
        [Import]
        public BrakeAutoCheckModel BrakeAutoCheckModel { get; private set; }
        /// <summary>
        /// 密码输入
        /// </summary>
        [Import]
        public PasswordModel PasswordModel { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        [Import]
        public StationModel StationModel { get; private set; }

        /// <summary>
        /// </summary>
        public IStateInterface CurrentStateInterface
        {
            get { return m_CurrentStateInterface; }
            private set
            {
                if (Equals(value, m_CurrentStateInterface))
                {
                    return;
                }
                m_CurrentStateInterface = value;
                m_CurrentStateInterface.UpdateState();
                RaisePropertyChanged(() => CurrentStateInterface);
            }
        }

        /// <summary>
        ///     黑屏控制
        /// </summary>
        public Visibility MMIBlack
        {
            get { return m_MMIBlack; }
            set
            {
                if (value == m_MMIBlack)
                {
                    return;
                }
                m_MMIBlack = value;
                if (value == Visibility.Visible)
                {
                    ModelList.ForEach(f => f.Value.Initialize());
                }
                RaisePropertyChanged(() => MMIBlack);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [ImportMany]
        public List<Lazy<IModels>> ModelList { get; private set; }
        /// <summary>
        ///     更新当前界面
        /// </summary>
        /// <param name="current">当前界面</param>
        public void UpdateCurrent(IStateInterface current)
        {
            if (current == null)
            {
                return;
            }
            CurrentStateInterface = current;
        }

        /// <summary>
        ///     初始化方法
        /// </summary>
        public override void Initialize()
        {
            MMIBlack = Visibility.Hidden;
        }
    }
}