using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.Infrasturcture.Model.Send;

namespace Subway.TCMS.Infrasturcture.Model.Monitor.Detail
{
    /// <summary>
    /// 屏发送数据监视
    /// </summary>
    public class SendMonitor : NotificationObject, ISendInterface
    {
        private readonly ISendInterface m_SendInterface;

        public SendMonitor(ISendInterface sendInterface)
        {
            m_SendInterface = sendInterface;
        }
    }
}
