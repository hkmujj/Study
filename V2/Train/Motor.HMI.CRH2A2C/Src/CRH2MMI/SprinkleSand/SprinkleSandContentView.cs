using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Controls.Grid.GridLine;
using CommonUtil.Util;
using CRH2MMI.Common;
using CRH2MMI.Common.Config;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Screen;
using CRH2MMI.Common.Util;
using CRH2MMI.Common.View;
using CRH2MMI.Extensions;
using MMI.Facility.Interface.Data;

namespace CRH2MMI.SprinkleSand
{
    class SprinkleSandContentView : CommonInnerControlBase, INavigateAware
    {
        private readonly SprinkleSandView m_SrcObj;

        private List<GridLine> m_GridLineCollection;

        private List<CommonInnerControlBase> m_ConstantInfoCollection;

        private CommonSetBtn m_CommonSetBtn;

        private List<CRH2Button> m_ControlBtns;

        private CRH2Button m_CurrentSelectedBtn;

        public SprinkleSandContentView(SprinkleSandView srcObj)
        {
            m_SrcObj = srcObj;
        }

        /// <summary>初始化</summary>
        public override void Init()
        {
            m_ConstantInfoCollection = new List<CommonInnerControlBase>();
            m_GridLineCollection = new List<GridLine>();
            var config = GlobalInfo.CurrentCRH2Config.SprinkleSandConfig;
            if (config == null)
            {
                return;
            }

            var gl = new GridLineInitialize
            {
                InnerTextFormat = {Alignment = StringAlignment.Center},
                ViewClass = m_SrcObj
            };
            foreach (var gridContent in config.GridContents)
            {
                gridContent.RefreshRelation();
                var grid = GDIGridLineHelper.CreateGridLine(gridContent, gl.InitInnerContrl);
                m_GridLineCollection.Add(grid);
                m_ConstantInfoCollection.AddRange(gl.CreateTitles(grid, gridContent));
            }

            m_ConstantInfoCollection.AddRange(config.RemarksConfig.ReflectViews());

            m_ControlBtns = config.ButtonConfigs.Select(s =>
            {
                var btn = s.ReflectButton();
                btn.ButtonDownEvent = OnCtrolButtonDownEvent;
                return btn;
            }).ToList();

            if (config.ButtonConfigs.Any())
            {
                m_CommonSetBtn = new CommonSetBtn
                {
                    SetButtonDown = OnSetButtonDown,
                    SetButtonUp = OnSetButtonUp,
                };

            }
        }

        private void OnCtrolButtonDownEvent(object sender, EventArgs eventArgs)
        {
            var btn = (CRH2Button) sender;
            UpdateSelectedBtn(btn);
        }

        private void OnSetButtonUp(object sender, EventArgs eventArgs)
        {
            SendSelectedValue(0);
            UpdateSelectedBtn(null);
        }

        private void OnSetButtonDown(object sender, EventArgs eventArgs)
        {
            SendSelectedValue(1);
        }

        private void SendSelectedValue(int setValue)
        {
            if (m_CurrentSelectedBtn != null)
            {
                var config = (ButtonConfig) m_CurrentSelectedBtn.Tag;
                m_SrcObj.append_postCmd(CmdType.SetBoolValue, m_SrcObj.GetOutBoolIndex(config.OutBoolColoumNames.First()),
                    setValue, 0);
            }
        }

        /// <summary>鼠标按下</summary>
        /// <param name="point"></param>
        public override bool OnMouseDown(Point point)
        {
            m_ControlBtns.ForEach(e => e.OnMouseDown(point));

            if (m_CommonSetBtn != null && m_CurrentSelectedBtn != null)
            {
                m_CommonSetBtn.OnMouseDown(point);
            }

            return true;
        }

        /// <summary>鼠标按下后弹起</summary>
        /// <param name="point"></param>
        public override bool OnMouseUp(Point point)
        {
            if (m_CommonSetBtn != null)
            {
                m_CommonSetBtn.OnMouseUp(point);
            }
            return true;

        }

        /// <summary>绘图</summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            m_ConstantInfoCollection.ForEach(e => e.OnDraw(g));
            m_GridLineCollection.ForEach(f => f.OnPaint(g));
            if (m_CommonSetBtn != null)
            {
                m_CommonSetBtn.OnDraw(g);
            }
            m_ControlBtns.ForEach(e => e.OnDraw(g));
        }

        public void NavigateInCurrent(ViewConfig current)
        {
            
        }

        public void NavigateTo(ViewConfig toThis)
        {

        }

        public void NavigateFrom(ViewConfig fromOhter)
        {
            UpdateSelectedBtn(null);
        }

        private void UpdateSelectedBtn(CRH2Button btn)
        {
            if (m_CurrentSelectedBtn != btn && m_CurrentSelectedBtn != null)
            {
                m_CurrentSelectedBtn.CurrentMouseState = MouseState.MouseUp;
            }
            m_CurrentSelectedBtn = btn;
            if (m_CurrentSelectedBtn != null)
            {
                m_CurrentSelectedBtn.CurrentMouseState = MouseState.MouseDown;
            }
        }
    }
}