using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Motor.TCMS.CRH400BF.Constant;
using Motor.TCMS.CRH400BF.View.Contents.Tow.Detail;
using Motor.TCMS.CRH400BF.ViewModel;

namespace Motor.TCMS.CRH400BF.Controller.BtnActionResponser
{
    [Export]
    class ChangeToTowTransformerActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick(StateViewModel stateViewModel)
        {

            stateViewModel.RegionManager.RequestNavigate(RegionNames.ContentTowContent,
             typeof(TowTransformerContentView).FullName);
            NavigateTo(stateViewModel,"Root-牵引界面变压器");
        }
    }
}
