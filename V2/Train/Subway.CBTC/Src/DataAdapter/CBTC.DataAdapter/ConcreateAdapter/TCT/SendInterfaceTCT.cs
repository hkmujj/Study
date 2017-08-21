using CBTC.DataAdapter.Model;
using CBTC.Infrasturcture.Model.Send;
using CommonUtil.Annotations;

namespace CBTC.DataAdapter.ConcreateAdapter.TCT
{
    public class SendInterfaceTCT : SendInterfaceBase
    {
        public SendInterfaceTCT(SignalDataOutTCT dataOut) : base(dataOut)
        {
             
        }

        ///<summary>
        /// 输入司机号数字
        /// </summary>
        /// <param name="driveridnum"></param>
        /// <returns></returns>
        public override bool InputDriverIDNum(SendModel<float> driveridnum)
        {
            DataOut.DriverNum = driveridnum.Content;
            return false;
            //return base.InputDriverIDNum(driveridnum);
        }

        ///<summary>
        /// 输入司机号数字
        /// </summary>
        /// <param name="trainidnum"></param>
        /// <returns></returns>
        public override bool InputTrainIDNum(SendModel<float> trainidnum)
        {
            DataOut.TrainNum = trainidnum.Content;
            return false;
            //return base.InputTrainIDNum(trainidnum);
        }

        ///<summary>
        /// 制动测试试闸
        /// </summary>
        ///<param name = "braketestswitch">
        /// <returen></returen>
        /// </param>
        public override bool InputBrakeTestSwitch(SendModel<bool> braketestswitch)
        {
            if (braketestswitch.Content)
            {
                DataOut.BrakeTestSwitch = true;
            }
            else
            {
                DataOut.BrakeTestSwitch = false;
            }

            return false;
            //return base.InputBrakeTestSwitch(braketestswitch);
        }

        ///<summary>
        /// 制动测试缓解
        /// </summary>
        ///<param name = "braketestrelease">
        /// <returen></returen>
        /// </param>
        public override bool InputBrakeTestRelease(SendModel<bool> braketestrelease)
        {
            if (braketestrelease.Content)
            {
                DataOut.BrakeTestRelease = true;
            }
            else
            {
                DataOut.BrakeTestRelease = false;
            }
            return false;
            //return base.InputBrakeTestRelease(braketestrelease);
        }

        ///<summary>
        /// 灯测试
        /// </summary>
        ///<param name = "lamptest">
        /// <returen></returen>
        /// </param>
        public override bool InputLampTest(SendModel<bool> lamptest)
        {
            if (lamptest.Content)
            {
                DataOut.LampTest = true;
            }
            else
            {
                DataOut.LampTest = false;
            }
            return false;
            //return base.InputLampTest(lamptest);
        }

        ///<summary>
        /// 广播测试
        /// </summary>
        ///<param name = "broadcasttest">
        /// <returen></returen>
        /// </param>
        public override bool InputBroadcastTest(SendModel<bool> broadcasttest)
        {
            if (broadcasttest.Content)
            {
                DataOut.BroadcastTest = true;
            }
            else
            {
                DataOut.BroadcastTest = false;
            }
            return false;
            //return base.InputBroadcastTest(broadcasttest);
        }
    }
}