using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using Engine.LCDM.HDX2.Entity.View;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;

namespace Engine.LCDM.HDX2.Entity
{
    public partial class HXD2Form : ProjectFormBase
    {
        protected HXD2Form()
        {
            InitializeComponent();
        }

        public HXD2Form(SubsystemInitParam initParam)
            : base(initParam.AppConfig.AppName, initParam.DataPackage)
        {
            InitializeComponent();

            RegionManager.SetRegionManager(elementHost1.HostContainer, ServiceLocator.Current.GetInstance<IRegionManager>());
            
            this.Text = initParam.AppConfig.AppName;

            UIElement lcdm = null;
            if (initParam.AppConfig.ActureFormConfig.IsOutterFrameVisible)
            {
                lcdm = ServiceLocator.Current.GetInstance<LCDMShellWithButtonView>();
            }
            else
            {
                lcdm = ServiceLocator.Current.GetInstance<LCDMShellView>();
            }
            elementHost1.Child = lcdm;
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);
        }
    }
}
