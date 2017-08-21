using Engine.LCDM.HDX2.Entity.Controller.ActionResponser;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.StateInterface
{
    [StateInterfaceExport]
    public class RootScreenInfo : StateInterfaceBase
    {
        public override StateInterfaceKey Id
        {
            get { return StateInterfaceKey.Parser(StateKeys.Root_ScreenInfo);}
        }

        public RootScreenInfo()
        {
            Title = "ScreenInfo";

            BtnF1 = new BtnItem(ResourceKeys.BrakeTest, null);
            BtnF3 = new BtnItem(ResourceKeys.TrainId, GetActionResponser<TrainIdActionResponser>());
            BtnF4 = new BtnItem(ResourceKeys.DateTime, GetActionResponser<DateTimeActionResponser>());
            BtnF5 = new BtnItem(ResourceKeys.Lang, GetActionResponser<LangActionResponser>());
            BtnF6 = new BtnItem(ResourceKeys.SoftVersion, GetActionResponser<SoftVersionActionResponser>());
            SetF8Return();
        }
    }
}