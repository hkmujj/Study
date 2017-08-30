using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.Infrasturcture.Model.Constance;
using Subway.TCMS.Infrasturcture.Model.Recive;
using Subway.TCMS.Infrasturcture.Model.Send;

namespace Subway.TCMS.Infrasturcture.Model
{
    /// <summary>
    /// 车辆屏Model根节点
    /// </summary>
    public class TCMS : NotificationObject
    {
        /// <summary>
        /// 车辆类型
        /// </summary>
        public TCMSType Type { get; protected set; }
        /// <summary>
        /// 接收数据模型
        /// </summary>
        public ISendInterface SendInterface { get; set; }
        /// <summary>
        /// 屏接收外部请求数据
        /// </summary>
        public IReciveInterface ReciveInterface { get; set; }
    }
}
