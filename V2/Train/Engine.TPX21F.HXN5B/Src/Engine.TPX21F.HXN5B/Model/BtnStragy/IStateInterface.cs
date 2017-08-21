using System.ComponentModel;

namespace Engine.TPX21F.HXN5B.Model.BtnStragy
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
        BtnItem BtnBQuestionMark { get; }
        BtnItem BtnBExclamation { get; }
        BtnItem BtnBContext { get; }
        BtnItem BtnBSoundUp { get; }
        BtnItem BtnBSoundDown { get; }
        BtnItem BtnBContrast { get; }
        BtnItem BtnBLightUp { get; }
        BtnItem BtnBLightDown { get; }
        BtnItem BtnBTemperature { get; }

        void UpdateState();
    }
}