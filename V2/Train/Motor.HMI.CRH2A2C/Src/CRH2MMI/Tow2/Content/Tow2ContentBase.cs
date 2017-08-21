using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Util;

namespace CRH2MMI.Tow2.Content
{
    abstract class Tow2ContentBase : CommonInnerControlBase
    {

        protected List<CRH2Button> m_CarBtns;
        protected Tow2Resource m_Resource;
        protected List<GDIRectText> m_CarInfos;
        protected CRH2Button m_SelectedCarBtn;

        protected Tow2ContentBase(Tow2Resource resource)
        {
            m_Resource = resource;
        }

        public void Initalize()
        {
            InitTextInfo();

            InitalizeButtons();
        }

        public override void OnDraw(Graphics g)
        {
            m_CarInfos.ForEach(e => e.OnPaint(g));

            m_CarBtns.ForEach(e => e.OnDraw(g));
        }


        protected abstract void InitalizeButtons();

        protected void OnCarButtonDownEvent(object sender, EventArgs eventArgs)
        {
            ReselectCarBtn(sender as CRH2Button);
        }

        public void ReselectCarBtn()
        {
            ReselectCarBtn(m_CarBtns.First());
        }

        protected virtual void ReselectCarBtn(CRH2Button btn)
        {
            if (m_SelectedCarBtn != null)
            {
                m_SelectedCarBtn.CurrentMouseState = MouseState.MouseUp;
            }
            m_SelectedCarBtn = btn;
            Debug.Assert(m_SelectedCarBtn != null, "m_SelectedCarBtn != null");
            m_SelectedCarBtn.CurrentMouseState = MouseState.MouseDown;
        }

        private void InitTextInfo()
        {
            m_CarInfos = new List<GDIRectText>();

            for (int i = 0; i < Tow2Resource.Tow2NameInfoList.Count; i += 2)
            {
                var offset = i / 2;
                var format = new StringFormat()
                {
                    FormatFlags = StringFormatFlags.DirectionRightToLeft,
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Near
                };
                var text = new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(36 + offset * 37, 114, 22, 186),
                    BkColor = Color.WhiteSmoke,
                    TextColor = Color.Black,
                    TextFormat = new StringFormat(format),
                    Text = Tow2Resource.Tow2NameInfoList[i].Name,
                };

                var text1 = new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(36 + offset * 37, 325, 22, 186),
                    BkColor = Color.WhiteSmoke,
                    TextColor = Color.Black,
                    TextFormat = new StringFormat(format),
                    Text = Tow2Resource.Tow2NameInfoList[i + 1].Name,
                };

                m_CarInfos.Add(text);
                m_CarInfos.Add(text1);
            }

            m_CarInfos.RemoveAll(r => string.IsNullOrEmpty(r.Text));

            for (var i = 0; i < m_CarInfos.Count; i++)
            {
                var idx = i;
                m_CarInfos[i].RefreshAction = o => OnRefreshAction(o, idx);
            }
        }

        private void OnRefreshAction(object o, int idx)
        {
            var txt = (GDIRectText)o;
            txt.BkColor = m_Resource.GetStateOf(m_SelectedCarBtn.Text, m_CarBtns.IndexOf(m_SelectedCarBtn), idx)
                    ? Tow2Resource.Tow2FinalNameInfoList[idx].ActiveColor
                    : CRH2Resource.DeactiveBkColor;
        }

        public override bool OnMouseDown(Point point)
        {
            return m_CarBtns.Any(a => a.OnMouseDown(point));
        }
    }
}
