using System.Diagnostics;

namespace Subway.XiaMenLine1.Subsystem.Events
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