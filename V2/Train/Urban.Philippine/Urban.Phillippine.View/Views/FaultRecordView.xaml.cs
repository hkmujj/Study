using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Interface;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.Views
{
    /// <summary>
    /// FaultRecordView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentRegion)]
    public partial class FaultRecordView : UserControl
    {
        private IPhilippineViewModel m_ViewModel;
        public FaultRecordView()
        {
            InitializeComponent();
            Data.SelectionChanged += Data_SelectionChanged;

            DataContextChanged += FaultRecordView_DataContextChanged;
        }



        private void FaultRecordView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                m_ViewModel = e.NewValue as IPhilippineViewModel;
            }
        }

        private void Data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var s = e.AddedItems[0] as IFaultInfo;
                if (s != null && s.Code != -1)
                {
                    if (m_ViewModel != null)
                    {
                        m_ViewModel.FaultRecord.Select.Execute(s);
                    }
                }
            }


        }

        private void Data_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var s = Data.SelectedItem as IFaultInfo;
            if (s != null && s.Code != -1)
            {
                if (m_ViewModel != null)
                {
                    m_ViewModel.FaultRecord.Select.Execute(s);
                }
            }
        }
    }
}
