using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.ATP._300D.Common;

namespace Motor.ATP._300D
{
    [GksDataType(DataType.isMMIObjectClass)]
    class FrameButton2D : ATPBase
    {
        public static int FrameChange_X = 0;
        public static int FrameChange_Y = 0;

        static Font Font9 = new Font("Arial", 9f);
        static Font Font10 = new Font("Arial", 10f);
        static Font Font11 = new Font("Arial", 11f);
        static Font Font12 = new Font("Arial", 12f);
        static Font Font14 = new Font("Arial", 14f);

        static Pen WhitePen2 = new Pen(Color.FromArgb(255, 255, 255), 2);
        static Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0), 2);

        readonly static SolidBrush MediumGreySolidBrush = new SolidBrush(Color.FromArgb(57, 75, 89));
        readonly static SolidBrush BlueSolidBrush = new SolidBrush(Color.FromArgb(50, 120, 255));
        readonly static SolidBrush BlueSolidBrush2 = new SolidBrush(Color.FromArgb(14, 78, 202));
        readonly static SolidBrush WhiteSolidBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        readonly static SolidBrush BlackSolidBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        readonly static SolidBrush OrangeSolidBrush = new SolidBrush(Color.FromArgb(240, 50, 10));
        readonly static SolidBrush OrangeSolidBrush2 = new SolidBrush(Color.FromArgb(203, 43, 9));

        private readonly StringFormat m_ButtonContentFormat = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };

        private List<Region> m_ButtonRegionCollection;

        readonly bool[] ButtonDown = new bool[20];

        public override void paint(Graphics g)
        {
            DrawOutLine(g);

            DrawInputButtons(g);

            DrawControlButtons(g);
        }

        public override bool Initalize()
        {
            if (UIObj.ParaList.Count < 0)
            {
                return false;
            }

            if (UIObj.InBoolList.Count != 1)
            {
                return false;
            }

            InitalizeButtonLocations();

            return true;
        }

        public override string GetInfo()
        {
            return "屏幕外按钮2D";
        }

        public override bool mouseDown(Point nPoint)
        {

            for (var index = 0; index < m_ButtonRegionCollection.Count; ++index)
            {
                if (m_ButtonRegionCollection[index].IsVisible(nPoint))
                {
                    //append_postCmd(CmdType.SetExValue, 101, sendValue[index], tmpValue);
                    append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[0] + index, 1, 0);
                    ButtonDown[index] = true;
                    return true;
                }
            }
            return false;
        }

        public override bool mouseUp(Point nPoint)
        {
            for (var index = 0; index < m_ButtonRegionCollection.Count; ++index)
            {
                if (m_ButtonRegionCollection[index].IsVisible(nPoint))
                {
                    //append_postCmd(CmdType.SetExValue, 101, sendValue[index], tmpValue);
                    append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[0] + index, 0, 0);
                    ButtonDown[index] = false;
                    return true;
                }
            }
            return false;
        }

        private void InitalizeButtonLocations()
        {
            m_ButtonRegionCollection = new List<Region>();
            var rect = new Rectangle(15, 665, 45, 45);

            for (int i = 0; i < 11; i++)
            {
                m_ButtonRegionCollection.Add(new Region(rect));
                rect.X += 73;
            }
            rect.X = 818;
            rect.Y = 70;

            for (int i = 0; i < 9; i++)
            {
                m_ButtonRegionCollection.Add(new Region(rect));
                rect.Y += 75;
            }
        }

        private static void DrawOutLine(Graphics g)
        {
            g.FillRectangle(MediumGreySolidBrush, new Rectangle(0, 0, 870, 60));
            g.FillRectangle(MediumGreySolidBrush, new Rectangle(0, 0, 10, 720));
            g.FillRectangle(MediumGreySolidBrush, new Rectangle(0, 660, 870, 60));
            g.FillRectangle(MediumGreySolidBrush, new Rectangle(810, 0, 60, 720));
        }

        private void DrawControlButtons(Graphics g)
        {
            for (int i = 0; i < 8; i++)
            {
                g.DrawRectangle(WhitePen2, new Rectangle(818, 70 + i * 75, 45, 45));
                g.DrawString("F" + (i + 1), Font14, WhiteSolidBrush, new Rectangle(818, 70 + i * 75, 45, 45), m_ButtonContentFormat);

                if (ButtonDown[i + 11])
                {
                    g.FillRectangle(WhiteSolidBrush, new Rectangle(820, 72 + i * 75, 41, 41));
                    g.DrawString("F" + (i + 1), Font14, BlackSolidBrush, new Rectangle(818, 70 + i * 75, 45, 45), m_ButtonContentFormat);
                }
            }

            g.DrawRectangle(WhitePen2, new Rectangle(818, 70 + 8 * 75, 45, 45));
            g.DrawString("ATP确认", Font14, WhiteSolidBrush, new Rectangle(818, 70 + 8 * 75, 45, 45), m_ButtonContentFormat);

            if (ButtonDown[8 + 11])
            {
                g.FillRectangle(WhiteSolidBrush, new Rectangle(820, 72 + 8 * 75, 41, 41));
                g.DrawString("ATP确认", Font14, BlackSolidBrush, new Rectangle(818, 70 + 8 * 75, 45, 45), m_ButtonContentFormat);
            }

        }

        private void DrawInputButtons(Graphics g)
        {
            var numb = new string[11] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "" };
            var letter = new string[11] { "", "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ", "-", "字母" };
            var chine = new string[11] { "调车", "目视", "机信", "启动", "缓解", "", "", "", "", "", "警惕" };

            for (var i = 0; i < 2; i++)
            {
                g.FillPolygon(OrangeSolidBrush, new Point[3] { new Point(307 + i * 438, 665), new Point(352 + i * 438, 665), new Point(307 + i * 438, 710) });
            }

            for (int i = 0; i < 11; i++)
            {
                g.DrawRectangle(WhitePen2, new Rectangle(15 + i * 73, 665, 45, 45));
                g.FillPolygon(BlueSolidBrush, new Point[3] { new Point(15 + i * 73, 710), new Point(60 + i * 73, 665), new Point(60 + i * 73, 710) });
                g.DrawString(numb[i], Font11, WhiteSolidBrush, 48 + i * 73, 680);
                g.DrawString(letter[i], Font9, WhiteSolidBrush, new Rectangle(23 + i * 73, 693, 40, 20), m_ButtonContentFormat);
                g.DrawString(chine[i], Font9, WhiteSolidBrush, 17 + i * 73, 668);

                if (ButtonDown[i])
                {
                    g.FillPolygon(BlueSolidBrush2, new Point[3] { new Point(15 + i * 73, 710), new Point(60 + i * 73, 665), new Point(60 + i * 73, 710) });
                    g.DrawString(numb[i], Font11, BlackSolidBrush, 48 + i * 73, 680);
                    g.DrawString(letter[i], Font9, BlackSolidBrush, new Rectangle(23 + i * 73, 693, 40, 20), m_ButtonContentFormat);
                    g.DrawString(chine[i], Font9, BlackSolidBrush, 17 + i * 73, 668);
                }
            }
        }
    }
}