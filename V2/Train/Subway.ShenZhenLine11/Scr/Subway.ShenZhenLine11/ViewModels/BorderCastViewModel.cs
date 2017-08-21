using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Excel.Interface;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Constance;
using Subway.ShenZhenLine11.Controller;

namespace Subway.ShenZhenLine11.ViewModels
{
    [Export]
    public class BorderCastViewModel : SubViewModelBase
    {
        private ObservableCollection<StationUnit> m_AllStation;
        private string m_EndStation;
        private string m_NextStation;
        private string m_StartStation;
        private bool m_IsAuto;

        [ImportingConstructor]
        public BorderCastViewModel(BorderCastController controller)
        {
            Controller = controller;
            controller.ViewModel = this;
            var tmp = ExcelParser.Parser<StationUnit>(GlobalParam.Instance.InitPara.AppConfig.AppPaths.ConfigDirectory);
            AllStation = new ObservableCollection<StationUnit>(tmp);
            Instance = this;
        }
        public static BorderCastViewModel Instance { get; private set; }
        public BorderCastController Controller { get; private set; }
        public ObservableCollection<StationUnit> AllStation
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
                RaisePropertyChanged(() => IsAuto);
            }
        }

        public override void Clear()
        {
            IsAuto = true;
        }
    }
}