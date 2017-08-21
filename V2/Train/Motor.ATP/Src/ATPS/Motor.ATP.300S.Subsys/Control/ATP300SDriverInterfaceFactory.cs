using System;
using System.IO;
using System.Linq;
using System.Reflection;
using CommonUtil.Util;
using Microsoft.Practices.ServiceLocation;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser;
using Motor.ATP._300S.Subsys.Control.UserAction.StateProvider;

namespace Motor.ATP._300S.Subsys.Control
{
    public class ATP300SDriverInterfaceFactory : DriverInterfaceFactory
    {
        public ATP300SDriverInterfaceFactory(IATP atp)
            : base(atp)
        {
            ActionResponserNamespacePrefix = typeof (F1CTCS3ActionResponser).Namespace;
            StateProviderNamespacePrefix = typeof (F1DataDriverIDStateProvider).Namespace;
            m_ConfigFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\ATP300S F 区结构定义.xls");
            var serachAssemblies = new Assembly[] { GetType().Assembly, typeof(NormalStateProvider).Assembly };
            m_SerachTypes = serachAssemblies.SelectMany(s => s.GetTypes()).OrderBy(o => o.FullName).ToArray();
        }

        protected override IDriverPopupView GetOrCreatePopupView(Type popViewType)
        {
            try
            {
                return ServiceLocator.Current.GetInstance(popViewType, popViewType.FullName) as IDriverPopupView;
            }
            catch (Exception e)
            {
                AppLog.Error("Can not get instance from ServiceLocator, where type={0}, key={1}, {2}", popViewType, popViewType.FullName, e);
            }
            return null;
        }
    }
}