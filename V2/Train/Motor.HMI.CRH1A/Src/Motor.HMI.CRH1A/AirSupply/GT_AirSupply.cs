using System;
using System.Drawing;
using System.Resources;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;

namespace Motor.HMI.CRH1A.AirSupply
{
    [GksDataType(DataType.isMMIObjectClass)]
    class GT_AirSupply : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("供风");//菜单标题
        public Rectangle Recposition = new Rectangle(0, 100, 800, 100);
        public CRH1AButton[] GButton = new CRH1AButton[2];//底部按钮
        public GDIRectText[] GText = new GDIRectText[3];//显示风压
        public Rectangle[] Rect = new Rectangle[3];//页面边框 
        Rectangle m_Rect;//页脚区域
        public SolidBrush Recbrush = new SolidBrush(Color.FromArgb(119, 136, 153));//背景画刷
        public SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 255, 0));//绿色画刷
        public SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));//红色画刷
        public SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(0, 0, 255));//蓝色画刷
        public SolidBrush BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));//黑色画刷 
        public SolidBrush GrayBrush = new SolidBrush(Color.FromArgb(121, 121, 121));//灰色画刷 
        public Pen Rectpen = new Pen(Color.FromArgb(209, 226, 242), 3);//背景画笔

        public Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0), 2);
        public Pen LinePen = new Pen(Color.FromArgb(121, 121, 121), 3);//粗条画笔
        public Pen SLinePen = new Pen(Color.FromArgb(121, 121, 121), 1);//细线条画笔
        public Pen MLinePen = new Pen(Color.FromArgb(121, 121, 121), 2);//中线条画笔
        AYaShuoJ[] m_Ayashuoji = new AYaShuoJ[2];//辅助压缩机
        MYaShuoJi[] m_Myashuoji = new MYaShuoJi[3];//主压缩机
        Presure[] m_Presure = new Presure[3];//压力感应器
        public Font Strfont = new Font("Arial", 11);
        public Rectangle[] Rectx = new Rectangle[2];
        public Point[] C1 = new Point[16];
        public Point[] C2 = new Point[16];
        public Point[] C3 = new Point[16];
        public bool[] Valueb = new bool[27];
        public float[] Valuef = new float[3];

        private int m_SelectedIndex = -1;

        private AirSupplyBtnMgr m_AirSupplyBtnMgr = new AirSupplyBtnMgr();

        public override string GetInfo()
        {
            return "风压状态";
        }

        public override bool Initialize()
        {
            //3
            InitData();

            m_AirSupplyBtnMgr.MCutInOrOutDown += (sender, args) => PostBoolMsm(sender as AirSupplyBtnMgr, 1);
            m_AirSupplyBtnMgr.MManulOpenOrCloseDown += (sender, args) => PostBoolMsm(sender as AirSupplyBtnMgr, 1);
            m_AirSupplyBtnMgr.PreCutInOrOutDown += (sender, args) => PostBoolMsm(sender as AirSupplyBtnMgr, 1);

            m_AirSupplyBtnMgr.MCutInOrOutUp+= (sender, args) => PostBoolMsm(sender as AirSupplyBtnMgr, 0);
            m_AirSupplyBtnMgr.MManulOpenOrCloseUp += (sender, args) => PostBoolMsm(sender as AirSupplyBtnMgr, 0);
            m_AirSupplyBtnMgr.PreCutInOrOutUp += (sender, args) => PostBoolMsm(sender as AirSupplyBtnMgr, 0);

            return true;
        }

        private void PostBoolMsm(AirSupplyBtnMgr air, int value)
        {
            OnPost(CmdType.SetBoolValue,
                    UIObj.OutBoolList[AirSupplyOutBoolIdxAdpt.GetIdx(air, m_SelectedIndex)], value, 0);
        }

        protected override void NavigateTo(ViewConfig to)
        {
            //if (nParaA == 2)
            {
                ReselectIndex(-1);
            }
        }

        private void ReselectIndex(int idx)
        {
            if (idx == m_SelectedIndex)
            {
                m_SelectedIndex = -1;
            }
            else
            {
                m_SelectedIndex = idx;
            }
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            ReFreshData();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 27)
            {
                for (int i = 0; i < 27; i++)
                {
                    Valueb[i] = BoolList[UIObj.InBoolList[i]];
                }
            }
            if (UIObj.InFloatList.Count >= 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    Valuef[i] = FloatList[UIObj.InFloatList[i]];
                }
            }

        }

        public void ReFreshData()
        {
            for (int i = 0; i < 3; i++)
            {
                //////////////////////////////////////////////
                //-ycl-
                //取整
                ////////////////////////////////////////////
                GText[i].SetText(Convert.ToInt32(Valuef[i]).ToString());
            }

            GButton[0].SetButtonTextColor(m_SelectedIndex >= 5 ? BlackBrush : GrayBrush);
            GButton[1].SetButtonTextColor(m_SelectedIndex >= 0 && m_SelectedIndex < 5 ? BlackBrush : GrayBrush);

            for (int i = 0; i < 2; i++)//辅助压缩机的状态设置
            {
                if (Valueb[12 + i])
                {
                    m_Ayashuoji[i].S = AYaShuoJ.Status.Working;

                }
                else if (Valueb[14 + i])
                {
                    m_Ayashuoji[i].S = AYaShuoJ.Status.Closed;
                }
                else if (Valueb[16 + i])
                {
                    m_Ayashuoji[i].S = AYaShuoJ.Status.Unknow;
                }
                else
                {
                    m_Ayashuoji[i].S = AYaShuoJ.Status.Unknow;
                }
            }

            //住压缩机状态设置
            for (int i = 0; i < 3; i++)
            {
                if (Valueb[i])
                {
                    m_Myashuoji[i].S = MYaShuoJi.Status.Working;
                }
                else if (Valueb[i + 3])
                {
                    m_Myashuoji[i].S = MYaShuoJi.Status.Closed;
                }
                else if (Valueb[i + 6])
                {
                    m_Myashuoji[i].S = MYaShuoJi.Status.CutOut;
                }
                else if (Valueb[i + 9])
                {
                    m_Myashuoji[i].S = MYaShuoJi.Status.Unknow;
                }
                else
                {
                    m_Myashuoji[i].S = MYaShuoJi.Status.Unknow;
                }

            }
            // 压力感应器状态设置

            for (int i = 0; i < 3; i++)
            {
                if (Valueb[i + 18])
                {
                    m_Presure[i].S = Presure.Status.Well;
                }
                else if (!Valueb[i + 18] && !Valueb[i + 21] && !Valueb[i + 24])
                {
                    m_Presure[i].S = Presure.Status.Fault;
                }
                else if (Valueb[i + 21])
                {
                    m_Presure[i].S = Presure.Status.CutOut;
                }
                else if (Valueb[i + 24])
                {
                    m_Presure[i].S = Presure.Status.Unknow;
                }
                else
                {
                    m_Presure[i].S = Presure.Status.Unknow;
                }
            }
        }


        public void InitData()
        {
            #region ;;;;;;;;;;;;;;;; 按钮初始化:::::::::::::::::::::::
            GButton[0] = new CRH1AButton();
            GButton[0].SetButtonRect(Recposition.X + 290, Recposition.Y + 285, 90, 50);
            GButton[0].SetButtonColor(192, 192, 192);
            GButton[0].SetButtonText("切入/切断");
            GButton[0].SetButtonTextColor(new SolidBrush(Color.Gray));

            GButton[1] = new CRH1AButton();
            GButton[1].SetButtonRect(Recposition.X + 450, Recposition.Y + 285, 90, 50);
            GButton[1].SetButtonColor(192, 192, 192);
            GButton[1].SetButtonText("接通/切断");
            GButton[1].SetButtonTextColor(new SolidBrush(Color.Gray));

            #endregion

            #region :::::::::::::::::::边 框 的位 置;;;;;;;;;;;;;;;;;;
            for (int i = 0; i < 3; i++)
            {
                Rect[i] = new Rectangle(Recposition.X + i * 155 + 100, Recposition.Y, 120, 260);
            }
            #endregion

            #region :::::::::::::::::::辅助压缩机显示位置及初始化;;;;;;;;;;;;;;;;;;;
            m_Ayashuoji[0] = new AYaShuoJ(Rect[0].X + 10, Rect[0].Y + 50, 40, 40);
            m_Ayashuoji[1] = new AYaShuoJ(Rect[2].X + Rect[2].Width - 60, Rect[2].Y + 50, 40, 40);
            #endregion

            #region ::::::::::::::::::::页 脚 信 息 区 域 位 置::::::::::::::::::::::::::::::::
            m_Rect = new Rectangle(Recposition.X, Recposition.Y + 270, 790, 80);
            m_AirSupplyBtnMgr.PageFootRectangle = m_Rect;
            m_AirSupplyBtnMgr.Init();
            #endregion



            #region :::::::::::::::::::主 压 缩 机 显 示 位 置 及 初 始 化 位 置::::::::::::::::::::::::::::::::
            for (int i = 0; i < 3; i++)
            {
                if (i < 1)
                {
                    m_Myashuoji[i] = new MYaShuoJi(Rect[i].X + 10, Rect[i].Y + 110, 40, 40);
                }
                else
                {
                    m_Myashuoji[i] = new MYaShuoJi(Rect[i].X + Rect[i].Width - 60, Rect[i].Y + 110, 40, 40);
                }

            }
            #endregion

            #region ::::::::::::::::::::压 力 传 感 器 位 置 及 初 始 化::::::::::::::::::::::::::::::::

            for (int i = 0; i < 3; i++)
            {
                if (i < 1)
                {
                    m_Presure[i] = new Presure(Rect[i].X + 10, Rect[i].Y + 170, 40, 40);
                }

                else
                {
                    m_Presure[i] = new Presure(Rect[i].X + Rect[i].Width - 60, Rect[i].Y + 170, 40, 40);
                }
            }

            #endregion
            #region :::::::::::::::::::::::::风 压 显 示 位 置:::::::::::::::;;;;;:::::::
            for (int i = 0; i < 3; i++)
            {
                GText[i] = new GDIRectText();
                GText[i].SetBkColor(192, 192, 192);
                GText[i].SetLinePen(131, 131, 131, 1);
                GText[i].Isdrawrectfrm = true;
                GText[i].SetTextColor(0, 128, 0);
                GText[i].SetTextStyle(10, FormatStyle.Center, true, "Arial");
                if (i < 1)
                {
                    GText[i].SetTextRect(Rect[i].X + 5, Rect[i].Y + 220, 40, 20);
                }
                else
                {
                    GText[i].SetTextRect(Rect[i].X + Rect[i].Width - 80, Rect[i].Y + 220, 40, 20);
                }


            }

            #endregion

            #region :::::::::::::::::::::::::各 线 段 点 坐 标 设 置:::::::::::::::;;;;;:::::::
            //1
            Rectx[0] = new Rectangle(Rect[0].X + 75, Rect[0].Y + 60, 32, 20);
            C1[0] = new Point(Rect[0].X + 75, Rect[0].Y + 2);
            C1[1] = new Point(Rect[0].X + 115, Rect[0].Y + 15);
            C1[2] = new Point(Rect[0].X + 90, Rect[0].Y + 30);
            C1[3] = new Point(Rect[0].X + 75, Rect[0].Y + 30);
            C1[4] = new Point(Rect[0].X + 115, Rect[0].Y + 30);
            C1[5] = new Point(Rect[0].X + 75, Rect[0].Y + 70);
            C1[6] = new Point(Rect[0].X + 50, Rect[0].Y + 70);
            C1[7] = new Point(Rect[0].X + 90, Rect[0].Y + 130);
            C1[8] = new Point(Rect[0].X + 50, Rect[0].Y + 130);
            C1[9] = new Point(Rect[0].X + 90, Rect[0].Y + 190);
            C1[10] = new Point(Rect[0].X + 50, Rect[0].Y + 190);
            C1[11] = new Point(Rect[0].X + 90, Rect[0].Y + 247);
            C1[12] = new Point(Rect[0].X, Rect[0].Y + 247);
            C1[13] = new Point(Rect[0].X + 120, Rect[0].Y + 247);
            C1[14] = new Point(Rect[0].X + 90, Rect[0].Y + 60);
            C1[15] = new Point(Rect[0].X + 90, Rect[0].Y + 80);

            //2
            C2[0] = new Point(Rect[1].X, Rect[1].Y + 247);
            C2[1] = new Point(Rect[1].X + 120, Rect[1].Y + 247);
            C2[2] = new Point(Rect[1].X + 30, Rect[1].Y + 130);
            C2[3] = new Point(Rect[1].X + 60, Rect[1].Y + 130);
            C2[4] = new Point(Rect[1].X + 30, Rect[1].Y + 190);
            C2[5] = new Point(Rect[1].X + 60, Rect[1].Y + 190);
            C2[6] = new Point(Rect[1].X + 30, Rect[1].Y + 130);
            C2[7] = new Point(Rect[1].X + 30, Rect[1].Y + 247);


            //3
            Rectx[1] = new Rectangle(Rect[2].X + 13, Rect[2].Y + 60, 32, 20);
            C3[0] = new Point(Rect[2].X + 45, Rect[2].Y + 2);
            C3[1] = new Point(Rect[2].X + 5, Rect[2].Y + 15);
            C3[2] = new Point(Rect[2].X + 30, Rect[2].Y + 30);
            C3[3] = new Point(Rect[2].X + 45, Rect[2].Y + 30);
            C3[4] = new Point(Rect[2].X + 5, Rect[2].Y + 30);
            C3[5] = new Point(Rect[2].X + 45, Rect[2].Y + 70);
            C3[6] = new Point(Rect[2].X + 60, Rect[2].Y + 70);
            C3[7] = new Point(Rect[2].X + 30, Rect[2].Y + 130);
            C3[8] = new Point(Rect[2].X + 60, Rect[2].Y + 130);
            C3[9] = new Point(Rect[2].X + 30, Rect[2].Y + 190);
            C3[10] = new Point(Rect[2].X + 60, Rect[2].Y + 190);
            C3[11] = new Point(Rect[2].X + 30, Rect[2].Y + 247);
            C3[12] = new Point(Rect[2].X, Rect[2].Y + 247);
            C3[13] = new Point(Rect[2].X + 120, Rect[2].Y + 247);
            C3[14] = new Point(Rect[2].X + 30, Rect[2].Y + 60);
            C3[15] = new Point(Rect[2].X + 30, Rect[2].Y + 80);

            #endregion
        }
        public void DrawOn(Graphics g)
        {

            //绘制菜单标题
            Title.OnDraw(g);

            //绘制边框
            for (int i = 0; i < 3; i++)
            {
                g.DrawRectangle(SLinePen, Rect[i]);
            }
            g.DrawString("02", new Font("Arial", 13), GrayBrush, Rect[0].X + 20, Rect[0].Y + 25);
            g.DrawString("05", new Font("Arial", 13), GrayBrush, Rect[1].X + 70, Rect[0].Y + 25);
            g.DrawString("07", new Font("Arial", 13), GrayBrush, Rect[2].X + 70, Rect[0].Y + 25);
            //辅助压缩机
            for (int i = 0; i < 2; i++)
            {
                m_Ayashuoji[i].OnDraw(g);
            }
            //主压缩机 压力感应器
            for (int i = 0; i < 3; i++)
            {
                m_Myashuoji[i].OnDraw(g);
                m_Presure[i].OnDraw(g);
            }

            //显示风压
            for (int i = 0; i < 3; i++)
            {
                GText[i].OnDraw(g);
                g.DrawString("kPa", Strfont, BlackBrush, GText[i].OutLineRectangle.X + GText[i].OutLineRectangle.Width, GText[i].OutLineRectangle.Y + 3);
            }


            //页脚区域绘制

            g.FillRectangle(Recbrush, m_Rect);
            g.DrawRectangle(Rectpen, m_Rect);
            m_AirSupplyBtnMgr.OnDraw(g);
            //绘制底部按钮

            //for (int i = 0; i < 2; i++)
            //{
            //    G_Button[i].OnDraw(g);
            //}
            //会直线条

            //1
            g.DrawRectangle(MLinePen, Rectx[0]);
            g.DrawLine(SLinePen, Rectx[0].X, Rectx[0].Y, Rectx[0].X + Rectx[0].Width, Rectx[0].Y + Rectx[0].Height);
            g.DrawLine(SLinePen, Rectx[0].X, Rectx[0].Y + Rectx[0].Height, Rectx[0].X + Rectx[0].Width, Rectx[0].Y);
            g.DrawLine(LinePen, C1[0], C1[1]);
            g.DrawLine(LinePen, C1[1], C1[2]);
            g.DrawLine(LinePen, C1[3], C1[4]);
            g.DrawLine(LinePen, C1[5], C1[6]);
            g.DrawLine(LinePen, C1[7], C1[8]);
            g.DrawLine(LinePen, C1[9], C1[10]);
            g.DrawLine(LinePen, C1[2], C1[14]);
            g.DrawLine(LinePen, C1[15], C1[11]);
            g.DrawLine(LinePen, C1[12], C1[13]);

            //2
            g.DrawLine(LinePen, C2[0], C2[1]);
            g.DrawLine(LinePen, C2[2], C2[3]);
            g.DrawLine(LinePen, C2[4], C2[5]);
            g.DrawLine(LinePen, C2[6], C2[7]);
            //3
            g.DrawRectangle(MLinePen, Rectx[1]);
            g.DrawLine(SLinePen, Rectx[1].X, Rectx[1].Y, Rectx[1].X + Rectx[1].Width, Rectx[1].Y + Rectx[1].Height);
            g.DrawLine(SLinePen, Rectx[1].X, Rectx[1].Y + Rectx[1].Height, Rectx[1].X + Rectx[1].Width, Rectx[1].Y);
            g.DrawLine(LinePen, C3[0], C3[1]);
            g.DrawLine(LinePen, C3[1], C3[2]);
            g.DrawLine(LinePen, C3[3], C3[4]);
            g.DrawLine(LinePen, C3[5], C3[6]);
            g.DrawLine(LinePen, C3[7], C3[8]);
            g.DrawLine(LinePen, C3[9], C3[10]);
            g.DrawLine(LinePen, C3[2], C3[14]);
            g.DrawLine(LinePen, C3[15], C3[11]);
            g.DrawLine(LinePen, C3[12], C3[13]);

            //绘制选中的切除元素
            if (m_SelectedIndex >= 0 && m_SelectedIndex < 2)
            {
                g.DrawRectangle(BlackPen, m_Ayashuoji[m_SelectedIndex].Rect);
            }
            else if (m_SelectedIndex >= 2 && m_SelectedIndex < 5)
            {
                g.DrawRectangle(BlackPen, m_Myashuoji[m_SelectedIndex - 2].Rect);
            }
            else if (m_SelectedIndex >= 5 && m_SelectedIndex < 8)
            {
                g.DrawRectangle(BlackPen, m_Presure[m_SelectedIndex - 5].Rect);
            }
        }

        protected override bool OnMouseDown(Point point)
        {
            OnButtonDown(point);
            return true;
        }
        protected override bool OnMouseUp(Point point)
        {
            OnButtonUp(point);
            return true;
        }

        public void OnButtonDown(Point point)
        {
            m_AirSupplyBtnMgr.OnMouseDown(point);

            #region 界面元素点击事件
            //辅助空压机点击事件
            for (int index = 0; index < 2; index++)
            {
                if (m_Ayashuoji[index].Rect.Contains(point))
                {
                    ReselectIndex(index);
                    m_AirSupplyBtnMgr.SelecControlType = m_SelectedIndex == -1 ? AirSupplyInnerControlType.Null : AirSupplyInnerControlType.AYaShuoJ;
                    return;
                }
            }

            //主空压机点击事件
            for (int index = 0; index < 3; index++)
            {
                if (m_Myashuoji[index].Rect.Contains(point))
                {
                    ReselectIndex(index + 2);
                    m_AirSupplyBtnMgr.SelecControlType = m_SelectedIndex == -1 ? AirSupplyInnerControlType.Null : AirSupplyInnerControlType.MYaShuoJi;
                    return;
                }
            }

            for (int index = 0; index < 3; index++)
            {
                if (m_Presure[index].Rect.Contains(point))
                {
                    ReselectIndex(index + 5);
                    m_AirSupplyBtnMgr.SelecControlType = m_SelectedIndex == -1 ? AirSupplyInnerControlType.Null : AirSupplyInnerControlType.Presure;
                    return;
                }
            }
            #endregion

            
        }

        public void OnButtonUp(Point point)
        {
            m_AirSupplyBtnMgr.OnMouseUp(point);
        }
    }
}

