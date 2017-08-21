using System.Diagnostics;

namespace Engine.LCDM.HDX2.Entity.Events
{

    [DebuggerDisplay("ToViewType={ToViewType}")]
    public class RequestChangeMainFooterRegionViewEventArge
    {
        [DebuggerStepThrough]
        public RequestChangeMainFooterRegionViewEventArge(MainFooterRegionViewType toViewType)
        {
            ToViewType = toViewType;
        }

        public MainFooterRegionViewType ToViewType { private set; get; }

    }

    public enum MainFooterRegionViewType
    {
        DateTimeInfo,

        Setting,
    }
}