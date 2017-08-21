using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace Subway.XiaMenLine1.Interface.Model
{
    public interface IStationSettingModel : IViewModel
    {
        DelegateCommand HalfModelClick { get; }
        DelegateCommand ManulModelClick { get; }
        ICommand StationViewDump { get; }
        ICommand StationSelect { get; }
        ICommand StationComfirm { get; }
        ICommand SendBorder { get; }
        bool HalfButtonDown { get; }
        bool ManualButtonDown { get; }
        bool HalfButtonEnable { get; }
        bool ManualButtonEnable { get; }
        bool StartStationDown { get; }
        bool NextStationDown { get; }
        bool EndStationDown { get; }
        string StartStation { get; }
        string NextStation { get; }
        string EndStation { get; }
        void Init();
    }
}