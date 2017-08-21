using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using CommonUtil.Util;
using Microsoft.Practices.ServiceLocation;
using Subway.TCMS.LanZhou.Model;
using Subway.TCMS.LanZhou.Model.BtnStragy;

namespace Subway.TCMS.LanZhou.Controller.BtnStragy
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
                GlobalParam.Instance.StateInterfaceCollection.FirstOrDefault(f => f.Key == interfaceKey.ToString());
            if (itConfig != null)
            {
                var state = new StateInterface
                {
                    Id = interfaceKey,
                    Title = itConfig.Title,
                    ContentViewName = string.Format("Subway.TCMS.LanZhou.View.Contents.{0}", itConfig.ContentViewName),
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

        private static BtnItem CreateBtnItem(string content, string actionClassName)
        {
            var actionClassFullName = string.Format("Subway.TCMS.LanZhou.Controller.BtnActionResponser.{0}ActionResponser",
                actionClassName);

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