using System.Drawing;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.Resources.ImageKeys;
using Motor.ATP._300T.共用;
using Motor.ATP._300T.共用.功能键与菜单;

namespace Motor.ATP._300T.主屏
{
    public class AreaCContent
    {
        private readonly ATPMainScreen m_ATPMainScreen;

        public AreaCContent(ATPMainScreen atpMainScreen)
        {
            m_ATPMainScreen = atpMainScreen;
        }

        private readonly Image[] m_C3BrakeImages = {
            BrakeImages.C3_紧急制动,
            BrakeImages.C3_最大常用制动,
            BrakeImages.C3_四级制动,
            BrakeImages.C3_一级制动,
            BrakeImages.C3_允许缓解,

        };

        private readonly Image[] m_C2BrakeImages = {
            BrakeImages.C2_紧急制动,
            BrakeImages.C2_最大常用制动,
            BrakeImages.C2_四级制动,
            BrakeImages.C2_一级制动,
            BrakeImages.C2_允许缓解,
        };

        /// <summary>
        /// C区，补充驾驶信息
        /// </summary>
        /// <param name="g"></param>
        public void DrawAreaC(Graphics g)
        {

            #region ::::::::::: C1区，下一控制模式信息

            /*
             * 在C1区以带黄色闪动边框的模式文本，显示要确认的各种模式，闪烁频率为1 Hz，以便司机确认下一有效模式。
             * 当司机进行确认，并且列控车载设备接受后，该图标从C1区消失，成为B7中显示的新的有效控制模式，如上表所示。
             * 字体为幼圆大小为18磅（推荐），颜色为白色。
             */
            if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inb冒进闪烁)])
            {
                g.DrawString(
                    "冒进",
                    FontsItems.Font18DefB, SolidBrushsItems.WhiteBrush,
                    RectangleF.Union(m_ATPMainScreen.m_RectsList[32], m_ATPMainScreen.m_RectsList[31]),
                    FontsItems.TheAlignment(FontRelated.居中));
                if (m_ATPMainScreen.CurrentTime.Second%2 == 0)
                {
                    g.DrawRectangle(PenItems.YellowPen2,
                        Rectangle.Round(RectangleF.Union(m_ATPMainScreen.m_RectsList[32],
                            m_ATPMainScreen.m_RectsList[31])));
                }
            }
            else if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.Inb越行闪烁)])
            {
                g.DrawString(
                    "越行",
                    FontsItems.Font18DefB, SolidBrushsItems.WhiteBrush,
                    RectangleF.Union(m_ATPMainScreen.m_RectsList[32], m_ATPMainScreen.m_RectsList[31]),
                    FontsItems.TheAlignment(FontRelated.居中));
                if (m_ATPMainScreen.CurrentTime.Second%2 == 0)
                {
                    g.DrawRectangle(PenItems.YellowPen2,
                        Rectangle.Round(RectangleF.Union(m_ATPMainScreen.m_RectsList[32],
                            m_ATPMainScreen.m_RectsList[31])));
                }
            }

            #endregion

            #region ::::::::::: C2/3/4区，预留

            /*
             */

            #endregion

            #region ::::::::::: C5/6/7区，预留

            /*
             */

            #endregion

            #region ::::::::::: C8区，CTCS等级

            /*
             * 以文字的方式显示列控车载设备的运行等级，字体为幼圆大小为14磅（推荐），颜色为白色。
             */
            if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.InbCTCS2)])
            {
                g.DrawString("CTCS 2", FontsItems.Font12YouB,
                    SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[10],
                    FontsItems.TheAlignment(FontRelated.居中));
            }
            else if (m_ATPMainScreen.BoolList[m_ATPMainScreen.GetInBoolIndex(InBoolKeys.InbCTCS3)])
            {
                g.DrawString("CTCS 3", FontsItems.Font12YouB,
                    SolidBrushsItems.WhiteBrush, m_ATPMainScreen.m_RectsList[10],
                    FontsItems.TheAlignment(FontRelated.居中));
            }

            #endregion

            #region ::::::::::: C9区，列控车载设备制动状态

            /*
             * 以图标的方式显示列控车载设备制动状态。
             * DMI根据列控车载设备的制动状态显示图标。
             * 如果列控车载设备处于非制动非允许缓解状态，则该区域不显示任何图标。
             */
            switch (m_ATPMainScreen.CurrentSignalSystem)
            {
                case SignalSystem.CTCS3:
                    for (var i = 0; i < 5; i++)
                    {
                        if (m_ATPMainScreen.BoolList[m_ATPMainScreen.m_BrakeIndexs[i]])
                        {
                            g.DrawImage(m_C3BrakeImages[i], m_ATPMainScreen.m_RectsList[11]);
                        }
                    }
                    break;
                case SignalSystem.CTCS2:
                    for (var i = 0; i < 5; i++)
                    {
                        if (m_ATPMainScreen.BoolList[m_ATPMainScreen.m_BrakeIndexs[i]])
                        {
                            g.DrawImage(m_C2BrakeImages[i], m_ATPMainScreen.m_RectsList[11]);
                        }
                    }
                    break;
            }

            #endregion
        }
    }
}