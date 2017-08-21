using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Controls;
using MMI.Facility.Interface.Project;
using System.IO;
using Engine.LCDM.HXD3.Resource;

namespace Engine.LCDM.HXD3.Views.Shells
{
    public partial class LCDM : ProjectFormBase
    {
        public LCDM()
        {
            InitializeComponent();
        }

        public LCDM(SubsystemInitParam init) : this()
        {
            AppConfig = init.AppConfig;
            AppName = init.AppConfig.AppName;
            DataPackage = init.DataPackage;
            elementHost1.Child = new DoMainShell();
            if (!DesignMode)
            {
                //var res = elementHost1.HostContainer.Resources;
                //elementHost1.HostContainer.Resources.MergedDictionaries.Add(LCDMResourceManager.Instance);

                System.Windows.Application.Current.Resources.MergedDictionaries.Add(LCDMResourceManager.Instance);
            }

        }
    }
}
