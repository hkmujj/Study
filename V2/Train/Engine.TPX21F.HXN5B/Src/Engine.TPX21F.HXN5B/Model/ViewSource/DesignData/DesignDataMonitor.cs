using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Engine.TPX21F.HXN5B.Model.ConfigModel;
using Engine.TPX21F.HXN5B.Model.Domain.Monitor.Detail;

namespace Engine.TPX21F.HXN5B.Model.ViewSource.DesignData
{
    public class DesignDataMonitor
    {
        public static readonly DesignDataMonitor Instance = new DesignDataMonitor();

        public Lazy<MonitorPage<AssistMachineMonitorItem>> MonitorAssistMachinePage { private set; get; }

        public List<MonitorPage> Monitors { private set; get; }

        private DesignDataMonitor()
        {
            Monitors = new List<MonitorPage>()
            {
                new MonitorPage(new ReadOnlyCollection<MonitorItem>(new List<MonitorItem>()
                {
                    new MonitorItem(new MonitorItemConfig("一架逆变启动柴油机转速r/min")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准2")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准3")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准4")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准5")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准6")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准7")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准8")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准9")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准10")) {ContentString = "999.999"},
                })),
                new MonitorPage(new ReadOnlyCollection<MonitorItem>(new List<MonitorItem>()
                {
                    new MonitorItem(new MonitorItemConfig("自负荷基准1")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准2")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准3")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准4")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准5")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准6")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准7")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准8")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准9")) {ContentString = "999.999"},
                    new MonitorItem(new MonitorItemConfig("自负荷基准10")) {ContentString = "999.999"},
                })),
            };

            MonitorAssistMachinePage = new Lazy<MonitorPage<AssistMachineMonitorItem>>(() =>
                new MonitorPage<AssistMachineMonitorItem>(
                    new ReadOnlyCollection<AssistMachineMonitorItem>(new List<AssistMachineMonitorItem>()
                    {
                        new AssistMachineMonitorItem(new MonitorAssistMachineItemConfig("一架逆变启动柴油机转速r/min")) {ContentString = "999.999"},
                        new AssistMachineMonitorItem(new MonitorAssistMachineItemConfig("自负荷基准2")) {ContentString = "999.999"},
                        new AssistMachineMonitorItem(new MonitorAssistMachineItemConfig("自负荷基准3")) {ContentString = "999.999"},
                        new AssistMachineMonitorItem(new MonitorAssistMachineItemConfig("自负荷基准4")) {ContentString = "999.999"},
                        new AssistMachineMonitorItem(new MonitorAssistMachineItemConfig("自负荷基准5")) {ContentString = "999.999"},
                        new AssistMachineMonitorItem(new MonitorAssistMachineItemConfig("自负荷基准6")) {ContentString = "999.999"},
                        new AssistMachineMonitorItem(new MonitorAssistMachineItemConfig("自负荷基准7")) {ContentString = "999.999"},
                    }),7));
        }
    }
}