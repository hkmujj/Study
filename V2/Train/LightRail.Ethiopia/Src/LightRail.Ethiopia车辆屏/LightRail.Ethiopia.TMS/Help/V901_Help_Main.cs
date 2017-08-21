#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-6
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图604-帮助-运行帮助-No.0
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using LightRail.Ethiopia.TMS.Common;
using LightRail.Ethiopia.TMS.Control;
using LightRail.Ethiopia.TMS.Control.Common;
using LightRail.Ethiopia.TMS.Pub;

namespace LightRail.Ethiopia.TMS.Help
{
    /// <summary>
    /// 功能描述：视图604-帮助-运行帮助-No.0
    /// 创建人：唐林
    /// 创建时间：2014-7-6
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V901_Help_Main : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        private List<Button> _btns = new List<Button>();
        private Int32 _currentPage = 0;
        private ViewState _currentViewState;

        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "Help-Mian";
        }


        /// <summary>
        /// 初始化函数：初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex">错误索性</param>
        /// <returns>初始化是否成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            //上下翻页按钮
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    "",
                    new RectangleF(465 + 60 * i, 460, 48, 40),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[2 + 3 * i],
                        DownImage = _resource_Image[3 + 3 * i],
                        DisableImage = _resource_Image[4 + 3 * i]
                    }
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }
            _btns[1].Enable = false;

            //Quit按钮
            Button btn_Quit = new Button(
                    "Quit",
                    new RectangleF(604, 460, 90, 40),
                    2,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[8],
                        DownImage = _resource_Image[9]
                    }
                    );
            btn_Quit.ClickEvent += btn_ClickEvent;
            btn_Quit.MouseDownEvent += btn_Quit_MouseDownEvent;
            _btns.Add(btn_Quit);

            return true;
        }

        void btn_Quit_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            ((ButtonStyle) ((Button) sender).Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
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
            }
        }

        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            if (_currentViewState != ViewState.Main || !VC_C0_Title.IsShowHelpView)
                return;
            dcGs.DrawImage(_resource_Image[_currentPage], new Rectangle(80, 85, 640, 440));
            _btns.ForEach(a => a.Paint(dcGs));

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
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0://向下翻页
                    if (_currentPage == 0) _currentPage = 1;
                    //_currentPage = _currentPage == 0 ? _currentPage + 1 : _currentPage;
                    if (_currentPage == 1)
                    {
                        _btns[0].Enable = false;
                        _btns[1].Enable = true;
                    }
                    break;
                case 1://向上翻页
                    if (_currentPage == 1) _currentPage = 0;
                    //_currentPage = _currentPage == 1 ? 0 : 1;
                    if (_currentPage == 0)
                    {
                        _btns[1].Enable = false;
                        _btns[0].Enable = true;
                    }
                    break;
                case 2://Quit按钮
                    ((ButtonStyle)((Button)sender).Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);
                    VC_C0_Title.IsShowHelpView = false;
                    _currentPage = 0;
                    _btns[1].Enable = false;
                    _btns[0].Enable = true;
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
