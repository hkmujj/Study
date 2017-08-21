using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1C0Frame : baseClass
    {
        private Image m_Background;

        public override string GetInfo()
        {
            return "背景";
        }


        public override bool init(ref int nErrorObjectIndex)
        {

            using (FileStream fs = new FileStream(RecPath + "\\" + UIObj.ParaList[0], FileMode.Open))
            {
                m_Background = Image.FromStream(fs);
            }

            return true;
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(m_Background, new Rectangle(0, 0, 800, 600));

            DrawText(g);
        }

        private void DrawText(Graphics g)
        {
            g.DrawString("模式", Global.m_FontArial16B, Brushes.White, new PointF(30, 15));
            g.DrawString("停靠", Global.m_FontArial16B, Brushes.White, new PointF(226, 15));
            g.DrawString("停站", Global.m_FontArial16B, Brushes.White, new PointF(435, 15));
            g.DrawString("发车", Global.m_FontArial16B, Brushes.White, new PointF(630, 15));
        }
    }
}
