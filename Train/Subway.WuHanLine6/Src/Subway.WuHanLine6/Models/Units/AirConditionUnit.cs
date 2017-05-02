using System.Collections.Generic;
using Excel.Interface;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Models.States;

namespace Subway.WuHanLine6.Models.Units
{
    /// <summary>
    /// �յ�ϵͳ
    /// </summary>
    [ExcelLocation("����ӿڱ�.xls", "AirConditionUnit")]
    public class AirConditionUnit : UnitBase
    {
        private AirConditionState m_State;

        /// <summary>
        /// ״̬
        /// </summary>
        public AirConditionState State
        {
            get { return m_State; }
            set
            {
                if (value == m_State)
                {
                    return;
                }
                m_State = value;
                RaisePropertyChanged(() => State);
            }
        }

        /// <summary>
        /// �յ�����
        /// </summary>
        [ExcelField("�յ�����")]
        public string Fault { get; set; }

        /// <summary>
        /// �յ�����
        /// </summary>
        [ExcelField("�յ�����")]
        public string Warn { get; set; }

        /// <summary>
        /// ����ͨ��
        /// </summary>
        [ExcelField("����ͨ��")]
        public string EmergencyFan { get; set; }

        /// <summary>
        /// ͨ��
        /// </summary>
        [ExcelField("ͨ��")]
        public string Fan { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [ExcelField("��������")]
        public string Limit { get; set; }

        /// <summary>
        /// �յ�����
        /// </summary>
        [ExcelField("�յ�����")]
        public string Run { get; set; }

        /// <summary>
        /// �յ��Ͽ�
        /// </summary>
        [ExcelField("�յ��Ͽ�")]
        public string Off { get; set; }

        /// <summary>
        /// ��
        /// </summary>
        [ExcelField("��")]
        public int Car { get; set; }

        /// <summary>
        /// λ��
        /// </summary>
        [ExcelField("λ��")]
        public int Location { get; set; }

        /// <summary>
        /// Boolֵ�ı�
        /// </summary>
        /// <param name="args"></param>
        public override void Changed(IDictionary<int, bool> args)
        {
            args.UpdateIfContainWhereTrue(Fault, (b => State = AirConditionState.Fault));
            args.UpdateIfContainWhereTrue(Warn, (b => State = AirConditionState.Warn));
            args.UpdateIfContainWhereTrue(EmergencyFan, (b => State = AirConditionState.EmergencyFan));
            args.UpdateIfContainWhereTrue(Fan, (b => State = AirConditionState.Fan));
            args.UpdateIfContainWhereTrue(Limit, (b => State = AirConditionState.Limit));
            args.UpdateIfContainWhereTrue(Run, (b => State = AirConditionState.Run));
            args.UpdateIfContainWhereTrue(Off, b => State = AirConditionState.Off);
        }
    }
}