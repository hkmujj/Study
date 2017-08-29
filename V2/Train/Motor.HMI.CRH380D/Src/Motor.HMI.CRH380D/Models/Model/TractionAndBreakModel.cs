using System.ComponentModel.Composition;
using Motor.HMI.CRH380D.Controller;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class TractionAndBreakModel : ModelBase
    {
        private float m_TractionValue0;
        private float m_TractionValue7;
        private float m_TractionValue6;
        private float m_TractionValue5;
        private float m_TractionValue4;
        private float m_TractionValue3;
        private float m_TractionValue2;
        private float m_TractionValue1;
        private float m_BreakValue0;
        private float m_BreakValue7;
        private float m_BreakValue6;
        private float m_BreakValue5;
        private float m_BreakValue4;
        private float m_BreakValue3;
        private float m_BreakValue2;
        private float m_BreakValue1;
        private float m_Acc;

        [ImportingConstructor]
        public TractionAndBreakModel(TractionAndBreakController tractionAndBreakController)
        {

            TractionAndBreakController = tractionAndBreakController;
            TractionAndBreakController.ViewModel = this;
            TractionAndBreakController.Initalize();
        }

        /// <summary>
        /// 控制
        /// </summary>
        public TractionAndBreakController TractionAndBreakController { get; set; }
        
       

        /// <summary>
        /// 车0牵引力
        /// </summary>
        public float TractionValue0
        {
            get { return m_TractionValue0; }
            set {
                if (value == m_TractionValue0)
                {
                    return;
                }
                m_TractionValue0 = value;
                RaisePropertyChanged(() => TractionValue0);
            }
        }

        /// <summary>
        /// 车1牵引力
        /// </summary>
        public float TractionValue1
        {
            get { return m_TractionValue1; }
            set
            {
                if (value == m_TractionValue1)
                {
                    return;
                }
                m_TractionValue1 = value;
                RaisePropertyChanged(() => TractionValue1);
            }
        }

        /// <summary>
        /// 车2牵引力
        /// </summary>
        public float TractionValue2
        {
            get { return m_TractionValue2; }
            set
            {
                if (value == m_TractionValue2)
                {
                    return;
                }
                m_TractionValue2 = value;
                RaisePropertyChanged(() => TractionValue2);
            }
        }

        /// <summary>
        /// 车3牵引力
        /// </summary>
        public float TractionValue3
        {
            get { return m_TractionValue3; }
            set
            {
                if (value == m_TractionValue3)
                {
                    return;
                }
                m_TractionValue3 = value;
                RaisePropertyChanged(() => TractionValue3);
            }
        }

        /// <summary>
        /// 车4牵引力
        /// </summary>
        public float TractionValue4
        {
            get { return m_TractionValue4; }
            set
            {
                if (value == m_TractionValue4)
                {
                    return;
                }
                m_TractionValue4 = value;
                RaisePropertyChanged(() => TractionValue4);
            }
        }

        /// <summary>
        /// 车5牵引力
        /// </summary>
        public float TractionValue5
        {
            get { return m_TractionValue5; }
            set
            {
                if (value == m_TractionValue5)
                {
                    return;
                }
                m_TractionValue5 = value;
                RaisePropertyChanged(() => TractionValue5);
            }
        }

        /// <summary>
        /// 车6牵引力
        /// </summary>
        public float TractionValue6
        {
            get { return m_TractionValue6; }
            set
            {
                if (value == m_TractionValue6)
                {
                    return;
                }
                m_TractionValue6 = value;
                RaisePropertyChanged(() => TractionValue6);
            }
        }

        /// <summary>
        /// 车7牵引力
        /// </summary>
        public float TractionValue7
        {
            get { return m_TractionValue7; }
            set
            {
                if (value == m_TractionValue7)
                {
                    return;
                }
                m_TractionValue7 = value;
                RaisePropertyChanged(() => TractionValue7);
            }
        }

        /// <summary>
        /// 车0制动力
        /// </summary>
        public float BreakValue0
        {
            get { return m_BreakValue0; }
            set
            {
                if (value == m_BreakValue0)
                {
                    return;
                }
                m_BreakValue0 = value;
                RaisePropertyChanged(() => BreakValue0);
            }
        }

        /// <summary>
        /// 车1制动力
        /// </summary>
        public float BreakValue1
        {
            get { return m_BreakValue1; }
            set
            {
                if (value == m_BreakValue1)
                {
                    return;
                }
                m_BreakValue1 = value;
                RaisePropertyChanged(() => BreakValue1);
            }
        }

        /// <summary>
        /// 车2制动力
        /// </summary>
        public float BreakValue2
        {
            get { return m_BreakValue2; }
            set
            {
                if (value == m_BreakValue2)
                {
                    return;
                }
                m_BreakValue2 = value;
                RaisePropertyChanged(() => BreakValue2);
            }
        }

        /// <summary>
        /// 车3制动力
        /// </summary>
        public float BreakValue3
        {
            get { return m_BreakValue3; }
            set
            {
                if (value == m_BreakValue3)
                {
                    return;
                }
                m_BreakValue3 = value;
                RaisePropertyChanged(() => BreakValue3);
            }
        }

        /// <summary>
        /// 车4制动力
        /// </summary>
        public float BreakValue4
        {
            get { return m_BreakValue4; }
            set
            {
                if (value == m_BreakValue4)
                {
                    return;
                }
                m_BreakValue4 = value;
                RaisePropertyChanged(() => BreakValue4);
            }
        }

        /// <summary>
        /// 车5制动力
        /// </summary>
        public float BreakValue5
        {
            get { return m_BreakValue5; }
            set
            {
                if (value == m_BreakValue5)
                {
                    return;
                }
                m_BreakValue5 = value;
                RaisePropertyChanged(() => BreakValue5);
            }
        }

        /// <summary>
        /// 车6制动力
        /// </summary>
        public float BreakValue6
        {
            get { return m_BreakValue6; }
            set
            {
                if (value == m_BreakValue6)
                {
                    return;
                }
                m_BreakValue6 = value;
                RaisePropertyChanged(() => BreakValue6);
            }
        }

        /// <summary>
        /// 车7制动力
        /// </summary>
        public float BreakValue7
        {
            get { return m_BreakValue7; }
            set
            {
                if (value == m_BreakValue7)
                {
                    return;
                }
                m_BreakValue7 = value;
                RaisePropertyChanged(() => BreakValue7);
            }
        }

        /// <summary>
        /// 平均加/减速度
        /// </summary>
        public float Acc
        {
            get { return m_Acc; }
            set
            {
                if (value == m_Acc)
                {
                    return;
                }
                m_Acc = value;
                RaisePropertyChanged(() => Acc);
            }
        }
    }
}