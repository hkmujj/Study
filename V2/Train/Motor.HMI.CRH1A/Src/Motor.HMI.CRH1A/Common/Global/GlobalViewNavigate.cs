using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motor.HMI.CRH1A.Common.Config;

namespace Motor.HMI.CRH1A.Common.Global
{
    /// <summary>
    /// 界面导航 
    /// </summary>
    class GlobalViewNavigate
    {
        /// <summary>
        /// 导航到界面
        /// </summary>
        public event Action<ViewConfig> NavigeteTo;

        /// <summary>
        /// 从哪个界面导航到
        /// </summary>
        public event Action<ViewConfig> NavigeteFrom;

        public static GlobalViewNavigate Instance { private set; get; }

        static GlobalViewNavigate()
        {
            Instance = new GlobalViewNavigate();
        }

        internal void OnNavigeteTo(ViewConfig obj)
        {
            Action<ViewConfig> handler = NavigeteTo;
            if (handler != null)
            {
                handler(obj);
            }
        }

        internal void OnNavigeteFrom(ViewConfig obj)
        {
            Action<ViewConfig> handler = NavigeteFrom;
            if (handler != null)
            {
                handler(obj);
            }
        }
    }
}
