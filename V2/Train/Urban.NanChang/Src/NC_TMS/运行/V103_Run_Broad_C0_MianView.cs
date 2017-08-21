#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-4
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图103-运行-紧急广播-No.0-主界面
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

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图103-运行-紧急广播-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-4
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V103_Run_Broad_C0_MianView : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片列表
        private List<Button> _btns_ = new List<Button>();//紧急广播按钮列表
        private Button _btn_Return;//返回按钮
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "运行（紧急广播）试图-紧急广播";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    this._resource_Image.Add(Image.FromStream(fs));
                }
            });

            //目前只有3个功能
            String[] strs = new String[] { "单次紧急广播有效", "重复紧急广播", "退出紧急广播" };
            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button(
                    strs[i],
                    new RectangleF(2 + i * 268, 104, 255, 38),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] }
                    );
                btn.ClickEvent += btn_ClickEvent;
                this._btns_.Add(btn);
            }

            for (int i = 0; i < 26; i++)
            {
                Button btn = new Button(
                     "NAME",
                     new RectangleF(2 + (i % 3) * 268, 104 + 44.22F + (i / 3) * 44.22f, 255, 38),
                     i,
                     new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] }
                     );
                this._btns_.Add(btn);
            }

            this._btn_Return = new Button(
                "返回",
                new RectangleF(569, 502, 201, 38),
                0,
                new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] }
                );
            this._btn_Return.ClickEvent += _btn_Return_ClickEvent;
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
            this._btns_.ForEach(a => a.MouseDown(nPoint));
            this._btn_Return.MouseDown(nPoint);
            return base.mouseDown(nPoint);
        }

        /// <summary>
        /// 组件鼠标弹起事件监测函数
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            this._btns_.ForEach(a => a.MouseUp(nPoint));
            this._btn_Return.MouseUp(nPoint);
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 返回按钮点击事件响应函数：返回主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _btn_Return_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, 1, 0, 0);
        }

        /// <summary>
        /// 紧急广播按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {

        }
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            this.paint_Frame(dcGs);
            this._btns_.ForEach(a => a.Paint(dcGs));
            this._btn_Return.Paint(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 线框与背景绘制
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new Point(0, 96), new Point(800, 96));
            dcGs.FillRectangle(Brushs.AshenBrush, new Rectangle(2, 97, 790, 450));
        }
        #endregion
    }
}
