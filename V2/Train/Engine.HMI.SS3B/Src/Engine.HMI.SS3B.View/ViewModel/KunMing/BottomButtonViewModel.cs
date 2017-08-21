using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using Engine.HMI.SS3B.View.Constance;
using Engine.HMI.SS3B.View.Event;

namespace Engine.HMI.SS3B.View.ViewModel.KunMing
{

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class BottomButtonViewModel : ViewModelBase
    {
        public string ViewName { get; set; }
        public void ChangedViewContent(string viewName)
        {
            ChangeViewContentEvent.Publish(new ChangeViewContentEventArg(KunMingRegionNames.ViewContent, viewName));
        }

        public void ChangedMainContent(string viewName)
        {
            ChangeViewContentEvent.Publish(new ChangeViewContentEventArg(KunMingRegionNames.MainContent, viewName));
            if (viewName == KunMingViewFullNames.ModelSelectPage)
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                    if (ViewName == KunMingViewFullNames.ModelSelectPage)
                    {
                        ChangedMainContent(KunMingViewFullNames.Main);
                        ChangedViewContent(KunMingViewFullNames.MasterMainPage);
                    }
                });
            }
            ViewName = viewName;
        }

    }
}