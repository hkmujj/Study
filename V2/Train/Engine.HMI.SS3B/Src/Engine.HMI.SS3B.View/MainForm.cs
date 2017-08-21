using System;
using System.Windows.Controls;
using Engine.HMI.SS3B.View.Config;
using Engine.HMI.SS3B.View.ViewModel;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;

namespace Engine.HMI.SS3B.View
{
    public partial class MainForm : ProjectFormBase
    {

        public MainForm()
        {
            InitializeComponent();
        }
        public MainForm(IAppConfig appconfig, IDataPackage dataPackage, ISS3BViewModel model)
            : this()
        {
            AppName = appconfig.AppName;
            AppConfig = appconfig;
            DataPackage = dataPackage;
            UserControl col = null;
            switch (GlobalParam.Instance.SS3BConfig.Type)
            {
                case SS3BType.LiuZhou:
                    col = ServiceLocator.Current.GetInstance<View.LiuZhou.DoMain>();
                    break;
                case SS3BType.KunMing:
                    col = ServiceLocator.Current.GetInstance<View.KunMing.DoMain>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            col.DataContext = model;
            elementHost1.Child = col;
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);
            RegionManager.SetRegionManager(elementHost1.HostContainer, ServiceLocator.Current.GetInstance<IRegionManager>());


        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
