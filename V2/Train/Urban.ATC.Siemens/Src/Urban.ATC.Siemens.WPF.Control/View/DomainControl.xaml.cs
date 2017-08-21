using System;
using System.Threading;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.ATC.Siemens.WPF.Control.Constant;
using Urban.ATC.Siemens.WPF.Control.Event;
using Urban.ATC.Siemens.WPF.Control.View.RegionD;

namespace Urban.ATC.Siemens.WPF.Control.View
{
    /// <summary>
    /// DomainControl.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRoot)]
    public partial class DomainControl
    {
        private readonly Timer m_Timer;
        private DateTime date;
        public DomainControl()
        {
            InitializeComponent();
            m_Timer = new Timer(state =>
            {
                if ((DateTime.Now - date).Seconds > 3)
                {
                    EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs()
                    {
                        Region = RegionNames.Menu,
                        Name = ControlNames.Menu,
                    });
                }
            });
            EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            this.Loaded += DomainControl_Loaded;
            this.IsVisibleChanged += VisibleChanged;

        }


        private IEventAggregator EventAggregator;
        private void DomainControl_Loaded(object sender, RoutedEventArgs e)
        {
            EventAggregator.GetEvent<NavigatorEvent>().Subscribe((args) =>
            {
                if (args.Region == RegionNames.Menu)
                {
                    this.ButtonDownConten.Content = null;
                    this.MenuContent.Content = null;
                    if (m_Menu == null && args.Name == ControlNames.Menu)
                    {
                        m_Menu = (Menu)Activator.CreateInstance(Type.GetType(args.Name));
                    }
                    if (m_MenuOpen == null && args.Name == ControlNames.MenuButtonOpen)
                    {
                        m_MenuOpen = (System.Windows.Controls.Control)Activator.CreateInstance(Type.GetType(args.Name));
                    }

                    if (args.Name == ControlNames.Menu)
                    {
                        MenuContent.Content = m_Menu;
                    }
                    else if (args.Name == ControlNames.MenuButtonOpen)
                    {
                        ButtonDownConten.Content = m_MenuOpen;
                    }

                    if (args.Name == ControlNames.MenuButtonOpen)
                    {
                        date = DateTime.Now;
                        //m_Timer.Change(3000, int.MaxValue);
                    }

                }
            }, ThreadOption.UIThread);
        }

        private void VisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue && !(bool)e.OldValue)
            {
                m_Timer.Change(0, 200);
            }
        }

        private Menu m_Menu = null;
        private System.Windows.Controls.Control m_MenuOpen = null;

        //public void NavagateTo(string fullName)
        //{
        //    if (Dispatcher.CheckAccess())
        //    {
        //        SetControlContent(fullName);
        //    }
        //    else
        //    {
        //        this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
        //        {
        //            SetControlContent(fullName);
        //        }));
        //    }
        //}
        //private void SetControlContent(string fullName)
        //{
        //    //this.ButtonDownConten.Content = null;
        //    //this.MenuContent.Content = null;

        //    //if (fullName == MenuName.MenuButton)
        //    //{
        //    //    m_Timer.Change(int.MaxValue, int.MaxValue);
        //    //    if (m_Menu == null)
        //    //    {
        //    //        m_Menu = (Menu)Activator.CreateInstance(Type.GetType(fullName));
        //    //        this.MenuContent.Content = m_Menu;
        //    //    }
        //    //    else
        //    //    {
        //    //        this.MenuContent.Content = m_Menu;
        //    //    }
        //    //}

        //    //if (fullName == MenuName.MenuButtonOpen)
        //    //{
        //    //    m_Timer.Change(3000, int.MaxValue);
        //    //    if (m_MenuOpen == null)
        //    //    {
        //    //        m_MenuOpen = (System.Windows.Controls.Control)Activator.CreateInstance(Type.GetType(fullName));
        //    //        this.ButtonDownConten.Content = m_MenuOpen;
        //    //    }
        //    //    else
        //    //    {
        //    //        this.ButtonDownConten.Content = m_MenuOpen;
        //    //    }
        //    //}
        //}

        //public string ViewName { get; set; }
    }
}