using System.ComponentModel.Composition;
using LightRail.HMI.SZLHLF.Model;
using LightRail.HMI.SZLHLF.Model.AirModel;
using LightRail.HMI.SZLHLF.Model.BroadcastControlModel;
using LightRail.HMI.SZLHLF.Model.EmergencyBroadcastModel;
using LightRail.HMI.SZLHLF.Model.MaintainModel;
using LightRail.HMI.SZLHLF.Model.NetWorkModel;
using LightRail.HMI.SZLHLF.Model.SettingModel;

namespace LightRail.HMI.SZLHLF.ViewModel
{
    [Export]
    public class TrainInfoViewModel : ModelBase
    {
        [ImportingConstructor]
        public TrainInfoViewModel(TrainInfoModel model, EnmergencyTalkModel enmergencyTalkModel, EmergencyBroadcastInfoModel emergencyBroadcastInfoModel,
            AirConditionModel airConditionModel, SettingInfoModel settingInfoModel, StationSetInfoModel stationSetInfoModel, NetWorkInfoModel netWorkInfoModel,
            InputKeyInfoModel inputKeyInfoModel)
        {
            Model = model;
            EnmergencyTalkModel = enmergencyTalkModel;
            EmergencyBroadcastInfoModel = emergencyBroadcastInfoModel;
            AirConditionModel = airConditionModel;
            SettingInfoModel = settingInfoModel;
            StationSetInfoModel = stationSetInfoModel;
            NetWorkInfoModel = netWorkInfoModel;
            InputKeyInfoModel = inputKeyInfoModel;
        }

        public TrainInfoModel Model { private set; get; }

        public EnmergencyTalkModel EnmergencyTalkModel { private set; get; }

        public EmergencyBroadcastInfoModel EmergencyBroadcastInfoModel { private set; get; }

        public AirConditionModel AirConditionModel { private set; get; }

        public SettingInfoModel SettingInfoModel { private set; get; }

        public StationSetInfoModel StationSetInfoModel { private set; get; }

        public NetWorkInfoModel NetWorkInfoModel { private set; get; }

        public InputKeyInfoModel InputKeyInfoModel { private set; get; }
    }
}
