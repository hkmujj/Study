using System;
using System.Windows.Forms;
using CommonUtil.Util;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using Urban.ATC.Siemens.WPF.Control;
using Urban.ATC.Siemens.WPF.Control.View;
using Urban.ATC.Siemens.WPF.Control.ViewModel;
using Timer = System.Threading.Timer;

namespace Urban.ATC.Siemens.View.View
{
    public partial class WpfForm : ProjectFormBase
    {
        private SiemensData m_Data;
        private Timer timer;
        private MainPage chid = null;
        public WpfForm()
        {
            InitializeComponent();
            chid = new MainPage();
            this.elementHost1.Child = chid;
            RegionManager.SetRegionManager(elementHost1.Child, ServiceLocator.Current.GetInstance<IRegionManager>());
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);
        }


        private const int ActiveMsg = USER_MSG + 888;
        private const int USER_MSG = 0x0400;
        protected override void WndProc(ref Message m)
        {
            //
            if (m.Msg == ActiveMsg && DataPackage.Config.SystemConfig.StartModel == StartModel.PTT)
            {
                elementHost1.Child = null;
                elementHost1.Child = chid;

                AppLog.Warn("Current_Activated" + DateTime.Now);
            }


            base.WndProc(ref m);
        }

        public SiemensData Data
        {
            get { return m_Data; }
            set
            {
                if (m_Data == value)
                {
                    return;
                }
                m_Data = value;
                chid.DataContext = value;
            }
        }

        public WpfForm(IAppConfig appconfig, IDataPackage dataPackage)
            : this()
        {
            AppName = appconfig.AppName;
            AppConfig = appconfig;
            DataPackage = dataPackage;
            BackColor = GDICommon.BacgroundColor;
            if (dataPackage.Config.SystemConfig.StartModel == StartModel.PTT)
            {
                timer1.Start();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            elementHost1.Child = chid;
        }
    }
}