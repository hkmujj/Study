using System.ComponentModel;

namespace Engine.TAX2.SS7C.Model.BtnStragy
{
    public interface IStateInterface : INotifyPropertyChanged, IRaiseResourceChangedProvider
    {
        string Title { get; }

        string ContentViewName { get; }

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

        void UpdateState();
    }
}