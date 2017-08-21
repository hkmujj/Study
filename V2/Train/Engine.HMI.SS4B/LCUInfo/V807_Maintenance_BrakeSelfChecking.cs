/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：维护-No.7-制动自检
 *
 *-------------------------------------------------------------------------------------------------*/

using ES.JCTMS.Common;
using ES.JCTMS.Common.Control;
using ES.JCTMS.Common.Control.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using SS4B_TMS.Common;
using SS4B_TMS.Resource;
using System.Collections.Generic;
using System.Drawing;

namespace SS4B_TMS.LCUInfo
{
    /// <summary>
    ///     功能描述：维护-No.7-制动自检
    ///     创建人：lih
    ///     创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V807MaintenanceBrakeSelfChecking : baseClass
    {
        /// <summary>
        ///     界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            //_listbox.Paint(dcGs);
            //_btn.Paint(dcGs);
            //base.paint(dcGs);
        }

        private Button m_Btn;
        private string m_BtnStr;
        private ListBox<BrakeCheckInfo> m_Listbox;
        private string[] m_ListStr;

        /// <summary>
        ///     获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "维护-制动自检";
        }

        /// <summary>
        ///     获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        /// <summary>
        ///     初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_ListStr = new string[2] { "单元一", "单元二" };
            m_BtnStr = "自检";
            m_Listbox = new ListBox<BrakeCheckInfo>(new RectangleF(227, 213, 345, 103), new List<BrakeCheckInfo>(),
                new ListBoxHeader
                {
                    Text = m_ListStr[0],
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14, FontStyle.Regular),
                    Height = 40,
                    Width = 170,
                    BackgroundBrush = Brushes.Transparent,
                    TProperty = "UnitOne",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                },
                new ListBoxHeader
                {
                    Text = m_ListStr[1],
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    Height = 40,
                    Width = 172,
                    BackgroundBrush = Brushes.Transparent,
                    TProperty = "UnitTwo",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                }
                );
            m_Listbox.BackgroundBrush = Brushes.Transparent;
            m_Listbox.GridBrush = Brushes.Black;
            m_Listbox.RowCount = 1;

            var brakeCheckInfo = new BrakeCheckInfo();
            m_Listbox.DataList.Add(brakeCheckInfo);

            var btnStyles = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_16_CC_B,
                Background = ImageResource.Button_NoSelected,
                DownImage = ImageResource.Button_Selected
            };
            m_Btn = new Button(m_BtnStr, new Rectangle(357, 339, 84, 45), (int)(ViewState.MtBscCheckBtn), btnStyles,
                true);
            m_Btn.ClickEvent += _btn_ClickEvent;

            return true;
        }

        /// <summary>
        ///     mouseDown
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            //判断点击的位置是否属于该button集合
            //if ((nPoint.X >= this._btn.Rect.X)
            //       && (nPoint.X <= this._btn.Rect.X + this._btn.Rect.Width)
            //       && (nPoint.Y >= this._btn.Rect.Y)
            //       && (nPoint.Y <= this._btn.Rect.Y + this._btn.Rect.Height))
            //{
            //    _btn.MouseDown(nPoint);
            //}

            return base.mouseDown(nPoint);
        }

        /// <summary>
        ///     mouseUp
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            //if ((nPoint.X >= this._btn.Rect.X)
            //       && (nPoint.X <= this._btn.Rect.X + this._btn.Rect.Width)
            //       && (nPoint.Y >= this._btn.Rect.Y)
            //       && (nPoint.Y <= this._btn.Rect.Y + this._btn.Rect.Height))
            //{
            //    _btn.MouseUp(nPoint);
            //}

            return base.mouseUp(nPoint);
        }

        /// <summary>
        ///     点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
        }
    }
}