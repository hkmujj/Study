using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
    /// <summary>
    /// 功能描述 MainAeroB类 
    ///     信号屏起摸区 显示
    /// 创建人：袁 凯    
    /// 创建时间：2013-12-16
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    internal class QiMoQu : ShowTypeEffectBaseClass
    {
        #region 私有字段

        private readonly float[] m_FValue = new float[100];

        private readonly StringFormat m_DrawFormat = new StringFormat();

        private readonly SolidBrush m_DistanceBrush = new SolidBrush(Color.FromArgb(255, 255, 255));

        /// <summary>
        /// 运行计划信息
        /// </summary>
        private readonly PointF[] m_Planpoint = new PointF[2];

        private readonly Pen m_PlanPen = new Pen(Color.FromArgb(255, 255, 255), 1);

        private readonly Font m_PlanFont = new Font(FormatStyle.StrFont, 10f);

        private int m_Cmdflag1;
        private int m_Cmdflag2;
        private int m_Cmdflag3;

        private int m_Cmdcount;

        #endregion

        #region 起模区参数

        /// <summary>
        /// 缩放模式 默认为0
        /// </summary>
        private readonly int m_ScaleModel = 0;

        /// <summary>
        /// 起模区 矩形
        /// </summary>
        private Rectangle m_Rect;

        #endregion

        #region 接口数据

        private readonly float[] m_PointValue = new float[20];

        private float m_QiMoDian;

        private SolidBrush m_BackgroundSolidBrush;
        private Pen m_StartPointPen;
        private SolidBrush m_DarySolidBrush;
        private SolidBrush m_SolidBrush1;
        private Font m_Font12;
        private int[] m_DistanceIndex;
        private int[] m_SpeedIndex;

        #endregion

        #region  重载方法

        /// <summary>
        /// 获取图元对象名称
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "起模区显示";
        }

        /// <summary>
        /// 图元初始化过程执行过程 (只在初始化过程执行一次)
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        // ReSharper disable once RedundantAssignment
        public override bool init(ref int nErrorObjectIndex)
        {
            m_DistanceIndex = GetInstanceIndexs().ToArray();
            m_SpeedIndex = GetSpeedIndexs().ToArray();

            InitData();

            m_StartPointPen = new Pen(Color.FromArgb(223, 223, 0), 2);
            m_BackgroundSolidBrush = new SolidBrush(Color.FromArgb(10, 24, 70));
            m_DarySolidBrush = new SolidBrush(Color.FromArgb(195, 195, 195));
            m_SolidBrush1 = new SolidBrush(Color.FromArgb(85, 85, 85));
            m_Font12 = new Font(FormatStyle.StrFont, 12f);

            nErrorObjectIndex = -1;

            return true;
        }

        /// <summary>
        /// 绘制方法(该方法会被定时器按一定的时间间隔重复调用) 
        /// </summary>
        /// <param name="g">参数 GDI+ 绘图对象</param>
        public override void paint(Graphics g)
        {
            RefreshShowType();

            UpdateValue();

            if (ShowType == ShowType.Normal)
            {
                OnDrawplan(g);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void InitData()
        {
            m_Rect = new Rectangle(416, 68, 294, 190);
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void UpdateValue()
        {
            for (var index = 0; index < 20; index++)
            {
                m_PointValue[index] = FloatList[UIObj.InFloatList[index]];
            }

            m_QiMoDian = FloatList[this.GetInFloatIndex(InFloatKeys.Inf起模点横坐标)];
        }


        #region 绘制计划区

        /// <summary>
        /// 计划信息区横坐标值
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public float Getx(float x)
        {
            float y = 0;
            if (x < 0)
            {
                x = 0;
            }
            switch (m_ScaleModel)
            {
                case 0:
                    if (x < 1000f)
                    {
                        y = 416 + x*80/1000f;
                    }
                    else
                    {
                        y = (float) (496 + 40*Math.Log10(x/1000f)/Math.Log10(2));
                    }
                    break;
                case 1:

                    if (x < 500f)
                    {
                        y = 416 + x*80/500f;
                    }
                    else
                    {
                        y = (float) (496 + 40*Math.Log10(x/500f)/Math.Log10(2));
                    }
                    break;
                case 2:
                    if (x < 250f)
                    {
                        y = 416 + x*80/1000f;
                    }
                    else
                    {
                        y = (float) (496 + 40*Math.Log10(x/250f)/Math.Log10(2));
                    }
                    break;
            }

            return y + 1;
        }

        /// <summary>
        /// 计划信息区纵坐标值
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public float Gety(float x)
        {
            if (x < 0)
            {
                x = 0;
            }
            else if (x > 450)
            {
                x = 450;
            }
            var y = x*40/100f;

            return 258 - y;
        }


        private IEnumerable<int> GetInstanceIndexs()
        {
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息1);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息2);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息3);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息4);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息5);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息6);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息7);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息8);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息9);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速速度的距离信息10);

        }

        private IEnumerable<int> GetSpeedIndexs()
        {
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息1);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息2);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息3);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息4);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息5);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息6);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息7);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息8);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息9);
            yield return this.GetInFloatIndex(InFloatKeys.Inf限速信息10);

        }

        //计划信息区
        public void OnDrawplan(Graphics g)
        {

            g.FillRectangle(m_BackgroundSolidBrush, m_Rect);

            //MRSP
            var flag = false;
            float mrspX;

            float lastx = 416;
            float lasty = 258;
            var psudu = new PointF();
            for (var i = 0; i < 10; i++)
            {
                mrspX = Getx(m_DistanceIndex[i]); //得到转换后的坐标
                var y = Gety(m_SpeedIndex[i]);

                if (i != 0)
                {
                    if (mrspX <= 417 || mrspX > 696) //容错处理
                    {
                        mrspX = 696;
                    }
                    if (i == 10)
                    {
                        mrspX = 696;
                        y = Gety(m_PointValue[1 + 9*2]);
                    }

                    g.FillRectangle(Brushes.Red, lastx, lasty, mrspX - lastx, 258 - lasty);

                    if (y < lasty && Math.Abs(mrspX - 696) > float.Epsilon)
                    {
                        // e.DrawImage(Img[59], _mrspX, 259, Img[59].Width, Img[59].Height);
                    }
                    else if (y > lasty && Math.Abs(mrspX - 696) > float.Epsilon)
                    {
                        if (!flag)
                        {
                            psudu.X = Getx(m_QiMoDian);
                            psudu.Y = lasty;

                            PointF[] temp = {new PointF(mrspX, y), new PointF(mrspX, lasty), new PointF(psudu.X, lasty)};
                            g.FillPolygon(m_BackgroundSolidBrush, temp);
                        }
                        flag = true;
                    }
                }


                lastx = mrspX;
                lasty = y;
            }

            for (var i = 0; i < 8; i++)
            {
                if (i == 1)
                {
                    continue;
                }
                m_Planpoint[0].X = 416 + i*40f;
                m_Planpoint[0].Y = 68;
                m_Planpoint[1].X = 416 + i*40f;
                m_Planpoint[1].Y = 352;
                g.DrawLine(m_PlanPen, m_Planpoint[0], m_Planpoint[1]);
            }

            for (var i = 0; i < 5; i++)
            {
                m_Planpoint[0].X = 416;
                m_Planpoint[0].Y = 258 - 40*i;
                m_Planpoint[1].X = 710;
                m_Planpoint[1].Y = 258 - 40*i;
                g.DrawLine(m_PlanPen, m_Planpoint[0], m_Planpoint[1]);

            }

            if (m_ScaleModel == 0)
            {
                g.DrawString("1000", m_PlanFont, m_DistanceBrush, 570, 360);
                g.DrawString("2000", m_PlanFont, m_DistanceBrush, 610, 360);
                g.DrawString("4000", m_PlanFont, m_DistanceBrush, 650, 360);
                g.DrawString("8000", m_PlanFont, m_DistanceBrush, 690, 360);
            }

            g.DrawString("0", m_PlanFont, m_DistanceBrush, 418, 242);
            g.DrawString("50", m_PlanFont, m_DistanceBrush, 418, 202);
            g.DrawString("100", m_PlanFont, m_DistanceBrush, 418, 162);
            g.DrawString("150", m_PlanFont, m_DistanceBrush, 418, 122);
            g.DrawString("200", m_PlanFont, m_DistanceBrush, 418, 82);
            g.DrawString("250", m_PlanFont, m_DistanceBrush, 418, 62);


            float lastx1 = 0;
            float lastx2 = 0;

            var cmdflag = 0;
            m_Cmdcount = 0;
            //预告
            for (var i = 0; i < 10; i++)
            {
                var yugao = (int) m_FValue[46 + i*2];
                if (yugao != 0)
                {
                    mrspX = Getx(m_FValue[45 + i*2]);
                    if (mrspX < 417 || mrspX > 696) //容错处理
                    {
                        continue;
                    }

                    if (i%2 == 0)
                    {
                        if (mrspX > lastx1)
                        {
                            lastx1 = mrspX + 23;
                        }
                    }
                    else
                    {
                        if (mrspX > lastx2)
                        {
                            lastx2 = mrspX + 23;
                        }

                    }
                    if (yugao == 5)
                    {
                        continue;
                    }
                    if (mrspX < 425)
                    {
                        m_Cmdcount++;
                    }
                    if (cmdflag >= m_Cmdcount)
                    {
                        continue;
                    }
                    //命令图标区
                    if (m_Cmdflag1 == 0 && mrspX < 425)
                    {
                        m_Cmdflag1 = 49 + yugao;
                        cmdflag++;
                    }
                    else if (m_Cmdflag2 == 0 && mrspX < 425)
                    {
                        m_Cmdflag2 = 49 + yugao;
                        cmdflag++;
                    }
                    else if (m_Cmdflag3 == 0 && mrspX < 425)
                    {
                        m_Cmdflag3 = 49 + yugao;
                        cmdflag++;
                    }
                }
            }

            //坡度
            lastx1 = 417;
            var lastpodu = 0;

            for (var i = 0; i < 5; i++)
            {
                mrspX = Getx(m_FValue[37 + i*2]);
                if ((mrspX < 417 || mrspX > 696) && i != 4) //容错处理
                {
                    continue;
                }

                var podu = (int) m_FValue[36 + i*2];
                if (i == 4)
                {
                    mrspX = 696;
                    podu = lastpodu;
                }
      
                if (podu > 0)
                {
                    g.FillRectangle(m_DarySolidBrush, lastx1, 281, mrspX - lastx1, 13);
                    if (mrspX - lastx1 > 29)
                    {
                        g.DrawString("+" + podu + "+", m_Font12,
                            m_SolidBrush1, new RectangleF(lastx1, 281, mrspX - lastx1, 13),
                            m_DrawFormat);
                    }

                }
                else if (podu == 0)
                {
                    g.FillRectangle(m_DarySolidBrush, lastx1, 281, mrspX - lastx1, 13);
                    if (mrspX - lastx1 > 29)
                    {
                        g.DrawString("0", m_Font12, m_SolidBrush1,
                            new RectangleF(lastx1, 281, mrspX - lastx1, 13), m_DrawFormat);
                    }
                }
                else if (podu < 0)
                {
                    g.FillRectangle(m_SolidBrush1, lastx1, 281, mrspX - lastx1, 13);
                    if (mrspX - lastx1 > 29)
                    {
                        g.DrawString("-" + (-podu) + "-", m_Font12,
                            m_DarySolidBrush,
                            new RectangleF(lastx1, 281, mrspX - lastx1, 13), m_DrawFormat);
                    }

                }
                lastpodu = podu;
                lastx1 = mrspX;
            }

            m_Planpoint[0].X = Getx(m_FValue[65]);
            m_Planpoint[0].Y = 60;
            m_Planpoint[1].X = Getx(m_FValue[65]);
            m_Planpoint[1].Y = 258;
            if (m_FValue[65] > 0 && m_Planpoint[0].X < 696)
            {
                
                g.DrawLine(m_StartPointPen, m_Planpoint[0], m_Planpoint[1]);
            }
        }

        #endregion

        #endregion
    }
}
