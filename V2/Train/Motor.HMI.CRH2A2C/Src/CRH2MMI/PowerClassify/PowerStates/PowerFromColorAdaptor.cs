using System;
using System.Drawing;
using CRH2MMI.Common.Global;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.PowerClassify.PowerStates
{
    class PowerFromColorAdaptor
    {
        private static IPowerFromColorAdaptor m_ColorAdaptor;

        public static Color GetColor(PowerFrom powerFrom)
        {
            if (m_ColorAdaptor == null)
            {
                if (GlobalInfo.CurrentCRH2Config.Type == CRH2Type.CRH380A)
                {
                    m_ColorAdaptor  =new CRH380APowerFromColorAdaptor();
                }
                else
                {
                    m_ColorAdaptor = new NomarPowerFromColorAdaptor();
                }
            }
            return m_ColorAdaptor.GetColor(powerFrom);
        }

    }

    interface IPowerFromColorAdaptor
    {
        Color GetColor(PowerFrom powerFrom);
    }

    class NomarPowerFromColorAdaptor : IPowerFromColorAdaptor
    {
        public Color GetColor(PowerFrom powerFrom)
        {
            switch (powerFrom)
            {
                case PowerFrom.Null:
                    return Color.White;
                case PowerFrom.MTr1:
                    return Color.Yellow;
                case PowerFrom.MTr2:
                    return Color.Turquoise;
                case PowerFrom.MTr3:
                    return Color.FromArgb(0,255,0);
                case PowerFrom.MTr4:
                    return Color.Violet;
                case PowerFrom.MTr5:
                    return Color.Yellow;
                case PowerFrom.MTr6:
                    return Color.Turquoise;
                case PowerFrom.MTr7:
                    return Color.FromArgb(0, 255, 0);
                default:
                    throw new ArgumentOutOfRangeException("powerFrom");
            }
            return Color.White;
        }
    }

    class CRH380APowerFromColorAdaptor : IPowerFromColorAdaptor
    {
        public Color GetColor(PowerFrom powerFrom)
        {
            switch (powerFrom)
            {
                case PowerFrom.Null:
                    return Color.White;
                case PowerFrom.MTr1:
                    return Color.Yellow;
                case PowerFrom.MTr2:
                    return Color.Blue;
                case PowerFrom.MTr3:
                    return Color.Turquoise;
                //case PowerFrom.MTr4:
                //    return Color.Violet;
                //case PowerFrom.MTr5:
                //    return Color.Yellow;
                //case PowerFrom.MTr6:
                //    return Color.Blue;
                //case PowerFrom.MTr7:
                //    return Color.Turquoise;
                default:
                    throw new ArgumentOutOfRangeException("powerFrom");
            }
            return Color.White;
        }
    }
}