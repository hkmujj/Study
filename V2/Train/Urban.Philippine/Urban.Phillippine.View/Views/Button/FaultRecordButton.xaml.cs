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
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Event;
using Urban.Phillippine.View.Interface.Enum;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.Views.Button
{
    /// <summary>
    /// FaultRecordButton.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ButtonRegion)]
    public partial class FaultRecordButton : UserControl
    {
        private IPhilippineViewModel m_ViewModel;
        public FaultRecordButton()
        {
            InitializeComponent();
            DataContextChanged += FaultRecordButton_DataContextChanged;
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ButtonInitEvent>().Subscribe((args) =>
            {
                if (args.ViewName == ControlNames.FaultRecordView)
                {
                    Current.IsChecked = true;
                    if(m_ViewModel != null)
                    {
                        m_ViewModel.FaultRecord.ChangedType.Execute(FaultType.Current.ToString());
                    }
                }
            }, ThreadOption.UIThread);
            Current.PreviewMouseDown += Current_MouseDown;
            Histoy.PreviewMouseDown += Current_MouseDown;
        }

        private void Current_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var tmp = sender as RadioButton;
            if (tmp != null)
            {
                if (tmp.Name.Equals("Current"))
                {
                    if (m_ViewModel != null)
                    {
                        m_ViewModel.FaultRecord.ChangedType.Execute(FaultType.Current.ToString());
                    }
                }
                else
                {
                    if(m_ViewModel != null)
                    {
                        m_ViewModel .FaultRecord.ChangedType.Execute(FaultType.History.ToString());
                    }
                }
            }
        }

        private void FaultRecordButton_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                m_ViewModel = e.NewValue as IPhilippineViewModel;

            }
        }
    }
}
