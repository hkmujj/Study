using System;
using System.ComponentModel;
using System.Windows.Forms;
using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.CommonView.View.RegionF
{
    public partial class RegionFControl : UserControl, IDriverInterfaceView
    {
        private IDriverInterface m_DriverInterface;

        public event EventHandler<DriverActionEventArgs> MouseDownEvent;

        public event EventHandler<DriverActionEventArgs> MouseUpEvent;

        public event EventHandler<DriverActionEventArgs> MouseClickEvent;

        public RegionFControl()
        {
            InitializeComponent();
        }

        public IDriverInterface DriverInterface
        {
            protected set
            {
                m_DriverInterface = value;
                Invalidate();
            }
            get { return m_DriverInterface; }
        }

        protected virtual void OnMouseDownEvent(DriverActionEventArgs e)
        {
            var handler = MouseDownEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnMouseUpEvent(DriverActionEventArgs e)
        {
            var handler = MouseUpEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnClickEvent(DriverActionEventArgs e)
        {
            var handler = MouseClickEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void UpdateView(IDriverInterface driverInterface)
        {
            if (DriverInterface != null)
            {
                DriverInterface.PropertyChanged -= DriverInterfaceOnPropertyChanged;
                DriverInterface.DriverSelectable.Selected -= DriverSelectableOnSelected;
            }
            if (driverInterface != null)
            {
                driverInterface.PropertyChanged += DriverInterfaceOnPropertyChanged;
                driverInterface.DriverSelectable.Selected += DriverSelectableOnSelected;
            }
            DriverInterface = driverInterface;
            DriverInterfaceOnPropertyChanged(null, null);
        }

        private void DriverSelectableOnSelected(object sender, DriverSelectedEventArgs driverSelectedEventArgs)
        {
            switch (driverSelectedEventArgs.SelectedType)
            {
                case DriverSelectedType.Press:
                    OnMouseDownEvent(new DriverActionEventArgs(driverSelectedEventArgs.SelectedItem.UserActionType,
                        driverSelectedEventArgs.SelectedItem));
                    break;
                case DriverSelectedType.Relase:
                    OnMouseUpEvent(new DriverActionEventArgs(driverSelectedEventArgs.SelectedItem.UserActionType,
                        driverSelectedEventArgs.SelectedItem));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 刷新界面绑定源数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="propertyChangedEventArgs"></param>
        protected virtual void DriverInterfaceOnPropertyChanged(object sender,
            PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs != null && propertyChangedEventArgs.PropertyName == "DriverSelectable")
            {

            }
        }
    }
}
