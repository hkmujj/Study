using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using CommonUtil.Util;
using DevExpress.XtraPrinting.Native;
using LightRail.HMI.GZYGDC.Model;
using LightRail.HMI.GZYGDC.Model.BtnStragy;
using LightRail.HMI.GZYGDC.Model.ConfigModel;
using Microsoft.Practices.ServiceLocation;

namespace LightRail.HMI.GZYGDC.Controller.BtnStragy
{
    [Export(typeof(IStateInterfaceFactory))]
    public class StateInterfaceFactory : IStateInterfaceFactory
    {
        private readonly Dictionary<StateInterfaceKey, IStateInterface> m_StateInterfacedDictionary;

        public StateInterfaceFactory()
        {
            m_StateInterfacedDictionary = new Dictionary<StateInterfaceKey, IStateInterface>();
        }

        public IStateInterface GetOrCreate(StateInterfaceKey interfaceKey, List<StateInterfaceItem> StateInterfaceCollection)
        {
            if (!m_StateInterfacedDictionary.ContainsKey(interfaceKey))
            {
                CreateStateInterface(interfaceKey, StateInterfaceCollection);
            }

            return m_StateInterfacedDictionary[interfaceKey];
        }

        private void CreateStateInterface(StateInterfaceKey interfaceKey, List<StateInterfaceItem> StateInterfaceCollection)
        {
            var itConfig =
                StateInterfaceCollection.FirstOrDefault(f => f.Key == interfaceKey.ToString());
            if (itConfig != null)
            {
                var state = new StateInterface
                {
                    Id = interfaceKey,
                    Title = itConfig.Title,
                    ContentViewName = string.Format("LightRail.HMI.GZYGDC.View.{0}", itConfig.ContentViewName),
                    ContentViewTitle = itConfig.ContentViewTitle,

                    BtnB1 = CreateBtnItem(itConfig.B1Content, itConfig.B1ActionClassName),
                    BtnB2 = CreateBtnItem(itConfig.B2Content, itConfig.B2ActionClassName),
                    BtnB3 = CreateBtnItem(itConfig.B3Content, itConfig.B3ActionClassName),
                    BtnB4 = CreateBtnItem(itConfig.B4Content, itConfig.B4ActionClassName),
                    BtnB5 = CreateBtnItem(itConfig.B5Content, itConfig.B5ActionClassName),
                    BtnB6 = CreateBtnItem(itConfig.B6Content, itConfig.B6ActionClassName),
                    BtnB7 = CreateBtnItem(itConfig.B7Content, itConfig.B7ActionClassName),
                    BtnB8 = CreateBtnItem(itConfig.B8Content, itConfig.B8ActionClassName),
                    BtnB9 = CreateBtnItem(itConfig.B9Content, itConfig.B9ActionClassName),
                    BtnB10 = CreateBtnItem(itConfig.B10Content, itConfig.B10ActionClassName),
                    BtnB11 = CreateBtnItem(itConfig.B1Content, itConfig.B11ActionClassName),
                    BtnB12 = CreateBtnItem(itConfig.B2Content, itConfig.B12ActionClassName),
                    BtnB13 = CreateBtnItem(itConfig.B3Content, itConfig.B13ActionClassName),
                    BtnB14 = CreateBtnItem(itConfig.B4Content, itConfig.B14ActionClassName),
                    BtnB15 = CreateBtnItem(itConfig.B5Content, itConfig.B15ActionClassName),
                    BtnB16 = CreateBtnItem(itConfig.B6Content, itConfig.B16ActionClassName),
                    BtnB17 = CreateBtnItem(itConfig.B7Content, itConfig.B17ActionClassName),
                    BtnB18 = CreateBtnItem(itConfig.B8Content, itConfig.B18ActionClassName),
                    BtnB19 = CreateBtnItem(itConfig.B9Content, itConfig.B19ActionClassName),
                    BtnB20 = CreateBtnItem(itConfig.B10Content, itConfig.B20ActionClassName),
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
            var actionClassFullName = string.Format("LightRail.HMI.GZYGDC.Controller.BtnActionResponser.{0}",
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