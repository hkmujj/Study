using Engine.LCDM.HDX2.Entity.Controller.ActionResponser;
using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.StateInterface
{
    [StateInterfaceExport]
    public class RootSetting :StateInterfaceBase
    {
        public override StateInterfaceKey Id { get { return StateInterfaceKey.Parser(StateKeys.Root_Setting); } }

        public RootSetting()
        {
            Title = ResourceKeys.Setting;

            var setacction = GetActionResponser<SetSetActionResponser>();
            setacction .SendEventArg  = new SetEventArg(SetType.ReserveCommon);
            BtnF4 = new BtnItem(ResourceKeys.ReserveOften, setacction);

            setacction = GetActionResponser<SetSetActionResponser>();
            setacction.SendEventArg = new SetEventArg(SetType.SigleMutil);
            BtnF5 = new BtnItem(ResourceKeys.SingleDouble, setacction);

            SetF8Return();
        }
    }
}