using System.Collections.Generic;
using System.Collections.ObjectModel;
using MMI.Facility.Interface;

namespace Motor.HMI.CRH5G.底层共用
{
    public class ObjectManager
    {
        public ReadOnlyCollection<baseClass> AllObject { get; private set; }

        private readonly List<baseClass> m_MllObject;

        public static ObjectManager Instance { private set; get; }

        static ObjectManager()
        {
            Instance = new ObjectManager();
        }

        public ObjectManager()
        {
            m_MllObject = new List<baseClass>();
            AllObject = new ReadOnlyCollection<baseClass>(m_MllObject);
        }

        public void Regist(baseClass obj)
        {
            m_MllObject.Add(obj);
        }
    }
}