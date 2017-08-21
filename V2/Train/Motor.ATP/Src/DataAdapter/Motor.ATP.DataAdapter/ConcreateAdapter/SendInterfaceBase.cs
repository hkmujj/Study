using System.Collections.ObjectModel;
using CommonUtil.Controls;
using Motor.ATP.DataAdapter.Model;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP.DataAdapter.ConcreateAdapter
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
        /// 制动测试确认
        /// </summary>
        /// <param name="brakeTest"></param>
        /// <returns></returns>
        public virtual bool EnsurceBrakeTestSelect(SendModel<BrakeTestModel> brakeTest)
        {
            return false;
        }

        /// <summary>
        /// 制动测试选择
        /// </summary>
        /// <param name="brakeTest"></param>
        /// <returns></returns>
        public virtual bool SendBrakeTestSelect(SendModel<BrakeTestModel> brakeTest)
        {
            return false;
        }

        public virtual bool SendAssistScreenTest(SendModel<AssistScreenTestModel> assistScreenTestModel)
        {
            return false;
        }

        /// <summary>
        /// 发送车次号
        /// </summary>
        /// <param name="trainId"></param>
        /// <returns></returns>
        public virtual bool SendTrainId(SendModel<DriverDataModel> trainId)
        {
            return false;
        }

        /// <summary>
        /// 发送司机号
        /// </summary>
        /// <param name="driverid"></param>
        /// <returns></returns>
        public virtual bool SendDriverId(SendModel<DriverDataModel> driverid)
        {
            return false;
        }

        /// <summary>
        /// 发送司机号和车次号
        /// </summary>
        /// <param name="driverid"></param>
        /// <returns></returns>
        public virtual bool SendDriverData(SendModel<DriverDataModel> driverid)
        {
            return false;
        }

        /// <summary>
        /// 发送列车数据
        /// </summary>
        /// <param name="traindata"></param>
        /// <returns></returns>
        public virtual bool SendTrainData(SendModel<ReadOnlyCollection<string>> traindata)
        {
            return false;
        }

        /// <summary>
        /// 驾驶数据
        /// </summary>
        /// <param name="trainid"></param>
        /// <param name="driverid"></param>
        /// <param name="rbcId"></param>
        /// <param name="tel"></param>
        /// <returns></returns>
        public virtual bool SendDriverData(string trainid, string driverid, string rbcId, string tel)
        {
            return false;
        }

        /// <summary>
        /// 发送启动
        /// </summary>
        /// <param name="startState"></param>
        /// <returns></returns>
        public virtual bool SendStart(SendModel<object> startState)
        {
            return false;
        }

        /// <summary>
        /// 缓解
        /// </summary>
        /// <param name="relaseState"></param>
        /// <returns></returns>
        public virtual bool SendRelease(SendModel<object> relaseState)
        {
            return false;
        }

        /// <summary>
        /// 警惕
        /// </summary>
        /// <returns></returns>
        public virtual bool SendAlert()
        {
            return false;
        }

        /// <summary>
        /// RBC
        /// </summary>
        /// <param name="rbcData"></param>
        /// <returns></returns>
        public virtual bool SendRBCData(SendModel<RBCDataModel> rbcData)
        {
            return false;
        }

        /// <summary>
        /// 选择载频
        /// </summary>
        /// <param name="trainFreq"></param>
        /// <returns></returns>
        public virtual bool SelectFreq(SendModel<TrainFreq> trainFreq)
        {
            return false;
        }

        /// <summary>
        /// 选择CTCS
        /// </summary>
        /// <param name="ctcsType"></param>
        /// <returns></returns>
        public virtual bool SelectCtcs(SendModel<CTCSType> ctcsType)
        {
            return false;
        }

        /// <summary>
        /// 选择控制模式
        /// </summary>
        /// <param name="controlType"></param>
        /// <returns></returns>
        public virtual bool SelectControlMode(SendModel<ControlType> controlType)
        {
            return false;
        }

        /// <summary>
        /// 发送 确认列车数据
        /// </summary>
        /// <returns></returns>
        public virtual bool SendTrainDataConfirm()
        {
            return false;
        }

        /// <summary>
        /// 发送 确认载频
        /// </summary>
        /// <returns></returns>
        public virtual bool SendFreqConfirm()
        {
            return false;
        }

        /// <summary>
        /// 确认一个消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual bool EnsureMessage(SendModel<IInfomationItem> message)
        {
            return false;
        }

        /// <summary>
        /// 通知消息计时变化 
        /// </summary>
        /// <param name="messageItem"></param>
        /// <returns></returns>
        public virtual bool OnMessageTimeChanged(IMessageItem messageItem)
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
        public virtual bool OnUserAction(UserActionType actionType, MouseState mouseState)
        {
            return false;
        }

        public virtual bool AssistSwich(SendModel<bool> asssistModel)
        {
            return false;
        }

        /// <summary>
        /// DMI 测试
        /// </summary>
        /// <param name="dmiModel"></param>
        /// <returns></returns>
        public virtual bool SendDMITest(SendModel<object> dmiModel)
        {
            return false;
        }
    }
}