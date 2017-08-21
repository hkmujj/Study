using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using CommonUtil.Controls.Button;
using CommonUtil.Util;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Global;


namespace Motor.HMI.CRH1A.Sanitary
{
    /// <summary>
    /// 污物箱的按键绘制
    /// </summary>
    internal class SanitaryButtonMgr
    {
        private CRH1AButton[] m_Button;
        private GT_Sanitary m_GtSanitary;

        #region 页脚的按键大小

        private const int FootButtonWidth = 130;

        private const int FootButtonHight = 55;

        /// <summary>
        /// 按键最小间隔
        /// </summary>
        private const int ButtonInterval = 25;

        /// <summary>
        /// 起始位置的 X 
        /// </summary>
        private const int StartLocationX = 45;

        #endregion

        #region 按键响应事件

        public EventHandler ClearSigleUpHandler;

        public EventHandler ClearAllUpHandler;

        public EventHandler ClearSigleDownHandler;

        public EventHandler ClearAllDownHandler;

        #endregion



        public CRH1AButton this[SanitaryButtonType btnType]
        {
            get { return m_Button[(int) btnType]; }
        }

        public CRH1AButton this[int idx]
        {
            get { return m_Button[idx]; }
        }

        public SanitaryButtonMgr(GT_Sanitary gtSanitary)
        {
            m_GtSanitary = gtSanitary;
            m_Button = new CRH1AButton[Enum.GetValues(typeof (SanitaryButtonType)).Length];
        }

        public void Init()
        {
            var rect = m_GtSanitary.PageFootRectangle;

            var clearSigleX = rect.X + StartLocationX;
            var clearSigleBtn = new CRH1AButton();
            clearSigleBtn.SetButtonColor(192, 192, 192);
            clearSigleBtn.SetButtonRect(clearSigleX, rect.Y + 10, FootButtonWidth, FootButtonHight);
            clearSigleBtn.SetButtonText(EnumUtil.GetDescription(SanitaryButtonType.ClearSigle)[0]);

            m_Button[(int) SanitaryButtonType.ClearSigle] = clearSigleBtn;

            var clearAllBtn = new CRH1AButton();
            clearAllBtn.SetButtonColor(192, 192, 192);
            clearAllBtn.SetButtonRect(clearSigleX + FootButtonWidth + ButtonInterval, rect.Y + 10, FootButtonWidth , FootButtonHight);
            clearAllBtn.SetButtonText(EnumUtil.GetDescription(SanitaryButtonType.ClearAll)[0]);

            m_Button[(int) SanitaryButtonType.ClearAll] = clearAllBtn;
            foreach (var crh1AButton in m_Button)
            {
                crh1AButton.IsEnable = GlobalInfo.Instance.ButtonEnable;
            }

        }

        public void OnButtonDown(int x, int y)
        {
            //  按 钮 响 应 事 件

            for (int index = 0; index < m_Button.Length; index++)
            {
                var btn = m_Button[index];

                if (btn.Contains(new Point(x, y)))
                {
                    var selBtn = (SanitaryButtonType)index;
                    switch (selBtn)
                    {
                        case SanitaryButtonType.ClearSigle:
                            HandleUtil.OnHandle(ClearSigleDownHandler);
                            break;
                        case SanitaryButtonType.ClearAll:
                            HandleUtil.OnHandle(ClearAllDownHandler);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    btn.OnButtonDown();
                    break;
                }
            }
        }

        public void OnButtonUp(int x, int y)
        {
            var btnCnt = m_Button.Length;
            for (int i = 0; i < btnCnt; i++)
            {
                if (m_Button[i].Contains(new Point(x, y)))
                {
                    var selBtn = (SanitaryButtonType) i;
                    switch (selBtn)
                    {
                        case SanitaryButtonType.ClearSigle:
                            HandleUtil.OnHandle(ClearSigleUpHandler);
                            break;
                        case SanitaryButtonType.ClearAll:
                            HandleUtil.OnHandle(ClearAllUpHandler);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    m_Button[i].OnButtonUp();
                    break;
                }
            }
        }

        public void OnDraw(Graphics g)
        {
            foreach (var bt in m_Button)
            {
                bt.OnDraw(g);
            }
        }
    }
}