using System;
using System.Drawing;
using CommonUtil.Controls;
using Engine.TCMS.HXD3C.Models;

namespace Engine.TCMS.HXD3C.Utils
{
    public class UnattendedTrialControl : GDIRectText
    {
        private UnattendedTrialType m_UnattendedTrialType;

        private bool FlashFlag { get { return IsFlash(); } }

        private int m_FlashTime;

        public int FlashFreq { set; get; }

        public UnattendedTrialType UnattendedTrialType
        {
            set
            {
                m_UnattendedTrialType = value;
                Visible = true;
                Text = "无人警惕";

                switch (value)
                {
                    case UnattendedTrialType.Unkown:
                        Visible = false;
                        TextColor = Color.White;
                        break;
                    case UnattendedTrialType.Normal:
                        BackColorVisible = false;
                        TextColor = Color.White;
                        break;
                    case UnattendedTrialType.Cutoff:
                        Text = "无人警惕\r\n隔离";
                        BackColorVisible = false;
                        TextColor = Color.White;
                        break;
                    case UnattendedTrialType.Green:
                        BackColorVisible = true;
                        BkColor = Common.GreenBrush.Color;
                        TextColor = Color.Black;
                        break;
                    case UnattendedTrialType.Yellow:
                        BackColorVisible = true;
                        BkColor = Common.YellowBrush.Color;
                        TextColor = Color.Black;
                        break;
                    case UnattendedTrialType.YellowFlash:
                        if (FlashFlag)
                        {
                            BackColorVisible = false;
                            TextColor = Color.White;
                        }
                        else
                        {
                            BackColorVisible = true;
                            BkColor = Common.YellowBrush.Color;
                            TextColor = Color.Black;
                        }
                        break;
                    case UnattendedTrialType.Red:
                        BackColorVisible = true;
                        BkColor = Common.RedBrush.Color;
                        TextColor = Color.Black;
                        break;
                    case UnattendedTrialType.RedFlash:
                        if (FlashFlag)
                        {
                            BackColorVisible = false;
                            TextColor = Color.White;
                        }
                        else
                        {
                            BackColorVisible = true;
                            BkColor = Common.RedBrush.Color;
                            TextColor = Color.Black;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value", value, null);
                }
            }
            get { return m_UnattendedTrialType; }
        }

        public UnattendedTrialControl()
        {
            FlashFreq = 1;
        }

        /// <summary>
        /// 闪烁
        /// </summary>
        /// <returns></returns>
        private bool IsFlash()
        {
            if (m_FlashTime < FlashFreq * 5)
            {
                ++m_FlashTime;
                return true;
            }
            if (m_FlashTime >= FlashFreq * 5 && m_FlashTime < FlashFreq * 10)
            {
                ++m_FlashTime;
                return false;
            }
            m_FlashTime = 0;
            return false;
        }
    }
}