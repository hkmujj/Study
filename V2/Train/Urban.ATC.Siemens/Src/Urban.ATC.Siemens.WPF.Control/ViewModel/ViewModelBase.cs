using Microsoft.Practices.Prism.ViewModel;
using System.Windows.Media;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Urban.ATC.Siemens.WPF.Control.ViewModel
{
    public class ViewModelBase : NotificationObject
    {
        protected SiemensData Parent;
        public  IEventAggregator EventAggregator { get; protected set; }
        public IRegionManager RegionManager { get; protected set; }
        public ViewModelBase()
        {
            BackColor = GDICommonColor.BacgroundBrush;
            LightGreyColor = GDICommonColor.LightGreyBrush;
            LogoColor = GDICommonColor.LogBrush;
            EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
        }

        private SolidColorBrush m_BackColor;
        private SolidColorBrush m_LightGreyColor;
        private SolidColorBrush m_LogoColor;

        public SolidColorBrush LogoColor
        {
            get { return m_LogoColor; }
            set
            {
                if (value.Equals(m_LogoColor))
                {
                    return;
                }
                m_LogoColor = value;
                RaisePropertyChanged(() => LogoColor);
            }
        }

        public SolidColorBrush BackColor
        {
            get { return m_BackColor; }
            set
            {
                if (value.Equals(m_BackColor))
                {
                    return;
                }
                m_BackColor = value;
                RaisePropertyChanged(() => BackColor);
            }
        }

        public SolidColorBrush LightGreyColor
        {
            get { return m_LightGreyColor; }
            set
            {
                if (value.Equals(m_LightGreyColor))
                {
                    return;
                }
                m_LightGreyColor = value;
                RaisePropertyChanged(() => LightGreyColor);
            }
        }
    }
}