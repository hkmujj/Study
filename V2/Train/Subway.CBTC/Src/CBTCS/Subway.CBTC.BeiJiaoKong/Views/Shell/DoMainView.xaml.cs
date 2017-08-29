using System.Windows.Controls;
using System.Windows.Input;

namespace Subway.CBTC.BeiJiaoKong.Views.Shell
{
    /// <summary>
    /// 创 建 者：谭栋炜
    /// 创建时间：2017/1/16
    /// 修 改 者：谭栋炜
    /// 修改时间：2017/1/16
    /// </summary>
    public partial class DoMainView : UserControl
    {
        public DoMainView()
        {
            InitializeComponent();
        }

        private void DoMainView_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.BeiJiaoKongViewModel.DoMainViewModel.Controller.ToScreenSaverCommand.Execute(null);
        }
    }
}
