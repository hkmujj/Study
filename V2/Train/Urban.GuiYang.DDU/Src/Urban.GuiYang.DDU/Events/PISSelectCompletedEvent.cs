using System.Diagnostics;
using Microsoft.Practices.Prism.Events;
using Urban.GuiYang.DDU.Model.PIS;

namespace Urban.GuiYang.DDU.Events
{
    public class PISSelectCompletedEvent: CompositePresentationEvent<PISSelectCompletedEvent.Args>
    {
        public class Args
        {
            [DebuggerStepThrough]
            public Args(SelectedSettingFlag selectedSettingFlag, object selectedContent)
            {
                SelectedSettingFlag = selectedSettingFlag;
                SelectedContent = selectedContent;
            }

            public SelectedSettingFlag SelectedSettingFlag { get; private set; }

            public object SelectedContent { get; private set; }

        }
    }
}