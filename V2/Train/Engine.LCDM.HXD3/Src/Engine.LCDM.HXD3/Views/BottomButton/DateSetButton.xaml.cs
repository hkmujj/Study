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
using Engine.LCDM.HXD3.Constance;
using Engine.LCDM.HXD3.Interfaces;
using Engine.LCDM.HXD3.LCMDAtt;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine.LCDM.HXD3.Views.BottomButton
{
    /// <summary>
    /// DateSetButton.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.BottomButton)]
    [Active(ControlType = typeof(DateAndTime.DateAndTime))]
    public partial class DateSetButton : IButtons
    {
        public DateSetButton()
        {
            InitializeComponent();
            F1 = ButtonF1;
            F2 = ButtonF2;
            F3 = ButtonF3;
            F4 = ButtonF4;
            F8 = ButtonF8;
        }

        public Button F1 { get; private set; }
        public Button F2 { get; private set; }
        public Button F3 { get; private set; }
        public Button F4 { get; private set; }
        public Button F5 { get; private set; }
        public Button F6 { get; private set; }
        public Button F7 { get; private set; }
        public Button F8 { get; private set; }
    }
}
