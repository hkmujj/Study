using System.ComponentModel.Composition;
using Engine._6A.Interface.ViewModel;

namespace Engine._6A.ViewModel.Common
{
    [Export(typeof(IVideoViewModel))]
    public class VideoViewModel : ViewModelBase, IVideoViewModel
    {
    }
}