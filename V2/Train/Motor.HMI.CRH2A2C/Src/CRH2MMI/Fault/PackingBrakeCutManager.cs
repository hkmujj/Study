using System;
using System.Collections.Generic;
using System.Linq;
using CRH2MMI.Common;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;

namespace CRH2MMI.Fault
{
    internal class PackingBrakeCutManager : IDataListener
    {
        public static readonly PackingBrakeCutManager Instance = new PackingBrakeCutManager();
        private CRH2BaseClass m_SrcObj;
        private ICourseService m_CourseService;
        private List<PackingBrakeCutItem> m_PackingBrakeCutItems;
        private object m_Locker = new object();
        private bool m_HasAnyNotConfirm;

        public bool HasAnyNotConfirm
        {
            set
            {
                lock (m_Locker)
                {
                    m_HasAnyNotConfirm = value;
                }
                OnPackingBrakeCutChangedEvent();
            }
            get { return m_HasAnyNotConfirm; }
        }

        public event Action PackingBrakeCutChangedEvent;


        public List<PackingBrakeCutItem> PackingBrakeCutItems
        {

            get
            {
                return m_PackingBrakeCutItems ?? (m_PackingBrakeCutItems = new[]
                {
                    "故障提示/01车停放制动隔离",
                    "故障提示/02车停放制动隔离",
                    "故障提示/03车停放制动隔离",
                    "故障提示/04车停放制动隔离",
                    "故障提示/05车停放制动隔离",
                    "故障提示/06车停放制动隔离",
                    "故障提示/07车停放制动隔离",
                    "故障提示/08车停放制动隔离",
                }.Select((s, i) => new PackingBrakeCutItem(s, m_SrcObj.GetInBoolIndex(s), i)).ToList());
            }
        }

        private PackingBrakeCutManager()
        {

        }

        public void SetDataPackage(CRH2BaseClass srcObj)
        {
            if (m_SrcObj == null)
            {
                m_SrcObj = srcObj;
                m_SrcObj.RegistDataListener(this);
                m_CourseService = m_SrcObj.DataPackage.ServiceManager.GetService<ICourseService>();
                m_CourseService.CourseStateChanged +=
                    (sender, args) =>
                    {
                        if (m_CourseService.CurrentCourseState == CourseState.Stoped)
                        {
                            PackingBrakeCutItems.ForEach(e => e.State = PackingBrakeCutState.Normal);
                        }
                    };
            }

        }

        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> communicationDataChangedArgs)
        {
            bool flag = false;
            foreach (var item in PackingBrakeCutItems)
            {
                var item1 = item;
                communicationDataChangedArgs.UpdateIfContains(item1.LogicIndex, (i, b) =>
                {
                    if (b)
                    {
                        flag = true;
                        item1.State = PackingBrakeCutState.Cut;
                    }
                    else
                    {
                        item1.State = PackingBrakeCutState.Normal;
                    }
                });
            }
            if (flag)
            {
                HasAnyNotConfirm = true;
                
            }
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            
        }

        protected virtual void OnPackingBrakeCutChangedEvent()
        {
            var handler = PackingBrakeCutChangedEvent;
            if (handler != null)
            {
                handler();
            }
        }
    }
}