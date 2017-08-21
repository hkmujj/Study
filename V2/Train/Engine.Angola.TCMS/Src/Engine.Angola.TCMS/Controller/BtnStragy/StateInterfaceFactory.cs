using CommonUtil.Util;
using Engine.Angola.TCMS.Model;
using Engine.Angola.TCMS.Model.BtnStragy;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using Engine.Angola.TCMS.Controller.BtnActionResponser;
using Engine.Angola.TCMS.View.Contents;
using Microsoft.Practices.ServiceLocation;

namespace Engine.Angola.TCMS.Controller.BtnStragy
{
    [Export(typeof(IStateInterfaceFactory))]
    public class StateInterfaceFactory : IStateInterfaceFactory
    {
        private readonly Dictionary<StateInterfaceKey, IStateInterface> m_StateInterfacedDictionary;

        public StateInterfaceFactory()
        {
            m_StateInterfacedDictionary = new Dictionary<StateInterfaceKey, IStateInterface>();
        }

        public IStateInterface GetOrCreate(StateInterfaceKey interfaceKey)
        {
            if (!m_StateInterfacedDictionary.ContainsKey(interfaceKey))
            {
                CreateStateInterface(interfaceKey);
            }

            return m_StateInterfacedDictionary[interfaceKey];
        }

        private void CreateStateInterface(StateInterfaceKey interfaceKey)
        {
            var rootViewNameSpace = typeof(RootContentView).Namespace;

            var itConfig =
                GlobalParam.Instance.StateInterfaceCollection.Value.FirstOrDefault(f => f.Key == interfaceKey.ToString());
            if (itConfig != null)
            {
                var state = new StateInterface
                {
                    Id = interfaceKey,
                    Title = itConfig.Title,
                    ContentViewName = string.Format("{0}.{1}",rootViewNameSpace, itConfig.ContentViewName),
                    BtnF1 = CreateBtnItem(itConfig.F1Content, itConfig.F1ActionClassName),
                    BtnF2 = CreateBtnItem(itConfig.F2Content, itConfig.F2ActionClassName),
                    BtnF3 = CreateBtnItem(itConfig.F3Content, itConfig.F3ActionClassName),
                    BtnF4 = CreateBtnItem(itConfig.F4Content, itConfig.F4ActionClassName),
                    BtnF5 = CreateBtnItem(itConfig.F5Content, itConfig.F5ActionClassName),
                };
                m_StateInterfacedDictionary.Add(interfaceKey, state);
            }
            else
            {
                AppLog.Error("Can not found config where key = {0}", interfaceKey.ToString());
            }
        }

        private static BtnItem CreateBtnItem(string content, string actionClassName)
        {
            var actionClassFullName = string.Format("{0}.{1}",typeof(BtnActionResponserBase).Namespace, actionClassName);

            IBtnActionResponser responser = null;
            if (!string.IsNullOrWhiteSpace(actionClassName))
            {
                try
                {
                    var type = Assembly.GetAssembly(typeof(StateInterfaceFactory))
                        .GetType(actionClassFullName, false, false);
                    responser = ServiceLocator.Current.GetInstance(type) as IBtnActionResponser;
                }
                catch (Exception e)
                {
                    AppLog.Error("can not found action response where name = {0}, {1}", actionClassFullName, e);
                }
            }
            return new BtnItem(content, responser);
        }
    }
    
}
