using System.Drawing;
using Engine.HMI.HXD1C.TPX21A.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    internal class HX_TemperatureView : baseClass, IButtonEventListener
    {
        private readonly bool[] m_IsNumButtonDown = new bool[10]; //�ײ����ְ�ť�Ƿ���
        private readonly HxRectText[] m_StrText = new HxRectText[18]; //��ʾ����״̬�ı���
        private readonly HxRectText[] m_StrDate = new HxRectText[18]; //��ʾ����״̬��������

        private readonly string[] m_Text = new string[18]
        {
            "���1�¶�", "���2�¶�", "���3�¶�", "���4�¶�", "���5�¶�", "���6�¶�", "��������1��ȴˮ�¶�", "��������2��ȴˮ�¶�",
            "��������1�����¶�", "��������2�����¶�", "����ѹ��1��ȴ���¶�", "����ѹ��2��ȴ���¶�", "����ѹ��1��ȴˮѹ��", "����ѹ��2��ȴˮѹ��",
            "����1״̬", "����2״̬", "��������1�м�ֱ����ѹ", "��������2�м�ֱ����ѹ"
        };

        private readonly float[] m_Temperatures = new float[12]; //������ʾ�������¶�
        private readonly float[] m_Presures = new float[2]; //����ѹ����ȴˮѹ��
        private readonly float[] m_Voltages = new float[2]; //���������м�ֱ����ѹ
        private readonly bool[] m_IsOilStatus = new bool[2]; //����״̬

        public override string GetInfo()
        {
            return "�¶���ͼ";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            GlobalParam.Instance.AddButtonEventListener(this);

            InitData();

            nErrorObjectIndex = -1;

            return true;
        }

        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 5)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                if (btn <= ButtonName.B10)
                {
                    m_IsNumButtonDown[(int) (btn - 800)] = true;
                }
                ButtonDownEvent();
                return true;
            }

            return false;
        }

        public bool ResponseMouseUp(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 5)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                return true;
            }

            return false;
        }

        public override void paint(Graphics g)
        {
            GetValue();
            ButtonDownEvent();
            DrawOn(g);
            base.paint(g);
        }

        public void InitData()
        {
            //�ı���ĳ�ʼ��

            //�����ı���
            for (int i = 0; i < 18; i++)
            {
                m_StrText[i] = new HxRectText();
                m_StrText[i].SetBkColor(0, 0, 0);
                m_StrText[i].SetTextColor(255, 255, 255);

                if (i%2 == 0)
                {
                    m_StrText[i].SetTextRect(HxCommon.Recposition.X + 20, HxCommon.Recposition.Y + 50 + 43*(i/2), 250,
                        38);
                    m_StrText[i].SetTextStyle(14, FormatStyle.DirectionRightToLeft, true, "����");

                }
                else
                {
                    m_StrText[i].SetTextRect(HxCommon.Recposition.X + 510, HxCommon.Recposition.Y + 50 + 43*(i/2), 250,
                        38);
                    m_StrText[i].SetTextStyle(14, FormatStyle.DirectionLeftToRight, true, "����");

                }

                m_StrText[i].SetText(m_Text[i]);
            }

            //�����ı���
            for (int i = 0; i < 18; i++)
            {
                m_StrDate[i] = new HxRectText();
                m_StrDate[i].SetBkColor(0, 0, 0);
                m_StrDate[i].SetTextColor(255, 255, 255);
                m_StrDate[i].SetTextStyle(14, FormatStyle.Center, true, "����");

                if (i%2 == 0)
                {
                    m_StrDate[i].SetTextRect(m_StrText[i].m_RectPosition.Right + 15, m_StrText[i].m_RectPosition.Y, 100, 38);
                }
                else
                {
                    m_StrDate[i].SetTextRect(m_StrDate[i - 1].m_RectPosition.Right, m_StrDate[i - 1].m_RectPosition.Y, 100, 38);
                }

                m_StrDate[i].SetLinePen(84, 84, 84, 2);
                m_StrDate[i].SetDrawFrm(true);
            }
        }

        /// <summary>
        /// ˢ������
        /// </summary>
        public void GetValue()
        {
            //���ñ���
            HxCommon.HTitle.SetText("�¶Ƚ���");

            HxCommon.ButtonText[0].SetText("ǣ������");
            HxCommon.ButtonText[1].SetText("�¶�");
            HxCommon.ButtonText[2].SetText("����");
            HxCommon.ButtonText[3].SetText("����ϵͳ");
            HxCommon.ButtonText[4].SetText("����״̬");
            HxCommon.ButtonText[5].SetText("��������");
            HxCommon.ButtonText[6].SetText(" ");
            HxCommon.ButtonText[7].SetText("��������");
            HxCommon.ButtonText[8].SetText("���ڶ���");
            HxCommon.ButtonText[9].SetText("������");
            for (int i = 0; i < 10; i++)
            {
                if (i == 1)
                {
                    HxCommon.ButtonText[i].SetBkColor(255, 255, 0);
                    HxCommon.ButtonText[i].SetTextColor(0, 0, 0);
                }
                else
                {
                    HxCommon.ButtonText[i].SetBkColor(0, 0, 0);
                    HxCommon.ButtonText[i].SetTextColor(255, 255, 255);
                }
            }

            #region ��״̬��ϢBool ���Ķ���

            //�ײ����ְ�ť
            for (int i = 0; i < 10; i++)
            {
                m_IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i]];
            }

            //����״̬
            for (int i = 0; i < 2; i++)
            {
                m_IsOilStatus[i] = BoolList[UIObj.InBoolList[i + 10]];
            }

            #endregion

            //���¶� ����ѹ��float���Ķ���
            //�¶�
            for (int i = 0; i < 12; i++)
            {
                m_Temperatures[i] = FloatList[UIObj.InFloatList[i]];
            }

            //ˮѹ ��ѹ
            for (int i = 0; i < 2; i++)
            {
                m_Presures[i] = FloatList[UIObj.InFloatList[i + 12]];
                m_Voltages[i] = FloatList[UIObj.InFloatList[i + 14]];
            }

            //���������ı���
            //�¶�
            for (int i = 0; i < 12; i++)
            {
                m_StrDate[i].SetText(m_Temperatures[i].ToString() + "��");
            }

            //ѹ��
            for (int i = 12; i < 14; i++)
            {
                m_StrDate[i].SetText(m_Presures[i - 12].ToString() + "bar");
            }

            //����״̬
            for (int i = 14; i < 16; i++)
            {
                if (m_IsOilStatus[i - 14])
                {
                    m_StrDate[i].SetText("�쳣");
                }
                else
                {
                    m_StrDate[i].SetText("����");
                }
            }

            //��ѹ
            for (int i = 16; i < 18; i++)
            {
                m_StrDate[i].SetText(m_Presures[i - 16].ToString() + "V");
            }
        }

        public void DrawOn(Graphics g)
        {
            //�ײ�������ť����
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].OnDraw(g);
            }

            //���� �� ���� �ı�����
            for (int i = 0; i < 18; i++)
            {
                m_StrText[i].OnDraw(g);
                m_StrDate[i].OnDraw(g);
            }
        }

        /// <summary>
        /// ��ӦӲ����ť�����¼�
        /// </summary>
        public void ButtonDownEvent()
        {
            for (int i = 0; i < 10; i++)
            {
                if (m_IsNumButtonDown[i])
                {
                    switch (i)
                    {
                        case 0:
                            break;
                        case 1: //��ת���¶���ͼ
                            append_postCmd(CmdType.ChangePage, 5, 0, 0);
                            break;
                        case 2: //��ת���������
                            append_postCmd(CmdType.ChangePage, 6, 0, 0);
                            break;
                        case 3: //��ת������ϵͳ
                            append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            break;
                        case 4: //��ת��ǣ��״̬
                            append_postCmd(CmdType.ChangePage, 8, 0, 0);
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7: //��ת���������� 
                            append_postCmd(CmdType.ChangePage, 11, 0, 0);
                            break;
                        case 8: //��ת�����ڶ��� 
                            append_postCmd(CmdType.ChangePage, 12, 0, 0);
                            break;
                        case 9: //����������
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
