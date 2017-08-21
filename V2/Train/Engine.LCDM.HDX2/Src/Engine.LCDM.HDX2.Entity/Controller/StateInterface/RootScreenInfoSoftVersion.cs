using Engine.LCDM.HDX2.Entity.Controller.ActionResponser;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.StateInterface
{
    [StateInterfaceExport]
    public class RootScreenInfoSoftVersion :StateInterfaceBase
    {
        public override StateInterfaceKey Id
        {
            get { return StateInterfaceKey.Parser(StateKeys.Root_ScreenInfo_SoftVersion); }
        }

        public RootScreenInfoSoftVersion()
        {
            Title = "SoftVersion";

            SetF8Return<ReturnStateToRootViewToMainActionResponser>();
        }
    }
}