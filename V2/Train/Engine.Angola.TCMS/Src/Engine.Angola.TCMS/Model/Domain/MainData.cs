using System.ComponentModel.Composition;
using System.Windows.Media;
using Engine.Angola.TCMS.Model.SWData;
using Microsoft.Practices.Prism.ViewModel;
using Engine.Angola.TCMS.Model.MainData;

namespace Engine.Angola.TCMS.Model.Domain
{
    [Export]
    public class MainData : NotificationObject
    {
        private float m_Tm6Corr;
        private float m_Tm5Corr;
        private float m_Tm4Corr;
        private float m_Tm3Corr;
        private float m_Tm2Corr;
        private float m_Tm1Corr;
        private float m_AltSaida;
        private float m_MotorVelo;
        private float m_CombPres;
        private float m_OleoPres;
        private float m_BateVolt;
        private float m_AguaTemp;
        private float m_AuxilCorr;
        private float m_AuxilVolt;
        private float m_AltCorr;
        private float m_AltVolt;
        private float m_Speed;
        private float m_FeedBack;
        private float m_Ventil;
        private float m_BateTemp1;
        private float m_BateTemp2;
        private Localiada m_Localiada;
        private LocaliadaColor m_LocaliadaColor;
        private CMD m_Cmd;

        public float Speed
        {
            set
            {
                if (value.Equals(m_Speed))
                {
                    return;
                }

                m_Speed = value;
                RaisePropertyChanged(() => Speed);
            }
            get { return m_Speed; }
        }

        public float AltVolt
        {
            set
            {
                if (value.Equals(m_AltVolt))
                {
                    return;
                }

                m_AltVolt = value;
                RaisePropertyChanged(() => AltVolt);
            }
            get { return m_AltVolt; }
        }

        public float AltCorr
        {
            set
            {
                if (value.Equals(m_AltCorr))
                {
                    return;
                }

                m_AltCorr = value;
                RaisePropertyChanged(() => AltCorr);
            }
            get { return m_AltCorr; }
        }

        public float AuxilVolt
        {
            set
            {
                if (value.Equals(m_AuxilVolt))
                {
                    return;
                }

                m_AuxilVolt = value;
                RaisePropertyChanged(() => AuxilVolt);
            }
            get { return m_AuxilVolt; }
        }

        public float AuxilCorr
        {
            set
            {
                if (value.Equals(m_AuxilCorr))
                {
                    return;
                }

                m_AuxilCorr = value;
                RaisePropertyChanged(() => AuxilCorr);
            }
            get { return m_AuxilCorr; }
        }

        public float AguaTemp
        {
            set
            {
                if (value.Equals(m_AguaTemp))
                {
                    return;
                }

                m_AguaTemp = value;
                RaisePropertyChanged(() => AguaTemp);
            }
            get { return m_AguaTemp; }
        }

        public float BateVolt
        {
            set
            {
                if (value.Equals(m_BateVolt))
                {
                    return;
                }

                m_BateVolt = value;
                RaisePropertyChanged(() => BateVolt);
            }
            get { return m_BateVolt; }
        }

        public float OleoPres
        {
            set
            {
                if (value.Equals(m_OleoPres))
                {
                    return;
                }

                m_OleoPres = value;
                RaisePropertyChanged(() => OleoPres);
            }
            get { return m_OleoPres; }
        }

        public float CombPres
        {
            set
            {
                if (value.Equals(m_CombPres))
                {
                    return;
                }

                m_CombPres = value;
                RaisePropertyChanged(() => CombPres);
            }
            get { return m_CombPres; }
        }

        public float MotorVelo
        {
            set
            {
                if (value.Equals(m_MotorVelo))
                {
                    return;
                }

                m_MotorVelo = value;
                RaisePropertyChanged(() => MotorVelo);
            }
            get { return m_MotorVelo; }
        }

        public float AltSaida
        {
            set
            {
                if (value.Equals(m_AltSaida))
                {
                    return;
                }

                m_AltSaida = value;
                RaisePropertyChanged(() => AltSaida);
            }
            get { return m_AltSaida; }
        }

        public float Tm1Corr
        {
            set
            {
                if (value.Equals(m_Tm1Corr))
                {
                    return;
                }

                m_Tm1Corr = value;
                RaisePropertyChanged(() => Tm1Corr);
            }
            get { return m_Tm1Corr; }
        }

        public float Tm2Corr
        {
            set
            {
                if (value.Equals(m_Tm2Corr))
                {
                    return;
                }

                m_Tm2Corr = value;
                RaisePropertyChanged(() => Tm2Corr);
            }
            get { return m_Tm2Corr; }
        }

        public float Tm3Corr
        {
            set
            {
                if (value.Equals(m_Tm3Corr))
                {
                    return;
                }

                m_Tm3Corr = value;
                RaisePropertyChanged(() => Tm3Corr);
            }
            get { return m_Tm3Corr; }
        }

        public float Tm4Corr
        {
            set
            {
                if (value.Equals(m_Tm4Corr))
                {
                    return;
                }

                m_Tm4Corr = value;
                RaisePropertyChanged(() => Tm4Corr);
            }
            get { return m_Tm4Corr; }
        }

        public float Tm5Corr
        {
            set
            {
                if (value.Equals(m_Tm5Corr))
                {
                    return;
                }

                m_Tm5Corr = value;
                RaisePropertyChanged(() => Tm5Corr);
            }
            get { return m_Tm5Corr; }
        }

        public float Tm6Corr
        {
            set
            {
                if (value.Equals(m_Tm6Corr))
                {
                    return;
                }

                m_Tm6Corr = value;
                RaisePropertyChanged(() => Tm6Corr);
            }
            get { return m_Tm6Corr; }
        }

        public float FeedBack
        {
            set
            {
                if (value.Equals(m_FeedBack))
                {
                    return;
                }

                m_FeedBack = value;
                RaisePropertyChanged(() => FeedBack);
            }
            get { return m_FeedBack; }
        }

        public float Ventil
        {
            set
            {
                if (value.Equals(m_Ventil))
                {
                    return;
                }

                m_Ventil = value;
                RaisePropertyChanged(() => Ventil);
            }
            get { return m_Ventil; }
        }

        public float BateTemp1
        {
            set
            {
                if (value.Equals(m_BateTemp1))
                {
                    return;
                }

                m_BateTemp1 = value;
                RaisePropertyChanged(() => BateTemp1);
            }
            get { return m_BateTemp1; }
        }

        public float BateTemp2
        {
            set
            {
                if (value.Equals(m_BateTemp2))
                {
                    return;
                }

                m_BateTemp2 = value;
                RaisePropertyChanged(() => BateTemp2);
            }
            get { return m_BateTemp2; }
        }

        public Localiada Localiada
        {
            get { return m_Localiada; }
            set
            {
                if (value == m_Localiada)
                {
                    return;
                }
                m_Localiada = value;
                RaisePropertyChanged(() => Localiada);
                LocaliadaColorChanged();
            }
        }

        public LocaliadaColor LocaliadaColor
        {
            get { return m_LocaliadaColor; }
            set
            {
                if (value == m_LocaliadaColor)
                {
                    return;
                }
                m_LocaliadaColor = value;
                RaisePropertyChanged(() => LocaliadaColor);
            }
        }

        private void LocaliadaColorChanged()
        {
            if (Localiada == Localiada.LenTa)
            {
                LocaliadaColor = LocaliadaColor.IndianRed;
            }
            else  if (Localiada == Localiada.Motor)
            {
                LocaliadaColor = LocaliadaColor.Lime;
            }
            else
                LocaliadaColor = LocaliadaColor.Cyan;
            //else
            //    LocaliadaColor = LocaliadaColor.Lime;
        }

        public CMD Cmd
        {
            get { return m_Cmd; }
            set
            {
                if (value == m_Cmd)
                {
                    return;
                }
                m_Cmd = value;
                RaisePropertyChanged(() => Cmd);
            }
        }
    }
}