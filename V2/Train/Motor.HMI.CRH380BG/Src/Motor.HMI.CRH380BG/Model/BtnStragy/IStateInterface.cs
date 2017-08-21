using System.ComponentModel;
using Motor.HMI.CRH380BG.ViewModel;

namespace Motor.HMI.CRH380BG.Model.BtnStragy
{
    public interface IStateInterface : INotifyPropertyChanged, IRaiseResourceChangedProvider
    {
        string Title { get; }

        string ContentViewName { get; }

        StateInterfaceKey Id { get; }

        BtnItem BtnOK { get; }
        BtnItem BtnSetting { get; }
        BtnItem BtnSaveAs { get; }
        BtnItem BtnDown { get; }
        BtnItem BtnUp { get; }
        BtnItem BtnRight { get; }
        BtnItem BtnLeft { get; }
        BtnItem BtnQuery { get; }
        BtnItem BtnB10 { get; }
        BtnItem BtnB9 { get; }
        BtnItem BtnB8 { get; }
        BtnItem BtnB7 { get; }
        BtnItem BtnB6 { get; }
        BtnItem BtnB5 { get; }
        BtnItem BtnB4 { get; }
        BtnItem BtnB3 { get; }
        BtnItem BtnB2 { get; }
        BtnItem BtnB1 { get; }
        BtnItem BtnBOK { get; }
        BtnItem BtnBReturn { get; }
        BtnItem BtnBSwitchDisplay { get; }
        BtnItem BtnBTrainStopCheck { get; }
        BtnItem BtnBTrainRunningCheck { get; }
        BtnItem BtnBFaultInfo { get; }
        BtnItem BtnBIofoBox { get; }
        BtnItem BtnBLanguageSelect { get; }
        BtnItem BtnBDayandnightSwitch { get; }
        BtnItem BtnBSwitch { get; }
        BtnItem BtnBLight { get; }

        void UpdateState(StateViewModel stateViewModel);
    }
}