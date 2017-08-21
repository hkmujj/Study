using System;
using System.Drawing;
using System.Windows.Forms;
using Motor.ATP.CommonResource;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.CommonView.View.RegionD
{
    public partial class CabSignalControl : PictureBox
    {
        private CabSignalCode m_CabSignalCode;

        private readonly Timer m_FlickerTimer;

        private Image m_ImageBuffer;
        private readonly Action m_UpdateCabSignalAction;

        public CabSignalCode CabSignalCode
        {
            set
            {
                m_CabSignalCode = value;
                this.InvokeIfNeed(m_UpdateCabSignalAction);
                Invalidate();
            }
            get { return m_CabSignalCode; }
        }

        public CabSignalControl()
        {
            InitializeComponent();
            m_FlickerTimer = new Timer() { Interval = 1000 };
            m_FlickerTimer.Tick += (sender, args) =>
            {
                Image = Image == null ? m_ImageBuffer : null;
            };
            m_UpdateCabSignalAction = RefreshBySignal;
        }

        public void RefreshBySignal()
        {
            m_FlickerTimer.Stop();

            switch (CabSignalCode)
            {
                case CabSignalCode.Unknown:
                    Image = null;
                    break;
                case CabSignalCode.L6:
                    //Image = ATPCommonResource.CabSignal_L6;
                    break;
                case CabSignalCode.L5:
                    Image = ATPCommonResource.CabSignal_L5;
                    break;
                case CabSignalCode.L4:
                    Image = ATPCommonResource.CabSignal_L4;
                    break;
                case CabSignalCode.L3:
                    Image = ATPCommonResource.CabSignal_L3;
                    break;
                case CabSignalCode.L2:
                    Image = ATPCommonResource.CabSignal_L3;
                    break;
                case CabSignalCode.L:
                    Image = ATPCommonResource.CabSignal_L;
                    break;
                case CabSignalCode.LU:
                    Image = ATPCommonResource.CabSignal_LU;
                    break;
                case CabSignalCode.LU2:
                    Image = ATPCommonResource.CabSignal_LU2;
                    break;
                case CabSignalCode.U:
                    Image = ATPCommonResource.CabSignal_U;
                    break;
                case CabSignalCode.U2:
                    Image = ATPCommonResource.CabSignal_U2;
                    break;
                case CabSignalCode.U2S:
                    m_ImageBuffer = ATPCommonResource.CabSignal_U2S;
                    Image = ATPCommonResource.CabSignal_U2S;
                    m_FlickerTimer.Interval = 1000;
                    m_FlickerTimer.Start();
                    break;
                case CabSignalCode.UU:
                    Image = ATPCommonResource.CabSignal_UU;
                    break;
                case CabSignalCode.UUS:
                    m_ImageBuffer = ATPCommonResource.CabSignal_UUS;
                    Image = ATPCommonResource.CabSignal_UUS;
                    m_FlickerTimer.Interval = 1000;
                    m_FlickerTimer.Start();
                    break;
                case CabSignalCode.HB:
                    m_ImageBuffer = ATPCommonResource.CabSignal_HB;
                    Image = ATPCommonResource.CabSignal_HB;
                    m_FlickerTimer.Interval = 1000;
                    m_FlickerTimer.Start();
                    break;
                case CabSignalCode.HU:
                    Image = ATPCommonResource.CabSignal_HU;
                    break;
                case CabSignalCode.H:
                    Image = ATPCommonResource.CabSignal_H;
                    break;
                case CabSignalCode.Code25:
                case CabSignalCode.Code27:
                case CabSignalCode.NC:
                    m_ImageBuffer = ATPCommonResource.CabSignal_HZ;
                    Image = ATPCommonResource.CabSignal_HZ;
                    //m_FlickerTimer.Interval = 1000;
                    //m_FlickerTimer.Start();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
