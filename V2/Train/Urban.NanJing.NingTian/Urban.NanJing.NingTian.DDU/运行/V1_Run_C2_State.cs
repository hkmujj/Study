#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-21
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图1-运行-No.3-状态
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using Urban.NanJing.NingTian.DDU.Common;

namespace Urban.NanJing.NingTian.DDU.运行
{
    /// <summary>
    /// 功能描述：视图1-运行-No.2-状态
    /// 创建人：唐林
    /// 创建时间：2014-7-21
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1_Run_C2_State : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Images = new List<Image>();//图片资源
        private SolidBrush[] _airBrakingStates;//空气制动状态
        private List<Label> _labels_Power = new List<Label>();//电源状态文本框列表
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
        /// 初始化函数：导入图片、初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            //导入图片资源
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Images.Add(Image.FromStream(fs));
                }
            });

            //空气制动状态（白、黄、绿、红）
            _airBrakingStates = new SolidBrush[4] { 
                new SolidBrush(Color.FromArgb(255, 255, 255)), 
                new SolidBrush(Color.FromArgb(255, 255, 0)),
                new SolidBrush(Color.FromArgb(0, 255, 0)),
                new SolidBrush(Color.FromArgb(255, 0, 0)) 
            };

            string[] strs = new string[] { "AC", "DC", "AC", "DC" };
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Label label = new Label(
                        strs[j + i * 2],
                        new RectangleF(148 + j * 42 + i * 311, 254, 36, 22),
                        new LabelStyle() { BorderPen = new Pen(new SolidBrush(Color.White)), FontStyle = FontStyles.FS_Song_10_CC_W },
                        j + i * 2
                        );
                    _labels_Power.Add(label);
                }
            }
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
            paint_Frame(g);

            paint_Power(g);
            paint_Breaking(g);
            paint_Drag(g);
            paint_PowerBreaking(g);
            paint_AirCondition(g);
            paint_StopBreaking(g);
            paint_BreakingPressure(g);
            paint_Passenger(g);
            //this.paint_AirPressure(g);

            base.paint(g);
        }

        /// <summary>
        /// 运行界面-状态栏线框和不变状态的绘制
        /// </summary>
        /// <param name="g"></param>
        private void paint_Frame(Graphics g)
        {
            string[] strs = { "电源", "制动", "牵引", "电制动", "空调", "停放制动", "制动缸压力(KPa)", "乘客信息" };
            for (int i = 0; i < 8; i++)
            {
                g.DrawRectangle(new Pen(new SolidBrush(Color.Black), 2), new Rectangle(7, 249 + 34 * i - 27 + 34, 651, 34));
                g.DrawString(strs[i], new Font("黑体", 12), Brushs.BlackBrush, new Rectangle(9, 251 + 34 * i - 27 + 34, 651, 34), FontInfo.SF_LC);
            }
        }

        /// <summary>
        /// 绘制电源状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_Power(Graphics g)
        {
            Brush[] brushs = new Brush[]{
                  Brushs.BlackBrush,
                  Brushs.YellowBrush,
                  Brushs.GrayBrush
                  };
            string[] strs = new string[] { "AC", "DC", "AC", "DC" };
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (BoolList[UIObj.InBoolList[0] + j + i * 6])
                    {
                        g.DrawRectangle(new Pen(new SolidBrush(Color.White)), new Rectangle(148 + (j / 3) * 42 + i * 311, 254 - 27 + 34, 36, 22));
                        g.FillRectangle(brushs[j % 3], new Rectangle(148 + (j / 3) * 42 + 1 + i * 311, 254 + 1 - 27 + 34, 36 - 1, 22 - 1));
                        g.DrawString(
                            strs[j / 3],
                            new Font("宋体", 11),
                            (j % 3) == 0 ? Brushs.WhiteBrush : Brushs.BlackBrush,
                            new Rectangle(148 + (j / 3) * 42 + i * 311, 254 - 27 + 34, 36, 22),
                            FontInfo.SF_CC
                            );
                    }
                }
            }

            for (int i = 0; i < 2; i++)
            {
                g.DrawString("-", new Font("黑体", 20, FontStyle.Bold), Brushs.BlackBrush, new Rectangle(241 + 102 * i, 249 - 27 + 34, 102, 34), FontInfo.SF_CC);
            }
        }

        /// <summary>
        /// 绘制制动状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_Breaking(Graphics g)
        {
            Brush[] brushs = new Brush[]{
                  Brushs.BlackBrush,
                  Brushs.YellowBrush,
                  new SolidBrush(Color.FromArgb(0,255,0)),
                  Brushs.RedBrush,
                  Brushs.RedBrush,
                  new SolidBrush(Color.FromArgb(192,192,192))
                  };
            string[] strs = new string[] { "OK", "FAIL", "OK", "ISOL", "车隔离", "?" };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (BoolList[UIObj.InBoolList[1] + j + i * 12])
                    {
                        g.DrawRectangle(new Pen(new SolidBrush(Color.White)), new Rectangle(148 + (j / 6) * 42 + i * 102, 254 + 34 - 27 + 34, 36, 22));
                        g.FillRectangle(brushs[j % 6], new Rectangle(148 + (j / 6) * 42 + 1 + i * 102, 254 + 34 + 1 - 27 + 34, 36 - 1, 22 - 1));
                        g.DrawString(
                            strs[j % 6],
                            (j % 6 == 4) ? new Font("宋体", 9) : new Font("宋体", 10),
                            (j % 6) == 0 ? Brushs.WhiteBrush : Brushs.BlackBrush,
                            new Rectangle(140 + (j / 6) * 42 + i * 102, 254 + 34 + 2 - 27 + 34, 36 + 16, 22), FontInfo.SF_CC
                            );
                    }
                }
            }
        }

        /// <summary>
        /// 绘制牵引状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_Drag(Graphics g)
        {
            Brush[] brushs = new Brush[]{
                  Brushs.BlackBrush,
                  Brushs.YellowBrush,
                  new SolidBrush(Color.FromArgb(0,255,0)),
                  new SolidBrush(Color.FromArgb(192,192,192))
                  };
            string[] strs = new string[] { "OK", "FAIL", "OK", "?" };

            //A1、A2
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[UIObj.InBoolList[2] + j + i * 20])
                    {
                        g.DrawRectangle(new Pen(new SolidBrush(Color.White)), new Rectangle(148 + (j / 4) * 42 + i * 306, 254 + 34 * 2 - 27 + 34, 78, 22));
                        g.FillRectangle(brushs[j % 4], new Rectangle(148 + (j / 4) * 42 + 1 + i * 306, 254 + 34 * 2 + 1 - 27 + 34, 78 - 1, 22 - 1));
                        g.DrawString(
                            strs[j % 4],
                            new Font("宋体", 11),
                            (j % 4) == 0 ? Brushs.WhiteBrush : Brushs.BlackBrush,
                            new Rectangle(148 + (j / 4) * 42 + i * 306, 254 + 34 * 2 - 27 + 34, 78, 22),
                            FontInfo.SF_CC
                            );
                    }
                }
            }

            //B1、B2
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (BoolList[UIObj.InBoolList[2] + 4 + j + i * 8])
                    {
                        g.DrawRectangle(new Pen(new SolidBrush(Color.White)), new Rectangle(148 + 102 + (j / 4) * 42 + i * 102, 254 + 34 * 2 - 27 + 34, 36, 22));
                        g.FillRectangle(brushs[j % 4], new Rectangle(148 + (j / 4) * 42 + 102 + 1 + i * 102, 254 + 34 * 2 + 1 - 27 + 34, 36 - 1, 22 - 1));
                        g.DrawString(
                            strs[j % 4],
                            new Font("宋体", 11),
                            (j % 4) == 0 ? Brushs.WhiteBrush : Brushs.BlackBrush,
                            new Rectangle(148 + 102 + (j / 4) * 42 + i * 102, 254 + 34 * 2 - 27 + 34, 36, 22),
                            FontInfo.SF_CC
                            );
                    }
                }
            }
        }

        /// <summary>
        /// 绘制电制动状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_PowerBreaking(Graphics g)
        {
            Brush[] brushs = new Brush[]{
                  Brushs.BlackBrush,
                  Brushs.YellowBrush,
                  new SolidBrush(Color.FromArgb(0,255,0)),
                  new SolidBrush(Color.FromArgb(192,192,192))
                  };
            string[] strs = new string[] { "OK", "FAIL", "OK", "?" };

            //A1、A2
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[UIObj.InBoolList[3] + j + i * 20])
                    {
                        g.DrawRectangle(new Pen(new SolidBrush(Color.White)), new Rectangle(148 + (j / 4) * 42 + i * 306, 254 + 34 * 3 - 27 + 34, 78, 22));
                        g.FillRectangle(brushs[j % 4], new Rectangle(148 + (j / 4) * 42 + 1 + i * 306, 254 + 34 * 3 + 1 - 27 + 34, 78 - 1, 22 - 1));
                        g.DrawString(
                            strs[j % 4],
                            new Font("宋体", 11),
                            (j % 4) == 0 ? Brushs.WhiteBrush : Brushs.BlackBrush,
                            new Rectangle(148 + (j / 4) * 42 + i * 306, 254 + 34 * 3 - 27 + 34, 78, 22),
                            FontInfo.SF_CC
                            );
                    }
                }
            }

            //B1、B2
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (BoolList[UIObj.InBoolList[3] + 4 + j + i * 8])
                    {
                        g.DrawRectangle(new Pen(new SolidBrush(Color.White)), new Rectangle(148 + 102 + (j / 4) * 42 + i * 102, 254 + 34 * 3 - 27 + 34, 36, 22));
                        g.FillRectangle(brushs[j % 4], new Rectangle(148 + (j / 4) * 42 + 102 + 1 + i * 102, 254 + 34 * 3 + 1 - 27 + 34, 36 - 1, 22 - 1));
                        g.DrawString(
                            strs[j % 4],
                            new Font("宋体", 11),
                            (j % 4) == 0 ? Brushs.WhiteBrush : Brushs.BlackBrush,
                            new Rectangle(148 + 102 + (j / 4) * 42 + i * 102, 254 + 34 * 3 - 27 + 34, 36, 22),
                            FontInfo.SF_CC
                            );
                    }
                }
            }
        }

        /// <summary>
        /// 绘制空调状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_AirCondition(Graphics g)
        {
            Brush[] brushs = new Brush[]{
                  Brushs.BlackBrush,
                  Brushs.YellowBrush,
                  new SolidBrush(Color.FromArgb(255,145,0)),
                  new SolidBrush(Color.FromArgb(192,192,192))
                  };
            string[] strs = new string[] { "OK", "FAIL", "OK", "?" };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[UIObj.InBoolList[4] + j + i * 4])
                    {
                        g.DrawRectangle(new Pen(new SolidBrush(Color.White)), new Rectangle(148 + i * 102, 254 + 34 * 4 - 27 + 34, 78, 22));
                        g.FillRectangle(brushs[j % 4], new Rectangle(148 + 1 + i * 102, 254 + 34 * 4 + 1 - 27 + 34, 78 - 1, 22 - 1));
                        g.DrawString(
                            strs[j % 4],
                            new Font("宋体", 11),
                            (j % 4) == 0 ? Brushs.WhiteBrush : Brushs.BlackBrush,
                            new Rectangle(148 + i * 102, 254 + 34 * 4 - 27 + 34, 78, 22),
                            FontInfo.SF_CC
                            );
                    }
                }
            }
        }

        /// <summary>
        /// 绘制车停放制动状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_StopBreaking(Graphics g)
        {
            Brush[] brushs = new Brush[]{
                  Brushs.BlackBrush,
                  Brushs.YellowBrush,
                  new SolidBrush(Color.FromArgb(0,255,0)),
                  Brushs.RedBrush,
                  new SolidBrush(Color.FromArgb(192,192,192))
                  };
            string[] strs = new string[] { "OK", "FAIL", "OK", "ISOL", "?" };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (BoolList[UIObj.InBoolList[5] + j + i * 5])
                    {
                        g.DrawRectangle(new Pen(new SolidBrush(Color.White)), new Rectangle(148 + i * 102, 254 + 34 * 5 - 27 + 34, 78, 22));
                        g.FillRectangle(brushs[j % 5], new Rectangle(148 + 1 + i * 102, 254 + 34 * 5 + 1 - 27 + 34, 78 - 1, 22 - 1));
                        g.DrawString(
                            strs[j % 5],
                            new Font("宋体", 11),
                            (j % 5) == 0 ? Brushs.WhiteBrush : Brushs.BlackBrush,
                            new Rectangle(148 + i * 102, 254 + 34 * 5 - 27 + 34, 78, 22),
                            FontInfo.SF_CC
                            );
                    }
                }
            }
        }

        /// <summary>
        /// 绘制制动压力值
        /// </summary>
        /// <param name="g"></param>
        private void paint_BreakingPressure(Graphics g)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    g.DrawRectangle(new Pen(Brushs.BlackBrush), new Rectangle(148 + j * 39 + i * 102, 254 + 34 * 6 - 27 + 34, 39, 22));
                    g.DrawString(
                        (FloatList[UIObj.InFloatList[0] + j + i * 2]).ToString("0"),
                        new Font("宋体", 11),
                        Brushs.BlackBrush,
                        new Rectangle(148 + j * 39 + i * 102, 254 + 34 * 6 - 27 + 34, 39, 22),
                        FontInfo.SF_CC
                        );
                }
            }
        }

        /// <summary>
        /// 绘制乘客信息状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_Passenger(Graphics g)
        {
            Brush[] brushs = new Brush[]{
                  new SolidBrush(Color.FromArgb(0,255,0)),
                  Brushs.YellowBrush,
                  new SolidBrush(Color.FromArgb(192,192,192))
                  };
            string[] strs = new string[] { "", "", "?" };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[6] + j + i * 3])
                    {
                        g.DrawRectangle(new Pen(new SolidBrush(Color.White)), new Rectangle(148 + i * 102, 254 + 34 * 7 - 27 + 34, 78, 22));
                        g.FillRectangle(brushs[j % 3], new Rectangle(148 + 1 + i * 102, 254 + 34 * 7 + 1 - 27 + 34, 78 - 1, 22 - 1));
                        g.DrawString(
                            strs[j % 3],
                            new Font("宋体", 11),
                            (j % 3) == 0 ? Brushs.WhiteBrush : Brushs.BlackBrush,
                            new Rectangle(148 + i * 102, 254 + 34 * 7 - 27 + 34, 78, 22),
                            FontInfo.SF_CC
                            );
                    }
                }
            }
        }
        #endregion
    }
}
