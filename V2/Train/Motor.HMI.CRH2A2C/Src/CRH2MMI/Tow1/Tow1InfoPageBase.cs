using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using CommonUtil.Controls;
using CommonUtil.Controls.Grid.GridLine;

namespace CRH2MMI.Tow1
{
    abstract class Tow1InfoPageBase : CommonInnerControlBase
    {
        public string PageName { protected set; get; }


        // ReSharper disable once InconsistentNaming
        protected TowInfo m_TowInfo;

        // ReSharper disable once InconsistentNaming
        private GridLine m_TowBaseGrid;

        // ReSharper disable once InconsistentNaming
        protected GridLine m_GridLine;

        /// <summary>
        /// 固定信息
        /// </summary>
        // ReSharper disable once InconsistentNaming
        protected List<CommonInnerControlBase> m_ConstInfos;

        protected Color TextColor = Color.FromArgb(230, 230, 0);

        protected Tow1InfoPageBase(TowInfo towInfo)
        {
            m_TowInfo = towInfo;
            m_ConstInfos = new List<CommonInnerControlBase>();
            GlobalEvent.Instance.ReversalChanged += OnReversalChanged;
        }

        public override void Init()
        {

            var conf = GlobalInfo.CurrentCRH2Config.Tow1Config.Tow1PageConfigs.Find(
                    f => f.PageName == "Base").Grid;
            var glInit = new GridLineInitialize()
            {
                ViewClass = m_TowInfo,
                InnerTextFormat =
                    new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }
            };
            m_TowBaseGrid = GDIGridLineHelper.CreateGridLine(conf, glInit.InitInnerContrl);

            var titles = glInit.CreateTitles(m_TowBaseGrid, conf);
            titles.ForEach(e =>
            {
                e.Location = new Point(55, e.Location.Y);
                e.Size = new Size(e.Size.Width + 40, e.Size.Height);
                //e.DrawFont = new Font(e.DrawFont.FontFamily, 9);
            });
            m_ConstInfos = titles.Cast<CommonInnerControlBase>().ToList();
        }

        protected virtual void OnReversalChanged(object sender, EventArgs eventArgs)
        {
            m_GridLine.Reverse();
            m_TowBaseGrid.Reverse();
        }

        public override void OnPaint(Graphics g)
        {
            OnDraw(g);
        }

        public override void OnDraw(Graphics g)
        {
            m_ConstInfos.ForEach(e => e.OnDraw(g));

            m_TowBaseGrid.OnPaint(g);

            m_GridLine.OnPaint(g);
        }
    }
}
