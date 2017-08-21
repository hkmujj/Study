using Motor.HMI.CRH1A.Alarm.Fault;

namespace Motor.HMI.CRH1A.Alarm.NotifyFilter
{
    /// <summary>
    /// 通知逻辑的过滤接口
    /// </summary>
    interface INotifyFilter
    {
        bool Filter(ExceptionData data);
    }
}
