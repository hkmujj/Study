using System.ComponentModel;

namespace Engine.Angola.TCMS.Model.BtnStragy
{
    public interface IStateInterface : INotifyPropertyChanged, IRaiseResourceChangedProvider
    {
        string Title { get; }

        string ContentViewName { get; }

        StateInterfaceKey Id { get; }

        BtnItem BtnF1 { get; }
        BtnItem BtnF2 { get; }
        BtnItem BtnF3 { get; }
        BtnItem BtnF4 { get; }
        BtnItem BtnF5 { get; }

        void UpdateState();
    }
}
