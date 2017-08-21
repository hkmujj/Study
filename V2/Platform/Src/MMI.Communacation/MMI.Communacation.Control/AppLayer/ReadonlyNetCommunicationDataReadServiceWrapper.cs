using System;
using System.Collections.Generic;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.IndexDescription;
using MMI.Facility.Interface.Service;

namespace MMI.Communacation.Control.AppLayer
{
    internal class ReadonlyNetCommunicationDataReadServiceWrapper : IWritableCommunicationDataReadService
    {
        private readonly NetCommunicationDataReadService m_ConcreateReadService;

        public ReadonlyNetCommunicationDataReadServiceWrapper(NetCommunicationDataReadService concreateReadService)
        {
            m_ConcreateReadService = concreateReadService;
        }


        public void Dispose()
        {

        }

        IReadOnlyDictionary<int, bool> ICommunicationDataReadService.ReadOnlyBoolDictionary
        {
            get { return m_ConcreateReadService.ReadOnlyBoolDictionary; }
        }

        IReadOnlyDictionary<int, float> ICommunicationDataWriteService.ReadOnlyFloatDictionary
        {
            get { return m_ConcreateReadService.ReadOnlyFloatDictionary; }
        }

        public bool ChangeBools(IDictionary<int, bool> changedBools, bool notifyDataChangedEvent = false)
        {
            SysLog.Info(string.Format("Can not chage bools when using net , indexs : {0}",
                string.Join(",", changedBools.Keys)));
            return false;
        }

        public bool ChangeBools(IDictionary<int, bool> changedBools, out CommunicationDataChangedArgs<bool> changed)
        {
            changed = CommunicationDataChangedArgs<bool>.Empty;
            SysLog.Info(string.Format("Can not chage bools when using net , indexs : {0}",
                string.Join(",", changedBools.Keys)));
            return false;
        }

        public bool ChangeBools(List<bool> changedBools, int startIndex)
        {
            SysLog.Info(string.Format("Can not chage bools when using net , indexs : {0}",
                string.Join(",", changedBools)));
            return false;
        }

        public bool ChangeBools(List<bool> changedBools, int startIndex, out CommunicationDataChangedArgs<bool> changed)
        {
            changed = CommunicationDataChangedArgs<bool>.Empty;
            SysLog.Info(string.Format("Can not chage bools when using net , indexs : {0}",
                string.Join(",", changedBools)));
            return false;
        }

        public bool ChangeBool(int index, bool value, bool notifyDataChangedEvent = false)
        {
            SysLog.Info(string.Format("Can not chage bools when using net , indexs : {0}", index));
            return false;
        }

        public bool ChangeBool(int index, bool value, out CommunicationDataChangedArgs<bool> changed)
        {
            changed = CommunicationDataChangedArgs<bool>.Empty;
            SysLog.Info(string.Format("Can not chage bools when using net , indexs : {0}", index));
            return false;
        }

        public bool ChangeFloats(IDictionary<int, float> changedFloats, bool notifyDataChangedEvent = false)
        {
            SysLog.Info(string.Format("Can not chage floats when using net , indexs : {0}",
                string.Join(",", changedFloats.Keys)));
            return false;
        }

        public bool ChangeFloats(IDictionary<int, float> changedFloats, out CommunicationDataChangedArgs<float> changed)
        {
            changed = CommunicationDataChangedArgs<float>.Empty;
            SysLog.Info(string.Format("Can not chage floats when using net , indexs : {0}",
                string.Join(",", changedFloats.Keys)));
            return false;
        }


        public bool ChangeFloats(List<float> changedFloats, int startIndex)
        {
            SysLog.Info(string.Format("Can not chage floats when using net , indexs : {0}",
                string.Join(",", changedFloats)));
            return false;
        }

        public bool ChangeFloats(List<float> changedFloats, int startIndex,
            out CommunicationDataChangedArgs<float> changed)
        {
            changed = CommunicationDataChangedArgs<float>.Empty;
            SysLog.Info(string.Format("Can not chage floats when using net , indexs : {0}",
                string.Join(",", changedFloats)));
            return false;
        }


        public bool ChangeFloat(int index, float value, bool notifyDataChangedEvent = false)
        {
            SysLog.Info(string.Format("Can not chage floats when using net , indexs : {0}", index))
                ;
            return false;
        }

        public bool ChangeFloat(int index, float value, out CommunicationDataChangedArgs<float> changed)
        {
            changed = CommunicationDataChangedArgs<float>.Empty;
            SysLog.Info(string.Format("Can not chage floats when using net , indexs : {0}", index));

            return false;
        }


        IReadOnlyDictionary<int, bool> ICommunicationDataWriteService.ReadOnlyBoolDictionary
        {
            get { return ReadOnlyBoolOldDictionary; }
        }

        IReadOnlyDictionary<int, float> ICommunicationDataReadService.ReadOnlyFloatDictionary
        {
            get { return ReadOnlyFloatOldDictionary; }
        }

        public IReadOnlyDictionary<int, bool> ReadOnlyBoolOldDictionary
        {
            get { return m_ConcreateReadService.ReadOnlyBoolOldDictionary; }
        }

        public IReadOnlyDictionary<int, float> ReadOnlyFloatOldDictionary
        {
            get { return m_ConcreateReadService.ReadOnlyFloatOldDictionary; }
        }

        /// <summary>
        /// 模拟量发生变化
        /// </summary>
        public event EventHandler<CommunicationDataChangedArgs<float>> FloatChanged;

        public event EventHandler<CommunicationDataChangedArgs<bool>> BoolChanged;
        public event EventHandler<CommunicationDataChangedArgs> DataChanged;
        public event EventHandler NetServiceEnd;
        public event EventHandler NetServiceBegin;

        public bool GetBoolAt(int index)
        {
            return m_ConcreateReadService.GetBoolAt(index);
        }

        public float GetFloatAt(int index)
        {
            return m_ConcreateReadService.GetFloatAt(index);
        }

        public void RaiseAllDataChanged()
        {
            m_ConcreateReadService.RaiseAllDataChanged();
        }

        protected virtual void OnBoolChanged(CommunicationDataChangedArgs<bool> e)
        {
            var handler = BoolChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnDataChanged(CommunicationDataChangedArgs e)
        {
            var handler = DataChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnFloatChanged(CommunicationDataChangedArgs<float> e)
        {
            var handler = FloatChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnNetServiceEnd()
        {
            var handler = NetServiceEnd;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void OnNetServiceBegin()
        {
            var handler = NetServiceBegin;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public bool IsReadonly
        {
            get { return true; }
        }

        public IList<IndexValueDescriptionModel<int, bool>> BoolList
        {
            get { return m_ConcreateReadService.BoolList; }
        }

        public IList<IndexValueDescriptionModel<int, float>> FloatList
        {
            get { return m_ConcreateReadService.FloatList; }
        }
    }
}

