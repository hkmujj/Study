using Engine.LCDM.HDX2.Entity.Controller.ActionResponser;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.StateInterface
{
    [StateInterfaceExport]
    public class RootScreenInfoLang: StateInterfaceBase
    {
        public override StateInterfaceKey Id
        {
            get { return StateInterfaceKey.Parser(StateKeys.Root_ScreenInfo_Lang); }
        }

        public RootScreenInfoLang()
        {
            Title = "ScreenInfo";

            var act = GetActionResponser<ChangeLangActionResponser>();
            act.TargetResourceType = ResourceType.En;
            BtnF2 = new BtnItem("", ResourceKeys.NationalEN, act);

            act = GetActionResponser<ChangeLangActionResponser>();
            act.TargetResourceType = ResourceType.Ch;
            BtnF5 = new BtnItem("",ResourceKeys.NationalZH, act);
            
            SetF8Return();
        }
    }
}