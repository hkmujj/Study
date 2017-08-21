using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Constant;
using Engine.TCMS.HXD3.Resource.Keys;
using Engine.TCMS.HXD3.View.Contents.Fault;
using Microsoft.Practices.Prism.Regions;

namespace Engine.TCMS.HXD3.Controller.BtnActionResponser
{
    [Export]
    public class ChangToFaultViewActionResponser : BtnActionResponserBase

    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_机器状态_故障履历);

            RegionManager.RequestNavigate(RegionNames.RegionFaultContent, typeof(FaultItemCollectionView).FullName);
        }
    }
}