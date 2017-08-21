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
        class HX_EventFault : baseClass, IButtonEventListener
       {
        private readonly bool[] m_IsNumButtonDown = new bool[10];//底部数字按钮是否按下
        private readonly bool[] m_IsPageButtonDown = new bool[2];//右侧翻页按钮
        private int m_Page = 0;//当前页
        
        private readonly HxRectText[] m_LevelText = new HxRectText[16];// 显示故障类别的文本框
        private readonly HxRectText[] m_TrainNoText = new HxRectText[16];//显示列车编号的文本框
        private readonly HxRectText[] m_FaultNoText = new HxRectText[16];//显示故障代码的文本框
        private readonly HxRectText[] m_InfoText = new HxRectText[16];//显示故障内容的文本框
        private readonly HxRectText[] m_HapenedTimeText = new HxRectText[16];//显示故障发生时间的文本框
        private readonly HxRectText[] m_EndedTimeText = new HxRectText[16];//显示故障结束时间的文本框 

        private readonly SolidBrush m_GrayBrush=new SolidBrush(Color.FromArgb(165, 162, 165));
        private Rectangle m_TitleRect;//标题栏
      
        private readonly SortedList<int, Image> m_ImageList = new SortedList<int, Image>();//界面中需要的界面元素
        public bool ResponseMouseDown(ButtonName btn)
        {
            if (GlobalParam.Instance.CurrentViewId == 15)
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
            if (GlobalParam.Instance.CurrentViewId == 15)
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
            return "历史故障信息";
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
            base.paint(g);
        }

       

        public void InitData()
        {
           
            //标题栏文本框初始化化
            m_TitleRect = new Rectangle(HxCommon.Recposition.X, HxCommon.Recposition.Y + 32, 800, 28);
            //各文本框的初始化
            for (int i= 0 ; i< 16 ; i++)
            {
                m_LevelText[i] = new HxRectText();
                m_LevelText[i].SetBkColor(255, 255, 0);
                m_LevelText[i].SetTextColor(255, 0, 0);
                m_LevelText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                m_LevelText[i].SetTextRect(HxCommon.Recposition.X+2, HxCommon.Recposition.Y + 63+26*i,32,26);

                m_TrainNoText[i] = new HxRectText();
                m_TrainNoText[i].SetBkColor(165, 162, 165);
                m_TrainNoText[i].SetTextColor(0, 0, 0);
                m_TrainNoText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                m_TrainNoText[i].SetTextRect(m_LevelText[i].m_RectPosition.Right, m_LevelText[i].m_RectPosition.Y, 43, 26);

                m_FaultNoText[i] = new HxRectText();
                m_FaultNoText[i].SetBkColor(0, 0, 255);
                m_FaultNoText[i].SetTextColor(255,255, 255);
                m_FaultNoText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
                m_FaultNoText[i].SetTextRect(m_TrainNoText[i].m_RectPosition.Right, m_TrainNoText[i].m_RectPosition.Y, 50, 26);

                m_InfoText[i] = new HxRectText();
                m_InfoText[i].SetBkColor(255, 255, 0);
                m_InfoText[i].SetTextColor(0, 0, 0);
                m_InfoText[i].SetTextStyle(12, FormatStyle.DirectionLeftToRight, true, "宋体");
                m_InfoText[i].SetTextRect(m_FaultNoText[i].m_RectPosition.Right, m_FaultNoText[i].m_RectPosition.Y, 345, 26);

               m_HapenedTimeText[i] = new HxRectText();
               m_HapenedTimeText[i].SetBkColor(255, 255, 0);
               m_HapenedTimeText[i].SetTextColor(0, 0, 0);
               m_HapenedTimeText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
               m_HapenedTimeText[i].SetTextRect(m_InfoText[i].m_RectPosition.Right, m_InfoText[i].m_RectPosition.Y, 150, 26);

               m_EndedTimeText[i] = new HxRectText();
               m_EndedTimeText[i].SetBkColor(255, 255, 0);
               m_EndedTimeText[i].SetTextColor(0, 0, 0);
               m_EndedTimeText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
               m_EndedTimeText[i].SetTextRect(m_HapenedTimeText[i].m_RectPosition.Right, m_HapenedTimeText[i].m_RectPosition.Y, 150, 26);
            }
        }

        public void GetValue()
        {
            //设置标题
            HxCommon.HTitle.SetText("历史故障");

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
                if (i ==8)
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
                m_IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i]];
            }

            //导航按钮
            for (int i = 0; i < 2; i++)
            {
                m_IsPageButtonDown[i] = BoolList[UIObj.InBoolList[i + 10]];
            }

            for (int i = 0; i < 16; i++)
            {
                if (i + m_Page*16 <  HXFaultCommom.m_TotalFaults.Count)
                {
                    switch ((int) HXFaultCommom.m_TotalFaults[i+m_Page*16].Level)
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

                    m_TrainNoText[i].SetText( HXFaultCommom.m_TotalFaults[i + m_Page * 16].CaNum.ToString());
                    m_FaultNoText[i].SetText( HXFaultCommom.m_TotalFaults[i + m_Page * 16].FaultID.ToString());
                    m_InfoText[i].SetText( HXFaultCommom.m_TotalFaults[i + m_Page * 16].FaultText);
                    m_HapenedTimeText[i].SetText(  HXFaultCommom.m_TotalFaults[i + m_Page * 16].HappenedTime.Month.ToString() + "-" +  HXFaultCommom.m_TotalFaults[i + m_Page * 16].HappenedTime.Day.ToString() +
                                                 " " +  HXFaultCommom.m_TotalFaults[i + m_Page * 16].HappenedTime.ToLongTimeString());
                    m_EndedTimeText[i].SetText( HXFaultCommom.m_TotalFaults[i + m_Page * 16].EndedTime.Month.ToString() + "-" +  HXFaultCommom.m_TotalFaults[i + m_Page * 16].EndedTime.Day.ToString() +
                                                 " " +  HXFaultCommom.m_TotalFaults[i + m_Page * 16].EndedTime.ToLongTimeString());
                }
                else
                {
                    m_LevelText[i].SetText(" ");
                    m_TrainNoText[i].SetText(" ");
                    m_FaultNoText[i].SetText(" ");
                    m_InfoText[i].SetText(" ");
                    m_HapenedTimeText[i].SetText(" ");
                    m_EndedTimeText[i].SetText(" ");
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
            g.DrawString("故障内容", HxCommon.Font12B, HxCommon.BlackBrush, m_TitleRect.X + 325, m_TitleRect.Y + 4);
            g.DrawString("故障发生时间", HxCommon.Font12B, HxCommon.DeadBrush, m_TitleRect.X + 500, m_TitleRect.Y + 4);
            g.DrawString("故障结束时间", HxCommon.Font12B, HxCommon.DeadBrush, m_TitleRect.X + 660, m_TitleRect.Y + 4);
            #endregion

            for (int i = 0; i < 16;i++ )
            {
                m_LevelText[i].OnDraw(g);
                m_TrainNoText[i].OnDraw(g);
                m_FaultNoText[i].OnDraw(g);
                m_InfoText[i].OnDraw(g);
                m_HapenedTimeText[i].OnDraw(g);
                m_EndedTimeText[i].OnDraw(g);
            }

            g.DrawImage(m_ImageList[0], HxCommon.Recposition.X + 773, HxCommon.Recposition.Y + 62, 25, 45);
            g.DrawImage(m_ImageList[1], HxCommon.Recposition.X + 773, m_LevelText[15].m_RectPosition.Bottom-25, 25, 25);
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
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            
                            break;
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
                        if ( HXFaultCommom.m_TotalFaults.Count % 16 == 0 )
                        {
                            if (m_Page <  HXFaultCommom.m_TotalFaults.Count / 16 - 1)
                            {
                                m_Page++;
                            }
                        }
                        else
                        {
                           if (m_Page< HXFaultCommom.m_TotalFaults.Count/16)
                           {
                               m_Page++;
                           }
                        }

                    }
                }
            }
        }
    }
}

//    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
//    class HX_EventFault : baseClass
//    {
//        private bool[] IsNumButtonDown = new bool[10];//底部数字按钮是否按下
//        private bool[] isPageButtonDown = new bool[2];//右侧翻页按钮
//        private int page = 0;//当前页

//        private HX_RectText[] levelText = new HX_RectText[16];// 显示故障类别的文本框
//        private HX_RectText[] trainNoText = new HX_RectText[16];//显示列车编号的文本框
//        private HX_RectText[] faultNoText = new HX_RectText[16];//显示故障代码的文本框
//        private HX_RectText[] infoText = new HX_RectText[16];//显示故障内容的文本框
//        private HX_RectText[] hapenedTimeText = new HX_RectText[16];//显示故障发生时间的文本框
//        private HX_RectText[] endedTimeText = new HX_RectText[16];//显示故障结束时间的文本框 

//        private SolidBrush grayBrush = new SolidBrush(Color.FromArgb(165, 162, 165));
//        private Rectangle titleRect;//标题栏

//        private SortedList<int, Image> imageList = new SortedList<int, Image>();//界面中需要的界面元素

//        public override string GetInfo()
//        {
//            return "历史故障信息";
//        }

//        public override string GetTypeName()
//        {
//            //1
//            return "HX_EventFault";
//        }

//        public override bool initValue(string nParaString, ref int nErrorObjectIndex)
//        {
//            return base.initValue(nParaString, ref nErrorObjectIndex);
//        }

//        public override bool init(ref int nErrorObjectIndex)
//        {
//            InitData();

//            //加载图片
//            for (int i = 0; i < UIObj.ParaList.Count; i++)
//            {
//                Image image = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
//                imageList.Add(i, image);
//            }
//            nErrorObjectIndex = -1;

//            return true;
//        }

//        public override void paint(System.Drawing.Graphics g)
//        {
//            GetValue();
//            ButtonDownEvent();
//            DrawOn(g);
//            base.paint(g);
//        }

//        //public override bool canSetStringList(int nPara)
//        //{
//        //    if (nPara ==4)
//        //    {
//        //        return true;
//        //    }
//        //    return base.canSetStringList(nPara);
//        //}

//        ///// <summary>
//        ///// 读入记事本中存储的所有故障信息
//        ///// 将所有种类的故障保存在静态链表中
//        ///// </summary>
//        ///// <param name="nIndex"></param>
//        ///// <param name="cStr"></param>
//        //public override void addStringByLine(int nIndex, string cStr)
//        //{
//        //    if (cStr.Trim() != "")
//        //    {
//        //        string[] str = cStr.Split(';');
//        //        if (str.Length==6)
//        //        {
//        //            Fault fauly = new Fault();
//        //            fauly.LogicalBit = int.Parse(str[0]);
//        //            fauly.Level = (Fault.FaultLevel) int.Parse(str[1]);
//        //            fauly.TrainNo = int.Parse(str[2]);
//        //            fauly.FaultID = int.Parse(str[3]);
//        //            fauly.FaultText = str[4];
//        //            fauly.HelpId = int.Parse(str[5]);

//        //            StaticFaults.Add(fauly.LogicalBit, fauly);
//        //        }

//        //    }
//        //}

//        public void InitData()
//        {

//            //标题栏文本框初始化化
//            titleRect = new Rectangle(HX_Common.recposition.X, HX_Common.recposition.Y + 32, 800, 28);
//            //各文本框的初始化
//            for (int i = 0; i < 16; i++)
//            {
//                levelText[i] = new HX_RectText();
//                levelText[i].SetBkColor(255, 255, 0);
//                levelText[i].SetTextColor(255, 0, 0);
//                levelText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
//                levelText[i].SetTextRect(HX_Common.recposition.X + 2, HX_Common.recposition.Y + 63 + 26 * i, 32, 26);

//                trainNoText[i] = new HX_RectText();
//                trainNoText[i].SetBkColor(165, 162, 165);
//                trainNoText[i].SetTextColor(0, 0, 0);
//                trainNoText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
//                trainNoText[i].SetTextRect(levelText[i].RectPosition.Right, levelText[i].RectPosition.Y, 43, 26);

//                faultNoText[i] = new HX_RectText();
//                faultNoText[i].SetBkColor(0, 0, 255);
//                faultNoText[i].SetTextColor(255, 255, 255);
//                faultNoText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
//                faultNoText[i].SetTextRect(trainNoText[i].RectPosition.Right, trainNoText[i].RectPosition.Y, 50, 26);

//                infoText[i] = new HX_RectText();
//                infoText[i].SetBkColor(255, 255, 0);
//                infoText[i].SetTextColor(0, 0, 0);
//                infoText[i].SetTextStyle(12, FormatStyle.DirectionLeftToRight, true, "宋体");
//                infoText[i].SetTextRect(faultNoText[i].RectPosition.Right, faultNoText[i].RectPosition.Y, 345, 26);

//                hapenedTimeText[i] = new HX_RectText();
//                hapenedTimeText[i].SetBkColor(255, 255, 0);
//                hapenedTimeText[i].SetTextColor(0, 0, 0);
//                hapenedTimeText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
//                hapenedTimeText[i].SetTextRect(infoText[i].RectPosition.Right, infoText[i].RectPosition.Y, 150, 26);

//                endedTimeText[i] = new HX_RectText();
//                endedTimeText[i].SetBkColor(255, 255, 0);
//                endedTimeText[i].SetTextColor(0, 0, 0);
//                endedTimeText[i].SetTextStyle(12, FormatStyle.Center, true, "宋体");
//                endedTimeText[i].SetTextRect(hapenedTimeText[i].RectPosition.Right, hapenedTimeText[i].RectPosition.Y, 150, 26);
//            }
//        }

//        public void GetValue()
//        {
//            //设置标题
//            HX_Common.H_Title.SetText("历史故障");

//            HX_Common.Button_Text[0].SetText("");
//            HX_Common.Button_Text[1].SetText("");
//            HX_Common.Button_Text[2].SetText("");
//            HX_Common.Button_Text[3].SetText("");
//            HX_Common.Button_Text[4].SetText("");
//            HX_Common.Button_Text[5].SetText("");
//            HX_Common.Button_Text[6].SetText("当前故障");
//            HX_Common.Button_Text[7].SetText("所有故障");
//            HX_Common.Button_Text[8].SetText("历史故障");
//            HX_Common.Button_Text[9].SetText("主界面");

//            for (int i = 0; i < 10; i++)
//            {
//                if (i == 8)
//                {
//                    HX_Common.Button_Text[i].SetBkColor(255, 255, 0);
//                    HX_Common.Button_Text[i].SetTextColor(0, 0, 0);
//                }
//                else
//                {
//                    HX_Common.Button_Text[i].SetBkColor(0, 0, 0);
//                    HX_Common.Button_Text[i].SetTextColor(255, 255, 255);
//                }
//            }

//            //按钮状态更新
//            for (int i = 0; i < 10; i++)
//            {
//                IsNumButtonDown[i] = BoolList[UIObj.InBoolList[i]];
//            }

//            //导航按钮
//            for (int i = 0; i < 2; i++)
//            {
//                isPageButtonDown[i] = BoolList[UIObj.InBoolList[i + 10]];
//            }

//            for (int i = 0; i < 16; i++)
//            {
//                if (i + page * 16 < FaultRefresh.totalFaults.Count)
//                {
//                    switch ((int)FaultRefresh.totalFaults[i + page * 16].Level)
//                    {
//                        case 1:
//                            levelText[i].SetText("A");
//                            break;
//                        case 2:
//                            levelText[i].SetText("B");
//                            break;
//                        case 3:
//                            levelText[i].SetText("C");
//                            break;
//                        case 4:
//                            levelText[i].SetText("D");
//                            break;
//                        default:
//                            levelText[i].SetText("NULL");
//                            break;
//                    }

//                    trainNoText[i].SetText(FaultRefresh.totalFaults[i + page * 16].FaultCategory);
//                    faultNoText[i].SetText(FaultRefresh.totalFaults[i + page * 16].FaultID.ToString());
//                    infoText[i].SetText(FaultRefresh.totalFaults[i + page * 16].FaultText);
//                    hapenedTimeText[i].SetText(FaultRefresh.totalFaults[i + page * 16].HappenedTime.Month.ToString() + "-" + FaultRefresh.totalFaults[i + page * 16].HappenedTime.Day.ToString() +
//                                                 " " + FaultRefresh.totalFaults[i + page * 16].HappenedTime.ToLongTimeString());
//                    endedTimeText[i].SetText(FaultRefresh.totalFaults[i + page * 16].EndedTime.Month.ToString() + "-" + FaultRefresh.totalFaults[i + page * 16].EndedTime.Day.ToString() +
//                                                 " " + FaultRefresh.totalFaults[i + page * 16].EndedTime.ToLongTimeString());
//                }
//                else
//                {
//                    levelText[i].SetText(" ");
//                    trainNoText[i].SetText(" ");
//                    faultNoText[i].SetText(" ");
//                    infoText[i].SetText(" ");
//                    hapenedTimeText[i].SetText(" ");
//                    endedTimeText[i].SetText(" ");
//                }
//            }
//        }

       

//        public void DrawOn(Graphics g)
//        {
//            //底部导航按钮绘制
//            for (int i = 0; i < 10; i++)
//            {
//                HX_Common.Button_Text[i].OnDraw(g);
//            }

//            #region 标题栏绘制
//            g.FillRectangle(grayBrush, titleRect);
//            g.DrawString("类别", HX_Common.font_12B, HX_Common.red_Brush, titleRect.X + 2, titleRect.Y + 4);
//            g.DrawString("车号", HX_Common.font_12B, HX_Common.blackBrush, titleRect.X + 35, titleRect.Y + 4);
//            g.DrawString("代码", HX_Common.font_12B, HX_Common.white_Brush, titleRect.X + 85, titleRect.Y + 4);
//            g.DrawString("故障内容", HX_Common.font_12B, HX_Common.blackBrush, titleRect.X + 325, titleRect.Y + 4);
//            g.DrawString("故障发生时间", HX_Common.font_12B, HX_Common.dead_Brush, titleRect.X + 500, titleRect.Y + 4);
//            g.DrawString("故障结束时间", HX_Common.font_12B, HX_Common.dead_Brush, titleRect.X + 660, titleRect.Y + 4);
//            #endregion

//            for (int i = 0; i < 16; i++)
//            {
//                levelText[i].OnDraw(g);
//                trainNoText[i].OnDraw(g);
//                faultNoText[i].OnDraw(g);
//                infoText[i].OnDraw(g);
//                hapenedTimeText[i].OnDraw(g);
//                endedTimeText[i].OnDraw(g);
//            }

//            g.DrawImage(imageList[0], HX_Common.recposition.X + 773, HX_Common.recposition.Y + 62, 25, 45);
//            g.DrawImage(imageList[1], HX_Common.recposition.X + 773, levelText[15].RectPosition.Bottom - 25, 25, 25);
//        }

//        /// <summary>
//        /// 响应硬件按钮按下事件
//        /// </summary>
//        public void ButtonDownEvent()
//        {
//            for (int i = 0; i < 10; i++)
//            {
//                if (IsNumButtonDown[i])
//                {
//                    switch (i)
//                    {
//                        case 0:
//                            break;
//                        case 1:
//                            break;
//                        case 2:
//                            break;
//                        case 3:
//                            break;
//                        case 4:
//                            break;
//                        case 5:

//                            break;
//                        case 6://按下7切换到当前故障视图
//                            append_postCmd(CmdType.ChangePage, 13, 0, 0);
//                            break;
//                        case 7://按下8切换到所有故障视图
//                            append_postCmd(CmdType.ChangePage, 14, 0, 0);
//                            break;
//                        case 8://按下7切换到历史故障视图
//                            append_postCmd(CmdType.ChangePage, 15, 0, 0);
//                            break;
//                        case 9://返回主界面
//                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
//                            break;
//                        default:
//                            break;
//                    }
//                }
//            }

//            for (int i = 0; i < 2; i++)
//            {
//                if (isPageButtonDown[i])
//                {
//                    if (i == 0)
//                    {
//                        if (page > 0)
//                        {
//                            page--;
//                        }
//                    }
//                    else
//                    {
//                        if (FaultRefresh.totalFaults.Count % 16 == 0)
//                        {
//                            if (page < FaultRefresh.totalFaults.Count / 16 - 1)
//                            {
//                                page++;
//                            }
//                        }
//                        else
//                        {
//                            if (page < FaultRefresh.totalFaults.Count / 16)
//                            {
//                                page++;
//                            }
//                        }

//                    }
//                }
//            }
//        }
//    }
//}