using System.Windows.Forms;
using Engine._6A.Adapter;
using Engine._6A.Views.Common;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Project;

namespace Engine._6A.Views.Shell
{
    public partial class ShellForm : ProjectFormBase
    {
        private readonly SubsystemInitParam m_InitParam;

        protected ShellForm()
        {
            InitializeComponent();
        }

        public ShellForm(SubsystemInitParam initParam)
            : base(initParam.AppConfig.AppName, initParam.DataPackage)
        {
            m_InitParam = initParam;
            InitializeComponent();
            var domain = new DoMain();
            elementHost1.Child = domain;
            RegionManager.SetRegionManager(this.elementHost1.Child, ServiceLocator.Current.GetInstance<IRegionManager>());
            this.FormBorderStyle = FormBorderStyle.None;
        }

        public void Init()
        {
            var child = elementHost1.Child as DoMain;
            if (child != null)
            {
                child.DataContext = ServiceLocator.Current.GetInstance<IEngineAdapter>().Model;
            }
        }

    }
}
