using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.PublicUI;
using System.IO;

namespace NJ_MMI
{
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V1_C0_Frame : baseClass
    {
        private Image background;

        public override string GetInfo()
        {
            return "背景";
        }

        public override bool init(ref int nErrorObjectIndex)
        {

            using (FileStream fs = new FileStream(Path.Combine(RecPath , UIObj.ParaList[0]), FileMode.Open))
            {
                background = Image.FromStream(fs);
            }

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.DrawImage(background, new Rectangle(0, 0, 800, 600));

            drawText(dcGs);
        }

        private void drawText(Graphics dcGs)
        {
            Font f = new Font("Arial", 16, FontStyle.Bold);
            dcGs.DrawString("模式", f, new SolidBrush(Color.White), new PointF(30, 15));
            dcGs.DrawString("停靠", f, new SolidBrush(Color.White), new PointF(226, 15));
            dcGs.DrawString("停站", f, new SolidBrush(Color.White), new PointF(435, 15));
            dcGs.DrawString("发车", f, new SolidBrush(Color.White), new PointF(630, 15));

            Font f1 = new Font("Arial", 12, FontStyle.Bold);
            dcGs.DrawString("目标", f1, new SolidBrush(Color.White), new PointF(46,100));
            dcGs.DrawString("米", f1, new SolidBrush(Color.White), new PointF(80, 390));

            Font f2 = new Font("Arial", 15, FontStyle.Bold);
            dcGs.DrawString("公里/时", f2, new SolidBrush(Color.White), new PointF(270,270));
            dcGs.DrawString("实际：", f2, new SolidBrush(Color.White), new PointF(155, 375));
            dcGs.DrawString("目标：", f2, new SolidBrush(Color.White), new PointF(270, 375));
            dcGs.DrawString("最大：", f2, new SolidBrush(Color.White), new PointF(385, 375));

            Font f3 = new Font("Arial", 12, FontStyle.Bold);
            dcGs.DrawString("列车门", f3, new SolidBrush(Color.White), new PointF(543, 212));
            dcGs.DrawString("站台门", f3, new SolidBrush(Color.White), new PointF(685, 212));
            dcGs.DrawString("下一站", f3, new SolidBrush(Color.White), new PointF(543, 320));
            dcGs.DrawString("终点站", f3, new SolidBrush(Color.White), new PointF(685, 320));

            Font f4 = new Font("Arial", 11, FontStyle.Bold);
            dcGs.DrawString("日期", f4, new SolidBrush(Color.White), new PointF(10, 562));
            dcGs.DrawString("时间", f4, new SolidBrush(Color.White), new PointF(183, 562));
            dcGs.DrawString("司机", f4, new SolidBrush(Color.White), new PointF(295, 562));
            dcGs.DrawString("车次", f4, new SolidBrush(Color.White), new PointF(415, 562));
            dcGs.DrawString("编组", f4, new SolidBrush(Color.White), new PointF(504, 562));
            dcGs.DrawString("VOBCs", f4, new SolidBrush(Color.White), new PointF(586, 562));
        }
    }
}
