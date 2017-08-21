using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class ExternalButton:NewQBaseclass
    {
        public List<Rectangle> Rects { get; private set; }

        public List<Region> Regions { get;private set; }

        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            InitData();
            return base.init(ref nErrorObjectIndex);
        }
        public override void paint(Graphics g)
        {
            DrawButtonImgs(g);
            base.paint(g);
        }
        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (;index<35;index++)
            {
                if (Regions[index].IsVisible(point))
                {
                    switch (index)
                    {
                        case 0:
                           
                            break;
                        case 1:
                            break;
                        case 2:
                            break;
                    }
                }
            }
            return base.mouseDown(point);
        }
        public override bool mouseUp(Point point)
        {
            return base.mouseUp(point);
        }

        private void DrawButtonImgs(Graphics e)
        {
         for (int i = 0; i< 4;i++)
         {
             e.DrawImage(m_Imgs[i], Rects[i]);
         }
        }

        /// <summary>
        /// 数据的初始化
        /// </summary>
        private void InitData()
        {
            Rects = new List<Rectangle>();

            Regions = new List<Region>();

          
            for (int i = 0; i < 2; i++)
            {
                Rects.Add(new Rectangle(7, 632 + i * 50, 445, 44));
            }

            for (int i = 0; i < 2; i++)
            {
                Rects.Add(new Rectangle(813 + i * 50, 166, 46, 287));
            }
            for (int i=0;i<6;i++)
            {
                Rects.Add(new Rectangle(823, 197+31*i, 30, 30));
            }
            for (int i=0;i<6;i++)
            {
                Rects.Add(new Rectangle(873,197+i*30,30,30));
            }
            Rects.Add(new Rectangle(873, 345, 30, 60));

            for (int i=0;i<10;i++)
            {
                Rects.Add(new Rectangle(22+i*40, 663, 28, 28));
            }

             Rects.Add(new Rectangle(417,667,25,31));

            for (int i=0;i<11;i++)
            {
                Rects.Add(new Rectangle(20+i*42,711,31,30));
            }
            for (int i=0;i<35;i++)
            {
                Regions.Add(new Region(Rects[4 + i]));
            }
        }

    }
}
