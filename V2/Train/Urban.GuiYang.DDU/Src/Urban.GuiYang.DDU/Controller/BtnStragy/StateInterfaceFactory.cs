using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using CommonUtil.Util;
using Microsoft.Practices.ServiceLocation;
using Urban.GuiYang.DDU.Controller.BtnStragy.UserAction.StateProvider;
using Urban.GuiYang.DDU.Model;
using Urban.GuiYang.DDU.Model.BtnStragy;

namespace Urban.GuiYang.DDU.Controller.BtnStragy
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
            var itConfig =
                GlobalParam.Instance.StateInterfaceCollection.Value.FirstOrDefault(f => f.Key == interfaceKey.ToString());
            if (itConfig != null)
            {
                var state = new StateInterface
                {
                    Id = interfaceKey,
                    Title = itConfig.Title,
                    ContentViewName =
                        string.Format(GlobalParam.Instance.DesignName + ".View.Contents.{0}", itConfig.ContentViewName),
                    BtnB1 = CreateBtnItem(itConfig.B1Content, itConfig.B1ActionClassName),
                    BtnB2 = CreateBtnItem(itConfig.B2Content, itConfig.B2ActionClassName),
                    BtnB3 = CreateBtnItem(itConfig.B3Content, itConfig.B3ActionClassName),
                    BtnB4 = CreateBtnItem(itConfig.B4Content, itConfig.B4ActionClassName),
                    BtnB5 = CreateBtnItem(itConfig.B5Content, itConfig.B5ActionClassName),
                    BtnB6 = CreateBtnItem(itConfig.B6Content, itConfig.B6ActionClassName),
                    BtnB7 = CreateBtnItem(itConfig.B7Content, itConfig.B7ActionClassName),
                    BtnB8 = CreateBtnItem(itConfig.B8Content, itConfig.B8ActionClassName),
                };
                m_StateInterfacedDictionary.Add(interfaceKey, state);
            }
            else
            {
                AppLog.Error("Can not found config where key = {0}", interfaceKey.ToString());
            }
        }

        private static BtnItem CreateBtnItem(string content, string actionName)
        {
            IBtnActionResponser responser = null;

            IBtnStateProvider stateProvider = null;


            if (!string.IsNullOrWhiteSpace(actionName))
            {
                var actionClassFullName =
                    string.Format(
                        GlobalParam.Instance.DesignName +
                        ".Controller.BtnStragy.UserAction.ActionResponser.{0}ActionResponser",
                        actionName);

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

                var stateName =
                    string.Format(
                        GlobalParam.Instance.DesignName +
                        ".Controller.BtnStragy.UserAction.StateProvider.{0}StateProvider",
                        actionName);
                try
                {
                    var type = Assembly.GetAssembly(typeof(StateInterfaceFactory))
                        .GetType(stateName, false, false);
                    stateProvider = ServiceLocator.Current.GetInstance(type) as IBtnStateProvider;
                }
                catch (Exception e)
                {
                    AppLog.Error("can not found state provider where name = {0}, {1}", stateName, e);
                }
            }
            else
            {
                stateProvider = ServiceLocator.Current.GetInstance<EmptyStateProvider>();
            }

            return new BtnItem(content, responser, stateProvider);
        }
    }
}