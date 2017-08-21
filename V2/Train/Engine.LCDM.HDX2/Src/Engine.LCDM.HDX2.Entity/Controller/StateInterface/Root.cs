using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Net.Mime;
using Engine.LCDM.HDX2.Entity.Controller.ActionResponser;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Resource;
using Microsoft.Practices.ServiceLocation;

namespace Engine.LCDM.HDX2.Entity.Controller.StateInterface
{
    [StateInterfaceExport]
    public class Root: StateInterfaceBase
    {
        public override StateInterfaceKey Id
        {
            get { return StateInterfaceKey.Root; }
        }

        public Root()
        {
            Title = "MainScreen";

            BtnF3 = new BtnItem(ResourceKeys.AirBrake, GetActionResponser<AirBrakeActionResponser>());

            BtnF4 = new BtnItem(ResourceKeys.Setting, GetActionResponser<SettingActionResponser>());

            BtnF7 = new BtnItem(ResourceKeys.ScreenInfo, GetActionResponser<ScreenInfoActionResponser>());
        }
    }
}