using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace ATP200C.MainView
{
    public static class TrainSpeedInfoExtension
    {
        private static Dictionary<TrainInfoColor, Pen> m_ColorNormalPenDictionary;

        private static Dictionary<TrainInfoColor, Pen> m_ColorBoldPenDictionary;

        static TrainSpeedInfoExtension()
        {
            const int normalWidth = 8;

            const int boldWidth = normalWidth * 2;

            const PenAlignment align = PenAlignment.Inset;

            m_ColorNormalPenDictionary = new Dictionary<TrainInfoColor, Pen>()
                                         {
                                             { TrainInfoColor.Gray, new Pen(Color.FromArgb(192, 192, 192), normalWidth) { Alignment = align}},
                                             { TrainInfoColor.Yellow, new Pen(Color.FromArgb(223, 223, 0), normalWidth){ Alignment = align} },
                                             { TrainInfoColor.Red, new Pen(Color.FromArgb(191, 0, 2), normalWidth){ Alignment = align} },
                                             { TrainInfoColor.Orange, new Pen(Color.FromArgb(234, 145, 0), normalWidth){ Alignment = align} },
                                             { TrainInfoColor.Null, new Pen(Color.White, normalWidth) }
                                         };
            m_ColorBoldPenDictionary = new Dictionary<TrainInfoColor, Pen>()
                                       {
                                           { TrainInfoColor.Gray, new Pen(Color.FromArgb(192, 192, 192), boldWidth){ Alignment = align} },
                                           { TrainInfoColor.Yellow, new Pen(Color.FromArgb(223, 223, 0), boldWidth) { Alignment = align}},
                                           { TrainInfoColor.Red, new Pen(Color.FromArgb(191, 0, 2), boldWidth) { Alignment = align}},
                                           { TrainInfoColor.Orange, new Pen(Color.FromArgb(234, 145, 0), boldWidth) { Alignment = align}},
                                           { TrainInfoColor.Null, new Pen(Color.White, boldWidth) { Alignment = align}}
                                       };
        }

        public static Pen GetNormalPen(this TrainInfoColor info)
        {
            return m_ColorNormalPenDictionary[info];
        }

        public static Pen GetBoldPen(this TrainInfoColor info)
        {
            return m_ColorBoldPenDictionary[info];
        }

        public static float GetBoldInside()
        {
            return (m_ColorNormalPenDictionary.First().Value.Width - m_ColorBoldPenDictionary.First().Value.Width) / 2;
        }

    }
}