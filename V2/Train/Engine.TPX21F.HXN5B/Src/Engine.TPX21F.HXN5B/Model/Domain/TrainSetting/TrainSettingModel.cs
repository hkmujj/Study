using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Model.Domain.TrainSetting.Detail;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.TrainSetting
{
    [Export]
    public class TrainSettingModel : NotificationObject
    {
        [ImportingConstructor]
        public TrainSettingModel(TrainAutoStartModel autoStartModel, CountMileageModel countMileageModel, LowConstSpeedModel lowConstSpeedModel)
        {
            AutoStartModel = autoStartModel;
            CountMileageModel = countMileageModel;
            LowConstSpeedModel = lowConstSpeedModel;
        }

        public TrainAutoStartModel AutoStartModel { get; private set; }

        public CountMileageModel CountMileageModel { get; private set; }

        public LowConstSpeedModel LowConstSpeedModel { get; private set; }
    }
}