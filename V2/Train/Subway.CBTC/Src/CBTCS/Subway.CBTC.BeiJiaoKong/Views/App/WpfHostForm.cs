using System;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;
using Subway.CBTC.BeiJiaoKong.Views.Shell;
using Subway.CBTC.BeiJiaoKong.ViewModel;

namespace Subway.CBTC.BeiJiaoKong.Views.App
{
    /// <summary>
    /// 创 建 者：谭栋炜
    /// 创建时间：2017/1/16
    /// 修 改 者：谭栋炜
    /// 修改时间：2017/1/16
    /// </summary>
    public partial class WpfHostForm : ProjectFormBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WpfHostForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="param">子系统参数</param>
        public WpfHostForm(SubsystemInitParam param)
            : this()
        {

            if (!DesignMode)
            {
                AppConfig = param.AppConfig;
                DataPackage = param.DataPackage;
                AppName = AppConfig.AppName;
                Text = AppName;

                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/Subway.CBTC.BeiJiaoKong;component/Resources/BeiJiaoKongResources.xaml")
                });
            }
            var main =
            new DoMainView();

            RegionManager.SetRegionContext(main, ServiceLocator.Current.GetInstance<IRegionManager>());
            RegionManager.SetRegionManager(main, ServiceLocator.Current.GetInstance<IRegionManager>());



            main.DataContext = ServiceLocator.Current.GetInstance<BeiJiaoKongViewModel>();

            elementHost1.Child = main;
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);
        }
        /// <returns>
        /// 与该控件关联的文本。
        /// </returns>
        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
    }
}
