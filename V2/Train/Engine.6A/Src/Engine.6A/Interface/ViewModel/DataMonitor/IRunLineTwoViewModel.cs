using System.ComponentModel;

namespace Engine._6A.Interface.ViewModel.DataMonitor
{
    /// <summary>
    /// 走形2
    /// </summary>
    public interface IRunLineTwoViewModel : IClearData, INotifyPropertyChanged
    {
        string ReferenceVelocity { get; set; }
        string OneEndCarcass { get; set; }
        string TwoEndCarcass { get; set; }
        string OneEndBody { get; set; }
        string TwoEndBody { get; set; }
    }
}