using System.Diagnostics;
using Motor.ATP.Domain.Interface.Infomation;

namespace Motor.ATP.Domain.Interface
{

    /// <summary>
    /// 发送接口
    /// </summary>
    public interface ISendInterface
    {

        bool SendTrainId(string trainId);

        bool SendDriverId(string driverid);

        bool SendTrainData(string traindata);

        /// <summary>
        /// 驾驶数据
        /// </summary>
        /// <param name="trainid"></param>
        /// <param name="driverid"></param>
        /// <param name="rbcId"></param>
        /// <param name="tel"></param>
        /// <returns></returns>
        bool SendDriverData(string trainid, string driverid, string rbcId, string tel);


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

        bool SendRBCData(SendModel<RBCDataModel> rbcData);

        /// <summary>
        /// 制动测试
        /// </summary>
        /// <returns></returns>
        bool SendBreakTest();

        bool SelectFreq(SendModel<TrainFreq> trainFreq);

        bool SelectCtcs(SendModel<CTCSType> ctcsType);

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

        bool EnsureMessage(SendModel<IInfomationItem> message);
    }

    [DebuggerDisplay("Type={Type}, Content={Content}")]
    public class SendModel<T>
    {
        [DebuggerStepThrough]
        public SendModel(T content = default(T), SendModelType contentType = SendModelType.Ok)
        {
            Content = content;
            Type = contentType;
        }

        public T Content { private set; get; }

        public SendModelType Type { private set; get; }

    }

    public enum SendModelType
    {
        Ok,
        Cancel,
    }
}