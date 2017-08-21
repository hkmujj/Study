using System;
using System.Collections.Generic;
using System.Drawing;
using Subway.ATC.Casco.Common;
using Subway.ATC.Casco.Config;
using Subway.ATC.Casco.MMI_Message;
using Subway.ATC.Casco.Resource;

namespace Subway.ATC.Casco.MMI_Main
{
    // ReSharper disable once InconsistentNaming
    public partial class MMI_Main
    {
        /// <summary>
        /// 区域A内的图形图像
        /// 请求制动/紧急制动/目标速度/目标距离
        /// </summary>
        /// <param name="g"></param>
        private void DrawAreaA(Graphics g)
        {
            //A1
            //请求制动/紧急制动
            if (BValue[2])
                g.FillRectangle(FormatStyle.RedSolidBrush, Rects[0]);
            else if (BValue[3])
                g.FillRectangle(FormatStyle.YellowSolidBrush, Rects[0]);

            //A2
            //目标速度/目标距离
            if (BValue[45])
            {
                g.DrawString(Convert.ToInt32(FValue[0]).ToString(), FormatStyle.Font16B,
                    FormatStyle.WhiteSolidBrush, Rects[99], RightFormat);
                g.DrawImage(Img[5], PDrawPoint[0]);
                g.FillRectangle(GetTagetDistanceBrush(),
                    new RectangleF(60f, 413f - TargetDistance(FValue[1]), 25f, TargetDistance(FValue[1])));
            }
        }

        private SolidBrush GetTagetDistanceBrush()
        {
            if (FValue[1] > 300)
            {
                return FormatStyle.LightGreenSolidBrush;
            }
            if (FValue[1] <= 300 && FValue[1] >= 150)
            {
                if (FValue[2] > 25)
                {
                    return FormatStyle.LightGreenSolidBrush;
                }
                return FormatStyle.YellowSolidBrush;
            }
            if (FValue[2] >= 60)
            {
                return FormatStyle.LightGreenSolidBrush;
            }
            if (FValue[2] > 0)
            {
                return FormatStyle.YellowSolidBrush;
            }
            return FormatStyle.RedSolidBrush;
        }
        /// <summary>
        /// 区域B内的图形图像
        /// 表盘/速度指针/限速/制动速度
        /// </summary>
        /// <param name="g"></param>
        private void DrawAreaB(Graphics g)
        {

            g.DrawString("km/h", FormatStyle.Font16, FormatStyle.WhiteSolidBrush, Rects[101]);
            //指针状态 根据制动状态判断
            var sppedState = BValue[2]
                ? SpeedPointerState.Emergent
                : BValue[3] ? SpeedPointerState.Warning : SpeedPointerState.Normal;

            var speedPointerImage = GetSpeedPoionterImage(sppedState);

            // 洗车模式
            if (BValue[52])
            {
                m_WmDial.OnDraw(g, FormatStyle.Font20);
                m_SpeedPointerWm.PaintPointer(g, speedPointerImage, FValue[2]);
            }
            else if (BValue[18])
            {
                if (!BValue[44])
                {
                    DialRm.OnDraw(g, FormatStyle.Font20);
                    SpeedPointerRm.PaintPointer(g, speedPointerImage, FValue[2]);
                }
                else
                {
                    m_DialTx.OnDraw(g, FormatStyle.Font20);
                    m_SpeedPointerTx.PaintPointer(g, speedPointerImage, FValue[2]);
                }
            }
            else
            {
                //B
                //表盘
                Dial.OnDraw(g, FormatStyle.Font20);

                SpeedPointer.PaintPointer(g, speedPointerImage, FValue[2]);

                XianSuPointer.PaintPointer(g, Img[3], FValue[3]);

                ZhiDongPointer.PaintPointer(g, Img[4], FValue[4]);
            }

            g.DrawString(Convert.ToInt32(FValue[2]).ToString(), FormatStyle.Font22B, FormatStyle.WhiteSolidBrush,
                Rects[94], RightFormat);
        }

        /// <summary>
        /// 区域C内的图形图像
        /// 机车工况/运行模式信息/车辆制动状态/司机室状态
        /// </summary>
        /// <param name="g"></param>
        private void DrawAreaC(Graphics g)
        {
            //C1
            //机车工况
            for (int i = 0; i < 3; i++)
            {
                if (BValue[4 + i])
                {
                    g.DrawImage(Img[6 + i], Rects[3]);
                }
            }
            //C2
            //运行模式信息
            for (int i = 0; i < 2; i++)
            {
                if (BValue[7 + i])
                {
                    g.DrawString(ConfigManager.Instance.CascoATCConfig.UsedEnv == UsedEnv.XiaMen ? PreselectionModeXiaMne[i] : PreselectionModeNormal[i], FormatStyle.Font14B,
                        i == 0 ? FormatStyle.RedSolidBrush : FormatStyle.WhiteSolidBrush,
                        Rects[42], DrawFormat);
                }
            }
            if (BoolList[61])
            {
                if (DateTime.Now.Second % 2 == 0)
                {
                    g.DrawString(ConfigManager.Instance.CascoATCConfig.UsedEnv == UsedEnv.XiaMen ? PreselectionModeXiaMne[0] : PreselectionModeNormal[0], FormatStyle.Font14B,
                  FormatStyle.RedSolidBrush,
                  Rects[42], DrawFormat);
                }

            }
            //C3

            //C4
            //车辆制动状态
            if (BValue[9])
            {
                g.DrawImage(Img[9], Rects[5]);
            }

            DrawAreaC5(g);
        }

        /// <summary>
        ///           //C5
        //司机室状态
        /// </summary>
        /// <param name="g"></param>
        private void DrawAreaC5(Graphics g)
        {
            bool isdown = false;
            for (int i = 0; i < 3; i++)
            {
                if (BValue[10 + i])
                {
                    g.DrawImage(Img[11 + i], Rects[43]);
                    isdown = true;
                }
                if (BValue[13 + i])
                {
                    g.DrawImage(Img[14 + i], Rects[43]);
                    isdown = true;
                }
            }
            // 默认为未激活
            if (!isdown)
            {
                g.DrawImage(Img[11], Rects[43]);
                g.DrawImage(Img[14], Rects[43]);
            }

            var atcState = ATCState.Normal;
            if (BValue[50])
            {
                atcState = ATCState.PartFault;
            }
            if (BValue[49])
            {
                atcState = ATCState.FullFault;
            }

            g.FillRectangle(atcState.GetStateBrush(), Rects[43].X, Rects[43].Y + 21, Rects[43].Width, 22);
        }

        /// <summary>
        /// 区域D内的图形图像
        /// </summary>
        /// <param name="e"></param>
        private void DrawAreaD(Graphics g)
        {

        }

        /// <summary>
        /// 区域E内的图形图像
        /// 文本消息
        /// </summary>
        /// <param name="g"></param>
        private void DrawAreaE(Graphics g)
        {
            #region :::::::::::::::::::: 高优先度消息 ::::::::::::::::::::::

            if (MMI_MsgReceive.msgInf.NeedShowHighMsg)
            {
                if (MMI_MsgReceive.msgInf.MsgHasShowed(50) == 1)
                {
                    //黄框
                    g.DrawRectangle(FormatStyle.YellowPen, Rects[56]);
                    //消息
                    g.DrawString(MMI_MsgReceive.msgInf.HighMsg.MsgContent,
                        FormatStyle.Font16B, FormatStyle.WhiteSolidBrush,
                        Rects[56], DrawFormat);
                    //
                    m_BtnCanDown[0] = false;
                    m_BtnCanDown[1] = false;

                    m_ShowMsgSign = false;
                }
            }
            #endregion

            #region :::::::::::::::::::: 低优先度消息 ::::::::::::::::::::::

            else
            {
                //
                g.DrawRectangle(FormatStyle.WhitePen2, Rects[56]);

                //
                if (MMI_MsgReceive.msgInf != null)
                {
                    int count = MMI_MsgReceive.msgInf.AllMsgsList.Count;
                    int index = count - m_RowId;

                    m_BtnCanDown[0] = m_RowId > 0 ? true : false;
                    m_BtnCanDown[1] = index > 3 ? true : false;

                    if (count > 0 && count <= 3)
                    {
                        m_BtnCanDown[0] = false; //向上
                        m_BtnCanDown[1] = false; //向下
                        for (int i = 0; i < count; i++)
                        {
                            g.DrawString(
                                MMI_MsgReceive.msgInf.AllMsgsList[count - i - 1].MsgReceiveTime.ToShortTimeString() +
                                "  " + MMI_MsgReceive.msgInf.AllMsgsList[count - i - 1].MsgContent, FormatStyle.Font12B,
                                FormatStyle.WhiteSolidBrush, Rects[62 + i], LeftFormat);
                        }
                    }
                    else if (count > 3)
                    {
                        for (int i = 0; i < (index > 3 ? 3 : index); i++)
                        {
                            g.DrawString(
                                MMI_MsgReceive.msgInf.AllMsgsList[count - i - 1 - m_RowId].MsgReceiveTime
                                    .ToShortTimeString() +
                                "  " + MMI_MsgReceive.msgInf.AllMsgsList[count - i - 1 - m_RowId].MsgContent,
                                FormatStyle.Font12B,
                                FormatStyle.WhiteSolidBrush, Rects[62 + i], LeftFormat);
                        }
                    }
                }
            }

            #endregion

            //消息指示
            if (m_ShowMsgSign && m_TimeIn)
            {
                g.DrawImage(Img[41], Rects[65]);
            }

            for (int i = 0; i < 2; i++)
            {
                if (m_BtnCanDown[i])
                {
                    g.DrawImage(ButtonIsDown[i] ? Img[33 + i * 2] : Img[34 + i * 2], Rects[57 + i]);
                }
                else
                {
                    g.DrawImage(Img[33 + i * 2], Rects[57 + i]);
                }
            }
        }

        /// <summary>
        /// 区域F内的图形图像
        /// 菜单/声音/亮度
        /// </summary>
        /// <param name="g"></param>
        private void DrawAreaF(Graphics g)
        {
            //菜单键能否按下
            if (m_BtnCanDown[4])
            {
                g.DrawImage(ButtonIsDown[4] ? Img[43] : Img[42], Rects[61]);
            }
            else
            {
                g.DrawImage(Img[43], Rects[61]);
            }
            g.DrawString("菜单", FormatStyle.Font16B,
                FormatStyle.BlackSolidBrush, Rects[61], DrawFormat);

            //声音
            if (m_BtnCanDown[2])
            {
                g.DrawImage(ButtonIsDown[2] ? Img[37] : Img[38], Rects[59]);
            }
            else
            {
                g.DrawImage(Img[37], Rects[59]);
            }

            //亮度
            if (m_BtnCanDown[3])
            {
                g.DrawImage(ButtonIsDown[3] ? Img[39] : Img[40], Rects[60]);
            }
            else
            {
                g.DrawImage(Img[39], Rects[60]);
            }
        }

        /// <summary>
        /// 区域G内的图形图像
        /// 生命显示/当前日期/当前时间
        /// </summary>
        /// <param name="g"></param>
        private void DrawAreaG(Graphics g)
        {
            //G1
            //生命显示
            LifeIndicator();
            switch (LiftIndic)
            {
                case 1:
                    g.FillRectangle(FormatStyle.LightGreySolidBrush, Rects[44]);
                    g.FillRectangle(FormatStyle.DarkGreySolidBrush, Rects[45]);
                    break;
                case 2:
                    g.FillRectangle(FormatStyle.DarkGreySolidBrush, Rects[44]);
                    g.FillRectangle(FormatStyle.LightGreySolidBrush, Rects[45]);
                    break;
                case 3:
                    g.FillRectangle(FormatStyle.LightGreySolidBrush, Rects[45]);
                    g.FillRectangle(FormatStyle.DarkGreySolidBrush, Rects[46]);
                    break;
                case 4:
                    g.FillRectangle(FormatStyle.DarkGreySolidBrush, Rects[45]);
                    g.FillRectangle(FormatStyle.LightGreySolidBrush, Rects[46]);
                    break;
                case 5:
                    g.FillRectangle(FormatStyle.LightGreySolidBrush, Rects[46]);
                    g.FillRectangle(FormatStyle.DarkGreySolidBrush, Rects[47]);
                    break;
                case 6:
                    g.FillRectangle(FormatStyle.DarkGreySolidBrush, Rects[46]);
                    g.FillRectangle(FormatStyle.LightGreySolidBrush, Rects[47]);
                    break;
                case 7:
                    g.FillRectangle(FormatStyle.LightGreySolidBrush, Rects[47]);
                    g.FillRectangle(FormatStyle.DarkGreySolidBrush, Rects[100]);
                    g.FillRectangle(FormatStyle.DarkGreySolidBrush, Rects[47]);
                    g.FillRectangle(FormatStyle.LightGreySolidBrush, Rects[100]);
                    break;
            }
            //G2
            //当前日期
            g.DrawString(DateTime.Today.ToString("MM") + "/" + DateTime.Today.ToString("dd")
                         + "/" + DateTime.Today.ToString("yyyy"), FormatStyle.Font16B,
                FormatStyle.WhiteSolidBrush, Rects[54], DrawFormat);

            //G3
            //当前时间
            g.DrawString(TheTimeNow.CurrentTime.ToLongTimeString(), FormatStyle.Font16B,
                FormatStyle.WhiteSolidBrush, Rects[55], DrawFormat);
        }

        /// <summary>
        /// 区域K内的图形图像
        /// 下一站/终点站/发车时间
        /// </summary>
        /// <param name="g"></param>
        private void DrawAreaK(Graphics g)
        {
            for (int i = 0; i < 3; i++)
            {
                g.DrawString(Str5[i], FormatStyle.Font12B, FormatStyle.WhiteSolidBrush,
                    Rects[48 + i], DrawFormat);
            }

            //k1 下一站        
            g.DrawString(TrainStation(Convert.ToInt32(FValue[6]), NextStation),
                FormatStyle.Font12B, FormatStyle.WhiteSolidBrush, Rects[51], DrawFormat);
            //k2 终点站
            g.DrawString(TrainStation(Convert.ToInt32(FValue[7]), EndStation),
                FormatStyle.Font12B, FormatStyle.WhiteSolidBrush, Rects[52], DrawFormat);
            //k3 发车时间
            g.DrawString(Drived(FValue[8]), FormatStyle.Font14B,
                FormatStyle.WhiteSolidBrush, Rects[53], DrawFormat);
        }

        private IEnumerable<bool> GetCurrentModels()
        {
            for (int i = 0; i < 4; i++)
            {
                yield return BValue[16 + i];
            }
            yield return BValue[52];
        }

        private IEnumerable<bool> GetCurrentLevels()
        {
            for (int i = 0; i < 3; i++)
            {
                yield return BValue[20 + i];
            }
            yield return BValue[52];
        }

        /// <summary>
        /// 区域M内的图形图像
        /// 当前选择的模式/当前运营的级别/列车折返/
        /// 列车停站/车门状态/列车离站信息/车门控制模式/
        /// 紧急信息/无线通信中断/特殊信息
        /// </summary>
        /// <param name="g"></param>
        private void DrawAreaM(Graphics g)
        {
            //M1
            //当前选择的模式
            var index = 0;
            foreach (var currentModel in GetCurrentModels())
            {

                if (currentModel)
                {
                    g.DrawString(ConfigManager.Instance.CascoATCConfig.UsedEnv == UsedEnv.XiaMen ? CurrentModelXiaMen[index] : CurrentModelNormal[index], FormatStyle.Font26B,
                        index == 3 ? FormatStyle.RedSolidBrush : FormatStyle.WhiteSolidBrush,
                        Rects[17], DrawFormat);
                    break;
                }
                index++;
            }

            //M2
            //当前运营的级别
            index = 0;
            foreach (var currentLevel in GetCurrentLevels())
            {

                if (currentLevel)
                {
                    g.DrawString(Str3[index], FormatStyle.Font26B,
                        FormatStyle.WhiteSolidBrush, Rects[22], DrawFormat);
                    break;
                }
                index++;

            }

            //M3
            //列车折返
            if (BValue[23])
            {
                if (FlashTimers[0].IsNeedFlash())
                    g.DrawImage(Img[17], Rects[18]);
            }
            else if (BValue[24])
            {
                g.DrawImage(Img[18], Rects[18]);
            }

            //M4
            //列车停站
            //if (!bValue[18])
            //{
            for (int i = 0; i < 2; i++)
            {
                if (BValue[25 + i])
                {
                    g.DrawImage(Img[19 + i], Rects[23]);
                }
            }
            //}

            //M5
            //车门状态
            for (int i = 0; i < 4; i++)
            {
                if (BValue[27 + i])
                {
                    g.DrawImage(Img[21 + i], Rects[19]);
                }
            }
            //车门预开门侧  M5 
            for (int i = 0; i < 3; ++i)
            {
                if (BValue[53 + i] && DateTime.Now.Second % 2 == 0)
                {
                    g.DrawImage(Img[21 + i], Rects[19]);
                }
            }
            //M6
            //列车离站信息
            if (BValue[46])
            {
                g.DrawImage(Img[52], Rects[24]);
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    if (BValue[31 + i])
                    {
                        g.DrawImage(Img[25 + i], Rects[24]);
                    }
                }
            }

            //M7
            //车门控制模式
            for (int i = 0; i < 3; i++)
            {
                if (BValue[33 + i])
                {
                    g.DrawString(Str4[i], FormatStyle.Font26B, FormatStyle.WhiteSolidBrush,
                        Rects[20], DrawFormat);
                }
            }

            //M8
            //紧急信息
            if (BValue[36])
            {
                g.DrawImage(Img[27], Rects[25]);
            }
            else if (BValue[38])
            {
                if (FlashTimers[1].IsNeedFlash())
                {
                    g.DrawImage(Img[28], Rects[25]);
                }
            }
            else if (BValue[37])
            {
                g.DrawImage(Img[28], Rects[25]);
            }
            else if (BValue[39])
            {
                g.DrawImage(Img[29], Rects[25]);
            }
            else if (BValue[40])
            {
                g.DrawImage(Img[30], Rects[25]);
            }

            //M9
            //无线通信中断
            if (BValue[41])
            {
                g.DrawImage(Img[31], Rects[21]);
            }

            //M10
            //特殊信息
            if (BValue[42])
            {
                g.DrawImage(Img[32], Rects[26]);
            }
            else if (BValue[48])
            {
                g.DrawImage(Img[54], Rects[26]);
            }
            else if (BValue[47])
            {
                g.DrawImage(Img[53], Rects[26]);
            }
        }

        /// <summary>
        /// 区域N内的图形图像
        /// 离站倒计时
        /// </summary>
        /// <param name="g"></param>
        private void DrawAreaN(Graphics g)
        {
            int stopTime = Convert.ToInt32(FValue[9]);
            if (stopTime > 99)
            {
                stopTime = 99;
            }
            if (stopTime < 0)
            {
                stopTime = 0;
            }
            if (BValue[1])
            {
                g.DrawString("发车:", FormatStyle.Font14B,
                    FormatStyle.WhiteSolidBrush, Rects[27], DrawFormat);
                g.DrawString(stopTime.ToString(), FormatStyle.Font24B,
                    FormatStyle.WhiteSolidBrush, Rects[8], DrawFormat);
            }
        }

        /// <summary>
        /// 区域T内的图形图像
        /// 服务号/目的号/工号
        /// </summary>
        /// <param name="g"></param>
        private void DrawAreaT(Graphics g)
        {
            g.DrawString("T" +
                         (Convert.ToInt32(FValue[10]) == 0 ? m_DefaultServiceID : Convert.ToInt32(FValue[10]).ToString(ConfigManager.Instance.DifferenceConfig.ContentDictionary[DifferenceResource.RowServiceIDFormat])),
                FormatStyle.Font16B, FormatStyle.WhiteSolidBrush, Rects[28], DrawFormat);

            g.DrawString(Convert.ToInt32(FValue[11]) == 0 ? " -- " : Convert.ToInt32(FValue[11]).ToString("00"),
                FormatStyle.Font16B, FormatStyle.WhiteSolidBrush, Rects[29], DrawFormat);

            g.DrawRectangle(FormatStyle.WhitePen, Rects[30]);

            if (m_BtnCanDown[5])
            {
                g.DrawImage(ButtonIsDown[5] ? Img[44] : Img[42], Rects[30]);
            }
            else
            {
                g.DrawImage(Img[43], Rects[30]);
            }

            g.DrawString(
                "C" + (Convert.ToInt32(FValue[12]) == 0 ? " --- " : Convert.ToInt32(FValue[12]).ToString("000")),
                FormatStyle.Font18B, FormatStyle.BlackSolidBrush, Rects[30], DrawFormat);
        }

        /// <summary>
        /// 工号界面
        /// </summary>
        /// <param name="g"></param>
        private void DrawCrewNumb(Graphics g)
        {
            g.DrawImage(Img[45], PDrawPoint[2]);
            g.DrawString(CrowNumb, FormatStyle.Font26B,
                FormatStyle.BlackSolidBrush, Rects[66], RightFormat);

            for (int i = 0; i < 13; i++)
            {
                if (ButtonIsDown[6 + i])
                {
                    g.DrawImage(Img[44], Rects[67 + i]);
                }
            }

            for (int i = 0; i < 13; i++)
            {
                g.DrawString(Str6[i], FormatStyle.Font24B,
                    FormatStyle.BlackSolidBrush, Rects[67 + i], DrawFormat);
            }
            g.DrawImage(Img[47], Rects[77]);
            g.DrawImage(Img[46], Rects[79]);
        }

        /// <summary>
        /// 声音界面
        /// </summary>
        /// <param name="g"></param>
        private void DrawSound(Graphics g)
        {
            g.DrawImage(Img[49], Rects[88]);
            for (int i = 0; i < 3; i++)
            {
                if (ButtonIsDown[26 + i])
                {
                    g.DrawImage(Img[51], Rects[89 + i]);
                }
                g.DrawString(Str8[i], FormatStyle.Font32B,
                    FormatStyle.BlackSolidBrush, Rects[89 + i], DrawFormat);

                if (m_SoundProgress > 0)
                {
                    g.FillRectangle(FormatStyle.BlueSolidBrush,
                        new Rectangle(PDrawPoint[3], new Size(m_SoundProgress * 39, 48)));
                }
            }
        }

        /// <summary>
        /// 亮度界面
        /// </summary>
        /// <param name="g"></param>
        private void DrawBrightness(Graphics g)
        {
            g.DrawImage(Img[50], Rects[88]);
            for (int i = 0; i < 3; i++)
            {
                if (ButtonIsDown[26 + i])
                {
                    g.DrawImage(Img[51], Rects[89 + i]);
                }
                g.DrawString(Str8[i], FormatStyle.Font32B,
                    FormatStyle.BlackSolidBrush, Rects[89 + i], DrawFormat);
            }
            if (ButtonIsDown[29])
            {
                g.DrawImage(Img[51], Rects[89 + 3]);
            }
            g.DrawString(Str8[3], FormatStyle.Font12B,
                FormatStyle.BlackSolidBrush, Rects[89 + 3], DrawFormat);

            if (_brightProgress > 0)
            {
                g.FillRectangle(FormatStyle.BlueSolidBrush,
                    new Rectangle(PDrawPoint[3], new Size(_brightProgress * 39, 48)));
            }
        }

        /// <summary>
        /// 菜单界面
        /// </summary>
        /// <param name="g"></param>
        private void DrawMenu(Graphics g)
        {
            g.DrawImage(Img[48], PDrawPoint[2]);

            if (m_BtnViewId == 1)
            {
                g.DrawString("菜单", FormatStyle.Font22B,
                    FormatStyle.WhiteSolidBrush, Rects[80], DrawFormat);

                for (int i = 0; i < 7; i++)
                {
                    if (i == 4 || i == 5)
                    {
                        continue;
                    }
                    g.DrawImage(ButtonIsDown[19 + i] ? Img[44] : Img[42], Rects[81 + i]);

                    g.DrawString(Str7[i], FormatStyle.Font22B,
                        FormatStyle.BlackSolidBrush, Rects[81 + i], DrawFormat);
                }
            }
        }

        /// <summary>
        /// BM模式确认
        /// </summary>
        /// <param name="g"></param>
        private void DrawACK(Graphics g)
        {
            g.FillRectangle(FormatStyle.BlackSolidBrush, Rects[98]);
            g.DrawRectangle(FormatStyle.YellowPen, Rects[98]);
            g.DrawString("后备模式选择", FormatStyle.Font14B, FormatStyle.WhiteSolidBrush,
                Rects[97], DrawFormat);
            for (int i = 0; i < 2; i++)
            {
                g.DrawImage(ButtonIsDown[30 + i] ? Img[44] : Img[42], Rects[95 + i]);

                g.DrawString(Str9[i], FormatStyle.Font18B, FormatStyle.BlackSolidBrush,
                    Rects[95 + i], DrawFormat);
            }
        }
    }
}
