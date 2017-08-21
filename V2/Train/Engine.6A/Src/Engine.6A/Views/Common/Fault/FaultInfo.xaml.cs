using System.Linq;
using System.Windows.Controls;
using CommonUtil.Util;
using Engine._6A.Interface;

namespace Engine._6A.Views.Common.Fault
{
    /// <summary>
    /// FaultInfo.xaml 的交互逻辑
    /// </summary>
    public partial class FaultInfo : UserControl
    {
        public FaultInfo(IFaultInfo faultInfo, ColumnDefinitionCollection columnDefinitionCollection)
        {
            InitializeComponent();
            Grid.ColumnDefinitions.Clear();

            foreach (var definition in columnDefinitionCollection)
            {
                var tmp = new ColumnDefinition { Width = definition.Width };
                Grid.ColumnDefinitions.Add(tmp);
            }
            One.Text = string.Format("{0}:{1} {2}:{3}", faultInfo.DataTime.Month.ToString().PadLeft(2, '0'), faultInfo.DataTime.Day.ToString().PadLeft(2, '0'), faultInfo.DataTime.Hour.ToString().PadLeft(2, '0'), faultInfo.DataTime.Minute.ToString().PadLeft(2, '0'));
            Two.Text = EnumUtil.GetDescription(faultInfo.SubSystem).FirstOrDefault();
            Three.Text = faultInfo.Context;
            Four.Text = EnumUtil.GetDescription(faultInfo.Distinction).FirstOrDefault();
            Five.Text = faultInfo.Position;
            Fix.Text = faultInfo.Speed;
        }
    }
}
