using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.�ײ㹲��;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.�ƶ�����.TestItems
{
    public class TestOfEmergenceUB : BrakeTestControl
    {
        public TestOfEmergenceUB(BrakeTestType brakeTestType, ControlBrakeTest srcObj) : base(brakeTestType, srcObj)
        {
            BrakeTestResult = new BrakeTestResult("�����ƶ�UB����");
        }

        public override string TitleName
        {
            get { return "�ƶ�����; �����ƶ�UB����"; }
        }

        public override void ResetStates()
        {
            base.ResetStates();

            m_Content0 = "�����ƶ�UB����:\n\n";
            m_Content1 = "-��\"��ʼ����\"�����������ƶ�����";
        }

        protected override void ResetWhenStartTest()
        {
            //base.ResetWhenStartTest();

            //TODO base.ResetWhenStartTest(); ���ͽ����ƶ�UB����
            //append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub�����ƶ����鿪ʼ), 1, 0);
        }

        protected override void UpdateContents()
        {
            //TODO  �滻���нӿں����� ��
            switch (m_Step)
            {
                case 0:
                    m_Content1 = "-���ƶ��ֱ����ڽ����ƶ�λ";
                    if (!GetInBoolValue(InBoolKeys.Inb�ƶ�����1369)){ m_Step = 1;}
                    m_TheTimeCounter.ResetCounter();
                    break;
                case 1:
                    m_Content1 = "-���BP��ѹ���Ƿ���200KPa����\n-����Ƿ������ƶ���ʩ��";
                    if (GetInFloatValue(InFloatKeys.Inf�г���_��) <= 200 && GetInBoolValue(InBoolKeys.Inb�ƶ�����1373))
                    {
                        m_Step = 2;
                    }
                    break;
                case 2:
                    m_Content1 = "-���BP��ѹ���Ƿ���200KPa����\n-����Ƿ������ƶ���ʩ��\n-���ƶ��ֱ����ڹ����λ";
                    if (GetInFloatValue(InFloatKeys.Inf�г���_��) <= 200 && GetInBoolValue(InBoolKeys.Inb�ƶ�����1373) &&
                        GetInBoolValue(InBoolKeys.Inb�ƶ�����1369))
                    {
                        m_Step = 3;
                    }
                    break;
                case 3:
                    m_Content1 = "-���BP��ѹ���Ƿ�������600KPa����\n-����Ƿ������ƶ��ѻ���";
                    if (GetInFloatValue(InFloatKeys.Inf�г���_��) >= 600 && GetInBoolValue(InBoolKeys.Inb�ƶ�����1371))
                    {
                        m_Step = 4;
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub�����ƶ��������), 1, 0); //�����ƶ��������
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub���Խ��5105), 1, 0);
                        BrakeTestTime[1] = TestOverTime(); //���������ƶ�����ʱ��
                        BrakeTestResult.TestOver(CurrentTime);
                    }
                    break;
                case 4:
                    m_Content1 = "-���BP��ѹ���Ƿ�������600KPa����\n-����Ƿ������ƶ��ѻ���\n\n\n\n�����ƶ��������\n-��\"�ƶ�����\"������";
                    break;
            }
        }
    }
}