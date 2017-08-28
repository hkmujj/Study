using System.Collections.Generic;
using System.Drawing;
using Engine.HMI.HXD1C.TPX21A.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    internal class HX_TotalFault : baseClass, IButtonEventListener
    {
        private readonly bool[] m_IsNumButtonDown = new bool[10]; //�ײ����ְ�ť�Ƿ���
        private readonly bool[] m_IsPageButtonDown = new bool[2]; //�Ҳ෭ҳ��ť
        private int m_Page = 0; //��ǰҳ

        private readonly HxRectText[] m_LevelText = new HxRectText[16]; // ��ʾ���������ı���
        private readonly HxRectText[] m_TrainNoText = new HxRectText[16]; //��ʾ�г���ŵ��ı���
        private readonly HxRectText[] m_FaultNoText = new HxRectText[16]; //��ʾ���ϴ�����ı���
        private readonly HxRectText[] m_InfoText = new HxRectText[16]; //��ʾ�������ݵ��ı���
        private readonly HxRectText[] m_HapenedTimeText = new HxRectText[16]; //��ʾ���Ϸ���ʱ����ı���
        private readonly HxRectText[] m_EndedTimeText = new HxRectText[16]; //��ʾ���Ͻ���ʱ����ı��� 

        private readonly SolidBrush m_GrayBrush = new SolidBrush(Color.FromArgb(165, 162, 165));
        private Rectangle m_TitleRect; //������

        private readonly SortedList<int, Image> m_ImageList = new SortedList<int, Image>(); //��������Ҫ�Ľ���Ԫ��

        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 14)
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
            if (GlobalParam.Instance.CurrentViewId == 14)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                return true;
            }

            return false;
        }

        public override string GetInfo()
        {
            return "���й�����Ϣ";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            GlobalParam.Instance.AddButtonEventListener(this);

            InitData();

            //����ͼƬ
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Image image = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
                m_ImageList.Add(i, image);
            }
            nErrorObjectIndex = -1;

            return true;
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

            //�������ı����ʼ����
            m_TitleRect = new Rectangle(HxCommon.Recposition.X, HxCommon.Recposition.Y + 32, 800, 28);
            //���ı���ĳ�ʼ��
            for (int i = 0; i < 16; i++)
            {
                m_LevelText[i] = new HxRectText();
                m_LevelText[i].SetBkColor(255, 255, 0);
                m_LevelText[i].SetTextColor(255, 0, 0);
                m_LevelText[i].SetTextStyle(12, FormatStyle.Center, true, "����");
                m_LevelText[i].SetTextRect(HxCommon.Recposition.X + 2, HxCommon.Recposition.Y + 63 + 26*i, 32, 26);

                m_TrainNoText[i] = new HxRectText();
                m_TrainNoText[i].SetBkColor(165, 162, 165);
                m_TrainNoText[i].SetTextColor(0, 0, 0);
                m_TrainNoText[i].SetTextStyle(12, FormatStyle.Center, true, "����");
                m_TrainNoText[i].SetTextRect(m_LevelText[i].m_RectPosition.Right, m_LevelText[i].m_RectPosition.Y, 43, 26);

                m_FaultNoText[i] = new HxRectText();
                m_FaultNoText[i].SetBkColor(0, 0, 255);
                m_FaultNoText[i].SetTextColor(255, 255, 255);
                m_FaultNoText[i].SetTextStyle(12, FormatStyle.Center, true, "����");
                m_FaultNoText[i].SetTextRect(m_TrainNoText[i].m_RectPosition.Right, m_TrainNoText[i].m_RectPosition.Y, 50, 26);

                m_InfoText[i] = new HxRectText();
                m_InfoText[i].SetBkColor(255, 255, 0);
                m_InfoText[i].SetTextColor(0, 0, 0);
                m_InfoText[i].SetTextStyle(12, FormatStyle.DirectionLeftToRight, true, "����");
                m_InfoText[i].SetTextRect(m_FaultNoText[i].m_RectPosition.Right, m_FaultNoText[i].m_RectPosition.Y, 345, 26);

                m_HapenedTimeText[i] = new HxRectText();
                m_HapenedTimeText[i].SetBkColor(255, 255, 0);
                m_HapenedTimeText[i].SetTextColor(0, 0, 0);
                m_HapenedTimeText[i].SetTextStyle(12, FormatStyle.Center, true, "����");
                m_HapenedTimeText[i].SetTextRect(m_InfoText[i].m_RectPosition.Right, m_InfoText[i].m_RectPosition.Y, 150, 26);

                m_EndedTimeText[i] = new HxRectText();
                m_EndedTimeText[i].SetBkColor(255, 255, 0);
                m_EndedTimeText[i].SetTextColor(0, 0, 0);
                m_EndedTimeText[i].SetTextStyle(12, FormatStyle.Center, true, "����");
                m_EndedTimeText[i].SetTextRect(m_HapenedTimeText[i].m_RectPosition.Right, m_HapenedTimeText[i].m_RectPosition.Y,
                    150, 26);
            }
        }

        public void GetValue()
        {
            //���ñ���
            HxCommon.HTitle.SetText("���й���");

            HxCommon.ButtonText[0].SetText("");
            HxCommon.ButtonText[1].SetText("");
            HxCommon.ButtonText[2].SetText("");
            HxCommon.ButtonText[3].SetText("");
            HxCommon.ButtonText[4].SetText("");
            HxCommon.ButtonText[5].SetText("");
            HxCommon.ButtonText[6].SetText("��ǰ����");
            HxCommon.ButtonText[7].SetText("���й���");
            HxCommon.ButtonText[8].SetText("��ʷ����");
            HxCommon.ButtonText[9].SetText("������");

            for (int i = 0; i < 10; i++)
            {
                if (i == 7)
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

            //��ť״̬����
            for (int i = 0; i < 10; i++)
            {
                m_IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i]];
            }

            //������ť
            for (int i = 0; i < 2; i++)
            {
                m_IsPageButtonDown[i] = BoolList[UIObj.InBoolList[i + 10]];
            }

            for (int i = 0; i < 16; i++)
            {
                if (i + m_Page*16 < HXFaultCommom.m_TotalFaults.Count)
                {
                    switch ((int) HXFaultCommom.m_TotalFaults[i + m_Page*16].Level)
                    {
                        case 0:
                            m_LevelText[i].SetText("A");
                            break;
                        case 1:
                            m_LevelText[i].SetText("B");
                            break;
                        case 2:
                            m_LevelText[i].SetText("C");
                            break;
                        case 3:
                            m_LevelText[i].SetText("D");
                            break;
                        default:
                            m_LevelText[i].SetText("NULL");
                            break;
                    }

                    m_TrainNoText[i].SetText(HXFaultCommom.m_TotalFaults[i + m_Page*16].CaNum.ToString());
                    m_FaultNoText[i].SetText(HXFaultCommom.m_TotalFaults[i + m_Page*16].FaultID.ToString());
                    m_InfoText[i].SetText(HXFaultCommom.m_TotalFaults[i + m_Page*16].FaultText);
                    m_HapenedTimeText[i].SetText(HXFaultCommom.m_TotalFaults[i + m_Page*16].HappenedTime.Month.ToString() +
                                               "-" + HXFaultCommom.m_TotalFaults[i + m_Page*16].HappenedTime.Day.ToString() +
                                               " " +
                                               HXFaultCommom.m_TotalFaults[i + m_Page*16].HappenedTime.ToLongTimeString());
                    m_EndedTimeText[i].SetText(HXFaultCommom.m_TotalFaults[i + m_Page*16].EndedTime.Month.ToString() + "-" +
                                             HXFaultCommom.m_TotalFaults[i + m_Page*16].EndedTime.Day.ToString() +
                                             " " + HXFaultCommom.m_TotalFaults[i + m_Page*16].EndedTime.ToLongTimeString());
                }
                else
                {
                    m_LevelText[i].SetText(" ");
                    m_TrainNoText[i].SetText(" ");
                    m_FaultNoText[i].SetText(" ");
                    m_InfoText[i].SetText(" ");
                    m_HapenedTimeText[i].SetText(" ");
                    m_EndedTimeText[i].SetText(" ");
                }
            }
        }

        public void DrawOn(Graphics g)
        {
            //�ײ�������ť����
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].OnDraw(g);
            }

            #region ����������

            g.FillRectangle(m_GrayBrush, m_TitleRect);
            g.DrawString("���", HxCommon.Font12B, HxCommon.RedBrush, m_TitleRect.X + 2, m_TitleRect.Y + 4);
            g.DrawString("����", HxCommon.Font12B, HxCommon.BlackBrush, m_TitleRect.X + 35, m_TitleRect.Y + 4);
            g.DrawString("����", HxCommon.Font12B, HxCommon.WhiteBrush, m_TitleRect.X + 85, m_TitleRect.Y + 4);
            g.DrawString("��������", HxCommon.Font12B, HxCommon.BlackBrush, m_TitleRect.X + 325, m_TitleRect.Y + 4);
            g.DrawString("���Ϸ���ʱ��", HxCommon.Font12B, HxCommon.DeadBrush, m_TitleRect.X + 500, m_TitleRect.Y + 4);
            g.DrawString("���Ͻ���ʱ��", HxCommon.Font12B, HxCommon.DeadBrush, m_TitleRect.X + 660, m_TitleRect.Y + 4);

            #endregion

            for (int i = 0; i < 16; i++)
            {
                m_LevelText[i].OnDraw(g);
                m_TrainNoText[i].OnDraw(g);
                m_FaultNoText[i].OnDraw(g);
                m_InfoText[i].OnDraw(g);
                m_HapenedTimeText[i].OnDraw(g);
                m_EndedTimeText[i].OnDraw(g);
            }

            g.DrawImage(m_ImageList[0], HxCommon.Recposition.X + 773, HxCommon.Recposition.Y + 62, 25, 45);
            g.DrawImage(m_ImageList[1], HxCommon.Recposition.X + 773, m_LevelText[15].m_RectPosition.Bottom - 25, 25, 25);
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
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:

                            break;
                        case 6: //����7�л�����ǰ������ͼ
                            append_postCmd(CmdType.ChangePage, 13, 0, 0);
                            break;
                        case 7: //����8�л������й�����ͼ
                            append_postCmd(CmdType.ChangePage, 14, 0, 0);
                            break;
                        case 8: //����7�л�����ʷ������ͼ
                            append_postCmd(CmdType.ChangePage, 15, 0, 0);
                            break;
                        case 9: //����������
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                        default:
                            break;
                    }
                }
            }

            for (int i = 0; i < 2; i++)
            {
                if (m_IsPageButtonDown[i])
                {
                    if (i == 0)
                    {
                        if (m_Page > 0)
                        {
                            m_Page--;
                        }
                    }
                    else
                    {
                        if (HXFaultCommom.m_TotalFaults.Count%16 == 0)
                        {
                            if (m_Page < HXFaultCommom.m_TotalFaults.Count/16 - 1)
                            {
                                m_Page++;
                            }
                        }
                        else
                        {
                            if (m_Page < HXFaultCommom.m_TotalFaults.Count/16)
                            {
                                m_Page++;
                            }
                        }

                    }
                }
            }
        }
    }
}