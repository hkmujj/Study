using System.ComponentModel.Composition;
using LightRail.HMI.GZYGDC.Resources.Keys;

// ReSharper disable once CheckNamespace
namespace LightRail.HMI.GZYGDC.Controller.BtnActionResponser.BroadcastControlView
{
    [Export]
    public class ReturnBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            ViewModel.Value.BroadcastControlViewModel.Controller.ResetData();

            NavigateTo(StateKeys.Root_运行界面按键);
        }
    }


  
}
