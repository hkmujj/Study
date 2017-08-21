using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Urban.GuiYang.DDU.Model.PIS.Details
{

    [Export]
    public class ManaulSettingModel : NotificationObject
    {
        public ManaulSettingModel()
        {
            SettingModel = new SettingStationModel();
        }
        public SettingStationModel SettingModel { get; private set; }
    }
}