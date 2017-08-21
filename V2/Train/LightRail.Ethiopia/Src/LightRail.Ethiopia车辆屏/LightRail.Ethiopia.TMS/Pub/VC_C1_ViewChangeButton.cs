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
using LightRail.Ethiopia.TMS.Common;
using LightRail.Ethiopia.TMS.Control;
using LightRail.Ethiopia.TMS.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace LightRail.Ethiopia.TMS.Pub
{
    /// <summary>
    /// 功能描述：公共组件-No.1-切换视图按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class VC_C1_ViewChangeButton : baseClass
    {
        #region 私有变量
        private ViewState _currentViewState;                      //当前视图
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        private List<Button> _btns_Down_TabView = new List<Button>();//按钮列表
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

            String[] _strs_Btn_TabView = new String[8] { "Main", "Net", "TCU", "ACU", "BCU", "PIS", "HVAC", "Maintain" };
            for (int i = 0; i < 8; i++)
            {
                Button btn = new Button(
                    _strs_Btn_TabView[i],
                    new Rectangle(5 + i * 99, 558, 90, 40),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold), 
                            TextBrush = new SolidBrush(Color.Yellow), 
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center,LineAlignment = StringAlignment.Center}
                        },
                        Background = _resource_Image[0],
                        DownImage = _resource_Image[1]
                    },
                    false
                    );
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns_Down_TabView.Add(btn);
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
                _currentViewState = (ViewState)nParaB;
                switch (_currentViewState)
                {
                    default:
                        if (_btns_Down_TabView.Find(a => a.ID == nParaB - 1) != null)
                        {
                            _btns_Down_TabView.Find(a => a.ID == nParaB - 1).IsReplication = false;
                            ((ButtonStyle)_btns_Down_TabView.Find(a => a.ID == nParaB - 1).Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
                        }
                        _btns_Down_TabView.FindAll(a => a.ID != nParaB - 1).ForEach(b =>
                        {
                            b.IsReplication = true;
                            ((ButtonStyle)b.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);
                        });
                        break;
                }
            }
        }
        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            _btns_Down_TabView.ToList().ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btns_Down_TabView.ToList().ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 菜单切换按钮鼠标按下事件响应函数：改变按钮字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            ((ButtonStyle)_btns_Down_TabView[e.Message].Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
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
                (_btns_Down_TabView.Find(a => a.ID == e.Message)).IsReplication = false;
                return;
            }//点击的为同一个视图，函数返回

            switch ((ViewState)(e.Message + 1))
            {
                case ViewState.HVAC://根据当前视图跳转到相应的帮助视图
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, 701, 0, 0);
                    break;
                case ViewState.MianTain://根据当前视图跳转到相应的帮助视图
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, 801, 0, 0);
                    break;
                default://切换到相应视图
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, e.Message + 1, 0, 0);
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(0, 552), new PointF(800, 552));
            _btns_Down_TabView.ToList().ForEach(a => a.Paint(dcGs));
            base.paint(dcGs);
        }
        #endregion
    }
}
