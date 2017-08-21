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
    public enum DeviceState
    {
        ALL = 0,
        TCU = 1,
        HVAC = 2,
        PIS = 3,
        ACU = 4,
        DOOR = 5,
        RIOM = 6,
        BCU = 7
    }

    /// <summary>
    /// 功能描述：公共组件-No.1-切换视图按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V803_804_Fault_DeviceSelect : baseClass
    {
        #region 私有变量
        private ViewState _currentViewState;                      //当前视图
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        private List<Button> _btns = new List<Button>();//按钮列表
        private ViewState _lastViewState = ViewState.MianTain_CurrentFaultList;
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

            Button btn_All = new Button(
                    "ALL",
                    new Rectangle(200, 112, 406, 64),
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
                    }
                    );
            btn_All.MouseDownEvent += btn_MouseDownEvent;
            btn_All.ClickEvent += btn_ClickEvent;
            _btns.Add(btn_All);

            String[] _strs = new[] { "TCU", "HVAC", "PIS", "ACU", "DOOR", "RIOM", "BCU" };
            for (int i = 0; i < 7; i++)
            {
                Button btn = new Button(
                    _strs[i],
                    new Rectangle(79 + i % 3 * 242, 217 + i / 3 * 105, 163, 64),
                    i + 1,
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
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
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

            switch (_lastViewState)
            {
                case ViewState.MianTain_CurrentFaultList:
                    V803_MianTain_CurrentFaultList.CurrentDeviceState = (DeviceState)e.Message;
                    break;
                case ViewState.MianTain_HistoryFaultList:
                    //V804_MianTain_HistoryFaultList
                    break;
            }
            append_postCmd(CmdType.ChangePage, (int)_lastViewState, 0, 0);
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            _btns.ToList().ForEach(a => a.Paint(dcGs));

            dcGs.FillRectangle(
                new SolidBrush(Color.FromArgb(255,255,0)),
                new Rectangle(0,0,800,70)
                );
            dcGs.DrawString(
                "Device Select",
                new Font("Verdana", 15),
                Brushes.Black,
                new RectangleF(0, 0, 800, 70),
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );

            base.paint(dcGs);
        }
        #endregion
    }
}
