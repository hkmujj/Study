using System;
using CommonUtil.Util;

namespace Motor.HMI.CRH1A.Alarm.Fault
{
    [Serializable]
    public class ExceptionData
    {
        public FaultSenderType SenderType { set; get; }
        
        /// <summary>
        /// ״̬�仯 
        /// </summary>
        [NonSerialized]
        public EventHandler StateChanged;

        private FaultState m_State;

        /// <summary>
        /// ���ϵ��߼�λ
        /// </summary>
        public int ExLogNo { get; set; }
        /// <summary>
        /// ����������Ԫ
        /// </summary>            
        public int ExUnit { get; set; }
        /// <summary>
        /// ���ϳ����
        /// </summary>
        public int ExCarId { get; set; }

        /// <summary>
        /// ���ϳ����������Ԫ
        /// </summary>
        public int ExCarUnit { get; set; }

        /// <summary>
        /// ���ϴ���(���)
        /// </summary>
        public int ExId { get; set; }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string ExText { get; set; }
        /// <summary>
        /// ��������(�ȼ�)
        /// </summary>
        public FaultType ExType { get; set; }

        public string ExTypeInfo
        {
            get
            {
                switch (ExType)
                {
                    case FaultType.OperError:
                        return "��Ϣ";
                    case FaultType.A:
                        return "A - ����";
                    case FaultType.E:
                        return "��Ϣ";
                    case FaultType.B:
                        return "B - ����";
                    default:
                        return "����";
                }
            }
        }

        /// <summary>
        /// ������ʾ/�������ݵı��
        /// </summary>
        public int ExSuggestId { get; set; }
        /// <summary>
        /// ���Ϸ���������
        /// </summary>
        public DateTime Startdate { get; set; }
        /// <summary>
        /// ���Ͻ���ʱ��
        /// </summary>
        public DateTime Enddate { get; set; }


        /// <summary>
        /// ������������
        /// </summary>
        public int PartId { get; set; }


        /// <summary>
        /// ����״̬
        /// </summary>
        public FaultState State
        {
            set
            {
                m_State = value;
                HandleUtil.OnHandle(StateChanged, this, null);
            }
            get { return m_State; }
        }


        /// <summary>
        /// �Ƿ�ȷ��(ture��ʾ��ȷ�� false��ʾδȷ��)
        /// </summary>
        public bool IsConfirm
        {
            set
            {
                if (value)
                {
                    State |= FaultState.Sure;
                }
                else
                {
                    State &= ~FaultState.Sure;
                }
            }
            get { return (State & FaultState.Sure) == FaultState.Sure; }
        }

        /// <summary>
        /// �Ƿ��ѽ��
        /// </summary>
        public bool IsFixed
        {
            set
            {
                if (value)
                {
                    State |= FaultState.Fixed;
                }
                else
                {
                    State &= ~FaultState.Fixed;
                }
            }
            get { return (State & FaultState.Fixed) == FaultState.Fixed; }
        }

        public ExceptionData()
        {
            State = FaultState.New;
            IsConfirm = false;

        }

    }
}