using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CommonUtil.Util;
using Motor.ATP.CommonResource;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.CommonView.View.RegionE
{
    public partial class RBCConnectStateControl : PictureBox
    {
        private IConnectState m_ConnectState;
        private RBCConnectState m_RBCConnectState;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public IConnectState ConnectState
        {
            set
            {
                if (m_ConnectState != null)
                {
                    ConnectState.PropertyChanged -= ConnectStateOnPropertyChanged;
                }

                m_ConnectState = value;

                if (ConnectState != null)
                {
                    UpdateState();
                    UpdateVisible();
                    ConnectState.PropertyChanged += ConnectStateOnPropertyChanged;
                }
            }
            get { return m_ConnectState; }
        }


        public RBCConnectState RBCConnectState
        {
            set
            {
                m_RBCConnectState = value;
                UpdateContens();
                Invalidate();
            }
            get { return m_RBCConnectState; }
        }

        public RBCConnectStateControl()
        {
            InitializeComponent();
        }

        private void UpdateState()
        {
            RBCConnectState = ConnectState.RBCConnectState;
        }


        private void ConnectStateOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == PropertySupport.ExtractPropertyName<IConnectState, RBCConnectState>(a => a.RBCConnectState))
            {
                UpdateState();
            }
            else if (propertyChangedEventArgs.PropertyName == PropertySupport.ExtractPropertyName<IConnectState, bool>(a => a.Visible))
            {
                UpdateVisible();
            }
        }

        private void UpdateVisible()
        {
            this.InvokeUpdateVisibleIfNeed(ConnectState.Visible);
        }

        private void UpdateContens()
        {
            Image img = null;
            switch (RBCConnectState)
            {
                case RBCConnectState.Unconnected:
                    img = ATPCommonResource.未与RBC相连;
                    break;
                case RBCConnectState.Connecting:
                    img = ATPCommonResource.正在与RBC相连;
                    break;
                case RBCConnectState.Connected:
                    img = ATPCommonResource.已经与RBC相连;
                    break;
                case RBCConnectState.ConnectFault:
                    img = ATPCommonResource.没有无线通信;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            this.InvokeUpdateImageIfNeed(img);
        }

    }
}
