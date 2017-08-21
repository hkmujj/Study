using System.Windows.Input;

namespace Subway.ShiJiaZhuangLine1.Interface.Model
{
    public interface IStationSettingModel : IViewModel
    {
        ICommand ModelSelect { get; }
        ICommand StationViewDump { get; }
        ICommand StationSelect { get; }
        ICommand StationComfirm { get; }
        bool AutoButtonDown { get; }
        bool ManualButtonDown { get; }
        bool StartStationDown { get; }
        bool NextStationDown { get; }
        bool EndStationDown { get; }
        string StartStation { get; }
        string NextStation { get; }
        string EndStation { get; }
        void Init();
        ICommand Up { get; }
        ICommand Down { get; }
        ICommand SkipStation { get; }
    }
}