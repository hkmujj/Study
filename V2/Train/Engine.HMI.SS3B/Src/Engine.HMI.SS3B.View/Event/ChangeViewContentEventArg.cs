using System.Diagnostics;

namespace Engine.HMI.SS3B.View.Event
{
    public class ChangeViewContentEventArg
    {
        [DebuggerStepThrough]
        public ChangeViewContentEventArg(string regionName, string viewName)
        {
            RegionName = regionName;
            ViewName = viewName;
        }

        public string RegionName { private set; get; }

        public string ViewName { private set; get; }
    }
}