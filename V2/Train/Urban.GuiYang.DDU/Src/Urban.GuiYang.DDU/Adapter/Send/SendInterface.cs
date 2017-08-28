using System.ComponentModel.Composition;
using System.Linq;
using MMI.Facility.Interface.Service;
using Urban.GuiYang.DDU.Extension;
using Urban.GuiYang.DDU.Model;
using Urban.GuiYang.DDU.Model.Constant;
using Urban.GuiYang.DDU.Model.PIS;
using Urban.GuiYang.DDU.Model.PIS.EmergBroadcast;
using Urban.GuiYang.DDU.Resources.Keys;

namespace Urban.GuiYang.DDU.Adapter.Send
{
    [Export(typeof(ISendInterface))]
    public class SendInterface : ISendInterface
    {
        public ICommunicationDataService DataService
        {
            get { return GlobalParam.Instance.InitParam.CommunicationDataService; }
        }

        //public void SendManualSettingStation(PISType type, SettingStationModel settingStation)
        //{
        //    if (settingStation.EndStation != null)
        //    {
        //        DataService.ChangeOutFloatOf(OufKeys.OufPIS终点站, settingStation.EndStation.StationConfig.StationKey);
        //    }
        //    if (!string.IsNullOrWhiteSpace(settingStation.LineId))
        //    {
        //        DataService.ChangeOutFloatOf(OufKeys.OufPIS线路ID, float.Parse(settingStation.LineId));
        //    }
        //}
        public void SendSettingStation(PISType type, SettingStationModel settingStation)
        {
            if (settingStation.NextStation != null)
            {
                DataService.ChangeOutFloatOf(OufKeys.OufPIS下一站, settingStation.NextStation.StationConfig.StationKey);
            }

            if (settingStation.EndStation != null)
            {
                DataService.ChangeOutFloatOf(OufKeys.OufPIS终点站, settingStation.EndStation.StationConfig.StationKey);
            }
            if (settingStation.StartStation != null)
            {
                DataService.ChangeOutFloatOf(OufKeys.OufPIS起始站, settingStation.StartStation.StationConfig.StationKey);
            }
            if (settingStation.SkipStation1 != null)
            {
                DataService.ChangeOutFloatOf(OufKeys.OufPIS跳站1, settingStation.SkipStation1.StationConfig.StationKey);
            }
            if (settingStation.SkipStation2 != null)
            {
                DataService.ChangeOutFloatOf(OufKeys.OufPIS跳站2, settingStation.SkipStation2.StationConfig.StationKey);
            }
            if (!string.IsNullOrWhiteSpace(settingStation.LineId))
            {
                var str = settingStation.LineId.Where(char.IsNumber).ToList();
                string lines = str.Aggregate(string.Empty, (current, ch) => current + ch);
                float fl = 0f;
                if (float.TryParse(lines, out fl))
                {
                    DataService.ChangeOutFloatOf(OufKeys.OufPIS线路ID, fl);
                }

            }
        }

        public void SelectedListBox(EmergBroadcastModel emergeroadcastmodel)
        {
            if (emergeroadcastmodel.SelectedEmergBroadcast != null)
            {
                DataService.ChangeOutFloatOf(OufKeys.OufPIS紧急广播选中项, emergeroadcastmodel.SelectedEmergBroadcast.Index);
            }

        }

        public void SendSigleEmergBroadcast(bool isPressed)
        {
            DataService.ChangeOutBoolOf(OubKeys.OubPIS单次, isPressed);
        }

        public void SendCircleEmergBroadcast(bool isPressed)
        {
            DataService.ChangeOutBoolOf(OubKeys.OubPIS循环, isPressed);
        }

        public void SetAuto(bool isPressed)
        {
            DataService.ChangeOutBoolOf(OubKeys.OubPIS自动, isPressed);
        }
        public void SetHalfAuto(bool isPressed)
        {
            DataService.ChangeOutBoolOf(OubKeys.OubPIS半自动, isPressed);
        }

        public void SetManual(bool isPressed)
        {
            DataService.ChangeOutBoolOf(OubKeys.OubPIS手动, isPressed);
        }

        public void ClearEmergBroadcast()
        {
        }

        public void EnsureDepart(bool isPressed)
        {
        }

        public void EnsureNextStation(bool isPressed)
        {
        }

        public void EnsureEndStation(bool isPressed)
        {
        }
    }
}