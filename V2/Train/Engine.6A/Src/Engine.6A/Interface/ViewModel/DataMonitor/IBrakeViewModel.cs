using System.ComponentModel;

namespace Engine._6A.Interface.ViewModel.DataMonitor
{
    public interface IBrakeViewModel : IClearData, INotifyPropertyChanged
    {
        double TrainPipe { get; set; }
        double ParkingCylinderOne { get; set; }
        double ParkingCylinderTwo { get; set; }
        double BalancingCylinderOne { get; set; }
        double BalancingCylinderTwo { get; set; }
        double BrakeCylinder { get; set; }
        string TrainNumber { get; set; }
        string CurrentState { get; set; }
        string CurrentNetworkFlow { get; set; }
        string BoardInformation { get; set; }
        string PressureTransmitter { get; set; }
        string FlowTransmitter { get; set; }
        string ParkingBrake { get; set; }
        string ReferSpeed { get; set; }
        string TransNum { get; set; }
    }
}