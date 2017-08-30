using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.Infrasturcture.Model.Recive;

namespace Subway.TCMS.Infrasturcture.Model.Monitor.Detail
{
    /// <summary>
    /// 屏请求监视
    /// </summary>
    public class ReciveMonitor : NotificationObject, IReciveInterface
    {
        private readonly IReciveInterface ReciveInterface;

        public ReciveMonitor(IReciveInterface reciveInterface)
        {
            ReciveInterface = reciveInterface;
        }
    }
}
