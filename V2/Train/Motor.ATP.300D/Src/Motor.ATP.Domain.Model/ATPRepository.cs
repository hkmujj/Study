using System.Collections.Generic;
using CommonUtil.Util;
using MMI.Facility.Interface;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model
{
    public class ATPRepository
    {
        internal static ATPRepository Instance { private set; get; }

        private readonly Dictionary<ScreenIdentity, ATPDomainBase> m_RepositoryDictionary;

        static ATPRepository()
        {
            Instance = new ATPRepository();
        }

        public ATPRepository()
        {
            m_RepositoryDictionary = new Dictionary<ScreenIdentity, ATPDomainBase>(new ScreenIdentityCompare());
        }

        public static ATPDomainBase GetATP(baseClass baseClass)
        {
            var id = ScreenIdentityHelper.CreateIdentity(baseClass);
            if (Instance.m_RepositoryDictionary.ContainsKey(id))
            {
                return Instance.m_RepositoryDictionary[id];
            }
            return null;
        }

        public void Regist(ATPDomainBase atpDomain)
        {
            if (m_RepositoryDictionary.ContainsKey(atpDomain.Identity))
            {
                LogMgr.Warn(string.Format("ATPRepository has regited atp which Identity is {0}", atpDomain.Identity));
                return;
            }
            m_RepositoryDictionary.Add(atpDomain.Identity, atpDomain);
        }
    }
}