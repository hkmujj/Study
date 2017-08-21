/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：维护-No.6-加速度测试
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
    ///     功能描述：维护-No.6-加速度测试
    ///     创建人：lih
    ///     创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V806MaintenanceAccelerationTest : baseClass
    {
        /// <summary>
        ///     界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            //_listbox.Paint(dcGs);

            //for (i = 0; i < _btns.Length; i++)
            //{
            //    _btns[i].Paint(dcGs);
            //}

            //base.paint(dcGs);
        }

        private Button[] m_Btns;
        private string[] m_BtnStrs;
        private ListBox<AccelerationTestInfo> m_Listbox;
        private Rectangle[] m_BtnRects;
        private int m_I;
        private string[] m_ListStrs;

        /// <summary>
        ///     获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "维护-加速度测试";
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
            m_BtnStrs = new string[5] { "初始化", "20", "40", "60", "80" };
            m_ListStrs = new string[8] { "类别", "初速度", "末速度", "距离", "时间", "加速度", "牵引", "制动" };
            m_Btns = new Button[5];

            var btnStyles = new ButtonStyle
            {
                FontStyle = FontStyles.FS_Song_16_CC_B,
                Background = ImageResource.Button_NoSelected,
                DownImage = ImageResource.Button_Selected
            };

            m_BtnRects = new Rectangle[5];
            m_BtnRects[0] = new Rectangle(172, 369, 84, 50);
            m_BtnRects[1] = new Rectangle(280, 369, 84, 50);
            m_BtnRects[2] = new Rectangle(369, 369, 84, 50);
            m_BtnRects[3] = new Rectangle(458, 369, 84, 50);
            m_BtnRects[4] = new Rectangle(547, 369, 84, 50);

            for (m_I = 0; m_I < m_Btns.Length; m_I++)
            {
                m_Btns[m_I] = new Button(m_BtnStrs[m_I], m_BtnRects[m_I], (int)(ViewState.MtAtInitalBtn + m_I), btnStyles, true);
                m_Btns[m_I].ClickEvent += V806_Maintenance_AccelerationTest_ClickEvent;
            }

            m_Listbox = new ListBox<AccelerationTestInfo>(new RectangleF(25, 152, 728, 186),
                new List<AccelerationTestInfo>(),
                new ListBoxHeader
                {
                    Text = m_ListStrs[0],
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14, FontStyle.Regular),
                    Height = 40,
                    Width = 120,
                    BackgroundBrush = Brushes.Transparent,
                    TProperty = "Style",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                },
                new ListBoxHeader
                {
                    Text = m_ListStrs[1],
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    Height = 40,
                    Width = 120,
                    BackgroundBrush = Brushes.Transparent,
                    TProperty = "InitialSpeed",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                },
                new ListBoxHeader
                {
                    Text = m_ListStrs[2],
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    Height = 40,
                    Width = 120,
                    BackgroundBrush = Brushes.Transparent,
                    TProperty = "EndSpeed",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                },
                new ListBoxHeader
                {
                    Text = m_ListStrs[3],
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    Height = 40,
                    Width = 120,
                    BackgroundBrush = Brushes.Transparent,
                    TProperty = "Distance",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                },
                new ListBoxHeader
                {
                    Text = m_ListStrs[4],
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    Height = 40,
                    Width = 120,
                    BackgroundBrush = Brushes.Transparent,
                    TProperty = "Time",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                },
                new ListBoxHeader
                {
                    Text = m_ListStrs[5],
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    Height = 40,
                    Width = 116,
                    BackgroundBrush = Brushes.Transparent,
                    TProperty = "Acceleration",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                }
                );
            m_Listbox.BackgroundBrush = Brushes.Transparent;
            m_Listbox.GridBrush = Brushes.Black;
            m_Listbox.RowCount = 2;

            var traction = new AccelerationTestInfo { Style = "牵引" };
            var brake = new AccelerationTestInfo { Style = "制动" };

            m_Listbox.DataList.Add(traction);
            m_Listbox.DataList.Add(brake);

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
            //for (i = 0; i < _btns.Length; i++)
            //{
            //    if ((nPoint.X >= this._btns[i].Rect.X)
            //           && (nPoint.X <= this._btns[i].Rect.X + this._btns[i].Rect.Width)
            //           && (nPoint.Y >= this._btns[i].Rect.Y)
            //           && (nPoint.Y <= this._btns[i].Rect.Y + this._btns[i].Rect.Height))
            //    {
            //        _btns[i].MouseDown(nPoint);
            //        break;
            //    }
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
            //for (i = 0; i < _btns.Length; i++)
            //{
            //    if ((nPoint.X >= this._btns[i].Rect.X)
            //           && (nPoint.X <= this._btns[i].Rect.X + this._btns[i].Rect.Width)
            //           && (nPoint.Y >= this._btns[i].Rect.Y)
            //           && (nPoint.Y <= this._btns[i].Rect.Y + this._btns[i].Rect.Height))
            //    {
            //        _btns[i].MouseUp(nPoint);
            //        break;
            //    }
            //}

            return base.mouseUp(nPoint);
        }

        /// <summary>
        ///     点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void V806_Maintenance_AccelerationTest_ClickEvent(object sender, ClickEventArgs<int> e)
        {
        }
    }
}