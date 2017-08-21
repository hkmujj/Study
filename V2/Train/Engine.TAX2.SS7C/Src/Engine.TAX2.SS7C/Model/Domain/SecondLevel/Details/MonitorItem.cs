using System.Diagnostics;
using Engine.TAX2.SS7C.Model.ConfigModel;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.Model.Domain.SecondLevel.Details
{
    [DebuggerDisplay("Content={ItemConfig.Content}")]
    public class MonitorItem : NotificationObject
    {
        [DebuggerStepThrough]
        public MonitorItem(SetMonitorItemConfig itemConfig)
        {
            ItemConfig = itemConfig;
        }

        public SetMonitorItemConfig ItemConfig { get; private set; }
    }
}