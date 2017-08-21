using System;
using System.Collections.Generic;

namespace Engine.LCDM.HXD3.Config
{
    public class ViewSetInterpreter
    {
        readonly Lazy<List<InitialSet>> m_InitSets;

        public ViewSetInterpreter(Lazy<List<InitialSet>> initSets)
        {
            m_InitSets = initSets;

            CanPressure500 = new Lazy<bool>(() =>
            {
                var cn = m_InitSets.Value.Find(f => f.Name == "是否能定压500");
                if (cn != null && cn.Content == "否")
                {
                    return false;
                }
                return true;
            });
        }

        /// <summary>
        /// 是否能定压500
        /// </summary>
        public Lazy<bool> CanPressure500 { get; private set; }
    }
}