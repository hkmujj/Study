using MMI.Facility.WPFInfrastructure.Behaviors;

namespace LightRail.HMI.SZLHLF.Event
{
    public class ViewChangedEventArgs
    {
        public ViewExportAttribute Att { get; set; }
        public string FullName { get; set; }
    }
}
