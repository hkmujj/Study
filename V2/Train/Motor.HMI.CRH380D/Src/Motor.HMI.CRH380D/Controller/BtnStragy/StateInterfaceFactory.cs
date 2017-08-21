using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using CommonUtil.Util;
using Microsoft.Practices.ServiceLocation;
using Motor.HMI.CRH380D.Models.BtnStragy;
using Motor.HMI.CRH380D.Models.ConfigModel;

namespace Motor.HMI.CRH380D.Controller.BtnStragy
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
                    ContentViewName = string.Format("Motor.HMI.CRH380D.View.{0}", itConfig.ContentViewName),
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
                    BtnB11 = CreateBtnItem(itConfig.B11Content, itConfig.B11ActionClassName),
                    BtnB12 = CreateBtnItem(itConfig.B12Content, itConfig.B12ActionClassName),
                    BtnB13 = CreateBtnItem(itConfig.B13Content, itConfig.B13ActionClassName),
                    BtnB14 = CreateBtnItem(itConfig.B14Content, itConfig.B14ActionClassName),
                    BtnB15 = CreateBtnItem(itConfig.B15Content, itConfig.B15ActionClassName),
                    BtnB16 = CreateBtnItem(itConfig.B16Content, itConfig.B16ActionClassName),
                    BtnB17 = CreateBtnItem(itConfig.B17Content, itConfig.B17ActionClassName),
                    BtnB18 = CreateBtnItem(itConfig.B18Content, itConfig.B18ActionClassName),
                    BtnB19 = CreateBtnItem(itConfig.B19Content, itConfig.B19ActionClassName),
                    BtnB20 = CreateBtnItem(itConfig.B20Content, itConfig.B20ActionClassName),
                    BtnB21 = CreateBtnItem(itConfig.B21Content, itConfig.B21ActionClassName),
                    BtnB22 = CreateBtnItem(itConfig.B22Content, itConfig.B22ActionClassName),
                    BtnB23 = CreateBtnItem(itConfig.B23Content, itConfig.B23ActionClassName),
                    BtnB24 = CreateBtnItem(itConfig.B24Content, itConfig.B24ActionClassName),
                    BtnB25 = CreateBtnItem(itConfig.B25Content, itConfig.B25ActionClassName),
                    BtnB26 = CreateBtnItem(itConfig.B26Content, itConfig.B26ActionClassName),
                    BtnB27 = CreateBtnItem(itConfig.B27Content, itConfig.B27ActionClassName),
                    BtnB28 = CreateBtnItem(itConfig.B28Content, itConfig.B28ActionClassName),
                    BtnB29 = CreateBtnItem(itConfig.B29Content, itConfig.B29ActionClassName),
                    BtnB30 = CreateBtnItem(itConfig.B30Content, itConfig.B30ActionClassName),
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
            var actionClassFullName = string.Format("Motor.HMI.CRH380D.Controller.BtnActionResponser.{0}",
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