using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.StateInterface
{
    [StateInterfaceExport]
    public class RootScreenInfoDateTime: StateInterfaceBase
    {
        public override StateInterfaceKey Id
        {
            get { return StateInterfaceKey.Parser(StateKeys.Root_ScreenInfo_DateTime); }
        }

        public RootScreenInfoDateTime()
        {
            Title = ResourceKeys.SetDateTime;

            BtnF3 = new BtnItem(ResourceKeys.ChangDate, null);
            BtnF4 = new BtnItem(ResourceKeys.ChangYear, null);
            BtnF5 = new BtnItem(ResourceKeys.ChangMounth, null);
            BtnF6 = new BtnItem(ResourceKeys.ChangDay, null);
            BtnF7 = new BtnItem(ResourceKeys.ChangDateFromNet, null);
            SetF8Return();
        }
    }
}