using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Subway.ShenZhenLine9.Controller.ViewModelController;
using Subway.ShenZhenLine9.Interfaces;
using Subway.ShenZhenLine9.Models;
using Subway.ShenZhenLine9.Service;

namespace Subway.ShenZhenLine9.ViewModels
{
    /// <summary>
    /// 广播ViewModel
    /// </summary>
    [Export]
    [Export(typeof(ICourceStatusChanged))]
    public class StationViewModel : ViewModelBase
    {
        private string m_StartStation;
        private string m_NextStation;
        private string m_EndStation;
        private bool m_IsLib;
        private bool m_IsManual;
        private bool m_IsAuto;
        private ObservableCollection<IStation> m_AllStation;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="controller"></param>
        [ImportingConstructor]
        public StationViewModel(StationController controller)
        {
            controller.ViewModel = this;
            Controller = controller;
            StationManagerService = GlobalParam.Instance.InitParam?.DataPackage.ServiceManager.GetService<StationManagerService>();
            if (StationManagerService != null)
            {
                AllStation = new ObservableCollection<IStation>(StationManagerService.GetAllStation());
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public StationManagerService StationManagerService { get; }
        /// <summary>
        /// 初始化运行数据
        /// </summary>
        public override void Start()
        {
            IsAuto = true;
            IsManual = false;
            IsLib = false;
        }

        /// <summary>
        /// 所有站点
        /// </summary>
        public ObservableCollection<IStation> AllStation
        {
            get { return m_AllStation; }
            set
            {
                if (Equals(value, m_AllStation))
                {
                    return;
                }
                m_AllStation = value;
                RaisePropertyChanged(() => AllStation);
            }
        }

        /// <summary>
        /// 控制
        /// </summary>
        public StationController Controller { get; }
        /// <summary>
        /// 始发站
        /// </summary>
        public string StartStation
        {
            get { return m_StartStation; }
            set
            {
                if (value == m_StartStation)
                {
                    return;
                }
                m_StartStation = value;
                RaisePropertyChanged(() => StartStation);
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
        /// 自动模式
        /// </summary>
        public bool IsAuto
        {
            get { return m_IsAuto; }
            set
            {
                if (value == m_IsAuto)
                {
                    return;
                }
                m_IsAuto = value;
                if (value)
                {
                    Controller.ModelChanged(1);
                }
                RaisePropertyChanged(() => IsAuto);
            }
        }

        /// <summary>
        /// 手动模式
        /// </summary>
        public bool IsManual
        {
            get { return m_IsManual; }
            set
            {
                if (value == m_IsManual)
                {
                    return;
                }

                m_IsManual = value;
                if (value)
                {
                    Controller.ModelChanged(2);
                }
                RaisePropertyChanged(() => IsManual);
            }
        }

        /// <summary>
        /// 库内报站
        /// </summary>
        public bool IsLib
        {
            get { return m_IsLib; }
            set
            {
                if (value == m_IsLib)
                {
                    return;
                }
                m_IsLib = value;
                if (value)
                {
                    Controller.ModelChanged(3);
                }
                RaisePropertyChanged(() => IsLib);
            }
        }
    }
}