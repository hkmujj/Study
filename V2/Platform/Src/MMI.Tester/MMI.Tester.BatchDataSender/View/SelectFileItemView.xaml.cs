using System.Windows.Controls;
using MMI.Tester.BatchDataSender.ViewModel;

namespace MMI.Tester.BatchDataSender.View
{
    /// <summary>
    /// SelectFileItemView.xaml 的交互逻辑
    /// </summary>
    public partial class SelectFileItemView : UserControl
    {
        public SelectFileItemView()
        {
            InitializeComponent();
        }

        private void Validation_OnError(object sender, ValidationErrorEventArgs e)
        {
            var vm = DataContext as TempldateDataFileItemViewModel;
            if (vm != null)
            {
                vm.FileHasError = true;
            }
        }
    }
}
