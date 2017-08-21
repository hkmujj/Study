using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.LCDM.HXD3.Events
{
    public class ViewChangedEventArgs
    {
        public ViewExportAttribute Att { get; set; }
        public string FullName { get; set; }
    }
}