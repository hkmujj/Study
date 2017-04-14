using MMI.Facility.Control.Flow;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Flow;

namespace MMI.Facility.Control.Entry
{
    class FlowFactory
    {
        public static IFlowController CreateFlowController(string model)
        {
            switch (model)
            {
                case "edit":
                    return new FlowController(StartModel.Edit);
                case "ptt":
                    return new FlowController(StartModel.PTT);
                default:
                    return new FlowController(StartModel.Normal);
            }
        }

        public static IFlowController CreateFlowController(StartModel model)
        {
            return new FlowController(model);
        }
    }
}
