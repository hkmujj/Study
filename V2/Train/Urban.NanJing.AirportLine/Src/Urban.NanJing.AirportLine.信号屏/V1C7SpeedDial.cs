using System;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1C7SpeedDial : baseClass
    {
        private SpeedPointer m_SpeedPointer;
        private SpeedPointer m_SpeedPointerTarget;
        private SpeedPointer m_SpeedPointerMax;
        private Image m_Pointer;
        private Image m_PointerTargetSpeed;
        private Image m_PointerMaxSpeed;
        private NeedChangeLength m_TargetPoint;

        public override string GetInfo()
        {
            return "速度表盘";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_SpeedPointer = new SpeedPointer(270f, -45f, 120f, new PointF(149f - 10, 79 - 10), new PointF(168 + 149 - 1, 168 + 79));
            m_SpeedPointerTarget = new SpeedPointer(270f, -45f, 120f, new PointF(149f - 10, 79 - 10), new PointF(168 + 149 - 1, 168 + 79));
            m_SpeedPointerMax = new SpeedPointer(270f, -45f, 120f, new PointF(149f - 10, 79 - 10), new PointF(168 + 149 - 1, 168 + 79));
            using (FileStream fs = new FileStream(RecPath + "\\" + UIObj.ParaList[0], FileMode.Open))
            {
                m_Pointer = Image.FromStream(fs);
            }
            using (FileStream fs = new FileStream(RecPath + "\\" + UIObj.ParaList[1], FileMode.Open))
            {
                m_PointerTargetSpeed = Image.FromStream(fs);
            }
            using (FileStream fs = new FileStream(RecPath + "\\" + UIObj.ParaList[2], FileMode.Open))
            {
                m_PointerMaxSpeed = Image.FromStream(fs);
            }

            m_TargetPoint = new NeedChangeLength(
                new PointF(28, 405),
                18,
                25.9f,
                new float[] { 1, 1f, 3f, 5f, 10f, 30, 50, 100, 300, 500 },
                new float[] { 0, 1, 2, 5, 10, 20, 50, 100, 200, 500, 1000 }
                );

            return true;
        }

        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="g"></param>
        public override void paint(Graphics g)
        {
            paint_Speed(g);
            paint_TargetPosition(g);

            base.paint(g);
        }

        /// <summary>
        /// 绘制停车距离
        /// </summary>
        /// <param name="g"></param>
        private void paint_TargetPosition(Graphics g)
        {
            //停车距离
            g.DrawString(
                "目标",
                Global.m_FontArial12B,
                Brushes.White,
                new RectangleF(8, 58 + 40, 119, 403),
                Global.m_SfCn
                );
            g.DrawString(
                "米",
                Global.m_FontArial12B,
                Brushes.White,
                new RectangleF(8 + 12, 58 - 20, 119, 403),
                Global.m_SfCf
                );
            float distance = FloatList[UIObj.InFloatList[3]];
            m_TargetPoint.DrawRectangle(g, ref distance, 3);
            g.DrawString(
                (FloatList[UIObj.InFloatList[3]]<=1000?FloatList[UIObj.InFloatList[3]]:1000).ToString("0"),
                Global.m_FontArial12B,
                Brushes.Yellow,
                new RectangleF(8, 58 - 20, 60, 403),
                Global.m_SfCf
                );
        }

        private Brush[] m_Brushs1 = new Brush[] { Brushes.Lime, Brushes.Red };
        private Brush[] m_Brushs2 = new Brush[] { Brushes.Red, Brushes.Lime, Brushes.Red };

        /// <summary>
        /// 绘制表盘
        /// </summary>
        /// <param name="g"></param>
        private void paint_Speed(Graphics g)
        {
            if (m_Pointer != null)
            {
                float realSpeed = FloatList[UIObj.InFloatList[0]];
                m_SpeedPointer.PaintPointer(g, m_Pointer, ref realSpeed);
            }

            if (m_PointerTargetSpeed != null)
            {
                float realSpeed = FloatList[UIObj.InFloatList[1]];
                m_SpeedPointerTarget.PaintPointer(g, m_PointerTargetSpeed, ref realSpeed);
            }

            if (m_PointerMaxSpeed != null)
            {
                float realSpeed = FloatList[UIObj.InFloatList[2]];
                m_SpeedPointerMax.PaintPointer(g, m_PointerMaxSpeed, ref realSpeed);
            }

            g.DrawString(
                "公里/时",
                Global.m_FontArial15B,
                Brushes.White,
                new RectangleF(272, 282, 82, 30),
                Global.m_SfCc
                );

            
            String[] strs1 = new[] { " 向前", "向后" };
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    g.DrawString(
                        strs1[i],
                        Global.m_FontArial15B,
                        m_Brushs1[i],
                        new RectangleF(267, 282 + 30, 82, 30),
                        Global.m_SfCc
                        );
                }
            }

            
            String[] strs2 = new[] { " 未较准", "校准", "记忆" };
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    g.DrawString(
                        strs2[i],
                        Global.m_FontArial15B,
                        m_Brushs2[i],
                        new RectangleF(267, 282 + 30 + 30, 82, 30),
                        Global.m_SfCc
                        );
                }
            }

            g.DrawString(
                "实际：",
                Global.m_FontArial15B,
                Brushes.White,
                new RectangleF(159, 400, 315, 30),
                Global.m_SfNc
                );
            g.DrawString(
                FloatList[UIObj.InFloatList[0]].ToString("0"),
                Global.m_FontArial15B,
                Brushes.Lime,
                new RectangleF(214, 400, 315, 30),
                Global.m_SfNc
                );

            g.DrawString(
                "目标：",
                Global.m_FontArial15B,
                Brushes.White,
                new RectangleF(159 - 15, 400, 315, 30),
                Global.m_SfCc
                );
            g.DrawString(
                FloatList[UIObj.InFloatList[1]].ToString("0"),
                Global.m_FontArial15B,
                Brushes.Yellow,
                new RectangleF(318, 400, 315, 30),
                Global.m_SfNc
                );

            g.DrawString(
                "最大：",
                Global.m_FontArial15B,
                Brushes.White,
                new RectangleF(159 - 20, 400, 315, 30),
                Global.m_SfFc
                );
            g.DrawString(
                FloatList[UIObj.InFloatList[2]].ToString("0"),
                Global.m_FontArial15B,
                Brushes.Red,
                new RectangleF(435, 400, 315, 30),
                Global.m_SfNc
                );
        }
    }
}
