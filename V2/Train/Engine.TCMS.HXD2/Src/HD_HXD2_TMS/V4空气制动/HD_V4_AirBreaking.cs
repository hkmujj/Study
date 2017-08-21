using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System;
using System.Drawing;

namespace HD_HXD2_TMS.V4空气制动
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V4_AirBreaking : baseClass
    {
        public override string GetInfo()
        {
            return "空气制动试图-概况";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            String[] strs = {"主控/从控/回送", "切入/隔离", "制动缸1", "制动缸2", "均衡风缸", "总风缸", "列车管", "流量", "列车管额定压力"};
            for (int i = 0; i < 9; i++)
            {
                dcGs.DrawRectangle(Pens.White, 223, 84 + 118+25*i, 360, 25);
                dcGs.DrawString(
                    strs[i],
                    new Font("宋体", 11),
                    Brushes.White,
                    new RectangleF(223, 84 + 118 + 25 * i+2, 200, 25),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
            }
            dcGs.DrawLine(Pens.White, new Point(423, 84 + 118), new Point(423, 84 + 118 + 25*9));

            //主控状态
            String[] strs1 = {"补机", "本机", "单机"};
            for (int i = 0; i < 3; i++)
            {
                if(BoolList[UIObj.InBoolList[0]+i])
                    dcGs.DrawString(
                        strs1[i],
                        new Font("宋体", 11),
                        Brushes.Yellow,
                        new RectangleF(423, 84 + 118 + 25 * 0 + 2, 160, 25),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                        );
            }

            //切入状态
            String[] strs2 = { "货车位", "客车位" };
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                    dcGs.DrawString(
                        strs2[i],
                        new Font("宋体", 11),
                        Brushes.Yellow,
                        new RectangleF(423, 84 + 118 + 25 * 1 + 2, 160, 25),
                        new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                        );
            }

            for (int i = 0; i < 7; i++)
            {
                dcGs.DrawString(
                    FloatList[UIObj.InFloatList[0]+i].ToString("0"),
                    new Font("宋体", 11),
                    Brushes.Yellow,
                    new RectangleF(423, 84 + 118 + 25 * (i+2) + 2, 160, 25),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
            }

            base.paint(dcGs);
        }
    }
}
