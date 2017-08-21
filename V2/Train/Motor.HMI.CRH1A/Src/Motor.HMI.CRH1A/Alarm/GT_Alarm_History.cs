using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls.Button;
using MMI.Facility.Interface.Attribute;
using Motor.HMI.CRH1A.Alarm.Fault;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.Alarm
{
    /// <summary>
    ///  历史事件 此菜单显示的是车上以探出的警报列表，当以司机登录时显示的是 3 和4
    /// 既（A-警报和B-警报,可以从主菜单进入此菜单
    /// </summary>  
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class GT_Alarm_History : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("报警记录");//菜单标题
        public Rectangle Recposition = new Rectangle(0, 178, 790, 277);
        public CRH1AButton[] GButton = new CRH1AButton[4];
        public Image[] Image = new Image[2];
        public string[] Strtitle = { "单", "车", "日期/时间", "代", "类", "描述" };//暂时有12条 可以根据需要添加和删除
        public Pen Pen = new Pen(Color.FromArgb(210, 210, 210));
        public Rectangle[] Rect = new Rectangle[12];
        public SolidBrush Brush = new SolidBrush(Color.FromArgb(255, 255, 225));
        public SolidBrush Whitebrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public SolidBrush Blackbrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        public Pen RectPen = new Pen(Color.FromArgb(55, 55, 55), 2);
        public Font Font = new Font("Arial", 11);
        public Font Smallfont = new Font("Arial", 9);
        public Point[,] InfoPoint = new Point[10, 6];
        public int[] Point = new int[6] { 35, 80, 125, 245, 290, 350 };
        // public Dictionary<int, ExceptionData> AlarmList = new Dictionary<int, ExceptionData>();

        private List<ExceptionData> m_EventFaultByCount = new List<ExceptionData>();
        private int m_SelectedCarId = -1;
        //public Sor<int, ExceptionData> EventByCount = new Dictionary<int, ExceptionData>();
        public int CurPage = 0;//当前页号 第一页为0
        public int NumPerPage = 10;//每页显示10条记录
        public int CurSelectIndex = 0;//当前索引号
        public override string GetInfo()
        {
            return "警报汇总";
        }

        public override bool Initialize()
        {
            //3
            if (UIObj.ParaList.Count >= 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    Image[i] = System.Drawing.Image.FromFile(RecPath + "//" + UIObj.ParaList[i]);
                }
            }
            InitData();

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            SelectFaultByCarId();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        /// <summary>
        /// 按车厢号对所有
        /// </summary>
        private void SelectFaultByCarId()
        {
            m_EventFaultByCount.Clear();
            m_SelectedCarId = GT_Trian2.SelectedCarId;
            if (m_SelectedCarId > 0 && m_SelectedCarId <= 8)//若选择车厢号在1和8之间则选择显示1到8之间的历史故障
            {
                foreach (ExceptionData exData in GT_GalobalFaultManage.Instance.FaultHistory)
                {
                    if (exData.ExCarId == m_SelectedCarId)
                    {
                        m_EventFaultByCount.Add(exData);
                    }
                }
            }
            else
            {
                foreach (ExceptionData exData in GT_GalobalFaultManage.Instance.FaultHistory)
                {
                    m_EventFaultByCount.Add(exData);
                }
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitData()
        {
            GButton[0] = new CRH1AButton();
            GButton[0].SetButtonRect(Recposition.X + 710, Recposition.Y - 75, 80, 60);
            GButton[0].SetButtonColor(192, 192, 192);
            GButton[0].SetButtonText("整车");

            GButton[1] = new CRH1AButton();
            GButton[1].SetButtonColor(192, 192, 192);
            GButton[1].SetButtonRect(Recposition.X, Recposition.Y + 335, 80, 50);

            GButton[2] = new CRH1AButton();
            GButton[2].SetButtonColor(192, 192, 192);
            GButton[2].SetButtonRect(Recposition.X + 90, Recposition.Y + 335, 80, 50);

            GButton[3] = new CRH1AButton();
            GButton[3].SetButtonColor(192, 192, 192);
            GButton[3].SetButtonRect(Recposition.X + 400, Recposition.Y + 335, 80, 50);
            GButton[3].SetButtonText("更新");


            /////每行区域矩形框初始化
            for (int i = 0; i < 11; i++)
            {
                Rect[i] = new Rectangle(Recposition.X, Recposition.Y + 25 * i, 800, 25);
            }
            Rect[11] = new Rectangle(Recposition.X, Recposition.Y, 30, 275);

            // 故障信息显示区域初始化
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    InfoPoint[i, j] = new Point(Point[j] + Recposition.X, Recposition.Y + i * 25 + 30);
                }
            }
            GlobalInfo.Instance.ButtonStatusChange += () =>
            {
                foreach (var button in GButton)
                {
                    button.IsEnable = GlobalInfo.Instance.ButtonEnable;
                }
            };
        }
        public void DrawOn(Graphics g)
        {
            #region 绘制基本元素
            //绘制菜单标题
            Title.OnDraw(g);

            //绘制底部按钮
            for (int i = 0; i < 4; i++)
            {
                GButton[i].OnDraw(g);
            }
            g.DrawImage(Image[0], GButton[1].OutLineRectangle);
            g.DrawImage(Image[1], GButton[2].OutLineRectangle);

            //填充颜色
            g.DrawRectangle(RectPen, Recposition);
            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                {
                    g.FillRectangle(Brush, Rect[i]);
                }
                else
                {
                    g.FillRectangle(Whitebrush, Rect[i]);
                }
            }
            g.FillRectangle(Brush, Rect[11]);

            //添加标题和编号
            for (int i = 0; i < 10; i++)//添加编号
            {
                if (i < 9)
                {
                    g.DrawString((i + 1).ToString(), Font, Blackbrush, Recposition.X + 10, Recposition.Y + (i + 1) * 25 + 5);
                }
                else
                {
                    g.DrawString("10", Font, Blackbrush, Recposition.X + 5, Recposition.Y + (i + 1) * 25 + 5);
                }
            }
            g.DrawString("单", Font, Blackbrush, Recposition.X + 38, Recposition.Y + 5);
            g.DrawString("车", Font, Blackbrush, Recposition.X + 85, Recposition.Y + 5);
            g.DrawString("日期/时间", Font, Blackbrush, Recposition.X + 130, Recposition.Y + 5);
            g.DrawString("代", Font, Blackbrush, Recposition.X + 250, Recposition.Y + 5);
            g.DrawString("类", Font, Blackbrush, Recposition.X + 290, Recposition.Y + 5);
            g.DrawString("描述", Font, Blackbrush, Recposition.X + 450, Recposition.Y + 5);
            #endregion

            //添加故障信息
            if (m_EventFaultByCount.Count > 0)
            {
                for (int i = CurSelectIndex; i <= m_EventFaultByCount.Count - 1 && i <= CurSelectIndex + 9; i++)
                {
                    g.DrawString(m_EventFaultByCount[i].ExCarUnit.ToString(), Smallfont, Blackbrush, InfoPoint[i - CurSelectIndex, 0]);
                    g.DrawString(m_EventFaultByCount[i].ExCarId.ToString("00"), Smallfont, Blackbrush, InfoPoint[i - CurSelectIndex, 1]);
                    g.DrawString(m_EventFaultByCount[i].Startdate.ToString(), Smallfont, Blackbrush, InfoPoint[i - CurSelectIndex, 2]);
                    g.DrawString(m_EventFaultByCount[i].ExId.ToString(), Smallfont, Blackbrush, InfoPoint[i - CurSelectIndex, 3]);
                    g.DrawString(m_EventFaultByCount[i].ExTypeInfo, Smallfont, Blackbrush, InfoPoint[i - CurSelectIndex, 4]);
                    g.DrawString(m_EventFaultByCount[i].ExText, Smallfont, Blackbrush, InfoPoint[i - CurSelectIndex, 5]);
                }
            }

            //绘制网格
            for (int i = 0; i <= 275; i += 25)
            {
                g.DrawLine(Pen, Recposition.X, Recposition.Y + i, Recposition.X + 800, Recposition.Y + i);
            }
            g.DrawLine(Pen, Recposition.X + 30, Recposition.Y, Recposition.X + 30, Recposition.Y + 275);
            g.DrawLine(Pen, Recposition.X + 75, Recposition.Y, Recposition.X + 75, Recposition.Y + 275);
            g.DrawLine(Pen, Recposition.X + 120, Recposition.Y, Recposition.X + 120, Recposition.Y + 275);
            g.DrawLine(Pen, Recposition.X + 240, Recposition.Y, Recposition.X + 240, Recposition.Y + 275);
            g.DrawLine(Pen, Recposition.X + 290, Recposition.Y, Recposition.X + 290, Recposition.Y + 275);
            g.DrawLine(Pen, Recposition.X + 350, Recposition.Y, Recposition.X + 350, Recposition.Y + 275);

            //添加页码
            g.DrawString("页码：" + CurPage, Font, Whitebrush, Recposition.X + 420, Recposition.Y - 30);

        }
        protected override bool OnMouseDown(Point point)
        {
            OnButtonDown(point.X, point.Y);
            return true;
        }
        protected override bool OnMouseUp(Point point)
        {
            OnButtonUp(point.X, point.Y);
            return true;
        }
        public void OnButtonDown(int x, int y)
        {
            //  按 钮 响 应 事 件
            for (int i = 0; i < 4; i++)
            {
                if (GButton[i].Contains(x, y) && GButton[i].IsEnable)
                {
                    GButton[i].OnButtonDown();
                }
            }
        }

        public void OnButtonUp(int x, int y)
        {
            for (int i = 0; i < 4; i++)
            {

                if (GButton[i].Contains(x, y) && GButton[i].IsEnable)
                {
                    switch (i)
                    {
                        case 0:
                            GT_Trian2.SelectedCarId = -1;
                            for (int j = 0; j < 8; j++)
                            {
                                GT_Trian2.Selected[i] = false;
                            }
                            GT_Trian2.Selected[8] = true;
                            CurPage = 0;
                            break;
                        case 1:

                            if (CurSelectIndex + 9 < m_EventFaultByCount.Count)
                            {
                                CurPage++;
                            }
                            CurSelectIndex = NumPerPage * CurPage + 1;
                            break;
                        case 2:
                            if (CurPage > 0)
                            {
                                CurPage--;
                            }
                            CurSelectIndex = NumPerPage * CurPage + 1;
                            break;
                        case 3:
                            CurPage = 0;
                            CurSelectIndex = NumPerPage * CurPage + 1;
                            break;
                    }
                    GButton[i].OnButtonUp();
                }
            }

        }
    }
}