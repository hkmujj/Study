using System.Collections.Generic;
using System.Windows.Media;

namespace Engine.HMI.SS3B.View
{
    public class ColorSolidBrushMgr
    {
         public static ColorSolidBrushMgr Instance=new ColorSolidBrushMgr();

         private readonly Dictionary<Color,SolidColorBrush> m_Dictionary;

        ColorSolidBrushMgr()
        {
            m_Dictionary=new Dictionary<Color, SolidColorBrush>();
        }

        public SolidColorBrush GetSolidColorBrush(Color color)
        {
            if (!m_Dictionary.ContainsKey(color))
            {
                m_Dictionary.Add(color,new SolidColorBrush(color));
            }
            return m_Dictionary[color];
        }
        public static Color NetPageColor1 = Color.FromRgb(94, 189, 200);
        public static Color NetPageColor2 = Color.FromRgb(100, 100, 112);
        public static Color NetPageColor3=Color.FromRgb(255,0,0);
    }
}