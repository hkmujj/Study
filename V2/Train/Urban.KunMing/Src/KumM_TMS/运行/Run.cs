using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace KumM_TMS.����
{
    /// <summary>
    /// ����
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    internal class Run : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::

        public override string GetInfo()
        {
            return "����";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }


        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::

        public override void paint(Graphics g)
        {
            DrawOn(g);
        }

        public override bool mouseDown(Point nPoint)
        {
            var index = 0;
            for (; index < m_Rect.Count; ++index)
            {
                if (m_Rect[index].IsVisible(nPoint))
                {
                    break;
                }
            }
            switch (index)
            {
                case 0:
                    if (m_BtnCanDown[0])
                    {
                        m_ButtonIsDown[0] = true;
                    }
                    break;
                case 1:
                    if (m_BtnCanDown[1])
                    {
                        m_ButtonIsDown[1] = true;
                    }
                    break;
                case 2:
                    if (m_BtnCanDown[2])
                    {
                        m_ButtonIsDown[2] = true;
                    }
                    break;
                case 3:
                    if (m_BtnCanDown[3])
                    {
                        m_ButtonIsDown[3] = true;
                    }
                    break;
                case 4:
                    m_ButtonIsDown[4] = !m_ButtonIsDown[4];
                    for (var i = 0; i < 2; i++)
                    {
                        m_BtnCanDown[5 + i] = m_ButtonIsDown[4];
                        m_ButtonIsDown[5 + i] = !m_ButtonIsDown[4];
                    }
                    break;
                case 5:
                    if (m_BtnCanDown[5])
                    {
                        m_ButtonIsDown[5] = true;
                        append_postCmd(CmdType.SetBoolValue, 1667, 1, 0);
                    }
                    break;
                case 6:
                    if (m_BtnCanDown[6])
                    {
                        m_ButtonIsDown[6] = true;
                        append_postCmd(CmdType.SetBoolValue, 1668, 1, 0);
                    }
                    break;
                case 7:
                    if (m_BtnCanDown[7])
                    {
                        m_ButtonIsDown[7] = true;
                    }
                    break;
                case 8:
                    if (m_BtnCanDown[8])
                    {
                        m_ButtonIsDown[8] = true;
                    }
                    break;
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            var index = 0;
            for (; index < m_Rect.Count; ++index)
            {
                if (m_Rect[index].IsVisible(nPoint))
                {
                    break;
                }
            }
            switch (index)
            {
                case 0: //�����㲥��
                    if (m_BtnCanDown[0])
                    {
                        m_ButtonIsDown[0] = false;
                        append_postCmd(CmdType.ChangePage, 29, 0, 0);
                    }
                    break;
                case 1: //վ�����á�
                    if (m_BtnCanDown[1])
                    {
                        m_ButtonIsDown[1] = false;
                        append_postCmd(CmdType.ChangePage, 30, 0, 0);
                    }
                    break;
                case 2: //��·��Ϣ��
                    if (m_BtnCanDown[2])
                    {
                        m_ButtonIsDown[2] = false;
                        append_postCmd(CmdType.ChangePage, 31, 0, 0);
                    }
                    break;
                case 3: //ǣ��������ʾ
                    if (m_BtnCanDown[3])
                    {
                        m_ButtonIsDown[3] = false;
                    }
                    break;
                case 4: //�ֶ���
                    break;
                case 5: //��
                    if (m_BtnCanDown[5])
                    {
                        m_ButtonIsDown[5] = false;
                        append_postCmd(CmdType.SetBoolValue, 1667, 0, 0);
                    }
                    break;
                case 6: //�ҡ�
                    if (m_BtnCanDown[6])
                    {
                        m_ButtonIsDown[6] = false;
                        append_postCmd(CmdType.SetBoolValue, 1668, 0, 0);
                    }
                    break;
                case 7: //����1��
                    if (m_BtnCanDown[7])
                    {
                        m_ButtonIsDown[7] = false;
                    }
                    break;
                case 8: //����2
                    if (m_BtnCanDown[8])
                    {
                        m_ButtonIsDown[8] = false;
                    }
                    break;
            }
            return true;
        }

        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::

        /// <summary>
        /// ���
        /// </summary>
        /// <param name="g"></param>
        private void DrawFrame(Graphics g)
        {
            //��
            for (var i = 0; i < 77; i++)
            {
                g.DrawRectangle(FormatStyle.WhitePen, m_Rects[i].X, m_Rects[i].Y, m_Rects[i].Width, m_Rects[i].Height);
            }
            //
            for (var i = 0; i < 11; i++)
            {
                g.DrawString(FormatStyle.Str2[i], FormatStyle.Font12B,
                    FormatStyle.WhiteBrush, m_Rects[i], m_DrawFormat);
            }
            for (var i = 0; i < 3; i++)
            {
                g.DrawRectangle(FormatStyle.WhitePen, m_Rects[83 + i].X, m_Rects[83 + i].Y, m_Rects[83 + i].Width,
                    m_Rects[83 + i].Height);
            }
        }

        /// <summary>
        /// �����ֵ
        /// </summary>
        /// <param name="g"></param>
        private void DrawValue(Graphics g)
        {
            #region ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

            //----
            for (var i = 0; i < 10; i++)
            {
                g.DrawString("-", FormatStyle.Font14B,
                    FormatStyle.WhiteBrush, m_Rects[11 + m_Lines[i]], m_DrawFormat);
            }

            //��ѹ��
            for (var i = 0; i < 2; i++)
            {
                g.FillRectangle(BoolList[204 + i] ? FormatStyle.LightGreenBrush : FormatStyle.MediumGreySolidBrush,
                    m_Rects[203 + i]);
            }

            //ǣ��ϵͳ
            //�г���>���ϡ�>ͨѶ���ϡ�>����
            for (var i = 0; i < 4; i++)
            {
                if (BoolList[208 + i*4]) //����
                {
                    g.FillRectangle(FormatStyle.RedBrush, m_Rects[205 + i]);
                }
                else if (BoolList[209 + i*4]) //ͨѶ����
                {
                    g.FillRectangle(FormatStyle.MediumGreySolidBrush, m_Rects[205 + i]);
                }
                else if (BoolList[206 + i*4]) //����
                {
                    g.FillRectangle(FormatStyle.LightGreenBrush, m_Rects[205 + i]);
                }

                //���Ϻ�
                if (BoolList[758 + i*3])
                {
                    g.DrawImage(m_Img[34], m_Rects[205 + i]);
                }
                else if (BoolList[759 + i*3])
                {
                    g.DrawImage(m_Img[35], m_Rects[205 + i]);
                }
            }

            //BHB/BLB
            //
            for (var i = 0; i < 4; i++)
            {
                g.FillRectangle(BoolList[720 + i] ? FormatStyle.LightGreenBrush : FormatStyle.MediumGreySolidBrush,
                    m_Rects[125 + i]);

                g.DrawRectangle(FormatStyle.WhitePen, m_Rects[125 + i].X, m_Rects[125 + i].Y, m_Rects[125 + i].Width,
                    m_Rects[125 + i].Height);
                g.DrawString(FormatStyle.Str17[i], FormatStyle.Font10B,
                    FormatStyle.WhiteBrush, m_Rects[125 + i], m_DrawFormat);
            }



            //�˿ͱ���
            //���ϡ�>������>�Խ���>����
            for (var i = 0; i < 12; i++)
            {
                if (BoolList[223 + i*4])
                {
                    g.DrawImage(m_Img[6], m_Rects[131 + i]);
                }
                else if (BoolList[224 + i*4])
                {
                    g.DrawImage(m_Img[7], m_Rects[131 + i]);
                }
                else if (BoolList[222 + i*4])
                {
                    g.DrawImage(m_Img[5], m_Rects[131 + i]);
                }
                else if (BoolList[225 + i*4])
                {
                    g.DrawImage(m_Img[8], m_Rects[131 + i]);
                }
            }

            //�̻𱨾�
            //���ϡ�>������>����
            for (var i = 0; i < 6; i++)
            {
                if (BoolList[272 + i*3])
                {
                    g.DrawImage(m_Img[11], m_Rects[143 + i]);
                }
                else if (BoolList[271 + i*3])
                {
                    g.DrawImage(m_Img[10], m_Rects[143 + i]);
                }
                else if (BoolList[270 + i*3])
                {
                    g.DrawImage(m_Img[9], m_Rects[143 + i]);
                }
                else
                {
                    g.DrawImage(m_Img[12], m_Rects[143 + i]);
                }
            }

            //�����¶�
            for (var i = 0; i < 6; i++)
            {
                g.DrawString(FloatList[52 + i].ToString("0.0��"), FormatStyle.Font14,
                    FormatStyle.WhiteBrush, m_Rects[41 + i], m_DrawFormat);
            }

            //1����
            for (var i = 0; i < 24; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    if (BoolList[288 + i*8 + j])
                    {
                        g.DrawImage(m_Img[13 + j], m_Rects[149 + i]);
                    }

                    if (BoolList[480 + i*8 + j])
                    {
                        g.DrawImage(m_Img[13 + j], m_Rects[173 + i]);
                    }
                }
            }

            //�ƶ���ѹ��
            for (var i = 0; i < 6; i++)
            {
                g.DrawLine(FormatStyle.WhitePen, m_PDrawPoint[i*2], m_PDrawPoint[1 + i*2]);
                g.DrawString(Convert.ToInt32(FloatList[58 + i*2]).ToString(),
                    FormatStyle.Font12, FormatStyle.WhiteBrush, m_Rects[90 + i*2], m_DrawFormat);
                g.DrawString(Convert.ToInt32(FloatList[59 + i*2]).ToString(),
                    FormatStyle.Font12, FormatStyle.WhiteBrush, m_Rects[91 + i*2], m_DrawFormat);
            }

            //ͣ���ƶ�
            for (var i = 0; i < 6; i++)
            {
                if (BoolList[672 + i*2])
                {
                    g.FillRectangle(FormatStyle.LightGreenBrush, m_Rects[209 + i]);
                }
                else if (BoolList[673 + i*2])
                {
                    g.FillRectangle(FormatStyle.RedBrush, m_Rects[209 + i]);
                }
                else
                {
                    g.FillRectangle(FormatStyle.MediumGreySolidBrush, m_Rects[209 + i]);
                }
                g.DrawString("P", FormatStyle.Font16B, FormatStyle.BlackBrush, m_Rects[209 + i], m_DrawFormat);
            }

            //�����ƶ�
            for (var i = 0; i < 12; i++)
            {
                if (BoolList[684 + i*3])
                {
                    g.FillRectangle(FormatStyle.WhiteBrush, m_Rects[102 + i]);
                }
                else if (BoolList[685 + i*3])
                {
                    g.FillRectangle(FormatStyle.YellowBrush, m_Rects[102 + i]);
                }
                else if (BoolList[686 + i*3])
                {
                    g.FillRectangle(FormatStyle.LightGreenBrush, m_Rects[102 + i]);
                }
            }

            //����1
            for (var i = 0; i < 5; i++)
            {
                if (BoolList[731 + i])
                {
                    g.DrawString(FormatStyle.Str18[i], FormatStyle.Font12B, FormatStyle.WhiteBrush,
                        m_Rects[83], m_DrawFormat);
                }
            }
            for (var i = 0; i < 2; i++)
            {
                if (BoolList[724 + i])
                {
                    g.DrawString(FormatStyle.Str18[i + 5], FormatStyle.Font12B, FormatStyle.WhiteBrush,
                        m_Rects[83], m_DrawFormat);
                }
            }
            //����2
            if (BoolList[729])
            {
                g.DrawString("ǣ�� - " + Convert.ToInt32(FloatList[47]) + "%",
                    FormatStyle.Font12B, FormatStyle.WhiteBrush, m_Rects[84], m_DrawFormat);
            }
            else if (BoolList[730])
            {
                g.DrawString("�����ƶ� - " + Convert.ToInt32(FloatList[47]) + "%",
                    FormatStyle.Font12B, FormatStyle.WhiteBrush, m_Rects[84], m_DrawFormat);
            }
            else if (BoolList[1097])
            {
                g.DrawString("���� - " + Convert.ToInt32(FloatList[47]) + "%",
                    FormatStyle.Font12B, FormatStyle.WhiteBrush, m_Rects[84], m_DrawFormat);
            }
            else
            {
                for (var i = 0; i < 4; i++)
                {
                    if (BoolList[736 + i])
                    {
                        g.DrawString(FormatStyle.Str19[i] + " - " + Convert.ToInt32(FloatList[47]) + "%"
                            , FormatStyle.Font12B, FormatStyle.WhiteBrush, m_Rects[84], m_DrawFormat);
                    }
                }
            }

            //����3
            //ǣ������
            if (BoolList[740])
            {
                g.DrawImage(m_Img[27], m_Rects[117]);
                g.DrawImage(m_Img[29], m_Rects[130]);
                g.DrawString("ǣ��������ʾ", FormatStyle.Font10B,
                    FormatStyle.BlackBrush, m_Rects[117], m_DrawFormat);

                g.FillRectangle(FormatStyle.RedBrush, m_Rects[85]);
                g.DrawString("ǣ������", FormatStyle.Font12B, FormatStyle.WhiteBrush,
                    m_Rects[85], m_DrawFormat);
            }
            //else if (BoolList[725])
            //{
            //    e.FillRectangle(FormatStyle.RedBrush, rects[85]);
            //    e.DrawString("����ģʽ", FormatStyle.Font12B, FormatStyle.WhiteBrush,
            //        rects[85], drawFormat);
            //}
            else if (BoolList[726])
            {
                g.FillRectangle(FormatStyle.RedBrush, m_Rects[85]);
                g.DrawString("����ǣ��", FormatStyle.Font12B, FormatStyle.WhiteBrush,
                    m_Rects[85], m_DrawFormat);
            }
            else if (BoolList[727])
            {
                g.FillRectangle(FormatStyle.RedBrush, m_Rects[85]);
                g.DrawString("���谴ť�ɿ�", FormatStyle.Font12B, FormatStyle.WhiteBrush,
                    m_Rects[85], m_DrawFormat);
            }
            else if (BoolList[728])
            {
                g.FillRectangle(FormatStyle.RedBrush, m_Rects[85]);
                g.DrawString("����" + FloatList[44].ToString("0") + "km/H", FormatStyle.Font12B, FormatStyle.WhiteBrush,
                    m_Rects[85], m_DrawFormat);
            }
            else if (BoolList[1584])
            {
                g.FillRectangle(FormatStyle.RedBrush, m_Rects[85]);
                g.DrawString("�����ƶ�", FormatStyle.Font12B, FormatStyle.WhiteBrush,
                    m_Rects[85], m_DrawFormat);
            }
            else
                g.DrawString("NA", FormatStyle.Font12B, FormatStyle.WhiteBrush,
                    m_Rects[85], m_DrawFormat);

            #endregion

            //�����㲥��վ������
            for (var i = 0; i < 2; i++)
            {
                g.DrawImage(m_ButtonIsDown[i] ? m_Img[22] : m_Img[21], m_Rects[114 + i]);
                g.DrawString(FormatStyle.վ�㰴��[i], FormatStyle.Font12B,
                    FormatStyle.BlackBrush, m_Rects[114 + i], m_DrawFormat);
            }

            //��·��Ϣ
            g.DrawImage(m_Img[27], m_Rects[116]);
            for (var i = 0; i < 11; i++)
            {
                if (!BoolList[80 + i])
                {
                    g.DrawImage(m_Img[28], m_Rects[129]);
                }
                else
                {
                    g.DrawImage(m_Img[29], m_Rects[129]);
                    break;
                }
            }
            g.DrawString("��·��Ϣ", FormatStyle.Font12B, FormatStyle.BlackBrush,
                m_Rects[129], m_DrawFormat);


            //�ֶ�ģʽ������
            for (var i = 0; i < 5; i++)
            {
                if (m_BtnCanDown[i + 4])
                {
                    g.DrawImage(m_ButtonIsDown[i + 4] ? m_Img[22] : m_Img[21], m_Rects[118 + i]);

                    if (i > 0)
                    {
                        g.DrawImage(m_Img[23 + i - 1], m_Rects[197 + i]);
                    }
                    else if (i == 0)
                    {
                        g.DrawString("�ֶ�ģʽ", FormatStyle.Font12B,
                            FormatStyle.BlackBrush, m_Rects[118], m_DrawFormat);
                    }
                }
                else
                {
                    g.DrawImage(m_Img[22], m_Rects[118 + i]);

                    if (i > 0)
                    {
                        g.DrawImage(m_Img[30 + i - 1], m_Rects[197 + i]);
                    }
                    else if (i == 0)
                    {
                        g.DrawString("�ֶ�ģʽ", FormatStyle.Font12B,
                            FormatStyle.HalfPGreySolidBrush, m_Rects[118], m_DrawFormat);
                    }
                }

            }
        }


        /// <summary>
        /// ��ͼ
        /// </summary>
        /// <param name="g"></param>
        private void DrawOn(Graphics g)
        {
            DrawValue(g);
            DrawFrame(g);
        }

        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        /// <summary>
        /// ��ʼ�����ꡢ���顢����
        /// </summary>
        private void InitData()
        {
            m_DrawFormat.LineAlignment = StringAlignment.Center;
            m_DrawFormat.Alignment = StringAlignment.Center;

            m_RightFormat.LineAlignment = StringAlignment.Far;
            m_RightFormat.Alignment = StringAlignment.Center;

            m_LeftFormat.LineAlignment = StringAlignment.Near;
            m_LeftFormat.Alignment = StringAlignment.Center;

            m_PDrawPoint = new PointF[20];

            m_Rects = new RectangleF[250];

            m_Img = new Image[37];

            m_ButtonIsDown = new bool[10];

            m_BtnCanDown = new bool[10] {true, true, true, true, true, false, false, true, true, true};

            m_Rect = new List<Region>();

            for (var i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            #region :::::::::::::::::::::::::::::: rects :::::::::::::::::::::::::::::::::::

            for (var i = 0; i < 11; i++)
            {
                m_Rects[i] = new RectangleF(10, 170 + i*29, 115, 29);

                for (var j = 0; j < 6; j++)
                {
                    m_Rects[11 + i*6 + j] = new RectangleF(125 + j*95, 170 + i*29, 95, 29);
                }
            }
            //��
            for (var i = 0; i < 6; i++)
            {
                m_Rects[77 + i] = new RectangleF(125 + i*95, 100, 95, 60);
            }
            //ģʽ��
            m_Rects[83] = new RectangleF(5, 515, 90, 30);
            //ǣ���ƶ���
            m_Rects[84] = new RectangleF(100, 515, 150, 30);
            //ǣ��������
            m_Rects[85] = new RectangleF(255, 515, 148, 30);


            for (var i = 0; i < 4; i++)
            {
                m_Rects[86 + i] = new RectangleF(503 + i*97, 502, 90, 40);
            }

            for (var i = 0; i < 6; i++)
            {
                //�ƶ���ѹ��
                m_Rects[90 + i*2] = new RectangleF(125 + i*95, 402, 47, 29);
                m_Rects[91 + i*2] = new RectangleF(172 + i*95, 402, 48, 29);

                //�����ƶ�
                m_Rects[102 + i*2] = new RectangleF(127 + i*95, 462, 45, 25);
                m_Rects[103 + i*2] = new RectangleF(174 + i*95, 462, 45, 25);
            }

            //�����㲥��վ�����á���·��Ϣ��ǣ��������ʾ
            for (var i = 0; i < 4; i++)
            {
                m_Rects[114 + i] = new RectangleF(406 + i*97, 505, 90, 40);
            }

            //�ֶ������ҡ�����1������2
            for (var i = 0; i < 5; i++)
            {
                m_Rects[118 + i] = new RectangleF(705, 200 + 50*i, 90, 40);
            }

            //BHB/BLB
            for (var i = 0; i < 2; i++)
            {
                m_Rects[125 + i*2] = new RectangleF(318 + i*95, 230, 43, 25);
                m_Rects[126 + i*2] = new RectangleF(364 + i*95, 230, 43, 25);
            }

            for (var i = 0; i < 2; i++)
            {
                m_Rects[129 + i] = new RectangleF(602 + i*97, 507, 86, 36);
            }


            //picture
            for (var i = 0; i < 6; i++)
            {
                //�˿ͱ���1
                m_Rects[131 + i*2] = new RectangleF(135 + i*95, 258, 30, 27);
                //�˿ͱ���2
                m_Rects[132 + i*2] = new RectangleF(180 + i*95, 258, 30, 27);
                //�̻𱨾�
                m_Rects[143 + i] = new RectangleF(157 + i*95, 286, 30, 27);
                //1����
                m_Rects[149 + i*4] = new RectangleF(127 + i*95, 345, 22, 27);
                m_Rects[150 + i*4] = new RectangleF(150 + i*95, 345, 22, 27);
                m_Rects[151 + i*4] = new RectangleF(173 + i*95, 345, 22, 27);
                m_Rects[152 + i*4] = new RectangleF(196 + i*95, 345, 22, 27);
                //2����
                m_Rects[173 + i*4] = new RectangleF(127 + i*95, 374, 22, 27);
                m_Rects[174 + i*4] = new RectangleF(150 + i*95, 374, 22, 27);
                m_Rects[175 + i*4] = new RectangleF(173 + i*95, 374, 22, 27);
                m_Rects[176 + i*4] = new RectangleF(196 + i*95, 374, 22, 27);
                //�ֶ�ģʽ
                m_Rects[197 + i] = new RectangleF(735, 205 + i*50, 32, 30);
            }

            //���
            //����ѹ����
            for (var i = 0; i < 2; i++)
            {
                m_Rects[203 + i] = new RectangleF(220f + i*3*95, 170, 94, 28);
            }
            //ǣ��ϵͳ
            for (var i = 0; i < 4; i++)
            {
                m_Rects[205 + i] = new RectangleF(220f + i*95, 170 + 29, 94, 28);
            }
            //ͣ���ƶ�
            for (var i = 0; i < 6; i++)
            {
                m_Rects[209 + i] = new RectangleF(125f + i*95, 431, 94, 28);
            }

            //MoveScreen
            for (var i = 0; i < m_Rects.Length; i++)
            {
                m_Rects[i].X = (m_Rects[i].X + FormatStyle.ScreenMoveX)*FormatStyle.Scale;
                m_Rects[i].Y = (m_Rects[i].Y + FormatStyle.ScreenMoveY)*FormatStyle.Scale;
                m_Rects[i].Width *= FormatStyle.Scale;
                m_Rects[i].Height *= FormatStyle.Scale;
            }

            #endregion

            #region :::::::::::::::::::::::::::::::: point :::::::::::::::::::::::::::::::::


            for (var i = 0; i < 6; i++)
            {
                //�ƶ���ѹ��
                m_PDrawPoint[i*2] = new PointF(173 + i*95, 402);
                m_PDrawPoint[1 + i*2] = new PointF(173 + i*95, 431);
            }

            //MoveScreen
            for (var i = 0; i < m_PDrawPoint.Length; i++)
            {
                m_PDrawPoint[i].X = (m_PDrawPoint[i].X + FormatStyle.ScreenMoveX)*FormatStyle.Scale;
                m_PDrawPoint[i].Y = (m_PDrawPoint[i].Y + FormatStyle.ScreenMoveY)*FormatStyle.Scale;
            }

            #endregion

            #region :::::::::::::::::::::::::::::::: Rect ::::::::::::::::::::::::::::::::::

            for (var i = 0; i < 9; i++)
            {
                m_Rect.Add(new Region(m_Rects[114 + i]));
            }

            #endregion
        }

        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        public static StringFormat m_DrawFormat = new StringFormat();

        public static StringFormat m_RightFormat = new StringFormat();

        public static StringFormat m_LeftFormat = new StringFormat();

        private int[] m_Lines = new int[10] {0, 2, 3, 5, 6, 11, 12, 13, 16, 17};

        #region:::::::::::::::::::::::::::��ֵ����::::::::::::::::::::::::::::::::

        /// <summary>
        /// ���꼯
        /// </summary>
        internal PointF[] m_PDrawPoint;

        /// <summary>
        /// ���ο�
        /// </summary>
        internal RectangleF[] m_Rects;

        /// <summary>
        /// ͼƬ��
        /// </summary>
        internal Image[] m_Img;

        /// <summary>
        /// ���Ƿ���
        /// </summary>
        internal bool[] m_ButtonIsDown;

        /// <summary>
        /// ���Ƿ��ܰ���
        /// </summary>
        internal bool[] m_BtnCanDown;

        /// <summary>
        /// �����б�
        /// </summary>
        internal List<Region> m_Rect;

        #endregion#

        #endregion
    }
}
