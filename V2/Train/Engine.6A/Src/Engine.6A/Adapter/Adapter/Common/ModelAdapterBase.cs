using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Engine._6A.Interface;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;

namespace Engine._6A.Adapter.Adapter.Common
{
    public class ModelAdapterBase : IDataChanged, IHeartTime
    {

        protected IEngineAdapter Adapter;
        public ModelAdapterBase(IEngineAdapter adapter)
        {
            Adapter = adapter;
        }

        public virtual void Changed(CommunicationDataChangedArgs<float> args)
        {

        }

        public virtual void Changed(CommunicationDataChangedArgs<bool> args)
        {

        }

        public virtual void DataChnged(object sender, CommunicationDataChangedArgs args)
        {
            Changed(args.ChangedBools);
            Changed(args.ChangedFloats);
        }

        public virtual void Heart()
        {

        }
    }
}
