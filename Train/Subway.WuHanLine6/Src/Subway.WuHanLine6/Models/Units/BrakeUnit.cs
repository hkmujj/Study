using System.Collections.Generic;
using Excel.Interface;
using Subway.WuHanLine6.Extention;
using Subway.WuHanLine6.Models.States;

namespace Subway.WuHanLine6.Models.Units
{
    /// <summary>
    /// �ƶ�ϵͳ
    /// </summary>
    [ExcelLocation("����ӿڱ�.xls", "BrakeUnit")]
    public class BrakeUnit : UnitBase
    {
        private BrakeState m_State;

        /// <summary>
        /// ��ǰ�ƶ�״̬
        /// </summary>
        public BrakeState State
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
        /// ͣ���ƶ�ʩ��
        /// </summary>
        [ExcelField("ͣ���ƶ�ʩ��")]
        public string Parking { get; set; }

        /// <summary>
        /// �ƶ��г�
        /// </summary>
        [ExcelField("�ƶ��г�")]
        public string Cut { get; set; }

        /// <summary>
        /// �ƶ��Լ켤��
        /// </summary>
        [ExcelField("�ƶ��Լ켤��")]
        public string AutoCheck { get; set; }

        /// <summary>
        /// �ƶ�����
        /// </summary>
        [ExcelField("�ƶ�����")]
        public string Fault { get; set; }

        /// <summary>
        /// �ƶ�����
        /// </summary>
        [ExcelField("�ƶ�����")]
        public string Warn { get; set; }

        /// <summary>
        /// �����ƶ�ʩ��
        /// </summary>
        [ExcelField("�����ƶ�ʩ��")]
        public string Infliction { get; set; }

        /// <summary>
        /// �����ƶ�����
        /// </summary>
        [ExcelField("�����ƶ�����")]
        public string Remission { get; set; }

        /// <summary>
        /// λ��
        /// </summary>
        [ExcelField("λ��")]
        public int Location { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [ExcelField("����")]
        public int Car { get; set; }

        /// <summary>
        /// Boolֵ�ı�
        /// </summary>
        /// <param name="args"></param>
        public override void Changed(IDictionary<int, bool> args)
        {
            args.UpdateIfContainWhereTrue(Parking, (b => State = BrakeState.Parking));
            args.UpdateIfContainWhereTrue(Cut, (b => State = BrakeState.Cut));
            args.UpdateIfContainWhereTrue(AutoCheck, (b => State = BrakeState.AutoCheck));
            args.UpdateIfContainWhereTrue(Fault, b => State = BrakeState.Fault);
            args.UpdateIfContainWhereTrue(Warn, (b => State = BrakeState.Warn));
            args.UpdateIfContainWhereTrue(Infliction, b => State = BrakeState.Infliction);
            args.UpdateIfContainWhereTrue(Remission, (b => State = BrakeState.Remission));
        }
    }
}