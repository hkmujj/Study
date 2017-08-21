using System;
using System.ComponentModel;

namespace Engine._6A.Interface.ViewModel
{
    public interface ITitleViewModel : IClearData, INotifyPropertyChanged
    {
        DateTime DateTime { get; set; }
        string Category { get; set; }
    }
}