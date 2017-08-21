using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Engine.HMI.HXD1C.TPX21A.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    class HX_AssistTestStatus : baseClass,IButtonEventListener
    {
        private readonly bool[] m_IsNumButtonDown = new bool[10];//底部数字按钮是否按下
        private readonly bool[] m_IsPageButtonDown = new bool[2];//右侧翻页按钮
        private readonly HxRectText[] m_NoText=new HxRectText[32];//显示编号
        private readonly HxRectText[] m_InfoText=new HxRectText[16];//显示文本信息
        private SolidBrush m_GrayBrush = new SolidBrush(Color.FromArgb(221, 221, 221));
        private int m_Page = 0;
        private readonly SortedList<int, string> m_InfoList = new SortedList<int, string>();
        private readonly bool[] m_IsTrue = new bool[27];
        private int m_StarBit;
        HxRectText m_PageButton;
        private readonly Pen m_IconPen = new Pen(Color.FromArgb(89, 92, 89), 3);//导航图标边框画笔
        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 11)
            {
                for (int i = 0; i < m_IsNumButtonDown.Length; i++)
                {
                    m_IsNumButtonDown[i] = false;
                }

                if (btn <= ButtonName.B10)
                {
                    m_IsNumButtonDown[(int)(btn - 800)] = true;
                }
                ButtonDownEvent();
                return true;
            }

            return false;
        }

        public bool ResponseMouseUp(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 11)
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
            return "辅机测试状态";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            GlobalParam.Instance.AddButtonEventListener(this);
            InitData();

            LoadTestInfo();

            nErrorObjectIndex = -1;

            return true;
        }

        private void LoadTestInfo()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "辅机测试.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                string[] str = cStr.Split(';');
                if (str.Length == 2)
                {
                    m_InfoList.Add(m_InfoList.Count, str[1]);
                }
            }
        }

        public override void paint(Graphics g)
        {
            GetValue();
            ButtonDownEvent();
            DrawOn(g);
            base.paint(g);
        }

        public void InitData()
        {
            m_StarBit = UIObj.InBoolList[0];

           //编号文本框初始化
            for (int i = 0; i < 32;i++ )
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
                    m_NoText[i].SetTextRect(HxCommon.Recposition.X + 1, HxCommon.Recposition.Y + 34 + i * 26, 35, 26);
                }
                else
                {
                    m_NoText[i].SetTextRect(HxCommon.Recposition.X + 650, HxCommon.Recposition.Y + 38 + (i-16) * 26, 35, 26);
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
                m_InfoText[i].SetTextRect(HxCommon.Recposition.X + 35, HxCommon.Recposition.Y + 34 + i * 26, 615, 26);
            }

            //导航按钮初始化
            m_PageButton = new HxRectText();
            m_PageButton.SetBkColor(33, 36, 33);
            m_PageButton.SetTextColor(255, 255, 255);
            m_PageButton.SetTextStyle(14, FormatStyle.Center, true, "宋体");
            m_PageButton.SetLinePen(255, 255, 255, 2);
            m_PageButton.SetText("下一页");
            m_PageButton.SetTextRect(HxCommon.Recposition.X + 700, HxCommon.Recposition.Y + 225,70, 45);
        }

        public void GetValue()
        {
            //设置标题
            HxCommon.HTitle.SetText("辅机测试");

            HxCommon.ButtonText[0].SetText("牵引数据");
            HxCommon.ButtonText[1].SetText("温度");
            HxCommon.ButtonText[2].SetText("网络");
            HxCommon.ButtonText[3].SetText("辅助系统");
            HxCommon.ButtonText[4].SetText("工作状态");
            HxCommon.ButtonText[5].SetText("无线重联");
            HxCommon.ButtonText[6].SetText(" ");
            HxCommon.ButtonText[7].SetText("辅机测试");
            HxCommon.ButtonText[8].SetText("库内动车");
            HxCommon.ButtonText[9].SetText("主界面");

            for (int i = 0; i < 10; i++)
            {
                if (i == 7)
                {
                    HxCommon.ButtonText[i].SetBkColor(255, 255, 0);
                    HxCommon.ButtonText[i].SetTextColor(0, 0, 0);
                }
                else
                {
                    HxCommon.ButtonText[i].SetBkColor(0, 0, 0);
                    HxCommon.ButtonText[i].SetTextColor(255, 255, 255);
                }
            }

            for (int i = 0; i < 27;i++ )
            {
                m_IsTrue[i] = BoolList[i + m_StarBit];
            }

            for (int i = 0; i < 16 ; i++ )
            {
                if (m_IsTrue[(i + m_Page * 16) % 21])
                {
                    if (i + m_Page * 16 < m_InfoList.Count)
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

            for (int i = 0; i < m_InfoList.Count;i++ )
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

            for (int i = 0; i < 16;i++ )
            {
                if (m_Page * 16 + i < m_InfoList.Count)
                {
                    m_InfoText[i].SetText(m_InfoList[m_Page * 16 + i]);
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
            for ( int i = 0 ; i < 2 ; i++ )
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

            for (int i = 0; i < 32;i++ )
            {
                m_NoText[i].OnDraw(g);
            }

            for (int i = 0; i < 16;i++ )
            {
                m_InfoText[i].OnDraw(g);
            }

            m_PageButton.OnDraw(g);
            g.DrawLine(m_IconPen, m_PageButton.m_RectPosition.X + 3, m_PageButton.m_RectPosition.Bottom + 1, m_PageButton.m_RectPosition.Right - 2, m_PageButton.m_RectPosition.Bottom + 1);
            g.DrawLine(m_IconPen, m_PageButton.m_RectPosition.Right - 2, m_PageButton.m_RectPosition.Bottom + 1, m_PageButton.m_RectPosition.Right - 2, m_PageButton.m_RectPosition.Y -1);
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
                            break;
                        case 1://切换到温度界面
                            append_postCmd(CmdType.ChangePage, 5, 0, 0);
                            break;
                        case 2://切换到网络界面
                            append_postCmd(CmdType.ChangePage, 6, 0, 0);
                            break;
                        case 3://切换到辅助系统
                            append_postCmd(CmdType.ChangePage, 6, 0, 0);
                            break;
                        case 4://按下5切换到工作状态视图
                            append_postCmd(CmdType.ChangePage, 8, 0, 0);
                            break;
                        case 5://按下6切换到无线重联视图？？
                            
                            break;
                        case 6:
                            break;
                        case 7://按下8切换到辅机测试视图
                            append_postCmd(CmdType.ChangePage, 11, 0, 0);
                            break;
                        case 8://按下9切换到库内动车视图
                            append_postCmd(CmdType.ChangePage, 12, 0, 0);
                            break;
                        case 9://返回主界面
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                        default:
                            break;
                    }
                }
            }

            for (int i = 0; i < 2;i++ )
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
                        if (m_Page==0)
                        {
                            m_Page++;
                        }
                    }
                }
            }
        }
    }
}

