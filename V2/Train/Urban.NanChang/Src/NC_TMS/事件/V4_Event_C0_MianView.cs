#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-8
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图4-事件-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using NC_TMS.Common;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图4-事件-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-8
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V4_Event_C0_MianView : baseClass
    {
        #region 私有变量
        private ListBox<FaultInfo> _listBox;//列表控件
        private List<Image> _resource_Image = new List<Image>();//图片资源
        private List<Button> _btn_Fault = new List<Button>();//故障操作按钮
        private List<Button> _btn_L_R = new List<Button>();//上下翻页按钮列表
        private Boolean _isOperate = false;//是否操作
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "事件视图-主界面";
        }

        /// <summary>
        /// 初始化函数：导入图片、创建控件、列表框
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    this._resource_Image.Add(Image.FromStream(fs));
                }
            });

            //右侧菜单按钮
            String[] str = new String[] { "现存\n故障", "分类\n故障", "故障\n履历", "操作\n提示" };
            for (int i = 0; i < 4; i++)
            {
                Button btn_fault = new Button(
                    str[i],
                    new RectangleF(713, 105 + i * 72, 85, 58),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] }
                    );
                btn_fault.ClickEvent += btn_fault_ClickEvent;

                this._btn_Fault.Add(btn_fault);
            }

            //上下翻页按钮
            for (int i = 0; i < 2; i++)
            {
                Button btn_L_R = new Button(
                    "",
                    new RectangleF(713, 105 + (i + 4) * 72, 85, 58),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[2 + i * 2], DownImage = _resource_Image[3 + i * 2]}
                    );
                btn_L_R.ClickEvent += btn_L_R_ClickEvent;

                this._btn_L_R.Add(btn_L_R);
            }

            //故障列表
            this._listBox = new ListBox<FaultInfo>(new RectangleF(9, 105, 707, 443), VC_C4_GetFault.CurrentFaults,
                new ListBoxHeader() { Text = "No", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("宋体", 13, FontStyle.Bold), DataFont = new Font("宋体", 11, FontStyle.Bold), BackgroundBrush = Brushs.GrayBrush, Height = 31, Width = 34, TProperty = "Code", SF_Data = FontInfo.SF_LC, SF_Header = FontInfo.SF_CC },
                new ListBoxHeader() { Text = "等级", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("宋体", 11), DataFont = new Font("宋体", 10.5f), BackgroundBrush = Brushs.GrayBrush, Height = 31, Width = 48, TProperty = "Grade", SF_Data = FontInfo.SF_LC, SF_Header = FontInfo.SF_CC },
                new ListBoxHeader() { Text = "代码", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("宋体", 11), DataFont = new Font("宋体", 10.5f), BackgroundBrush = Brushs.GrayBrush, Height = 31, Width = 64, TProperty = "Code", SF_Data = FontInfo.SF_LC, SF_Header = FontInfo.SF_CC },
                new ListBoxHeader() { Text = "故障内容", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("宋体", 11), DataFont = new Font("宋体", 10.5f), BackgroundBrush = Brushs.GrayBrush, Height = 31, Width = 394, TProperty = "Display", SF_Data = FontInfo.SF_LC, SF_Header = FontInfo.SF_CC },
                new ListBoxHeader() { Text = "时间", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("宋体", 11), DataFont = new Font("宋体", 10.5f), BackgroundBrush = Brushs.GrayBrush, Height = 31, Width = 152, TProperty = "Time", SF_Data = FontInfo.SF_LC, SF_Header = FontInfo.SF_CC }
                );
            this._listBox.RowCount = 10;

            return true;
        }
        #endregion

        #region 鼠标事件
        public override bool mouseUp(Point nPoint)
        {
            this._btn_Fault.ForEach(a => a.MouseUp(nPoint));
            this._btn_L_R.ForEach(a => a.MouseUp(nPoint));
            _listBox.MouseUp(nPoint);

            return base.mouseUp(nPoint);
        }

        public override bool mouseDown(Point nPoint)
        {
            this._btn_Fault.ForEach(a => a.MouseDown(nPoint));
            this._btn_L_R.ForEach(a => a.MouseDown(nPoint));

            return base.mouseDown(nPoint);
        }

        /// <summary>
        /// 上下翻页按钮点击事件响应函数：实现上下翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_L_R_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (_isOperate == false)
                return;

            if (e.Message == 0) this._listBox.NextPage();
            else if (e.Message == 1) this._listBox.LastPage();
        }

        /// <summary>
        /// 右边菜单按钮上4个按钮鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_fault_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0://现存故障
                    this._listBox.DataList = VC_C4_GetFault.CurrentFaults;
                    List<FaultInfo> v = (from c in VC_C4_GetFault.CurrentFaults orderby c.StartTime select c).ToList();
                    VC_C4_GetFault.CurrentFaults.Clear();
                    for (int i = 0; i < v.Count; i++)
                    {
                        VC_C4_GetFault.CurrentFaults.Add(v[i]);
                    }
                    _isOperate = false;
                    break;
                case 1://分类故障
                    this._listBox.DataList = VC_C4_GetFault.CurrentFaults;
                    List<FaultInfo> v1 = (from c in VC_C4_GetFault.CurrentFaults orderby c.Grade select c).ToList();
                    VC_C4_GetFault.CurrentFaults.Clear();
                    for (int i = 0; i < v1.Count; i++)
                    {
                        VC_C4_GetFault.CurrentFaults.Add(v1[i]);
                    }
                    _isOperate = false;
                    break;
                case 2://故障履历
                    this._listBox.DataList = VC_C4_GetFault.AllFaults;
                    _isOperate = false;
                    break;
                case 3://操作提示
                    _isOperate = true;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            //绘制列表框
            //for (int i = 0; i < VC_C4_GetFault.CurrentFaults.Count; i++)
            //{
            //    if (VC_C4_GetFault.CurrentFault != null && VC_C4_GetFault.CurrentFaults[i].Logic == VC_C4_GetFault.CurrentFault.Logic)
            //        this._listBox.SelectedIndex = i;
            //}

            if (this._listBox.SelectedIndex == -1)
            {
                if (VC_C4_GetFault.CurrentFaults != null && VC_C4_GetFault.CurrentFaults.Count != 0)
                {
                    this._listBox.SelectedIndex = 0;
                }
            }

            this._btn_Fault[3].Enable = this._listBox.IsCurrentPageItemSelected;
            this.paint_Frame(dcGs);

            this._btn_Fault.ForEach(a => a.Paint(dcGs));
            this._btn_L_R.ForEach(a => a.Paint(dcGs));

            if (!this._isOperate)
            {
                this._listBox.Paint(dcGs);
            }
            else
            {
                this.paint_Operation(dcGs);
            }

            base.paint(dcGs);
        }

        /// <summary>
        /// 事件视图的线框
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new PointF(5, 96), new PointF(5, 550));
            dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new PointF(5, 550), new PointF(710, 550));
            dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new PointF(710, 550), new PointF(710, 96));
        }

        /// <summary>
        /// 绘制操作提示界面
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Operation(Graphics dcGs)
        {
            if (!this._isOperate)
                return;

            dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(36, 121, 650, 74));
            dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(36, 202, 650, 325));
            dcGs.DrawString(VC_C4_GetFault.CurrentFault.Display, new Font("宋体", 15), Brushs.WhiteBrush, new RectangleF(36, 121, 650, 74), FontInfo.SF_LC);
            dcGs.DrawString(VC_C4_GetFault.CurrentFault.PointOut, new Font("宋体", 13), Brushs.WhiteBrush, new RectangleF(36, 202 + 30, 650, 325), FontInfo.SF_LT);
        }
        #endregion
    }
}
