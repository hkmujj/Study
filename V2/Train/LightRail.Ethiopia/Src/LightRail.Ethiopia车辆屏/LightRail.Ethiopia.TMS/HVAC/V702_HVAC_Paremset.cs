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
using LightRail.Ethiopia.TMS.Control;
using LightRail.Ethiopia.TMS.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace LightRail.Ethiopia.TMS.HVAC
{
    /// <summary>
    /// 功能描述：公共组件-No.1-切换视图按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V702_HVAC_Paremset : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        public static List<Button> _btns = new List<Button>();//按钮列表
        private List<Button> _btns_Deriction = new List<Button>();
        private Int32 _currentTemperature = 0;
        public static Int32 _currentStateID = 0;
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

            String[] _strs = new String[] { "Auto Warm", "Auto Cold", "Ventilation", "Stop" };
            for (int i = 0; i < 4; i++)
            {
                Button btn = new Button(
                    _strs[i],
                    new RectangleF(80 + 157 * i, 296, 145, 52),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[5],
                        DownImage = _resource_Image[6]
                    },
                    false
                    );
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            String[] _strs_ = new String[] { "Half Cold T", "CabEB", "EBGlass" };
            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button(
                    _strs_[i],
                    new RectangleF(80 + 157 * i, 423, 145, 52),
                    i + 4,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[5],
                        DownImage = _resource_Image[6]
                    },
                    false
                    );
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }
            _btns[3].IsReplication = false;
            ((ButtonStyle)_btns[3].Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
            _currentStateID = UIObj.OutBoolList[0] + 3;
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 3 , 1, 0);

            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    "",
                    new RectangleF(485, 182 + 46 * i, 45, 38),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[1 + 2 * i],
                        DownImage = _resource_Image[2 + 2 * i]
                    }
                    );
                btn.ClickEvent += btn_ClickEvent_;
                _btns_Deriction.Add(btn);
            }

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            _btns.ForEach(a => a.Paint(dcGs));
            _btns_Deriction.ForEach(a => a.Paint(dcGs));
            paint_Frame(dcGs);

            base.paint(dcGs);
        }

        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            _btns.ForEach(a => a.MouseDown(nPoint));
            _btns_Deriction.ForEach(a => a.MouseDown(nPoint));

            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btns.ForEach(a => a.MouseUp(nPoint));
            _btns_Deriction.ForEach(a => a.MouseUp(nPoint));

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
        private void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            //Button btn = _btns.Find(a => !a.IsReplication);
            //if(btn==null||btn.ID==e.Message)
            //    return;

            //_btns.Find(a => a.ID == e.Message).IsReplication = false;
            //((ButtonStyle)btn.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);

            //btn.IsReplication = true;

            _btns.FindAll(a => a.ID != e.Message).ForEach(b =>
            {
                b.IsReplication = true;
                ((ButtonStyle)b.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);

            });
            V703_HVAC_Test._btns.FindAll(a => a.ID != e.Message).ForEach(b =>
            {
                b.IsReplication = true;
                ((ButtonStyle)b.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);

            });
            _btns[e.Message].IsReplication = false;
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + e.Message , 1, 0);
            append_postCmd(CmdType.SetBoolValue, _currentStateID , 0, 0);
            _currentStateID = UIObj.OutBoolList[0] + e.Message;
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClickEvent_(object sender, ClickEventArgs<int> e)
        {
            ((ButtonStyle)((Button)sender).Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);

            switch (e.Message)
            {
                case 0:
                    _currentTemperature++;
                    break;
                case 1:
                    _currentTemperature--;
                    break;
            }
        }
        #endregion

        private void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawLine(
                new Pen(Brushes.White, 2),
                new PointF(0, 384),
                new PointF(800, 384)
                );

            dcGs.DrawImage(
                _resource_Image[0],
                new RectangleF(300, 146, 200, 32)
                );

            dcGs.DrawString(
                "452",
                new Font("Verdana", 11),
                Brushes.Yellow,
                new RectangleF(300, 89, 200, 57),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                "Temperature\nSet",
                new Font("Verdana", 10),
                Brushes.Yellow,
                new RectangleF(293, 195, 147, 57),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );

            dcGs.DrawString(
                _currentTemperature.ToString(),
                new Font("Verdana", 10),
                Brushes.Yellow,
                new RectangleF(430, 195, 45, 57),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
        }
    }
}
