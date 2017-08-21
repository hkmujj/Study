using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShenZhenLine11.Constance;
using Subway.ShenZhenLine11.Extention;
using Subway.ShenZhenLine11.ShenZhenAttribute;
using Subway.ShenZhenLine11.ViewModels;
using Subway.ShenZhenLine11.Views.MainRoot;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace Subway.ShenZhenLine11.Views.MainContent
{
    /// <summary>
    /// EventInfo.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContent)]
    [SuperiorNavigator(Type = typeof(MainShell))]
    [TitleName(Name = "帮助")]
    public partial class EventInfo : UserControl
    {
        public EventInfo()
        {
            InitializeComponent();
            DataGrid.SelectionChanged += DataGrid_SelectionChanged;
            DataGrid.Loaded += (sender, args) => { RowChanged(); };
            ServiceLocator.Current.GetInstance<EventInfoViewModel>().Manager.CurrentChanged += (args, args1) =>
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(RowChanged));
            };
        }

        private void RowChanged()
        {
            foreach (var item in DataGrid.Items)
            {
                var index = DataGrid.Items.IndexOf(item);

                if (index == DataGrid.Items.Count - 1)
                {
                    DataGrid.GetCell(index, 0)?.SetGridCellBorderThiness(m_Th7);
                    DataGrid.GetCell(index, 1)?.SetGridCellBorderThiness(m_Th8);
                    DataGrid.GetCell(index, 2)?.SetGridCellBorderThiness(m_Th8);
                    DataGrid.GetCell(index, 3)?.SetGridCellBorderThiness(m_Th9);
                }
                else
                {
                    DataGrid.GetCell(index, 0)?.SetGridCellBorderThiness(m_Th10);
                    DataGrid.GetCell(index, 1)?.SetGridCellBorderThiness(m_Th11);
                    DataGrid.GetCell(index, 2)?.SetGridCellBorderThiness(m_Th11);
                    DataGrid.GetCell(index, 3)?.SetGridCellBorderThiness(m_Th12);
                }
            }
            var s = DataGridExtention.GetVisualChild<DataGridCellsPanel>(this);
            if (s!=null)
            {
                if (DataGrid.Items.Count == 0)
                {
                    foreach (var child in s.Children)
                    {
                        SetHeaderBorderOne(child, s);
                    }
                }
                else
                {
                    foreach (var child in s.Children)
                    {
                        SetHeaderBorderTwo(child, s);
                    }
                }
            }
          
            SetDataGridBack();
        }

        private void SetHeaderBorderTwo(object child, DataGridCellsPanel s)
        {
            var tmp = child as DataGridColumnHeader;
            var bors = tmp?.Template.FindName("PART_Border", tmp) as Border;
            if (bors != null)
            {
                var index = s.Children.IndexOf(tmp);
                if (index == 0)
                {
                    bors.BorderThickness = m_Th4;
                }
                else if (index == s.Children.Count - 1)
                {
                    bors.BorderThickness = m_Th5;
                }
                else
                {
                    bors.BorderThickness = m_Th6;
                }
            }
        }

        private void SetHeaderBorderOne(object child, DataGridCellsPanel s)
        {
            var tmp = child as DataGridColumnHeader;
            var bors = tmp?.Template.FindName("PART_Border", tmp) as Border;
            if (bors != null)
            {
                var index = s.Children.IndexOf(tmp);
                if (index == 0)
                {
                    bors.BorderThickness = m_Th1;
                }
                else if (index == s.Children.Count - 1)
                {
                    bors.BorderThickness = m_Th2;
                }
                else
                {
                    bors.BorderThickness = m_Th3;
                }
            }
        }

        private const double ThValue1 = 2;
        private const double ThValue2 = 1;
        private readonly Thickness m_Th1 = new Thickness(ThValue1, ThValue1, ThValue2, ThValue1);
        private readonly Thickness m_Th2 = new Thickness(ThValue2, ThValue1, ThValue1, ThValue1);
        private readonly Thickness m_Th3 = new Thickness(ThValue2, ThValue1, ThValue2, ThValue1);
        private readonly Thickness m_Th4 = new Thickness(ThValue1, ThValue1, ThValue2, ThValue2);
        private readonly Thickness m_Th5 = new Thickness(ThValue2, ThValue1, ThValue1, ThValue2);
        private readonly Thickness m_Th6 = new Thickness(ThValue2, ThValue1, ThValue2, ThValue2);
        private readonly Thickness m_Th7 = new Thickness(ThValue1, ThValue2, ThValue2, ThValue1);
        private readonly Thickness m_Th8 = new Thickness(ThValue2, ThValue2, ThValue2, ThValue1);
        private readonly Thickness m_Th9 = new Thickness(ThValue2, ThValue2, ThValue1, ThValue1);
        private readonly Thickness m_Th10 = new Thickness(ThValue1, ThValue2, ThValue2, ThValue2);
        private readonly Thickness m_Th11 = new Thickness(ThValue2, ThValue2, ThValue2, ThValue2);
        private readonly Thickness m_Th12 = new Thickness(ThValue2, ThValue2, ThValue1, ThValue2);
        private readonly SolidColorBrush m_Normal = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        private readonly SolidColorBrush m_Red = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetDataGridBack();

            //InvalidateVisual();
        }

        private void SetDataGridBack()
        {
            for (int i = 1; i < 4; i++)
            {
                foreach (var index in from object item in DataGrid.Items select DataGrid.Items.IndexOf(item))
                {
                    DataGrid.GetCell(index, i)?.SetGridCellBackground(index == DataGrid.SelectedIndex ? m_Red : m_Normal);
                }
            }
            TextBlock.Text = (DataGrid.SelectedItem as Info.EventInfo)?.Emergency;
        }
    }
}