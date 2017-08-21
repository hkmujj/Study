using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using LightRail.HMI.SZLHLF.Controller;
using LightRail.HMI.SZLHLF.Interface;

namespace LightRail.HMI.SZLHLF.Model.SettingModel
{
    [Export]
    [Export(typeof(IModels))]
    public class StationSetInfoModel : ModelBase
    {
        private ObservableCollection<StationItem> m_AllStartStationItem;
        private ObservableCollection<StationItem> m_AllEndStationItem;

        [ImportingConstructor]
        StationSetInfoModel(StationSetController settingController)
        {
            AllStartStationItem = new ObservableCollection<StationItem>(GlobalParam.Instance.StationItem.OrderBy(o => o.Index));
            AllEndStationItem = new ObservableCollection<StationItem>(GlobalParam.Instance.StationItem.OrderBy(o => o.Index));
            
            SettingController = settingController;
            SettingController.ViewModel = new Lazy<StationSetInfoModel>(() => this);
            settingController.Initalize();
        }

        public override void Initialize()
        {
            SettingController.Initalize();
        }

        public override void Clear()
        {
            SettingController.Clear();
        }

        /// <summary>
        /// 控制
        /// </summary>
        public StationSetController SettingController { get; set; }
        
        /// <summary>
        /// 站台管理
        /// </summary>
        public ObservableCollection<StationItem> AllStartStationItem
        {
            get { return m_AllStartStationItem; }
            set
            {
                if (Equals(value, m_AllStartStationItem))
                {
                    return;
                }
                m_AllStartStationItem = value;
                RaisePropertyChanged(() => AllStartStationItem);
            }
        }

        /// <summary>
        /// 站台管理
        /// </summary>
        public ObservableCollection<StationItem> AllEndStationItem
        {
            get { return m_AllEndStationItem; }
            set
            {
                if (Equals(value, m_AllEndStationItem))
                {
                    return;
                }
                m_AllEndStationItem = value;
                RaisePropertyChanged(() => AllEndStationItem);
            }
        }
        
    }
}
