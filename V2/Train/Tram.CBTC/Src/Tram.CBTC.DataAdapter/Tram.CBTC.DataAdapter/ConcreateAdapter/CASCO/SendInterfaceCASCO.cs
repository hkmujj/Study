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
            SignalDataOutCASCO dataout = DataOut as SignalDataOutCASCO;
            if (model.Content == VehicleRunningModel.OnBoardOnline)
            {
                dataout.TrainRunMode = 1;
            }
            else if(model.Content == VehicleRunningModel.VehicleIndependent)
            {
                dataout.TrainRunMode = (int)model.Content;
            }
            else if (model.Content == VehicleRunningModel.ManualControl)
            {
                dataout.TrainRunMode = (int)model.Content;
            }
            else
            {
                dataout.TrainRunMode = 0;
            }


             return true;
            //return base.SelectVehicleRunningModel(model);
        }

        /// <summary>
        /// 发送终点站
        /// </summary>
        /// <param name="endStation"></param>
        /// <returns></returns>
        public override bool SendEndStation(SendModel<string> endStation)
        {
            return true;
            //return base.SendEndStation(endStation);
        }

        /// <summary>
        /// 发送线路号
        /// </summary>
        /// <param name="lineId">LineID</param>
        /// <returns></returns>
        public override bool SendLineID(SendModel<string> lineId)
        {
            return true;
            //return base.SendLineID(lineId);
        }

        /// <summary>
        /// 发送计划，车次，单程
        /// </summary>
        /// <param name="planinfo"></param>
        /// <returns></returns>
        public override bool SendPlanInfo(SendModel<PlanInfo> planinfo)
        {
            return true;
            // return base.SendPlanInfo(planinfo);
        }

        ///<summary>
        /// 输入司机号数字
        /// </summary>
        /// <param name="driveridnum"></param>
        /// <returns></returns>
        public override bool InputDriverIDNum(SendModel<float> driveridnum)
        {
            if (driveridnum.Content>0)
            {
                DataOut.DriverNum = driveridnum.Content;
            }
            return true;
            // return base.InputDriverIDNum(driveridnum);
        }

        /// <summary>
        /// 输入司机号
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        public override bool InputDriverId(SendModel<string> driverId)
        {
            var drivernum = int.Parse(driverId.Content);
            if (drivernum > 0)
            {
                DataOut.DriverNum = drivernum;
            }
            return true;
            // return base.InputDriverId(driverId);
        }
    }
}