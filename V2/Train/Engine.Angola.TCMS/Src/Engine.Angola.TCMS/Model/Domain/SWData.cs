using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Engine.Angola.TCMS.Model.SWData;

namespace Engine.Angola.TCMS.Model.Domain
{
    [Export]
    public class SWData : NotificationObject
    {
        private Color1 m_Color1;
        private Color2 m_Color2;
        private Color3 m_Color3;
        private Color4 m_Color4;
        private Color5 m_Color5;
        private Color6 m_Color6;
        private Color7 m_Color7;
        private Color8 m_Color8;
        private Color9 m_Color9;
        private Color10 m_Color10;
        private Color11 m_Color11;
        private Color12 m_Color12;
        private ThacaoCmd m_ThacaoCmd;
        private DynPreioCmd m_DynPreioCmd;
        private PropCargaCmd m_PropCargaCmd;
        private FieldWeakening m_FieldWeakening;
        private EfcNo m_EfcNo;
        private MotorFbkIn m_MotorFbkIn;
        private BrakeFbkIn m_BrakeFbkIn;
        private LocospeedCont m_LocospeedCont;
        private VigilanciaSwt m_VigilanciaSwt;
        private AirFbk m_AirFbk;
        private AirStartIn m_AirStartIn;
        private AirStartDelay m_AirStartDelay;

        public ThacaoCmd ThacaoCmd
        {
            get { return m_ThacaoCmd; }
            set
            {
                if (value == m_ThacaoCmd)
                {
                    return;
                }
                m_ThacaoCmd = value;
                RaisePropertyChanged(() => ThacaoCmd);
                Color1Changed();
            }
        }

        public DynPreioCmd DynPreioCmd
        {
            get { return m_DynPreioCmd; }
            set
            {
                if (value == m_DynPreioCmd)
                {
                    return;
                }
                m_DynPreioCmd = value;
                RaisePropertyChanged(() => DynPreioCmd);
                Color2Changed();
            }
        }

        public PropCargaCmd PropCargaCmd
        {
            get { return m_PropCargaCmd; }
            set
            {
                if (value == m_PropCargaCmd)
                {
                    return;
                }
                m_PropCargaCmd = value;
                RaisePropertyChanged(() => PropCargaCmd);
                Color3Changed();
            }
        }

        public FieldWeakening FieldWeakening
        {
            get { return m_FieldWeakening; }
            set
            {
                if (value == m_FieldWeakening)
                {
                    return;
                }
                m_FieldWeakening = value;
                RaisePropertyChanged(() => FieldWeakening);
                Color4Changed();
            }
        }

        public EfcNo EfcNo
        {
            get { return m_EfcNo; }
            set
            {
                if (value == m_EfcNo)
                {
                    return;
                }
                m_EfcNo = value;
                RaisePropertyChanged(() => EfcNo);
                Color5Changed();
            }
        }

        public MotorFbkIn MotorFbkIn
        {
            get { return m_MotorFbkIn; }
            set
            {
                if (value == m_MotorFbkIn)
                {
                    return;
                }
                m_MotorFbkIn = value;
                RaisePropertyChanged(() => MotorFbkIn);
                Color6Changed();
            }
        }

        public BrakeFbkIn BrakeFbkIn
        {
            get { return m_BrakeFbkIn; }
            set
            {
                if (value == m_BrakeFbkIn)
                {
                    return;
                }
                m_BrakeFbkIn = value;
                RaisePropertyChanged(() => BrakeFbkIn);
                Color7Changed();
            }
        }

        public LocospeedCont LocospeedCont
        {
            get { return m_LocospeedCont; }
            set
            {
                if (value == m_LocospeedCont)
                {
                    return;
                }
                m_LocospeedCont = value;
                RaisePropertyChanged(() => LocospeedCont);
                Color8Changed();
            }
        }

        public VigilanciaSwt VigilanciaSwt
        {
            get { return m_VigilanciaSwt; }
            set
            {
                if (value == m_VigilanciaSwt)
                {
                    return;
                }
                m_VigilanciaSwt = value;
                RaisePropertyChanged(() => VigilanciaSwt);
                Color9Changed();
            }
        }

        public AirFbk AirFbk
        {
            get { return m_AirFbk; }
            set
            {
                if (value == m_AirFbk)
                {
                    return;
                }
                m_AirFbk = value;
                RaisePropertyChanged(() => AirFbk);
                Color10Changed();
            }
        }

        public AirStartIn AirStartIn
        {
            get { return m_AirStartIn; }
            set
            {
                if (value == m_AirStartIn)
                {
                    return;
                }
                m_AirStartIn = value;
                RaisePropertyChanged(() => AirStartIn);
                Color11Changed();
            }
        }

        public AirStartDelay AirStartDelay
        {
            get { return m_AirStartDelay; }
            set
            {
                if (value == m_AirStartDelay)
                {
                    return;
                }
                m_AirStartDelay = value;
                RaisePropertyChanged(() => AirStartDelay);
                Color12Changed();
            }
        }

        public Color1 Color1
        {
            get { return m_Color1; }
            set
            {
                if (value == m_Color1)
                {
                    return;
                }
                m_Color1 = value;
                RaisePropertyChanged(() => Color1);
               
            }
        }

        private void Color1Changed()
        {
            if (ThacaoCmd == ThacaoCmd.ON)
            {
                Color1 = Color1.Green;
            }
            else
                Color1 = Color1.Yellow;
        }

        public Color2 Color2
        {
            get { return m_Color2; }
            set
            {
                if (value == m_Color2)
                {
                    return;
                }
                m_Color2 = value;
                RaisePropertyChanged(() => Color2);

            }
        }

        private void Color2Changed()
        {
            if (DynPreioCmd == DynPreioCmd.ON)
            {
                Color2 = Color2.Green;
            }
            else
                Color2 = Color2.Yellow;
        }

        public Color3 Color3
        {
            get { return m_Color3; }
            set
            {
                if (value == m_Color3)
                {
                    return;
                }
                m_Color3 = value;
                RaisePropertyChanged(() => Color3);

            }
        }

        private void Color3Changed()
        {
            if (PropCargaCmd == PropCargaCmd.ON)
            {
                Color3 = Color3.Green;
            }
            else
                Color3 = Color3.Yellow;
        }

        public Color4 Color4
        {
            get { return m_Color4; }
            set
            {
                if (value == m_Color4)
                {
                    return;
                }
                m_Color4 = value;
                RaisePropertyChanged(() => Color4);

            }
        }

        private void Color4Changed()
        {
            if (FieldWeakening == FieldWeakening.ON)
            {
                Color4 = Color4.Green;
            }
            else
                Color4 = Color4.Yellow;
        }

        public Color5 Color5
        {
            get { return m_Color5; }
            set
            {
                if (value == m_Color5)
                {
                    return;
                }
                m_Color5 = value;
                RaisePropertyChanged(() => Color5);

            }
        }

        private void Color5Changed()
        {
            if (EfcNo == EfcNo.ON)
            {
                Color5 = Color5.Green;
            }
            else
                Color5 = Color5.Yellow;
        }

        public Color6 Color6
        {
            get { return m_Color6; }
            set
            {
                if (value == m_Color6)
                {
                    return;
                }
                m_Color6 = value;
                RaisePropertyChanged(() => Color6);

            }
        }

        private void Color6Changed()
        {
            if (MotorFbkIn == MotorFbkIn.ON)
            {
                Color6 = Color6.Green;
            }
            else
                Color6 = Color6.Yellow;
        }

        public Color7 Color7
        {
            get { return m_Color7; }
            set
            {
                if (value == m_Color7)
                {
                    return;
                }
                m_Color7 = value;
                RaisePropertyChanged(() => Color7);

            }
        }

        private void Color7Changed()
        {
            if (BrakeFbkIn == BrakeFbkIn.ON)
            {
                Color7 = Color7.Green;
            }
            else
                Color7 = Color7.Yellow;
        }

        public Color8 Color8
        {
            get { return m_Color8; }
            set
            {
                if (value == m_Color8)
                {
                    return;
                }
                m_Color8 = value;
                RaisePropertyChanged(() => Color8);

            }
        }

        private void Color8Changed()
        {
            if (LocospeedCont == LocospeedCont.ON)
            {
                Color8 = Color8.Green;
            }
            else
                Color8 = Color8.Yellow;
        }

        public Color9 Color9
        {
            get { return m_Color9; }
            set
            {
                if (value == m_Color9)
                {
                    return;
                }
                m_Color9 = value;
                RaisePropertyChanged(() => Color9);

            }
        }

        private void Color9Changed()
        {
            if (VigilanciaSwt == VigilanciaSwt.ON)
            {
                Color9 = Color9.Green;
            }
            else
                Color9 = Color9.Yellow;
        }

        public Color10 Color10
        {
            get { return m_Color10; }
            set
            {
                if (value == m_Color10)
                {
                    return;
                }
                m_Color10 = value;
                RaisePropertyChanged(() => Color10);

            }
        }

        private void Color10Changed()
        {
            if (AirFbk == AirFbk.ON)
            {
                Color10 = Color10.Green;
            }
            else
                Color10 = Color10.Yellow;
        }

        public Color11 Color11
        {
            get { return m_Color11; }
            set
            {
                if (value == m_Color11)
                {
                    return;
                }
                m_Color11 = value;
                RaisePropertyChanged(() => Color11);
            }
        }

        private void Color11Changed()
        {
            if (AirStartIn == AirStartIn.ON)
            {
                Color11 = Color11.Green;
            }
            else
                Color11 = Color11.Yellow;
        }

        public Color12 Color12
        {
            get { return m_Color12; }
            set
            {
                if (value == m_Color12)
                {
                    return;
                }
                m_Color12 = value;
                RaisePropertyChanged(() => Color12);

            }
        }

        private void Color12Changed()
        {
            if (AirStartDelay == AirStartDelay.ON)
            {
                Color12 = Color12.Green;
            }
            else
                Color12 = Color12.Yellow;
        }
    }
}
