using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Engine._6A.CommonControl;
using Engine._6A.Constance;
using Engine._6A.Interface;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.SystemSetting
{
    /// <summary>
    /// TrainSetting.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.SystemTabTabRegion, Priority = 1)]
    public partial class TrainSetting : ITabItemInfoProvider
    {
        private readonly IDictionary<string, object[]> m_Controls = new Dictionary<string, object[]>();

        public TrainSetting()
        {
            InitializeComponent();
            foreach (var child in ButtonGrid.Children)
            {
                var con = child as ImageTextButton;
                if (con != null)
                {
                    m_Controls.Add(con.Content.ToString(), new[] { con.Tag, con.Background, con });
                }
            }

        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var con = sender as ImageTextButton;
            if (con != null)
            {
                var resou = (Brush)Resources["AquaBrush"];
                var transparentBrush = (Brush)Resources["TransparentBrush"];
                var tmp = ServiceLocator.Current.GetInstance<MouseEventClass>();
                var cons = m_Controls.ToDictionary(s => s.Key, s => ((Control)s.Value[2]));
                tmp.ResetBackground(m_Controls);
                tmp.ResetBorderBrush(cons, transparentBrush);
                tmp.SetBackground(con, (Brush)((object[])m_Controls[con.Content.ToString()])[0]);
                tmp.SetBorderBrush(con, resou);
            }
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var con = sender as ImageTextButton;
            if (con != null)
            {
                var tmp = ServiceLocator.Current.GetInstance<MouseEventClass>();
                tmp.SetBackground(con, (Brush)((object[])m_Controls[con.Content.ToString()])[1]);
            }
        }

        public string HeadName
        {
            get { return "系统设置"; }
        }
    }
}
