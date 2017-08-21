using System.ComponentModel.Composition;
using System.Windows.Media;
using General.CIR.Controller.ViewModelController;

namespace General.CIR.ViewModels
{
    [Export]
    public class ColumnEndViewModel : ViewModelBase
    {
        private SolidColorBrush m_ColumnEndStatusBrush;

        private string m_ColumnEndStatusStr;

        private string m_ColumnEndStr;

        private string m_FanPress;

        private string m_FanPressDisplay;

        private string m_ID;

        private bool m_IsConnect;

        private bool m_IsRequst;

        private string m_SettingID;

        public ColumnEndController Controller { get; }

        public bool IsConnect
        {
            get
            {
                return m_IsConnect;
            }
            set
            {
                bool flag = value == m_IsConnect;
                if (!flag)
                {
                    m_IsConnect = value;
                    if (value)
                    {
                        IsRequst = false;
                    }
                    Controller.UpdateState();
                    RaisePropertyChanged<bool>(() => IsConnect);
                }
            }
        }

        public bool IsRequst
        {
            get
            {
                return m_IsRequst;
            }
            set
            {
                bool flag = value == m_IsRequst;
                if (!flag)
                {
                    m_IsRequst = value;
                    Controller.UpdateState();
                    RaisePropertyChanged<bool>(() => IsRequst);
                }
            }
        }

        public string ColumnEndStr
        {
            get
            {
                return m_ColumnEndStr;
            }
            set
            {
                bool flag = value == m_ColumnEndStr;
                if (!flag)
                {
                    m_ColumnEndStr = value;
                    RaisePropertyChanged<string>(() => ColumnEndStr);
                }
            }
        }

        public string ColumnEndStatusStr
        {
            get
            {
                return m_ColumnEndStatusStr;
            }
            set
            {
                bool flag = value == m_ColumnEndStatusStr;
                if (!flag)
                {
                    m_ColumnEndStatusStr = value;
                    RaisePropertyChanged<string>(() => ColumnEndStatusStr);
                }
            }
        }

        public string FanPress
        {
            get
            {
                return m_FanPress;
            }
            set
            {
                bool flag = value == m_FanPress;
                if (!flag)
                {
                    m_FanPress = value;
                    Controller.UpdateState();
                    RaisePropertyChanged<string>(() => FanPress);
                }
            }
        }

        public string FanPressDisplay
        {
            get
            {
                return m_FanPressDisplay;
            }
            set
            {
                bool flag = value == m_FanPressDisplay;
                if (!flag)
                {
                    m_FanPressDisplay = value;
                    RaisePropertyChanged<string>(() => FanPressDisplay);
                }
            }
        }

        public string ID
        {
            get
            {
                return m_ID;
            }
            set
            {
                bool flag = value == m_ID;
                if (!flag)
                {
                    m_ID = value;
                    Controller.UpdateState();
                    RaisePropertyChanged<string>(() => ID);
                }
            }
        }

        public string SettingID
        {
            get
            {
                return m_SettingID;
            }
            set
            {
                bool flag = value == m_SettingID;
                if (!flag)
                {
                    m_SettingID = value;
                    RaisePropertyChanged<string>(() => SettingID);
                }
            }
        }

        public SolidColorBrush ColumnEndStatusBrush
        {
            get
            {
                return m_ColumnEndStatusBrush;
            }
            set
            {
                bool flag = Equals(value, m_ColumnEndStatusBrush);
                if (!flag)
                {
                    m_ColumnEndStatusBrush = value;
                    RaisePropertyChanged<SolidColorBrush>(() => ColumnEndStatusBrush);
                }
            }
        }

        [ImportingConstructor]
        public ColumnEndViewModel(ColumnEndController controller)
        {
            Controller = controller;
            Controller.ViewModel = this;
            Controller.Initialize();
        }
    }
}
