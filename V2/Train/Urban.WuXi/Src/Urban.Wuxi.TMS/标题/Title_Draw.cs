using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.PublicUI;
using MMI.Facility.Interface.Data;
using MMI_Message;

namespace KumM_TMS.DMITitle
{
    public partial class Title : baseClass
    {
        /// <summary>
        /// 按键复位
        /// </summary>
        private void ButtonReset()
        {
            for (int i = 0; i < 7; i++)
            {
                buttonIsDown[i + 1] = false;
            }
        }

        /// <summary>
        /// 绘制框架
        /// 上方部分
        /// </summary>
        /// <param name="e"></param>
        private void DrawFrameUp(Graphics e)
        {
            e.DrawString(titleName, FormatStyle.Font14B,
                FormatStyle.WhiteBrush, rects[14], drawFormat);

            e.DrawRectangle(FormatStyle.WhitePen, rects[0].X, rects[0].Y, rects[0].Width, rects[0].Height);
            e.DrawRectangle(FormatStyle.WhitePen, rects[1].X, rects[1].Y, rects[1].Width, rects[1].Height);

            //e.DrawString("编组", FormatStyle.Font14B, FormatStyle.WhiteBrush,
            //    rects[2], drawFormat);
            e.DrawString("V", FormatStyle.Font16B, FormatStyle.WhiteBrush,
                rects[3], drawFormat);
            e.DrawString("km/h", FormatStyle.Font16B, FormatStyle.WhiteBrush,
                rects[4], drawFormat);
            //e.DrawString(Convert.ToInt32(fValue[0]).ToString("00000"), FormatStyle.Font14B,
            //    FormatStyle.WhiteBrush, rects[5], drawFormat);

            //底图
            for (int i = 0; i < 6; i++)
            {
                e.DrawImage(Img[1], rects[36 + i]);
            }
            e.DrawImage(Img[2], rects[42]);
            e.DrawImage(Img[3], rects[43]);
            e.DrawImage(Img[4], rects[44]);

            //故障
            e.DrawImage(Img[0], rects[45]);

            //时间
            e.DrawString(DateTime.Today.ToString("yyyy-MM-dd"), FormatStyle.Font14B,
                FormatStyle.WhiteBrush, rects[6], drawFormat);
            e.DrawString(DateTime.Now.ToLongTimeString(), FormatStyle.Font14B,
                FormatStyle.WhiteBrush, rects[7], drawFormat);

            //下一站
            e.DrawString("下一站", FormatStyle.Font14B, FormatStyle.WhiteBrush, rects[8], drawFormat);
            if (TrainStation(Convert.ToInt32(fValue[1]), nextStation) == null)
                e.DrawString("----", FormatStyle.Font14B, FormatStyle.WhiteBrush, rects[9], drawFormat);
            else
                e.DrawString(TrainStation(Convert.ToInt32(fValue[1]), nextStation), FormatStyle.Font14B,
                    FormatStyle.WhiteBrush, rects[9], drawFormat);

            //终点站
            e.DrawString("终点站", FormatStyle.Font14B, FormatStyle.WhiteBrush, rects[10], drawFormat);
            if (TrainStation(Convert.ToInt32(fValue[2]), nextStation) == null)
                e.DrawString("----", FormatStyle.Font14B, FormatStyle.WhiteBrush, rects[11], drawFormat);
            else
                e.DrawString(TrainStation(Convert.ToInt32(fValue[2]), nextStation), FormatStyle.Font14B,
                    FormatStyle.WhiteBrush, rects[11], drawFormat);

            //网压
            e.DrawString(Convert.ToInt32(fValue[3]).ToString(), FormatStyle.Font32B,
                FormatStyle.LightGreenBrush, rects[12], RightFormat);
            //速度
            if (fValue[4] < 100 && fValue[4] > 0)
                e.DrawString(fValue[4].ToString("0.0"), FormatStyle.Font32B,
                    FormatStyle.LightGreenBrush, rects[13], RightFormat);
            else if (fValue[4] >= 100)
                e.DrawString("100", FormatStyle.Font32B,
                    FormatStyle.LightGreenBrush, rects[13], RightFormat);
            else
                e.DrawString("0.0", FormatStyle.Font32B,
                    FormatStyle.LightGreenBrush, rects[13], RightFormat);
        }

        /// <summary>
        /// 绘制框架
        /// 下方部分
        /// </summary>
        /// <param name="e"></param>
        private void DrawFrameDown(Graphics e)
        {
            for (int i = 0; i < 2; i++)
            {
                e.DrawLine(FormatStyle.WhitePen, pDrawPoint[i * 2], pDrawPoint[1 + i * 2]);
            }

            //亮度调节
            e.DrawImage(Img[5], rects[46]);

            //button
            for (int i = 0; i < 7; i++)
            {
                if (buttonIsDown[i + 1])
                    e.DrawImage(Img[7], rects[21 + i]);
                else
                    e.DrawImage(Img[6], rects[21 + i]);

                if (i != 5)
                    e.DrawString(FormatStyle.Str_1[i], FormatStyle.Font12B, FormatStyle.BlackBrush, rects[21 + i], drawFormat);
                else
                    e.DrawString("帮助", FormatStyle.Font12B, isExistHelp ? FormatStyle.BlackBrush : FormatStyle.DarkGreyBrush,
                        rects[26], drawFormat);
            }
        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            DrawFrameUp(e);
            DrawFrameDown(e);

            //列车
            if (isShowCar)
            {
                e.DrawImage(Img[9], rects[47]);
                for (int i = 0; i < 6; i++)
                {
                    e.DrawString(FormatStyle.str3[i], FormatStyle.Font14B,
                        FormatStyle.WhiteBrush, rects[30 + i], drawFormat);
                }
            }

            //司机室占用和列车方向
            if (isShowDirection)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (BoolList[i])
                        e.DrawImage(Img[11], rects[48 + i]);
                    else
                        e.DrawImage(Img[10], rects[48 + i]);
                }

                if (BoolList[2])
                    e.DrawImage(Img[12], rects[50]);
                else if (BoolList[3])
                    e.DrawImage(Img[13], rects[51]);
            }
        }

    }
}
