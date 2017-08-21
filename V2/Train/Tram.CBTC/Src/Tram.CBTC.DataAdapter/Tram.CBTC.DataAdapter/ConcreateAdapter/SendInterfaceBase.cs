using Tram.CBTC.DataAdapter.Model;
using Tram.CBTC.Infrasturcture.Events;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Msg.Details;
using Tram.CBTC.Infrasturcture.Model.Send;

namespace Tram.CBTC.DataAdapter.ConcreateAdapter
{
    public class SendInterfaceBase<TDataOut> : SendInterfaceBase where TDataOut : SignalDataOut
    {
        public SendInterfaceBase(TDataOut dataOut) : base(dataOut)
        {
            DataOut = dataOut;
        }

        public new TDataOut DataOut { private set; get; }


    }

    public class SendInterfaceBase : ISendInterface
    {
        public SendInterfaceBase(SignalDataOut dataOut)
        {
            DataOut = dataOut;
        }

        public SignalDataOut DataOut { get; private set; }


        /// <summary>
        /// 确认一个消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual bool EnsureMessage(SendModel<IInformationContent> message)
        {
            return false;
        }

        /// <summary>
        /// 输入司机号
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        public virtual bool InputDriverId(SendModel<string> driverId)
        {
            return false;
        }

        /// <summary>
        /// 确认密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>匹配成功true  匹配失败 false</returns>
        public virtual bool ConfirmPassword(SendModel<string> password)
        {
            return false;
        }


        /// <summary>
        /// 请求开启／关闭雷达
        /// </summary>
        /// <param name="open">开启／关闭</param>
        /// <returns>返回是否允许</returns>
        public virtual bool OpenRadar(SendModel<bool> open)
        {
            return false;
        }

        /// <summary>
        /// 输入车次号
        /// </summary>
        /// <param name="trainId"></param>
        /// <returns></returns>
        public virtual bool InputTrainId(SendModel<string> trainId)
        {
            return false;
        }

        /// <summary>
        /// 输入线路运行方向
        /// </summary>
        /// <param name="lineRunDirection"></param>
        /// <returns></returns>
        public virtual bool InputLineRunDirection(SendModel<LineRunDirection> lineRunDirection)
        {
            return false;
        }

        /// <summary>
        /// 输入亮度
        /// </summary>
        /// <param name="light"></param>
        /// <returns></returns>
        public virtual bool InputLight(SendModel<double> light)
        {
            return false;
        }

        /// <summary>
        /// 输入音量
        /// </summary>
        /// <param name="volumn"></param>
        /// <returns></returns>
        public virtual bool InputVolumn(SendModel<double> volumn)
        {
            return false;
        }

        ///<summary>
        /// 输入司机号数字
        /// </summary>
        /// <param name="driveridnum"></param>
        /// <returns></returns>
        public virtual bool InputDriverIDNum(SendModel<float> driveridnum)
        {
            return false;
        }

        ///<summary>
        /// 输入司机号数字
        /// </summary>
        /// <param name="trainidnum"></param>
        /// <returns></returns>
        public virtual bool InputTrainIDNum(SendModel<float> trainidnum)
        {
            return false;
        }

        ///<summary>
        /// 制动测试试闸
        /// </summary>
        ///<param name = "braketestswitch">
        /// <returen></returen>
        /// </param>
        public virtual bool InputBrakeTestSwitch(SendModel<bool> braketestswitch)
        {
            return false;
        }

        ///<summary>
        /// 制动测试缓解
        /// </summary>
        ///<param name = "braketestrelease">
        /// <returen></returen>
        /// </param>
        public virtual bool InputBrakeTestRelease(SendModel<bool> braketestrelease)
        {
            return false;
        }

        ///<summary>
        /// 广播测试
        /// </summary>
        ///<param name = "broadcasttest">
        /// <returen></returen>
        /// </param>
        public virtual bool InputBroadcastTest(SendModel<bool> broadcasttest)
        {
            return false;
        }

        ///<summary>
        /// 灯测试
        /// </summary>
        ///<param name = "lamptest">
        /// <returen></returen>
        /// </param>
        public virtual bool InputLampTest(SendModel<bool> lamptest)
        {
            return false;
        }

        /// <summary>
        /// 打开关闭声音
        /// </summary>
        /// <param name="sound"></param>
        /// <returns></returns>
        public virtual bool OpenSound(SendModel<bool> sound)
        {
            DataOut.CBTC.TrainInfo.System.IsSoundOpen = sound.Content;
            return true;
        }

        /// <summary>
        /// 选择车载联机模式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool SelectVehicleRunningModel(SendModel<VehicleRunningModel> model)
        {
            return false;
        }

        /// <summary>
        /// 发送线路号
        /// </summary>
        /// <param name="lineId">LineID</param>
        /// <returns></returns>
        public virtual bool SendLineID(SendModel<string> lineId)
        {
            return false;
        }

        /// <summary>
        /// 发送终点站
        /// </summary>
        /// <param name="endStation"></param>
        /// <returns></returns>
        public virtual bool SendEndStation(SendModel<string> endStation)
        {
            return false;
        }

        /// <summary>
        /// 发送计划，车次，单程
        /// </summary>
        /// <param name="planinfo"></param>
        /// <returns></returns>
        public virtual bool SendPlanInfo(SendModel<PlanInfo> planinfo)
        {
            return false;
        }
    }
}