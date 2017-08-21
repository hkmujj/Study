using System;
using System.Drawing;

namespace Urban.Wuxi.TMS.DMITitle
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Title
    {
        /// <summary>
        /// 按键复位
        /// </summary>
        private void ButtonReset()
        {
            for (int i = 0; i < 7; i++)
            {
                m_ButtonIsDown[i + 1] = false;
            }
        }

        /// <summary>
        /// 绘制框架
        /// 上方部分
        /// </summary>
        /// <param name="g"></param>

        private void DrawFrameUp(Graphics g)
        {
            g.DrawString(m_TitleName, FormatStyle.m_Font14B,
               FormatStyle.m_WhiteBrush, m_Rects[14], m_DrawFormat);
            g.DrawRectangle(FormatStyle.m_WhitePen, m_Rects[0].X, m_Rects[0].Y, m_Rects[0].Width, m_Rects[0].Height);
            g.DrawRectangle(FormatStyle.m_WhitePen, m_Rects[1].X, m_Rects[1].Y, m_Rects[1].Width, m_Rects[1].Height);
            g.DrawString("V", FormatStyle.m_Font16B, FormatStyle.m_WhiteBrush,
                m_Rects[3], m_DrawFormat);
            g.DrawString("km/h", FormatStyle.m_Font16B, FormatStyle.m_WhiteBrush,
                m_Rects[4], m_DrawFormat);
            //底图
            for (int i = 0; i < 6; i++)
            {
                g.DrawImage(m_Images[1], m_Rects[36 + i]);
            }
            g.DrawImage(m_Images[2], m_Rects[42]);
            g.DrawImage(m_Images[3], m_Rects[43]);
            g.DrawImage(m_Images[4], m_Rects[44]);
            var tmp = m_MsgInfList.CurrentMsgList.FindAll(f => !f.TheMsgFlag);
            //故障
            if (tmp.Count != 0)
            {
                if (DateTime.Now.Second % 2 == 0)
                {
                    g.FillRectangle(FormatStyle.m_RedBrush, m_Rects[77]);
                }
                g.DrawImage(m_Images[0], m_Rects[45]);

            }
            //时间
            g.DrawString(DateTime.Now.ToString("yyyy-MM-dd"), FormatStyle.m_Font14B,
                FormatStyle.m_WhiteBrush, m_Rects[6], m_DrawFormat);
            g.DrawString(DateTime.Now.ToLongTimeString(), FormatStyle.m_Font14B,
                FormatStyle.m_WhiteBrush, m_Rects[7], m_DrawFormat);
            //当前站

            g.DrawString("当前站", FormatStyle.m_Font14B, FormatStyle.m_WhiteBrush, m_Rects[8], m_DrawFormat);
            if (TrainStation(Convert.ToInt32(m_FValue[1]), NextStation_) == null)
                g.DrawString("----", FormatStyle.m_Font14B, FormatStyle.m_WhiteBrush, m_Rects[9], m_DrawFormat);
            else
                g.DrawString(TrainStation(Convert.ToInt32(m_FValue[1]), NextStation_), FormatStyle.m_Font14B,
                    FormatStyle.m_WhiteBrush, m_Rects[9], m_DrawFormat);
            //终点站
            g.DrawString("终点站", FormatStyle.m_Font14B, FormatStyle.m_WhiteBrush, m_Rects[10], m_DrawFormat);
            if (TrainStation(Convert.ToInt32(m_FValue[2]), NextStation_) == null)
                g.DrawString("----", FormatStyle.m_Font14B, FormatStyle.m_WhiteBrush, m_Rects[11], m_DrawFormat);
            else
                g.DrawString(TrainStation(Convert.ToInt32(m_FValue[2]), NextStation_), FormatStyle.m_Font14B,
                  FormatStyle.m_WhiteBrush, m_Rects[11], m_DrawFormat);
            //网压
            g.DrawString(Convert.ToInt32(m_FValue[3]).ToString(), FormatStyle.m_Font32B,
                FormatStyle.m_LightGreenBrush, m_Rects[12], m_RightFormat);
            //速度
            if (m_FValue[4] < 100 && m_FValue[4] > 0)
                g.DrawString(m_FValue[4].ToString("0.0"), FormatStyle.m_Font32B,
                    FormatStyle.m_LightGreenBrush, m_Rects[13], m_RightFormat);
            else if (m_FValue[4] >= 100)
                g.DrawString("100", FormatStyle.m_Font32B,
                    FormatStyle.m_LightGreenBrush, m_Rects[13], m_RightFormat);
            else
                g.DrawString("0.0", FormatStyle.m_Font32B,
                    FormatStyle.m_LightGreenBrush, m_Rects[13], m_RightFormat);
        }

        /// <summary>
        /// 绘制框架
        /// 下方部分
        /// </summary>
        /// <param name="g"></param>
        private void DrawFrameDown(Graphics g)
        {
            for (int i = 0; i < 2; i++)
            {
                g.DrawLine(FormatStyle.m_WhitePen, m_PDrawPoint[i * 2], m_PDrawPoint[1 + i * 2]);
            }

            //亮度调节
            g.DrawImage(m_Images[5], m_Rects[46]);

            //底部按钮
            for (int i = 0; i < 7; i++)
            {
                if (m_ButtonIsDown[i + 1])
                    g.DrawImage(m_Images[7], m_Rects[21 + i]);
                else
                    g.DrawImage(m_Images[6], m_Rects[21 + i]);

                if (i != 5)
                    g.DrawString(FormatStyle.m_Str1[i], FormatStyle.m_Font12B, FormatStyle.m_BlackBrush, m_Rects[21 + i],
                        m_DrawFormat);
                else
                    g.DrawString("帮助", FormatStyle.m_Font12B,
                        m_IsExistHelp ? FormatStyle.m_BlackBrush : FormatStyle.m_DarkGreyBrush,
                        m_Rects[26], m_DrawFormat);
            }
        }

        private int m_RectCarNumb;

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="g"></param>
        private void DrawOn(Graphics g)
        {
            DrawFrameUp(g);
            DrawFrameDown(g);

            switch (m_CurrentView)
            {
                case 11: //运行
                    m_RectCarNumb = 57;
                    break;
                case 13: //空调界面的车
                    m_RectCarNumb = 64;
                    break;
                case 15: //通讯状态
                    m_RectCarNumb = 71;
                    break;
                case 43:
                    {

                    }
                    break;
                default:
                    m_RectCarNumb = 30;
                    break;
            }
            //列车
            if (m_IsShowCar)
            {
                //司机室占用和列车方向
                if (m_IsShowDirection)
                {
                    if (m_CurrentView == 11)
                    {
                        if (BoolList[m_BoolIds[0]])
                        {
                            g.DrawImage(m_Images[16], m_Rects[53]);

                            g.DrawImage(m_Images[17], m_Rects[54]);
                        }
                        else if (BoolList[m_BoolIds[1]])
                        {
                            g.DrawImage(m_Images[18], m_Rects[54]);
                            g.DrawImage(m_Images[15], m_Rects[53]);
                        }

                        else
                        {
                            g.DrawImage(m_Images[15], m_Rects[53]);
                            g.DrawImage(m_Images[17], m_Rects[54]);
                        }



                        if (BoolList[m_BoolIds[2]])
                            g.DrawImage(m_Images[21], m_Rects[55]);
                        else if (BoolList[m_BoolIds[3]])
                            g.DrawImage(m_Images[22], m_Rects[55]);
                        else if (BoolList[m_BoolIds[4]])
                            g.DrawImage(m_Images[21], m_Rects[56]);
                        else if (BoolList[m_BoolIds[4] + 1])
                            g.DrawImage(m_Images[22], m_Rects[56]);
                    }
                    else
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            if (BoolList[m_BoolIds[0 + i]])
                                g.DrawImage(m_Images[11], m_Rects[48 + i]);
                            else
                                g.DrawImage(m_Images[10], m_Rects[48 + i]);
                        }

                        if (BoolList[m_BoolIds[2]])
                            g.DrawImage(m_Images[12], m_Rects[50]);
                        else if (BoolList[m_BoolIds[5]])
                            g.DrawImage(m_Images[13], m_Rects[51]);
                        else if (BoolList[m_BoolIds[3]])
                            g.DrawImage(m_Images[13], m_Rects[50]);
                        else if (BoolList[m_BoolIds[4]])
                            g.DrawImage(m_Images[12], m_Rects[51]);
                    }
                }
            }
        }
    }
}
