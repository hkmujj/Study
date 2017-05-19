using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShiJiaZhuangLine1.Interface.Attibutes;
using Subway.ShiJiaZhuangLine1.Interface.Model;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;
using Subway.ShiJiaZhuangLine1.Subsystem.Controls;
using Subway.ShiJiaZhuangLine1.Subsystem.ViewModels;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View.Contents.MainContentContents
{
    /// <summary>
    /// StationSettingView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContentContentRegion)]
    [TitleName("站点设置")]
    public partial class StationSettingView 
    {
        private IDictionary<string, Control> m_Controls;
        private bool m_IsLoad = false;
        public StationSettingView()
        {
            InitializeComponent();
            this.DataContextChanged += ControlDataContextChanged;
        }

        private void ControlDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                Data = ((ShellViewModel)DataContext).Model;
                if (!m_IsLoad)
                {
                    InitControl();
                }
            }
        }

        private void InitControl()
        {
            m_IsLoad = true;
            m_Controls = new Dictionary<string, Control>();
            var data = Data;
            var list = data.StationsMgr.AllData.Values.ToList().OrderBy(o => o.Number);
            var brush = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
            var thickness = new Thickness(2, 2, 2, 2);
            var col = 0;
            var row = 0;
            foreach (var station in list)
            {
                var tmp = new RecTextButton
                {
                    Content = station.StationName,
                    Tag = station.Number,
                    Foreground = Brushes.White,
                    FontSize = 18,
                    Padding = new Thickness(0, 0, 0, 0),
                    Background = Brushes.Transparent,
                    BorderThickness = thickness,
                    BorderBrush = Brushes.DarkGray,
                    Margin = new Thickness(10, 10, 10, 10)
                };


                Grid.SetColumn(tmp, col);
                Grid.SetRow(tmp, row);
                tmp.PreviewMouseDown += (sender, args) =>
                {
                    var obj = sender as RecTextButton;
                    if (obj != null && obj.Tag != null)
                    {
                        foreach (var control in m_Controls.Values)
                        {
                            control.Background = Brushes.Transparent;
                            control.Foreground = Brushes.White;
                        }
                        obj.Background = brush;
                        obj.Foreground = Brushes.Black;
                        data.StationSettingModel.StationSelect.Execute(obj.Tag.ToString());
                    }
                };
                if (col == 4)
                {
                    col = 0;
                    row++;
                }
                else
                {
                    col++;
                }
                Station.Children.Add(tmp);
                m_Controls.Add(station.Number.ToString(), tmp);
            }
            var coltmp = col;
            var rowtmp = row;
            for (int i = row; i <= 4 - rowtmp;)
            {
                for (int j = coltmp; j <= 4; j++)
                {
                    var tmp = new RecTextButton();

                    tmp.Foreground = Brushes.White;
                    tmp.FontSize = 18;
                    tmp.Padding = new Thickness(0, 0, 0, 0);
                    tmp.Background = Brushes.Transparent;
                    tmp.BorderThickness = thickness;
                    tmp.BorderBrush = Brushes.DarkGray;

                    tmp.Margin = new Thickness(10, 10, 10, 10);

                    Grid.SetColumn(tmp, col);
                    Grid.SetRow(tmp, row);
                    if (col == 4)
                    {
                        col = 0;
                        coltmp = 0;
                        row++;
                        i++;
                    }
                    else
                    {
                        col++;

                    }
                    Station.Children.Add(tmp);
                }
            }
        }

        public IMMI Data { get; private set; }

    }
}
