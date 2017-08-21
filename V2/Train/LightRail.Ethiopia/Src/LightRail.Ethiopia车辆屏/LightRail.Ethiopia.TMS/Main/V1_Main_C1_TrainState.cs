#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-4
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图1-运行-No.3-状态
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using LightRail.Ethiopia.TMS.Common;
using System.Timers;
using LightRail.Ethiopia.TMS.Control;
using LightRail.Ethiopia.TMS.Control.Common;

namespace LightRail.Ethiopia.TMS.Main
{
    /// <summary>
    /// 功能描述：视图1-运行-No.2-状态
    /// 创建人：唐林
    /// 创建时间：2014-7-4
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1_Main_C1_TrainState : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Images = new List<Image>();//图片资源
        private List<Button> _btns = new List<Button>();//按钮列表
        private Timer _timer_Left;
        private int _countLeft = 0;
        private Timer _timer_Right;
        private int _countRight = 0;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "Main试图-车辆简图";
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

            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    "",
                    new Rectangle(77 + i * (60 + 18), 357, 60, 85),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Images[19 + i * 2],
                        DownImage = _resource_Images[19 + i * 2 + 1]
                    }
                    );
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            _timer_Left = new Timer(1000);
            _timer_Left.Elapsed += _timer_Left_Elapsed;

            _timer_Right = new Timer(1000);
            _timer_Right.Elapsed += _timer_Right_Elapsed;

            return true;
        }

        private int _countRightFlag = 0;
        void _timer_Right_Elapsed(object sender, ElapsedEventArgs e)
        {
            _countRightFlag = (_countRightFlag + 1) % 2;
            ((ButtonStyle)_btns[1].Style).Background = _resource_Images[21 + _countRightFlag];
        }

        private int _countLeftFlag = 0;
        void _timer_Left_Elapsed(object sender, ElapsedEventArgs e)
        {
            _countLeftFlag = (_countLeftFlag + 1)%2;
            ((ButtonStyle) _btns[0].Style).Background = _resource_Images[19 + _countLeftFlag];
        }

        public override bool mouseDown(Point nPoint)
        {
            _btns.ToList().ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btns.ToList().ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 菜单切换按钮鼠标按下事件响应函数：改变按钮字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0:
                    _countLeft = (_countLeft + 1)%2;
                    if (_countLeft == 1)
                    {
                        _timer_Left.Start();
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                    }
                    else
                    {
                        _timer_Left.Stop();
                        ((ButtonStyle)_btns[0].Style).Background = _resource_Images[19];
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                    }
                    break;
                case 1:
                    _countRight = (_countRight + 1) % 2;
                    if (_countRight == 1)
                    {
                        _timer_Right.Start();
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 1, 0);
                    }
                    else
                    {
                        _timer_Right.Stop();
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 0, 0);
                        ((ButtonStyle)_btns[1].Style).Background = _resource_Images[21];
                    }
                    break;
            }
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {

            //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1] + 4800, 0, 0);

            //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 4800, 0, 0);
        }

        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            paint_Frame(dcGs);
            paint_DriverRoom(dcGs);
            paint_DoorState(dcGs);
            paint_Direction(dcGs);
            paint_TractionState(dcGs);
            paint_AuxiliaryState(dcGs);
            paint_BreakState(dcGs);
            paint_HSCB(dcGs);
            paint_Gong(dcGs);
            _btns.ToList().ForEach(a => a.Paint(dcGs));
            paint_BCC(dcGs);
            paint_TrainID(dcGs);
            paint_TCU(dcGs);

            base.paint(dcGs);
        }
        #endregion

        #region 自定义方法
        /// <summary>
        /// Main-车辆简图背景绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Images[0], new Point(0, 77));
        }

        /// <summary>
        /// Main-司机室激活
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_DriverRoom(Graphics dcGs)
        {
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    dcGs.DrawImage(_resource_Images[2], new Rectangle(252 + 420 * i, 360, 53, 82));
                }
            }
        }

        /// <summary>
        /// Main-门状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_DoorState(Graphics dcGs)
        {
            SolidBrush[] sbs = new SolidBrush[7]
            {
                new SolidBrush(Color.DarkGray),
                new SolidBrush(Color.Cyan),
                new SolidBrush(Color.LimeGreen),
                new SolidBrush(Color.Red),
                new SolidBrush(Color.Violet), 
                new SolidBrush(Color.Black),
                new SolidBrush(Color.Red)
            };

            for (int i = 0; i < 8; i++)
            {
                String nr = (i + 1).ToString();
                Font font = new Font("Verdana", 17, FontStyle.Bold);
                SolidBrush textBrush = new SolidBrush(Color.Black);
                SolidBrush xBrush = new SolidBrush(Color.Black);
                bool isShowX = false;
                for (int j = 0; j < 7; j++)
                {
                    dcGs.DrawRectangle(new Pen(new SolidBrush(Color.Yellow), 1), new Rectangle(315 + 101 * ((i % 4) / 2) + 222 * (i / 4), 338 + 93 * (i % 2), 23, 32));
                    if (BoolList[UIObj.InBoolList[1] + i * 7 + j])
                    {
                        dcGs.FillRectangle(sbs[j], new Rectangle(316 + 101 * ((i % 4) / 2) + 222 * (i / 4), 339 + 93 * (i % 2), 22, 31));
                        if (j == 0)
                        {
                            isShowX = true;
                        }
                        if (j == 5)
                        {
                            isShowX = true;
                            xBrush = new SolidBrush(Color.Red);
                            textBrush = new SolidBrush(Color.Red);
                        }
                        if (j == 6)
                        {
                            nr = "ER";
                            font = new Font("Verdana", 10, FontStyle.Bold);
                        }
                    }
                }
                dcGs.DrawString(
                    nr,
                    font,
                    textBrush,
                    new RectangleF(313 + 101 * ((i % 4) / 2) + 222 * (i / 4), 338 + 92 * (i % 2), 27, 32),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
                if (isShowX)
                {
                    dcGs.DrawLine(
                        new Pen(xBrush, 2),
                        new Point(313 + 101 * ((i % 4) / 2) + 222 * (i / 4) + 2, 338 + 92 * (i % 2) - 2),
                        new Point(313 + 101 * ((i % 4) / 2) + 222 * (i / 4) + 27 - 2, 338 + 92 * (i % 2) + 32)
                        );
                    dcGs.DrawLine(
                        new Pen(xBrush, 2),
                        new Point(313 + 101 * ((i % 4) / 2) + 222 * (i / 4) + 27 - 2, 338 + 92 * (i % 2) - 2),
                        new Point(313 + 101 * ((i % 4) / 2) + 222 * (i / 4) + 2, 338 + 92 * (i % 2) + 32)
                        );
                    //dcGs.DrawString(
                    //     "X",
                    //     new Font("Verdana", 32),
                    //     textBrush,
                    //     new RectangleF(313 + 101 * ((i % 4) / 2) + 222 * (i / 4)+2, 338 + 92 * (i % 2)+2, 27, 32),
                    //     new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    //     );
                }
            }
        }

        /// <summary>
        /// Main-方向
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Direction(Graphics dcGs)
        {
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    dcGs.DrawImage(_resource_Images[4 + i], new Rectangle(229 + 451 * (BoolList[UIObj.InBoolList[0]] ? 0 : 1), 285, 65, 35));
                }
            }
        }

        /// <summary>
        /// Main-牵引状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_TractionState(Graphics dcGs)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (BoolList[UIObj.InBoolList[3] + i * 2 + j])
                    {
                        dcGs.DrawImage(_resource_Images[6 + j], new Rectangle(310 + 313 * i, 381, 43, 34));
                    }
                }
            }
        }

        /// <summary>
        /// Main-辅助状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_AuxiliaryState(Graphics dcGs)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (BoolList[UIObj.InBoolList[4] + i * 2 + j])
                    {
                        dcGs.DrawImage(_resource_Images[8 + j], new Rectangle(401 + 131 * i, 381, 43, 34));
                    }
                }
            }
        }

        /// <summary>
        /// Main-制动状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_BreakState(Graphics dcGs)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[UIObj.InBoolList[5] + i * 4 + j])
                    {
                        dcGs.DrawImage(_resource_Images[10 + j], new Rectangle(355 + 5 + 111 * i, 387, 33, 24));
                    }
                }
            }
        }

        /// <summary>
        /// Main-HSCB状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_HSCB(Graphics dcGs)
        {
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[6] + i])
                {
                    dcGs.DrawImage(_resource_Images[14 + i], new Rectangle(463, 343, 50, 34));
                }
            }
        }

        private void paint_Gong(Graphics dcGs)
        {
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[7] + i])
                {
                    dcGs.DrawImage(_resource_Images[16 + i], new Rectangle(461, 298, 51, 39));
                    break;
                }
            }
        }

        private Brush[] _pens = new Brush[] { Brushes.LimeGreen, Brushes.DarkGray, Brushes.Red };
        private void paint_BCC(Graphics dcGs)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[8] + j + i * 3])
                    {
                        //dcGs.DrawRectangle(new Pen(new SolidBrush(Color.Yellow), 1), new Rectangle(344 + 222 * i, 342, 66, 31));
                        dcGs.FillRectangle(_pens[j], new Rectangle(345 + 222 * i, 343, 64, 29));
                        dcGs.DrawString(
                            "BCC",
                            new Font("Verdana", 10, FontStyle.Bold),
                            new SolidBrush(Color.Yellow),
                            new RectangleF(345 + 222 * i, 343, 64, 29),
                            new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                            );
                    }
                }
            }
        }

        private void paint_TrainID(Graphics dcGs)
        {
            dcGs.DrawString(
                FloatList[UIObj.InFloatList[0]].ToString(),
                new Font("Verdana", 15),
                new SolidBrush(Color.Yellow),
                new RectangleF(355, 310, 48, 26),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                );
        }


        private void paint_TCU(Graphics dcGs)
        {
            if (BoolList[UIObj.InBoolList[9]])
            {
                //dcGs.DrawRectangle(new Pen(new SolidBrush(Color.Yellow), 1), new Rectangle(344 + 222 * i, 342, 66, 31));
                dcGs.FillEllipse(Brushes.LimeGreen, new Rectangle(539, 298, 30, 30));
                dcGs.DrawString(
                    "TCU",
                    new Font("Verdana", 8, FontStyle.Bold),
                    new SolidBrush(Color.Black),
                    new RectangleF(539 + 1, 298 + 1, 28, 28),
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
                    );
            }
        }

        #endregion
    }
}
