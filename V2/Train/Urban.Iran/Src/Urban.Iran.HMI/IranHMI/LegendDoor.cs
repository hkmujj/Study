using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class LegendDoor : HMIBase
    {
        private FjButtonEx m_BackBtn;
        private  Rectangle[] m_LgdRects;
        private  Bitmap[] m_BitMapArr;

        public LegendDoor()
        {
           
        }

        public override string GetInfo()
        {
            return "车门状态图例";
        }

        protected override bool Initalize()
        {
            m_BitMapArr = new Bitmap[8]
                        {
                            new Bitmap(RecPath + "\\frame/drClose.jpg"),
                            new Bitmap(RecPath + "\\frame/drOpen.jpg"),
                            new Bitmap(RecPath + "\\frame/drCutout.jpg"),
                            new Bitmap(RecPath + "\\frame/drFaulty.jpg"),
                            new Bitmap(RecPath + "\\frame/drEmergencyUnlock.jpg"),
                            new Bitmap(RecPath + "\\frame/peaActive.jpg"),
                            new Bitmap(RecPath + "\\frame/peaSpeeking.jpg"),
                            new Bitmap(RecPath + "\\frame/peaFaulty.jpg")
                        };
            m_LgdRects = new[]
                       {
                           new Rectangle(60, 194, 26, 23),
                           new Rectangle(60, 223, 26, 23),
                           new Rectangle(315, 194, 26, 23),
                           new Rectangle(315, 223, 26, 23),
                           new Rectangle(315, 252, 26, 23),
                           new Rectangle(590, 197, 18, 18),
                           new Rectangle(590, 227, 18, 18),
                           new Rectangle(590, 257, 18, 18)
                       };

            m_BackBtn = new FjButtonEx(1, GlobleParam.ResManager.GetString("String243"), GlobleRect.m_LegendbtnRect);
            m_BackBtn.MouseDown += (sender, pt) =>  ChangedPage(IranViewIndex.Doors);;

            return true;
        }

        public override void paint(Graphics g)
        {
            GlobleParam.Instance.CurrentIranViewIndex = (IranViewIndex) 3;
            for (var index = 0; index < 8; ++index)
            {
                g.DrawImage(m_BitMapArr[index], m_LgdRects[index]);
            }

            g.DrawString(GlobleParam.ResManager.GetString("String3"), GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, 95, 195);
            g.DrawString(GlobleParam.ResManager.GetString("String34"), GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, 95, 225);
            g.DrawString(GlobleParam.ResManager.GetString("String35"), GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, 345, 195);
            g.DrawString(GlobleParam.ResManager.GetString("String36"), GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, 345, 225);
            g.DrawString(GlobleParam.ResManager.GetString("String37"), GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, 345, 255);
            g.DrawString(GlobleParam.ResManager.GetString("String38"), GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, 612, 195);
            g.DrawString(GlobleParam.ResManager.GetString("String39"), GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, 612, 225);
            g.DrawString(GlobleParam.ResManager.GetString("String40"), GdiCommon.Txt12Font, GdiCommon.MediumGreyBrush, 612, 255);

         //   m_BackBtn.OnDraw(g);

        }

        //public override bool mouseDown(Point point)
        //{
        //    if (m_BackBtn.IsVisible(point))
        //    {
        //        m_BackBtn.OnMouseDown(point);
        //    }
        //    return true;
        //}
    }
}