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
    public class V703_HVAC_Test : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        public static List<Button> _btns = new List<Button>();//按钮列表
        //private Int32 _currentStateID = 0;
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

            String[] _strs = new String[] { "FullColdTest", "FullWarmTest", "Emergency\nVentilation", "HalfColdTest", "HalfWarmTest", "Stop" };
            for (int i = 0; i < 6; i++)
            {
                Button btn = new Button(
                    _strs[i],
                    new RectangleF(80 + 157 * (i % 4), 250 + 75 * (i / 4), 145, 52),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[1],
                        DownImage = _resource_Image[2]
                    },
                    false
                    );
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }
            //_btns[5].IsReplication = false;
            //((ButtonStyle)_btns[5].Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
            //append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 5 + 4800, 1, 0);

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            _btns.ForEach(a => a.Paint(dcGs));
            paint_Frame(dcGs);

            base.paint(dcGs);
        }

        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            _btns.ForEach(a => a.MouseDown(nPoint));

            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btns.ForEach(a => a.MouseUp(nPoint));

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
            _btns.FindAll(a => a.ID != e.Message).ForEach(b =>
            {
                b.IsReplication = true;
                ((ButtonStyle)b.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);

            });
            V702_HVAC_Paremset._btns.FindAll(a => a.ID != e.Message).ForEach(b =>
            {
                b.IsReplication = true;
                ((ButtonStyle)b.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);

            });

            _btns[e.Message].IsReplication = false;

            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + e.Message, 1, 0);
            append_postCmd(CmdType.SetBoolValue, V702_HVAC_Paremset._currentStateID , 0, 0);
            V702_HVAC_Paremset._currentStateID = UIObj.OutBoolList[0] + e.Message;
        }
        #endregion

        private void paint_Frame(Graphics dcGs)
        {
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
        }
    }
}