using System;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace ShenHuaHaoTMS.Common
{
    public class ListenViewChangedBridge : IDataListener
    {
        public event EventHandler<CommunicationDataChangedArgs<float>> FloatChanged;

        public event EventHandler<CommunicationDataChangedArgs<bool>> BoolChanged;

        public event EventHandler<CommunicationDataChangedArgs> DataChanged;

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            if (BoolChanged != null)
            {
                BoolChanged(sender, dataChangedArgs);
            }
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            if (FloatChanged != null)
            {
                FloatChanged(sender, dataChangedArgs);
            }
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            if (DataChanged != null)
            {
                DataChanged(sender, dataChangedArgs);
            }
        }

    }
}