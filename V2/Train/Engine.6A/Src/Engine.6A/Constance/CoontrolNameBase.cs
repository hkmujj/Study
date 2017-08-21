using Engine._6A.Views.Axle6;
using Engine._6A.Views.Axle8;
using Engine._6A.Views.Common;
using Engine._6A.Views.Common.Main;
using Engine._6A.Views.Common.SystemSetting.TrainInfo;

namespace Engine._6A.Constance
{
    public class CoontrolNameBase
    {
        public static readonly string StartingView = typeof(StartingView).FullName;
        public static readonly string MainContentView = typeof(MainContentView).FullName;
        public static readonly string BlackScreenView = typeof(BlackScreenView).FullName;

        public static readonly string CurrentDialView = typeof(CurrentDialView).FullName;
        public static readonly string MainView = typeof(MainView).FullName;


        public static readonly string MainContent = typeof(MainContent).FullName;
        public static readonly string DataMonitorShell = typeof(DataMonitorShell).FullName;
        public static readonly string Axle8DataMonitorShell = typeof(Axle8DataMonitorShell).FullName;
        public static readonly string VideoView = typeof(VideoView).FullName;
        public static readonly string FaultShell = typeof(FaultShell).FullName;
        public static readonly string Axle8FaultShell = typeof(Axle8FaultShell).FullName;
        public static readonly string SystemSettingShell = typeof(SystemSettingShell).FullName;


        public static readonly string MicrocomputerInfoView = typeof(MicrocomputerInfoView).FullName;
        public static readonly string MonitorView = typeof(MonitorView).FullName;
        public static readonly string OilLevelMeterTestView = typeof(OilLevelMeterTestView).FullName;
        public static readonly string ElectropsychometerTestView = typeof(ElectropsychometerTestView).FullName;
    }
}