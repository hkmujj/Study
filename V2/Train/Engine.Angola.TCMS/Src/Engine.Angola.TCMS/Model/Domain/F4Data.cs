using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.Angola.TCMS.Model.Domain
{
    [Export]
    public class F4Data : NotificationObject
    {
        private float m_Carga;
        private float m_MotorOleoPressao;
        private float m_BateriaVoltagem;
        private float m_PropulsaoPressao;
        private float m_MotorCoolantTemp;
        private float m_ConbustivelPressao;
        private float m_AlarneSaideEstado;
        private float m_MotorVelocidade;
        private float m_MotorHaoas;
        private float m_ConbustivelConsumo;
        private float m_AtmosfericaPressuao;
        private float m_DesejadaMotorVelo;
        private float m_OleoFiltroDifPres;
        private float m_CombFiltroDifPres;
        private float m_DepoisCoolerTemp;
        private float m_EsquerdoExaustorTemp;
        private float m_DireitoExaustorTemp;
        private float m_DireitoArFiltRestr;
        private float m_EsquerdoArFiltRestr;
        private float m_CarterPressao;

        public float Carga
        {
            set
            {
                if (value.Equals(m_Carga))
                {
                    return;
                }

                m_Carga = value;
                RaisePropertyChanged(() => Carga);
            }
            get { return m_Carga; }
        }

        public float MotorOleoPressao
        {
            set
            {
                if (value.Equals(m_MotorOleoPressao))
                {
                    return;
                }

                m_MotorOleoPressao = value;
                RaisePropertyChanged(() => MotorOleoPressao);
            }
            get { return m_MotorOleoPressao; }
        }

        public float BateriaVoltagem
        {
            set
            {
                if (value.Equals(m_BateriaVoltagem))
                {
                    return;
                }

                m_BateriaVoltagem = value;
                RaisePropertyChanged(() => BateriaVoltagem);
            }
            get { return m_BateriaVoltagem; }
        }
         
        public float PropulsaoPressao
        {
            set
            {
                if (value.Equals(m_PropulsaoPressao))
                {
                    return;
                }

                m_PropulsaoPressao = value;
                RaisePropertyChanged(() => PropulsaoPressao);
            }
            get { return m_PropulsaoPressao; }
        }

        public float MotorCoolantTemp
        {
            set
            {
                if (value.Equals(m_MotorCoolantTemp))
                {
                    return;
                }

                m_MotorCoolantTemp = value;
                RaisePropertyChanged(() => MotorCoolantTemp);
            }
            get { return m_MotorCoolantTemp; }
        }

        public float ConbustivelPressao
        {
            set
            {
                if (value.Equals(m_ConbustivelPressao))
                {
                    return;
                }

                m_ConbustivelPressao = value;
                RaisePropertyChanged(() => ConbustivelPressao);
            }
            get { return m_ConbustivelPressao; }
        }

        public float AlarneSaideEstado
        {
            set
            {
                if (value.Equals(m_AlarneSaideEstado))
                {
                    return;
                }

                m_AlarneSaideEstado = value;
                RaisePropertyChanged(() => AlarneSaideEstado);
            }
            get { return m_AlarneSaideEstado; }
        }

        public float MotorVelocidade
        {
            set
            {
                if (value.Equals(m_MotorVelocidade))
                {
                    return;
                }

                m_MotorVelocidade = value;
                RaisePropertyChanged(() => MotorVelocidade);
            }
            get { return m_MotorVelocidade; }
        }

        public float MotorHaoas
        {
            set
            {
                if (value.Equals(m_MotorHaoas))
                {
                    return;
                }

                m_MotorHaoas = value;
                RaisePropertyChanged(() => MotorHaoas);
            }
            get { return m_MotorHaoas; }
        }

        public float ConbustivelConsumo
        {
            set
            {
                if (value.Equals(m_ConbustivelConsumo))
                {
                    return;
                }

                m_ConbustivelConsumo = value;
                RaisePropertyChanged(() => ConbustivelConsumo);
            }
            get { return m_ConbustivelConsumo; }
        }

        public float AtmosfericaPressuao
        {
            set
            {
                if (value.Equals(m_AtmosfericaPressuao))
                {
                    return;
                }

                m_AtmosfericaPressuao = value;
                RaisePropertyChanged(() => AtmosfericaPressuao);
            }
            get { return m_AtmosfericaPressuao; }
        }

        public float DesejadaMotorVelo
        {
            set
            {
                if (value.Equals(m_DesejadaMotorVelo))
                {
                    return;
                }

                m_DesejadaMotorVelo = value;
                RaisePropertyChanged(() => DesejadaMotorVelo);
            }
            get { return m_DesejadaMotorVelo; }
        }

        public float OleoFiltroDifPres
        {
            set
            {
                if (value.Equals(m_OleoFiltroDifPres))
                {
                    return;
                }

                m_OleoFiltroDifPres = value;
                RaisePropertyChanged(() => OleoFiltroDifPres);
            }
            get { return m_OleoFiltroDifPres; }
        }

        public float CombFiltroDifPres
        {
            set
            {
                if (value.Equals(m_CombFiltroDifPres))
                {
                    return;
                }

                m_CombFiltroDifPres = value;
                RaisePropertyChanged(() => CombFiltroDifPres);
            }
            get { return m_CombFiltroDifPres; }
        }

        public float DepoisCoolerTemp
        {
            set
            {
                if (value.Equals(m_DepoisCoolerTemp))
                {
                    return;
                }

                m_DepoisCoolerTemp = value;
                RaisePropertyChanged(() => DepoisCoolerTemp);
            }
            get { return m_DepoisCoolerTemp; }
        }

        public float EsquerdoExaustorTemp
        {
            set
            {
                if (value.Equals(m_EsquerdoExaustorTemp))
                {
                    return;
                }

                m_EsquerdoExaustorTemp = value;
                RaisePropertyChanged(() => EsquerdoExaustorTemp);
            }
            get { return m_EsquerdoExaustorTemp; }
        }

        public float DireitoExaustorTemp
        {
            set
            {
                if (value.Equals(m_DireitoExaustorTemp))
                {
                    return;
                }

                m_DireitoExaustorTemp = value;
                RaisePropertyChanged(() => DireitoExaustorTemp);
            }
            get { return m_DireitoExaustorTemp; }
        }

        public float DireitoArFiltRestr
        {
            set
            {
                if (value.Equals(m_DireitoArFiltRestr))
                {
                    return;
                }

                m_DireitoArFiltRestr = value;
                RaisePropertyChanged(() => DireitoArFiltRestr);
            }
            get { return m_DireitoArFiltRestr; }
        }

        public float EsquerdoArFiltRestr
        {
            set
            {
                if (value.Equals(m_EsquerdoArFiltRestr))
                {
                    return;
                }

                m_EsquerdoArFiltRestr = value;
                RaisePropertyChanged(() => EsquerdoArFiltRestr);
            }
            get { return m_EsquerdoArFiltRestr; }
        }

        public float CarterPressao
        {
            set
            {
                if (value.Equals(m_CarterPressao))
                {
                    return;
                }

                m_CarterPressao = value;
                RaisePropertyChanged(() => CarterPressao);
            }
            get { return m_CarterPressao; }
        }


    }
}
