using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Urban.ATC.Siemens.WPF.Control.ViewModel;

namespace Urban.ATC.Siemens.WPF.Control.View.Common
{
    /// <summary>
    /// ButtonStatus.xaml 的交互逻辑
    /// </summary>
    public partial class ButtonStatus : UserControl
    {
        private string m_ChianInfo;
        private string m_EnglishInfo;
        private Info.Interface.ButtonReactivation.ButtonStatus m_Status;

        public ButtonStatus()
        {
            InitializeComponent();
        }

        public string ChianInfo
        {
            get { return m_ChianInfo; }
            set
            {
                m_ChianInfo = value;
                SetVisibility();
            }
        }

        public string EnglishInfo
        {
            get { return m_EnglishInfo; }
            set
            {
                m_EnglishInfo = value;
                SetVisibility();
            }
        }

        public int OutLogic { get; set; }

        public Info.Interface.ButtonReactivation.ButtonStatus Status
        {
            get { return m_Status; }
            set
            {
                m_Status = value;
            }
        }

        private void SetVisibility()
        {
            if (string.IsNullOrEmpty(m_ChianInfo) && string.IsNullOrEmpty(m_EnglishInfo))
            {
                this.Visibility = Visibility.Hidden;
            }
            else
            {
                this.ChianInfoTb.Text = ChianInfo;
                this.EnglishInfoRTb.Text = EnglishInfo;
                this.Visibility = Visibility.Visible;
            }
        }

        private void Button_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext != null)
            {
                ((SiemensData)DataContext).ReactionModel.SenData.Execute(OutLogic.ToString());
            }
        }

        private void Button_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext != null)
            {
                ((SiemensData)DataContext).ReactionModel.ClearData.Execute(OutLogic.ToString());
            }
        }
    }
}