using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Engine.HMI.HXD1C.TPX21A.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class HX_LunYuanJinHua : baseClass, IButtonEventListener
    {
        private readonly Rectangle[] m_Rects = new Rectangle[3];
        private readonly HxRectText[] m_InputTexts = new HxRectText[5];
        private readonly Font m_Font = new Font("宋体", 14, FontStyle.Bold);
        private readonly bool[] m_IsNumButtonDown = new bool[10];
        private readonly bool[] m_IsRightButtonDown = new bool[4];
        private HxRectText m_ModeText; //模式信息
        private readonly Rectangle[] m_NavigateIcon = new Rectangle[5]; //导航图标
        private readonly SolidBrush m_IconBrush = new SolidBrush(Color.FromArgb(33, 36, 33)); //导航图标背景刷
        private readonly Pen m_IconPen = new Pen(Color.FromArgb(89, 92, 89), 2); //导航图标边框画笔

        private int m_Index = 0;

        public override string GetInfo()
        {
            return "轮缘润滑滑";
        }

        private List<int> m_RightBtnIndex;

        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 18)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }
                for (int i = 0; i < m_IsRightButtonDown.Length; i++)
                {
                    m_IsRightButtonDown[i] = false;
                }

                if (btn <= ButtonName.B10)
                {
                    m_IsNumButtonDown[(int) (btn - 800)] = true;
                }

                else
                {
                    var i = m_RightBtnIndex.IndexOf((int) btn);
                    if (i != -1)
                    {
                        m_IsRightButtonDown[i] = true;
                    }
                }

                ButtonDownEvent();

                return true;
            }

            return false;
        }

        public bool ResponseMouseUp(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 18)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                return true;
            }

            return false;
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_RightBtnIndex = Enumerable.Range(0, 4).Select(s => UIObj.InBoolList[s + 10]).ToList();
            GlobalParam.Instance.AddButtonEventListener(this);

            InitData();
            nErrorObjectIndex = -1;
            return true;
        }

        public override void paint(Graphics g)
        {
            GetValue();
            OnDraw(g);
            ButtonDownEvent();
            base.paint(g);
        }

        private void InitData()
        {
            for (int i = 0; i < 3; i++)
            {
                m_Rects[i] = new Rectangle(HxCommon.Recposition.X + 125, HxCommon.Recposition.Y + 105*i + 100, 400, 80);
            }

            m_ModeText = new HxRectText();
            m_ModeText.SetBkColor(0, 0, 0);
            m_ModeText.SetDrawFrm(true);
            m_ModeText.SetLinePen(235, 235, 235, 2);
            m_ModeText.SetTextColor(255, 255, 255);
            m_ModeText.SetTextStyle(16, FormatStyle.Center, true, "宋体");
            m_ModeText.SetTextRect(m_Rects[0].X + 200, m_Rects[0].Y + 32, 100, 30);
            m_ModeText.SetText("距离模式");

            for (int i = 0; i < 5; i++)
            {
                m_InputTexts[i] = new HxRectText();
                m_InputTexts[i].SetBkColor(0, 0, 0);
                m_InputTexts[i].m_Isdrawrectfrm = true;
                m_InputTexts[i].SetTextColor(255, 255, 255);
                m_InputTexts[i].SetTextStyle(14, FormatStyle.Center, true, "宋体");
                m_InputTexts[i].SetLinePen(255, 255, 255, 2);
                m_InputTexts[i].SetText("9");

                if (i < 2)
                {
                    m_InputTexts[i].SetTextRect(m_Rects[1].X + 240 + 30*i, m_Rects[1].Y + 28, 25, 40);
                }
                else
                {
                    m_InputTexts[i].SetTextRect(m_Rects[2].X + 225 + 30*(i - 2), m_Rects[2].Y + 28, 25, 40);
                }
            }

            //导航图标区初始化
            for (int i = 0; i < 5; i++)
            {
                if (i < 2)
                {
                    m_NavigateIcon[i] = new Rectangle(HxCommon.Recposition.X + 725, HxCommon.Recposition.Y + 75 + 62*i,
                        65, 55);
                }
                else
                {
                    m_NavigateIcon[i] = new Rectangle(HxCommon.Recposition.X + 725,
                        HxCommon.Recposition.Y + 280 + 62*(i - 2), 65, 55);
                }
            }
        }

        private void OnDraw(Graphics g)
        {
            //底部导航按钮绘制
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].OnDraw(g);
            }

            for (int i = 0; i < 3; i++)
            {
                g.DrawRectangle(HxCommon.WhitePen2, m_Rects[i]);
            }
            g.DrawString("工作模式", m_Font, HxCommon.WhiteBrush, m_ModeText.m_RectPosition.X - 160, m_ModeText.m_RectPosition.Y + 8);
            m_ModeText.OnDraw(g);

            for (int i = 0; i < 5; i++)
            {
                m_InputTexts[i].OnDraw(g);
            }
            g.DrawString("时间间隔", m_Font, HxCommon.WhiteBrush, m_ModeText.m_RectPosition.X - 160,
                m_InputTexts[0].m_RectPosition.Y);
            g.DrawString("(10-99)", m_Font, HxCommon.WhiteBrush, m_ModeText.m_RectPosition.X - 160,
                m_InputTexts[0].m_RectPosition.Y + 18);
            g.DrawString("s", HxCommon.Font16B, HxCommon.WhiteBrush, m_InputTexts[1].m_RectPosition.Right + 5,
                m_InputTexts[0].m_RectPosition.Y + 18);

            g.DrawString("距离间隔", m_Font, HxCommon.WhiteBrush, m_ModeText.m_RectPosition.X - 160,
                m_InputTexts[4].m_RectPosition.Y);
            g.DrawString("(100-909)", m_Font, HxCommon.WhiteBrush, m_ModeText.m_RectPosition.X - 170,
                m_InputTexts[4].m_RectPosition.Y + 18);
            g.DrawString("m", HxCommon.Font16B, HxCommon.WhiteBrush, m_InputTexts[4].m_RectPosition.Right + 5,
                m_InputTexts[4].m_RectPosition.Y + 18);

            //绘制导航区图标
            for (int i = 0; i < 5; i++)
            {
                if (i != 2)
                {
                    g.FillRectangle(m_IconBrush, m_NavigateIcon[i]);
                    g.DrawLine(m_IconPen, m_NavigateIcon[i].X + 3, m_NavigateIcon[i].Bottom + 3, m_NavigateIcon[i].Right + 3,
                        m_NavigateIcon[i].Bottom + 3);
                    g.DrawLine(m_IconPen, m_NavigateIcon[i].Right + 3, m_NavigateIcon[i].Y + 3, m_NavigateIcon[i].Right + 3,
                        m_NavigateIcon[i].Bottom + 3);
                }

            }

            g.DrawString("主", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[0].X + 20, m_NavigateIcon[0].Y + 5);
            g.DrawString("界面", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[0].X + 12, m_NavigateIcon[0].Y + 22);
            g.DrawString("[C]", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[0].X + 16,
                m_NavigateIcon[0].Y + 39);

            g.DrawString("模式", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[1].X + 14, m_NavigateIcon[1].Y + 5);
            g.DrawString("选择", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[1].X + 14, m_NavigateIcon[1].Y + 22);
            g.DrawString("[ˆ]", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[1].X + 18,
                m_NavigateIcon[1].Y + 39);

            g.DrawString("确认", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[3].X + 18, m_NavigateIcon[3].Y + 5);
            g.DrawString("输入", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[3].X + 18, m_NavigateIcon[3].Y + 22);
            g.DrawString("[>]", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[3].X + 22,
                m_NavigateIcon[3].Y + 39);

            g.DrawString("开始", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[4].X + 18, m_NavigateIcon[4].Y + 5);
            g.DrawString("测试", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[4].X + 18, m_NavigateIcon[4].Y + 22);
            g.DrawString("[E]", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[4].X + 22,
                m_NavigateIcon[4].Y + 39);


        }

        private void GetValue()
        {
            //设置标题
            HxCommon.HTitle.SetText("轮缘润滑参数输入界面");

            HxCommon.ButtonText[1].SetText("0");
            HxCommon.ButtonText[2].SetText("1");
            HxCommon.ButtonText[3].SetText("2");
            HxCommon.ButtonText[4].SetText("3");
            HxCommon.ButtonText[5].SetText("4");
            HxCommon.ButtonText[6].SetText("5");
            HxCommon.ButtonText[7].SetText("6");
            HxCommon.ButtonText[8].SetText("7");
            HxCommon.ButtonText[9].SetText("8");
            HxCommon.ButtonText[0].SetText("9");

            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].SetBkColor(0, 0, 0);
                HxCommon.ButtonText[i].SetTextColor(255, 255, 255);
                m_IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i]];
            }

            for (int i = 0; i < 4; i++)
            {
                m_IsRightButtonDown[i] = BoolList[UIObj.InBoolList[i + 10]];
            }

        }

        /// <summary>
        /// 响应硬件按钮按下事件
        /// </summary>
        public void ButtonDownEvent()
        {
            for (int i = 0; i < 4; i++)
            {
                if (m_IsRightButtonDown[i])
                {
                    switch (i)
                    {
                        case 0:
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                        case 1:
                            break;
                        case 2:

                            break;
                        case 3:
                            break;
                    }
                }
            }
        }

        //public void SelectedChange(int caseNum)
        //{
        //    inputNum[index] = caseNum;
        //    if (index==0||index==2||index==3)
        //    {
        //        index++;
        //    }
        //}

    }
}
