using System.Windows.Controls;
using Engine.HMI.SS3B.View.Model;

namespace Engine.HMI.SS3B.View.View.LiuZhou
{
    /// <summary>
    /// FatulePage.xaml 的交互逻辑
    /// </summary>
    public partial class FatulePage : UserControl
    {
        public FatulePage()
        {
            InitializeComponent();
            this.DataContext=new MainViewTableModel();
        }
    }
}
