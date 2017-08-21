using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.StateInterface
{
    [StateInterfaceExport]
    public class RootScreenInfoDateTimeOkCancel: RootScreenInfoDateTime
    {
        public override StateInterfaceKey Id
        {
            get { return StateInterfaceKey.Parser(StateKeys.Root_ScreenInfo_DateTime_OkCancel); }
        }

        public RootScreenInfoDateTimeOkCancel()
        {
            var key = StateInterfaceKey.Parser(StateKeys.Root_ScreenInfo_DateTime);
            SetF1OK(key);
            SetF2Cancel(key);
        }
    }
}