using Tram.CBTC.DataAdapter.Model;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Send;

namespace Tram.CBTC.DataAdapter.ConcreateAdapter.NRIET
{
    public class SendInterfaceNRIET : SendInterfaceBase
    {
        private SignalDataOutNRIET signalDataOutNRIET;
        public SendInterfaceNRIET(SignalDataOutNRIET signalDataOutNRIET)
            : base(signalDataOutNRIET)//(SignalDataOut dataOut) : base(dataOut)
        {
           // InitalizeMessageActions();
            this.signalDataOutNRIET = signalDataOutNRIET;
        }

        /// <summary>
        /// 输入司机号
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        public override bool InputDriverId(SendModel<string> driverId)
        {
            signalDataOutNRIET.StartFinish = false;
            if (driverId.Type == SendModelType.Ok)
            {
                
                signalDataOutNRIET.DriverCode = driverId.Content;
                signalDataOutNRIET.StartFinish = true;
            }else if (driverId.Type == SendModelType.Cancel)
            {
                signalDataOutNRIET.StartFinish = true;
            }
            return true;
        }

        /// <summary>
        /// 确认密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>匹配成功true  匹配失败 false</returns>
        public override bool ConfirmPassword(SendModel<string> password)
        {
            return true;
        }

        /// <summary>
        /// 请求开启／关闭雷达
        /// </summary>
        /// <param name="open">开启／关闭</param>
        /// <returns>返回是否允许</returns>
        public override bool OpenRadar(SendModel<bool> open)
        {
            if (open.Type == SendModelType.Ok)
            {
                signalDataOutNRIET.OpenRadar = open.Content;
            }
            return true;
        }

        /// <summary>
        /// 输入车次号
        /// </summary>
        /// <param name="trainId"></param>
        /// <returns></returns>
        public override bool InputTrainId(SendModel<string> trainId)
        {
            return true;
        }

        /// <summary>
        /// 输入线路运行方向
        /// </summary>
        /// <param name="lineRunDirection"></param>
        /// <returns></returns>
        public override bool InputLineRunDirection(SendModel<LineRunDirection> lineRunDirection)
        {
            return true;
        }
    }
}