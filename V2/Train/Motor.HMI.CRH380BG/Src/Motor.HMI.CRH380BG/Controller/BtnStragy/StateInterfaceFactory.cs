using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using CommonUtil.Util;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.Model.BtnStragy;
using Microsoft.Practices.ServiceLocation;

namespace Motor.HMI.CRH380BG.Controller.BtnStragy
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
                    ContentViewName = string.Format("Motor.HMI.CRH380BG.View.Contents.{0}", itConfig.ContentViewName),
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
                    BtnOK = CreateBtnItem("", itConfig.BOKActionClassName),
                    BtnDown = CreateBtnItem("", itConfig.BDownActionClassName),
                    BtnUp = CreateBtnItem("", itConfig.BUpActionClassName),
                    BtnRight = CreateBtnItem("", itConfig.BRightActionClassName),
                    BtnLeft = CreateBtnItem("", itConfig.BLeftActionClassName),
                    BtnBOK = CreateBtnItem("", itConfig.BOKActionClassName),
                    BtnBReturn = CreateBtnItem("", itConfig.BReturnActionClassName),
                    BtnBSwitchDisplay = CreateBtnItem("", itConfig.BSwitchDisplayActionClassName),
                    BtnBTrainStopCheck = CreateBtnItem("", itConfig.BTrainStopCheckActionClassName),
                    BtnBTrainRunningCheck = CreateBtnItem("", itConfig.BTrainRunningCheckActionClassName),
                    BtnBFaultInfo = CreateBtnItem("", itConfig.BFaultInfoActionClassName),
                    BtnBIofoBox = CreateBtnItem("", itConfig.BInfoBoxActionClassName),
                    BtnBLanguageSelect = CreateBtnItem("", itConfig.BLanguageSelectActionClassName),
                    BtnBDayandnightSwitch = CreateBtnItem("", itConfig.BDayandnightSwitchActionClassName),
                    BtnBSwitch = CreateBtnItem("", itConfig.BSwitchActionClassName),
                    BtnBLight = CreateBtnItem("", itConfig.BLightActionClassName),

                };
                m_StateInterfacedDictionary.Add(interfaceKey, state);
            }
            else
            {
                AppLog.Error("Can not found config where key = {0}", interfaceKey.ToString());
            }
        }

        private static BtnItem CreateBtnItem(string content, string actionClassName, bool isSelected = false)
        {
            var actionClassFullName = string.Format("Motor.HMI.CRH380BG.Controller.BtnActionResponser.{0}",
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
            return new BtnItem(content, isSelected, responser);
        }
    }
}