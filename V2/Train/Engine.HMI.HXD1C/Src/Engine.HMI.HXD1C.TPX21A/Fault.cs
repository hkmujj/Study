using System;

namespace Engine.HMI.HXD1C.TPX21A
{
    /// <summary>
    /// ������
    /// ���� ���϶�Ӧ���߼�λ ���ϵȼ� ����������
    /// ���ϴ��� �������� ����ʱ�� ������ʾ��Ӧ���� ����ʱ���
    /// </summary>
    public class Fault
    {
        public enum FaultLevel //���ϵȼ�����  ��A B C D �ĸ��ȼ�
        {
            A,
            B,
            C,
            D
        }

        public FaultLevel Level { get; set; }

        public string FaultCategory { get; set; }

        public int FaultID { get; set; }

        public string FaultText { get; set; }

        public DateTime HappenedTime { get; set; }

        public DateTime EndedTime { get; set; }

        public int LogicalBit { get; set; }

        public string HelpinfoV { get; set; }

        public string HelpinfoV0 { get; set; }

        public int CaNum { get; set; }

        public string ProcedInfo { get; set; }
    }
}