using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls.Grid.Items;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Models;
using CRH2MMI.Common.Util;
using CRH2MMI.WorkState;
using CommonUtil.Controls;
using CommonUtil.Controls.Grid;
using CommonUtil.Controls.Grid.GridLine;

namespace CRH2MMI.BrakeInfo
{
    abstract class BrakeInfoPageBase : CommonInnerControlBase
    {
        public string PageName { protected set; get; }

        // ReSharper disable once InconsistentNaming
        protected BrakeInfo m_BrakeInfo;

        /// <summary>
        /// 制动信号
        /// </summary>
        // ReSharper disable once InconsistentNaming
        protected GridLine m_BrakeSigal;

        /// <summary>
        /// 固定信息
        /// </summary>
        // ReSharper disable once InconsistentNaming
        protected List<CommonInnerControlBase> m_ConstInfos;

        protected Color TextColor = Color.FromArgb(230, 230, 0);

        protected BrakeInfoPageBase(BrakeInfo brakeInfo)
        {
            m_BrakeInfo = brakeInfo;
            m_ConstInfos = new List<CommonInnerControlBase>();
            GlobalEvent.Instance.ReversalChanged += OnReversalChanged;
        }

        public override void Init()
        {
            var conf = GlobalInfo.CurrentCRH2Config.BrakeInfoConfig.BrakePageConfigs.Find(
                    f => f.PageName == "Base").Grid;
            m_BrakeSigal = GDIGridLineHelper.CreateGridLine(conf, (line, config, arg3) => BrakeSigalAction(line, conf, config, arg3));
            var glInit = new GridLineInitialize() { ViewClass = m_BrakeInfo };
            var conInfo = glInit.CreateTitles(m_BrakeSigal, conf);
            conInfo.ForEach(e =>
            {
                e.DrawFont = new Font(e.DrawFont.FontFamily, 13);
                e.Size = new Size(e.Size.Width + 40, e.Size.Height);
            });
            m_ConstInfos = conInfo.Cast<CommonInnerControlBase>().ToList();
            

        }

        private void BrakeSigalAction(GridLine gridLine,GridConfig gridConfig, GridColumnConfig gridColumnConfig, GridItemBase arg3)
        {
            var txtItem = ((GridTextItem) arg3);
            var txt = txtItem.GetInnerContrl();
            txt.TextColor = TextColor;
            txt.NeedDarwOutline = true;
            
            txtItem.ContentSize = new Size(gridConfig.TextWidth, gridConfig.TextHeight);

            if (gridColumnConfig.InBoolColoumNames.Any())
            {
                txt.Text = "耐雪";
                txt.RefreshAction = o =>
                {
                    var ctl = (GDIRectText) o;
                    ctl.Visible = m_BrakeInfo.GetInBoolValue(gridColumnConfig.InBoolColoumNames.First());
                };
            }
            else
            {
                txt.RefreshAction = o =>
                {
                    var ctl = (GDIRectText)o;
                    ctl.Text = WorkStateResource.Instance.GetLevel();
                };
            }
        }

        protected virtual void OnReversalChanged(object sender, EventArgs eventArgs)
        {
            m_BrakeSigal.Reverse();
        }

        public override void OnPaint(Graphics g)
        {
            m_ConstInfos.ForEach(e => e.OnDraw(g));

            m_BrakeSigal.OnPaint(g);
        }
    }
}
