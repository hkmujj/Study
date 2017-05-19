using System.Diagnostics;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Events
{
    public class ChangMainContentRegionEventArgs
    {
        [DebuggerStepThrough]
        public ChangMainContentRegionEventArgs(string targetViewName)
        {
            TargetViewName = targetViewName;
        }

        public string TargetViewName { private set; get; }
    }
}