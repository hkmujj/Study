using System;
using MMI.Facility.Interface.Service;

namespace Motor.ATP.Infrasturcture.Interface.Service
{
    public interface IClearDataService : IService
    {
        event Action<object> ClearDataEvent;

        void NotifyClearData(object notifier, bool notifyInUiThread = true);
    }
}