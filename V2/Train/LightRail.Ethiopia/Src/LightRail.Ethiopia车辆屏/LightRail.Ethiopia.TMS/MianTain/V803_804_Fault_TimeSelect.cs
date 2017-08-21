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

namespace LightRail.Ethiopia.TMS.MianTain
{
    public enum TimeState
    {
        StartTime = 0,
        EndTime = 1
    }

    /// <summary>
    /// 功能描述：公共组件-No.1-切换视图按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V803_804_Fault_TimeSelect : baseClass
    {
        #region 私有变量
        private ViewState _currentViewState;                      //当前视图
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        private List<Button> _btns = new List<Button>();//按钮列表
        private ViewState _lastViewState = ViewState.MianTain_CurrentFaultList;
        private List<TextBox> _textBoxs_Station_F_T = new List<TextBox>();//文本输入框列表
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


            for (int i = 1; i < 11; i++)
            {
                Button btn = new Button(
                    (i % 10).ToString(),
                    new Rectangle(500 + (i - 1) % 3 * 62, 123 + (i - 1) / 3 * 62, 64, 64),
                    i - 1,
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
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }
            Button btn_Clear = new Button(
                    "Clear",
                    new Rectangle(500 + (11 - 1) % 3 * 62, 123 + (11 - 1) / 3 * 62, 126, 64),
                    10,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[2],
                        DownImage = _resource_Image[3]
                    }
                    );
            btn_Clear.MouseDownEvent += btn_MouseDownEvent;
            btn_Clear.ClickEvent += btn_ClickEvent;
            _btns.Add(btn_Clear);

            String[] _strs = new[] { "Set", "Quit", "Default" };
            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button(
                    _strs[i],
                    new Rectangle(85 + i * 238, 474, 153, 64),
                    i + 11,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[4],
                        DownImage = _resource_Image[5]
                    }
                    );
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            for (int i = 0; i < 5; i++)
            {
                TextBox textBox = new TextBox(
                    new RectangleF(97 + 127 * (i % 3), 186 + 125 * (i / 3), 89, 38),
                    new TextBoxStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[6],
                        BackgroundFocus = _resource_Image[7]
                    },
                    i
                    );
                if (i == 0) textBox.IsFocus = true;
                _textBoxs_Station_F_T.Add(textBox);
            }

            return true;
        }

        /// <summary>
        /// 获取到当前运行视图：根据当前视图设置按钮状态
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                _lastViewState = (ViewState)nParaC;
            }
        }
        #endregion

        #region 鼠标事件
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
            ((ButtonStyle)((Button)sender).Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            ((ButtonStyle)((Button)sender).Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);

            switch (e.Message)
            {
                case 10:
                    break;
                case 11:
                    TextBox textBox = _textBoxs_Station_F_T.Find(a => a.IsFocus);
                    textBox.IsFocus = false;
                    _textBoxs_Station_F_T[textBox.ID == _textBoxs_Station_F_T.Count - 1 ? 0 : textBox.ID + 1].IsFocus =
                        true;
                    break;
                case 12:
                    append_postCmd(CmdType.ChangePage, (int)_lastViewState,0,0);
                    break;
                case 13:
                    break;
                default:
                    TextBox textBox1 = _textBoxs_Station_F_T.Find(a => a.IsFocus);
                    if (textBox1 != null)
                    {
                        textBox1.Text += e.Message;
                    }
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            _btns.ToList().ForEach(a => a.Paint(dcGs));
            _textBoxs_Station_F_T.ForEach(a => a.Paint(dcGs));

            dcGs.FillRectangle(
                new SolidBrush(Color.FromArgb(255, 255, 0)),
                new Rectangle(0, 0, 800, 70)
                );
            String str = "";
            if (_lastViewState == ViewState.MianTain_CurrentFaultList)
            {
                str = (V803_MianTain_CurrentFaultList.CurrentTimeState == TimeState.StartTime ? "Start Time Set" : "End Time Set");
            }
            else
            {
                str = (V804_MianTain_HistoryFaultList.CurrentTimeState == TimeState.StartTime ? "Start Time Set" : "End Time Set");
            }
            dcGs.DrawString(
                str,
                new Font("Verdana", 15),
                Brushes.Black,
                new RectangleF(0, 0, 800, 70),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );

            String[] strs = new[] { "year", "month", "day", "hour", "minute" };
            for (int i = 0; i < 5; i++)
            {
                dcGs.DrawString(
                    strs[i],
                    new Font("Verdana", 9),
                    Brushes.Yellow,
                    new RectangleF(97 + 127 * (i % 3), 127 + 125 * (i / 3), 89, 58),
                    new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                    );
            }

            base.paint(dcGs);
        }
        #endregion
    }
}
