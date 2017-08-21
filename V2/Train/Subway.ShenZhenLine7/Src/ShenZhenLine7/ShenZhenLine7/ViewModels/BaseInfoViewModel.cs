using System.ComponentModel.Composition;
using Subway.ShenZhenLine7.Controller.ViewModelController;

namespace Subway.ShenZhenLine7.ViewModels
{
    /// <summary>
    /// 基础信息
    /// </summary>
    [Export]
    public class BaseInfoViewModel : ViewModelBase
    {
        private string m_CurrenStation;
        private double m_Storage;
        private double m_NetPress;
        private string m_EndStation;
        private string m_NextStation;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        [ImportingConstructor]
        public BaseInfoViewModel(BaseInfoController controller)
        {
            controller.ViewModel = this;
            Controller = controller;
        }

        /// <summary>
        /// 控制类
        /// </summary>
        public BaseInfoController Controller { get; private set; }
        /// <summary>
        /// 当前站
        /// </summary>
        public string CurrenStation
        {
            get { return m_CurrenStation; }
            set
            {
                if (value == m_CurrenStation)
                {
                    return;
                }
                m_CurrenStation = value;
                RaisePropertyChanged(() => CurrenStation);
            }
        }

        /// <summary>
        /// 下一站
        /// </summary>
        public string NextStation
        {
            get { return m_NextStation; }
            set
            {
                if (value == m_NextStation)
                {
                    return;
                }
                m_NextStation = value;
                RaisePropertyChanged(() => NextStation);
            }
        }

        /// <summary>
        /// 终点站
        /// </summary>
        public string EndStation
        {
            get { return m_EndStation; }
            set
            {
                if (value == m_EndStation)
                {
                    return;
                }
                m_EndStation = value;
                RaisePropertyChanged(() => EndStation);
            }
        }

        /// <summary>
        /// 网压
        /// </summary>
        public double NetPress
        {
            get { return m_NetPress; }
            set
            {
                if (value == m_NetPress)
                {
                    return;
                }
                m_NetPress = value;
                RaisePropertyChanged(() => NetPress);
            }
        }

        /// <summary>
        /// 蓄电池电压
        /// </summary>
        public double Storage
        {
            get { return m_Storage; }
            set
            {
                if (value == m_Storage)
                {
                    return;
                }
                m_Storage = value;
                RaisePropertyChanged(() => Storage);
            }
        }
    }
}