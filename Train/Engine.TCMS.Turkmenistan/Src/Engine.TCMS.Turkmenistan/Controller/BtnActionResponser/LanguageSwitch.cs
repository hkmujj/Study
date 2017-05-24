﻿using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using Engine.TCMS.Turkmenistan.Model;
using Engine.TCMS.Turkmenistan.Model.BtnStragy;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.Controller.BtnActionResponser
{
    /// <summary>
    /// 
    /// </summary>

    [Export]
    public class LanguageSwitch : IBtnActionResponser
    {
        public BtnItem Parent { get; set; }
        public void UpdateState()
        {

        }

        public void ResponseClick()
        {
            GlobalParam.Instance.IsTurkmenistan = !GlobalParam.Instance.IsTurkmenistan;
            var model = ServiceLocator.Current.GetInstance<DomainModel>();
            model.CurrentStateInterface.UpdateState();
            var resourcedic = new ResourceDictionary
            {
                Source = new Uri(
                    string.Format("Resources/Text/StringResource-{0}.xaml", GlobalParam.Instance.IsTurkmenistan ? "tm" : "ch"),
                    UriKind.Relative)
            };
            Application.Current.Resources.MergedDictionaries[0] = resourcedic;
            //Application.Current.Resources.MergedDictionaries[0].MergedDictionaries.Insert(0,resourcedic);
            //Application.Current.Resources.MergedDictionaries[0].MergedDictionaries[0] = resourcedic;

            // resource.MergedDictionaries[0].MergedDictionaries[0].Source = new Uri(tmp);

        }
    }
}
