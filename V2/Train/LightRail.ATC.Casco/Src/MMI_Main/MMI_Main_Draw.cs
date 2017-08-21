using System;
using System.Drawing;
using System.Globalization;
using LightRail.ATC.Casco.MMI_Message;

namespace LightRail.ATC.Casco.MMI_Main
{
    public partial class MMI_Main
    {
        /// <summary>
        /// A1��������߼���ͼ
        /// </summary>
        /// <param name="g">��ͼ����</param>
        private void DrawA(Graphics g)
        {
            SetColor(Color.White);
            //�����ƶ�
            if (bValue[5])
            {
                SetColor(Color.Red);
                g.FillRectangle(FormatStyle.RedSolidBrush, rects[0]);
            }
            //���������ƶ�
            else if (bValue[4])
            {
                SetColor(Color.FromArgb(234, 145, 0));
                g.FillRectangle(FormatStyle.OrangeSolidBrush, rects[0]);
            }
            //TODO ȱ�� �ж� �ǲ���ATPģʽ Ȼ����ʾ Ŀ���ٶ�/Ŀ�����
            //A2
            //Ŀ���ٶ�/Ŀ�����
            if (bValue[13] && bValue[11] && bValue[2])
            {
                g.DrawString(Convert.ToInt32(fValue[0]).ToString(CultureInfo.InvariantCulture), FormatStyle.Font16B,
                    FormatStyle.WhiteSolidBrush, rects[68], RightFormat);
                g.DrawImage(Img[18], pDrawPoint[0]);
                g.FillRectangle(FormatStyle.LightGreenSolidBrush,
                    new RectangleF(60f, 395 - TargetDistance(fValue[1]), 25f, TargetDistance(fValue[1])));
            }

        }

        /// <summary>
        /// B��������߼���ͼ
        /// </summary>
        /// <param name="g">��ͼ����</param>
        private void DrawB(Graphics g)
        {
            if (bValue[10])
            {
                m_DialPlate.ControlType = ATCControlType.RM;
                pointer = SetPointer(m_DialPlate.ContentColor);
            }
            else if (bValue[33])
            {
                m_DialPlate.ControlType = ATCControlType.RMR;
                pointer = SetPointer(m_DialPlate.ContentColor);
            }
            else
            {
                //B
                //����
                //dial.OnDraw(g, FormatStyle.Font20);
                m_DialPlate.ControlType = ATCControlType.Other;

                //�ٶ�ָ��/����/�ƶ��ٶ�
                pointer = SetPointer(m_DialPlate.ContentColor);

                xianSuPointer.PaintPointer(g, Img[26], fValue[3]);

                zhiDongPointer.PaintPointer(g, Img[27], fValue[4]);
            }

            m_DialPlate.OnDraw(g);

            speedPointer.PaintPointer(g, pointer, fValue[2]);

            g.DrawString(Convert.ToInt32(fValue[2]).ToString(CultureInfo.InvariantCulture), FormatStyle.Font22B,
                FormatStyle.BlackSolidBrush, rects[67], drawFormat);
            g.DrawString("Km/h", FormatStyle.Font22B, LineDialPlateModel.TextSolidBrush, rects[69], drawFormat);
        }

        /// <summary>
        /// C1��������߼���ͼ
        /// </summary>
        /// <param name="g">��ͼ����</param>
        private void DrawC(Graphics g)
        {
            if (bValue[26])
            {
                g.DrawImage(Img[1], rects[7]);
            }
            else if (bValue[27])
            {
                g.DrawImage(Img[3], rects[7]);
            }
            else if (bValue[28])
            {
                g.DrawImage(Img[2], rects[7]);
            }

        }

        /// <summary>
        /// D��������߼���ͼ
        /// </summary>
        /// <param name="g">��ͼ����</param>
        private void DrawD(Graphics g)
        {
            g.DrawImage(Img[4], rects[8]);
            g.DrawString("Menu", FormatStyle.Font18B, FormatStyle.BlackSolidBrush, rects[8], drawFormat);
        }

        /// <summary>
        /// E��������߼���ͼ
        /// </summary>
        /// <param name="g">��ͼ����</param>
        private void DrawE(Graphics g)
        {
            #region :::::::::::::::::::: �����ȶ���Ϣ ::::::::::::::::::::::

            if (MMI_MsgReceive.MsgInf.NeedShowHighMsg)
            {
                if (MMI_MsgReceive.MsgInf.MsgHasShowed(50) == 1)
                {
                    //�ƿ�
                    g.DrawRectangle(FormatStyle.YellowPen, rects[21]);
                    //��Ϣ
                    g.DrawString(MMI_MsgReceive.MsgInf.HighMsg.MsgContent,
                        FormatStyle.Font16B, FormatStyle.WhiteSolidBrush,
                        rects[21], drawFormat);
                    //
                    m_BtnCanDown[0] = false;
                    m_BtnCanDown[1] = false;

                    m_ShowMsgSign = false;
                }
            }
                #endregion

                #region :::::::::::::::::::: �����ȶ���Ϣ ::::::::::::::::::::::

            else
            {
                //
                g.DrawRectangle(FormatStyle.WhitePen2, rects[21]);

                //
                if (MMI_MsgReceive.MsgInf != null)
                {
                    int count = MMI_MsgReceive.MsgInf.AllMsgsList.Count;
                    int index = count - m_RowId;

                    m_BtnCanDown[0] = m_RowId > 0;
                    m_BtnCanDown[1] = index > 3;

                    if (count > 0 && count <= 3)
                    {
                        m_BtnCanDown[0] = false; //����
                        m_BtnCanDown[1] = false; //����
                        for (int i = 0; i < count; i++)
                        {
                            g.DrawString(
                                MMI_MsgReceive.MsgInf.AllMsgsList[count - i - 1].MsgReceiveTime.ToShortTimeString() +
                                "  " + MMI_MsgReceive.MsgInf.AllMsgsList[count - i - 1].MsgContent, FormatStyle.Font12B,
                                FormatStyle.WhiteSolidBrush, rects[25 + i], LeftFormat);
                        }
                    }
                    else if (count > 3)
                    {
                        for (int i = 0; i < (index > 3 ? 3 : index); i++)
                        {
                            g.DrawString(
                                MMI_MsgReceive.MsgInf.AllMsgsList[count - i - 1 - m_RowId].MsgReceiveTime
                                    .ToShortTimeString() +
                                "  " + MMI_MsgReceive.MsgInf.AllMsgsList[count - i - 1 - m_RowId].MsgContent,
                                FormatStyle.Font12B,
                                FormatStyle.WhiteSolidBrush, rects[25 + i], LeftFormat);
                        }
                    }
                }
            }

            #endregion

            //��Ϣָʾ
            //if (m_ShowMsgSign && m_TimeIn)
            //    g.DrawImage(Img[41], rects[24]);

            for (int i = 0; i < 2; i++)
            {
                if (m_BtnCanDown[i])
                {
                    g.DrawImage(buttonIsDown[i] ? Img[19 + i*2] : Img[20 + i*2], rects[22 + i]);
                }
                else
                {
                    g.DrawImage(Img[19 + i*2], rects[22 + i]);
                }
            }
        }

        /// <summary>
        /// G��������߼���ͼ
        /// </summary>
        /// <param name="e">��ͼ����</param>
        private void DrawG(Graphics e)
        {
            LifeIndicator();
            switch (liftIndic)
            {
                case 1:
                    e.FillRectangle(FormatStyle.LightGreySolidBrush, rects[28]);
                    e.FillRectangle(FormatStyle.DarkGreySolidBrush, rects[29]);
                    break;
                case 2:
                    e.FillRectangle(FormatStyle.DarkGreySolidBrush, rects[28]);
                    e.FillRectangle(FormatStyle.LightGreySolidBrush, rects[29]);
                    break;
                case 3:
                    e.FillRectangle(FormatStyle.LightGreySolidBrush, rects[29]);
                    e.FillRectangle(FormatStyle.DarkGreySolidBrush, rects[30]);
                    break;
                case 4:
                    e.FillRectangle(FormatStyle.DarkGreySolidBrush, rects[29]);
                    e.FillRectangle(FormatStyle.LightGreySolidBrush, rects[30]);
                    break;
                case 5:
                    e.FillRectangle(FormatStyle.LightGreySolidBrush, rects[30]);
                    e.FillRectangle(FormatStyle.DarkGreySolidBrush, rects[31]);
                    break;
                case 6:
                    e.FillRectangle(FormatStyle.DarkGreySolidBrush, rects[30]);
                    e.FillRectangle(FormatStyle.LightGreySolidBrush, rects[31]);
                    break;
                case 7:
                    e.FillRectangle(FormatStyle.LightGreySolidBrush, rects[31]);
                    e.FillRectangle(FormatStyle.DarkGreySolidBrush, rects[32]);
                    e.FillRectangle(FormatStyle.DarkGreySolidBrush, rects[31]);
                    e.FillRectangle(FormatStyle.LightGreySolidBrush, rects[32]);
                    break;
            }
            //G2
            //��ǰ����
            e.DrawString(DateTime.Today.ToString("MM") + "/" + DateTime.Today.ToString("dd")
                         + "/" + DateTime.Today.ToString("yyyy"), FormatStyle.Font16B,
                FormatStyle.WhiteSolidBrush, rects[33], drawFormat);
            //��ǰʱ��
            e.DrawString(TheTimeNow.CurrentTime.ToLongTimeString(), FormatStyle.Font16B,
                FormatStyle.WhiteSolidBrush, rects[34], drawFormat);
        }

        /// <summary>
        /// M�������ͼ�λ���
        /// </summary>
        /// <param name="g">��ͼ����</param>
        private void DrawM(Graphics g)
        {
            //TODO �ж�M�����е�����Bool�߼���ʾ��Ӧ��ͼ��
            //M1 M3 RMģʽ�Ƿ��
            if (bValue[10] || bValue[33])
            {
                g.DrawImage(Img[6], rects[13]);

            }
            if (bValue[30])
            {
                g.DrawImage(bValue[12] ? Img[17] : Img[8], rects[15]);
            }
            //M2 M4ATPģʽ
            if (bValue[11])
            {
                g.DrawImage(Img[0], rects[14]);

            }
            if (bValue[31])
            {
                g.DrawImage(bValue[13] ? Img[17] : Img[8], rects[16]);
            }

            //M5
            if (bValue[22])
            {
                g.DrawImage(Img[11], rects[17]);
            }
            //M6
            if (bValue[24])
            {
                g.DrawImage(Img[10], rects[18]);
            }
            //M7
            if (bValue[25])
            {
                g.DrawImage(Img[7], rects[19]);
            }
            //�����ж�
            if (bValue[32])
            {
                g.DrawImage(Img[28], rects[94]);
            }

        }

        /// <summary>
        /// T��������߼���ͼ
        /// </summary>
        /// <param name="e">��ͼ����</param>
        private void DrawT(Graphics e)
        {
            e.DrawRectangle(FormatStyle.WhitePen, rects[20]);
            e.DrawImage(Img[13], rects[20]);
            e.DrawString(
                (Convert.ToInt32(fValue[12]) == 0 ? "CrewID" : "C" + Convert.ToInt32(fValue[12]).ToString("000")),
                FormatStyle.Font18B, FormatStyle.BlackSolidBrush, rects[20], drawFormat);
        }

        /// <summary>
        /// ���Ʋ˵�����߼�
        /// </summary>
        /// <param name="e">��ͼ����</param>
        private void DrawMenu(Graphics e)
        {
            e.DrawImage(Img[5], pDrawPoint[2]);
            //TODO  �˵������������δ��� rectsδ����
            for (int i = 0; i < 7; i++)
            {
                e.DrawString(str7[i], FormatStyle.Font22B,
                    FormatStyle.BlackSolidBrush, rects[70 + i], LeftFormat);
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="e">��ͼ����</param>
        private void DrawCrewNumb(Graphics e)
        {

            e.DrawImage(Img[12], rects[77]);
            e.DrawImage(Img[14], rects[78]);
            e.DrawImage(Img[14], rects[79]);
            e.DrawImage(Img[16], rects[78]);
            e.DrawImage(Img[9], rects[79]);
            e.DrawString(crowNumb, FormatStyle.Font12B,
                FormatStyle.BlackSolidBrush, rects[92], LeftFormat);
            e.DrawString("CrewID", FormatStyle.Font16B,
                FormatStyle.BlackSolidBrush, rects[93], LeftFormat);
            for (int i = 0; i < 12; i++)
            {
                e.DrawImage(buttonIsDown[19 + i] ? Img[14] : Img[15], rects[80 + i]);
                if (i == 9)
                {
                    e.DrawString("C", FormatStyle.Font24B,
                        FormatStyle.BlackSolidBrush, rects[80 + i], drawFormat);
                    continue;
                }
                if (i == 11)
                {
                    e.DrawString("BACK", FormatStyle.Font12,
                        FormatStyle.BlackSolidBrush, rects[80 + i], drawFormat);
                    continue;
                }
                e.DrawString((i + 1).ToString(), FormatStyle.Font24B,
                    FormatStyle.BlackSolidBrush, rects[80 + i], drawFormat);
            }
        }

        private void DrawAtc(Graphics g)
        {
            if (bValue[14])
            {
                g.FillRectangle(Brushes.Black, g.ClipBounds);
            }

        }
    }
}
