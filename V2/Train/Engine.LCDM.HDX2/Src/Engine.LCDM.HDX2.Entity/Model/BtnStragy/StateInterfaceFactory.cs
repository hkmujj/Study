using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Engine.LCDM.HDX2.Entity.Model.BtnStragy
{
    [Export( typeof(IStateInterfaceFactory))]
    public class StateInterfaceFactory : IStateInterfaceFactory, IPartImportsSatisfiedNotification
    {
        [ImportMany]
        private List<IStateInterface> m_StateInterfaces;

        private Dictionary<StateInterfaceKey, IStateInterface> m_StateInterfacedDictionary;

        public IStateInterface GetOrCreate(StateInterfaceKey interfaceKey)
        {
            if (m_StateInterfacedDictionary.ContainsKey(interfaceKey))
            {
                return m_StateInterfacedDictionary[interfaceKey];
            }
            return null;
        }

        /// <summary>
        /// 在满足部件的导入并可安全使用时调用。
        /// </summary>
        public void OnImportsSatisfied()
        {
            var cp = new StateInterfaceKeyEqualityComparer();
            m_StateInterfacedDictionary = m_StateInterfaces.ToDictionary(kvp => kvp.Id, kvp => kvp, cp);
        }
    }
}