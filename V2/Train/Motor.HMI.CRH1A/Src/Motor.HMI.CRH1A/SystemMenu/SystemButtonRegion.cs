using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CommonUtil.Controls;
using Motor.HMI.CRH1A.Common.View;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.SystemMenu
{
    class SystemButtonRegion : CommonInnerControlBase
    {
        public uint RowCount { private set; get; }

        public uint ColumnCount { private set; get; }

        private CRH1Button[][] m_Buttons;

        private List<CRH1Button> m_ActiveButtons;

        private readonly GT_SystemMenu m_View;

        /// <summary>
        /// 背景
        /// </summary>
        private readonly SolidBrush m_Brush = new SolidBrush(Color.FromArgb(119, 136, 153));//菜单栏背景刷
        /// <summary>
        /// 轮廓
        /// </summary>
        private readonly Pen m_Pen = new Pen(Color.FromArgb(217, 223, 229), 3);

        public SystemButtonRegion(SystemMenuRegion regionConfig, GT_SystemMenu view)
        {
            m_View = view;
            m_OutLineRectangle = regionConfig.Rectangle;

            RowCount = regionConfig.RowCount;
            ColumnCount = regionConfig.ColumnCount;

            InitBtns(regionConfig);
        }

        private void InitBtns(SystemMenuRegion regionConfig)
        {
            m_ActiveButtons = new List<CRH1Button>();
            m_Buttons = new CRH1Button[RowCount][];
            for (int index = 0; index < m_Buttons.Length; index++)
            {
                m_Buttons[index] = new CRH1Button[ColumnCount];
            }

            var actBtnRegion = new Rectangle(m_OutLineRectangle.X + regionConfig.XInterval + regionConfig.XOffset,
                m_OutLineRectangle.Y + regionConfig.YInterval + regionConfig.YOffset,
                m_OutLineRectangle.Width - regionConfig.XInterval * 2 - regionConfig.XOffset,
                m_OutLineRectangle.Height - regionConfig.YInterval * 2 - regionConfig.YOffset);
            var yoffset = (int)(actBtnRegion.Height / RowCount);
            var xoffset = (int)(actBtnRegion.Width / ColumnCount);
            for (int i = 0; i < regionConfig.ButtonConfigs.Count; i++)
            {
                var buttonConfig = regionConfig.ButtonConfigs[i];
                var btn = new CRH1Button()
                          {
                              OutLineRectangle =
                                  new Rectangle(actBtnRegion.X + buttonConfig.ColoumnIndex * xoffset,
                                  actBtnRegion.Y + buttonConfig.RowIndex * yoffset,
                                  regionConfig.ButtonWidth,
                                  regionConfig.ButtonHeight),
                              //Text = buttonConfig.Text,
                              //TextColor = Color.Black,

                              ButtonUpEvent = (sender, args) => m_View.OnPost(CmdType.ChangePage, (int)buttonConfig.Goto, 1, 0),
                          };
                btn.SetButtonText(buttonConfig.Text);
                btn.SetButtonColor(192, 192, 192);
                m_ActiveButtons.Add(btn);
                m_Buttons[buttonConfig.RowIndex][buttonConfig.ColoumnIndex] = btn;
            }
            GlobalInfo.Instance.ButtonStatusChange += () =>
            {
                m_ActiveButtons.ForEach(f => f.IsEnable = GlobalInfo.Instance.ButtonEnable);
                foreach (var button in m_Buttons)
                {
                    foreach (var crh1Button in button.Where(w => w != null))
                    {
                        crh1Button.IsEnable = GlobalInfo.Instance.ButtonEnable;
                    }
                }
            };
        }

        public override void OnDraw(Graphics g)
        {
            g.FillRectangle(m_Brush, m_OutLineRectangle);
            g.DrawRectangle(m_Pen, m_OutLineRectangle);
            m_ActiveButtons.ForEach(e => e.OnDraw(g));
        }

        public override bool OnMouseDown(Point point)
        {
            return m_ActiveButtons.Any(e => e.OnMouseDown(point));
        }

        public override bool OnMouseUp(Point point)
        {
            return m_ActiveButtons.Any(a => a.OnMouseUp(point));
        }
    }
}
