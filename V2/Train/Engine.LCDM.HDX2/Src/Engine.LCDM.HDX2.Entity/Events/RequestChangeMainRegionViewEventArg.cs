using System.Diagnostics;

namespace Engine.LCDM.HDX2.Entity.Events
{
    [DebuggerDisplay("ToViewType={ToViewType}")]
    public class RequestChangeMainRegionViewEventArg
    {
        [DebuggerStepThrough]
        public RequestChangeMainRegionViewEventArg(MainRegionViewType toViewType)
        {
            ToViewType = toViewType;
        }

        public MainRegionViewType ToViewType { private set; get; }

    }

    public enum MainRegionViewType
    {
        MainView,

        SoftVersion,

        Maintenance,
    }
}