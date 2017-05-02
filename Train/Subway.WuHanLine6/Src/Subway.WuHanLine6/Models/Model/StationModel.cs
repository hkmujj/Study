using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Controller;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.FaultInfos;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    /// 车站
    /// </summary>
    [Export]
    [Export(typeof(IModels))]
    public class StationModel : ModelBase
    {
        private bool m_IsAuto;
        private bool m_IsManual;
        private string m_CurrentStartStation;
        private string m_NewStartStation;
        private string m_CurrentEndStation;
        private string m_NewEndStation;
        private ObservableCollection<StationInfo> m_AllStation;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="stationManager"></param>
        [ImportingConstructor]
        public StationModel(StationController controller, StationManager stationManager)
        {
            controller.ViewModel = this;
            Controller = controller;
            StationManager = stationManager;
            AllStation = new ObservableCollection<StationInfo>(stationManager.AllStation.Values);
            AllStation.ForEach(f => f.IsCheckedChanged += (b) => { SelectInfo = b; });
        }

        /// <summary>
        /// 
        /// </summary>
        public StationInfo SelectInfo { get; private set; }
        /// <summary>
        /// 初始化方法
        /// </summary>
        public override void Initialize()
        {
            IsAuto = true;
            IsManual = false;
            Model = States.StationModel.Auto;
            NewEndStation = "- -";
            NewStartStation = "- -";
            CurrentStartStation = "- -";
            CurrentEndStation = "- -";
            OutBoolKeys.OutBo手动模式.SendBoolData(false, false);
            OutBoolKeys.OutBo自动模式.SendBoolData(true, false);

        }

        /// <summary>
        /// 
        /// </summary>
        public StationController Controller { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public StationManager StationManager { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<StationInfo> AllStation
        {
            get { return m_AllStation; }
            private set
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
        /// 起始站  当前设定值
        /// </summary>
        public string CurrentStartStation
        {
            get { return m_CurrentStartStation; }
            set
            {
                if (value == m_CurrentStartStation)
                {
                    return;
                }
                m_CurrentStartStation = value;
                RaisePropertyChanged(() => CurrentStartStation);
            }
        }

        /// <summary>
        /// 起始站 新设定值
        /// </summary>
        public string NewStartStation
        {
            get { return m_NewStartStation; }
            set
            {
                if (value == m_NewStartStation)
                {
                    return;
                }
                m_NewStartStation = value;
                RaisePropertyChanged(() => NewStartStation);
            }
        }

        /// <summary>
        /// 终点站 当前设定值
        /// </summary>
        public string CurrentEndStation
        {
            get { return m_CurrentEndStation; }
            set
            {
                if (value == m_CurrentEndStation)
                {
                    return;
                }
                m_CurrentEndStation = value;
                RaisePropertyChanged(() => CurrentEndStation);
            }
        }

        /// <summary>
        /// 终点站 新设定值
        /// </summary>
        public string NewEndStation
        {
            get { return m_NewEndStation; }
            set
            {
                if (value == m_NewEndStation)
                {
                    return;
                }
                m_NewEndStation = value;
                RaisePropertyChanged(() => NewEndStation);
            }
        }

        /// <summary>
        /// 自动
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
                Controller.ModelChanged();
                RaisePropertyChanged(() => IsAuto);
            }
        }

        /// <summary>
        /// 手动
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
                RaisePropertyChanged(() => IsManual);
            }
        }

        /// <summary>
        /// 报站模式
        /// </summary>
        public States.StationModel Model { get; set; }
    }
}