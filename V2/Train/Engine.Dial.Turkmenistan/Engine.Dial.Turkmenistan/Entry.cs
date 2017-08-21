using System.Collections.Generic;
using System.IO;
using CommonUtil.Util;
using Engine.Dial.Turkmenistan.Config;
using Engine.Dial.Turkmenistan.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace Engine.Dial.Turkmenistan
{
    [SubsystemExport(typeof(Entry))]
    public class Entry : ISubsystem
    {
        public void Initalize(SubsystemInitParam initParam)
        {
            AppLog.Info("----------------------Engine.Dial.Angola is Start!-----------------------");

            //var config = new TurkmenistanDialConfig();
            //config.Dials = new List<DialConfig>();
            //config.Dials.Add(new DialConfig()
            //{
            //    Angle = -45,
            //    Bottom = 0,
            //    Height = 300,
            //    Width = 300,
            //    ImagePath = "",
            //    Left = 100,
            //    Right = 100,
            //    Top = 100,
            //});
            //config.Texts = new List<TextConfig>();
            //config.Texts.Add(new TextConfig()
            //{
            //    Bottom = 1,
            //    FontSize = 30,
            //    Left = 30,
            //    LongWith = 10,
            //    Right = 10,
            //    ShortWith = 10,
            //    Top = 10
            //});

            //DataSerialization.SerializeToXmlFile(config,Path.Combine(@"c:\mmi",TurkmenistanDialConfig.FileName));
           
            GlobalConfigParam.Instance.InitParam = initParam;
            GlobalConfigParam.Instance.IndexConfig =
                initParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                    new CommunicationDataKey(initParam.AppConfig));
            var model = ServiceLocator.Current.GetInstance<TurkmenistanViewModel>();
            var frm = new EntryForm(initParam);
            initParam.DataPackage.ServiceManager.GetService<IViewService>().Regist(frm.AppName, frm);
            initParam.RegistDataListener(model);
            AppLog.Info("----------------------Engine.Dial.Angola Suncceed-----------------------");
        }
    }
}