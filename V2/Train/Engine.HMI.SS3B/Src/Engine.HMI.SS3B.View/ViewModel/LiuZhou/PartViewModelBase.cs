using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.HMI.SS3B.View.ViewModel.LiuZhou
{
    public class PartViewModelBase : NotificationObject
    {
        [Import]
        public IRegionManager RegionManager;
        public PartViewModelBase(KunMing.SS3BViewModel parent)
        {
            Parent = parent;
        }

        public KunMing.SS3BViewModel Parent { protected set; get; }
    }
}