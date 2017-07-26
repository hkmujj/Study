using System.Collections.Generic;
using CommonUtil.Util;
using MMI.Facility.Interface;
using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model
{
    public class ATPRepository
    {
        internal static ATPRepository Instance { private set; get; }

        private readonly Dictionary<ScreenIdentity, ATPDomain> m_RepositoryDictionary;

        static ATPRepository()
        {
            Instance = new ATPRepository();
        }

        public ATPRepository()
        {
            m_RepositoryDictionary = new Dictionary<ScreenIdentity, ATPDomain>(new ScreenIdentityCompare());
        }

        public static ATPDomain GetATP(baseClass baseClass)
        {
            var id = ScreenIdentityHelper.CreateIdentity(baseClass);

            return GetATP(id);
        }

        public static ATPDomain GetATP(ScreenIdentity identity)
        {
            if (Instance.m_RepositoryDictionary.ContainsKey(identity))
            {
                return Instance.m_RepositoryDictionary[identity];
            }
            return null;
        }

        public void Regist(ATPDomain atpDomain)
        {
            if (m_RepositoryDictionary.ContainsKey(atpDomain.Identity))
            {
                AppLog.Warn(string.Format("ATPRepository has regited atp which Identity is {0}", atpDomain.Identity));
                return;
            }
            m_RepositoryDictionary.Add(atpDomain.Identity, atpDomain);
        }
    }
}