#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-1
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：公共组件-No.1-切换视图按钮
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
using LightRail.Ethiopia.TMS.Control;
using LightRail.Ethiopia.TMS.Control.Common;
using LightRail.Ethiopia.TMS.Pub;

namespace LightRail.Ethiopia.TMS.MianTain
{
    /// <summary>
    /// 功能描述：公共组件-No.1-切换视图按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V803_MianTain_CurrentFaultList : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        private Grid grid;
        private List<List<object>> _values = new List<List<object>>();
        private List<Button> _btns = new List<Button>();
        private FaultInfo _currentFaultInfo = null;
        private Button _btn_Quit;

        public static DeviceState CurrentDeviceState = DeviceState.ALL;
        public static TimeState CurrentTimeState= TimeState.StartTime;
        #endregion

        #region 框架函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共试图-视图切换按钮";
        }

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            _values = new List<List<object>>() { };

            grid = new Grid(
                new RectangleF(20, 123 + 38F, 754, 380 - 38),
                new Row[9],
                new Column[8]
                {
                    new Column()
                    {
                        Width = 50,Font = new Font("Verdana", 9),
                        SF=new StringFormat(){Alignment = StringAlignment.Near,LineAlignment = StringAlignment.Center},
                        Brush = Brushes.Yellow,
                        LinePen = new Pen(Brushes.Yellow,1)
                    } ,
                    new Column()
                    {
                        Width = 50,Font = new Font("Verdana", 9),
                        SF=new StringFormat(){Alignment = StringAlignment.Center,LineAlignment = StringAlignment.Center} ,
                        Brush = Brushes.Yellow,
                        LinePen = new Pen(Brushes.Yellow,1)
                    } ,
                    new Column()
                    {
                        Width = 176,Font = new Font("Verdana", 9),
                        SF=new StringFormat(){Alignment = StringAlignment.Center,LineAlignment = StringAlignment.Center} ,
                        Brush = Brushes.Yellow,
                        LinePen = new Pen(Brushes.Yellow,1)
                    } ,
                    new Column()
                    {
                        Width = 50,Font = new Font("Verdana", 9),
                        SF=new StringFormat(){Alignment = StringAlignment.Center,LineAlignment = StringAlignment.Center} ,
                        Brush = Brushes.Yellow,
                        LinePen = new Pen(Brushes.Yellow,1)
                    } ,
                    new Column()
                    {
                        Width = 230,Font = new Font("Verdana", 9),
                        SF=new StringFormat(){Alignment = StringAlignment.Center,LineAlignment = StringAlignment.Center} ,
                        Brush = Brushes.Yellow,
                        LinePen = new Pen(Brushes.Yellow,1)
                    }  ,
                    new Column()
                    {
                        Width = 66,Font = new Font("Verdana", 9),
                        SF=new StringFormat(){Alignment = StringAlignment.Center,LineAlignment = StringAlignment.Center} ,
                        Brush = Brushes.Yellow,
                        LinePen = new Pen(Brushes.Yellow,1)
                    } ,
                    new Column()
                    {
                        Width = 66,Font = new Font("Verdana", 9),
                        SF=new StringFormat(){Alignment = StringAlignment.Center,LineAlignment = StringAlignment.Center} ,
                        Brush = Brushes.Yellow,
                        LinePen = new Pen(Brushes.Yellow,1)
                    } ,
                    new Column()
                    {
                        Width = 66,Font = new Font("Verdana", 9),
                        SF=new StringFormat(){Alignment = StringAlignment.Center,LineAlignment = StringAlignment.Center} ,
                        Brush = Brushes.Yellow,
                        LinePen = new Pen(Brushes.Yellow,1)
                    } 
                },
                _values,
                new PageButton()
                {
                    Rect = new Rectangle(644, 67, 60, 50),
                    UpImage = _resource_Image[0 + 0 * 2],
                    DownImage = _resource_Image[0 + 0 * 2 + 1],
                    DisableImage = _resource_Image[0 + 0 * 2 + 1],
                },
                new PageButton()
                {
                    Rect = new Rectangle(714, 67, 60, 50),
                    UpImage = _resource_Image[0 + 1 * 2],
                    DownImage = _resource_Image[0 + 1 * 2 + 1],
                    DisableImage = _resource_Image[0 + 1 * 2 + 1],
                }
                );
            grid.ClickEvent += grid_ClickEvent;

            String[] strs = new[] { "Vehicle", "Device", "Start Time", "End Time", "Default", "Quit" };
            RectangleF[] rects = new RectangleF[]
            {
                new RectangleF(19,14,77,45), 
                new RectangleF(19,14+51,77,45), 
                new RectangleF(296,14,118,45), 
                new RectangleF(296,14+51,118,45), 
                new RectangleF(644,14,130,45),
                new RectangleF(506,383,91,41)
            };
            for (int i = 0; i < 5; i++)
            {
                Button btn = new Button(
                    strs[i],
                    rects[i],
                    i,
                    new ButtonStyle()
                    {
                        Background = _resource_Image[4 + i / 2 * 2],
                        DownImage = _resource_Image[5 + i / 2 * 2],
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        }
                    }
                    );
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            _btn_Quit = new Button(
                    strs[5],
                    rects[5],
                    5,
                    new ButtonStyle()
                    {
                        Background = _resource_Image[4 + 5 / 2 * 2],
                        DownImage = _resource_Image[5 + 5 / 2 * 2],
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        }
                    }
                    );
            _btn_Quit.MouseDownEvent += btn_MouseDownEvent;
            _btn_Quit.ClickEvent += btn_ClickEvent;
            return true;
        }

        public override bool mouseDown(Point nPoint)
        {
            grid.MouseDown(nPoint);
            _btns.ForEach(a => a.MouseDown(nPoint));
            if (_currentFaultInfo != null) _btn_Quit.MouseDown(nPoint);

            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            grid.MouseUp(nPoint);
            _btns.ForEach(a => a.MouseUp(nPoint));
            if (_currentFaultInfo != null) _btn_Quit.MouseUp(nPoint);

            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grid_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            Int32 a = e.Message;
            _currentFaultInfo = VC_C4_GetFault.CurrentFaults[a];
        }

        /// <summary>
        /// 菜单切换按钮鼠标按下事件响应函数：改变按钮字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            ((ButtonStyle)((Button)sender).Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            ((ButtonStyle)((Button)sender).Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);
            switch (e.Message)
            {
                case 1:
                    append_postCmd(CmdType.ChangePage, (int)ViewState.MianTain_Fault_DeviceSelect, 0, 0);
                    break;
                case 2:
                    CurrentTimeState= TimeState.StartTime;
                    append_postCmd(CmdType.ChangePage, (int)ViewState.MianTain_Fault_TimeSet, 0, 0);
                    break;
                case 3:
                    CurrentTimeState= TimeState.EndTime;
                    append_postCmd(CmdType.ChangePage, (int)ViewState.MianTain_Fault_TimeSet, 0, 0);
                    break;
                case 5:
                    _currentFaultInfo = null;
                    break;
            }
        }

        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            getValue();
            grid.Paint(dcGs, new Rectangle(644 - 60, 67, 60, 50));
            _btns.ForEach(a => a.Paint(dcGs));

            paint_Frame(dcGs);
            paint_ShowFaultInfo(dcGs);

            base.paint(dcGs);
        }
        #endregion

        /// <summary>
        /// 主要框架绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawLine(new Pen(new SolidBrush(Color.Yellow), 1), new PointF(20, 123), new PointF(774, 123));

            dcGs.DrawLine(new Pen(new SolidBrush(Color.Yellow), 1), new PointF(20, 123), new PointF(20, 123 + 38));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.Yellow), 1), new PointF(20 + 50, 123), new PointF(20 + 50, 123 + 38));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.Yellow), 1), new PointF(20 + 50 + 50, 123), new PointF(20 + 50 + 50, 123 + 38));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.Yellow), 1), new PointF(20 + 50 + 50 + 176, 123), new PointF(20 + 50 + 50 + 176, 123 + 38));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.Yellow), 1), new PointF(20 + 50 + 50 + 176 + 50, 123), new PointF(20 + 50 + 50 + 176 + 50, 123 + 38));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.Yellow), 1), new PointF(20 + 50 + 50 + 176 + 50 + 230, 123), new PointF(20 + 50 + 50 + 176 + 50 + 230, 123 + 38));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.Yellow), 1), new PointF(20 + 50 + 50 + 176 + 50 + 230 + 66, 123), new PointF(20 + 50 + 50 + 176 + 50 + 230 + 66, 123 + 38));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.Yellow), 1), new PointF(20 + 50 + 50 + 176 + 50 + 230 + 66 + 66, 123), new PointF(20 + 50 + 50 + 176 + 50 + 230 + 66 + 66, 123 + 38));
            dcGs.DrawLine(new Pen(new SolidBrush(Color.Yellow), 1), new PointF(20 + 50 + 50 + 176 + 50 + 230 + 66 + 66 + 66, 123), new PointF(20 + 50 + 50 + 176 + 50 + 230 + 66 + 66 + 66, 123 + 38));


            dcGs.DrawString(
                "Vehicle",
                new Font("Verdana", 9),
                Brushes.Yellow,
                new RectangleF(20, 123, 50, 38),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                "Code",
                new Font("Verdana", 9),
                Brushes.Yellow,
                new RectangleF(20 + 50, 123, 50, 38),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                "DateTime",
                new Font("Verdana", 9),
                Brushes.Yellow,
                new RectangleF(20 + 50 + 50, 123, 176, 38),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                "Grade",
                new Font("Verdana", 9),
                Brushes.Yellow,
                new RectangleF(20 + 50 + 50 + 176, 123, 50, 38),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                "Name",
                new Font("Verdana", 9),
                Brushes.Yellow,
                new RectangleF(20 + 50 + 50 + 176 + 50, 123, 230, 38),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                "Device",
                new Font("Verdana", 9),
                Brushes.Yellow,
                new RectangleF(20 + 50 + 50 + 176 + 50 + 230, 123, 66, 38),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                "Position",
                new Font("Verdana", 9),
                Brushes.Yellow,
                new RectangleF(20 + 50 + 50 + 176 + 50 + 230 + 66, 123, 66, 38),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                "Confirm",
                new Font("Verdana", 9),
                Brushes.Yellow,
                new RectangleF(20 + 50 + 50 + 176 + 50 + 230 + 66 + 66, 123, 66, 38),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
        }

        /// <summary>
        /// 获取网络数据（由三个位置数据表示：第一个位置：起始接口编号；第二个位置：包括条目数目；第三个位置：每个条目的状态数目）
        /// </summary>
        private void getValue()
        {
            if (VC_C4_GetFault.CurrentFaults == null)
                return;

            _values.Clear();
            List<FaultInfo> faultInfos =
                CurrentDeviceState == DeviceState.ALL
                    ? VC_C4_GetFault.CurrentFaults
                    : VC_C4_GetFault.CurrentFaults.FindAll(a => a.Device == CurrentDeviceState.ToString());
            for (int i = 0; i < faultInfos.Count; i++)
            {
                List<Object> value = new List<object>()
                     {
                         faultInfos[i].Vehicle,
                         faultInfos[i].Code,
                         faultInfos[i].DateTime,
                         faultInfos[i].Grade,
                         faultInfos[i].Name,
                         faultInfos[i].Device,
                         faultInfos[i].Position,
                         faultInfos[i].IsConfirm
                     };
                _values.Add(value);
            }
        }

        private void paint_ShowFaultInfo(Graphics dcGs)
        {
            if (_currentFaultInfo == null)
                return;

            dcGs.FillRectangle(
                new SolidBrush(Color.FromArgb(54, 54, 54)),
                new RectangleF(34, 11, 581, 423)
                );

            for (int i = 0; i < 5; i++)
            {
                dcGs.DrawLine(new Pen(Brushes.White, 2), new PointF(53, 31 + 41 + 41 * i), new PointF(596, 31 + 41 + 41 * i));
            }

            dcGs.DrawRectangle(new Pen(Brushes.White, 2), new Rectangle(53, 31, 543, 341));

            String[] strs = new[]
            {
                "Vehicle:  " + _currentFaultInfo.Vehicle,
                "ID:  " + _currentFaultInfo.Logic,
                "Code:  " + _currentFaultInfo.Code,
                "Grade:  " + _currentFaultInfo.Grade,
                "Position:  " + _currentFaultInfo.Position,
                "Device:  " + _currentFaultInfo.Device,
                "DateTime:  " + _currentFaultInfo.DateTime,
                "Name:  " + _currentFaultInfo.Name,
                "Advice:  " + _currentFaultInfo.Advice
            };
            for (int i = 0; i < 6; i++)
            {
                dcGs.DrawString(
                    strs[i],
                    new Font("Verdana", 9),
                    Brushes.Yellow,
                    new RectangleF(69 + i % 2 * 256, 31 + i / 2 * 41, 256, 41),
                    new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center }
                    );
            }

            for (int i = 0; i < 2; i++)
            {
                dcGs.DrawString(
                    strs[i + 6],
                    new Font("Verdana", 9),
                    Brushes.Yellow,
                    new RectangleF(69, 31 + (i + 3) * 41, 256, 41),
                    new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center }
                    );
            }

            dcGs.DrawString(
                strs[8],
                new Font("Verdana", 9),
                Brushes.Yellow,
                new RectangleF(69, 31 + (2 + 3) * 41, 256, 136),
                new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center }
                );

            _btn_Quit.Paint(dcGs);
        }
    }
}
