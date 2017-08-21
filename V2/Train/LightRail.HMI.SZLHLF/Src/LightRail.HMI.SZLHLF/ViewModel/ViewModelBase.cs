using LightRail.HMI.SZLHLF.Interface;
using LightRail.HMI.SZLHLF.Model;
using MMI.Facility.Interface.Data;

namespace LightRail.HMI.SZLHLF.ViewModel
{
    public class ViewModelBase : ModelBase, IDataChanged, IClear, ICourceStatusChanged
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
