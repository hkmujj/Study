using System;
using System.Drawing;
using ATPComControl.MRSP;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.Resources.ImageKeys;
using Motor.ATP._300T.共用;

namespace Motor.ATP._300T.主屏
{
    public class AreaDContent
    {
        private readonly ATPMainScreen m_ATPMainScreen;

        private readonly string[] m_BrakeDistanceIndexs;
        private readonly string[] m_BrakeSpeedIndexs;

        private readonly string[] m_CaveatDistanceIndexs;
        private readonly string[] m_CaveatSpeedIndexs;

        private readonly string[] m_GradientDistanceIndexs;
        private readonly string[] m_GradientSpeedIndexs;
        public AreaDContent(ATPMainScreen atpMainScreen)
        {
            m_ATPMainScreen = atpMainScreen;
            m_BrakeDistanceIndexs = new[]
            {
                InFloatKeys.限速速度的距离信息1,
                InFloatKeys.限速速度的距离信息2,
                InFloatKeys.限速速度的距离信息3,
                InFloatKeys.限速速度的距离信息4,
                InFloatKeys.限速速度的距离信息5,
                InFloatKeys.限速速度的距离信息6,
                InFloatKeys.限速速度的距离信息7,
                InFloatKeys.限速速度的距离信息8,
                InFloatKeys.限速速度的距离信息9,
                InFloatKeys.限速速度的距离信息10,
            };

            m_BrakeSpeedIndexs = new[]
            {
                InFloatKeys.限速信息1,
                InFloatKeys.限速信息2,
                InFloatKeys.限速信息3,
                InFloatKeys.限速信息4,
                InFloatKeys.限速信息5,
                InFloatKeys.限速信息6,
                InFloatKeys.限速信息7,
                InFloatKeys.限速信息8,
                InFloatKeys.限速信息9,
                InFloatKeys.限速信息10,
            };

            m_GradientDistanceIndexs = new[]
            {
                InFloatKeys.坡度的距离信息1,
                InFloatKeys.坡度的距离信息2,
                InFloatKeys.坡度的距离信息3,
                InFloatKeys.坡度的距离信息4,
                InFloatKeys.坡度的距离信息5,
            };

            m_GradientSpeedIndexs = new[]
            {
                InFloatKeys.坡度信息1,
                InFloatKeys.坡度信息2,
                InFloatKeys.坡度信息3,
                InFloatKeys.坡度信息4,
                InFloatKeys.坡度信息5,
            };

            m_CaveatDistanceIndexs = new[]
            {
                InFloatKeys.预告的距离信息1,
                InFloatKeys.预告的距离信息2,
                InFloatKeys.预告的距离信息3,
                InFloatKeys.预告的距离信息4,
                InFloatKeys.预告的距离信息5,
                InFloatKeys.预告的距离信息6,
                InFloatKeys.预告的距离信息7,
                InFloatKeys.预告的距离信息8,
                InFloatKeys.预告的距离信息9,
                InFloatKeys.预告的距离信息10,
            };

            m_CaveatSpeedIndexs = new[]
            {
                InFloatKeys.预告信息1,
                InFloatKeys.预告信息2,
                InFloatKeys.预告信息3,
                InFloatKeys.预告信息4,
                InFloatKeys.预告信息5,
                InFloatKeys.预告信息6,
                InFloatKeys.预告信息7,
                InFloatKeys.预告信息8,
                InFloatKeys.预告信息9,
                InFloatKeys.预告信息10,
            };
        }

        /// <summary>
        /// D 区 速度监视区
        /// </summary>
        /// <param name="g"></param>
        public void DrawAreaD(Graphics g)
        {
            //计划区
            DrawPanRegion(g);

            #region ::::::::::: D6区，机车信号

            /*
             * 当列车运行在CTCS-2级和CTCS-3级时，D6区用于显示机车信号，半径为20个像素。
             * 机车信号的显示标准符合相应规定，字体为Arial大小为14磅（推荐），颜色为黑色。
             */
            DrawAreaD6Signal(g);

            #endregion
        }

        /// <summary>
        /// D6 区机车信号
        /// </summary>
        /// <param name="g"></param>
        public void DrawAreaD6Signal(Graphics g)
        {
            var signal = m_ATPMainScreen.GetInFloatValue(InFloatKeys.信号);
            if ((signal >= 0) && (signal < 20))
            {
                if (Math.Abs(signal - 11) < float.Epsilon ||
                    Math.Abs(signal - 13) < float.Epsilon ||
                    Math.Abs(signal - 14) < float.Epsilon)
                {
                    g.DrawImage(m_ATPMainScreen.CurrentTime.Second % 2 == 0
                        ? SignalImages.无色
                        : m_ATPMainScreen.UpdateSignalLamp(Convert.ToInt32(signal)), m_ATPMainScreen.m_RectsList[44]);
                }
                else
                {
                    g.DrawImage(m_ATPMainScreen.UpdateSignalLamp(Convert.ToInt32(signal)), m_ATPMainScreen.m_RectsList[44]);
                }

                g.DrawString(m_ATPMainScreen.m_SingleLampStr[Convert.ToInt32(signal)],
                    FontsItems.Font24YouB, SolidBrushsItems.BlackBrush,
                    m_ATPMainScreen.m_RectsList[44], FontsItems.TheAlignment(FontRelated.居中));
            }
        }

        /// <summary>
        /// 计划区
        /// </summary>
        /// <param name="g"></param>
        public void DrawPanRegion(Graphics g)
        {
            if (m_ATPMainScreen.m_ShowPlanningArea)
            {
                for (var i = 0; i < 10; i++)
                {
                    m_ATPMainScreen.m_TheMrsp.InitalSpeedInfo[i].BrakingDistance =
                        m_ATPMainScreen.GetInFloatValue(m_BrakeDistanceIndexs[i]);
                    m_ATPMainScreen.m_TheMrsp.InitalSpeedInfo[i].BrakingSpeed = m_ATPMainScreen.GetProportionValue(m_ATPMainScreen.GetInFloatValue(m_BrakeSpeedIndexs[i]));

                    m_ATPMainScreen.m_TheMrsp.AllCaveatInfo[i].Distance = m_ATPMainScreen.GetInFloatValue(m_CaveatDistanceIndexs[i]);
                    m_ATPMainScreen.m_TheMrsp.AllCaveatInfo[i].CaveatType =
                      (MRSPCaveatType)(int)m_ATPMainScreen.GetInFloatValue(m_CaveatSpeedIndexs[i]);
                }
                for (var i = 0; i < 5; i++)
                {
                    m_ATPMainScreen.m_TheMrsp.AllGradientInfo[i].Distance = m_ATPMainScreen.GetInFloatValue(m_GradientDistanceIndexs[i]);
                    m_ATPMainScreen.m_TheMrsp.AllGradientInfo[i].Gradient = Convert.ToInt32(m_ATPMainScreen.GetInFloatValue(m_GradientSpeedIndexs[i]));
                }
                m_ATPMainScreen.m_TheMrsp.StartPointTSM = m_ATPMainScreen.GetInFloatValue(InFloatKeys.起模点位置);
                m_ATPMainScreen.m_TheMrsp.IdOfNextStartPoint = Convert.ToInt32(m_ATPMainScreen.GetInFloatValue(InFloatKeys.起模点结束位置));
                m_ATPMainScreen.m_TheMrsp.Update();
                m_ATPMainScreen.m_TheMrsp.Paint(g);
            }
        }


    }
}