#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-4
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图1-运行-No.0-方向列表、列车逻辑、限速条件公共组件
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using NC_TMS.Common;

namespace NC_TMS.运行
{
    /// <summary>
    /// 功能描述：视图1-运行-No.0-方向列表、列车逻辑、限速条件公共组件
    /// 创建人：唐林
    /// 创建时间：2014-7-4
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V1_Run_Common_C0_List : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        private List<Button> _btns = new List<Button>();//按钮列表
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "运行视图-方向列表、列车逻辑、限速条件公共组件";
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns>是否初始化成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            //导入图片
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    this._resource_Image.Add(Image.FromStream(fs));
                }
            });

            //创建按钮
            String[] str = new String[] { "方向\n列表", "列车\n逻辑", "限速\n条件", "旁路信息", "", "", "返回" };
            for (int i = 6; i < 7; i++)
            {
                Button btn = new Button(
                    str[i],
                    new RectangleF(721f, 106.375f + (10.375f + 53) * i, 76, 53),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] }
                    );
                btn.ClickEvent += btn_ClickEvent;
                this._btns.Add(btn);
            }

            return true;
        }
        #endregion

        #region 鼠标事件
        /// <summary>
        /// 组件鼠标按下事件监测函数
        /// </summary>
        /// <param name="nPoint">按下点坐标</param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            this._btns.ForEach(a => a.MouseDown(nPoint));

            return base.mouseDown(nPoint);
        }

        /// <summary>
        /// 组件鼠标弹起事件监测函数
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            this._btns.ForEach(a => a.MouseUp(nPoint));

            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 组件按钮点击事件响应函数：切换到相应的视图
        /// </summary>
        /// <param name="sender">点击事件发生的按钮对象</param>
        /// <param name="e">数据</param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0://方向列表按钮
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.提示信息_方向列表, 0, 0);
                    break;
                case 1://列车逻辑按钮
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.提示信息_列车逻辑, 0, 0);
                    break;
                case 2://限速条件按钮
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.提示信息_限速条件, 0, 0);
                    break;
                case 3://旁路信息按钮
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.提示信息_旁路信息, 0, 0);
                    break;
                case 6://返回按钮
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.运行, 0, 0);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            //线框
            dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(4, 96, 711, 454));
            dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(716, 96, 85, 454));

            //按钮
            this._btns.ForEach(a => a.Paint(dcGs));

            base.paint(dcGs);
        }
        #endregion
    }
}
