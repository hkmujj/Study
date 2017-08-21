using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.DataType;
using System.Threading;

using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace HXD3
{
    //牵引制动画面
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class QianYinHuaMian : baseClass
    {
        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "牵引制动画面";
        }

        public override int getInBoolParaCount()
        {
            return 0;
        }

        public override int getInFloatParaCount()
        {
            return 9;
        }

        public override int getUIParaCount()
        {
            return 0;
        }

        public override bool initValue(string nParaString, ref int nErrorObjectIndex)
        {
            return base.initValue(nParaString, ref nErrorObjectIndex);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        public override string GetTypeName()
        {
            //1
            return "QianYinHuaMian";
        }

        public override bool initObject(string nPara)
        {
            //2
            return base.initObject(nPara);
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            //3
            nErrorObjectIndex = -1;

            if (UIObj.ParaList.Count > 0)
            {
                for (int i = 0; i < UIObj.ParaList.Count; i++)
                {
                    Img[i] = Image.FromFile(_recPath + "\\" + UIObj.ParaList[i]);
                }
            }
            else
            {
                nErrorObjectIndex = 0;
                return false;
            }
            InitData();
            return true;
        }

        public override bool canSetStringList(int nPara)
        {
            //4
            return base.canSetStringList(nPara);
        }

        public override void addStringByLine(int nIndex, string cStr)
        {
            //4-1
            base.addStringByLine(nIndex, cStr);
        }

        #endregion#

        #region ::::::::::::::::::::::::event override:::::::::::::::::::::::::::::#

        public override void paint(System.Drawing.Graphics dcGs)
        {
            GetValue();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public override bool mouseUp(System.Drawing.Point nPoint)
        {
            return base.mouseUp(nPoint);
        }

        public override bool mouseDown(System.Drawing.Point nPoint)
        {
            return base.mouseDown(nPoint);
        }
        #endregion#

        #region ::::::::::::::::::::::::  ex funes ::::::::::::::::::::::::::::::::#
        /// <summary>
        /// 更新数据
        /// </summary>
        private void GetValue()
        {
            for (int i = 0; i < this.UIObj.InBoolList.Count; i++)
            {
                bValue[i] = BoolList[this.UIObj.InBoolList[i]];
                oldbValue[i] = BoolOldList[this.UIObj.InBoolList[i]];
            }
            for (int i = 0; i < this.UIObj.InFloatList.Count; i++)
            {
                fValue[i] = FloatList[this.UIObj.InFloatList[i]];
            }
            for (int i = 0; i < this.UIObj.OutBoolList.Count; i++)
            {
                setBValue[i] = BoolList[this.UIObj.OutBoolList[i]];
                setBValueNumb[i] = this.UIObj.OutBoolList[i];
            }

        }

        /// <summary>
        /// 画刻度
        /// </summary>
        /// <param name="e"></param>
        private void DrawGraduation(Graphics e, int raiseNumb, int MaxNumb, float width, bool direction)
        {
            if (direction)
            {
                for (int i = 0; i < MaxNumb; i++)
                {
                    if (i % 10 == 0 && i > 0)
                    {
                        e.DrawLine(Common.whitePen2, new PointF(pDrawPoint[0].X + raiseNumb - 10, pDrawPoint[0].Y + (i + 1) * width),
                            new PointF(pDrawPoint[0].X + raiseNumb, pDrawPoint[0].Y + (i + 1) * width));
                    }
                    else
                    {
                        e.DrawLine(Common.whitePen1, new PointF(pDrawPoint[0].X + raiseNumb - 5, pDrawPoint[0].Y + (i + 1) * width),
                            new PointF(pDrawPoint[0].X + raiseNumb, pDrawPoint[0].Y + (i + 1) * width));
                    }
                }
            }
            else
            {                
                for (int i = 0; i < MaxNumb; i++)
                {
                    if (i % 10 == 0 && i > 0)
                    {
                        e.DrawLine(Common.whitePen2, new PointF(pDrawPoint[0].X + raiseNumb + 10, pDrawPoint[0].Y + (i + 1) * width),
                            new PointF(pDrawPoint[0].X + raiseNumb, pDrawPoint[0].Y + (i + 1) * width));
                    }
                    else
                    {
                        e.DrawLine(Common.whitePen1, new PointF(pDrawPoint[0].X + raiseNumb + 5, pDrawPoint[0].Y + (i + 1) * width),
                            new PointF(pDrawPoint[0].X + raiseNumb, pDrawPoint[0].Y + (i + 1) * width));
                    }
                }
            }

        }

        /// <summary>
        /// 长条框
        /// </summary>
        /// <param name="e"></param>
        private void DrawRect(Graphics e)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    e.DrawLine(Common.whitePen1, new PointF(pDrawPoint[0].X + i * 100 + j * 25, pDrawPoint[0].Y),
                        new PointF(pDrawPoint[0].X + i * 100 + j * 25, pDrawPoint[0].Y + 250));
                }
                e.DrawLine(Common.whitePen2, new PointF(pDrawPoint[0].X - 15 + i * 100, pDrawPoint[0].Y),
                    new PointF(pDrawPoint[0].X + i * 100, pDrawPoint[0].Y));
                e.DrawLine(Common.whitePen2, new PointF(pDrawPoint[0].X - 15 + i * 100, pDrawPoint[0].Y + 250),
                    new PointF(pDrawPoint[0].X + 25 + i * 100, pDrawPoint[0].Y + 250));
            }
            DrawGraduation(e, 0, 39, 6.25f, true);
            DrawGraduation(e, 100, 49, 5f, true);
            DrawGraduation(e, 200, 19, 12.5f, true);

            for (int i = 0; i < 7; i++)
            {
                e.DrawLine(Common.whitePen1, new PointF(pDrawPoint[0].X + 280 + i * 25, pDrawPoint[0].Y),
                    new PointF(pDrawPoint[0].X + 280 + i * 25, pDrawPoint[0].Y + 250));
            }
            for (int i = 0; i < 2; i++ )
            {
                e.DrawLine(Common.whitePen2, new PointF(pDrawPoint[0].X + 280 - 15 + i * 165, pDrawPoint[0].Y),
                    new PointF(pDrawPoint[0].X + 280 + i * 165, pDrawPoint[0].Y));
            }
            e.DrawLine(Common.whitePen2, new PointF(pDrawPoint[0].X + 280 - 15, pDrawPoint[0].Y + 250),
                new PointF(pDrawPoint[0].X + 280 + 165, pDrawPoint[0].Y + 250));

            DrawGraduation(e, 280, 19, 12.5f, true);
            DrawGraduation(e, 430, 19, 12.5f, false);

            e.DrawString("40", Common.Txt12FontR, Common.whiteBrush, 
                new PointF(pDrawPoint[0].X - 35, pDrawPoint[0].Y - 10));
            e.DrawString("500", Common.Txt12FontR, Common.whiteBrush, 
                new PointF(pDrawPoint[0].X + 100 - 45, pDrawPoint[0].Y - 10));
            e.DrawString("200", Common.Txt12FontR, Common.whiteBrush, 
                new PointF(pDrawPoint[0].X + 200 - 45, pDrawPoint[0].Y - 10));
            e.DrawString("200", Common.Txt12FontR, Common.whiteBrush, 
                new PointF(pDrawPoint[0].X + 280 - 45, pDrawPoint[0].Y - 10));

            for (int i = 0; i < 4; i++)
            {
                e.DrawString(str1[i], Common.Txt12FontB, Common.whiteBrush, 
                    new RectangleF(pDrawPoint[10 + i], new Size(80, 50)), Common.drawFormat);
            }
        }

        /// <summary>
        /// 进度条
        /// </summary>
        /// <param name="e"></param>
        private void DrawGrogressBar(Graphics e)
        {
            if (fValue[0] >= 40) fValue[0] = 40;
            e.DrawString(fValue[0].ToString("0.0"), Common.Txt12FontR, Common.greenBrush, 
                new RectangleF(pDrawPoint[14], fSize), Common.drawFormat);

            if (fValue[1] >= 500) fValue[1] = 500;

            for (int i = 2; i < 9; i++)
            {
                if (fValue[i] >= 200) fValue[i] = 200;
            }
            for (int i = 0; i < 9; i++)
            {
                grogressBar[i].DrawRectangle(e, ref fValue[i], 3);
            }

            for (int i = 1; i < 3; i++)
            {
                e.DrawString(Convert.ToInt32(fValue[i]).ToString(), Common.Txt12FontR, Common.greenBrush, 
                    new RectangleF(pDrawPoint[14 + i], fSize), Common.drawFormat);
            }

            e.DrawString(Convert.ToInt32(fValue[3]).ToString(), Common.Txt12FontR, Common.yellowBrush, 
                new RectangleF(pDrawPoint[17], fSize), Common.drawFormat);
            e.DrawString(Convert.ToInt32(fValue[4]).ToString(), Common.Txt12FontR, Common.marineBlueBrush, 
                new RectangleF(pDrawPoint[18], fSize), Common.drawFormat);
            e.DrawString(Convert.ToInt32(fValue[5]).ToString(), Common.Txt12FontR, Common.pinkBrush, 
                new RectangleF(pDrawPoint[19], fSize), Common.drawFormat);
            e.DrawString(Convert.ToInt32(fValue[6]).ToString(), Common.Txt12FontR, Common.yellowBrush, 
                new RectangleF(pDrawPoint[20], fSize), Common.drawFormat);
            e.DrawString(Convert.ToInt32(fValue[7]).ToString(), Common.Txt12FontR, Common.marineBlueBrush, 
                new RectangleF(pDrawPoint[21], fSize), Common.drawFormat);
            e.DrawString(Convert.ToInt32(fValue[8]).ToString(), Common.Txt12FontR, Common.greenBrush, 
                new RectangleF(pDrawPoint[22], fSize), Common.drawFormat);
        }

        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            DrawRect(e);

            DrawGrogressBar(e);

            //电压电流
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(str2[i], Common.Txt12FontR, Common.whiteBrush, 
                    new RectangleF(pDrawPoint[23 + i], new SizeF(150, 20)), Common.leftFormat);
                e.DrawString(Convert.ToInt32(fValue[i + 9]).ToString() + "V", Common.Txt12FontR, Common.whiteBrush,
                    new RectangleF(pDrawPoint[23 + i], new SizeF(150, 20)), Common.rightFormat);
                e.DrawString(str2[2], Common.Txt12FontR, Common.whiteBrush,
                    new RectangleF(pDrawPoint[25 + i], new SizeF(120, 20)), Common.leftFormat);
                e.DrawString(Convert.ToInt32(fValue[i + 11]).ToString() + "A", Common.Txt12FontR, Common.whiteBrush,
                    new RectangleF(pDrawPoint[25 + i], new SizeF(120, 20)), Common.rightFormat);
            }
        }
        #endregion#

        #region:::::::::::::::所有坐标点的初始化、表盘和进度条的初始化:::::::::::::::#
        /// <summary>
        /// 初始化坐标点
        /// </summary>
        private void InitData()
        {
            Common.drawFormat.Alignment = (StringAlignment)1;
            Common.drawFormat.LineAlignment = (StringAlignment)1;

            Common.rightFormat.Alignment = (StringAlignment)2;
            Common.rightFormat.LineAlignment = (StringAlignment)1;

            Common.leftFormat.Alignment = (StringAlignment)0;
            Common.leftFormat.LineAlignment = (StringAlignment)1;


            #region ::::::::::::::::::::::::坐标点:::::::::::::::::::::::::::::#
            pDrawPoint[0] = new PointF(35f, 235f);

            pDrawPoint[1] = new PointF(36f, 485f);
            pDrawPoint[2] = new PointF(136f, 485f);
            pDrawPoint[3] = new PointF(236f, 485f);
            pDrawPoint[4] = new PointF(316f, 485f);
            pDrawPoint[5] = new PointF(341f, 485f);
            pDrawPoint[6] = new PointF(366f, 485f);
            pDrawPoint[7] = new PointF(391f, 485f);
            pDrawPoint[8] = new PointF(416f, 485f);
            pDrawPoint[9] = new PointF(441f, 485f);

            pDrawPoint[10] = new PointF(0, 185f);
            pDrawPoint[11] = new PointF(100f, 185f);
            pDrawPoint[12] = new PointF(200f, 185f);
            pDrawPoint[13] = new PointF(350f, 185f);

            pDrawPoint[14] = new PointF(20f, 480f);
            pDrawPoint[15] = new PointF(120f, 480f);
            pDrawPoint[16] = new PointF(220f, 480f);
            pDrawPoint[17] = new PointF(300f, 480f);
            pDrawPoint[18] = new PointF(330, 500f);
            pDrawPoint[19] = new PointF(350, 480f);
            pDrawPoint[20] = new PointF(380, 500f);
            pDrawPoint[21] = new PointF(400, 480f);
            pDrawPoint[22] = new PointF(430, 500f);

            pDrawPoint[23] = new PointF(0, 510f);
            pDrawPoint[24] = new PointF(0, 530f);
            pDrawPoint[25] = new PointF(180f, 510f);
            pDrawPoint[26] = new PointF(180f, 530f);

            #endregion#
            grogressBar[0] = new NeedChangeLength(pDrawPoint[1], 24, 2f, 250f / 40, Common.greenBrush);
            grogressBar[1] = new NeedChangeLength(pDrawPoint[2], 24, 20f, 250f / 500, Common.greenBrush);
            grogressBar[2] = new NeedChangeLength(pDrawPoint[3], 24, 10f, 250f / 200, Common.greenBrush);

            grogressBar[3] = new NeedChangeLength(pDrawPoint[4], 24, 10f, 250f / 200, Common.yellowBrush);
            grogressBar[4] = new NeedChangeLength(pDrawPoint[5], 24, 10f, 250f / 200, Common.marineBlueBrush);
            grogressBar[5] = new NeedChangeLength(pDrawPoint[6], 24, 10f, 250f / 200, Common.pinkBrush);
            grogressBar[6] = new NeedChangeLength(pDrawPoint[7], 24, 10f, 250f / 200, Common.yellowBrush);
            grogressBar[7] = new NeedChangeLength(pDrawPoint[8], 24, 10f, 250f / 200, Common.marineBlueBrush);
            grogressBar[8] = new NeedChangeLength(pDrawPoint[9], 24, 10f, 250f / 200, Common.greenBrush);
        }
        #endregion#

        #region :::::::::::::::::::::::: value init :::::::::::::::::::::::::::::::#


        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::::#
        /// <summary>
        /// 当前周期接收数字量
        /// </summary>
        public bool[] bValue = new bool[120];

        /// <summary>
        /// 前一个周期接收的数字量
        /// </summary>
        public bool[] oldbValue = new bool[120];

        /// <summary>
        /// 发送的数字量
        /// </summary>
        public bool[] setBValue = new bool[10];

        /// <summary>
        /// 发送的数字量在boollist中的序号
        /// </summary>
        public int[] setBValueNumb = new int[10];

        /// <summary>
        /// 接收模拟量
        /// </summary>
        public float[] fValue = new float[20];

        /// <summary>
        /// 坐标集
        /// </summary>
        public PointF[] pDrawPoint = new PointF[50];

        /// <summary>
        /// 图片集
        /// </summary>
        public static Image[] Img = new Image[30];
        #endregion#

        /// <summary>
        /// 进度条
        /// </summary>
        public NeedChangeLength[] grogressBar = new NeedChangeLength[9];

        public string[] str1 = new string[4] { "原边电压\n(kv)", "原边电流\n(A)", "控制电压\n(V)", "牵引/制动\n(kN)01" };
        public string[] str2 = new string[3] { "LG1 电压:", "LG2 电压:", "电流:" };

        public SizeF fSize = new SizeF(50, 30);



        #endregion#
    }
}
