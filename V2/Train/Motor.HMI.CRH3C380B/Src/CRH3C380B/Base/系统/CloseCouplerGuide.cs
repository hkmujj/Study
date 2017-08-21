using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util;
using Motor.HMI.CRH3C380B.Base.系统.CloseCouplerHatches;

namespace Motor.HMI.CRH3C380B.Base.系统
{
    /// <summary>
    /// 关闭车√状态指南
    /// </summary>
    public class CouplerGuide : CommonInnerControlBase
    {
        protected List<CommonInnerControlBase> ConstInfoCollection;

        public CouplerGuide(int x = 420, int y = 50)
        {
            const int offsetX = 10;
            const int offsetY = 10;
            var sizeItem = new Size(25, 25);
            var sizeStr = new Size(100, 25);
            var regionSize = new Size(350, 125);

            m_OutLineRectangle = new Rectangle(x, y, regionSize.Width, regionSize.Height);
            ConstInfoCollection = new List<CommonInnerControlBase>
                                    {
                                        new GDIRectText
                                        {
                                            OutLinePen = Pens.White,
                                            OutLineRectangle = new Rectangle(x, y, regionSize.Width, regionSize.Height),
                                            NeedDarwOutline = true
                                        }
                                    };
            for (int i = 0; i < 3; i++)
            {
                var location = new Point(x + offsetX, y + offsetY * (i + 1) + sizeItem.Height * i);

                ConstInfoCollection.Add(new CouplerHatchesViewItem { OutLineRectangle = new Rectangle(location, sizeItem), State = (CouplerHatchesState)i });

                location.Offset(sizeItem.Width + offsetX, 0);

                ConstInfoCollection.Add(new GDIRectText
                {
                    Text = EnumUtil.GetDescription((CouplerHatchesState)i).FirstOrDefault(),
                    TextColor = Color.White,
                    OutLineRectangle = new Rectangle(location, sizeStr)
                });
            }
            for (int i = 0; i < 3; i++)
            {
                var location = new Point(x + offsetX + sizeItem.Width + offsetX + sizeStr.Width + offsetX, y + offsetY * (i + 1) + sizeItem.Height * i);

                ConstInfoCollection.Add(new CouplerHatchesViewItem { OutLineRectangle = new Rectangle(location, sizeItem), State = (CouplerHatchesState)(i == 0 ? 3 : i == 1 ? 5 : 7) });

                location.Offset(sizeItem.Width + offsetX, 0);

                ConstInfoCollection.Add(new GDIRectText
                {
                    Text = EnumUtil.GetDescription((CouplerHatchesState)(i == 0 ? 3 : i == 1 ? 5 : 7)).FirstOrDefault(),
                    TextColor = Color.White,
                    OutLineRectangle = new Rectangle(location, sizeStr)
                });
            }
        }

        public override void OnDraw(Graphics g)
        {
            ConstInfoCollection.ForEach(e => e.OnDraw(g));
        }
    }
}