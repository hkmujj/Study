using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.PublicUI;
using KumM_MMI.MMI_Message;

namespace KumM_MMI
{
    public partial class MMI_Main : baseClass
    {
        #region ::::::::::::::::::::::::::::: ex Drawing :::::::::::::::::::::::::::
        /// <summary>
        /// ��ҳ���ܣ�����ʾ
        /// </summary>
        /// <param name="e"></param>
        private void DrawFrame(Graphics e)
        {
            for (int i = 0; i < 31; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen2, rects[i]);
            }
        }

        /// <summary>
        /// ����A�ڵ�ͼ��ͼ��
        /// �����ƶ�/�����ƶ�/Ŀ���ٶ�/Ŀ�����
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaA(Graphics e)
        {
            //A1
            //�����ƶ�/�����ƶ�
            if (bValue[2])
                e.FillRectangle(FormatStyle.RedSolidBrush, rects[0]);
            else if (bValue[3])
                e.FillRectangle(FormatStyle.YellowSolidBrush, rects[0]);

            //A2
            //Ŀ���ٶ�/Ŀ�����
            e.DrawString(Convert.ToInt32(fValue[0]).ToString(), FormatStyle.Font16B,
                FormatStyle.WhiteSolidBrush, rects[99], RightFormat);
            e.DrawImage(Img[5], pDrawPoint[0]);
            e.FillRectangle(FormatStyle.LightGreenSolidBrush,
                new RectangleF(60f, 413f - TargetDistance(fValue[1]), 25f, TargetDistance(fValue[1])));
        }

        /// <summary>
        /// ����B�ڵ�ͼ��ͼ��
        /// ����/�ٶ�ָ��/����/�ƶ��ٶ�
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaB(Graphics e)
        {
            if (bValue[18])
            {
                dialRM.OnDraw(e, FormatStyle.Font20);
                pointer = Img[0];
                speedPointerRM.PaintPointer(e, pointer, ref fValue[2]);
            }
            else
            {
                //B
                //����
                dial.OnDraw(e, FormatStyle.Font20);
                //e.DrawRectangle(FormatStyle.WhitePen, 0, 0, 800, 600);

                //�ٶ�ָ��/����/�ƶ��ٶ�
                if (Convert.ToInt32(fValue[2]) == 0) pointer = Img[0];
                else if (fValue[2] >= fValue[3] && fValue[2] < fValue[4]) pointer = Img[1];
                else if (fValue[2] >= fValue[4]) pointer = Img[2];
                else pointer = Img[0];
                speedPointer.PaintPointer(e, pointer, ref fValue[2]);

                xianSuPointer.PaintPointer(e, Img[3], ref fValue[3]);

                zhiDongPointer.PaintPointer(e, Img[4], ref fValue[4]);
            }

            e.DrawString(Convert.ToInt32(fValue[2]).ToString(), FormatStyle.Font22B, FormatStyle.WhiteSolidBrush, rects[94], RightFormat);
        }

        /// <summary>
        /// ����C�ڵ�ͼ��ͼ��
        /// ��������/����ģʽ��Ϣ/�����ƶ�״̬/˾����״̬
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaC(Graphics e)
        {
            //C1
            //��������
            for (int i = 0; i < 3; i++)
            {
                if (bValue[4 + i])
                {
                    e.DrawImage(Img[6 + i], rects[3]);
                }
            }
            //C2
            //����ģʽ��Ϣ
            for (int i = 0; i < 2; i++)
            {
                if (bValue[7 + i])
                {
                    e.DrawString(str1[i], FormatStyle.Font14B,
                        i == 0 ? FormatStyle.RedSolidBrush : FormatStyle.WhiteSolidBrush,
                        rects[42], drawFormat);
                }
            }
            //C3

            //C4
            //�����ƶ�״̬
            if (bValue[9])
                e.DrawImage(Img[9], rects[5]);

            //C5
            //˾����״̬
            for (int i = 0; i < 3; i++)
            {
                bool isdown = false;
                if (bValue[10 + i])
                {
                    e.DrawImage(Img[11 + i], rects[43]);
                    isdown = true;
                }
                if (bValue[13 + i])
                {
                    e.DrawImage(Img[14 + i], rects[43]);
                    isdown = true;
                }
                if (isdown)
                    e.DrawImage(Img[10], rects[43]);
            }
        }

        /// <summary>
        /// ����D�ڵ�ͼ��ͼ��
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaD(Graphics e)
        {

        }

        /// <summary>
        /// ����E�ڵ�ͼ��ͼ��
        /// �ı���Ϣ
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaE(Graphics e)
        {
            #region :::::::::::::::::::: �����ȶ���Ϣ ::::::::::::::::::::::
            if (MMI_MsgReceive.msgInf.NeedShowHighMsg)
            {
                if (MMI_MsgReceive.msgInf.MsgHasShowed(50) == 1)
                {
                    //�ƿ�
                    e.DrawRectangle(FormatStyle.YellowPen, rects[56]);
                    //��Ϣ
                    e.DrawString(MMI_MsgReceive.msgInf.ShowHighPriMsg.MsgName,
                        FormatStyle.Font16B, FormatStyle.WhiteSolidBrush,
                        rects[56], drawFormat);
                    //
                    btnCanDown[0] = false;
                    btnCanDown[1] = false;

                    showMsgSign = false;
                }
            }
            #endregion

            #region :::::::::::::::::::: �����ȶ���Ϣ ::::::::::::::::::::::
            else
            {            //
                e.DrawRectangle(FormatStyle.WhitePen2, rects[56]);

                //
                if (MMI_MsgReceive.msgInf != null)
                {
                    int count = MMI_MsgReceive.msgInf.MsgRecDic.Count;
                    int index = count - RowId;

                    btnCanDown[0] = RowId > 0 ? true : false;
                    btnCanDown[1] = index > 3 ? true : false;

                    if (count > 0 && count <= 3)
                    {
                        btnCanDown[0] = false;      //����
                        btnCanDown[1] = false;      //����
                        for (int i = 0; i < count; i++)
                        {
                            e.DrawString(MMI_MsgReceive.msgInf.MsgRecDic[count - i - 1].MsgStartTime.ToShortTimeString() +
                                "  " + MMI_MsgReceive.msgInf.MsgRecDic[count - i - 1].MsgName, FormatStyle.Font12B,
                                FormatStyle.WhiteSolidBrush, rects[62 + i], LeftFormat);
                        }
                    }
                    else if (count > 3)
                    {
                        for (int i = 0; i < (index > 3 ? 3 : index); i++)
                        {
                            e.DrawString(MMI_MsgReceive.msgInf.MsgRecDic[count - i - 1 - RowId].MsgStartTime.ToShortTimeString() +
                                "  " + MMI_MsgReceive.msgInf.MsgRecDic[count - i - 1 - RowId].MsgName, FormatStyle.Font12B,
                                FormatStyle.WhiteSolidBrush, rects[62 + i], LeftFormat);
                        }
                    }
                }
            }
            #endregion

            //��Ϣָʾ
            if (showMsgSign && timeIn)
                e.DrawImage(Img[41], rects[65]);

            for (int i = 0; i < 2; i++)
            {
                if (btnCanDown[i])
                {
                    if (buttonIsDown[i])
                        e.DrawImage(Img[33 + i * 2], rects[57 + i]);
                    else
                        e.DrawImage(Img[34 + i * 2], rects[57 + i]);
                }
                else
                    e.DrawImage(Img[33 + i * 2], rects[57 + i]);
            }
        }

        /// <summary>
        /// ����F�ڵ�ͼ��ͼ��
        /// �˵�/����/����
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaF(Graphics e)
        {
            //�˵����ܷ���
            if (btnCanDown[4])
            {
                if (buttonIsDown[4])
                    e.DrawImage(Img[43], rects[61]);
                else
                    e.DrawImage(Img[42], rects[61]);
            }
            else
                e.DrawImage(Img[43], rects[61]);
            e.DrawString("�˵�", FormatStyle.Font16B,
                FormatStyle.BlackSolidBrush, rects[61], drawFormat);

            //����
            if (btnCanDown[2])
            {
                if (buttonIsDown[2])
                    e.DrawImage(Img[37], rects[59]);
                else
                    e.DrawImage(Img[38], rects[59]);
            }
            else
                e.DrawImage(Img[37], rects[59]);

            //����
            if (btnCanDown[3])
            {
                if (buttonIsDown[3])
                    e.DrawImage(Img[39], rects[60]);
                else
                    e.DrawImage(Img[40], rects[60]);
            }
            else
                e.DrawImage(Img[39], rects[60]);
        }

        /// <summary>
        /// ����G�ڵ�ͼ��ͼ��
        /// ������ʾ/��ǰ����/��ǰʱ��
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaG(Graphics e)
        {
            //G1
            //������ʾ
            LifeIndicator();
            switch (liftIndic)
            {
                case 1:
                    e.FillRectangle(FormatStyle.LightGreySolidBrush, rects[44]);
                    e.FillRectangle(FormatStyle.DarkGreySolidBrush, rects[45]);
                    break;
                case 2:
                    e.FillRectangle(FormatStyle.DarkGreySolidBrush, rects[44]);
                    e.FillRectangle(FormatStyle.LightGreySolidBrush, rects[45]);
                    break;
                case 3:
                    e.FillRectangle(FormatStyle.LightGreySolidBrush, rects[45]);
                    e.FillRectangle(FormatStyle.DarkGreySolidBrush, rects[46]);
                    break;
                case 4:
                    e.FillRectangle(FormatStyle.DarkGreySolidBrush, rects[45]);
                    e.FillRectangle(FormatStyle.LightGreySolidBrush, rects[46]);
                    break;
                case 5:
                    e.FillRectangle(FormatStyle.LightGreySolidBrush, rects[46]);
                    e.FillRectangle(FormatStyle.DarkGreySolidBrush, rects[47]);
                    break;
                case 6:
                    e.FillRectangle(FormatStyle.DarkGreySolidBrush, rects[46]);
                    e.FillRectangle(FormatStyle.LightGreySolidBrush, rects[47]);
                    break;
                case 7:
                    e.FillRectangle(FormatStyle.LightGreySolidBrush, rects[47]);
                    e.FillRectangle(FormatStyle.DarkGreySolidBrush, rects[100]);
                    break;
                case 8:
                    e.FillRectangle(FormatStyle.DarkGreySolidBrush, rects[100]);
                    e.FillRectangle(FormatStyle.LightGreySolidBrush, rects[44]);
                    break;
            }
            //G2
            //��ǰ����
            e.DrawString(DateTime.Today.ToString("MM") + "/" + DateTime.Today.ToString("dd")
                + "/" + DateTime.Today.ToString("yyyy"), FormatStyle.Font16B,
                FormatStyle.WhiteSolidBrush, rects[54], drawFormat);

            //G3
            //��ǰʱ��
            e.DrawString(DateTime.Now.ToLongTimeString(), FormatStyle.Font16B,
                FormatStyle.WhiteSolidBrush, rects[55], drawFormat);
        }

        /// <summary>
        /// ����K�ڵ�ͼ��ͼ��
        /// ��һվ/�յ�վ/����ʱ��
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaK(Graphics e)
        {
            for (int i = 0; i < 3; i++)
            {
                e.DrawString(str5[i], FormatStyle.Font12B, FormatStyle.WhiteSolidBrush, 
                    rects[48 + i], drawFormat);
            }

            //k1 ��һվ        
            e.DrawString(TrainStation(Convert.ToInt32(fValue[6]), nextStation),
                FormatStyle.Font14B, FormatStyle.WhiteSolidBrush, rects[51], drawFormat);
            //k2 �յ�վ
            e.DrawString(TrainStation(Convert.ToInt32(fValue[7]), endStation),
                FormatStyle.Font14B, FormatStyle.WhiteSolidBrush, rects[52], drawFormat);
            //k3 ����ʱ��
            e.DrawString(Drived(fValue[8]), FormatStyle.Font14B,
                FormatStyle.WhiteSolidBrush, rects[53], drawFormat);
        }

        /// <summary>
        /// ����M�ڵ�ͼ��ͼ��
        /// ��ǰѡ���ģʽ/��ǰ��Ӫ�ļ���/�г��۷�/
        /// �г�ͣվ/����״̬/�г���վ��Ϣ/���ſ���ģʽ/
        /// ������Ϣ/����ͨ���ж�/������Ϣ
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaM(Graphics e)
        {
            //M1
            //��ǰѡ���ģʽ
            for (int i = 0; i < 4; i++)
            {
                if (bValue[16 + i])
                {
                    e.DrawString(str2[i], FormatStyle.Font26B,
                        i == 3 ? FormatStyle.RedSolidBrush : FormatStyle.WhiteSolidBrush,
                        rects[17], drawFormat);
                }
            }
            //M2
            //��ǰ��Ӫ�ļ���
            for (int i = 0; i < 3; i++)
            {
                if (bValue[20 + i])
                {
                    e.DrawString(str3[i], FormatStyle.Font26B,
                        FormatStyle.WhiteSolidBrush, rects[22], drawFormat);
                }
            }
            //M3
            //�г��۷�
            if (bValue[23])
            {
                if (flashTimers[0].IsNeedFlash())
                    e.DrawImage(Img[17], rects[18]);
            }
            else if (bValue[24])
                e.DrawImage(Img[18], rects[18]);

            //M4
            //�г�ͣվ
            if (!bValue[18])
            {
                for (int i = 0; i < 2; i++)
                {
                    if (bValue[25 + i])
                        e.DrawImage(Img[19 + i], rects[23]);
                }
            }

            //M5
            //����״̬
            for (int i = 0; i < 4; i++)
            {
                if (bValue[27 + i])
                    e.DrawImage(Img[21 + i], rects[19]);
            }

            //M6
            //�г���վ��Ϣ
            for (int i = 0; i < 2; i++)
            {
                if (bValue[31 + i])
                    e.DrawImage(Img[25 + i], rects[24]);
            }

            //M7
            //���ſ���ģʽ
            for (int i = 0; i < 3; i++)
            {
                if (bValue[33 + i])
                    e.DrawString(str4[i], FormatStyle.Font26B, FormatStyle.WhiteSolidBrush,
                        rects[20], drawFormat);
            }

            //M8
            //������Ϣ
            if (bValue[36])
                e.DrawImage(Img[27], rects[25]);
            else if (bValue[38])
            {
                if (flashTimers[1].IsNeedFlash())
                    e.DrawImage(Img[28], rects[25]);
            }
            else if (bValue[37])
                e.DrawImage(Img[28], rects[25]);
            else if (bValue[39])
                e.DrawImage(Img[29], rects[25]);
            else if (bValue[40])
                e.DrawImage(Img[30], rects[25]);

            //M9
            //����ͨ���ж�
            if (bValue[41])
                e.DrawImage(Img[31], rects[21]);

            //M10
            //������Ϣ
            if (bValue[42])
                e.DrawImage(Img[32], rects[26]);
        }

        /// <summary>
        /// ����N�ڵ�ͼ��ͼ��
        /// ��վ����ʱ
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaN(Graphics e)
        {
            int stopTime = Convert.ToInt32(fValue[9]);
            if (stopTime > 99) stopTime = 99;
            if (stopTime < 0) stopTime = 0;
            if (bValue[1])
            {
                e.DrawString("����:", FormatStyle.Font14B,
                    FormatStyle.WhiteSolidBrush, rects[27], drawFormat);
                e.DrawString(stopTime.ToString(), FormatStyle.Font24B,
                    FormatStyle.WhiteSolidBrush, rects[8], drawFormat);
            }
        }

        /// <summary>
        /// ����T�ڵ�ͼ��ͼ��
        /// �����/Ŀ�ĺ�/����
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaT(Graphics e)
        {
            e.DrawString("T" +
                (Convert.ToInt32(fValue[10]) == 0 ? " ---- " : Convert.ToInt32(fValue[10]).ToString("0000")),
                FormatStyle.Font16B, FormatStyle.WhiteSolidBrush, rects[28], drawFormat);
        
            e.DrawString(Convert.ToInt32(fValue[11]) == 0 ? " -- " : Convert.ToInt32(fValue[11]).ToString("00"),
                FormatStyle.Font16B, FormatStyle.WhiteSolidBrush, rects[29], drawFormat);

            e.DrawRectangle(FormatStyle.WhitePen, rects[30]);

            if (btnCanDown[5])
            {
                if (buttonIsDown[5])
                    e.DrawImage(Img[44], rects[30]);
                else
                    e.DrawImage(Img[42], rects[30]);
            }
            else
                e.DrawImage(Img[43], rects[30]);

            e.DrawString("C" + (Convert.ToInt32(fValue[12]) == 0 ? " --- " : Convert.ToInt32(fValue[12]).ToString("000")),
                FormatStyle.Font18B, FormatStyle.BlackSolidBrush, rects[30], drawFormat);
        }

        /// <summary>
        /// ���Ž���
        /// </summary>
        /// <param name="e"></param>
        private void DrawCrewNumb(Graphics e)
        {
            e.DrawImage(Img[45], pDrawPoint[2]);
            e.DrawString(crowNumb, FormatStyle.Font26B,
                FormatStyle.BlackSolidBrush, rects[66], RightFormat);

            for (int i = 0; i < 13; i++)
            {
                if (buttonIsDown[6 + i])
                {
                    e.DrawImage(Img[44], rects[67 + i]);
                }
            }

            for (int i = 0; i < 13; i++)
            {
                e.DrawString(str6[i], FormatStyle.Font24B, 
                    FormatStyle.BlackSolidBrush, rects[67 + i], drawFormat);
            }
            e.DrawImage(Img[47], rects[77]);
            e.DrawImage(Img[46], rects[79]);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="e"></param>
        private void DrawSound(Graphics e)
        {
            e.DrawImage(Img[49], rects[88]);
            for (int i = 0; i < 3; i++)
            {
                if (buttonIsDown[26 + i])
                    e.DrawImage(Img[51], rects[89 + i]);
                e.DrawString(str8[i], FormatStyle.Font32B,
                    FormatStyle.BlackSolidBrush, rects[89 + i], drawFormat);

                if (soundProgress > 0)
                    e.FillRectangle(FormatStyle.BlueSolidBrush, new Rectangle(pDrawPoint[3], new Size(soundProgress * 39, 48)));
            }
        }

        /// <summary>
        /// ���Ƚ���
        /// </summary>
        /// <param name="e"></param>
        private void DrawBrightness(Graphics e)
        {
            e.DrawImage(Img[50], rects[88]);
            for (int i = 0; i < 3; i++)
            {
                if (buttonIsDown[26 + i])
                    e.DrawImage(Img[51], rects[89 + i]);
                e.DrawString(str8[i], FormatStyle.Font32B,
                    FormatStyle.BlackSolidBrush, rects[89 + i], drawFormat);
            }
            if(buttonIsDown[29])
                e.DrawImage(Img[51], rects[89 + 3]);
            e.DrawString(str8[3], FormatStyle.Font12B,
                    FormatStyle.BlackSolidBrush, rects[89 + 3], drawFormat);

            if (_brightProgress > 0)
                e.FillRectangle(FormatStyle.BlueSolidBrush, new Rectangle(pDrawPoint[3], new Size(_brightProgress * 39, 48)));
        }

        /// <summary>
        /// �˵�����
        /// </summary>
        /// <param name="e"></param>
        private void DrawMenu(Graphics e)
        {
            e.DrawImage(Img[48], pDrawPoint[2]);

            if (btnViewId == 1)
            {
                e.DrawString("�˵�", FormatStyle.Font22B,
                    FormatStyle.WhiteSolidBrush, rects[80], drawFormat);

                for (int i = 0; i < 7; i++)
                {
                    if(i == 4 || i == 5) continue;
                    if (buttonIsDown[19 + i])
                        e.DrawImage(Img[44], rects[81 + i]);
                    else
                        e.DrawImage(Img[42], rects[81 + i]);

                    e.DrawString(str7[i], FormatStyle.Font22B,
                        FormatStyle.BlackSolidBrush, rects[81 + i], drawFormat);
                }
            }
        }

        /// <summary>
        /// BMģʽȷ��
        /// </summary>
        /// <param name="e"></param>
        private void DrawACK(Graphics e)
        {
            e.FillRectangle(FormatStyle.BlackSolidBrush, rects[98]);
            e.DrawRectangle(FormatStyle.YellowPen, rects[98]);
            e.DrawString("AM-I preselection", FormatStyle.Font14B, FormatStyle.WhiteSolidBrush,
                rects[97], drawFormat);
            for (int i = 0; i < 2; i++ )
            {
                if (buttonIsDown[30 + i])
                    e.DrawImage(Img[44], rects[95 + i]);
                else
                    e.DrawImage(Img[42], rects[95 + i]);

                e.DrawString(str9[i], FormatStyle.Font18B, FormatStyle.BlackSolidBrush,
                    rects[95 + i], drawFormat);
            }
        }
        #endregion
    }
}
