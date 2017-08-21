using Engine.LCDM.HDX2.Entity.Controller.ActionResponser;
using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;
using Engine.LCDM.HDX2.Resource;
using Microsoft.Practices.ServiceLocation;

namespace Engine.LCDM.HDX2.Entity.Controller.StateInterface
{
    [StateInterfaceExport]
    public class RootScreenInfoTrainId : StateInterfaceBase
    {
        public override StateInterfaceKey Id
        {
            get { return StateInterfaceKey.Parser(StateKeys.Root_ScreenInfo_TrainID);}
        }

        public RootScreenInfoTrainId()
        {
            Title = ResourceKeys.TrainId;

            var ct = GetActionResponser<ChangeTrainIdActionResponser>();
            ct.SetTrainIdType = SetTrainIdType.SetData1;
            BtnF3 = new BtnItem(ResourceKeys.ChangData1, ct);

            ct = GetActionResponser<ChangeTrainIdActionResponser>();
            ct.SetTrainIdType = SetTrainIdType.SetData2;
            BtnF4 = new BtnItem(ResourceKeys.ChangData2, ct);

            ct = GetActionResponser<ChangeTrainIdActionResponser>();
            ct.SetTrainIdType = SetTrainIdType.SetData3;
            BtnF5 = new BtnItem(ResourceKeys.ChangData3, ct);

            ct = GetActionResponser<ChangeTrainIdActionResponser>();
            ct.SetTrainIdType = SetTrainIdType.SetData4;
            BtnF6 = new BtnItem(ResourceKeys.ChangData4, ct);

            ct = GetActionResponser<ChangeTrainIdActionResponser>();
            ct.SetTrainIdType = SetTrainIdType.SetData5;
            BtnF7 = new BtnItem(ResourceKeys.ChangData5, ct);

            SetF8Return();
        }
    }
}