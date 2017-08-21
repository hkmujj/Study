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
using System.Timers;
using System.Windows.Forms.VisualStyles;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using LightRail.Ethiopia.TMS.Common;
using LightRail.Ethiopia.TMS.Control;
using LightRail.Ethiopia.TMS.Control.Common;

namespace LightRail.Ethiopia.TMS.Main
{
    /// <summary>
    /// 功能描述：公共组件-No.1-切换视图按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V101_Main_ControlButton : baseClass
    {
        #region 私有变量
        private ViewState _currentViewState;                      //当前视图
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        public static List<Button> _btns = new List<Button>();//按钮列表
        public static List<Button> _btns_Switch = new List<Button>();
        public static List<Button> _btns_Mutual = new List<Button>();
        public static Button _btn_SkipStation;
        #endregion

        #region 框架初始化函数
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

            String[] _strs = new[] { "Auto Sanding\nForbidden", "Washing Mode", "Shunting", "Light", "Compulsive\nPump Wind", "HMI Fan", "Skip Station","Coupling" };
            #region 开关命令
            Button btn_0 = new Button(
                    _strs[0],
                    new Rectangle(150 + 0 % 2 * 300, 157 + 0 / 2 * 85-30, 137, 48),
                    0,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[0],
                        DownImage = _resource_Image[1]
                    },
                    false
                    );
            btn_0.MouseDownEvent += btn_Switch_MouseDownEvent;
            btn_0.ClickEvent += btn_Switch_ClickEvent;
            _btns_Switch.Add(btn_0);

            Button btn_5 = new Button(
                    _strs[5],
                    new Rectangle(150 + 5 % 2 * 300, 157 + 5 / 2 * 85 - 30, 137, 48),
                    5,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[0],
                        DownImage = _resource_Image[1]
                    },
                    false
                    );
            btn_5.MouseDownEvent += btn_Switch_MouseDownEvent;
            btn_5.ClickEvent += btn_Switch_ClickEvent;
            _btns_Switch.Add(btn_5);
            #endregion

            #region 互斥命令
            for (int i = 1; i < 3; i++)
            {
                Button btn = new Button(
                    _strs[i],
                    new Rectangle(150 + i % 2 * 300, 157 + i / 2 * 85 - 30, 137, 48),
                    i==1?2:1,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[0],
                        DownImage = _resource_Image[1]
                    },
                    false
                    );
                btn.MouseDownEvent += btn_Mutual_MouseDownEvent;
                btn.ClickEvent += btn_Mutual_ClickEvent;
                _btns_Mutual.Add(btn);
            }
            #endregion

            #region 按键命令
            for (int i = 3; i < 5; i++)
            {
                Button btn = new Button(
                    _strs[i],
                    new Rectangle(150 + i % 2 * 300, 157 + i / 2 * 85 - 30, 137, 48),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[0],
                        DownImage = _resource_Image[1]
                    },
                    false
                    );
                btn.MouseDownEvent += btn_Switch_MouseDownEvent;
                btn.ClickEvent += btn_Switch_ClickEvent;
                _btns_Switch.Add(btn);
            }
            #endregion

            _btn_SkipStation = new Button(
                    _strs[6],
                    new Rectangle(150 + 6 % 2 * 300, 157 + 6 / 2 * 85 - 30, 137, 48),
                    6,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[0],
                        DownImage = _resource_Image[1]
                    },
                    false
                    );
            _btn_SkipStation.MouseDownEvent += _btn_SkipStation_MouseDownEvent;

            Button btn1 = new Button(
                    _strs[7],
                    new Rectangle(150 + 7 % 2 * 300, 157 + 7 / 2 * 85 - 30, 137, 48),
                    3,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[0],
                        DownImage = _resource_Image[1]
                    },
                    false
                    );
            btn1.MouseDownEvent += btn_Mutual_MouseDownEvent;
            btn1.ClickEvent += btn_Mutual_ClickEvent;
            _btns_Mutual.Add(btn1);
            
            Button btn_Quit = new Button(
                    "Quit",
                    new Rectangle(150 + 9 % 2 * 300, 157 + 9 / 2 * 85 - 30, 137, 48),
                    //new Rectangle(617, 512, 137, 48),
                    7,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[0],
                        DownImage = _resource_Image[1]
                    }
                    );
            btn_Quit.MouseDownEvent += btn_MouseDownEvent;
            btn_Quit.ClickEvent += btn_ClickEvent;
            _btns.Add(btn_Quit);

           _timer=new Timer(10000);
           _timer.Elapsed += _timer_Elapsed;

            return true;
        }
        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            _btns.ForEach(a => a.MouseDown(nPoint));
            _btns_Switch.ForEach(a => a.MouseDown(nPoint));
            _btns_Mutual.ForEach(a => a.MouseDown(nPoint));
            _btn_SkipStation.MouseDown(nPoint);
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btns.ForEach(a => a.MouseUp(nPoint));
            _btns_Switch.ForEach(a => a.MouseUp(nPoint));
            _btns_Mutual.ForEach(a => a.MouseUp(nPoint));
            _btn_SkipStation.MouseUp(nPoint);
            return base.mouseUp(nPoint);
        }

        #region 开关命令

        private Int32[] _switchCounts = new[] {0, 0};
        /// <summary>
        /// 菜单切换按钮鼠标按下事件响应函数：改变按钮字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Switch_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            Button btn = _btns_Switch.Find(a => a.ID == e.Message);
            if (btn != null)
            {
                if (btn.IsReplication == true)
                    ((ButtonStyle)btn.Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
            }
            //((ButtonStyle)((Button)sender).Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_Switch_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            //if (Convert.ToInt32(((Int32)this._currentViewState).ToString().Substring(0, 1)) == e.Message + 1)
            //{
            //    (this._btns.Find(a => a.ID == e.Message)).IsReplication = false;
            //    return;
            //}//点击的为同一个视图，函数返回
            Button btn = _btns_Switch.Find(a => a.ID == e.Message);
            if (btn != null)
            {
                if (btn.IsReplication == true)
                {
                    ((ButtonStyle) btn.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + e.Message, 0, 0);
                }
                else
                {
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + e.Message, 1, 0);
                }
            }

            //((ButtonStyle)((Button)sender).Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);


            //switch (e.Message)
            //{
            //    case 7://Quit按钮
            //        append_postCmd(CmdType.ChangePage, 1, 0, 0);
            //        break;
            //}
        }
        #endregion

        #region 互斥命令
        /// <summary>
        /// 菜单切换按钮鼠标按下事件响应函数：改变按钮字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Mutual_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            ((ButtonStyle)((Button)sender).Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
        }

        private Int32 _flag = -1;
        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_Mutual_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (_flag != e.Message)
            {
                _btns_Mutual.FindAll(a => a.ID != e.Message).ForEach(b =>
                {
                    b.IsReplication = true;
                    ((ButtonStyle) b.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);
                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 4800 + b.ID, 1, 0);
                    _flag = e.Message;
                });
                ((Button) sender).IsReplication = false;
                //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 4800 + e.Message, 0, 0);
                //_flag = e.Message;
            }
            else
            {
                //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 4800 + _flag, 0, 0);
                ((Button)sender).IsReplication = true;
                ((ButtonStyle)((Button)sender).Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);
                _flag = -1;
            }
        }
        #endregion

        #region 按键命令
        /// <summary>
        /// 菜单切换按钮鼠标按下事件响应函数：改变按钮字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            ((ButtonStyle)((Button)sender).Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
            //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + e.Message + 4800, 1, 0);
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (Convert.ToInt32(((Int32)_currentViewState).ToString().Substring(0, 1)) == e.Message + 1)
            {
                (_btns.Find(a => a.ID == e.Message)).IsReplication = false;
                return;
            }//点击的为同一个视图，函数返回

            ((ButtonStyle)((Button)sender).Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);


            switch (e.Message)
            {
                case 7://Quit按钮
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
                    break;
                default:
                    //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + e.Message + 4800, 0, 0);
                    break;
            }
        }
        #endregion

        private Boolean IsDown
        {
            set
            {
                if(_isDown==value) return;
                _isDown = value;
                _timer.Start();
            }
        }
        private Boolean _isDown = false;
        private Timer _timer;

        #region 按键命令
        /// <summary>
        /// 菜单切换按钮鼠标按下事件响应函数：改变按钮字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btn_SkipStation_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            ((ButtonStyle)((Button)sender).Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + e.Message, 1, 0);
            IsDown = ((Button) sender).IsReplication;
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _btn_SkipStation.IsReplication = true;
            ((ButtonStyle)_btn_SkipStation.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + _btn_SkipStation.ID, 0, 0);
            _timer.Stop();
        }
        #endregion
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            _btns.ForEach(a => a.Paint(dcGs));
            _btns_Switch.ForEach(a => a.Paint(dcGs));
            _btns_Mutual.ForEach(a => a.Paint(dcGs));
            _btn_SkipStation.Paint(dcGs);

            base.paint(dcGs);
        }
        #endregion
    }
}
