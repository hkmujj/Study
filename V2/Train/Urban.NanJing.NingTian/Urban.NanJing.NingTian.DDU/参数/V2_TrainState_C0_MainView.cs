#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-22
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图2-车辆状态-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using Urban.NanJing.NingTian.DDU.Common;

namespace Urban.NanJing.NingTian.DDU.参数
{
    /// <summary>
    /// 功能描述：视图2-车辆状态-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-22
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V2_TrainState_C0_MainView : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        private Font _font;//字体
        private Button _btn_Check;//自检按钮
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "车辆状态视图-主界面";
        }

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            _font = new Font("宋体", 10.5f, FontStyle.Bold);

            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            _btn_Check = new Button(
                "制动\n自检",
                new RectangleF(714, 471, 81, 63),
                0,
                new ButtonStyle()
                {
                    FontStyle = FontStyles.FS_Song_13_CC_B,
                    Background = _resource_Image[1],
                    DownImage = _resource_Image[2]
                }
                );
            _btn_Check.ClickEvent += _btn_Check_ClickEvent;

            return true;
        }
        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            _btn_Check.MouseDown(nPoint);
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btn_Check.MouseUp(nPoint);
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 开始自检按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _btn_Check_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            //开始自检
            //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
            _isBreakingSelf = true;
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics g)
        {
            _btn_Check.Paint(g);

            paint_Frame(g);
            paint_LinkingState(g);
            paint_VVVF(g);
            paint_MiddleVoltageState(g);
            paint_MachineElectricity(g);
            paint_SIVState(g);
            paint_300VOut(g);
            paint_110VOut(g);
            paint_ChargeElectricity(g);
            paint_WindPressure(g);
            paint_PowerPlug(g);
            paint_NoSpringPressure(g);
            paint_KMK(g);
            paint_TSK(g);
            paint_ATC(g);
            paint_BrakingPressure(g);
            paint_BreakingSelfCheck(g);

            base.paint(g);
        }

        /// <summary>
        /// 线框与不变文本绘制
        /// </summary>
        /// <param name="g"></param>
        private void paint_Frame(Graphics g)
        {
            //上线框
            g.DrawLine(new Pen(Brushs.BlackBrush, 2), new Point(0, 108), new Point(800, 108));
            g.DrawImage(_resource_Image[0], new Point(0, 108));//背景

            //标题表格线框
            for (int i = 0; i < 15; i++)
            {
                g.DrawRectangle(new Pen(Brushs.WhiteBrush, 1.5f), new Rectangle(53, 160 + 25 * i, 152, 23));
            }

            //内容线框
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 15; i++)
                {
                    g.DrawRectangle(new Pen(Brushs.WhiteBrush, 1.5f), new Rectangle(206 + 125 * j, 160 + 25 * i, 124, 23));
                }
            }

            //上4行中的小表格
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    g.DrawRectangle(new Pen(Brushs.WhiteBrush, 1.5f), 206 + 125 * (j / 2 + 1) + 62.5F * (j % 2), 160 + 25 * i, 61.5F, 23);
                }
            }

            //最后1行小表格
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    g.DrawRectangle(new Pen(Brushs.WhiteBrush, 1.5f), 206 + 125 * i + 62.5F * j, 160 + 25 * 14, 61.5F, 23);
                }
            }

            string[] terms = new string[] { "主断/主熔断器", "VVVF", "中间电压(V)", "中间电流(A)", "SIV", "辅助输出相电压(V)", "110V输出(V)", "充电电流(A)", "总风压力(bar)", "车间电源插头", "空簧压力(kPa)", "KMK", "TSK", "ATC系统断路器", "制动自检" };
            for (int i = 0; i < 15; i++)
            {
                g.DrawString(terms[i], _font, new SolidBrush(Color.White), new RectangleF(53, 160 + 2 + 25 * i, 152, 22), FontInfo.SF_CC);
            }
        }

        /// <summary>
        /// 主断状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_LinkingState(Graphics g)
        {
            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 0, 160 + 2, 123, 22), FontInfo.SF_CC);
            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 3, 160 + 2, 123, 22), FontInfo.SF_CC);

            Brush[] brushs = new Brush[] { Brushs.GreenBrush, Brushs.WhiteBrush, new SolidBrush(Color.FromArgb(156, 168, 156)) };

            string[] strs = new string[] { "HSCB", "FUSE", "FUSE", "HSCB" };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[0] + j + i * 3])
                    {
                        g.FillRectangle(brushs[j], new RectangleF(206 + 125 * (i / 2 + 1) + 0.5F + 62f * (i % 2), 160 + 25 * 0 + 1F, 61.5f - 2.5f, 23 - 2f));
                    }
                }
                g.DrawString(strs[i], new Font("Arial", 13), Brushs.WhiteBrush, new RectangleF(206 + 125 * (i / 2 + 1) + 0.5F + 62f * (i % 2), 160 + 25 * 0 + 1F, 61.5f - 2.5f, 23 - 2f), FontInfo.SF_CC);
            }
        }

        /// <summary>
        /// VVVF状态绘制
        /// </summary>
        /// <param name="g"></param>
        private void paint_VVVF(Graphics g)
        {
            Brush[] brushs = new Brush[] { Brushs.GreenBrush, Brushs.RedBrush, new SolidBrush(Color.FromArgb(255, 255, 0)), new SolidBrush(Color.FromArgb(156, 168, 156)) };

            //A1 A2
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[UIObj.InBoolList[1] + j + i * 4])
                    {
                        g.FillRectangle(brushs[j], new RectangleF(206 + 0.5F + 125 * (i * 3), 160 + 25 * 1 + 1F, 122.5f, 23 - 2f));
                    }
                }
            }

            //B1 B2
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[UIObj.InBoolList[1] + 8 + j + i * 4])
                    {
                        g.FillRectangle(brushs[j], new RectangleF(206 + 125 * (i / 2 + 1) + 0.5F + 62f * (i % 2), 160 + 25 * 1 + 1F, 61.5f - 2.5f, 23 - 2f));
                    }
                }
            }
        }

        /// <summary>
        /// 中间电压状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_MiddleVoltageState(Graphics g)
        {
            //A1 
            g.DrawString(
                    FloatList[UIObj.InFloatList[0] + 0].ToString("0"),
                    new Font("Arial", 13),
                    new SolidBrush(Color.White),
                    new RectangleF(206 + 0.5F + 125 * (0 * 3), 160 + 25 * 2 + 1F, 122.5f, 23 - 2f),
                    FontInfo.SF_CC
                    );
            //B1 B2
            for (int i = 0; i < 4; i++)
            {
                g.DrawString(
                    FloatList[UIObj.InFloatList[0] + 1 + i].ToString("0"),
                    new Font("Arial", 13),
                    new SolidBrush(Color.White),
                    new RectangleF(206 + 125 * (i / 2 + 1) + 0.5F + 62f * (i % 2), 160 + 25 * 2 + 1F, 61.5f - 2.5f, 23 - 2f),
                    FontInfo.SF_CC
                    );
            }
            //A2
            g.DrawString(
                    FloatList[UIObj.InFloatList[0] + 5].ToString("0"),
                    new Font("Arial", 13),
                    new SolidBrush(Color.White),
                    new RectangleF(206 + 0.5F + 125 * (1 * 3), 160 + 25 * 2 + 1F, 122.5f, 23 - 2f),
                    FontInfo.SF_CC
                    );
        }

        /// <summary>
        /// 电机电流状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_MachineElectricity(Graphics g)
        {
            //A1 
            g.DrawString(
                    FloatList[UIObj.InFloatList[1] + 0].ToString("0"),
                    new Font("Arial", 13),
                    new SolidBrush(Color.White),
                    new RectangleF(206 + 0.5F + 125 * (0 * 3), 160 + 25 * 3 + 1F, 122.5f, 23 - 2f),
                    FontInfo.SF_CC
                    );
            //B1 B2
            for (int i = 0; i < 4; i++)
            {
                g.DrawString(
                    FloatList[UIObj.InFloatList[1] + 1 + i].ToString("0"),
                    new Font("Arial", 13),
                    new SolidBrush(Color.White),
                    new RectangleF(206 + 125 * (i / 2 + 1) + 0.5F + 62f * (i % 2), 160 + 25 * 3 + 1F, 61.5f - 2.5f, 23 - 2f),
                    FontInfo.SF_CC
                    );
            }
            //A2
            g.DrawString(
                    FloatList[UIObj.InFloatList[1] + 5].ToString("0"),
                    new Font("Arial", 13),
                    new SolidBrush(Color.White),
                    new RectangleF(206 + 0.5F + 125 * (1 * 3), 160 + 25 * 3 + 1F, 122.5f, 23 - 2f),
                    FontInfo.SF_CC
                    );
        }

        /// <summary>
        /// SIV状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_SIVState(Graphics g)
        {
            Brush[] brushs = new Brush[] { Brushs.GreenBrush, new SolidBrush(Color.FromArgb(255, 255, 0)), new SolidBrush(Color.FromArgb(156, 168, 156)) };

            //A1 A2
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[2] + j + i * 3])
                    {
                        g.FillRectangle(brushs[j], new RectangleF(206 + 0.5F + 125 * (i * 3), 160 + 25 * 4 + 1F, 122.5f, 23 - 2f));
                    }
                }
            }

            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 1, 160 + 25 * 4 + 1, 123, 22), FontInfo.SF_CC);
            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 2, 160 + 25 * 4 + 1, 123, 22), FontInfo.SF_CC);
        }

        /// <summary>
        /// 300v电压状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_300VOut(Graphics g)
        {
            //A1 A2
            for (int i = 0; i < 2; i++)
            {
                g.DrawString(FloatList[UIObj.InFloatList[2] + i].ToString("0"), new Font("Arial", 13), Brushs.WhiteBrush, new RectangleF(206 + 0.5F + 125 * (i * 3), 160 + 25 * 5 + 1F, 122.5f, 23 - 2f), FontInfo.SF_CC);
            }

            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 1, 160 + 25 * 5 + 1, 123, 22), FontInfo.SF_CC);
            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 2, 160 + 25 * 5 + 1, 123, 22), FontInfo.SF_CC);
        }

        /// <summary>
        /// 110v电压状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_110VOut(Graphics g)
        {
            //A1 A2
            for (int i = 0; i < 2; i++)
            {
                g.DrawString(FloatList[UIObj.InFloatList[3] + i].ToString("0"), new Font("Arial", 13), Brushs.WhiteBrush, new RectangleF(206 + 0.5F + 125 * (i * 3), 160 + 25 * 6 + 1F, 122.5f, 23 - 2f), FontInfo.SF_CC);
            }

            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 1, 160 + 25 * 6 + 1, 123, 22), FontInfo.SF_CC);
            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 2, 160 + 25 * 6 + 1, 123, 22), FontInfo.SF_CC);
        }

        /// <summary>
        /// 充电电流状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_ChargeElectricity(Graphics g)
        {
            //A1 A2
            for (int i = 0; i < 2; i++)
            {
                g.DrawString(FloatList[UIObj.InFloatList[4] + i].ToString("0"), new Font("Arial", 13), Brushs.WhiteBrush, new RectangleF(206 + 0.5F + 125 * (i * 3), 160 + 25 * 7 + 1F, 122.5f, 23 - 2f), FontInfo.SF_CC);
            }

            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 1, 160 + 25 * 7 + 1, 123, 22), FontInfo.SF_CC);
            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 2, 160 + 25 * 7 + 1, 123, 22), FontInfo.SF_CC);
        }

        /// <summary>
        /// 总风压力状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_WindPressure(Graphics g)
        {
            //A1 A2
            for (int i = 0; i < 2; i++)
            {
                g.DrawString(FloatList[UIObj.InFloatList[5] + i].ToString("0.0"), new Font("Arial", 13), Brushs.WhiteBrush, new RectangleF(206 + 0.5F + 125 * (i * 3), 160 + 25 * 8 + 1F, 122.5f, 23 - 2f), FontInfo.SF_CC);
            }

            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 1, 160 + 25 * 8 + 1, 123, 22), FontInfo.SF_CC);
            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 2, 160 + 25 * 8 + 1, 123, 22), FontInfo.SF_CC);
        }

        /// <summary>
        /// 车间电源插头状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_PowerPlug(Graphics g)
        {
            Brush[] brushs = new Brush[] { new SolidBrush(Color.FromArgb(255, 255, 0)), Brushs.GreenBrush, new SolidBrush(Color.FromArgb(156, 168, 156)) };

            //A1 A2
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[3] + j + i * 3])
                    {
                        g.FillRectangle(brushs[j], new RectangleF(206 + 0.5F + 125 * (i + 1), 160 + 25 * 9 + 1F, 122.5f, 23 - 2f));
                    }
                }
            }

            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 0, 160 + 25 * 9 + 1, 123, 22), FontInfo.SF_CC);
            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 3, 160 + 25 * 9 + 1, 123, 22), FontInfo.SF_CC);
        }

        /// <summary>
        /// 空簧压力状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_NoSpringPressure(Graphics g)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    g.DrawRectangle(new Pen(Brushs.WhiteBrush, 1.5f), 206 + 125 * i + 31.5F * j, 160 + 25 * 10, 30.5F, 23);
                    g.DrawString(
                        FloatList[UIObj.InFloatList[6] + j + i * 4].ToString("0"),
                        new Font("Arial", 11),
                        Brushs.WhiteBrush,
                        new RectangleF(206 + 125 * i + 31.5F * j, 160 + 25 * 10, 30.5F, 23),
                        FontInfo.SF_CC
                        );
                }
            }
        }

        /// <summary>
        /// KMK状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_KMK(Graphics g)
        {
            Brush[] brushs = new Brush[] { new SolidBrush(Color.FromArgb(255, 255, 0)), Brushs.BlackBrush, new SolidBrush(Color.FromArgb(156, 168, 156)) };

            //A1 A2
            for (int j = 0; j < 3; j++)
            {
                if (BoolList[UIObj.InBoolList[4] + j])
                {
                    g.FillRectangle(brushs[j], new RectangleF(206 + 0.5F + 125 * (0 + 1), 160 + 25 * 11 + 1F, 122.5f, 23 - 2f));
                }
            }

            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 0, 160 + 25 * 11 + 1, 123, 22), FontInfo.SF_CC);
            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 2, 160 + 25 * 11 + 1, 123, 22), FontInfo.SF_CC);
            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 3, 160 + 25 * 11 + 1, 123, 22), FontInfo.SF_CC);
        }

        /// <summary>
        /// TSK状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_TSK(Graphics g)
        {
            Brush[] brushs = new Brush[] { Brushs.GreenBrush, Brushs.RedBrush, new SolidBrush(Color.FromArgb(156, 168, 156)) };

            //A1 A2
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[5] + j + i * 3])
                    {
                        g.FillRectangle(brushs[j], new RectangleF(206 + 0.5F + 125 * (i * 3), 160 + 25 * 12 + 1F, 122.5f, 23 - 2f));
                    }
                }
            }

            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 1, 160 + 25 * 12 + 1, 123, 22), FontInfo.SF_CC);
            g.DrawString("-", new Font("Arial", 18, FontStyle.Bold), Brushs.WhiteBrush, new Rectangle(207 + 125 * 2, 160 + 25 * 12 + 1, 123, 22), FontInfo.SF_CC);
        }

        /// <summary>
        /// ATC状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_ATC(Graphics g)
        {
            Brush[] brushs = new Brush[] { Brushs.GreenBrush, Brushs.RedBrush, new SolidBrush(Color.FromArgb(156, 168, 156)) };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[6] + j + i * 3])
                    {
                        g.FillRectangle(brushs[j], new RectangleF(206 + 0.5F + 125 * i, 160 + 25 * 13 + 1F, 122.5f, 23 - 2f));
                    }
                }
            }
        }

        /// <summary>
        /// 制动缸压力状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_BrakingPressure(Graphics g)
        {
            Brush[] brushs = new Brush[] { Brushs.RedBrush, Brushs.GreenBrush, Brushs.WhiteBrush, new SolidBrush(Color.FromArgb(156, 168, 156)) };

            //B1 B2
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[UIObj.InBoolList[7] + j + i * 4])
                    {
                        g.FillRectangle(brushs[j], new RectangleF(206 + 125 * (i / 2) + 0.5F + 62f * (i % 2), 160 + 25 * 14 + 1F, 61.5f - 2.5f, 23 - 2f));
                    }
                }
            }
        }



        private int _time = 0;
        private bool _isBreakingSelf = false;
        private void paint_BreakingSelfCheck(Graphics g)
        {
            if (_isBreakingSelf == false) return;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    g.FillRectangle(Brushes.White, new RectangleF(206 + 125 * i + 62.5F * j+0.5F, 160 + 25 * 14 + 1F, 61.5f - 2.5f, 23 - 2f));
                }
            }
            _time++;
            for (int i = 0; i < 2; i++)
            {
                g.FillRectangle(Brushes.LimeGreen, new RectangleF(206 + 375 * i + 0.5F + 62f * (i % 2), 160 + 25 * 14 + 1F, 61.5f - 2.5f + 2, 23 - 2f));
                g.DrawString("通过", new Font("Arial", 10.5F), Brushs.WhiteBrush, new RectangleF(206 + 375 * i + 0.5F + 62f * (i % 2), 160 + 25 * 14 + 1F + 3, 61.5f - 2.5f + 2, 23 - 2f), FontInfo.SF_CC);
            }
            if (_time < 3) return;
            for (int i = 0; i < 2; i++)
            {
                g.FillRectangle(Brushes.LimeGreen, new RectangleF(206 + 62f + 250 * i + 0.5F + 62f * (i % 2), 160 + 25 * 14 + 1F, 61.5f - 2.5f + 2, 23 - 2f));
                g.DrawString("通过", new Font("Arial", 10.5F), Brushs.WhiteBrush, new RectangleF(206 + 62f + 250 * i + 0.5F + 62f * (i % 2), 160 + 25 * 14 + 1F + 3, 61.5f - 2.5f + 2, 23 - 2f), FontInfo.SF_CC);
            }
            if (_time < 6) return;
            for (int i = 0; i < 2; i++)
            {
                g.FillRectangle(Brushes.LimeGreen, new RectangleF(206 + 124 + 125 * i + 0.5F + 62f * (i % 2), 160 + 25 * 14 + 1F, 61.5f - 2.5f + 2, 23 - 2f));
                g.DrawString("通过", new Font("Arial", 10.5F), Brushs.WhiteBrush, new RectangleF(206 + 124 + 125 * i + 0.5F + 62f * (i % 2), 160 + 25 * 14 + 1F + 3, 61.5f - 2.5f + 2, 23 - 2f), FontInfo.SF_CC);
            }
            if (_time < 9) return;
            for (int i = 0; i < 2; i++)
            {
                g.FillRectangle(Brushes.LimeGreen, new RectangleF(206 + 186 + 0.5F + 62f * (i % 2) + 1.5F * i, 160 + 25 * 14 + 1, 61.5f - 2.5f + 2F, 23 - 2f));
                g.DrawString("通过", new Font("Arial", 10.5F), Brushs.WhiteBrush, new RectangleF(206 + 186 + 0.5F + 62f * (i % 2), 160 + 25 * 14 + 1F + 3, 61.5f - 2.5f + 3, 23 - 2f), FontInfo.SF_CC);
            }
        }
        #endregion
    }
}
