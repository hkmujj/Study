using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Model.Domain.TrainState.TAX.Detail;

namespace Engine.TAX2.SS7C.Model.Domain.TrainState.TAX
{
    [Export]
    public class CheckWalkModel : TAXModelBase
    {
        private float m_EnviormentTemper2;
        private float m_EnviormentTemper1;
        private float m_MaxTemper;
        private int m_MaxTemperAixe;
        private int m_SensorCount;
        private bool m_IsEffective;

        /// <summary>
        /// 数据是否有效
        /// </summary>
        public bool IsEffective
        {
            get { return m_IsEffective; }
            set
            {
                if (value == m_IsEffective)
                {
                    return;
                }

                m_IsEffective = value;
                RaisePropertyChanged(() => IsEffective);
            }
        }
        public int SensorCount
        {
            get { return m_SensorCount; }
            set
            {
                if (value == m_SensorCount)
                {
                    return;
                }

                m_SensorCount = value;
                RaisePropertyChanged(() => SensorCount);
            }
        }

        public int MaxTemperAixe
        {
            get { return m_MaxTemperAixe; }
            set
            {
                if (value == m_MaxTemperAixe)
                {
                    return;
                }

                m_MaxTemperAixe = value;
                RaisePropertyChanged(() => MaxTemperAixe);
            }
        }

        public float MaxTemper
        {
            get { return m_MaxTemper; }
            set
            {
                if (value.Equals(m_MaxTemper))
                {
                    return;
                }

                m_MaxTemper = value;
                RaisePropertyChanged(() => MaxTemper);
            }
        }

        public float EnviormentTemper1
        {
            get { return m_EnviormentTemper1; }
            set
            {
                if (value.Equals(m_EnviormentTemper1))
                {
                    return;
                }

                m_EnviormentTemper1 = value;
                RaisePropertyChanged(() => EnviormentTemper1);
            }
        }

        public float EnviormentTemper2
        {
            get { return m_EnviormentTemper2; }
            set
            {
                if (value.Equals(m_EnviormentTemper2))
                {
                    return;
                }

                m_EnviormentTemper2 = value;
                RaisePropertyChanged(() => EnviormentTemper2);
            }
        }

        public Lazy<ReadOnlyCollection<AixeTemperItem>> AixeTemperItemCollection { set; get; }
    }
}