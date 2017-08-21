using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Motor.TCMS.CRH400BF.Constant;
using Motor.TCMS.CRH400BF.View.Contents.Tow.Detail;
using Motor.TCMS.CRH400BF.ViewModel;

namespace Motor.TCMS.CRH400BF.Controller.BtnActionResponser
{
    [Export]
    class ChangeToTowConverterActionResponser : BtnActionResponserBase
    {

        public override void ResponseClick(StateViewModel stateViewModel)
        {
            stateViewModel.RegionManager.RequestNavigate(RegionNames.ContentTowContent,
                typeof(TowConverterContentView).FullName);
            NavigateTo(stateViewModel, "Root-牵引界面牵引变流器");
        }
    }
}
