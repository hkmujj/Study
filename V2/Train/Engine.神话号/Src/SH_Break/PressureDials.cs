using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
//using System.Windows.Forms;
using System.Timers;

namespace NJ_MMI
{
    /// <summary>
    /// 速度指针
    /// </summary>
    public class Pointer
    {
        /// <summary>
        /// 指针起始点
        /// </summary>
        private PointF StartPoint;
        /// <summary>
        /// 指针顶点
        /// </summary>
        private PointF EndPoint;
        /// <summary>
        /// 指针画笔
        /// </summary>
        private Pen P;
        /// <summary>
        /// 表盘最大角度
        /// </summary>
        private float MaxAnglev;
        /// <summary>
        /// 表盘最大值
        /// </summary>
        private float MaxValue;
        /// <summary>
        /// 转动角度
        /// </summary>
        private float Anglev;
        /// <summary>
        /// 实际值
        /// </summary>
        private float ShijiValue;
        private Matrix matrix;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="p"></param>
        /// <param name="maxAnglev"></param>
        /// <param name="maxValue"></param>
        /// <param name="shijiValue"></param>
        public Pointer(PointF startPoint,PointF endPoint,float maxAnglev,float maxValue)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            MaxAnglev = maxAnglev;
            MaxValue = maxValue;
            
        }
        /// <summary>
        /// 绘制指针
        /// </summary>
        /// <param name="g"></param>
        public void drawPointer(Graphics g ,Image tmpPic, ref float shijiValue )
        {
            ShijiValue = shijiValue;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.FillEllipse(Global.brush1,StartPoint.X-3,StartPoint .Y -3,6,6 );
            try
            {
                if (ShijiValue <= MaxValue)
                {
                    Anglev = ShijiValue * (MaxAnglev / MaxValue) + (360 - MaxAnglev) / 2;
                }
                else
                {
                    Anglev =MaxAnglev + (360 - MaxAnglev) / 2;
                }
                matrix = g.Transform;
                matrix.RotateAt(Anglev, StartPoint);
                g.Transform = matrix;
                g.DrawImage(tmpPic,new RectangleF ((StartPoint.X-2F),StartPoint .Y ,4F,EndPoint .Y-StartPoint.Y));
                matrix.Reset();
                g.Transform = matrix;

            }
                
            catch (Exception e)
            {
            }
            //Anglev = ShijiValue * (MaxAnglev / MaxValue) + (360 - MaxAnglev) / 2;
            //g.TranslateTransform(StartPoint.X, StartPoint.Y);
            //g.RotateTransform(Anglev);
            //g.TranslateTransform(-StartPoint.X, -StartPoint.Y);
            //g.DrawLine(P, StartPoint, EndPoint);
            //g.ResetTransform();
        }
       
    }
    /// <summary>
    /// 表盘
    /// </summary>
    public class biaopan
    {
        Pen P;
        private float QishiAnglev;
        private float MaxAnglev;
        private Rectangle PosRect;
        public biaopan(float qishiAnglev, float maxAnglec, Rectangle posRect, Pen p)
        {
            QishiAnglev = qishiAnglev;
            MaxAnglev = maxAnglec;
            PosRect = posRect;
            P = p;
        }
        public void PaintBiaopan(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.DrawArc(P, PosRect, QishiAnglev, MaxAnglev);
        }
    }
    /// <summary>
    /// 刻度
    /// </summary>
    public class KeDu
    {
        /// <summary>
        /// 刻度所在圆半径
        /// </summary>
        private int BanJin;
        /// <summary>
        /// 无刻度部分角度
        /// </summary>
        private float QueshengAnglev;
        /// <summary>
        /// 大刻度数量值
        /// </summary>
        private int BigStep;
        /// <summary>
        /// 刻度圆圆心
        /// </summary>
        private Point CenterPoint;
        /// <summary>
        /// 绘制刻度的画笔
        /// </summary>
        public Pen P;
        /// <summary>
        /// 构造刻度方法
        /// </summary>
        /// <param name="banJin"></param>
        /// <param name="queshengAnglev"></param>
        /// <param name="bigStep"></param>
        /// <param name="centerPoint"></param>
        /// <param name="p"></param>
        public KeDu(int banJin, float queshengAnglev, int bigStep, Point centerPoint, Pen p)
        {
            BanJin = banJin;
            QueshengAnglev = queshengAnglev;
            BigStep = bigStep;
            CenterPoint = centerPoint;
            P = p;
        }
        /// <summary>
        /// 绘制刻度
        /// </summary>
        /// <param name="g"></param>
        public void drawKeDU(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TranslateTransform(CenterPoint.X, CenterPoint.Y);
            g.RotateTransform(QueshengAnglev / 2);
            g.TranslateTransform(-CenterPoint.X, -CenterPoint.Y);
            g.DrawLine(P, new Point(CenterPoint.X, CenterPoint.Y + BanJin - 20), new Point(CenterPoint.X, CenterPoint.Y + BanJin));
            for (int i = 1; i <= 10 * BigStep; i++)
            {
                g.TranslateTransform(CenterPoint.X, CenterPoint.Y);
                g.RotateTransform((360 - QueshengAnglev) / (10 * BigStep));
                g.TranslateTransform(-CenterPoint.X, -CenterPoint.Y);
                if (i % 5 == 0)
                {
                    g.DrawLine(P, new Point(CenterPoint.X, CenterPoint.Y + BanJin - 20), new Point(CenterPoint.X, CenterPoint.Y + BanJin));
                }
                else
                {
                    g.DrawLine(P, new Point(CenterPoint.X, CenterPoint.Y + BanJin - 10), new Point(CenterPoint.X, CenterPoint.Y + BanJin));
                }


            }
            g.ResetTransform();
        }
        [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
        public class PressureDials : baseClass
        {
            /// <summary>
            /// 设置画笔
            /// </summary>
             Pen p1 = new Pen(Color.White);
             Pen p2 = new Pen(Color.White, 2);
             Pen p3 = new Pen(Color.Red, 3);
             Pen p4 = new Pen(Color.White, 3);
             private List<Image> _images = new List<Image>();
             private Image _pointerwhite;
             private Image _pointerred;
            /// <summary>
            /// 设置字体
            /// </summary>
            StringFormat format = new StringFormat(StringFormatFlags.DirectionVertical);
            StringFormat format2 = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            biaopan _biappan1;
            biaopan _biaopan2;
            KeDu _kedu1;
            KeDu _kedu2;
            Pointer pointer1;
            Pointer pointer2;
            Pointer pointer3;
            Pointer pointer4;
            private string[] line;
            private List<int> id = new List<int>();
            Timer timer = new Timer(1000);

            public int totaltime;
            public override string GetInfo()
            {
                return "表盘界面";
            }

            public override bool init(ref int nErrorObjectIndex)
            {
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                //timer.Enabled = true;
                timer.AutoReset = true;
                totaltime = 60;
                System.Text.Encoding.GetEncoding("gb2312");
                line = File.ReadAllLines(@"1D\config\显示内容配置.txt", System.Text.Encoding.Default);
                for (int i = 0; i < line.Length; i++)
                {
                    string text = line[i].Substring(line[i].IndexOf("["));
                    int a = line[i].IndexOf("[");
                   string  num = line[i].Substring(0, a);
                    id.Add(Convert.ToInt32(num));
                }
                UIObj.ParaList.ForEach(
   a =>
   {
       using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
       {
           _images.Add(Image.FromStream(fs));
       }
   }
   );
                _pointerwhite = _images[0];
                _pointerred = _images[1];
                pointer1 = new Pointer(new PointF(335F, 200F), new PointF(335F, 323F), 320F, 1600F);
                pointer2 = new Pointer(new PointF(335F, 200F), new PointF(335F, 323F), 320F, 1600F);
                pointer3 = new Pointer(new PointF(635F, 200F), new PointF(635F, 323F), 320F, 1000F);
                pointer4 = new Pointer(new PointF(635F, 200F), new PointF(635F, 323F), 320F, 1000F);
                _biappan1 = new biaopan(0F, 360F, new Rectangle(200, 65, 270, 270), p2);
                _biaopan2 = new biaopan(0F, 360F, new Rectangle(500, 65, 270, 270), p2);
                _kedu1 = new KeDu(135, 40, 8, new Point(335, 200), p2);
                _kedu2 = new KeDu(135, 40, 5, new Point(635, 200), p2);

                return true;

            }
           
            /// <summary>
            /// 生成指针对象
            /// </summary>
            /// <param name="g"></param>
            public void PointerPainter(Graphics g)
            {
                float shijivalue2=  FloatList[UIObj.InFloatList[8]];                           
                pointer2.drawPointer(g,_pointerred, ref shijivalue2 );
                float shijivalue1 = FloatList[UIObj.InFloatList[1]];
                pointer1.drawPointer(g,_pointerwhite, ref shijivalue1);
                float shijivalue4 = FloatList[UIObj.InFloatList[7]];
                pointer4.drawPointer(g,_pointerred, ref shijivalue4);
                float shijivalue3 = FloatList[UIObj.InFloatList[0]];
                pointer3.drawPointer(g,_pointerwhite, ref shijivalue3);
            }
            public override void paint(Graphics g)
            {
                if (BoolList[UIObj.InBoolList[7]])
                {

                    timer.Start();
                    g.DrawString(Convert.ToString(totaltime), Global.Font_Arial_11_B, Global.brush1, new Rectangle(240, 495, 200, 20), Global.SF_NC);
                    g.DrawString("紧急制动", Global.Font_Arial_11_B, Global.brush1, new Rectangle(20, 470, 120, 20), Global.SF_NC);
                    g.DrawString("需等待60s回重联位解锁", Global.Font_Arial_11_B, Global.brush1, new Rectangle(20, 495, 200, 20), Global.SF_NC);

                    if (totaltime == 0)
                    {

                        timer.Stop();
                        g.FillRectangle(new SolidBrush(Color.Black), 17, 460, 744, 54);
                    }


                }
                else
                {
                    timer.Stop();
                    totaltime = 60;
                }
                
                
                _biappan1.PaintBiaopan(g);
                _biaopan2.PaintBiaopan(g);
                drawRectangle(g);
                showTime(g);
                drawKeduShuzi(g);
                PointerPainter(g);
                g.DrawString("kPa", Global.font1, Global.brush1, new Rectangle(320, 310, 30, 20), Global.SF_CC);
                g.DrawString("kPa", Global.font1, Global.brush1, new Rectangle(620, 310, 30, 20), Global.SF_CC);
                g.SmoothingMode = SmoothingMode.HighQuality;
                _kedu1.drawKeDU(g);
                _kedu2.drawKeDU(g);

                for (int i = 0; i < id.Count; i++)
                {
                    if (!BoolList[UIObj.InBoolList[11]])
                    {
                        if (BoolList[id[i]])
                        {
                            append_postCmd(CmdType.ChangePage, 118, 0, 0);
                            break;
                        }
                    }
                }
                if (!BoolList[UIObj.InBoolList[10]])
                {
                    append_postCmd(CmdType.ChangePage, 103, 0, 0);
                }           
                base.paint(g);

            }
            /// <summary>
            /// 绘制文本框
            /// </summary>
            /// <param name="g"></param>
            public void drawRectangle(Graphics g)
            {
                //本机文本框
                g.DrawRectangle(p2, 50, 80, 100, 250);
                g.DrawLine(p2, new Point(90, 80), new Point(90, 80 + 250));
                g.DrawString("电 空 制 动", Global.font, Global.brush1, new Rectangle(60, 170, 40, 250), format);
                if (BoolList[UIObj.InBoolList[11]])// added by zhangjh 2016-04-12
                {
                    g.DrawString("**", Global.font1, Global.brush4, new Rectangle(90, 80, 60, 20), Global.SF_CC);
                }  
                else
                {
                    if (BoolList[UIObj.InBoolList[4]])
                    {
                        g.DrawString("本机", Global.font1, Global.brush4, new Rectangle(90, 80, 60, 20), Global.SF_CC);
                    }
                    if (BoolList[UIObj.InBoolList[12]])
                    {
                        g.DrawString("主控", Global.font1, Global.brush4, new Rectangle(90, 80, 60, 20), Global.SF_CC);
                    }
                    if (BoolList[UIObj.InBoolList[13]])
                    {
                        g.DrawString("从控", Global.font1, Global.brush4, new Rectangle(90, 80, 60, 20), Global.SF_CC);
                    }
                    if (BoolList[UIObj.InBoolList[5]])
                    {
                        g.DrawString("单机", Global.font1, Global.brush4, new Rectangle(90, 80, 60, 20), Global.SF_CC);
                    }
                    if (BoolList[UIObj.InBoolList[6]])
                    {
                        g.DrawString("补机", Global.font1, Global.brush4, new Rectangle(90, 80, 60, 20), Global.SF_CC);
                    }
                }
                g.DrawLine(p2, new Point(90, 100), new Point(90 + 60, 80 + 20));
                g.DrawLine(p2, new Point(90, 310), new Point(90 + 60, 310));
                string liuliang=FloatList[UIObj.InFloatList[4]].ToString ("F1");
                
                if (BoolList[UIObj.InBoolList[11]])// added by zhangjh 2016-04-12
                {
                    g.DrawString("*", Global.font1, Global.brush4, new Rectangle(90, 310, 60, 20), Global.SF_CC);
                }
                else
                {
                    g.DrawString(liuliang, Global.font1, Global.brush4, new Rectangle(90, 310, 60, 20), Global.SF_CC);
                }
                g.DrawRectangle(p2, 100, 120, 20, 165);
                g.DrawString("流量", Global.font1, Global.brush1, new Rectangle(90, 100, 60, 20), Global.SF_CC);
                g.DrawString("CMM", Global.font1, Global.brush1, new Rectangle(90, 290, 60, 20), Global.SF_CC);
                //绘制刻度
                for (int i = 0; i <= 12; i++)
                {
                    if (i % 2 == 0)
                    {
                        g.DrawLine(p2, new PointF(120F, 120 + i * 13.75F), new PointF(125F, 120 + i * 13.75F));
                        double kedu = 0.25 * i;
                        Font font3 = new Font("宋体", 7, FontStyle.Bold);
                        g.DrawString(kedu.ToString("f1"), font3, Global.brush1, new RectangleF(127F, 280 - i * 13.75F, 20F, 10F), Global.SF_CC);
                    }
                    else
                    {
                        g.DrawLine(p1, new PointF(120F, 120 + i * 13.75F), new PointF(123F, 120 + i * 13.75F));
                    }
                }
                //流量填充
                float a;
                if (FloatList[UIObj.InFloatList[4]] <= 3 && FloatList[UIObj.InFloatList[4]] >= 0)
                {
                    a = FloatList[UIObj.InFloatList[4]];
                }
                else if (FloatList[UIObj.InFloatList[4]] < 0)
                {
                    a = 0;
                }
                else
                {
                    a = 3;
                }
                g.FillRectangle(Global.brush5, new RectangleF(100, 285 - (165F / 3F) * a, 20, (165F / 3F) * a));
                //中1文本框
                g.DrawRectangle(p2, 25, 360, 160, 60);
                g.DrawLine(p2, new Point(25, 390), new Point(185, 390));
                g.DrawLine(p2, new Point(105, 360), new Point(105, 420));
                if (BoolList[UIObj.InBoolList[11]])// added by zhangjh 2016-04-12
                {
                    g.DrawString("*", Global.font, Global.brush5, new Rectangle(25, 360, 80, 30), Global.SF_CC);
                    g.DrawString("*", Global.font, Global.brush5, new Rectangle(105, 360, 80, 30), Global.SF_CC);
                    g.DrawString("*", Global.font, Global.brush5, new Rectangle(25, 390, 80, 30), Global.SF_CC);
                    g.DrawString("*", Global.font, Global.brush5, new Rectangle(105, 390, 80, 30), Global.SF_CC);
                }
                else
                {
                    if (BoolList[UIObj.InBoolList[0]])
                    {
                        g.DrawString("补风", Global.font, Global.brush5, new Rectangle(25, 360, 80, 30), Global.SF_CC);
                    }
                    if (BoolList[UIObj.InBoolList[1]])
                    {
                        g.DrawString("不补风", Global.font, Global.brush5, new Rectangle(25, 360, 80, 30), Global.SF_CC);
                    }
                    if (BoolList[UIObj.InBoolList[2]])
                    {
                        g.DrawString("空联投入", Global.font, Global.brush5, new Rectangle(105, 360, 80, 30), Global.SF_CC);
                    }
                    if (BoolList[UIObj.InBoolList[8]])
                    {
                        g.DrawString("空联切除", Global.font, Global.brush5, new Rectangle(105, 360, 80, 30), Global.SF_CC);
                    }
                    if (BoolList[UIObj.InBoolList[3]])
                    {
                        g.DrawString("ATP投入", Global.font, Global.brush5, new Rectangle(25, 390, 80, 30), Global.SF_CC);
                    }
                    if (BoolList[UIObj.InBoolList[9]])
                    {
                        g.DrawString("ATP切除", Global.font, Global.brush5, new Rectangle(25, 390, 80, 30), Global.SF_CC);
                    }
                    if (FloatList[UIObj.InFloatList[9]] > 0)
                    {
                        g.DrawString(Convert.ToString(FloatList[UIObj.InFloatList[9]]) + "kPa", Global.font1, Global.brush5, new Rectangle(105, 390, 80, 30), Global.SF_CC);
                    }
                }
                //中2文本框
                g.DrawRectangle(p2, 255, 360, 160, 90);
                g.DrawLine(p2, new Point(335, 360), new Point(335, 450));
                g.DrawLine(p2, new Point(255, 390), new Point(415, 390));
                g.DrawLine(p2, new Point(255, 420), new Point(415, 420));
                g.DrawString("总风缸", Global.font, Global.brush3, new Rectangle(255, 360, 80, 30), Global.SF_CC);
                int zf = Convert.ToInt32(FloatList[UIObj.InFloatList[8]]);             
                g.DrawString("均衡风缸", Global.font, Global.brush1, new Rectangle(335, 360, 80, 30), Global.SF_CC);
                int jh = Convert.ToInt32(FloatList[UIObj.InFloatList[1]]);              
                if (BoolList[UIObj.InBoolList[11]])// added by zhangjh 2016-04-12
                {
                    g.DrawString("*", Global.font1, Global.brush4, new Rectangle(255, 390, 80, 30), Global.SF_CC);
                    g.DrawString("*", Global.font1, Global.brush4, new Rectangle(335, 390, 80, 30), Global.SF_CC);
                    g.DrawString("---", Global.font1, Global.brush4, new Rectangle(255, 420, 80, 30), Global.SF_CC);
                    g.DrawString("---", Global.font1, Global.brush4, new Rectangle(335, 420, 80, 30), Global.SF_CC);

                }
                else
                {
                    g.DrawString(Convert.ToString(zf), Global.font1, Global.brush4, new Rectangle(255, 390, 80, 30), Global.SF_CC);
                    g.DrawString("-", Global.font1, Global.brush4, new Rectangle(255, 420, 80, 30), Global.SF_CC);
                    g.DrawString(Convert.ToString(jh), Global.font1, Global.brush4, new Rectangle(335, 390, 80, 30), Global.SF_CC);
                    g.DrawString("-", Global.font1, Global.brush4, new Rectangle(335, 420, 80, 30), Global.SF_CC);
                }
                //中3文本框
                g.DrawRectangle(p2, 525, 360, 240, 90);
                g.DrawLine(p2, new Point(685, 360), new Point(685, 450));
                g.DrawLine(p2, new Point(525, 390), new Point(765, 390));
                g.DrawLine(p2, new Point(525, 420), new Point(765, 420));
                g.DrawString("制动缸1  制动缸2", Global.font, Global.brush3, new Rectangle(525, 360, 160, 30), Global.SF_CC);
                g.DrawString("/", Global.font, Global.brush1, new Rectangle(525, 360, 160, 30), Global.SF_CC);
                int zda1 = Convert.ToInt32(FloatList[UIObj.InFloatList[2]]);
                int zda2 = Convert.ToInt32(FloatList[UIObj.InFloatList[3]]);
                int zdb1 = Convert.ToInt32(FloatList[UIObj.InFloatList[5]]);
                int zdb2 = Convert.ToInt32(FloatList[UIObj.InFloatList[6]]);
               

                g.DrawString("列车管", Global.font, Global.brush1, new Rectangle(685, 360, 80, 30), Global.SF_CC);
                int lcg = Convert.ToInt32(FloatList[UIObj.InFloatList[0]]);
                
                if (BoolList[UIObj.InBoolList[11]])// added by zhangjh 2016-04-12
                {
                    g.DrawString("*", Global.font1, Global.brush4, new Rectangle(525, 390, 80, 30), Global.SF_CC);
                    g.DrawString("*", Global.font1, Global.brush4, new Rectangle(605, 390, 80, 30), Global.SF_CC);
                    g.DrawString("*", Global.font1, Global.brush4, new Rectangle(525, 420, 80, 30), Global.SF_CC);
                    g.DrawString("*", Global.font1, Global.brush4, new Rectangle(605, 420, 80, 30), Global.SF_CC);
                    g.DrawString("*", Global.font1, Global.brush4, new Rectangle(685, 390, 80, 30), Global.SF_CC);
                    g.DrawString("---", Global.font1, Global.brush4, new Rectangle(685, 420, 80, 30), Global.SF_CC);
                }
                else
                {
                    g.DrawString(Convert.ToString(zda1), Global.font1, Global.brush4, new Rectangle(525, 390, 80, 30), Global.SF_CC);
                    g.DrawString(Convert.ToString(zda2), Global.font1, Global.brush4, new Rectangle(605, 390, 80, 30), Global.SF_CC);
                    g.DrawString(Convert.ToString(zdb1), Global.font1, Global.brush4, new Rectangle(525, 420, 80, 30), Global.SF_CC);
                    g.DrawString(Convert.ToString(zdb2), Global.font1, Global.brush4, new Rectangle(605, 420, 80, 30), Global.SF_CC);
                    g.DrawString(Convert.ToString(lcg), Global.font1, Global.brush4, new Rectangle(685, 390, 80, 30), Global.SF_CC);
                    g.DrawString("-", Global.font1, Global.brush4, new Rectangle(685, 420, 80, 30), Global.SF_CC);
                }

                //末尾文本框
                g.DrawRectangle(p2, 15, 458, 748, 58);
                if (BoolList[UIObj.InBoolList[11]])// added by zhangjh 2016-04-12
                {
                    g.DrawString("通信中断", Global.font, Global.brush3, new Rectangle(390, 458, 374, 58), Global.SF_CC);
                }

            }
            /// <summary>
            /// 绘制时间界面
            /// </summary>
            public void showTime(Graphics g)
            {
                g.DrawString("机车 1001", Global.font1, Global.brush1, new Rectangle(710, 25, 70, 15), Global.SF_CC);
                string NowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                g.DrawString(NowTime, Global.font1, Global.brush1, new Rectangle(705, 20, 80, 80), Global.SF_CC);

            }
            /// <summary>
            /// 绘制表盘数字
            /// </summary>
            /// <param name="g"></param>
            public void drawKeduShuzi(Graphics g)
            {
               
                Font font3 = new Font("宋体", 8, FontStyle.Bold);
                double PI = Math.PI;
                for (int i = 0; i <= 6; i++)
                {
                    g.TranslateTransform(335, 200);
                    g.TranslateTransform((float)Convert.ToDouble(100 * Math.Sin(PI * (20 + 40 * i) / 180)), (float)Convert.ToDouble(100 * Math.Cos(PI * (20 + 40 * i) / 180)));
                    string KEDU = Convert.ToString(1600 - i * 200);
                    g.DrawString(KEDU, font3, Global.brush1, new Rectangle(-15, -15, 30, 20), Global.SF_CC);
                    g.ResetTransform();
                }
                g.TranslateTransform(335, 200);
                g.TranslateTransform((float)Convert.ToDouble(100 * Math.Sin(PI * (-20) / 180)), (float)Convert.ToDouble(100 * Math.Cos(PI * (-20) / 180)));
                g.DrawString("0", font3, Global.brush1, new Rectangle(-15, -15, 30, 20), Global.SF_CC);
                g.ResetTransform();
                for (int i = 0; i <= 4; i++)
                {
                    g.TranslateTransform(635, 200);
                    g.TranslateTransform((float)Convert.ToDouble(100 * Math.Sin(PI * (20 + 64 * i) / 180)), (float)Convert.ToDouble(100 * Math.Cos(PI * (20 + 64 * i) / 180)));
                    string KEDU = Convert.ToString(1000 - i * 200);
                    g.DrawString(KEDU, font3, Global.brush1, new Rectangle(-15, -15, 30, 20), Global.SF_CC);
                    g.ResetTransform();
                }
                g.TranslateTransform(635, 200);
                g.TranslateTransform((float)Convert.ToDouble(100 * Math.Sin(PI * (-20) / 180)), (float)Convert.ToDouble(100 * Math.Cos(PI * (-20) / 180)));
                g.DrawString("0", font3, Global.brush1, new Rectangle(-15, -15, 30, 20), Global.SF_CC);
                g.ResetTransform();
            }
            public void timer_Elapsed(object sender, EventArgs e)
            {

                --totaltime;

            }     
        }
    }
}
