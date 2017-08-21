using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Engine._6A.Interface;
using Engine._6A.Interface.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine._6A.Views.Common.Fault
{
    /// <summary>
    /// FaultViewContent.xaml 的交互逻辑
    /// </summary>
    public partial class FaultViewContent : UserControl
    {

        public FaultViewContent()
        {
            InitializeComponent();
            DataContextChanged += FaultViewContent_DataContextChanged;
            IsVisibleChanged += FaultViewContent_OnIsVisibleChanged;
        }

        private IFaultViewModel m_FaultViewModel;
        void FaultViewContent_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                m_FaultViewModel = ServiceLocator.Current.GetInstance<IFaultViewModel>();
            }
        }

        public static readonly DependencyProperty FaultInfosProperty = DependencyProperty.Register(
            "FaultInfos", typeof(IList<IFaultInfo>), typeof(FaultViewContent), new PropertyMetadata(default(IList<IFaultInfo>), FaultInfoChanged));

        private static void FaultInfoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FaultViewContent)d).OnFaultInfoChanged();
        }

        private void OnFaultInfoChanged()
        {
            var index = 1;
            Grid.Children.Clear();
            if (FaultInfos == null)
            {
                return;
            }
            foreach (var tmp in FaultInfos.Select(info => new FaultInfo(info, ContentName.ColumnDefinitions)))
            {
                Grid.SetRow(tmp, index);
                Grid.Children.Add(tmp);
                index++;
            }
        }
        public IList<IFaultInfo> FaultInfos
        {
            get { return (IList<IFaultInfo>)GetValue(FaultInfosProperty); }
            set { SetValue(FaultInfosProperty, value); }
        }

        private void FaultViewContent_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue && !(bool)e.OldValue)
            {
                m_FaultViewModel.Reset.Execute("Fault");
            }
        }
    }
}
