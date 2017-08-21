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
    public partial class GSMRStateControl : PictureBox
    {
        private IConnectState m_ConnectState;
        private GSMRState m_GSMRState;

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


        public GSMRState GSMRState
        {
            set
            {
                m_GSMRState = value;
                UpdateContens();
                Invalidate();
            }
            get { return m_GSMRState; }
        }

        public GSMRStateControl()
        {
            InitializeComponent();
        }

        private void UpdateState()
        {
            GSMRState = ConnectState.GSMRState;
        }


        private void ConnectStateOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == PropertySupport.ExtractPropertyName<IConnectState, GSMRState>(a => a.GSMRState))
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
            switch (GSMRState)
            {
                case GSMRState.None:
                    img = ATPCommonResource.GSM_灰;
                    break;
                case GSMRState.HasOne:
                    img = ATPCommonResource.GSM_1;
                    break;
                case GSMRState.HasTwo:
                    img = ATPCommonResource.GSM_2;
                    break;
                case GSMRState.HasThree:
                    img = ATPCommonResource.GSM_3;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            this.InvokeUpdateImageIfNeed(img);
        }

    }
}
