using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.ATC.Siemens.WPF.Control.Constant;
using Urban.ATC.Siemens.WPF.Control.ViewModel;
using Urban.Info.Interface.ButtonReactivation;
using ButtonStatus = Urban.ATC.Siemens.WPF.Control.View.Common.ButtonStatus;

namespace Urban.ATC.Siemens.WPF.Control.View
{
    /// <summary>
    /// ButtonRestScreen.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRoot)]
    public partial class ButtonRestScreen 
    {
        private Dictionary<int, System.Windows.Controls.Control> m_Control;

        public ButtonRestScreen()
        {
            InitializeComponent();
            LoadControl();
            DataContextChanged += ButtonRestScreen_DataContextChanged;
        }

        private void ButtonRestScreen_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                ((SiemensData)DataContext).ButtonReactivationMgr.FaultButtonCoutChanged += ButtonReactivationMgr_FaultButtonCoutChanged;
            }
        }

        private void ButtonReactivationMgr_FaultButtonCoutChanged()
        {
            OnChange();
        }

        private void LoadControl()
        {
            m_Control = new Dictionary<int, System.Windows.Controls.Control>();
            for (int i = 0; i < 5; i++)
            {
                var tmp = new ButtonStatus();
                Grid.SetRow(tmp, i);
                m_Control.Add(i, tmp);
            }
        }

        public static readonly DependencyProperty ButtonInfosProperty = DependencyProperty.Register(
            "ButtonInfos", typeof(IList<IButtonInfo>), typeof(ButtonRestScreen), new PropertyMetadata(default(IList<IButtonInfo>), ChangeSatus));

        private static void ChangeSatus(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ButtonRestScreen)d).OnChange();
        }

        private void OnChange()
        {
            ButtonGrid.Children.RemoveRange(1, 5);

            int i = 0;
            foreach (var info in ButtonInfos)
            {
                ((ButtonStatus)(m_Control[i])).ChianInfo = info.ChinaInfo;
                ((ButtonStatus)(m_Control[i])).EnglishInfo = info.EnglishInfo;
                ((ButtonStatus)(m_Control[i])).OutLogic = info.OutLogic;
                ButtonGrid.Children.Add(m_Control[i]);
                i++;
            }
        }

        public IList<IButtonInfo> ButtonInfos
        {
            get { return (IList<IButtonInfo>)GetValue(ButtonInfosProperty); }
            set { SetValue(ButtonInfosProperty, value); }
        }

        private void ButtonRestScreen_OnLoaded(object sender, RoutedEventArgs e)
        {
        }
    }
}