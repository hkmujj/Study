using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using CommonUtil.Util;
using Motor.HMI.CRH1A.Common;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;


namespace Motor.HMI.CRH1A.AirSupply
{
    public class AirSupplyBtnMgr : IInnerControl
    {
        /// <summary>
        /// 选中的控件, null 代表没有选中
        /// </summary>
        public AirSupplyInnerControlType SelecControlType { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public ButtonType ClickBtnType { private set; get; }

        /// <summary>
        /// 页脚区域
        /// </summary>
        public Rectangle PageFootRectangle { set; get; }

        private static readonly Size BtnSize = new Size(90, 50);

        /// <summary>
        /// 间距
        /// </summary>
        private const int BtnInterval = 50;

        /// <summary>
        /// 主压缩机选中后显示的btn
        /// </summary>
        private CRH1AButton[] m_MSelectBtns = new CRH1AButton[2];

        /// <summary>
        /// 压力传感器选中后的btn
        /// </summary>
        private CRH1AButton m_PreSelectBtn;

        #region 事件

        /// <summary>
        /// 主压缩机的事件
        /// </summary>
        public EventHandler MCutInOrOutDown;
        public EventHandler MCutInOrOutUp;
        public EventHandler MManulOpenOrCloseDown;
        public EventHandler MManulOpenOrCloseUp;

        /// <summary>
        /// 压力传感器事件
        /// </summary>
        public EventHandler PreCutInOrOutDown;
        public EventHandler PreCutInOrOutUp;

        #endregion

        public Point Location { get; set; }
        public Size Size { get; set; }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            m_PreSelectBtn = new CRH1AButton();
            var midleX = PageFootRectangle.X + PageFootRectangle.Width / 2;
            m_MSelectBtns[0] = new CRH1AButton();
            m_MSelectBtns[0].SetButtonRect(midleX - BtnInterval - BtnSize.Width, PageFootRectangle.Y + 10, BtnSize.Width, BtnSize.Height);
            m_MSelectBtns[0].SetButtonColor(192, 192, 192);
            m_MSelectBtns[0].SetButtonText("切入/切除");
            m_MSelectBtns[1] = new CRH1AButton();
            m_MSelectBtns[1].SetButtonRect(midleX + BtnSize.Width, PageFootRectangle.Y + 10, BtnSize.Width, BtnSize.Height);
            m_MSelectBtns[1].SetButtonColor(192, 192, 192);
            m_MSelectBtns[1].SetButtonText("手动 开/关");

            m_PreSelectBtn = new CRH1AButton();
            m_PreSelectBtn.SetButtonRect(midleX - BtnSize.Width / 2, PageFootRectangle.Y + 10, BtnSize.Width, BtnSize.Height);
            m_PreSelectBtn.SetButtonColor(192, 192, 192);
            m_PreSelectBtn.SetButtonText("切入/切除");
        }

        public bool OnMouseDown(Point point)
        {
            var hasProcess = false;
            switch (SelecControlType)
            {
                case AirSupplyInnerControlType.AYaShuoJ:
                    break;
                case AirSupplyInnerControlType.MYaShuoJi:
                    for (int i = 0; i < m_MSelectBtns.Length; i++)
                    {
                        if (m_MSelectBtns[i].Contains(point))
                        {
                            ClickBtnType = i == 0 ? ButtonType.CutInOrOut : ButtonType.ManaulOpenOrClose;
                            HandleUtil.OnHandle(i == 0 ? MCutInOrOutDown : MManulOpenOrCloseDown, this, null);
                            hasProcess = true;
                            m_MSelectBtns[i].OnButtonDown();
                        }
                    }
                    break;
                case AirSupplyInnerControlType.Presure:
                    if (m_PreSelectBtn.Contains(point))
                    {
                        HandleUtil.OnHandle(PreCutInOrOutDown, this, null);
                        ClickBtnType = ButtonType.CutInOrOut;
                        hasProcess = true;
                        m_PreSelectBtn.OnButtonDown();
                    }
                    break;
                case AirSupplyInnerControlType.Null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return hasProcess;
        }

        public bool OnMouseUp(Point point)
        {
            var hasProcess = false;
            switch (SelecControlType)
            {
                case AirSupplyInnerControlType.AYaShuoJ:
                    break;
                case AirSupplyInnerControlType.MYaShuoJi:
                    for (int i = 0; i < m_MSelectBtns.Length; i++)
                    {
                        if (m_MSelectBtns[i].Contains(point))
                        {
                            ClickBtnType = i == 0 ? ButtonType.CutInOrOut : ButtonType.ManaulOpenOrClose;
                            HandleUtil.OnHandle(i == 0 ? MCutInOrOutUp : MManulOpenOrCloseUp, this, null);
                            hasProcess = true;
                            m_MSelectBtns[i].OnButtonUp();
                        }
                    }
                    break;
                case AirSupplyInnerControlType.Presure:
                    if (m_PreSelectBtn.Contains(point))
                    {
                        ClickBtnType = ButtonType.CutInOrOut;
                        HandleUtil.OnHandle(PreCutInOrOutUp, this, null);
                        hasProcess = true;
                        m_PreSelectBtn.OnButtonUp();
                    }
                    break;
                case AirSupplyInnerControlType.Null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return hasProcess;
        }

        public void OnDraw(Graphics g)
        {
            switch (SelecControlType)
            {
                case AirSupplyInnerControlType.AYaShuoJ:
                    break;
                case AirSupplyInnerControlType.MYaShuoJi:
                    foreach (var btn in m_MSelectBtns)
                    {
                        btn.OnDraw(g);
                    }
                    break;
                case AirSupplyInnerControlType.Presure:
                    m_PreSelectBtn.OnDraw(g);
                    break;
                case AirSupplyInnerControlType.Null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnPaint(Graphics g)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Point point)
        {
            throw new NotImplementedException();
        }

        public Action<object> RefreshAction { get; set; }
        public object Tag { get; set; }
    }

    public enum ButtonType
    {
        CutInOrOut,
        ManaulOpenOrClose,
    }
}
