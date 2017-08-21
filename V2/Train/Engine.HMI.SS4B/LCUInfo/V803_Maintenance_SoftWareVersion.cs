/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：维护-No.3-软件版本
 *
 *-------------------------------------------------------------------------------------------------*/

using ES.JCTMS.Common;
using ES.JCTMS.Common.Control;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using SS4B_TMS.Common;
using System.Collections.Generic;
using System.Drawing;

namespace SS4B_TMS.LCUInfo
{
    /// <summary>
    ///     功能描述：维护-No.3-软件版本
    ///     创建人：lih
    ///     创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V803MaintenanceSoftWareVersion : baseClass
    {
        /// <summary>
        ///     界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            //dcGs.DrawRectangle(_whiteLinePen, _frameRects);
            //dcGs.FillRectangle(Brushs.BlackBrush, _fillRect);
            //_versionInfos.Paint(dcGs);
            //base.paint(dcGs);
        }

        private Pen m_BlackLinePen = new Pen(new SolidBrush(Color.Black), 1.6f);
        private Pen m_WhiteLinePen = new Pen(new SolidBrush(Color.White), 1.6f);
        private Font m_ChineseFont = new Font("宋体", 16);
        private int m_I = 0;
        private Rectangle m_FrameRects;
        private Rectangle m_FillRect;
        private ListBox<SoftwareVersionInfo> m_VersionInfos;

        /// <summary>
        ///     获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "维护-软件版本";
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
            m_FrameRects = new Rectangle(51, 126, 674, 310);
            m_FillRect = new Rectangle(52, 127, 673, 309);

            m_VersionInfos = new ListBox<SoftwareVersionInfo>(new RectangleF(82, 127, 630, 309),
                new List<SoftwareVersionInfo>(),
                new ListBoxHeader
                {
                    Text = "系统",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14, FontStyle.Regular),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 28,
                    Width = 110,
                    TProperty = "SoftwareName",
                    SF_Data = FontInfo.SfLc,
                    SF_Header = FontInfo.SfLc
                },
                new ListBoxHeader
                {
                    Text = "1",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("Arial", 14),
                    DataFont = new Font("Arial", 14f),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 28,
                    Width = 90,
                    TProperty = "Version1",
                    SF_Data = FontInfo.SfLc,
                    SF_Header = FontInfo.SfLc
                },
                new ListBoxHeader
                {
                    Text = "2",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("Arial", 14),
                    DataFont = new Font("Arial", 14f),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 28,
                    Width = 90,
                    TProperty = "Version2",
                    SF_Data = FontInfo.SfLc,
                    SF_Header = FontInfo.SfLc
                },
                new ListBoxHeader
                {
                    Text = "3",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("Arial", 14),
                    DataFont = new Font("Arial", 14f),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 28,
                    Width = 90,
                    TProperty = "Version3",
                    SF_Data = FontInfo.SfLc,
                    SF_Header = FontInfo.SfLc
                },
                new ListBoxHeader
                {
                    Text = "4",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("Arial", 14),
                    DataFont = new Font("Arial", 14f),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 28,
                    Width = 90,
                    TProperty = "Version4",
                    SF_Data = FontInfo.SfLc,
                    SF_Header = FontInfo.SfLc
                },
                new ListBoxHeader
                {
                    Text = "5",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("Arial", 14),
                    DataFont = new Font("Arial", 14f),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 28,
                    Width = 80,
                    TProperty = "Version5",
                    SF_Data = FontInfo.SfLc,
                    SF_Header = FontInfo.SfLc
                },
                new ListBoxHeader
                {
                    Text = "6",
                    TextBrush = Brushs.WhiteBrush,
                    HeaderFont = new Font("Arial", 14),
                    DataFont = new Font("Arial", 14f),
                    BackgroundBrush = Brushs.BlackBrush,
                    Height = 28,
                    Width = 80,
                    TProperty = "Version6",
                    SF_Data = FontInfo.SfLc,
                    SF_Header = FontInfo.SfLc
                }
                );
            m_VersionInfos.RowCount = 8;

            var hfi1 = new SoftwareVersionInfo { SoftwareName = "ACU" };
            var hfi2 = new SoftwareVersionInfo { SoftwareName = "DDU" };
            var hfi3 = new SoftwareVersionInfo { SoftwareName = "PCE" };
            var hfi4 = new SoftwareVersionInfo { SoftwareName = "BCE" };
            var hfi5 = new SoftwareVersionInfo { SoftwareName = "ACE" };
            var hfi6 = new SoftwareVersionInfo { SoftwareName = "PIS" };
            var hfi7 = new SoftwareVersionInfo { SoftwareName = "EDCU" };

            m_VersionInfos.DataList.Add(hfi1);
            m_VersionInfos.DataList.Add(hfi2);
            m_VersionInfos.DataList.Add(hfi3);
            m_VersionInfos.DataList.Add(hfi4);
            m_VersionInfos.DataList.Add(hfi5);
            m_VersionInfos.DataList.Add(hfi6);
            m_VersionInfos.DataList.Add(hfi7);

            return true;
        }
    }
}