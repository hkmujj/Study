using System.Collections.ObjectModel;
using CBTC.DataAdapter.Model;
using CBTC.Infrasturcture.Model.Msg.Details;
using CBTC.Infrasturcture.Model.Send;

namespace CBTC.DataAdapter.ConcreateAdapter
{
    public class SendInterfaceBase<TDataOut> : SendInterfaceBase where TDataOut: SignalDataOut
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

        public virtual bool InputDriverId(SendModel<string> driverId)
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
    }
}