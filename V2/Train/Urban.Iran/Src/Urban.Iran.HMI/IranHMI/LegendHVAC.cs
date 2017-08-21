using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class LegendHVAC : HMIBase
    {
        private  Rectangle[] m_RectArr;
        private  Bitmap[] m_BmpArr;
        private  FjButtonEx m_BackBtn;

        public LegendHVAC()
        {
            
        }

        public override string GetInfo()
        {
            return "LegendHVAC";
        }

        protected override bool Initalize()
        {
            m_RectArr = new[]
                      {
                          new Rectangle(120, 186, 20, 20),
                          new Rectangle(120, 221, 20, 20),
                          new Rectangle(120, 246, 20, 20),
                          new Rectangle(402, 184, 20, 40),
                          new Rectangle(402, 240, 20, 40),
                          new Rectangle(402, 286, 20, 40),
                          new Rectangle(402, 332, 20, 40),
                          new Rectangle(402, 378, 20, 40),

                          new Rectangle(150, 186, 240, 20),
                          new Rectangle(150, 221, 240, 20),
                          new Rectangle(150, 246, 240, 20),
                          new Rectangle(432, 184, 200, 40),
                          new Rectangle(432, 240, 200, 40),
                          new Rectangle(432, 286, 200, 40),
                          new Rectangle(432, 332, 200, 40),
                          new Rectangle(432, 378, 200, 40),
                      };

            m_BmpArr = new[]
                     {
                         new Bitmap(RecPath + "\\frame/unitOn.jpg"),
                         new Bitmap(RecPath + "\\frame/unitOff.jpg"),
                         new Bitmap(RecPath + "\\frame/unitFaulty.jpg"),
                         new Bitmap(RecPath + "\\frame/airFull.jpg"),
                         new Bitmap(RecPath + "\\frame/air2in3.jpg"),
                         new Bitmap(RecPath + "\\frame/air1in3.jpg"),
                         new Bitmap(RecPath + "\\frame/airOff.jpg"),
                         new Bitmap(RecPath + "\\frame/airFaulty.jpg")
                     };

            m_BackBtn = new FjButtonEx(1, GlobleParam.ResManager.GetString("String243"), GlobleRect.m_LegendbtnRect);
            m_BackBtn.MouseDown += BackBtnMouseDown;
            return true;
        }

        public override void paint(Graphics g)
        {
            GlobleParam.Instance.CurrentIranViewIndex = (IranViewIndex) 10;
            var temp = 92;
            GdiCommon.m_StrFormat.LineAlignment = StringAlignment.Center;
            for (var index = 0; index < 8; ++index)
            {
                g.DrawImage(m_BmpArr[index], m_RectArr[index]);
                g.DrawString(GlobleParam.ResManager.GetString("String" + (temp++)),
                    GdiCommon.Txt12Font,
                    GdiCommon.MediumGreyBrush,
                    m_RectArr[index + 8],
                    GdiCommon.m_StrFormat);
            }
            m_BackBtn.OnDraw(g);
        }

        public override bool mouseDown(Point point)
        {
            if (m_BackBtn.IsVisible(point))
                m_BackBtn.OnMouseDown(point);
            return true;
        }

        private void BackBtnMouseDown(FjButtonEx btnSender, Point pt)
        {
            ChangedPage(IranViewIndex.HVACPage2);;
        }
    }
}