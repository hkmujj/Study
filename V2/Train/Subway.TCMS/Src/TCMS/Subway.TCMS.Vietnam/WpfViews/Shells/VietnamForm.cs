using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using MMI.Facility.Interface.Project;
using Subway.TCMS.Vietnam.Model;


namespace Subway.TCMS.Vietnam.WpfViews.Shells
{
    public partial class VietnamForm : ProjectFormBase
    {
        public VietnamForm()
        {
            InitializeComponent();
            Initliza();
        }

        private void Initliza()
        {
            if (!DesignMode)
            {
                AppConfig = GlobalParams.Instance.InitParam.AppConfig;
                AppName = AppConfig.AppName;
                DataPackage = GlobalParams.Instance.InitParam.DataPackage;
                this.Text = GlobalParams.Instance.InitParam.AppConfig.AppName;
                //Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                //{
                //    Source =
                //        new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}",
                //            Path.GetFileNameWithoutExtension(GetType().Assembly.Location), "CascoResource.xaml")),
                //});
            }

            Load += OnLoad;


            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);
        }

        private void OnLoad(object sender, EventArgs e)
        {

        }
    }
}
