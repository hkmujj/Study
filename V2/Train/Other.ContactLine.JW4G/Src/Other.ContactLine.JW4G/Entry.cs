using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Other.ContactLine.JW4G.Model;
using Other.ContactLine.JW4G.Views.Shells;

namespace Other.ContactLine.JW4G
{
    [SubsystemExport(typeof(Entry))]
    public class Entry : ISubsystem
    {
        /// <summary/>
        /// <param name="initParam"/>
        public void Initalize(SubsystemInitParam initParam)
        {
            //初始化全局变量
            GlobalParam.Instance.Initalize(initParam);
            //获取数据转换类
            var data = ServiceLocator.Current.GetInstance<DataAdapter.DataAdapter>();
            //初始化承载WPF控件的窗体
            var frm = new ContactLineForm();
            //获取视图服务
            var viewServer = initParam.DataPackage.ServiceManager.GetService<IViewService>();
            //注册视图服务窗口
            viewServer.Regist(initParam.AppConfig.AppName, frm);
            //激活窗口
            viewServer.Active(initParam.AppConfig.AppName);
            //初始化数据转换类
            data.Initalize();
        }
    }
}