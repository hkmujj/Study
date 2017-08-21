using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using MMI.Facility.DataType.Config;
using MMI.Facility.Interface.Data;
using MMITool.Addin.MMIConfiguration.Controller;
using MMITool.Addin.MMIConfiguration.Model;

namespace MMITool.Addin.MMIConfiguration.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SystemConfigViewModel : ConfigureContentEditerViewModeBase<SystemConfigController>
    {
        [ImportingConstructor]
        public SystemConfigViewModel(SystemConfigController controller, SystemConfigModel model)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            //TODO delete test code
            model.SystemConfig  = new SystemConfig(){ IsDebugModel = true, StartModel = StartModel.Edit, SubsystemConfigs = new ObservableCollection<SubsystemConfig>()};
        }

        public SystemConfigModel Model { private set; get; }

    }
}