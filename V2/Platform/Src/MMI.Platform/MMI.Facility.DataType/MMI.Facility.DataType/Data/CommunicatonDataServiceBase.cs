using System;
using System.Collections.Generic;
using System.Linq;
using MMI.Communacation.Interface.AppLayer;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config.NetDataPackage;
using MMI.Facility.Interface.IndexDescription;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.DataType.Data
{
    public abstract class CommunicatonDataServiceBase : IWritableCommunicationDataReadService
    {
        public CommunicationDataEntity CommunicationDataEntity { private set; get; }

        protected CommunicationChangedDataBuffer m_ChangedDataBuffer;

        public DictionaryProxy<int, bool> BoolDic
        {

            get { return CommunicationDataEntity.BoolDic; }
        }

        public DictionaryProxy<int, float> FloatDic
        {
            get { return CommunicationDataEntity.FloatDic; }
        }


        public IReadOnlyDictionary<int, bool> ReadOnlyBoolDictionary
        {
            get { return CommunicationDataEntity.ReadOnlyBoolDictionary; }
        }

        public IReadOnlyDictionary<int, float> ReadOnlyFloatDictionary
        {
            get { return CommunicationDataEntity.FloatDic; }
        }

        public IReadOnlyDictionary<int, bool> ReadOnlyBoolOldDictionary
        {
            get { return CommunicationDataEntity.ReadOnlyBoolOldDictionary; }
        }

        public IReadOnlyDictionary<int, float> ReadOnlyFloatOldDictionary
        {
            get { return CommunicationDataEntity.ReadOnlyFloatOldDictionary; }
        }

        public event EventHandler<CommunicationDataChangedArgs<float>> FloatChanged;

        public event EventHandler<CommunicationDataChangedArgs<bool>> BoolChanged;

        public event EventHandler<CommunicationDataChangedArgs> DataChanged;

        protected CommunicatonDataServiceBase(CommunicationDataEntity communicationDataEntity)
        {
            CommunicationDataEntity = communicationDataEntity;
            CommunicationDataEntity.FloatChanged += NotifyFloatChanged;
            CommunicationDataEntity.BoolChanged += NotifyBoolChanged;
            CommunicationDataEntity.DataChanged += NotifyDataChanged;


            BoolList =
                communicationDataEntity.BoolList.Select(s => new IndexValueDescriptionModelWrapper<int, bool>(s))
                    .Cast<IndexValueDescriptionModel<int, bool>>()
                    .ToList();

            FloatList = communicationDataEntity.FloatList.Select(s => new IndexValueDescriptionModelWrapper<int, float>(s))
                    .Cast<IndexValueDescriptionModel<int, float>>()
                    .ToList();

        }

        protected virtual void NotifyDataChanged(object sender, CommunicationDataChangedArgs e)
        {
            if (DataChanged != null)
            {
                DataChanged(this, e);
            }
        }

        protected virtual void NotifyBoolChanged(object sender, CommunicationDataChangedArgs<bool> e)
        {
            if (BoolChanged  != null)
            {
                BoolChanged(this, e);
            }
        }

        protected virtual void NotifyFloatChanged(object sender, CommunicationDataChangedArgs<float> e)
        {
            if (FloatChanged != null)
            {
                FloatChanged(this, e);
            }
        }

        protected abstract void InitChangedDataBuffer(INetDataConfig netDataConfig);

        protected abstract void InitBoolDic(INetDataConfig config);

        protected abstract void InitFloatDic(INetDataConfig config);

        public virtual void NetServiceOnDataReceived(NetDatas netDatas)
        {

        }

        protected virtual void OnCourseStart()
        {

        }

        protected virtual void OnCourseStop()
        {
            ClearDatasOnCourseEnd();
        }

        public bool GetBoolAt(int index)
        {
            return CommunicationDataEntity.GetBoolAt(index);
        }

        public float GetFloatAt(int index)
        {
            return CommunicationDataEntity.GetFloatAt(index);
        }

        public void RaiseAllDataChanged()
        {
            RaiseDataChanged();
        }

        public void RaiseDataChanged(CommunicationDataChangedArgs changedArgs = null)
        {
            CommunicationDataEntity.RaiseDataChanged(changedArgs);
        }

        public bool ChangeBools(IDictionary<int, bool> changedBools, bool notifyDataChangedEvent = false)
        {
            CommunicationDataChangedArgs<bool> changed;
            var rlt = CommunicationDataEntity.ChangeBools(changedBools, out changed);
            if (notifyDataChangedEvent)
            {
                NotifyDataChanged(this, new CommunicationDataChangedArgs(changed, CommunicationDataChangedArgs<float>.Empty));
            }
            return rlt;
        }

        public bool ChangeBools(IDictionary<int, bool> changedBools, out CommunicationDataChangedArgs<bool> changed)
        {
            return CommunicationDataEntity.ChangeBools(changedBools, out changed);
        }

        public bool ChangeBools(List<bool> changedBools, int startIndex)
        {
            return CommunicationDataEntity.ChangeBools(changedBools, startIndex);
        }

        public bool ChangeBools(List<bool> changedBools, int startIndex, out CommunicationDataChangedArgs<bool> changed)
        {
            return CommunicationDataEntity.ChangeBools(changedBools, startIndex, out changed);
        }

        public bool ChangeBool(int index, bool value, bool notifyDataChangedEvent = false)
        {
            return CommunicationDataEntity.ChangeBool(index, value, notifyDataChangedEvent);
        }

        public bool ChangeBool(int index, bool value, out CommunicationDataChangedArgs<bool> changed)
        {
            return CommunicationDataEntity.ChangeBool(index, value, out changed);
        }

        protected virtual void ClearBools(out CommunicationDataChangedArgs<bool> changed)
        {
            CommunicationDataEntity.ClearBools(out changed);
        }

        public bool ChangeFloats(IDictionary<int, float> changedFloats, bool notifyDataChangedEvent = false)
        {
            CommunicationDataChangedArgs<float> changed;
            var rlt = CommunicationDataEntity.ChangeFloats(changedFloats, out changed);
            if (notifyDataChangedEvent)
            {
                NotifyDataChanged(this, new CommunicationDataChangedArgs(CommunicationDataChangedArgs<bool>.Empty, changed));
            }

            return rlt;
        }

        public bool ChangeFloats(IDictionary<int, float> changedFloats, out CommunicationDataChangedArgs<float> changed)
        {
            return CommunicationDataEntity.ChangeFloats(changedFloats, out changed);
        }

        public bool ChangeFloats(List<float> changedFloats, int startIndex)
        {
            return CommunicationDataEntity.ChangeFloats(changedFloats, startIndex);
        }

        public bool ChangeFloats(List<float> changedFloats, int startIndex,
            out CommunicationDataChangedArgs<float> changed)
        {
            return CommunicationDataEntity.ChangeFloats(changedFloats, startIndex, out changed);
        }

        public bool ChangeFloat(int index, float value, bool notifyDataChangedEvent = false)
        {
            return CommunicationDataEntity.ChangeFloat(index, value, notifyDataChangedEvent);
        }

        public bool ChangeFloat(int index, float value, out CommunicationDataChangedArgs<float> changed)
        {
            return CommunicationDataEntity.ChangeFloat(index, value, out changed);
        }

        protected virtual void ClearFloats(out CommunicationDataChangedArgs<float> changed)
        {
            CommunicationDataEntity.ClearFloats(out changed);
        }

        protected void ClearDatasOnCourseEnd()
        {
            CommunicationDataEntity.ClearDatasOnCourseEnd();
        }

        public virtual void Dispose()
        {

        }

        /// <summary>
        /// 是否可写
        /// </summary>
        public bool IsReadonly
        {
            get { return false; }
        }

        public IList<IndexValueDescriptionModel<int, bool>> BoolList { private set; get; }

        public IList<IndexValueDescriptionModel<int, float>> FloatList { private set; get; }
    }
}
