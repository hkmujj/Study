using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Motor.HMI.CRH1A.HighVoltage
{
    public class ImageMgr
    {
        private Image[] m_Images;

        public ImageMgr(Image[] images)
        {
            Debug.Assert(images.Length == 19);
            m_Images = images;
        }

        public Image GetImage(AcceptEleArc acceptEleArc)
        {
            return m_Images[(int) acceptEleArc.CurrentState];
        }

        public Image GetImage(HighVoltageSwitch highVoltageSwitch)
        {
            var offset = 0;
            if (highVoltageSwitch.Orientation == Orientation.Horizontal)
            {
                switch (highVoltageSwitch.CurrentState)
                {
                    case HighVoltageSwitch.State.HasConnect:
                        offset = 6;
                        break;
                    case HighVoltageSwitch.State.DisconnectOrUnkown:
                        offset = 7;
                        break;
                    case HighVoltageSwitch.State.CutOffButConnect:
                        offset = 8;
                        break;
                    case HighVoltageSwitch.State.CutOffAndDisconnect:
                        offset = 9;
                        break;
                    case HighVoltageSwitch.State.FaultButConnect:
                        offset = 10;
                        break;
                    case HighVoltageSwitch.State.FaultAndDisconnect:
                        offset = 11;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {

                switch (highVoltageSwitch.CurrentState)
                {
                    case HighVoltageSwitch.State.HasConnect:
                        offset = 0;
                        break;
                    case HighVoltageSwitch.State.DisconnectOrUnkown:
                        offset = 1;
                        break;
                    case HighVoltageSwitch.State.CutOffButConnect:
                        offset = 2;
                        break;
                    case HighVoltageSwitch.State.CutOffAndDisconnect:
                        offset = 3;
                        break;
                    case HighVoltageSwitch.State.FaultButConnect:
                        offset = 4;
                        break;
                    case HighVoltageSwitch.State.FaultAndDisconnect:
                        offset = 5;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return m_Images[offset];
        }
    }
}
