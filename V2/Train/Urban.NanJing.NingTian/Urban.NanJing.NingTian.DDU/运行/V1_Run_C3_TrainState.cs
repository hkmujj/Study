#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-21
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图1-运行-No.3-机车状态
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using Urban.NanJing.NingTian.DDU.Common;

namespace Urban.NanJing.NingTian.DDU.运行
{
    /// <summary>
    /// 功能描述：视图1-运行-No.3-机车状态
    /// 创建人：唐林
    /// 创建时间：2014-7-21
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class V1_Run_C3_TrainState : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Images = new List<Image>();//图片资源
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "运行试图-状态栏";
        }


        /// <summary>
        /// 初始化函数：导入图片
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Images.Add(Image.FromStream(fs));
                }
            });

            return true;
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="g"></param>
        public override void paint(Graphics g)
        {
            paint_TrainState(g);
            paint_Frame(g);
            paint_DoorState(g);
            paint_DriverDoorState(g);
            paint_TrainDirection(g);
            paint_Bow(g);
            paint_AirPressure(g);
            panit_Mushroom(g);

            base.paint(g);
        }

        /// <summary>
        /// 绘制线框、机车2D模型、车厢名称
        /// </summary>
        /// <param name="g"></param>
        private void paint_Frame(Graphics g)
        {
            g.DrawLine(new Pen(Color.Black, 2), new Point(0, 108), new Point(800, 108));

            //g.DrawImage(_resource_Images[0], new Point(120, 192 - 27));
            string[] strs = new string[] { "A1", "B1", "B2", "A2" };
            for (int i = 0; i < 4; i++)
            {
                g.DrawString(
                    strs[i],
                    new Font("宋体", 12),
                    new SolidBrush(Color.Black),
                    new RectangleF(139 + 102 * i, 192 - 27 + 34, 102, 25),
                    FontInfo.SF_CC
                    );
            }
        }

        private void paint_TrainState(Graphics g)
        {
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[8] + i])
                {
                    g.DrawImage(_resource_Images[21 + i], new Point(120, 192 - 27 + 34));
                    return;
                }
            }
            g.DrawImage(_resource_Images[0], new Point(120, 192 - 27 + 34));

            Brush[] brushes = { Brushes.White, Brushes.Yellow };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (BoolList[UIObj.InBoolList[11] + i * 2 + j])
                    {
                        g.FillRectangle(brushes[j], new Rectangle(141 + 103 * i, 201, i == 3 ? 98 : 100, 22));
                    }
                }
            }
        }

        /// <summary>
        /// 客室门状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_DoorState(Graphics g)
        {
            Brush[] doorStates = { Brushes.Black, Brushes.Yellow, Brushes.White, Brushes.Red, Brushes.Gray };
            //A1
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int state = 0; state < 5; state++)//5种客室门状态
                    {
                        if (BoolList[UIObj.InBoolList[4] + state + j * 7 + i * 28])
                        {
                            g.FillRectangle(
                                doorStates[state],
                                new Rectangle(142 + j * 25, 181 + 36 * i - 27 + 34, 23, 10)
                                );
                            if (state == 4)
                                g.DrawString(
                                    "?",
                                    new Font("宋体", 9),
                                    Brushes.Black,
                                    new Rectangle(142 + j * 25, 181 + 36 * i - 27 + 34, 23, 10),
                                    FontInfo.SF_CC
                                    );
                        }
                    }
                    for (int state = 0; state < 2; state++)//2种门故障状态
                    {
                        if (BoolList[UIObj.InBoolList[4] + state + 5 + j * 7 + i * 28])
                            g.DrawImage(
                                _resource_Images[1 + i + state * 2],
                                new Rectangle(155 + j * 25, 165 + 62 * i - 27 + 34, 7, 16)
                                );
                    }
                }
            }

            //B1
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int state = 0; state < 5; state++)//5种客室门状态
                    {
                        if (BoolList[UIObj.InBoolList[4] + state + j * 7 + i * 28 + 56 * 1])
                        {
                            g.FillRectangle(
                                doorStates[state],
                                new Rectangle(245 + j * 25, 181 + 36 * i - 27 + 34, 23, 10)
                                );
                            if (state == 4)
                                g.DrawString(
                                    "?",
                                    new Font("宋体", 9),
                                    Brushes.Black,
                                    new Rectangle(245 + j * 25, 181 + 36 * i - 27 + 34, 23, 10),
                                    FontInfo.SF_CC
                                    );
                        }
                    }
                    for (int state = 0; state < 2; state++)//2种门故障状态
                    {
                        if (BoolList[UIObj.InBoolList[4] + state + 5 + j * 7 + i * 28 + 56 * 1])
                            g.DrawImage(
                                _resource_Images[1 + i + state * 2],
                                new Rectangle(245 + 13 + j * 25, 165 + 62 * i - 27 + 34, 7, 16)
                                );
                    }
                }
            }

            //B2
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int state = 0; state < 5; state++)//5种客室门状态
                    {
                        if (BoolList[UIObj.InBoolList[4] + state + j * 7 + i * 28 + 56 * 2])
                        {
                            g.FillRectangle(
                                doorStates[state],
                                new Rectangle(348 + j * 25, 181 + 36 * i - 27 + 34, 23, 10)
                                );
                            if (state == 4)
                                g.DrawString(
                                    "?",
                                    new Font("宋体", 9),
                                    Brushes.Black,
                                    new Rectangle(348 + j * 25, 181 + 36 * i - 27 + 34, 23, 10),
                                    FontInfo.SF_CC
                                    );
                        }
                    }
                    for (int state = 0; state < 2; state++)//2种门故障状态
                    {
                        if (BoolList[UIObj.InBoolList[4] + state + 5 + j * 7 + i * 28 + 56 * 2])
                            g.DrawImage(
                                _resource_Images[1 + i + state * 2],
                                new Rectangle(348 + 13 + j * 25, 165 + 62 * i - 27 + 34, 7, 16)
                                );
                    }
                }
            }

            //A2
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int state = 0; state < 5; state++)//5种客室门状态
                    {
                        if (BoolList[UIObj.InBoolList[4] + state + j * 7 + i * 28 + 56 * 3])
                        {
                            g.FillRectangle(
                                doorStates[state],
                                new Rectangle(450 + j * 25, 181 + 36 * i - 27 + 34, 23, 10)
                                );
                            if (state == 4)
                                g.DrawString(
                                    "?",
                                    new Font("宋体", 9),
                                    Brushes.Black,
                                    new Rectangle(450 + j * 25, 181 + 36 * i - 27 + 34, 23, 10),
                                    FontInfo.SF_CC
                                    );
                        }
                    }
                    for (int state = 0; state < 2; state++)//2种门故障状态
                    {
                        if (BoolList[UIObj.InBoolList[4] + state + 5 + j * 7 + i * 28 + 56 * 3])
                            g.DrawImage(
                                _resource_Images[1 + i + state * 2],
                                new Rectangle(450 + 13 + j * 25, 165 + 62 * i - 27 + 34, 7, 16)
                                );
                    }
                }
            }
        }

        /// <summary>
        /// 司机室门状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_DriverDoorState(Graphics g)
        {
            Brush[] brushes = new Brush[] { Brushes.Black, Brushes.Yellow, Brushes.Red, Brushes.Gray };
            Brush[] brushes1 = new Brush[] { Brushes.Red, Brushes.Black};
            //A1
            for (int j = 0; j < 4; j++)
            {
                if (BoolList[UIObj.InBoolList[5] + j])
                {
                    g.FillRectangle(brushes[j], new Rectangle(124, 200 - 27 + 34, 5, 10));
                }
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[7] + i * 3 + j])
                    {
                        g.FillRectangle(brushes[j], new Rectangle(129, 195 + 15 * i - 27 + 34, 10, 5));
                    }
                }
            }

            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[12] + i])
                {
                    g.FillRectangle(brushes1[i], new Rectangle(139, 200 - 27 + 34, 5, 10));
                }
            }

            //A2
            for (int j = 0; j < 4; j++)
            {
                if (BoolList[UIObj.InBoolList[5] + 4 + j])
                {
                    g.FillRectangle(brushes[j], new Rectangle(543 + 17, 200 - 27 + 34, 5, 10));
                }
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[7] + (i + 2) * 3 + j])
                    {
                        g.FillRectangle(brushes[j], new Rectangle(549, 195 + 15 * i - 27 + 34, 10, 5));
                    }
                }
            }

            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[13] + i])
                {
                    g.FillRectangle(brushes1[i], new Rectangle(544, 200 - 27 + 34, 5, 10));
                }
            }
        }

        /// <summary>
        /// 绘制车方向20 21
        /// </summary>
        /// <param name="g"></param>
        private void paint_TrainDirection(Graphics g)
        {
            for (int i = 0; i < 2; i++)//2车厢
            {
                for (int state = 0; state < 2; state++)//状态（向前/向后）
                {
                    if (BoolList[UIObj.InBoolList[6] + state + i * 2])
                    {
                        g.DrawImage(_resource_Images[19 + state], new Point(96 + 480 * i, 191 - 27 + 34));
                    }
                }
            }
        }

        /// <summary>
        /// 弓状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_Bow(Graphics g)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (BoolList[UIObj.InBoolList[2 + i] + j])
                        g.DrawImage(_resource_Images[5 + i * 7 + j], new Point(282 + 101 * i - 6, 107 + 34 - 8));
                }
            }
        }


        /// <summary>
        /// 空压机状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_AirPressure(Graphics g)
        {
            Brush[] brushs = { Brushs.RedBrush, Brushs.GreenBrush, Brushs.BlackBrush };

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[9] + j + i * 3])
                    {
                        g.FillEllipse(new SolidBrush(Color.Black), new Rectangle(174 + 316 * i, 107 + 34 + 7, 26, 26));
                        g.FillEllipse(new SolidBrush(Color.FromArgb(221, 221, 221)), new Rectangle(175 + 316 * i, 107 + 34 + 1 + 7, 24, 24));
                        g.FillEllipse(brushs[j], new Rectangle(177 + 316 * i, 107 + 34 + 2 + 7, 20, 20));
                        g.DrawString("M", new Font("宋体", 13), Brushs.WhiteBrush, new Rectangle(174 + 316 * i, 107 + 34 + 7, 26, 26), FontInfo.SF_CC);
                    }
                }
            }
        }

        private void panit_Mushroom(Graphics g)
        {
            if (BoolList[UIObj.InBoolList[10]])
            {
                g.FillEllipse(
                    new SolidBrush(Color.FromArgb(242, 77, 75)),
                    new RectangleF(458 - 5F - 10 - 216, 153 - 10, 16, 16)
                    );
                g.FillPolygon(
                    Brushes.Black,
                    new[] { new PointF(451 - 216, 169 - 10), new PointF(451 - 5F - 216, 169 + 10), new PointF(451 + 5F - 216, 169 + 10) }
                    );
            }
        }

        #endregion
    }
}
