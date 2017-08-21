using System.ComponentModel.Composition;
using Engine._6A.Interface.ViewModel.DataMonitor;
using Engine._6A.ViewModel.Common;

namespace Engine._6A.ViewModel.Axle6
{
    [Export(typeof(IFirePreventionViewModel))]
    public class FirePreventionViewModel : ViewModelBase, IFirePreventionViewModel
    {

    }
}