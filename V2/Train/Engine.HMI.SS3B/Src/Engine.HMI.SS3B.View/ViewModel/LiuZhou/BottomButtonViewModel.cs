using System.ComponentModel.Composition;
using Engine.HMI.SS3B.View.Constance;
using Engine.HMI.SS3B.View.Event;

namespace Engine.HMI.SS3B.View.ViewModel.LiuZhou
{

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class BottomButtonViewModel : ViewModelBase
    {

        public void ChangedViewContent(string viewName)
        {
            ChangeViewContentEvent.Publish(new ChangeViewContentEventArg(LiuZhouRegionNames.ViewContent, viewName));
            
        }

        public string ChangedMainContent(string viewName)
        {
            ChangeViewContentEvent.Publish(new ChangeViewContentEventArg(LiuZhouRegionNames.MainContent, viewName));
            return viewName;
        }

    }
}