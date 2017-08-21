using System;
using System.Windows;
using MMI.Facility.Interface.Project;
using Other.ContactLine.JW4G.Model;

namespace Other.ContactLine.JW4G.Views.Shells
{
    public partial class ContactLineForm : ProjectFormBase
    {
        public ContactLineForm()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                AppConfig = GlobalParam.Instance.InitParam.AppConfig;
                AppName = AppConfig.AppName;
                DataPackage = GlobalParam.Instance.InitParam.DataPackage;
                Text = GlobalParam.Instance.InitParam.AppConfig.AppName;
               System.Windows.Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                   Source = new Uri("pack://application:,,,/Other.ContactLine.JW4G;component/Resources/ContactLineResource.xaml")
               });
            }
            this.elementHost1.Child = new DoMainSheel();
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);
        }

    }
}
