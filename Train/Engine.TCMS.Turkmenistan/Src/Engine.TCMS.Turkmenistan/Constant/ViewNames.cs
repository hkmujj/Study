using Engine.TCMS.Turkmenistan.View.Contents.AxleTemperature;
using Engine.TCMS.Turkmenistan.View.Contents.ProgressBar;
using Engine.TCMS.Turkmenistan.View.Contents.RunParam;

namespace Engine.TCMS.Turkmenistan.Constant
{
    public class ViewNames
    {
        public static readonly string CurrentAxleView = typeof(CurrentAxleView).FullName;
        public static readonly string CurrentRunparamView = typeof(CurrentRunparamView).FullName;
        public static readonly string CurrentProgressbarView = typeof(ReconnectionProgressBarView).FullName;
        public static readonly string ReconnectionProgressBarView = typeof(CurrentProgressbarView).FullName;
        public static readonly string ReconnectionRunparamView = typeof(ReconnectionRunparamView).FullName;

    }
}