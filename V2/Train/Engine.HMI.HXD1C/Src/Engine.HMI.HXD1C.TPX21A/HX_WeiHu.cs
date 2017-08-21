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
    public  class HX_WeiHu:baseClass, IButtonEventListener
    {
        private Rectangle m_Rect ;
        private readonly HxRectText[] m_Texts = new HxRectText[6];
        private readonly Pen m_RectPen = new Pen(Color.FromArgb(25, 25, 25), 2);
        private readonly Rectangle[] m_NavigateIcon = new Rectangle[5];//导航图标
        private readonly SolidBrush m_IconBrush = new SolidBrush(Color.FromArgb(33, 36, 33));//导航图标背景刷
        private readonly Pen m_IconPen = new Pen(Color.FromArgb(89, 92, 89), 2);//导航图标边框画笔
        private readonly bool[] m_IsRightButtonDown = new bool[2];//右边按钮是否按下

        private List<int> m_RightBtnIndex;

        public override string GetInfo()
        {
            return "维护";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            m_RightBtnIndex = Enumerable.Range(0, 2).Select(s => UIObj.InBoolList[s]).ToList();

            InitData();

            nErrorObjectIndex = -1;

            return true;
        }

        public void InitData()
        {
            m_Rect = new Rectangle(300, 150,250,200);

            for (int i = 0; i < 6;i++ )
            {
                m_Texts[i] = new HxRectText();
                m_Texts[i].SetBkColor(0, 0, 0);
                m_Texts[i].m_Isdrawrectfrm = true;
                m_Texts[i].SetLinePen(255, 255, 255, 2);
                m_Texts[i].SetTextColor(255, 255, 255);
                m_Texts[i].SetTextStyle(14, FormatStyle.Center, true, "Arial");
                m_Texts[i].SetText("*");
                m_Texts[i].SetTextRect(m_Rect.X + 50 + i * 19, m_Rect.Y + 65, 15, 35);
            }


            //导航图标区初始化
            for (int i = 0; i < 5; i++)
            {
                if (i < 2)
                {
                    m_NavigateIcon[i] = new Rectangle(HxCommon.Recposition.X + 725, HxCommon.Recposition.Y + 75 + 62 * i, 65, 55);
                }
                else
                {
                    m_NavigateIcon[i] = new Rectangle(HxCommon.Recposition.X + 725, HxCommon.Recposition.Y + 280 + 62 * (i - 2), 65, 55);
                }
            }
        }

        public override void paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
            ButtonDownEvent();
            base.paint(g);
        }

        public void DrawOn(Graphics g)
        {
            g.DrawRectangle(m_RectPen, m_Rect);

            for (int i = 0; i < 6;i++ )
            {
                m_Texts[i].OnDraw(g);
            }

            g.DrawString("进入维护界面必须通过身份验证", HxCommon.Font12B, HxCommon.WhiteBrush, m_Rect.X + 10, m_Rect.Top - 25);
            g.DrawString("请输入密码:", HxCommon.Font12B, HxCommon.WhiteBrush, m_Rect.X + 45, m_Rect.Top + 25);


            //绘制导航区图标
            for (int i = 0; i < 5; i++)
            {
                if (i==0||i==4)
                {
                    g.FillRectangle(m_IconBrush, m_NavigateIcon[i]);
                    g.DrawLine(m_IconPen, m_NavigateIcon[i].X + 3, m_NavigateIcon[i].Bottom + 3, m_NavigateIcon[i].Right + 3, m_NavigateIcon[i].Bottom + 3);
                    g.DrawLine(m_IconPen, m_NavigateIcon[i].Right + 3, m_NavigateIcon[i].Y + 3, m_NavigateIcon[i].Right + 3, m_NavigateIcon[i].Bottom + 3);
                } 
            }

            g.DrawString("退出[C]", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[0].X + 4, m_NavigateIcon[0].Y + 15);
           
           
            g.DrawString("确认[E]", HxCommon.Font12B, HxCommon.WhiteBrush, m_NavigateIcon[4].X + 4, m_NavigateIcon[4].Y + 18);
           

            for (int i = 0; i < 10;i++ )
            {
                HxCommon.ButtonText[i].OnDraw(g);
            }
        }

        public void GetValue()
        {

            //设置标题
            HxCommon.HTitle.SetText("维护");

            HxCommon.ButtonText[0].SetText("1");
            HxCommon.ButtonText[1].SetText("2");
            HxCommon.ButtonText[2].SetText("3");
            HxCommon.ButtonText[3].SetText("4");
            HxCommon.ButtonText[4].SetText("5");
            HxCommon.ButtonText[5].SetText("6");
            HxCommon.ButtonText[6].SetText("7");
            HxCommon.ButtonText[7].SetText("8");
            HxCommon.ButtonText[8].SetText("9");
            HxCommon.ButtonText[9].SetText("0");
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].SetBkColor(0, 0, 0);
                HxCommon.ButtonText[i].SetTextColor(255, 255, 255);
            }

            for (int i = 0; i < 2;i++ )
            {
                m_IsRightButtonDown[i] = BoolList[UIObj.InBoolList[i]];
            }
        }

         /// <summary>
        /// 响应硬件按钮按下事件
        /// </summary>
        public void ButtonDownEvent()
        {
            if (m_IsRightButtonDown[0])
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
            else
            { 
            }
        }

        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 17)
            {
                for (int i = 0; i < m_IsRightButtonDown.Length; i++)
                {
                    m_IsRightButtonDown[i] = false;
                }

                var idx = m_RightBtnIndex.IndexOf((int) btn);
                if (idx != -1)
                {
                    m_IsRightButtonDown[idx] = true;
                }

                ButtonDownEvent();
                return true;
            }

            return false;
        }

        public bool ResponseMouseUp(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 17)
            {
                for (int i = 0; i < m_IsRightButtonDown.Length; i++)
                {
                    m_IsRightButtonDown[i] = false;
                }

                return true;
            }

            return false;
        }

    }
}
