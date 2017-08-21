using System.Collections.ObjectModel;
using CommonUtil.Controls;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP.Infrasturcture.Model
{
    public class NoneSendInterface : ISendInterface
    {
        /// <summary>
        /// 制动测试确认
        /// </summary>
        /// <param name="brakeTest"></param>
        /// <returns></returns>
        public bool EnsurceBrakeTestSelect(SendModel<BrakeTestModel> brakeTest)
        {
            return false;
        }

        /// <summary>
        /// 制动测试选择
        /// </summary>
        /// <param name="brakeTest"></param>
        /// <returns></returns>
        public bool SendBrakeTestSelect(SendModel<BrakeTestModel> brakeTest)
        {
            return false;
        }

        public bool SendAssistScreenTest(SendModel<AssistScreenTestModel> assistScreenTestModel)
        {
            return false;
        }

        public bool SendTrainId(SendModel<DriverDataModel> trainId)
        {
            return false;
        }

        public bool SendDriverId(SendModel<DriverDataModel> driverid)
        {
            return false;
        }

        /// <summary>
        /// 发送司机号和车次号
        /// </summary>
        /// <param name="driverid"></param>
        /// <returns></returns>
        public bool SendDriverData(SendModel<DriverDataModel> driverid)
        {
            return false;
        }

        public bool SendTrainData(SendModel<ReadOnlyCollection<string>> traindata)
        {
            return false;
        }

        public bool SendDriverData(string trainid, string driverid, string rbcId, string tel)
        {
            return false;
        }

        public bool SendStart(SendModel<object> startState)
        {
            return false;
        }

        public bool SendRelease(SendModel<object> relaseState)
        {
            return false;
        }

        public bool SendAlert()
        {
            return false;
        }

        public bool SendRBCData(SendModel<RBCDataModel> rbcData)
        {
            return false;
        }

        public bool SelectFreq(SendModel<TrainFreq> trainFreq)
        {
            return false;
        }

        public bool SelectCtcs(SendModel<CTCSType> ctcsType)
        {
            return false;
        }

        public bool SelectControlMode(SendModel<ControlType> controlType)
        {
            return false;
        }

        public bool SendTrainDataConfirm()
        {
            return false;
        }

        public bool SendFreqConfirm()
        {
            return false;
        }

        public bool EnsureMessage(SendModel<IInfomationItem> message)
        {
            return false;
        }

        /// <summary>
        /// 通知消息计时变化 
        /// </summary>
        /// <param name="messageItem"></param>
        /// <returns></returns>
        public bool OnMessageTimeChanged(IMessageItem messageItem)
        {
            return false;
        }

        /// <summary>
        /// 响应用户动作，目前只有root界面 B10按键触发
        ///     用于快速跳过启动界面等特殊需求
        /// </summary>
        /// <param name="actionType"></param>
        /// <param name="mouseState"></param>
        /// <returns></returns>
        public bool OnUserAction(UserActionType actionType, MouseState mouseState)
        {
            return false;
        }

        public bool AssistSwich(SendModel<bool> asssistModel)
        {
            return false;
        }

        /// <summary>
        /// DMI 测试
        /// </summary>
        /// <param name="dmiModel"></param>
        /// <returns></returns>
        public bool SendDMITest(SendModel<object> dmiModel)
        {
            return false;
        }
    }
}