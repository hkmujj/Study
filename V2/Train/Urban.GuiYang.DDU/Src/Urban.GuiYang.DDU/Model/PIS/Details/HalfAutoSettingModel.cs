using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace Urban.GuiYang.DDU.Model.PIS.Details
{
    [Export]
    public class HalfAutoSettingModel : NotificationObject
    {
        public HalfAutoSettingModel()
        {
            SettingModel = new SettingStationModel();
        }

        public SettingStationModel SettingModel { get; private set; }

        public DelegateCommand OkModifyCommand { get; set; }

        public DelegateCommand CancelModifyCommand { get; set; }

    }
}