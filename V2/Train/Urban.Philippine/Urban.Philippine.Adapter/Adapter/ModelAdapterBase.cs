using System;
using System.ComponentModel.Composition;
using MMI.Facility.Interface.Data;
using Urban.Philippine.Adapter.Interface;

namespace Urban.Philippine.Adapter.Adapter
{
    public class ModelAdapterBase : IModelAdapterBase
    {
        [Import]
        public IModelAdapter Adapter { get; private set; }

        public void DataChanged(object sender, CommunicationDataChangedArgs args)
        {
            DataChanged(args.ChangedBools);
            DataChanged(args.ChangedFloats);
        }

        public virtual void DataChanged(CommunicationDataChangedArgs<bool> args)
        {
        }

        public virtual void DataChanged(CommunicationDataChangedArgs<float> args)
        {
        }

        public virtual void Clear()
        {
        }

        public void Clear(object obj, Type type)
        {
        }
    }
}