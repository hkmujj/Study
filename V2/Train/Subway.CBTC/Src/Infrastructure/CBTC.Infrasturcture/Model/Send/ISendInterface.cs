using System.Diagnostics;
using CBTC.Infrasturcture.Model.Msg.Details;

namespace CBTC.Infrasturcture.Model.Send
{
    /// <summary>
    /// 发送接口
    /// </summary>
    public interface ISendInterface
    {

        /// <summary>
        /// 确认一个消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        bool EnsureMessage(SendModel<IInformationContent> message);

        /// <summary>
        /// 输入司机号
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        bool InputDriverId(SendModel<string> driverId);


        /// <summary>
        /// 输入亮度
        /// </summary>
        /// <param name="light"></param>
        /// <returns></returns>
        bool InputLight(SendModel<double> light);

        /// <summary>
        /// 输入音量
        /// </summary>
        /// <param name="volumn"></param>
        /// <returns></returns>
        bool InputVolumn(SendModel<double> volumn);

        ///<summary>
        /// 输入司机号数字
        /// </summary>
        /// <param name="driveridnum"></param>
        /// <returns></returns>
        bool InputDriverIDNum(SendModel<float> driveridnum);

        ///<summary>
        /// 输入司机号数字
        /// </summary>
        /// <param name="trainidnum"></param>
        /// <returns></returns>
        bool InputTrainIDNum(SendModel<float> trainidnum);
        ///<summary>
        /// 制动测试试闸
        /// </summary>
        ///<param name = "braketestswitch">
        /// <returen></returen>
        /// </param>
        bool InputBrakeTestSwitch(SendModel<bool> braketestswitch);

        ///<summary>
        /// 制动测试缓解
        /// </summary>
        ///<param name = "braketestrelease">
        /// <returen></returen>
        /// </param>
        bool InputBrakeTestRelease(SendModel<bool> braketestrelease);


        ///<summary>
        /// 广播测试
        /// </summary>
        ///<param name = "broadcasttest">
        /// <returen></returen>
        /// </param>
        bool InputBroadcastTest(SendModel<bool> broadcasttest);


        ///<summary>
        /// 灯测试
        /// </summary>
        ///<param name = "lamptest">
        /// <returen></returen>
        /// </param>
        bool InputLampTest(SendModel<bool> lamptest);

        /// <summary>
        /// 发送后备模式是否强制
        /// </summary>
        /// <param name="bm">true 强制 false 非强制</param>
        /// <returns>true 发送成功  false  发送失败</returns>
        bool InputBM(SendModel<bool> bm);


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