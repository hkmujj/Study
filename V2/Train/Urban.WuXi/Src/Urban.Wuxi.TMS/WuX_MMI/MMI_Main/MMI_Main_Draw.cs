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
        /// 主页面框架，不显示
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
        /// 区域A内的图形图像
        /// 请求制动/紧急制动/目标速度/目标距离
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaA(Graphics e)
        {
            //A1
            //请求制动/紧急制动
            if (bValue[2])
                e.FillRectangle(FormatStyle.RedSolidBrush, rects[0]);
            else if (bValue[3])
                e.FillRectangle(FormatStyle.YellowSolidBrush, rects[0]);

            //A2
            //目标速度/目标距离
            e.DrawString(Convert.ToInt32(fValue[0]).ToString(), FormatStyle.Font16B,
                FormatStyle.WhiteSolidBrush, rects[99], RightFormat);
            e.DrawImage(Img[5], pDrawPoint[0]);
            e.FillRectangle(FormatStyle.LightGreenSolidBrush,
                new RectangleF(60f, 413f - TargetDistance(fValue[1]), 25f, TargetDistance(fValue[1])));
        }

        /// <summary>
        /// 区域B内的图形图像
        /// 表盘/速度指针/限速/制动速度
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
                //表盘
                dial.OnDraw(e, FormatStyle.Font20);
                //e.DrawRectangle(FormatStyle.WhitePen, 0, 0, 800, 600);

                //速度指针/限速/制动速度
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
        /// 区域C内的图形图像
        /// 机车工况/运行模式信息/车辆制动状态/司机室状态
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaC(Graphics e)
        {
            //C1
            //机车工况
            for (int i = 0; i < 3; i++)
            {
                if (bValue[4 + i])
                {
                    e.DrawImage(Img[6 + i], rects[3]);
                }
            }
            //C2
            //运行模式信息
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
            //车辆制动状态
            if (bValue[9])
                e.DrawImage(Img[9], rects[5]);

            //C5
            //司机室状态
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
        /// 区域D内的图形图像
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaD(Graphics e)
        {

        }

        /// <summary>
        /// 区域E内的图形图像
        /// 文本消息
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaE(Graphics e)
        {
            #region :::::::::::::::::::: 高优先度消息 ::::::::::::::::::::::
            if (MMI_MsgReceive.msgInf.NeedShowHighMsg)
            {
                if (MMI_MsgReceive.msgInf.MsgHasShowed(50) == 1)
                {
                    //黄框
                    e.DrawRectangle(FormatStyle.YellowPen, rects[56]);
                    //消息
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

            #region :::::::::::::::::::: 低优先度消息 ::::::::::::::::::::::
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
                        btnCanDown[0] = false;      //向上
                        btnCanDown[1] = false;      //向下
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

            //消息指示
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
        /// 区域F内的图形图像
        /// 菜单/声音/亮度
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaF(Graphics e)
        {
            //菜单键能否按下
            if (btnCanDown[4])
            {
                if (buttonIsDown[4])
                    e.DrawImage(Img[43], rects[61]);
                else
                    e.DrawImage(Img[42], rects[61]);
            }
            else
                e.DrawImage(Img[43], rects[61]);
            e.DrawString("菜单", FormatStyle.Font16B,
                FormatStyle.BlackSolidBrush, rects[61], drawFormat);

            //声音
            if (btnCanDown[2])
            {
                if (buttonIsDown[2])
                    e.DrawImage(Img[37], rects[59]);
                else
                    e.DrawImage(Img[38], rects[59]);
            }
            else
                e.DrawImage(Img[37], rects[59]);

            //亮度
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
        /// 区域G内的图形图像
        /// 生命显示/当前日期/当前时间
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaG(Graphics e)
        {
            //G1
            //生命显示
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
            //当前日期
            e.DrawString(DateTime.Today.ToString("MM") + "/" + DateTime.Today.ToString("dd")
                + "/" + DateTime.Today.ToString("yyyy"), FormatStyle.Font16B,
                FormatStyle.WhiteSolidBrush, rects[54], drawFormat);

            //G3
            //当前时间
            e.DrawString(DateTime.Now.ToLongTimeString(), FormatStyle.Font16B,
                FormatStyle.WhiteSolidBrush, rects[55], drawFormat);
        }

        /// <summary>
        /// 区域K内的图形图像
        /// 下一站/终点站/发车时间
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaK(Graphics e)
        {
            for (int i = 0; i < 3; i++)
            {
                e.DrawString(str5[i], FormatStyle.Font12B, FormatStyle.WhiteSolidBrush, 
                    rects[48 + i], drawFormat);
            }

            //k1 下一站        
            e.DrawString(TrainStation(Convert.ToInt32(fValue[6]), nextStation),
                FormatStyle.Font14B, FormatStyle.WhiteSolidBrush, rects[51], drawFormat);
            //k2 终点站
            e.DrawString(TrainStation(Convert.ToInt32(fValue[7]), endStation),
                FormatStyle.Font14B, FormatStyle.WhiteSolidBrush, rects[52], drawFormat);
            //k3 发车时间
            e.DrawString(Drived(fValue[8]), FormatStyle.Font14B,
                FormatStyle.WhiteSolidBrush, rects[53], drawFormat);
        }

        /// <summary>
        /// 区域M内的图形图像
        /// 当前选择的模式/当前运营的级别/列车折返/
        /// 列车停站/车门状态/列车离站信息/车门控制模式/
        /// 紧急信息/无线通信中断/特殊信息
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaM(Graphics e)
        {
            //M1
            //当前选择的模式
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
            //当前运营的级别
            for (int i = 0; i < 3; i++)
            {
                if (bValue[20 + i])
                {
                    e.DrawString(str3[i], FormatStyle.Font26B,
                        FormatStyle.WhiteSolidBrush, rects[22], drawFormat);
                }
            }
            //M3
            //列车折返
            if (bValue[23])
            {
                if (flashTimers[0].IsNeedFlash())
                    e.DrawImage(Img[17], rects[18]);
            }
            else if (bValue[24])
                e.DrawImage(Img[18], rects[18]);

            //M4
            //列车停站
            if (!bValue[18])
            {
                for (int i = 0; i < 2; i++)
                {
                    if (bValue[25 + i])
                        e.DrawImage(Img[19 + i], rects[23]);
                }
            }

            //M5
            //车门状态
            for (int i = 0; i < 4; i++)
            {
                if (bValue[27 + i])
                    e.DrawImage(Img[21 + i], rects[19]);
            }

            //M6
            //列车离站信息
            for (int i = 0; i < 2; i++)
            {
                if (bValue[31 + i])
                    e.DrawImage(Img[25 + i], rects[24]);
            }

            //M7
            //车门控制模式
            for (int i = 0; i < 3; i++)
            {
                if (bValue[33 + i])
                    e.DrawString(str4[i], FormatStyle.Font26B, FormatStyle.WhiteSolidBrush,
                        rects[20], drawFormat);
            }

            //M8
            //紧急信息
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
            //无线通信中断
            if (bValue[41])
                e.DrawImage(Img[31], rects[21]);

            //M10
            //特殊信息
            if (bValue[42])
                e.DrawImage(Img[32], rects[26]);
        }

        /// <summary>
        /// 区域N内的图形图像
        /// 离站倒计时
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaN(Graphics e)
        {
            int stopTime = Convert.ToInt32(fValue[9]);
            if (stopTime > 99) stopTime = 99;
            if (stopTime < 0) stopTime = 0;
            if (bValue[1])
            {
                e.DrawString("发车:", FormatStyle.Font14B,
                    FormatStyle.WhiteSolidBrush, rects[27], drawFormat);
                e.DrawString(stopTime.ToString(), FormatStyle.Font24B,
                    FormatStyle.WhiteSolidBrush, rects[8], drawFormat);
            }
        }

        /// <summary>
        /// 区域T内的图形图像
        /// 服务号/目的号/工号
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
        /// 工号界面
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
        /// 声音界面
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
        /// 亮度界面
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
        /// 菜单界面
        /// </summary>
        /// <param name="e"></param>
        private void DrawMenu(Graphics e)
        {
            e.DrawImage(Img[48], pDrawPoint[2]);

            if (btnViewId == 1)
            {
                e.DrawString("菜单", FormatStyle.Font22B,
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
        /// BM模式确认
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
