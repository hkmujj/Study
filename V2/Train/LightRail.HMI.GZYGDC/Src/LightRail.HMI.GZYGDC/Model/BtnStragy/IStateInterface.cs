using System.ComponentModel;

namespace LightRail.HMI.GZYGDC.Model.BtnStragy
{
    public interface IStateInterface : INotifyPropertyChanged, IRaiseResourceChangedProvider
    {
        string Title { get; }

        string ContentViewName { get; }

        string ContentViewTitle { get; }

        StateInterfaceKey Id { get; }

        BtnItem BtnB1 { get; }
        BtnItem BtnB2 { get; }
        BtnItem BtnB3 { get; }
        BtnItem BtnB4 { get; }
        BtnItem BtnB5 { get; }
        BtnItem BtnB6 { get; }
        BtnItem BtnB7 { get; }
        BtnItem BtnB8 { get; }
        BtnItem BtnB9 { get; }
        BtnItem BtnB10 { get; }
        BtnItem BtnB11 { get; }
        BtnItem BtnB12 { get; }
        BtnItem BtnB13 { get; }
        BtnItem BtnB14 { get; }
        BtnItem BtnB15 { get; }
        BtnItem BtnB16 { get; }
        BtnItem BtnB17 { get; }
        BtnItem BtnB18 { get; }
        BtnItem BtnB19 { get; }
        BtnItem BtnB20 { get; }
       

        void UpdateState();
    }
}