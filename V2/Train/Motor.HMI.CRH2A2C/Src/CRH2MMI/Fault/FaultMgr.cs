using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CommonUtil.Util;
using CRH2MMI.Common.Global;

namespace CRH2MMI.Fault
{
    partial class FaultMgr : IList<FaultItemInfo>
    {
        protected EventHandler<FaultChangedArgs> FaultChangedEvent;

        public static FaultMgr Instance { private set; get; }
        //tandw 2015/05/28 update 修改点进故障一览页面没有值 Start
        public List<FaultItemInfo> AllFaultItemInfos
        {
            get
            {
                return m_AllFaultItemInfos;
            }
            set
            {
                m_AllFaultItemInfos = value;
            }
        }
        //tandw 2015/05/28 update  end
        private  List<FaultItemInfo> m_AllFaultItemInfos;

        /// <summary>
        /// 还处于激活状态的故障
        /// </summary>
        private readonly List<FaultItemInfo> m_ActiveFaultItemInfos;

        /// <summary>
        /// 已被浏览过的
        /// </summary>
        private readonly List<FaultItemInfo> m_HasViewdFaultItemInfos;

        /// <summary>
        /// 配置中的故障信息, 
        /// key : 故障逻辑位
        /// value : 故障信息
        /// </summary>
        public Dictionary<int, FaultNameInfo> FaultNameInfoDic { private set; get; }

        private bool m_HasInit;

        private readonly List<IFaultGetter> m_FaultGetterList;

        /// <summary>
        /// 当前显示的故障
        /// </summary>
        public FaultItemInfo CurrentShowFaultItemInfo { get; private set; }

        public FaultGetterFacotry GetterFacotry { private set; get; }

        static FaultMgr() { Instance = new FaultMgr(); }

        private FaultMgr()
        {
            m_AllFaultItemInfos = new List<FaultItemInfo>();
            m_ActiveFaultItemInfos = new List<FaultItemInfo>();
            m_HasViewdFaultItemInfos = new List<FaultItemInfo>();
            FaultNameInfoDic = new Dictionary<int, FaultNameInfo>();
            m_FaultGetterList = new List<IFaultGetter>();
            GetterFacotry = new FaultGetterFacotry(this);
            m_HasInit = false;
        }

        public void Init()
        {
            if (m_HasInit)
            {
                return;
            }
            m_HasInit = true;

            GlobalEvent.Instance.RestartEvent += (sender, args) => Clear();

            var infos = GlobalResource.Instance.IFaultInfoFacade.GetFaultNameInfos();

            FaultNameInfoDic = infos.ToDictionary(k => k.FaultLogicIndex, v => v);

        }

        /// <summary>
        ///  是否有活动的故障
        /// </summary>
        /// <returns></returns>
        public bool AnyActiveFault()
        {
            return m_ActiveFaultItemInfos.Any();
        }

        /// <summary>
        /// 取消故障的激活状态
        /// </summary>
        /// <param name="faultItem"></param>
        protected bool DeactiveFault(FaultItemInfo faultItem)
        {
            faultItem.Activie = false;
            return m_ActiveFaultItemInfos.Remove(faultItem);
            //// 强制删除已不激活的
            //m_HasViewdFaultItemInfos.Remove(faultItem);
        }

        #region IList 接口

        public IEnumerator<FaultItemInfo> GetEnumerator()
        {
            return m_AllFaultItemInfos.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_AllFaultItemInfos.GetEnumerator();
        }

        public void Add(FaultItemInfo item)
        {
            AllFaultItemInfos.Add(item);
            m_ActiveFaultItemInfos.Add(item);
            m_HasViewdFaultItemInfos.Add(item);
           //var cnt = m_AllFaultItemInfos.Count;
           // if (cnt == 1)
           // {
           //     m_FaultGetterList.ForEach(e => e.ResetCurrentShowFaultIndex());
           // }

            HandleUtil.OnHandle(FaultChangedEvent,
                this,
                new FaultChangedArgs(FaultChangedType.Add, new List<FaultItemInfo>() { item }.AsReadOnly()));

        }

        public void Clear()
        {
            HandleUtil.OnHandle(this.FaultChangedEvent, this, new FaultChangedArgs(FaultChangedType.Clear, m_AllFaultItemInfos.AsReadOnly()));
            AllFaultItemInfos.Clear();
            m_ActiveFaultItemInfos.Clear();
            m_HasViewdFaultItemInfos.Clear();
        }

        public bool Contains(FaultItemInfo item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(FaultItemInfo[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(FaultItemInfo item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return m_AllFaultItemInfos.Count; }
        }

        public bool IsReadOnly { get; private set; }

        public int IndexOf(FaultItemInfo item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, FaultItemInfo item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public FaultItemInfo this[int index]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        #endregion

    }
}
