using System;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using Subway.ShenZhenLine11.Interface;

namespace Subway.ShenZhenLine11.ViewModels
{
    public class SubViewModelBase : NotificationObject, IStatusChanged, IClear, IDisposable
    {
        public virtual void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {

        }

        public virtual void Changed(object sender, CommunicationDataChangedArgs<float> args)
        {

        }

        public virtual void Clear()
        {
            
        }

        public void Clear(Type type, object obj)
        {
            foreach (var p in type.GetProperties())
            {
                if (p.PropertyType == typeof(string))
                {
                    p.SetValue(obj, string.Empty, null);
                    continue;
                }
                if (p.PropertyType == typeof(int))
                {
                    p.SetValue(obj, 0, null);
                    continue;
                }
                if (p.PropertyType == typeof(double))
                {
                    p.SetValue(obj, 0, null);
                }
            }

        }

        public virtual void Dispose()
        {

        }
    }
}