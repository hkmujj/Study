using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using LightRail.HMI.SZLHLF.Controller;
using LightRail.HMI.SZLHLF.Interface;
using LightRail.HMI.SZLHLF.Model.Units;

namespace LightRail.HMI.SZLHLF.Model.SettingModel
{
    [Export]
    [Export(typeof(IModels))]
    public class SettingInfoModel : ModelBase
    {
        private float m_Light;

        [ImportingConstructor]
        SettingInfoModel(SettingController settingController)
        {
            AllRoadLineSet = new ObservableCollection<RoadLineSetUnit>(GlobalParam.Instance.RoadLineSetUnit.OrderBy(o => o.ID));
            AllStationBroadcasModeltSet = new ObservableCollection<StationBroadcasModeltSetUnit>(GlobalParam.Instance.StationBroadcasModeltSetUnit.OrderBy(o => o.ID));
            
            SettingController = settingController;
            SettingController.ViewModel = new Lazy<SettingInfoModel>(()=>this);
            SettingController.Initalize();
        }

        public override void Initialize()
        {
            SettingController.Initalize();
        }

        public override void Clear()
        {
            foreach (var VARIABLE in AllStationBroadcasModeltSet)
            {
                VARIABLE.IsChecked = false;
            }
            SettingController.Clear();
        }

        public SettingController SettingController { get; set; }
        
        /// <summary>
        /// 亮度
        /// </summary>
        public float Light
        {
            get { return m_Light; }
            set
            {
                if (value == m_Light)
                {
                    return;
                }
                m_Light = value;
                RaisePropertyChanged(() => Light);
            }
        }

        /// <summary>
        /// 线路选择
        /// </summary>
        public ObservableCollection<RoadLineSetUnit> AllRoadLineSet { get; set; }

        /// <summary>
        /// 报站控制设置
        /// </summary>
        public ObservableCollection<StationBroadcasModeltSetUnit> AllStationBroadcasModeltSet { get; set; }


        
    }
}
