using System;
using System.Drawing;
using System.Globalization;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.Resources.ImageKeys;
using Motor.ATP._300T.共用;

namespace Motor.ATP._300T.主屏
{
    public class AreaAContent
    {
        private readonly ATPMainScreen m_ATPMainScreen;

        public AreaAContent(ATPMainScreen atpMainScreen)
        {
            m_ATPMainScreen = atpMainScreen;
        }

        /// <summary>
        /// A区，距离监控信息
        /// </summary>
        public void DrawAreaA(Graphics g)
        {
            #region ::::::::::: A1区，制动预警时间

            /* 
             * 制动预警时间在A1区中心以正方形图标显示，图标的大小取决于距触发制动的预期时间（8s）。
             * 一旦距触发制动的预期时间低于设定时间值，方块图标开始变大直至最大尺寸。
             */
            g.FillRectangle(
                m_ATPMainScreen.GetBreakWarning(Convert.ToInt32(m_ATPMainScreen.GetInFloatValue(InFloatKeys.制动预警时间)))
                    .BreakWarmingBrush,
                m_ATPMainScreen.GetBreakWarning(
                    Convert.ToInt32(m_ATPMainScreen.GetInFloatValue(InFloatKeys.制动预警时间))).BreakWarningRect);

            #endregion

            #region ::::::::::: A2区，目标距离

            /*
             * A2区使用两种方法表示目标距离：柱状光带表示法和数字表示法。
             * 柱状光带最大尺寸为15×172，颜色总为白色，光带正上方为数字表示区，单位为米。
             * 柱状光带的左侧为坐标系刻度，该坐标系采用对数坐标（0-100米采用线性坐标），最大显示范围是1000米。
             * 字体为Arial大小为16磅（推荐），颜色为白色。
             * 当目标距离大于1000米时，光带上方（能显示5个数字）用数字显示实际目标距离，柱状光带的高度保持不变，数字显示的精度为10米；
             * 当目标距离小于1000米时，柱状光带逐渐缩短，数字显示的精度为1米
             * 当列车处于目标速度监视区时，A2区进行显示，当列车处于顶棚速度监视区时A2区无显示。
             *--------------------------------------------------------
             * 监视区      |    运行状态     |  目标距离是否显示
             * ------------|-----------------|------------------------
                   |CSM    |    全部状态     |      不显示
             *     |-------|-----------------|------------------------
                FS |TSM    |    全部状态     |       显示
             *     |-------|-----------------|------------------------
                   |RSM    |    全部状态     |       显示
             * ------------|-----------------|------------------------
                其它模式   |    全部状态     |      不显示
             * -------------------------------------------------------
             */
            if ((m_ATPMainScreen.m_TheControlMode == ControlMode.TSM || m_ATPMainScreen.m_TheControlMode == ControlMode.RSM) &&
                (m_ATPMainScreen.CurrentTrainMode == TrainMode.完全 || m_ATPMainScreen.CurrentTrainMode == TrainMode.引导))
            {


                g.DrawImage(ComImages.目标距离, m_ATPMainScreen.m_RectsList[5]);

                m_ATPMainScreen.m_TargetSpeed.ForEach(e => e.CurrentValue = 0);
                for (var i = 0; i < 10; i++)
                {
                    if (m_ATPMainScreen.m_TarDistance > (i + 1) * 100)
                    {
                        m_ATPMainScreen.m_TargetSpeed[i].CurrentValue = 100;
                    }
                    else if (m_ATPMainScreen.m_TarDistance > i * 100 && m_ATPMainScreen.m_TarDistance <= (i + 1) * 100)
                    {
                        m_ATPMainScreen.m_TargetSpeed[i].CurrentValue = m_ATPMainScreen.m_TarDistance - i * 100;
                    }
                }

                m_ATPMainScreen.m_TargetSpeed.ForEach(e => e.OnDraw(g));
                var s = m_ATPMainScreen.m_TargetSpeed.FindAll(f => f.OutLineRectangle.Height != 0);
                if (s.Count != 0)
                {
                    g.FillRectangle(new SolidBrush(s[0].BackgroundColor), ATPMainScreen.GetRec(s));
                }
                g.DrawString(Convert.ToInt32(m_ATPMainScreen.m_TarDistance).ToString(CultureInfo.InvariantCulture),
                    FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[4],
                    FontsItems.TheAlignment(FontRelated.靠左));
            }

            #endregion

            #region ::::::::::: A3区，预留

            /*
             */

            #endregion
        }
    }
}