using System.ComponentModel.Composition;
using LightRail.HMI.GZYGDC.Resources.Keys;

// ReSharper disable once CheckNamespace
namespace LightRail.HMI.GZYGDC.Controller.BtnActionResponser.NetTopologyView
{
    [Export]
    public class ReturnBtnActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_运行界面按键);
        }
    }


  
}
