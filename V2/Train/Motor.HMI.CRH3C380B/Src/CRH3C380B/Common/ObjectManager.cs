using System.Collections.Generic;
using System.Collections.ObjectModel;
using MMI.Facility.Interface;


namespace Motor.HMI.CRH3C380B.Common
{
    public class ObjectManager
    {
        public ReadOnlyCollection<baseClass> AllObject { get; private set; }

        private readonly List<baseClass> m_AllObject;

        public static ObjectManager Instance { private set; get; }

        static ObjectManager()
        {
            Instance = new ObjectManager();
        }

        public ObjectManager()
        {
            m_AllObject = new List<baseClass>();
            AllObject = new ReadOnlyCollection<baseClass>(m_AllObject);
        }

        public void Regist(baseClass obj)
        {
            m_AllObject.Add(obj);
        }
    }
}