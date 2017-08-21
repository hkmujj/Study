using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using CommonUtil.Util;
using Engine.TAX2.SS7C.Model;
using Engine.TAX2.SS7C.Model.BtnStragy;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TAX2.SS7C.Controller.BtnStragy
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
                    ContentViewName = string.Format("Engine.TAX2.SS7C.View.Contents.{0}", itConfig.ContentViewName),
                    ContentViewLocation = itConfig.ContentViewLocation,
                    BtnB1 = CreateBtnItem(itConfig.B1Content, itConfig.B1ActionClassName,itConfig.SelectedBtnNames.Contains("B1")),
                    BtnB2 = CreateBtnItem(itConfig.B2Content, itConfig.B2ActionClassName,itConfig.SelectedBtnNames.Contains("B2")),
                    BtnB3 = CreateBtnItem(itConfig.B3Content, itConfig.B3ActionClassName,itConfig.SelectedBtnNames.Contains("B3")),
                    BtnB4 = CreateBtnItem(itConfig.B4Content, itConfig.B4ActionClassName,itConfig.SelectedBtnNames.Contains("B4")),
                    BtnB5 = CreateBtnItem(itConfig.B5Content, itConfig.B5ActionClassName,itConfig.SelectedBtnNames.Contains("B5")),
                    BtnB6 = CreateBtnItem(itConfig.B6Content, itConfig.B6ActionClassName,itConfig.SelectedBtnNames.Contains("B6")),
                    BtnB7 = CreateBtnItem(itConfig.B7Content, itConfig.B7ActionClassName,itConfig.SelectedBtnNames.Contains("B7")),
                    BtnB8 = CreateBtnItem(itConfig.B8Content, itConfig.B8ActionClassName,itConfig.SelectedBtnNames.Contains("B8")),
                    BtnB9 = CreateBtnItem(itConfig.B9Content, itConfig.B9ActionClassName, itConfig.SelectedBtnNames.Contains("B9")),
                    BtnB10 = CreateBtnItem(itConfig.B10Content, itConfig.B10ActionClassName, itConfig.SelectedBtnNames.Contains("B0")),
                    BtnQuery = CreateBtnItem(itConfig.BQueryContent, itConfig.BQueryActionClassName),
                    BtnSaveAs = CreateBtnItem(itConfig.BSaveAsContent, itConfig.BSaveAsActionClassName),
                    BtnRelase = CreateBtnItem(itConfig.BRelaseContent, itConfig.BRelaseActionClassName),
                    BtnOk = CreateBtnItem(itConfig.BOKContent, itConfig.BOKActionClassName),
                    BtnSetting = CreateBtnItem(itConfig.BSettingContent, itConfig.BSettingActionClassName),
                    BtnRight = CreateBtnItem(itConfig.BRightContent, itConfig.BRightActionClassName),
                    BtnLeft = CreateBtnItem(itConfig.BLeftContent, itConfig.BLeftActionClassName),
                    BtnDown = CreateBtnItem(itConfig.BDownContent, itConfig.BDownActionClassName),
                    BtnUp= CreateBtnItem(itConfig.BUpContent, itConfig.BUpActionClassName),
                    BtnUnlock= CreateBtnItem(itConfig.BUnlockContent, itConfig.BUnlockActionClassName),
                    BtnVigilant= CreateBtnItem(itConfig.BAlertContent, itConfig.BAlertActionClassName),
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
            var actionClassFullName = string.Format("Engine.TAX2.SS7C.Controller.BtnActionResponser.{0}",
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
            return new BtnItem(content, responser, isSelected);
        }
    }
}