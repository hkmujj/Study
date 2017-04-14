using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using CommonUtil.Util.Extension;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Communacation.Interface.AppLayer;
using MMI.Facility.DataType.Extension;
using MMI.Facility.DataType.Running;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.NetDataPackage;
using MMI.Facility.Interface.IndexDescription;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.DataType.Data
{
    public class CommunicationDataEntity : NotificationObject, ICommunicationDataEntity
    {
        private DictionaryProxy<int, bool> m_BoolDic;
        private DictionaryProxy<int, float> m_FloatDic;
        private AdapterChangedDataReadOnlyDictionary<int, bool> m_ReadOnlyBoolOldDictionary;
        private AdapterChangedDataReadOnlyDictionary<int, float> m_ReadOnlyFloatOldDictionary;

        protected CommunicationChangedDataBuffer m_ChangedDataBuffer;

        public DictionaryProxy<int, bool> BoolDic
        {
            protected set
            {

                m_BoolDic = value;
                value.ValueChanged += BoolValueOnValueChanged;
                m_ReadOnlyBoolOldDictionary = new AdapterChangedDataReadOnlyDictionary<int, bool>(value);
                ReadOnlyBoolDictionary = DictionaryExtention.AsReadOnlyDictionary(value);
            }
            get { return m_BoolDic; }
        }

        public DictionaryProxy<int, float> FloatDic
        {
            protected set
            {
                m_FloatDic = value;
                value.ValueChanged += FloatValueOnValueChanged;
                m_ReadOnlyFloatOldDictionary = new AdapterChangedDataReadOnlyDictionary<int, float>(value);
                ReadOnlyFloatDictionary = DictionaryExtention.AsReadOnlyDictionary(value);
            }
            get { return m_FloatDic; }
        }


        public IReadOnlyDictionary<int, bool> ReadOnlyBoolDictionary { get; protected set; }

        public IReadOnlyDictionary<int, float> ReadOnlyFloatDictionary { get; protected set; }

        public IReadOnlyDictionary<int, bool> ReadOnlyBoolOldDictionary
        {
            get { return m_ReadOnlyBoolOldDictionary; }
        }

        public IReadOnlyDictionary<int, float> ReadOnlyFloatOldDictionary
        {
            get { return m_ReadOnlyFloatOldDictionary; }
        }

        public event EventHandler<CommunicationDataChangedArgs<float>> FloatChanged;

        public event EventHandler<CommunicationDataChangedArgs<bool>> BoolChanged;

        public event EventHandler<CommunicationDataChangedArgs> DataChanged;

        protected virtual void FloatValueOnValueChanged(DictionaryProxy<int, float> dictionaryProxy,
            IndexValueDescriptionModel<int, float> indexValueDescriptionModel)
        {
            var changed = m_ChangedDataBuffer.GetCleanedFloat();
            var newValue = changed.NewValue;
            newValue.Add(indexValueDescriptionModel.Index, indexValueDescriptionModel.Value);

            NotifyFloatChanged(changed);
            NotifyDataChanged(new CommunicationDataChangedArgs(CommunicationDataChangedArgs<bool>.Empty, changed));

        }

        protected virtual void BoolValueOnValueChanged(DictionaryProxy<int, bool> dictionaryProxy,
            IndexValueDescriptionModel<int, bool> indexValueDescriptionModel)
        {
            var changed = m_ChangedDataBuffer.GetCleanedBool();
            var oldValue = changed.OldValue;
            var newValue = changed.NewValue;
            newValue.Add(indexValueDescriptionModel.Index, indexValueDescriptionModel.Value);

            NotifyBoolChanged(changed);
            NotifyDataChanged(new CommunicationDataChangedArgs(changed, CommunicationDataChangedArgs<float>.Empty));
        }

        public CommunicationDataEntity(IConfig config, INetDataPackageConfig netDataConfig)
        {

            InitBoolDic(netDataConfig);

            InitFloatDic(netDataConfig);

            InitChangedDataBuffer(netDataConfig);

            var vs = App.Current.ServiceManager.GetService<IRunningViewService>();
            vs.AcitvedFormChanged += VsOnAcitvedFormChanged;

            var es = App.Current.ServiceManager.GetService<IEventService>();
            es.CoursStarting += args => OnCourseStart();
            es.CourseStopping += args => OnCourseStop();

        }

        protected virtual void InitChangedDataBuffer(INetDataPackageConfig netDataConfig)
        {
            m_ChangedDataBuffer =
                new CommunicationChangedDataBuffer(netDataConfig.GetTotalBoolCount(),
                    netDataConfig.GetTotalFloatCount());
        }

        private void VsOnAcitvedFormChanged(IRunningViewService runningViewService,
            NotifyCollectionChangedAction notifyCollectionChangedAction)
        {
            m_ReadOnlyBoolOldDictionary.UpdateActivedProjectNameCollection();
            m_ReadOnlyFloatOldDictionary.UpdateActivedProjectNameCollection();
        }

        protected virtual void InitBoolDic(INetDataPackageConfig config)
        {
            BoolDic =
                new DictionaryProxy<int, bool>(
                    Enumerable.Range(config.BoolMappedStartIndex,
                        config.GetTotalBoolCount())
                        .Select(s => new IndexValueDescriptionModel<int, bool>() {Index = s})
                        .ToList());
        }

        protected virtual void InitFloatDic(INetDataPackageConfig config)
        {
            FloatDic = new DictionaryProxy<int, float>(
                Enumerable.Range(config.FloatMappedStartIndex,
                    config.GetTotalFloatCount())
                    .Select(s => new IndexValueDescriptionModel<int, float>() {Index = s})
                    .ToList());
        }

        protected void NotifyBoolChanged(CommunicationDataChangedArgs<bool> arg)
        {
            m_ReadOnlyBoolOldDictionary.AddChanges(arg.OldValue);

            if (BoolChanged != null)
            {
                BoolChanged(this, arg);
            }
        }

        public virtual void PresentationLayerNetServiceOnDataReceived(NetDatas netDatas)
        {
            CommunicationDataChangedArgs<bool> changedBool;
            ChangeBools(netDatas.ReceivedBools.DataList, netDatas.ReceivedBools.StartIndex, out changedBool);

            CommunicationDataChangedArgs<float> changedFloat;
            ChangeFloats(netDatas.ReceivedFloats.DataList, netDatas.ReceivedFloats.StartIndex, out changedFloat);

            NotifyDataChanged(new CommunicationDataChangedArgs(changedBool, changedFloat));
        }

        protected virtual void OnCourseStart()
        {

        }

        protected virtual void OnCourseStop()
        {
            ClearDatasOnCourseEnd();
        }

        protected void NotifyFloatChanged(CommunicationDataChangedArgs<float> arg)
        {
            m_ReadOnlyFloatOldDictionary.AddChanges(arg.OldValue);

            if (FloatChanged != null)
            {
                FloatChanged(this, arg);
            }
        }

        public bool GetBoolAt(int index)
        {
            Debug.Assert(index <= BoolDic.Count);
            return BoolDic[index];
        }

        public float GetFloatAt(int index)
        {
            Debug.Assert(index <= FloatDic.Count);
            return FloatDic[index];
        }

        public void RaiseAllDataChanged()
        {
            RaiseDataChanged();
        }

        public void RaiseDataChanged(CommunicationDataChangedArgs changedArgs = null)
        {
            if (changedArgs == null)
            {
                var bs = m_ChangedDataBuffer.GetCleanedBool(RaiseCommunicationDataChangedType.ByUserManul);
                var fs = m_ChangedDataBuffer.GetCleanedFloat(RaiseCommunicationDataChangedType.ByUserManul);
                foreach (var kvp in BoolDic)
                {
                    bs.NewValue.Add(kvp.Key, kvp.Value);
                }
                foreach (var kvp in FloatDic)
                {
                    fs.NewValue.Add(kvp.Key, kvp.Value);
                }

                changedArgs = new CommunicationDataChangedArgs(bs, fs, RaiseCommunicationDataChangedType.ByUserManul);
            }

            NotifyBoolChanged(changedArgs.ChangedBools);
            NotifyFloatChanged(changedArgs.ChangedFloats);
            NotifyDataChanged(changedArgs);
        }

        public bool ChangeBools(IDictionary<int, bool> changedBools)
        {
            CommunicationDataChangedArgs<bool> changed;
            return ChangeBools(changedBools, out changed);
        }

        public bool ChangeBools(IDictionary<int, bool> changedBools, out CommunicationDataChangedArgs<bool> changed)
        {
            changed = m_ChangedDataBuffer.GetCleanedBool();
            var oldValue = changed.OldValue;
            var newValue = changed.NewValue;

            foreach (var changedBool in changedBools)
            {
                if (changedBool.Value != BoolDic[changedBool.Key])
                {
                    oldValue.Add(changedBool.Key, BoolDic[changedBool.Key]);
                    newValue.Add(changedBool.Key, changedBool.Value);
                    BoolDic[changedBool.Key] = changedBool.Value;
                }
            }

            if (newValue.Count != 0)
            {
                NotifyBoolChanged(changed);
                return true;
            }

            changed = CommunicationDataChangedArgs<bool>.Empty;
            return false;
        }

        public bool ChangeBools(List<bool> changedBools, int startIndex)
        {
            CommunicationDataChangedArgs<bool> changed;
            return ChangeBools(changedBools, startIndex, out changed);
        }

        public bool ChangeBools(List<bool> changedBools, int startIndex, out CommunicationDataChangedArgs<bool> changed)
        {
            changed = m_ChangedDataBuffer.GetCleanedBool();
            var oldValue = changed.OldValue;
            var newValue = changed.NewValue;

            for (var i = 0; i < changedBools.Count; i++)
            {
                var idx = startIndex + i;

                if (changedBools[i] != BoolDic[idx])
                {
                    oldValue.Add(idx, BoolDic[idx]);
                    newValue.Add(idx, changedBools[i]);
                    BoolDic[idx] = changedBools[i];
                }
            }

            if (newValue.Count != 0)
            {
                NotifyBoolChanged(changed);
                return true;
            }

            changed = CommunicationDataChangedArgs<bool>.Empty;
            return false;
        }

        public bool ChangeBool(int index, bool value, bool notifyDataChangedEvent = false)
        {
            CommunicationDataChangedArgs<bool> changed;
            var rlt = ChangeBool(index, value, out changed);
            if (notifyDataChangedEvent)
            {
                NotifyDataChanged(new CommunicationDataChangedArgs(changed, CommunicationDataChangedArgs<float>.Empty));
            }
            return rlt;
        }

        public bool ChangeBool(int index, bool value, out CommunicationDataChangedArgs<bool> changed)
        {
            changed = m_ChangedDataBuffer.GetCleanedBool();
            var oldValue = changed.OldValue;
            var newValue = changed.NewValue;

            if (value != BoolDic[index])
            {
                oldValue.Add(index, BoolDic[index]);
                BoolDic[index] = value;
                newValue.Add(index, BoolDic[index]);
                NotifyBoolChanged(changed);
                return true;
            }

            return false;
        }

        public virtual void ClearBools(out CommunicationDataChangedArgs<bool> changed)
        {
            changed = m_ChangedDataBuffer.GetCleanedBool();
            var oldValue = changed.OldValue;
            var newValue = changed.NewValue;
            var startIndex = BoolDic.Min(m => m.Key);

            for (var i = 0; i < BoolDic.Count; i++)
            {
                var idx = startIndex + i;

                if (BoolDic[idx])
                {
                    oldValue.Add(idx, BoolDic[idx]);
                    newValue.Add(idx, false);
                    BoolDic[idx] = false;
                }
            }

            if (newValue.Count != 0)
            {
                NotifyBoolChanged(changed);
            }
        }

        public bool ChangeFloats(IDictionary<int, float> changedFloats)
        {
            CommunicationDataChangedArgs<float> changed;
            return ChangeFloats(changedFloats, out changed);
        }

        public bool ChangeFloats(IDictionary<int, float> changedFloats, out CommunicationDataChangedArgs<float> changed)
        {
            changed = m_ChangedDataBuffer.GetCleanedFloat();
            var oldValue = changed.OldValue;
            var newValue = changed.NewValue;

            foreach (var changedFloat in changedFloats)
            {
                if (Math.Abs(changedFloat.Value - FloatDic[changedFloat.Key]) > float.Epsilon)
                {
                    oldValue.Add(changedFloat.Key, FloatDic[changedFloat.Key]);
                    newValue.Add(changedFloat.Key, changedFloat.Value);
                    FloatDic[changedFloat.Key] = changedFloat.Value;
                }
            }

            if (newValue.Count != 0)
            {
                NotifyFloatChanged(changed);
                return true;
            }

            return false;
        }

        public bool ChangeFloats(List<float> changedFloats, int startIndex)
        {
            CommunicationDataChangedArgs<float> changed;
            return ChangeFloats(changedFloats, startIndex, out changed);
        }

        public bool ChangeFloats(List<float> changedFloats, int startIndex,
            out CommunicationDataChangedArgs<float> changed)
        {
            changed = m_ChangedDataBuffer.GetCleanedFloat();
            var oldValue = changed.OldValue;
            var newValue = changed.NewValue;

            for (var i = 0; i < changedFloats.Count; i++)
            {
                var idx = startIndex + i;

                if (changedFloats[i].IsDifferentTo(FloatDic[idx]))
                {
                    oldValue.Add(idx, FloatDic[idx]);
                    newValue.Add(idx, changedFloats[i]);
                    FloatDic[idx] = changedFloats[i];
                }
            }

            if (newValue.Count != 0)
            {
                NotifyFloatChanged(changed);
                return true;
            }

            return false;
        }

        public bool ChangeFloat(int index, float value, bool notifyDataChangedEvent = false)
        {
            CommunicationDataChangedArgs<float> changed;
            var rlt = ChangeFloat(index, value, out changed);
            if (notifyDataChangedEvent)
            {
                NotifyDataChanged(new CommunicationDataChangedArgs(CommunicationDataChangedArgs<bool>.Empty, changed));
            }
            return rlt;
        }

        public bool ChangeFloat(int index, float value, out CommunicationDataChangedArgs<float> changed)
        {
            changed = m_ChangedDataBuffer.GetCleanedFloat();
            var oldValue = changed.OldValue;
            var newValue = changed.NewValue;

            if (FloatDic[index].IsDifferentTo(value))
            {
                oldValue.Add(index, FloatDic[index]);
                FloatDic[index] = value;
                newValue.Add(index, FloatDic[index]);
                NotifyFloatChanged(changed = new CommunicationDataChangedArgs<float>(oldValue, newValue));
                return true;
            }

            return false;
        }

        public virtual void ClearFloats(out CommunicationDataChangedArgs<float> changed)
        {
            changed = CommunicationDataChangedArgs<float>.Empty;
            var oldValue = new Dictionary<int, float>(FloatDic.Count);
            var newValue = new Dictionary<int, float>(FloatDic.Count);
            var startIndex = FloatDic.Min(m => m.Key);

            for (var i = 0; i < FloatDic.Count; i++)
            {
                var idx = startIndex + i;

                if (FloatDic[idx].IsDifferentTo(0))
                {
                    oldValue.Add(idx, FloatDic[idx]);
                    newValue.Add(idx, 0);
                    FloatDic[idx] = 0;
                }
            }

            if (newValue.Count != 0)
            {
                NotifyFloatChanged(changed = new CommunicationDataChangedArgs<float>(oldValue, newValue));
            }
        }

        internal virtual void ClearDatasOnCourseEnd()
        {
            CommunicationDataChangedArgs<bool> changedBool;
            ClearBools(out changedBool);

            CommunicationDataChangedArgs<float> changedFloat;
            ClearFloats(out changedFloat);

            NotifyDataChanged(new CommunicationDataChangedArgs(changedBool, changedFloat));
        }

        public virtual void Dispose()
        {

        }

        protected void NotifyDataChanged(CommunicationDataChangedArgs e)
        {
            var handler = DataChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// 是否可写
        /// </summary>
        public bool IsReadonly
        {
            get { return false; }
        }

        public IList<IndexValueDescriptionModel<int, bool>> BoolList
        {
            get { return BoolDic.ProxyValues; }
        }

        public IList<IndexValueDescriptionModel<int, float>> FloatList
        {
            get { return FloatDic.ProxyValues; }
        }
    }
}