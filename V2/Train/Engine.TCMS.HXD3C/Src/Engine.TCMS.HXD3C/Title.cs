using System;
using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3C.Fault;
using Engine.TCMS.HXD3C.Models;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3C
{
    //�����Լ���������
    [GksDataType(DataType.isMMIObjectClass)]
    public class Title : baseClass, IButtonBtnEventResponser
    {
        public static Title Instance { private set; get; }

        public override string GetInfo()
        {
            return "�����Լ���������";
        }

        public UnattendedTrialType UnattendedTrialType
        {
            set { m_UnattendedTrialControl.UnattendedTrialType = value; }
        }

        public bool UnattendedTrialTypeLocked { set; get; }

        /// <summary>
        /// ����Ҫ���� ����ͼ
        /// </summary>
        private List<int> m_NotNeedWorkStateViews;

        private UnattendedTrialControl m_UnattendedTrialControl;

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {

            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                UnattendedTrialTypeLocked = false;
                }

            else if (nParaA == SetRunValueDefine.ParaADefine.InCurrent)
            {
                m_WorkStateVisible = true;
                if (m_NotNeedWorkStateViews.Contains(nParaB))
                {
                    m_WorkStateVisible = false;
                }

                if (nParaB != 212)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        OpenScreen.m_ButtonOldState[i] = false;
                        OpenScreen.m_ChangeState[i] = true;
                    }
                }
                switch (nParaB)
                {
                    case 201:
                        m_TitleName = "ǣ��/�ƶ�����";
                        break;
                    case 202:
                        m_TitleName = "��������/ǣ���������";
                        break;
                    case 203:
                        m_TitleName = "����״̬����";
                        break;
                    case 204:
                        m_TitleName = "����״̬����";
                        break;
                    case 205:
                        m_TitleName = "���״̬����";
                        break;
                    case 206:
                        m_TitleName = "����״̬����";
                        break;
                    case 207:
                        m_TitleName = "���ػ���";
                        break;
                    case 208:
                        m_TitleName = "������Դ����";
                        break;
                    case 209:
                        m_TitleName = "������������";
                        break;
                    case 210:
                        m_TitleName = "������������";
                        break;
                    case 211:
                        m_TitleName = "�����л���";
                        break;
                    case 212:
                        m_TitleName = "���Ż���";
                        break;
                    case 214:
                        if (!m_GDkey)
                        {
                            append_postCmd(CmdType.ChangePage, 201, 0, 0);
                        }
                        else
                        {
                            m_TitleName = "�г����续��";
                        }
                        break;
                    case 215:
                        m_TitleName = "�����趨����";
                        break;
                    case 216:
                        m_TitleName = "";
                        break;
                    case 217:
                        m_TitleName = "���˾�������";
                        break;
                    case 218:
                        m_TitleName = "����˵�����";
                        break;
                    default:
                        m_TitleName = "";
                        break;
                }
            }
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            Instance = this;

            m_NotNeedWorkStateViews = new List<int>() { 210, 209, 215, 217, 218 };

            //3
            nErrorObjectIndex = -1;

            m_Img = this.GetImages();

            InitData();

            m_UnattendedTrialControl = new UnattendedTrialControl()
            {
                BackColorVisible = false,
                Text = "���˾���",
                DrawFont = Common.Txt12FontB,
                TextBrush = new SolidBrush(Color.White),
                OutLineRectangle = new Rectangle(m_PDrawPoint[7], new Size(90, 55)),
                NeedDarwOutline = false,
                TextFormat =
                    new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            };
            UnattendedTrialType = UnattendedTrialType.Unkown;

            ButtomButtonView.Instance.AddResponser(this);

            return true;
        }

        public override void paint(Graphics g)
        {
            GetValue();

            RefreshDate();

            DrawOn(g);

            m_UnattendedTrialControl.OnDraw(g);
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void GetValue()
        {
            for (int i = 0; i < UIObj.InBoolList.Count; i++)
            {
                m_BValue[i] = BoolList[UIObj.InBoolList[i]];
                m_OldbValue[i] = BoolOldList[UIObj.InBoolList[i]];
            }
            for (int i = 0; i < UIObj.InFloatList.Count; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }
            if (m_BValue[20] || m_BValue[21])
            {
                m_GDkey = true;
            }
            else
            {
                m_GDkey = false;
            }

            if (!m_BValue[31])
            {
                append_postCmd(CmdType.ChangePage, 216, 0, 0);
            }

            //��������
            m_TimeCount++;
            m_TimeTmp = m_TimeTmp / 5.0f;
            m_DicNumb1 += m_FValue[2] * m_FValue[4] * m_TimeTmp / 1000f / 3600f;
            m_DicNumb2 += m_FValue[3] * m_FValue[5] * m_TimeTmp / 1000f / 3600f;
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private void RefreshDate()
        {
            DateTime currenttime = DateTime.Now;

            //currenttime.ToString("yy-MM-dd");
            m_TimeStr2 = currenttime.ToString("yy��    MM��    dd��");

            //currenttime.ToString("hh:mm:ss t z")
            m_TimeStr1 = currenttime.ToString("HHʱ    mm��    ss��");
        }


        /// <summary>
        /// �л���ͼ
        /// </summary>
        /// <param name="i"></param>
        public bool Response(int i)
        {
            #region :::::::::::::::::::::::::::: ǣ��/�ƶ� ::::::::::::::::::::::::::::::::

            if (m_TitleName == "ǣ��/�ƶ�����" || m_TitleName == "���ػ���" || m_TitleName == "�г����续��" ||
                m_TitleName == "�����л���" || m_TitleName == "���Ż���" || m_TitleName == "����״̬" ||
                m_TitleName == "���� ��")
            {
                switch (i)
                {
                    case 1: //����״̬
                        append_postCmd(CmdType.ChangePage, 202, 0, 0);
                        break;
                    case 2: //�г�����
                        if (m_GDkey)
                            append_postCmd(CmdType.ChangePage, 214, 0, 0);
                        break;
                    case 3: //����
                        append_postCmd(CmdType.ChangePage, 207, 0, 0);
                        break;
                    case 4: //������
                        append_postCmd(CmdType.ChangePage, 211, 0, 0);
                        break;
                    case 5: //����״̬
                        append_postCmd(CmdType.ChangePage, 212, 0, 0);
                        break;
                    case 6: //����״̬
                        append_postCmd(CmdType.ChangePage, 215, 0, 0);
                        break;
                    case 7: //��������
                        append_postCmd(CmdType.ChangePage, 210, 0, 0);
                        break;
                    case 8: //���� ��
                        if (m_TitleName == "ǣ��/�ƶ�����")
                        {

                            if (Common.Str201[7] == "����  ��")
                            {
                                Common.Str201[7] = "����  ��";
                                Screen_LiangDu.m_Background = Screen_LiangDu.m_BackgroundBrush0;
                            }
                            else if (Common.Str201[7] == "����  ��")
                            {
                                Common.Str201[7] = "����  ��";
                                Screen_LiangDu.m_Background = Screen_LiangDu.m_BackgroundBrush2;
                            }
                            else if (Common.Str201[7] == "����  ��")
                            {
                                Common.Str201[7] = "����  ��";
                                Screen_LiangDu.m_Background = Screen_LiangDu.m_BackgroundBrush1;
                            }
                        }
                        else
                            append_postCmd(CmdType.ChangePage, 201, 0, 0);
                        break;
                }
            }
            #endregion

            #region :::::::::::::::::::::::::::: ����״̬ ::::::::::::::::::::::::::::::::

            else if (m_TitleName == "��������/ǣ���������" || m_TitleName == "����״̬����" ||
                     m_TitleName == "���״̬����" || m_TitleName == "������Դ����" ||
                     m_TitleName == "����״̬����" || m_TitleName == "����״̬����")
            {
                switch (i)
                {
                    case 1: //��������
                        append_postCmd(CmdType.ChangePage, 202, 0, 0);
                        break;
                    case 2: //����״̬
                        append_postCmd(CmdType.ChangePage, 204, 0, 0);
                        break;
                    case 3: //���״̬
                        append_postCmd(CmdType.ChangePage, 205, 0, 0);
                        break;
                    case 4: //������Դ
                        append_postCmd(CmdType.ChangePage, 208, 0, 0);
                        break;
                    case 5: //����״̬
                        append_postCmd(CmdType.ChangePage, 206, 0, 0);
                        break;
                    case 6: //����״̬
                        append_postCmd(CmdType.ChangePage, 203, 0, 0);
                        break;
                    case 7: //��������
                        append_postCmd(CmdType.ChangePage, 210, 0, 0);
                        break;
                    case 8: //����
                        append_postCmd(CmdType.ChangePage, 201, 0, 0);
                        break;
                }
            }
            #endregion

            #region ::::::::::::::::::::::::::: �������� ::::::::::::::::::::::::::::::::::

            else if (m_TitleName == "������������")
            {
                switch (i)
                {
                    case 1: //��������
                        append_postCmd(CmdType.ChangePage, 201, 0, 0);
                        break;
                    case 2: //��
                        break;
                    case 3: //����
                        append_postCmd(CmdType.ChangePage, 207, 0, 0);
                        break;
                    case 4: //������
                        append_postCmd(CmdType.ChangePage, 211, 0, 0);
                        break;
                    case 5: //��
                        break;
                    case 6: //��
                        break;
                    case 7: //��������
                        append_postCmd(CmdType.ChangePage, 210, 0, 0);
                        break;
                    case 8: //����
                        append_postCmd(CmdType.ChangePage, 201, 0, 0);
                        break;
                }
            }
            #endregion

            
            else
            {
                return false;
            }
            return true;
        }

        private int m_FlashTime = 0;

        /// <summary>
        /// ��˸
        /// </summary>
        /// <returns></returns>
        private bool IsFlash(int time)
        {
            if (m_FlashTime < time * 5)
            {
                ++m_FlashTime;
                return true;
            }
            else if (m_FlashTime >= time * 5 && m_FlashTime < time * 10)
            {
                ++m_FlashTime;
                return false;
            }
            else
            {
                m_FlashTime = 0;
                return false;
            }
        }

        /// <summary>
        /// ���˾���/����
        /// </summary>
        private void UpdateUnattendedTrialType()
        {
            if (UnattendedTrialTypeLocked)
            {
                return;
            }

            UnattendedTrialType = UnattendedTrialType.Unkown;
            if (m_BValue[7])
            {
                UnattendedTrialType = UnattendedTrialType.Cutoff;
            }
            else
            {
                if (m_BValue[36])
                {
                    UnattendedTrialType = UnattendedTrialType.Red;
                }
                else if (m_BValue[35])
                {
                    UnattendedTrialType = UnattendedTrialType.RedFlash;
                }
                else if (m_BValue[34])
                {
                    UnattendedTrialType = UnattendedTrialType.YellowFlash;
                }
                else if (m_BValue[33])
                {
                    UnattendedTrialType = UnattendedTrialType.Green;
                }
                else if (m_BValue[8])
                {
                    UnattendedTrialType = UnattendedTrialType.Normal;
                }
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="g"></param>
        private void DrawFrameLines(Graphics g)
        {
            UpdateUnattendedTrialType();

            //����            
            g.FillRectangle(Common.BlueBrush, new Rectangle(m_PDrawPoint[0], new Size(800, 35)));
            g.DrawRectangle(Common.WhitePen1, new Rectangle(m_PDrawPoint[0], new Size(800, 35)));
            g.DrawString(m_TitleName, Common.Txt14FontB, Common.WhiteBrush,
                new Rectangle(m_PDrawPoint[0], new Size(800, 35)), Common.DrawFormat);

            //
            for (int i = 0; i < 7; i++)
            {
                g.DrawRectangle(Common.WhitePen1,
                    new Rectangle(m_PDrawPoint[i + 1], new Size(m_PDrawPoint[i + 2].X - m_PDrawPoint[i + 1].X, 55)));
            }

            //ʱ��
            g.DrawString(m_TimeStr2, Common.Txt12FontB, Common.WhiteBrush,
                new Rectangle(m_PDrawPoint[1], new Size(195, 30)), Common.DrawFormat);
            g.DrawString(m_TimeStr1, Common.Txt12FontB, Common.WhiteBrush,
                new Rectangle(m_PDrawPoint[9], new Size(195, 30)), Common.DrawFormat);

            //�ٶ�
            g.DrawString(Convert.ToInt32(m_FValue[0]).ToString(), Common.Txt30FontLr, Common.WhiteBrush,
                new Rectangle(m_PDrawPoint[2], new Size(105, 55)), Common.RightFormat);
            g.DrawString("km/h", Common.Txt14FontB, Common.WhiteBrush,
                new Rectangle(m_PDrawPoint[2], new Size(155, 55)), Common.RightFormat);

            //�ƶ�����
            for (int i = 0; i < 4; i++)
            {
                //ȡ�������ƶ����ͷ��ƶ��������ƶ�
                //2012-11-12 ����л˼�������޸�
                if (i == 0 || i == 1 || i == 2) continue;

                if (m_OldbValue[i] && !m_BValue[i])
                {
                    m_FlashTime = 0;
                }
                if (m_BValue[i])
                {
                    if (IsFlash(1))
                    {
                        g.FillRectangle(Common.YellowBrush, new Rectangle(m_PDrawPoint[62], new Size(94, 54)));
                        g.DrawString(m_Zhidgk[i], Common.Txt16FontB, Common.BlackBrush,
                            new Rectangle(m_PDrawPoint[3], new Size(100, 55)), Common.DrawFormat);
                    }
                    else
                        g.DrawString(m_Zhidgk[i], Common.Txt16FontB, Common.WhiteBrush,
                            new Rectangle(m_PDrawPoint[3], new Size(100, 55)), Common.DrawFormat);
                    break;
                }
            }
            //ͣ���ƶ�
            if (!m_BValue[3] && m_BValue[38])
            {
                g.FillRectangle(Common.YellowBrush, new Rectangle(m_PDrawPoint[62], new Size(94, 54)));
                g.DrawString(m_Zhidgk[3], Common.Txt16FontB, Common.BlackBrush,
                    new Rectangle(m_PDrawPoint[3], new Size(100, 55)), Common.DrawFormat);
            }
            if (!m_BValue[3] && !m_BValue[38])
            {
                g.DrawString(m_Zhidgk[3], Common.Txt16FontB, Common.WhiteBrush,
                    new Rectangle(m_PDrawPoint[3], new Size(100, 55)), Common.DrawFormat);
            }

            //��������
            for (int i = 0; i < 3; i++)
            {
                if (m_BValue[i + 4])
                {
                    g.DrawString(m_Jicgk[i], Common.Txt16FontB, Common.WhiteBrush,
                        new Rectangle(m_PDrawPoint[4], new Size(90, 55)), Common.DrawFormat);
                }
            }

            //��λ
            if (m_FValue[1] <= 10)
            {
                g.DrawString(m_FValue[1].ToString("0.0"), Common.Txt30FontLr, Common.WhiteBrush,
                    new Rectangle(m_PDrawPoint[5].X - 10, m_PDrawPoint[5].Y, 80, 55), Common.RightFormat);
            }
            else
            {
                g.DrawString(m_FValue[1].ToString("00.0"), Common.Txt30FontLr, Common.WhiteBrush,
                    new Rectangle(m_PDrawPoint[5].X - 10, m_PDrawPoint[5].Y, 80, 55), Common.RightFormat);
            }
            g.DrawString("��", Common.Txt14FontB, Common.WhiteBrush,
                new Rectangle(m_PDrawPoint[10].X, m_PDrawPoint[10].Y, 25, 55), Common.RightFormat);

            //����
            if (m_BValue[32])
                g.DrawString("����", Common.Txt14FontB, Common.WhiteBrush,
                    new Rectangle(m_PDrawPoint[6], new Size(90, 55)), Common.DrawFormat);

            if (m_WorkStateVisible)
            {
                DrawGk(g);
            }

        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="g"></param>
        private void DrawGk(Graphics g)
        {
            #region ::::::::::::::::::::::: 1 ����/���� 1 2 ::::::::::::::::::::::::::::

            g.DrawString("1", Common.Txt14FontB, Common.WhiteBrush,
                m_Rectangles[25], Common.DrawFormat);
            //1��
            g.FillRectangle(m_BValue[18] ? Common.YellowBrush : Common.BlackBrush,
                m_Rectangles[27]);
            g.DrawString("1", Common.Txt12FontB,
                m_BValue[18] ? Common.BlackBrush : Common.WhiteBrush,
                m_Rectangles[27], Common.DrawFormat);
            //2��
            g.FillRectangle(m_BValue[19] ? Common.YellowBrush : Common.BlackBrush,
                m_Rectangles[28]);
            g.DrawString("2", Common.Txt12FontB,
                m_BValue[19] ? Common.BlackBrush : Common.WhiteBrush,
                m_Rectangles[28], Common.DrawFormat);

            if (!DefaultState.m_IsDefaultOccur)
            {
                g.FillRectangle(Common.GreenBrush, m_Rectangles[26]);
                g.DrawString("����", Common.Txt12FontB, Common.BlackBrush,
                    m_Rectangles[26], Common.DrawFormat);
            }
            else
            {
                g.FillRectangle(Common.RedBrush, m_Rectangles[26]);
                g.DrawString("����", Common.Txt12FontB, Common.WhiteBrush,
                    m_Rectangles[26], Common.DrawFormat);
            }
            for (int i = 0; i < 4; i++)
            {
                g.DrawRectangle(Common.WhitePen1, m_Rectangles[25 + i]);
            }

            #endregion

            #region :::::::::::::::::::::::::: ǣ��/�����г� :::::::::::::::::::::::::::

            m_PowerCut = 0;
            for (int i = 0; i < 6; i++)
            {
                if (m_BValue[22 + i])
                {
                    m_PowerCut++;
                }
            }
            if (m_PowerCut > 0 && m_BValue[6])
            {
                g.DrawImage(m_Img[9], m_Rectangles[29]);
                g.DrawString(m_PowerCut.ToString(), Common.Txt34FontLr,
                    Common.YellowBrush, m_Rectangles[30], Common.DrawFormat);
            }
            else if (m_PowerCut > 0 && m_BValue[5])
            {
                g.DrawImage(m_Img[10], m_Rectangles[29]);
                g.DrawString(m_PowerCut.ToString(), Common.Txt34FontLr,
                    Common.YellowBrush, m_Rectangles[30], Common.DrawFormat);
            }
            else if (m_PowerCut == 0 && m_BValue[6])
            {
                g.DrawImage(m_Img[15], m_Rectangles[29]);
            }
            else if (m_PowerCut == 0 && m_BValue[5])
            {
                g.DrawImage(m_Img[14], m_Rectangles[29]);
            }

            #endregion

            DrawPic(g);

            //������·ѹ����
            if (m_BValue[37])
            {
                g.DrawString("������·ѹ����", Common.Txt12FontLb, Common.WhiteBrush,
                    new Rectangle(m_PDrawPoint[12], new Size(300, 30)), Common.LeftFormat);
            }

            //��������
            for (int i = 0; i < 10; i++)
            {
                g.DrawRectangle(Common.WhitePen1,
                    new Rectangle(m_PDrawPoint[11].X + i * 60, m_PDrawPoint[11].Y, 60, 60));
            }
            g.DrawLine(Common.BlackPen1, new Point(m_PDrawPoint[11].X + 7 * 60, m_PDrawPoint[11].Y + 1),
                new Point(m_PDrawPoint[11].X + 7 * 60, m_PDrawPoint[11].Y + 59));
            g.DrawRectangle(Common.WhitePen1, new Rectangle(m_PDrawPoint[12], new Size(300, 30)));
        }

        /// <summary>
        /// �����������ܵ繭�����ϵ�
        /// </summary>
        /// <param name="e"></param>
        private void DrawPic(Graphics e)
        {
            for (int index = 0; index < 9; index++)
            {
                if (m_BValue[9 + index])
                {
                    e.DrawImage(m_Img[index], m_Rectangles[16 + index]);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (m_BValue[28 + i])
                {
                    e.DrawImage(m_Img[11 + i], m_Rectangles[31 + i]);
                }
            }

            if (m_BValue[39])
                e.DrawImage(m_Img[16], m_Rectangles[34]);
        }

        /// <summary>
        /// ��ͼ
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            DrawFrameLines(e);
        }

        /// <summary>
        /// ��ʼ�������
        /// </summary>
        private void InitData()
        {
            Common.DrawFormat.Alignment = (StringAlignment)1;
            Common.DrawFormat.LineAlignment = (StringAlignment)1;

            Common.RightFormat.Alignment = (StringAlignment)2;
            Common.RightFormat.LineAlignment = (StringAlignment)1;

            Common.LeftFormat.Alignment = (StringAlignment)0;
            Common.LeftFormat.LineAlignment = (StringAlignment)1;


            #region ::::::::::::::::::::::::�����:::::::::::::::::::::::::::::#

            #region::::::::::::::::::: 00 - 14 :::::::::::::::::::::::::#

            m_PDrawPoint[0] = new Point(0, 0);

            m_PDrawPoint[1] = new Point(0, 35);
            m_PDrawPoint[2] = new Point(195, 35);
            m_PDrawPoint[3] = new Point(350, 35);
            m_PDrawPoint[4] = new Point(445, 35);
            m_PDrawPoint[5] = new Point(530, 35);
            m_PDrawPoint[6] = new Point(620, 35);
            m_PDrawPoint[7] = new Point(710, 35);
            m_PDrawPoint[8] = new Point(800, 35);

            m_PDrawPoint[9] = new Point(0, 60);
            m_PDrawPoint[10] = new Point(595, 35);

            m_PDrawPoint[11] = new Point(0, 90);
            m_PDrawPoint[12] = new Point(0, 150);

            m_PDrawPoint[13] = new Point(0, 550);
            m_PDrawPoint[14] = new Point(0, 0);

            #endregion#

            #region::::::::::::::::::: 15 - 54 :::::::::::::::::::::::::#

            //1
            m_PDrawPoint[15] = new Point(0, 552);
            m_PDrawPoint[16] = new Point(100, 552);
            m_PDrawPoint[17] = new Point(0, 597);
            m_PDrawPoint[18] = new Point(100, 597);

            m_PDrawPoint[19] = new Point(3, 555);

            //2
            m_PDrawPoint[20] = new Point(100, 552);
            m_PDrawPoint[21] = new Point(200, 552);
            m_PDrawPoint[22] = new Point(100, 597);
            m_PDrawPoint[23] = new Point(200, 597);

            m_PDrawPoint[24] = new Point(103, 555);

            //3
            m_PDrawPoint[25] = new Point(200, 552);
            m_PDrawPoint[26] = new Point(300, 552);
            m_PDrawPoint[27] = new Point(200, 597);
            m_PDrawPoint[28] = new Point(300, 597);

            m_PDrawPoint[29] = new Point(203, 555);

            //4
            m_PDrawPoint[30] = new Point(300, 552);
            m_PDrawPoint[31] = new Point(400, 552);
            m_PDrawPoint[32] = new Point(300, 597);
            m_PDrawPoint[33] = new Point(400, 597);

            m_PDrawPoint[34] = new Point(303, 555);

            //5
            m_PDrawPoint[35] = new Point(400, 552);
            m_PDrawPoint[36] = new Point(500, 552);
            m_PDrawPoint[37] = new Point(400, 597);
            m_PDrawPoint[38] = new Point(500, 597);

            m_PDrawPoint[39] = new Point(403, 555);

            //6
            m_PDrawPoint[40] = new Point(500, 552);
            m_PDrawPoint[41] = new Point(600, 552);
            m_PDrawPoint[42] = new Point(500, 597);
            m_PDrawPoint[43] = new Point(600, 597);

            m_PDrawPoint[44] = new Point(503, 555);

            //7
            m_PDrawPoint[45] = new Point(600, 552);
            m_PDrawPoint[46] = new Point(700, 552);
            m_PDrawPoint[47] = new Point(600, 597);
            m_PDrawPoint[48] = new Point(700, 597);

            m_PDrawPoint[49] = new Point(603, 555);

            //8
            m_PDrawPoint[50] = new Point(700, 552);
            m_PDrawPoint[51] = new Point(800, 552);
            m_PDrawPoint[52] = new Point(700, 597);
            m_PDrawPoint[53] = new Point(800, 597);

            m_PDrawPoint[54] = new Point(703, 555);

            #endregion#

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    m_PDrawPoint[55 + i * 3 + j] = new Point(610 + i * 30, 95 + j * 23);
                }
            }

            m_PDrawPoint[61] = new Point(611, 119);
            m_PDrawPoint[62] = new Point(m_PDrawPoint[3].X + 1, m_PDrawPoint[3].Y + 1);


            #endregion#

            m_Zhidgk = new string[] { "�����ƶ�", "�ͷ��ƶ�", "�����ƶ�", "ͣ���ƶ�" };
            m_Jicgk = new string[] { "����", "�ƶ�", "ǣ��" };

            m_ButtonIsDown = new bool[8];

            m_Rect = new List<Region>();
            Rectangle rect = new Rectangle(m_PDrawPoint[13], m_RectSize1);
            m_Rect.Add(new Region(rect));
            for (int i = 0; i < 7; i++)
            {
                rect.X += 100;
                m_Rect.Add(new Region(rect));
            }



            m_Rectangles = new Rectangle[40];
            for (int i = 0; i < 8; i++)
            {
                m_Rectangles[i] = new Rectangle(0 + i * 100, 550, 100, 50);
                m_Rectangles[i + 8] = new Rectangle(5 + i * 100, 555, 90, 40);
            }

            m_Rectangles[16] = new Rectangle(1, 91, 59, 59);
            m_Rectangles[17] = new Rectangle(1, 91, 59, 59);
            m_Rectangles[18] = new Rectangle(1, 91, 59, 59);
            m_Rectangles[19] = new Rectangle(61, 91, 59, 59);
            m_Rectangles[20] = new Rectangle(61, 91, 59, 59);
            m_Rectangles[21] = new Rectangle(121, 91, 59, 59);
            m_Rectangles[22] = new Rectangle(121, 91, 59, 59);
            m_Rectangles[23] = new Rectangle(181, 91, 59, 59);
            m_Rectangles[24] = new Rectangle(181, 91, 59, 59);

            m_Rectangles[25] = new Rectangle(610, 95, 60, 18);
            m_Rectangles[26] = new Rectangle(610, 113, 60, 23);
            m_Rectangles[27] = new Rectangle(610, 136, 30, 23);
            m_Rectangles[28] = new Rectangle(640, 136, 30, 23);

            m_Rectangles[29] = new Rectangle(360, 90, 60, 60);
            m_Rectangles[30] = new Rectangle(420, 90, 60, 60);

            m_Rectangles[31] = new Rectangle(240, 90, 60, 60);
            m_Rectangles[32] = new Rectangle(300, 90, 60, 60);
            m_Rectangles[33] = new Rectangle(540, 90, 60, 60);
            m_Rectangles[34] = new Rectangle(480, 90, 60, 60);
        }

        /// <summary>
        /// ��ǰ���ڽ���������
        /// </summary>
        public bool[] m_BValue = new bool[120];

        /// <summary>
        /// ǰһ�����ڽ��յ�������
        /// </summary>
        public bool[] m_OldbValue = new bool[120];


        /// <summary>
        /// ����ģ����
        /// </summary>
        public float[] m_FValue = new float[20];

        /// <summary>
        /// ���꼯
        /// </summary>
        public Point[] m_PDrawPoint = new Point[80];

        /// <summary>
        /// ͼƬ��
        /// </summary>
        public static Image[] m_Img = new Image[30];

        /// <summary>
        /// ���Ű����ߴ�
        /// </summary>
        private readonly Size m_RectSize1 = new Size(100, 50);

        /// <summary>
        /// ��ͷ
        /// </summary>
        public string m_TitleName = "";

        /// <summary>
        /// ʱ��
        /// </summary>
        public string m_TimeStr1 = "";

        public string m_TimeStr2 = "";

        /// <summary>
        /// �ƶ�����
        /// </summary>
        public string[] m_Zhidgk;

        /// <summary>
        /// ��������
        /// </summary>
        public string[] m_Jicgk;






        /// <summary>
        /// ���Ƿ���
        /// </summary>
        public bool[] m_ButtonIsDown;

        /// <summary>
        /// �����б�
        /// </summary>
        public List<Region> m_Rect;

        /// <summary>
        /// ���ο�
        /// </summary>
        public Rectangle[] m_Rectangles;

        /// <summary>
        /// �����Ƿ�Ҫ��
        /// </summary>
        private bool m_WorkStateVisible = true;

        public string[] m_Str200 = new string[8] { "", "", "", "", "", "", "", "" };

        /// <summary>
        /// �г�����Կ��
        /// </summary>
        public static bool m_GDkey = false;

        /// <summary>
        /// ǣ��/�����г�����
        /// </summary>
        public int m_PowerCut = 0;

        /// <summary>
        /// �����1����
        /// </summary>
        public static float m_DicNumb1 = 0;

        /// <summary>
        /// �����2����
        /// </summary>
        public static float m_DicNumb2 = 0;

        /// <summary>
        /// �ۼ�ʱ��
        /// </summary>
        public int m_TimeCount = 0;

        /// <summary>
        /// ʱ��
        /// </summary>
        public float m_TimeTmp = 0f;

        private UnattendedTrialType m_UnattendedTrialType;

        //���Ȼ���
        public static SolidBrush m_Background = new SolidBrush(Color.FromArgb(0, 0, 0, 0));
        public static SolidBrush m_BackgroundBrush0 = new SolidBrush(Color.FromArgb(0, 0, 0, 0));
        public static SolidBrush m_BackgroundBrush1 = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
        public static SolidBrush m_BackgroundBrush2 = new SolidBrush(Color.FromArgb(200, 0, 0, 0));
    }

}
