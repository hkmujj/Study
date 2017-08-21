using System.Drawing;
using Engine.HMI.HXD1C.TPX21A.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class FaultHelpInfo : baseClass, IButtonEventListener
    {
        private readonly HxRectText[] m_Texts = new HxRectText[3];

        private readonly HxRectText m_TextInfo = new HxRectText(); //显示故障帮助信息
        private readonly bool[] m_IsNumButtonDown = new bool[10];
        private readonly string[] m_TitleStr = {"V=0", "V>0", "信息"};
        private int m_Index;

        public override string GetInfo()
        {
            return "故障帮助信息";
        }

        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 25)
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
            if (GlobalParam.Instance.CurrentViewId == 25)
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
        }

        private void InitData()
        {
            m_TextInfo.SetBkColor(66, 117, 162);
            m_TextInfo.SetTextColor(255, 255, 255);
            m_TextInfo.SetTextStyle(14, FormatStyle.DirectionLeftToRight, true, "宋体");
            m_TextInfo.m_Isdrawrectfrm = true;
            m_TextInfo.SetLinePen(117, 126, 119, 2);
            m_TextInfo.SetTextRect(3, 75, 790, 400);

            for (int i = 0; i < 3; i++)
            {
                m_Texts[i] = new HxRectText();
                m_Texts[i].SetBkColor(139, 144, 137);
                m_Texts[i].SetDrawFrm(true);
                m_Texts[i].SetLinePen(255, 255, 255, 2);
                m_Texts[i].SetTextStyle(14, FormatStyle.Center, true, "宋体");
                m_Texts[i].SetTextColor(0, 0, 0);
            }

            m_Texts[0].SetTextRect(3, 40, 150, 30);
            m_Texts[1].SetTextRect(m_Texts[0].m_RectPosition.Right + 3, 40, 484, 30);
            m_Texts[2].SetTextRect(m_Texts[1].m_RectPosition.Right + 3, 40, 150, 30);

        }

        private void OnDraw(Graphics g)
        {
            //底部导航按钮绘制
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].OnDraw(g);
            }


            m_TextInfo.OnDraw(g);

            for (int i = 0; i < 3; i++)
            {
                m_Texts[i].OnDraw(g);
            }

        }

        private void GetValue()
        {
            //设置标题
            HxCommon.HTitle.SetText("处理提示");

            HxCommon.ButtonText[0].SetText("V=0");
            HxCommon.ButtonText[1].SetText("V>0");
            HxCommon.ButtonText[2].SetText("信息");
            HxCommon.ButtonText[3].SetText(" ");
            HxCommon.ButtonText[4].SetText(" ");
            HxCommon.ButtonText[5].SetText(" ");
            HxCommon.ButtonText[6].SetText(" ");
            HxCommon.ButtonText[7].SetText(" ");
            HxCommon.ButtonText[8].SetText(" ");
            HxCommon.ButtonText[9].SetText("主界面");

            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].SetBkColor(0, 0, 0);
                HxCommon.ButtonText[i].SetTextColor(255, 255, 255);
                m_IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i]];
            }


            if (HX_Fault.m_CurrentFault != null)
            {
                m_Texts[0].SetText(HX_Fault.m_CurrentFault.Level.ToString());
                m_Texts[1].SetText(HX_Fault.m_CurrentFault.FaultText);
                m_Texts[2].SetText(m_TitleStr[m_Index]);

                if (m_Index == 0) //显示V=0的信息
                {
                    m_TextInfo.SetText(HX_Fault.m_CurrentFault.HelpinfoV0);
                }
                else if (m_Index == 1)
                {
                    m_TextInfo.SetText(HX_Fault.m_CurrentFault.HelpinfoV);
                }
                else
                {
                    m_TextInfo.SetText(HX_Fault.m_CurrentFault.ProcedInfo);
                }

            }
            else
            {
                m_TextInfo.SetText(" ");
                m_Texts[0].SetText(" ");
                m_Texts[1].SetText(" ");
                m_Texts[2].SetText(" ");
            }


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
                    switch (i)
                    {
                        case 0:
                            m_Index = 0;
                            break;
                        case 1:
                            m_Index = 1;
                            break;
                        case 2:
                            m_Index = 2;
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9: //返回主界面
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }
}
