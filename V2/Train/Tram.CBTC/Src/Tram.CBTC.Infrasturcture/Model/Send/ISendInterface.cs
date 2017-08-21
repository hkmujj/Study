using System.Diagnostics;
using System.Windows.Documents;
using Tram.CBTC.Infrasturcture.Events;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Msg.Details;

namespace Tram.CBTC.Infrasturcture.Model.Send
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
        /// 确认密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>匹配成功true  匹配失败 false</returns>
        bool ConfirmPassword(SendModel<string> password);

        /// <summary>
        /// 请求开启／关闭雷达
        /// </summary>
        /// <param name="open">开启／关闭</param>
        /// <returns>返回是否允许</returns>
        bool OpenRadar(SendModel<bool> open);


        /// <summary>
        /// 输入车次号
        /// </summary>
        /// <param name="trainId"></param>
        /// <returns></returns>
        bool InputTrainId(SendModel<string> trainId);


        /// <summary>
        /// 输入线路运行方向
        /// </summary>
        /// <param name="lineRunDirection"></param>
        /// <returns></returns>
        bool InputLineRunDirection(SendModel<LineRunDirection> lineRunDirection);



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
        /// 打开关闭声音
        /// </summary>
        /// <param name="sound"></param>
        /// <returns></returns>
        bool OpenSound(SendModel<bool> sound);
        /// <summary>
        /// 选择车载联机模式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SelectVehicleRunningModel(SendModel<VehicleRunningModel> model);
        /// <summary>
        /// 发送线路号
        /// </summary>
        /// <param name="lineId">LineID</param>
        /// <returns></returns>
        bool SendLineID(SendModel<string> lineId);
        /// <summary>
        /// 发送终点站
        /// </summary>
        /// <param name="endStation"></param>
        /// <returns></returns>
        bool SendEndStation(SendModel<string> endStation);
        /// <summary>
        /// 发送计划，车次，单程
        /// </summary>
        /// <param name="planinfo"></param>
        /// <returns></returns>
        bool SendPlanInfo(SendModel<PlanInfo> planinfo);
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