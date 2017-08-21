using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using CRH2MMI.Config.ConfigModel;
using MMI.Facility.Interface.Attribute;

namespace CRH2MMI.Marshing
{
    [GksDataType(DataType.isMMIObjectClass)]
    class ConnectHoodInfo : CRH2BaseClass
    {
        private static StringFormat m_Format = new StringFormat()
            {
                LineAlignment = StringAlignment.Near,
                Alignment = StringAlignment.Center
            };
        private ConnectHoodInfoConfig m_Config;
        private List<CommonInnerControlBase> m_Collection;
        public override bool Init()
        {
            m_Config = GlobalInfo.CurrentCRH2Config.ConnectHoodInfoConfig;
            if (m_Config == null)
            {
                return true;
            }
            m_Collection = new List<CommonInnerControlBase>();

            InitData();
            return true;
        }

        void InitData()
        {
            foreach (var info in m_Config.ConnectHoodInfo)
            {
                m_Collection.Add(new GDIConnectHood()
                               {
                                   TitleConnect = GetTitleControl(info),

                                   Connect = GetContentControl(info.InnerInfo),
                                   OutLineRectangle = info.Rectangle,

                               });
            }
        }

        static CommonInnerControlBase GetTitleControl(CarConnectInfo info)
        {
            var tem = new GDIRectText()
            {
                Text = info.TitleName,
                TextBrush = CRH2Resource.WhiteBrush,
                DrawFont = CRH2Resource.Font14,
                TextFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
                NeedDarwOutline = false,
                BackColorVisible = true,
                BkColor = Color.Black,
                OutLineRectangle = new Rectangle(info.X + info.Width / 6, info.Y - CRH2Resource.Font14.Height / 2, info.Width / 3, CRH2Resource.Font14.Height)
            };
            return tem;
        }
        List<CommonInnerControlBase> GetContentControl(IEnumerable<ContentInfo> info)
        {
            return info.Select(contentInfo => new GDIRectText()
            {
                Text = contentInfo.Text,
                Tag = new object[] { contentInfo.InBoolName, contentInfo.Colors },
                OutLineRectangle = contentInfo.Rectangle,
                NeedDarwOutline = false,
                TextBrush = CRH2Resource.BlackBrush,
                BkColor = contentInfo.Colors[0],
                BackColorVisible = true,
                TextFormat = m_Format,
                DrawFont = CRH2Resource.Font16,
                RefreshAction = o => RefreshItem((GDIRectText)o)

            }).Cast<CommonInnerControlBase>().ToList();
        }

        void RefreshItem(GDIRectText item)
        {
            var InboolName = ((object[])item.Tag)[0].ToString();
            var colors = (List<Color>)((object[])item.Tag)[1];
            item.BkColor = BoolList[Convert.ToInt32(GlobalResource.Instance.GetInBoolIndex(InboolName))] ? colors[1] : colors[0];
        }

        public override void paint(Graphics dcGs)
        {
            m_Collection.ForEach(f => f.OnPaint(dcGs));
        }
    }
}