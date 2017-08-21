using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Controller;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Models.Units;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    /// 紧急广播
    /// </summary>
    [Export]
    [Export(typeof(IModels))]
    public class EmergencyBordercastModel : ModelBase
    {
        private ObservableCollection<EmergencyBordercastUnit> m_Units;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="controller"></param>
        [ImportingConstructor]
        public EmergencyBordercastModel(EmergencyBorderCasetController controller)
        {
            Controller = controller;
            controller.ViewModel = this;
            Instance = this;
            Units = new ObservableCollection<EmergencyBordercastUnit>(GlobalParams.Instance.EmergencyBordercastUnits); ;
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        public override void Initialize()
        {
            Controller.InitData();
        }

        /// <summary>
        /// 所有紧急广播
        /// </summary>
        public ObservableCollection<EmergencyBordercastUnit> Units
        {
            get { return m_Units; }
            set
            {
                if (Equals(value, m_Units))
                {
                    return;
                }
                m_Units = value;
                RaisePropertyChanged(() => Units);
            }
        }

        /// <summary>
        /// 控制
        /// </summary>
        public EmergencyBorderCasetController Controller { get; private set; }

        /// <summary>
        /// 静态访问类
        /// </summary>
        public static EmergencyBordercastModel Instance { get; private set; }
    }
}