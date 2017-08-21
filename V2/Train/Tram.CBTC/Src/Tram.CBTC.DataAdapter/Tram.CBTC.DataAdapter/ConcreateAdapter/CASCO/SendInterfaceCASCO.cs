using Tram.CBTC.DataAdapter.Model;
using Tram.CBTC.Infrasturcture.Events;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Send;

namespace Tram.CBTC.DataAdapter.ConcreateAdapter.CASCO
{
    public class SendInterfaceCASCO : SendInterfaceBase
    {
        public SendInterfaceCASCO(SignalDataOut dataOut) : base(dataOut)
        {
        }

        /// <summary>
        /// 选择车载联机模式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override bool SelectVehicleRunningModel(SendModel<VehicleRunningModel> model)
        {
            return base.SelectVehicleRunningModel(model);
        }

        /// <summary>
        /// 发送终点站
        /// </summary>
        /// <param name="endStation"></param>
        /// <returns></returns>
        public override bool SendEndStation(SendModel<string> endStation)
        {
            return base.SendEndStation(endStation);
        }

        /// <summary>
        /// 发送线路号
        /// </summary>
        /// <param name="lineId">LineID</param>
        /// <returns></returns>
        public override bool SendLineID(SendModel<string> lineId)
        {
            return base.SendLineID(lineId);
        }

        /// <summary>
        /// 发送计划，车次，单程
        /// </summary>
        /// <param name="planinfo"></param>
        /// <returns></returns>
        public override bool SendPlanInfo(SendModel<PlanInfo> planinfo)
        {
            return base.SendPlanInfo(planinfo);
        }
    }
}