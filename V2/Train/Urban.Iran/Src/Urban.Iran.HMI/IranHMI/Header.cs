using System;
using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Header : HMIBase
    {
        private readonly Rectangle m_StartRect;
        private readonly Rectangle m_MidRect;
        private readonly Rectangle m_EndRect;
        private readonly StringFormat m_StrFormat;
        private Bitmap m_HeadBitmap;
        private Bitmap m_ArrowBitmap;
        private Bitmap m_ArrowToBitmap;
        private readonly Rectangle m_ArrowRect;
        private readonly Rectangle m_ArrowRectTo;
        private string m_Title;
        private readonly Rectangle m_TitleRectangle;
        private readonly StringFormat m_TitleFormat = new StringFormat { Alignment = StringAlignment.Center };
        private List<Rectangle> m_Rec;

        public Header()
        {
            m_StartRect = new Rectangle(120, 13, 175, 22);
            m_MidRect = new Rectangle(335, 13, 175, 22);
            m_EndRect = new Rectangle(540, 13, 175, 22);

            m_ArrowRect = new Rectangle(280, 13, 45, 22);
            m_ArrowRectTo = new Rectangle(495, 13, 45, 22);

            m_TitleRectangle = new Rectangle(90, 40, 600, 30);

            m_StrFormat = new StringFormat { LineAlignment = StringAlignment.Center };

            m_Title = GlobleParam.ResManager.GetString("String1");
        }

        public override string GetInfo()
        {
            return "标题栏";
        }

        private List<Line> allLine;
        protected override bool Initalize()
        {
            m_HeadBitmap = new Bitmap(RecPath + "\\frame/head.jpg");
            m_ArrowBitmap = new Bitmap(RecPath + "\\frame/arrow.jpg");
            m_ArrowToBitmap = new Bitmap(RecPath + "\\frame/arrowTo.jpg");
            m_Rec = new List<Rectangle> { new Rectangle(655, 5, 130, 30), new Rectangle(655, 35, 130, 30) };
            allLine = new List<Line>();
            for (int i = 0; i < 800;)
            {
                allLine.Add(new Line(new Point(i, 0), new Point(i, 200)));

                i += 10;
            }
            return true;
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(m_HeadBitmap, 0, 0, 800, 65);

            g.DrawString(DateTime.Now.ToLongTimeString(), GdiCommon.Txt20Font, GdiCommon.OceanBlueBrush, new Point(8, 7));
            m_StrFormat.Alignment = StringAlignment.Far;
            g.DrawString(FloatList[1].ToString("0 km/h"), GdiCommon.Txt15Font, GdiCommon.OceanBlueBrush, m_Rec[0],
                m_StrFormat);
            g.DrawString(FloatList[3].ToString("0 V"), GdiCommon.Txt15Font, GdiCommon.OceanBlueBrush, m_Rec[1],
                m_StrFormat);

            g.DrawImage(m_ArrowBitmap, m_ArrowRect);
            g.DrawImage(m_ArrowToBitmap, m_ArrowRectTo);

            GetViewTitle(GlobleParam.Instance.CurrentIranViewIndex);
            g.DrawString(m_Title, GdiCommon.Txt15Font, GdiCommon.MediumGreyBrush, m_TitleRectangle, m_TitleFormat);

            m_StrFormat.Alignment = StringAlignment.Center;
            g.DrawString(
                GlobleParam.Instance.StationList.ContainsKey(Convert.ToInt32(FloatList[GlobleParam.Instance.InFloatDictionary["当前站"]]))
                    ? GlobleParam.Instance.StationList[Convert.ToInt32(FloatList[GlobleParam.Instance.InFloatDictionary["当前站"]])]
                    : "Ei Goli", GdiCommon.Txt12Font, GdiCommon.OceanBlueBrush, m_StartRect, m_StrFormat);
            m_StrFormat.Alignment = StringAlignment.Center;
            g.DrawString(
                GlobleParam.Instance.StationList.ContainsKey(Convert.ToInt32(FloatList[GlobleParam.Instance.InFloatDictionary["下一站"]]))
                    ? GlobleParam.Instance.StationList[Convert.ToInt32(FloatList[GlobleParam.Instance.InFloatDictionary["下一站"]])]
                    : "Ei Goli", GdiCommon.Txt12Font, GdiCommon.OceanBlueBrush, m_MidRect, m_StrFormat);
            m_StrFormat.Alignment = StringAlignment.Center;
            g.DrawString(
                GlobleParam.Instance.StationList.ContainsKey(Convert.ToInt32(FloatList[GlobleParam.Instance.InFloatDictionary["终点站"]]))
                    ? GlobleParam.Instance.StationList[Convert.ToInt32(FloatList[GlobleParam.Instance.InFloatDictionary["终点站"]])]
                    : "", GdiCommon.Txt12Font, GdiCommon.OceanBlueBrush, m_EndRect, m_StrFormat);
            //allLine.ForEach(f => f.OnDraw(g));

        }

        protected override void SetNavigateValue(NavigateType naviType, IranViewIndex currentView, IranViewIndex lastView)
        {
            GlobleParam.Instance.CurrentIranViewIndex = (IranViewIndex)currentView;
        }

        private void GetViewTitle(IranViewIndex view)
        {
            var iranViewIndex = (int)view;
            m_Title = GlobleParam.ResManagerText.GetString("Title" + iranViewIndex.ToString("0000")).Equals("")
                ? m_Title
                : GlobleParam.ResManagerText.GetString("Title" + iranViewIndex.ToString("0000"));
        }
    }
}