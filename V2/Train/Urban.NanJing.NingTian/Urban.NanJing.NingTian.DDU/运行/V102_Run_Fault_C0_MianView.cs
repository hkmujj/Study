#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-21
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图102-运行-故障-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using Urban.NanJing.NingTian.DDU.Common;
using Urban.NanJing.NingTian.DDU.公共组件;

namespace Urban.NanJing.NingTian.DDU.运行
{
    /// <summary>
    /// 功能描述：视图102-运行-故障-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-21
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V102_Run_Fault_C0_MianView : baseClass
    {
        #region 私有变量
        private ListBox<FaultInfo> _listBox;//列表控件
        private List<Image> _resource_Image = new List<Image>();//图片资源
        private List<Button> _btn_Fault = new List<Button>();//故障操作按钮
        private List<Button> _btn_L_R = new List<Button>();//上下翻页按钮列表
        private bool _isOperate = false;//是否操作
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
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            //右侧菜单按钮
            string[] str = new string[] { "现存\n故障", "故障\n履历", "操作\n提示" };
            for (int i = 0; i < 3; i++)
            {
                Button btn_fault = new Button(
                    str[i],
                    new RectangleF(710, 116 + i * 77, 78, 61),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES() { Font = new Font("宋体", 19, FontStyle.Bold), StringFormat = FontInfo.SF_CC, TextBrush = Brushs.BlackBrush },
                        Background = _resource_Image[0],
                        DownImage = _resource_Image[1]
                    },
                    false
                    );
                btn_fault.ClickEvent += btn_fault_ClickEvent;

                _btn_Fault.Add(btn_fault);
            }
            _btn_Fault[0].IsReplication = false;

            //上下翻页按钮
            for (int i = 0; i < 2; i++)
            {
                Button btn_L_R = new Button(
                    "",
                    new RectangleF(710, 116 + (i + 3) * 77, 78, 61),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES() { Font = new Font("宋体", 19, FontStyle.Bold), StringFormat = FontInfo.SF_CC, TextBrush = Brushs.BlackBrush },
                        Background = _resource_Image[2 + i * 2],
                        DownImage = _resource_Image[3 + i * 2]
                    }
                    );
                btn_L_R.ClickEvent += btn_L_R_ClickEvent;

                _btn_L_R.Add(btn_L_R);
            }

            //故障列表
            _listBox = new ListBox<FaultInfo>(new RectangleF(3, 108, 698, 385), VC_C4_GetFault.CurrentFaults,
                new ListBoxHeader() { Text = "No", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("黑体", 11), DataFont = new Font("宋体", 11, FontStyle.Bold), BackgroundBrush = Brushs.BlackBrush, Height = 25, Width = 27, TProperty = "Code", SF_Data = FontInfo.SF_LC, SF_Header = FontInfo.SF_CC, IsCount = true, IsCountUp = true },
                new ListBoxHeader() { Text = "等级", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("黑体", 11), DataFont = new Font("宋体", 10.5f), BackgroundBrush = Brushs.BlackBrush, Height = 25, Width = 43, TProperty = "Grade", SF_Data = FontInfo.SF_LC, SF_Header = FontInfo.SF_CC },
                new ListBoxHeader() { Text = "代码", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("黑体", 11), DataFont = new Font("宋体", 10.5f), BackgroundBrush = Brushs.BlackBrush, Height = 25, Width = 65, TProperty = "Code", SF_Data = FontInfo.SF_LC, SF_Header = FontInfo.SF_CC },
                new ListBoxHeader() { Text = "故障内容", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("黑体", 11), DataFont = new Font("宋体", 10.5f), BackgroundBrush = Brushs.BlackBrush, Height = 25, Width = 395, TProperty = "Display", SF_Data = FontInfo.SF_LC, SF_Header = FontInfo.SF_CC },
                new ListBoxHeader() { Text = "时间", TextBrush = Brushs.WhiteBrush, HeaderFont = new Font("黑体", 11), DataFont = new Font("Arial", 9f), BackgroundBrush = Brushs.BlackBrush, Height = 25, Width = 160, TProperty = "Time", SF_Data = FontInfo.SF_LC, SF_Header = FontInfo.SF_CC }
                );
            _listBox.RowCount = 10;

            return true;
        }
        #endregion

        #region 鼠标事件
        public override bool mouseUp(Point nPoint)
        {
            _btn_Fault.ForEach(a => a.MouseUp(nPoint));
            _btn_L_R.ForEach(a => a.MouseUp(nPoint));

            return base.mouseUp(nPoint);
        }

        public override bool mouseDown(Point nPoint)
        {
            _btn_Fault.ForEach(a => a.MouseDown(nPoint));
            _btn_L_R.ForEach(a => a.MouseDown(nPoint));

            return base.mouseDown(nPoint);
        }

        /// <summary>
        /// 上下翻页按钮点击事件响应函数：实现上下翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_L_R_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (_isOperate != false)
                return;

            if (e.Message == 0) _listBox.NextPage();
            else if (e.Message == 1) _listBox.LastPage();
        }

        private int _currentFaultState = 0;
        /// <summary>
        /// 右边菜单按钮上4个按钮鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_fault_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (_currentFaultState == e.Message)
            {
                _btn_Fault[e.Message].IsReplication = false; 
                return;
            }

            _btn_Fault[_currentFaultState].IsReplication = true;
            _btn_Fault[e.Message].IsReplication = false;
            _currentFaultState = e.Message;

            switch (e.Message)
            {
                case 0://现存故障
                    _listBox.DataList = VC_C4_GetFault.CurrentFaults;
                    List<FaultInfo> v = (from c in VC_C4_GetFault.CurrentFaults orderby c.StartTime select c).ToList();
                    VC_C4_GetFault.CurrentFaults.Clear();
                    for (int i = 0; i < v.Count; i++)
                    {
                        VC_C4_GetFault.CurrentFaults.Add(v[i]);
                    }
                    _isOperate = false;
                    break;
                case 1://故障履历
                    _listBox.DataList = VC_C4_GetFault.AllFaults;
                    _isOperate = false;
                    break;
                case 2://操作提示
                    _isOperate = true;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics g)
        {
            //绘制列表框
            for (int i = 0; i < VC_C4_GetFault.CurrentFaults.Count; i++)
            {
                if (VC_C4_GetFault.CurrentFault != null && VC_C4_GetFault.CurrentFaults[i].Logic == VC_C4_GetFault.CurrentFault.Logic)
                    _listBox.SelectedIndex = i;
            }
            _btn_Fault[2].Enable = _listBox.IsCurrentPageItemSelected;

            _btn_Fault.ForEach(a => a.Paint(g));
            _btn_L_R.ForEach(a => a.Paint(g));

            if (!_isOperate)
            {
                _listBox.Paint(g);
            }
            else
            {
                paint_Operation(g);
            }

            base.paint(g);
        }

        /// <summary>
        /// 绘制操作提示界面
        /// </summary>
        /// <param name="g"></param>
        private void paint_Operation(Graphics g)
        {
            if (!_isOperate)
                return;

            g.FillRectangle(Brushes.Black, new Rectangle(3, 108, 698, 423));
            g.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(36, 121, 650, 74));
            g.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(36, 202, 650, 325));
            g.DrawString(VC_C4_GetFault.CurrentFault.Display, new Font("宋体", 15), Brushs.WhiteBrush, new RectangleF(36, 121, 650, 74), FontInfo.SF_LC);
            g.DrawString(VC_C4_GetFault.CurrentFault.PointOut, new Font("宋体", 13), Brushs.WhiteBrush, new RectangleF(36, 202 + 30, 650, 325), FontInfo.SF_LT);
        }
        #endregion
    }
}
