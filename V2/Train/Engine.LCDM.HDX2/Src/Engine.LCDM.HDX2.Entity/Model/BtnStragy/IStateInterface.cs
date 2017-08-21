using System.Collections.ObjectModel;
using System.ComponentModel;
using Engine.LCDM.HDX2.Entity.Model.Domain;

namespace Engine.LCDM.HDX2.Entity.Model.BtnStragy
{
    public interface IStateInterface : INotifyPropertyChanged, IRaiseResourceChangedProvider
    {
        string Title {  get; }

        StateInterfaceKey Id { get; }

        BtnItem BtnF1 {  get; }
        BtnItem BtnF2 {  get; }
        BtnItem BtnF3 {  get; }
        BtnItem BtnF4 {  get; }
        BtnItem BtnF5 {  get; }
        BtnItem BtnF6 {  get; }
        BtnItem BtnF7 {  get; }
        BtnItem BtnF8 {  get; }
    }
}