using System.Collections.Generic;
using System.Drawing;
using Engine.HMI.HXD1C.TPX21A.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    class HX_Fault : baseClass, IButtonEventListener
    {
        private readonly bool[] m_IsNumButtonDown = new bool[10];//底部数字按钮是否按下
        private readonly bool[] m_IsPageButtonDown = new bool[2];//右侧翻页按钮
        private readonly bool[] m_IsSelectItemButtom = new bool[2];//上下选择按钮
        public  int m_Page = 0;//当前页
        public  int  m_Index = 0;//当前选中项
        public static Fault m_CurrentFault ;//当前选中项的故障提示信息；
 
        private readonly HxRectText[] m_LevelText = new HxRectText[16];// 显示故障类别的文本框
        private readonly HxRectText[] m_TrainNoText = new HxRectText[16];//显示列车编号的文本框
        private readonly HxRectText[] m_FaultNoText = new HxRectText[16];//显示故障代码的文本框
        private readonly HxRectText[] m_InfoText = new HxRectText[16];//显示故障内容的文本框
        private readonly HxRectText[] m_HapenedTimeText = new HxRectText[16];//显示故障发生时间的文本框

        private readonly SolidBrush m_GrayBrush = new SolidBrush(Color.FromArgb(165, 162, 165));
        private Rectangle m_TitleRect;//标题栏
       
        private readonly SortedList<int, Image> m_ImageList = new SortedList<int, Image>();//界面中需要的界面元素
        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 13)
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
            if (GlobalParam.Instance.CurrentViewId == 13)
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
            return "当前故障信息";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            GlobalParam.Instance.AddButtonEventListener(this);
            InitData();

            //加载图片
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Image image = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
                m_ImageList.Add(i, image);
            }
            nErrorObjectIndex = -1;

            return true;
        }

        public override void paint(Graphics g)
        {
            GetValue();
            ButtonDownEvent();
            DrawOn(g);
            SetSortIndex();
            base.paint(g);
        }

        public event FaultRemoveHander FaultRemove;

        public void OnFaultRemove(Fault fault)
        {
            if (FaultRemove != null)
            {
                FaultRemove(this, new FaultEventArgs(fault));
            }
        }

        public void InitData()
        {
            //starBit = UIObj.InBoolList[0];
            //faultCount = UIObj.InBoolList[1];
            //标题栏文本框初始化化
            m_TitleRect = new Rectangle(HxCommon.Recposition.X, HxCommon.Recposition.Y + 32, 800, 28);
            //各文本框的初始化
            for (int i = 0; i < 16; i++)
            {
                m_LevelText[i] = new HxRectText();
                m_LevelText[i].SetBkColor(255, 255, 0);
                m_LevelText[i].SetTextColor(255, 0, 0);
                m_LevelText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                m_LevelText[i].SetTextRect(HxCommon.Recposition.X + 2, HxCommon.Recposition.Y + 63 + 26 * i, 32, 26);

                m_TrainNoText[i] = new HxRectText();
                m_TrainNoText[i].SetBkColor(165, 162, 165);
                m_TrainNoText[i].SetTextColor(0, 0, 0);
                m_TrainNoText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                m_TrainNoText[i].SetTextRect(m_LevelText[i].m_RectPosition.Right, m_LevelText[i].m_RectPosition.Y, 43, 26);

                m_FaultNoText[i] = new HxRectText();
                m_FaultNoText[i].SetBkColor(0, 0, 255);
                m_FaultNoText[i].SetTextColor(255, 255, 255);
                m_FaultNoText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                m_FaultNoText[i].SetTextRect(m_TrainNoText[i].m_RectPosition.Right, m_TrainNoText[i].m_RectPosition.Y, 50, 26);

                m_InfoText[i] = new HxRectText();
                m_InfoText[i].SetBkColor(255, 255, 0);
                m_InfoText[i].SetTextColor(0, 0, 0);
                m_InfoText[i].SetTextStyle(12, FormatStyle.DirectionLeftToRight, true, "宋体");
                m_InfoText[i].SetTextRect(m_FaultNoText[i].m_RectPosition.Right, m_FaultNoText[i].m_RectPosition.Y, 495, 26);

                m_HapenedTimeText[i] = new HxRectText();
                m_HapenedTimeText[i].SetBkColor(255, 255, 0);
                m_HapenedTimeText[i].SetTextColor(0, 0, 0);
                m_HapenedTimeText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                m_HapenedTimeText[i].SetTextRect(m_InfoText[i].m_RectPosition.Right, m_InfoText[i].m_RectPosition.Y, 150, 26);
            }
        }

        public void GetValue()
        {
            //设置标题
            HxCommon.HTitle.SetText("当前故障");

            HxCommon.ButtonText[0].SetText("");
            HxCommon.ButtonText[1].SetText("");
            HxCommon.ButtonText[2].SetText("");
            HxCommon.ButtonText[3].SetText("");
            HxCommon.ButtonText[4].SetText("");
            HxCommon.ButtonText[5].SetText("");
            HxCommon.ButtonText[6].SetText("当前故障");
            HxCommon.ButtonText[7].SetText("所有故障");
            HxCommon.ButtonText[8].SetText("历史故障");
            HxCommon.ButtonText[9].SetText("主界面");

            for (int i = 0; i < 10; i++)
            {
                if (i == 6)
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

            //上下选择按钮
            for (int i = 0; i < 2; i++)
            {
                m_IsSelectItemButtom[i] = BoolList[UIObj.InBoolList[i + 14]];
            }

            #region 刷新活动故障

         
           
            #endregion

            for (int i = 0; i < 16; i++)
            {
                if (i + m_Page * 16 < HXFaultCommom.m_SortedFaults.Count)
                {
                    switch ((int) HXFaultCommom.m_SortedFaults[i + m_Page * 16].Level)
                    {
                        case 0:
                            m_LevelText[i].SetText("A");
                            break;
                        case 1:
                            m_LevelText[i].SetText("B");
                            break;
                        case 2:
                            m_LevelText[i].SetText("C");
                            break;
                        case 3:
                            m_LevelText[i].SetText("D");
                            break;
                        default:
                            m_LevelText[i].SetText("NULL");
                            break;
                    }

                    m_TrainNoText[i].SetText(HXFaultCommom.m_SortedFaults[i + m_Page * 16].CaNum.ToString());
                    m_FaultNoText[i].SetText(HXFaultCommom.m_SortedFaults[i + m_Page * 16].FaultID.ToString());
                    m_InfoText[i].SetText(HXFaultCommom.m_SortedFaults[i + m_Page * 16].FaultText);
                    m_HapenedTimeText[i].SetText(HXFaultCommom.m_SortedFaults[i + m_Page * 16].HappenedTime.Month.ToString() + "-" + HXFaultCommom.m_SortedFaults[i + m_Page * 16].HappenedTime.Day.ToString() +
                        " " + HXFaultCommom.m_SortedFaults[i + m_Page * 16].HappenedTime.ToLongTimeString());
                }
                else
                {
                    m_LevelText[i].SetText(" ");
                    m_TrainNoText[i].SetText(" ");
                    m_FaultNoText[i].SetText(" ");
                    m_InfoText[i].SetText(" ");
                    m_HapenedTimeText[i].SetText(" ");
                }
            }
        }

        public void DrawOn(Graphics g)
        {
            //底部导航按钮绘制
            for (int i = 0; i < 10; i++)
            {
                HxCommon.ButtonText[i].OnDraw(g);
            }

            #region 标题栏绘制
            g.FillRectangle(m_GrayBrush, m_TitleRect);
            g.DrawString("类别", HxCommon.Font12B, HxCommon.RedBrush, m_TitleRect.X + 2, m_TitleRect.Y + 4);
            g.DrawString("车号", HxCommon.Font12B, HxCommon.BlackBrush, m_TitleRect.X + 35, m_TitleRect.Y + 4);
            g.DrawString("代码", HxCommon.Font12B, HxCommon.WhiteBrush, m_TitleRect.X + 85, m_TitleRect.Y + 4);
            g.DrawString("故障内容", HxCommon.Font12B, HxCommon.BlackBrush, m_TitleRect.X + 460, m_TitleRect.Y + 4);
            g.DrawString("故障发生时间", HxCommon.Font12B, HxCommon.DeadBrush, m_TitleRect.X + 620, m_TitleRect.Y + 4);
            #endregion

            for (int i = 0; i < 16; i++)
            {
                if (i == m_Index)
                {
                    m_InfoText[i].SetBkColor(192, 192, 192);
                    m_HapenedTimeText[i].SetBkColor(192, 192, 192);
                }
                else
                {
                    m_InfoText[i].SetBkColor(255, 255, 0);
                    m_HapenedTimeText[i].SetBkColor(255, 255, 0);
                }
                m_LevelText[i].OnDraw(g);
                m_TrainNoText[i].OnDraw(g);
                m_FaultNoText[i].OnDraw(g);
                m_InfoText[i].OnDraw(g);
                m_HapenedTimeText[i].OnDraw(g);

            }

            g.DrawImage(m_ImageList[0], HxCommon.Recposition.X + 773, HxCommon.Recposition.Y + 62, 25, 45);
            g.DrawImage(m_ImageList[1], HxCommon.Recposition.X + 773, m_LevelText[15].m_RectPosition.Bottom - 25, 25, 25);
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
                        case 6://按下7切换到当前故障视图
                            append_postCmd(CmdType.ChangePage, 13, 0, 0);
                            break;
                        case 7://按下8切换到所有故障视图
                            append_postCmd(CmdType.ChangePage, 14, 0, 0);
                            break;
                        case 8://按下7切换到历史故障视图
                            append_postCmd(CmdType.ChangePage, 15, 0, 0);
                            break;
                        case 9://返回主界面
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            break;
                        default:
                            break;
                    }
                }
            }

            for (int i = 0; i < 2; i++)
            {
                if (m_IsPageButtonDown[i])
                {
                    if (i == 0)
                    {
                        if (m_Page > 0)
                        {
                            m_Page--;
                        }
                    }
                    else
                    {
                        if (HXFaultCommom.m_SortedFaults.Count % 16 == 0)
                        {
                            if (m_Page < HXFaultCommom.m_SortedFaults.Count / 16 - 1)
                            {
                                m_Page++;
                            }
                        }
                        else
                        {
                            if (m_Page < HXFaultCommom.m_SortedFaults.Count / 16)
                            {
                                m_Page++;
                            }
                        }
                    }
                }
            }

            if (m_IsSelectItemButtom[0])//向上键按下
            {
                if (m_Index == 0)
                {
                    if (m_Page > 0)
                    {
                        m_Page--;
                        m_Index = 15;
                    }
                }
                else
                {
                    m_Index--;
                }
            }

            if (m_IsSelectItemButtom[1])//向下键按下
            {
                if (m_Index + m_Page * 16 <HXFaultCommom.m_SortedFaults.Count-1)
                {
                    if (m_Index == 15)
                    {
                        if (HXFaultCommom.m_SortedFaults.Count % 16 == 0)
                        {
                            if (m_Page < HXFaultCommom.m_SortedFaults.Count / 16 - 1)
                            {
                                m_Page++;
                                m_Index = 0;
                            }
                        }
                        else
                        {
                            if (m_Page < HXFaultCommom.m_SortedFaults.Count / 16)
                            {
                                m_Page++;
                                m_Index = 0;
                            }
                        }
                    }
                    else
                    {
                        m_Index++;
                    }

                }

            }

        }

        private void SetSortIndex()
        {
            if (HXFaultCommom.m_SortedFaults.Count > 0)
            {
                if (HXFaultCommom.m_SortedFaults.ContainsKey(m_Page * 16 + m_Index))
                {
                    m_CurrentFault = HXFaultCommom.m_SortedFaults[m_Page * 16 + m_Index];
                } 
            }
            else
            {
                m_CurrentFault = null;
            }

            
        }
    }
}