using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.底层共用;


namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class WaitForStart: NewQBaseclass
    {
        private FlashTimer myTime = new FlashTimer(1);
        public List<RectangleF> Rects;
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            InitData();
            return base.init(ref nErrorObjectIndex);
        }
        public override void paint(Graphics g)
        {
            PaintBackGround(g);
            base.paint(g);
        }

        void PaintBackGround(Graphics e)
        {
            e.DrawImage(m_Imgs[0], Rects[0]);
            e.FillRectangle(Common.m_BlueBrush, Rects[1]);

            if (myTime.isDone(FloatList[m_FoolatIds[0]]))
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
            else
            {
                float m=myTime.GetTimeFlash();
                float n=m-(int)m;
                if (m % 4 <= 1.0f)
                {
                    e.FillRectangle(Common.m_ThinBlue, Rects[1].X, Rects[1].Y, n * Rects[1].Width, Rects[1].Height);
                    return;
                }
                if (m % 4 <= 2.0f)
                {
                    e.FillRectangle(Common.m_ThinBlue,Rects[1].X,Rects[1].Y,(1-n)*Rects[1].Width,Rects[1].Height);
                    return;
                }
                if (m % 4 < 3.0f)
                {
                    e.FillRectangle(Common.m_ThinBlue, Rects[1].X+(1-n)*Rects[1].Width, Rects[1].Y, n * Rects[1].Width, Rects[1].Height);
                    return;
                }
                e.FillRectangle(Common.m_ThinBlue, Rects[1].X + n * Rects[1].Width, Rects[1].Y, (1-n) * Rects[1].Width, Rects[1].Height);
            }
        }
        /// <summary>
        /// 数据的初始化
        /// </summary>
        private void InitData()
        {
            Rects = new List<RectangleF>();
            Rects.Add(new RectangleF(0,0,800,620));
            Rects.Add(new RectangleF(200, 400, 400, 15));
        }
    }
}
