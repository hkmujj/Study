using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Reflection;
using System.Text;
using MMI.Facility.DataType;
using GT_MMI.Interface.Button;
using GT_MMI.Interface.State;

namespace GT_MMI.States
{
    class StateFlyWeight : IStateFlyWeight
    {
        private Dictionary<StateType, IState> m_StateDictionary;

        public IState this[StateType type]
        {
            get { return m_StateDictionary[type]; }
        }

        public void Initalize(baseClass viewClass, IButtonManager btnManager)
        {
            m_StateDictionary = new Dictionary<StateType, IState>();

            var typeDefines = Enum.GetValues(typeof (StateType)).Cast<StateType>().ToArray();

            var ass = Assembly.GetAssembly(typeof (InitializationState));

            var types = ass.GetTypes();

            foreach (var typeDefine in typeDefines)
            {
                var typeName = string.Format("{0}State", typeDefine);
                var type = types.FirstOrDefault(f => f.Name == typeName);
                if (type == null)
                {
                    throw new InstanceNotFoundException(string.Format("Can not found class {0} in {1}", typeName, ass.FullName));
                }

                var constructor = type.GetConstructor(new Type[2] { typeof (baseClass), typeof (IButtonManager) });

                if (constructor != null)
                {
                    m_StateDictionary.Add(typeDefine,(IState) constructor.Invoke(new object[]{viewClass, btnManager}));
                }
                else
                {
                    throw new Exception(string.Format("Can not foud constructor of ({0},{1})", typeof (baseClass), typeof (IButtonManager)));
                }
            }
        }
    }
}
