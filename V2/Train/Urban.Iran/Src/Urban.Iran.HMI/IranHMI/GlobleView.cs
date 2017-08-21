using System;
using System.Drawing;
using System.IO;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Events;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class GlobleView : HMIBase
    {
        public static GlobleView Instance { private set; get; }

        public override string GetInfo()
        {
            return "GlobleView";
        }

        protected override bool Initalize()
        {
            Instance = this;

            EventManager.Instance.EventAdded += InstanceOnEventAdded;

            if (UIObj.InBoolList.Count < 2)
            {
                return false;
            }

            LoadStations();

            LoadFaultInfo();

            EventManager.Instance.InitalizeEventInfos(Path.Combine(AppPaths.ConfigDirectory, "伊朗事件信息.xls"));

            UIObj.InBoolList.AddRange(EventManager.Instance.AllEvent.Values.Select(s => s.LogicIndex));

            return true;
        }

        private void InstanceOnEventAdded(EventLife eventLife)
        {
            switch (eventLife.EventInfo.Priority)
            {
                case EventPriority.AAlarm:
                    break;
                case EventPriority.BAlarm:
                    break;
                case EventPriority.Event:
                    break;
                case EventPriority.Fault:
                    break;
                case EventPriority.Information:
                    ChangedPage((IranViewIndex)38);
                    break;
            }
        }

        private void LoadFaultInfo()
        {
            var file = Path.Combine(
                DataPackage.Config.AppConfigs.First(f => f.AppName == ProjectName).AppPaths.ConfigDirectory,
                "广播信息.txt");

            GlobleParam.Instance.InitalizeFaultFixEnchiridion(file);
        }

        private void LoadStations()
        {
            var file =
                Path.Combine(
                    DataPackage.Config.AppConfigs.First(f => f.AppName == ProjectName).AppPaths.ConfigDirectory,
                    "车站信息.txt");

            GlobleParam.Instance.InitalizeStationInfos(file);
        }

        public override void paint(Graphics g)
        {
            RefreshEvent();

            g.FillRectangle(GdiCommon.DarkBlueBrush, GlobleRect.m_BKRect);

            GlobleParam.Instance.WorkModel = BoolList[GlobleParam.Instance.FindInBoolIndex("司机室钥匙激活")] ? HMIWorkModel.Maintenance : HMIWorkModel.NoActoveDrive;
        }

        private void RefreshEvent()
        {
            EventManager.Instance.RefreshEvents(BoolList);

            append_postCmd(CmdType.SetBoolValue, GlobleParam.Instance.FindOutBoolIndex("屏还有未确认的故障时"),
                EventManager.Instance.GetCurrentActivableEvent() != null ? 1 : 0, 0);
        }
    }
}