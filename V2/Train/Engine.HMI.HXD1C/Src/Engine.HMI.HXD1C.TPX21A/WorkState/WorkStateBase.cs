using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Engine.HMI.HXD1C.TPX21A.Button;
using MMI.Facility.Interface;

namespace Engine.HMI.HXD1C.TPX21A.WorkState
{
    internal abstract class WorkStateBase : baseClass, IButtonEventListener
    {
        private readonly bool[] m_IsNumButtonDown = new bool[10]; //底部数字按钮是否按下
        private readonly bool[] m_IsPageButtonDown = new bool[2]; //右侧翻页按钮
        private readonly HxRectText[] m_NoText = new HxRectText[32]; //显示编号
        private readonly HxRectText[] m_InfoText = new HxRectText[16]; //显示文本信息
        private SolidBrush m_GrayBrush = new SolidBrush(Color.FromArgb(221, 221, 221));
        private int m_Page = 0;
        private SortedList<int, Tuple<int, string>> m_InfoList = new SortedList<int, Tuple<int, string>>();
        private bool[] m_IsTrue;
        private int m_StarBit;
        private HxRectText m_PageButton;
        private readonly Pen m_IconPen = new Pen(Color.FromArgb(89, 92, 89), 3); //导航图标边框画笔
        protected int BelongViewId { get; set; }

        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == BelongViewId)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                if (btn <= ButtonName.B10)
                {
                    m_IsNumButtonDown[(int) (btn - 800)] = true;
                }
                ButtonDownEvent();
                return true;
            }

            return false;
        }

        public bool ResponseMouseUp(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == BelongViewId)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                return true;
            }

            return false;
        }

        public override string GetInfo()
        {
            return String.Empty;
        }

        public override sealed bool init(ref int nErrorObjectIndex)
        {
            GlobalParam.Instance.AddButtonEventListener(this);

            m_InfoList = InitalizeInfos();

            m_IsTrue = new bool[m_InfoList.Count];

            UIObj.InBoolList.AddRange(m_InfoList.Values.Select(s => s.Item1));

            InitData();

            nErrorObjectIndex = -1;

            return true;
        }

        protected abstract SortedList<int, Tuple<int, string>> InitalizeInfos();


        public override sealed void paint(Graphics g)
        {
            GetValue();
            ButtonDownEvent();
            DrawOn(g);
        }

        public void InitData()
        {
            m_StarBit = UIObj.InBoolList[0];

            //编号文本框初始化
            for (int i = 0; i < 32; i++)
            {
                m_NoText[i] = new HxRectText();
                m_NoText[i].SetBkColor(0, 0, 0);
                m_NoText[i].SetDrawFrm(true);
                m_NoText[i].SetTextColor(255, 255, 255);
                m_NoText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                m_NoText[i].SetLinePen(255, 255, 255, 2);
                m_NoText[i].SetText((i + 1).ToString());
                if (i < 16)
                {
                    m_NoText[i].SetTextRect(HxCommon.Recposition.X + 1, HxCommon.Recposition.Y + 34 + i*26, 35, 26);
                }
                else
                {
                    m_NoText[i].SetTextRect(HxCommon.Recposition.X + 650, HxCommon.Recposition.Y + 38 + (i - 16)*26, 35,
                        26);
                }
            }

            //显示内容文本框初始化
            for (int i = 0; i < 16; i++)
            {
                m_InfoText[i] = new HxRectText();
                m_InfoText[i].SetBkColor(0, 0, 0);
                m_InfoText[i].SetDrawFrm(true);
                m_InfoText[i].SetTextColor(255, 255, 255);
                m_InfoText[i].SetTextStyle(12, FormatStyle.DirectionLeftToRight, true, "宋体");
                m_InfoText[i].SetLinePen(255, 255, 255, 2);
                m_InfoText[i].SetText((i + 1).ToString());
                m_InfoText[i].SetTextRect(HxCommon.Recposition.X + 35, HxCommon.Recposition.Y + 34 + i*26, 615, 26);
            }

            //导航按钮初始化
            m_PageButton = new HxRectText();
            m_PageButton.SetBkColor(33, 36, 33);
            m_PageButton.SetTextColor(255, 255, 255);
            m_PageButton.SetTextStyle(14, FormatStyle.Center, true, "宋体");
            m_PageButton.SetLinePen(255, 255, 255, 2);
            m_PageButton.SetText("下一页");
            m_PageButton.SetTextRect(HxCommon.Recposition.X + 700, HxCommon.Recposition.Y + 225, 70, 45);
        }

        protected abstract void SetCommonButtons();

        public void GetValue()
        {
            //设置标题
            SetCommonButtons();

            for (int i = 0; i < m_InfoList.Count; i++)
            {
                m_IsTrue[i] = BoolList[m_InfoList[i].Item1];
            }

            for (int i = 0; i < 16; i++)
            {
                if (m_IsTrue[(i + m_Page*16)%m_InfoList.Count])
                {
                    if (i + m_Page*16 < m_InfoList.Count)
                    {
                        m_InfoText[i].SetBkColor(221, 223, 222);
                        m_InfoText[i].SetTextColor(0, 0, 0);
                    }
                    else
                    {
                        m_InfoText[i].SetBkColor(0, 0, 0);
                        m_InfoText[i].SetTextColor(255, 255, 255);
                    }

                }
                else
                {
                    m_InfoText[i].SetBkColor(0, 0, 0);
                    m_InfoText[i].SetTextColor(255, 255, 255);

                }
            }

            for (int i = 0; i < m_InfoList.Count; i++)
            {
                if (m_IsTrue[i])
                {
                    m_NoText[i].SetBkColor(221, 223, 222);
                    m_NoText[i].SetTextColor(0, 0, 0);
                }
                else
                {
                    m_NoText[i].SetBkColor(0, 0, 0);
                    m_NoText[i].SetTextColor(255, 255, 255);
                }
            }

            for (int i = 0; i < 16; i++)
            {
                if (m_Page*16 + i < m_InfoList.Count)
                {
                    m_InfoText[i].SetText(m_InfoList[m_Page*16 + i].Item2);
                }
                else
                {
                    m_InfoText[i].SetText(" ");
                }
            }

            //按钮状态更新
            for (int i = 0; i < 10; i++)
            {
                m_IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i + 2]];
            }

            //导航按钮
            for (int i = 0; i < 2; i++)
            {
                m_IsPageButtonDown[i] = BoolList[UIObj.InBoolList[i + 12]];
            }
        }

        public void DrawOn(Graphics g)
        {
            //底部导航按钮绘制
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].OnDraw(g);
            }

            for (int i = 0; i < 32; i++)
            {
                m_NoText[i].OnDraw(g);
            }

            for (int i = 0; i < 16; i++)
            {
                m_InfoText[i].OnDraw(g);
            }

            m_PageButton.OnDraw(g);
            g.DrawLine(m_IconPen, m_PageButton.m_RectPosition.X + 3, m_PageButton.m_RectPosition.Bottom + 1,
                m_PageButton.m_RectPosition.Right - 2, m_PageButton.m_RectPosition.Bottom + 1);
            g.DrawLine(m_IconPen, m_PageButton.m_RectPosition.Right - 2, m_PageButton.m_RectPosition.Bottom + 1,
                m_PageButton.m_RectPosition.Right - 2, m_PageButton.m_RectPosition.Y - 1);
        }

        /// <summary>
        /// 响应硬件按钮按下事件
        /// </summary>
        public void ButtonDownEvent()
        {
            for (int i = 0; i < 10; i++)
            {
                if (m_IsNumButtonDown[i])
                {
                    ChagePage(i);
                }
            }

            for (int i = 0; i < 2; i++)
            {
                if (m_IsPageButtonDown[i])
                {
                    if (i == 0)
                    {
                        if (m_Page == 1)
                        {
                            m_Page--;
                        }
                    }
                    else
                    {
                        if (m_Page == 0)
                        {
                            m_Page++;
                        }
                    }
                }
            }
        }

        protected abstract void ChagePage(int i);
    }
}