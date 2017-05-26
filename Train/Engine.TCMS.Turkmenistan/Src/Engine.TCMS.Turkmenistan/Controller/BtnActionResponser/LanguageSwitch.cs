using System;
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
            var str = string.Format("pack://application:,,,/{0};component/Resources/{1}",
                Path.GetFileNameWithoutExtension(GetType().Assembly.Location), GlobalParam.Instance.IsTurkmenistan ? "Text/StringResource-tm.xaml" : "Text/StringResource-ch.xaml");
            var resourcedic = new ResourceDictionary
            {

                Source = new Uri(
                    str,
                    UriKind.Absolute)
            };
            Application.Current.Resources.MergedDictionaries[0] = resourcedic;


        }
    }
}
