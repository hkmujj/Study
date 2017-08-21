using Urban.GuiYang.DDU.Model.Constant;
using Urban.GuiYang.DDU.Model.PIS;
using Urban.GuiYang.DDU.Model.PIS.EmergBroadcast;

namespace Urban.GuiYang.DDU.Adapter
{
    public interface ISendInterface
    {
        void SendSettingStation(PISType type, SettingStationModel settingStation);

        //void SendManualSettingStation(PISType type, SettingStationModel settingStation);

        void SelectedListBox(EmergBroadcastModel emergeroadcastmodel);

        void SendSigleEmergBroadcast(bool isPressed);

        void SendCircleEmergBroadcast(bool isPressed);

        void ClearEmergBroadcast();

        void EnsureDepart(bool isPressed);

        void EnsureNextStation(bool isPressed);

        void EnsureEndStation(bool isPressed);


        void SetAuto(bool isPressed);
        void SetHalfAuto(bool isPressed);
        void SetManual(bool isPressed);







    }
}