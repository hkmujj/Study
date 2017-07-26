using System.Collections.ObjectModel;
using System.Diagnostics;
using CommonUtil.Controls;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP.Infrasturcture.Interface
{

    /// <summary>
    /// 发送接口
    /// </summary>
    public interface ISendInterface
    {
        /// <summary>
        /// 制动测试确认
        /// </summary>
        /// <param name="brakeTest"></param>
        /// <returns></returns>
        bool EnsurceBrakeTestSelect(SendModel<BrakeTestModel> brakeTest);

        /// <summary>
        /// 制动测试选择
        /// </summary>
        /// <param name="brakeTest"></param>
        /// <returns></returns>
        bool SendBrakeTestSelect(SendModel<BrakeTestModel> brakeTest);

        /// <summary>
        /// 辅屏测试
        /// </summary>
        /// <param name="assistScreenTestModel"></param>
        /// <returns></returns>
        bool SendAssistScreenTest(SendModel<AssistScreenTestModel> assistScreenTestModel);

        /// <summary>
        /// 发送车次号
        /// </summary>
        /// <param name="trainId"></param>
        /// <returns></returns>
        bool SendTrainId(SendModel<DriverDataModel> trainId);

        /// <summary>
        /// 发送司机号
        /// </summary>
        /// <param name="driverid"></param>
        /// <returns></returns>
        bool SendDriverId(SendModel<DriverDataModel> driverid);

        /// <summary>
        /// 发送司机号和车次号
        /// </summary>
        /// <param name="driverid"></param>
        /// <returns></returns>
        bool SendDriverData(SendModel<DriverDataModel> driverid);

        /// <summary>
        /// 发送列车数据
        /// </summary>
        /// <param name="traindata"></param>
        /// <returns></returns>
        bool SendTrainData(SendModel<ReadOnlyCollection<string>> traindata);

        /// <summary>
        /// 发送启动
        /// </summary>
        /// <param name="startState"></param>
        /// <returns></returns>
        bool SendStart(SendModel<object> startState);

        /// <summary>
        /// 缓解
        /// </summary>
        /// <param name="relaseState"></param>
        /// <returns></returns>
        bool SendRelease(SendModel<object> relaseState);

        /// <summary>
        /// 警惕
        /// </summary>
        /// <returns></returns>
        bool SendAlert();

        /// <summary>
        /// RBC 数据（包括RBCid和电话号码等）
        /// </summary>
        /// <param name="rbcData"></param>
        /// <returns></returns>
        bool SendRBCData(SendModel<RBCDataModel> rbcData);

        /// <summary>
        /// 选择载频
        /// </summary>
        /// <param name="trainFreq"></param>
        /// <returns></returns>
        bool SelectFreq(SendModel<TrainFreq> trainFreq);

        /// <summary>
        /// 选择CTCS
        /// </summary>
        /// <param name="ctcsType"></param>
        /// <returns></returns>
        bool SelectCtcs(SendModel<CTCSType> ctcsType);

        /// <summary>
        /// 选择控制模式
        /// </summary>
        /// <param name="controlType"></param>
        /// <returns></returns>
        bool SelectControlMode(SendModel<ControlType> controlType);

        //启动流程相关
        /// <summary>
        /// 发送 确认列车数据
        /// </summary>
        /// <returns></returns>
        bool SendTrainDataConfirm();

        /// <summary>
        /// 发送 确认载频
        /// </summary>
        /// <returns></returns>
        bool SendFreqConfirm();

        /// <summary>
        /// 确认一个消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        bool EnsureMessage(SendModel<IInfomationItem> message);

        /// <summary>
        /// DMI 测试
        /// </summary>
        /// <param name="dmiModel"></param>
        /// <returns></returns>
        bool SendDMITest(SendModel<object> dmiModel);

        /// <summary>
        /// 通知消息计时变化 
        /// </summary>
        /// <param name="messageItem"></param>
        /// <returns></returns>
        bool OnMessageTimeChanged(IMessageItem messageItem);

        /// <summary>
        /// 响应用户动作，目前只有root界面 B10按键触发
        ///     用于快速跳过启动界面等特殊需求
        /// </summary>
        /// <param name="actionType"></param>
        /// <param name="mouseState"></param>
        /// <returns></returns>
        bool OnUserAction(UserActionType actionType, MouseState mouseState);
    }

    /// <summary>
    /// 封装的发送的数据 。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DebuggerDisplay("Type={Type}, Content={Content}")]
    public class SendModel<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        [DebuggerStepThrough]
        public SendModel(T content = default(T), SendModelType contentType = SendModelType.Ok)
        {
            Content = content;
            Type = contentType;
        }

        /// <summary>
        /// 具体发送的数据
        /// </summary>
        public T Content { private set; get; }

        /// <summary>
        /// 发送的数据的类型，Ok or Cancel
        /// </summary>
        public SendModelType Type { private set; get; }

    }

    /// <summary>
    /// 
    /// </summary>
    public enum SendModelType
    {
        /// <summary>
        /// 
        /// </summary>
        Ok,
        /// <summary>
        /// 
        /// </summary>
        Cancel,
    }
}