using Engine.LCDM.HXD3.Interfaces;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;

namespace Engine.LCDM.HXD3.ViewModels
{
    public class ViewModelBase : NotificationObject, IDataChanged, IClear, ICourceStatusChanged
    {
        public virtual void DataChanged(object sender, CommunicationDataChangedArgs<bool> args)
        {

        }

        public virtual void DataChanged(object sender, CommunicationDataChangedArgs<float> args)
        {

        }

        public virtual void Clear()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Stop()
        {

        }
    }
}