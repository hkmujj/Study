using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Motor.HMI.CRH1A.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls.Button;

namespace Motor.HMI.CRH1A
{


    /// <summary>
    ///   返回互锁页面的导航按钮
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class GT_InTerLock_Button : CRH1BaseClass
    {
        public Rectangle Recposition = new Rectangle(2, 513, 790, 300);
        public CRH1AButton GButton;


        public override string GetInfo()
        {
            return "返回互锁页面按钮";
        }

        public override bool Initialize()
        {
            InitData();

            return true;
        }
        public override void paint(Graphics dcGs)
        {


            OnPaint((dcGs));
            base.paint(dcGs);
        }

        public void InitData()
        {

            GButton = new CRH1AButton();
            GButton.SetButtonRect(Recposition.X + 400, Recposition.Y, 90, 50);
            GButton.SetButtonText("互锁");
            GButton.SetButtonColor(192, 192, 192);

        }

        public void OnPaint(Graphics g)
        {

            //绘制菜单标题

            GButton.OnDraw(g);
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

        public void OnButtonDown(Point nPoint)
        {

            if (GButton.Contains(nPoint))
                GButton.OnButtonDown();

        }
        public void OnButtonUp(Point nPoint)
        {
            if (GButton.Contains(nPoint))
            {
                OnPost(CmdType.ChangePage, 19, 0, 0);
                GButton.OnButtonUp();
            }
        }

    }

    [GksDataType(DataType.isMMIObjectClass)]
    class GT_InterLock_System : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("互锁");//菜单标题 
        public Rectangle Recposition = new Rectangle(2, 140, 790, 300);
        public Pen BackPen = new Pen(Color.FromArgb(163, 180, 198), 3);
        public SolidBrush BackBrush = new SolidBrush(Color.FromArgb(119, 136, 153));

        public CRH1AButton[] GButton = new CRH1AButton[3];
        public bool[] TractionStatus = new bool[21];
        public bool[] SpeekStatus = new bool[5];
        public bool[] BrakeStatus = new bool[9];
        public bool[] Status = new bool[3];
        public override string GetInfo()
        {
            return "互锁";
        }


        public override bool Initialize()
        {
            InitData();

            return true;
        }
        public override void paint(Graphics dcGs)
        {
            GetValue();
            RefreshData();
            OnPaint((dcGs));
            base.paint(dcGs);
        }
        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 34)
            {
                for (int i = 0; i < 21; i++)
                {
                    TractionStatus[i] = BoolList[UIObj.InBoolList[i]];
                }

                for (int i = 0; i < 5; i++)
                {
                    SpeekStatus[i] = BoolList[UIObj.InBoolList[i + 21]];
                }
                for (int i = 0; i < 9; i++)
                {
                    BrakeStatus[i] = BoolList[UIObj.InBoolList[i + 25]];
                }

                BrakeStatus[0] = BoolList[UIObj.InBoolList[10]];
            }

        }
        public void InitData()
        {
            for (int i = 0; i < 3; i++)
            {
                GButton[i] = new CRH1AButton();
                GButton[i].SetButtonRect(Recposition.X + 140 + i * 180, Recposition.Y + 100, 110, 60);

            }
            GButton[0].SetButtonText("牵引封锁");
            GButton[1].SetButtonText("限速");
            GButton[2].SetButtonText("紧急制动");

        }

        public void RefreshData()
        {
            foreach (bool b in TractionStatus)
            {
                if (b)
                {
                    Status[0] = true;
                    break;
                }
                Status[0] = false;
            }
            foreach (bool b in SpeekStatus)
            {
                if (b)
                {
                    Status[1] = true;
                    break;
                }
                Status[1] = false;

            }
            foreach (bool b in BrakeStatus)
            {
                if (b)
                {
                    Status[2] = true;
                    break;
                }
                Status[2] = false;
            }
        }
        public void OnPaint(Graphics g)
        {
            Title.OnDraw(g);
            //绘制背景框
            g.FillRectangle(BackBrush, Recposition);
            g.DrawRectangle(BackPen, Recposition);

            for (int i = 0; i < 3; i++)
            {
                if (Status[i])
                {
                    GButton[i].SetButtonColor(255, 0, 0);
                }
                else
                {
                    GButton[i].SetButtonColor(0, 128, 0);
                }
                GButton[i].OnDraw(g);
            }



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
            for (int i = 0; i < 3; i++)
            {
                if (GButton[i].Contains(x, y))
                    GButton[i].OnButtonDown();
            }
        }

        public void OnButtonUp(int x, int y)
        {
            for (int i = 0; i < 3; i++)
            {
                if (GButton[i].Contains(x, y))
                {
                    if (i == 0)
                    {
                        OnPost(CmdType.ChangePage, 20, 0, 0);
                    }
                    else if (i == 1)
                    {
                        OnPost(CmdType.ChangePage, 21, 0, 0);
                    }
                    else
                    {
                        if (i == 2)
                        {
                            OnPost(CmdType.ChangePage, 22, 0, 0);
                        }

                    }
                    GButton[i].OnButtonUp();
                }
            }
        }
    }



    /// <summary>
    ///   牵引阻断互锁菜单 详细显示那些引起牵引受阻联锁的信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class Tractionblock : CRH1BaseClass
    {

        public GT_MenuTitle Title = new GT_MenuTitle("牵引封锁");//菜单标题
        public Rectangle Recposition = new Rectangle(2, 140, 790, 300);
        public Pen BackPen = new Pen(Color.FromArgb(163, 180, 198), 2);
        public SolidBrush BackBrush = new SolidBrush(Color.FromArgb(119, 136, 153));
        public SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
        public SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 128, 0));
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0));
        public Rectangle[] StatusRect = new Rectangle[21];

        public string[] StrInfo = new string[21]{ "紧急制动","司机紧急制动","门缓解","主控手柄处在制动位置","主控手柄故障","调车/洗车模式下速度过高","DSD处于试验模式",
                                                 "回送模式激活","制动力低","速度计算错误","紧急停车按钮已按下","乘客紧急制动已启动","牵引试验已激活","牵引安全回路已失效",
                                                 "转向架上车轴已抱死","实施停放制动","受电弓降弓故障","外接电源已连接配置","TC/CCU太多","不是所有的车门都关闭并锁上","ATP请求常用制动" };


        public Font Strfont = new Font("Arial", 11);
        public bool[] Valueb = new bool[21];

        public override string GetInfo()
        {
            return "牵引封锁";
        }


        public override bool Initialize()
        {
            InitData();
            return true;
        }
        public override void paint(Graphics dcGs)
        {
            GetValue();
            OnPaint((dcGs));
            base.paint(dcGs);
        }
        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 21)
            {
                for (int i = 0; i < 21; i++)
                {
                    Valueb[i] = BoolList[UIObj.InBoolList[i]];
                }
            }

        }
        public void InitData()
        {
            for (int i = 0; i < 21; i++)
            {
                if (i < 11)
                {
                    StatusRect[i] = new Rectangle(Recposition.X + 20, Recposition.Y + 25 * (i + 1), 35, 20);
                }
                else
                {
                    StatusRect[i] = new Rectangle(Recposition.X + 420, Recposition.Y + 25 * (i - 10), 35, 20);
                }
            }

        }
        public void OnPaint(Graphics g)
        {
            //绘制菜单标题
            Title.OnDraw(g);

            g.FillRectangle(BackBrush, Recposition);
            g.DrawRectangle(BlackPen, Recposition);
            for (int i = 0; i < 21; i++)
            {
                if (Valueb[i])
                {
                    g.FillRectangle(RedBrush, StatusRect[i]);
                }
                else
                {
                    g.FillRectangle(GreenBrush, StatusRect[i]);
                }
                g.DrawRectangle(BlackPen, StatusRect[i]);
                g.DrawString(StrInfo[i], Strfont, WhiteBrush, StatusRect[i].X + 40, StatusRect[i].Y + 3);
            }
            g.DrawString("牵引封锁", new Font("Arial", 12), WhiteBrush, Recposition.X + 385, Recposition.Y + 6);
        }
    }



    /// <summary>
    ///   详细显示那些促使速度限制互锁的条件信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class GT_SpeedLimit : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("限速");//菜单标题    
        public Rectangle Recposition = new Rectangle(2, 140, 790, 300);
        public Pen BackPen = new Pen(Color.FromArgb(163, 180, 198), 2);
        public SolidBrush BackBrush = new SolidBrush(Color.FromArgb(119, 136, 153));
        public SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
        public SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 128, 0));
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0));
        public Rectangle[] StatusRect = new Rectangle[5];
        public string[] StrInfo = new string[5] { "限速", "倒车", "牵引/制动力减少", "摩擦/制动力减少", "维护人员登录" };

        public Font Strfont = new Font("Arial", 11);
        public bool[] Valueb = new bool[5];

        public override string GetInfo()
        {
            return "限速";
        }

        public override bool Initialize()
        {
            InitData();
            return true;
        }
        public override void paint(Graphics dcGs)
        {
            GetValue();
            OnPaint((dcGs));
            base.paint(dcGs);
        }
        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    Valueb[i] = BoolList[UIObj.InBoolList[i]];
                }
            }

        }
        public void InitData()
        {
            for (int i = 0; i < 5; i++)
            {
                StatusRect[i] = new Rectangle(Recposition.X + 280, Recposition.Y + 25 * i + 60, 35, 20);
            }
        }
        public void OnPaint(Graphics g)
        {
            //绘制菜单标题
            Title.OnDraw(g);

            g.FillRectangle(BackBrush, Recposition);
            g.DrawRectangle(BlackPen, Recposition);
            for (int i = 0; i < 5; i++)
            {
                if (Valueb[i])
                {
                    g.FillRectangle(RedBrush, StatusRect[i]);
                }
                else
                {
                    g.FillRectangle(GreenBrush, StatusRect[i]);
                }
                g.DrawRectangle(BlackPen, StatusRect[i]);
                g.DrawString(StrInfo[i], Strfont, WhiteBrush, StatusRect[i].X + 40, StatusRect[i].Y + 3);
            }
            g.DrawString("限速", new Font("Arial", 12), WhiteBrush, Recposition.X + 385, Recposition.Y + 6);
        }
    }


    /// <summary>
    ///   详细显示引起紧急制动互锁的条件信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class GT_EmergencyBrake : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("紧急制动");//菜单标题
        public Rectangle Recposition = new Rectangle(2, 140, 790, 300);
        public Pen BackPen = new Pen(Color.FromArgb(163, 180, 198), 2);
        public SolidBrush BackBrush = new SolidBrush(Color.FromArgb(119, 136, 153));
        public SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
        public SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 128, 0));
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0));
        public Rectangle[] StatusRect = new Rectangle[9];
        public string[] StrInfo = new string[9]{ "紧急停车按钮已按下", "由司机主控手柄激活", "ATP系统请求", "安全回路电源被切断", "DSD系统请求" ,
                                                   "紧急制动回路监控","主风缸压力低","从控单元请求紧急制动","制动系统请求" };

        public Font Strfont = new Font("Arial", 11);
        public bool[] Valueb = new bool[9];

        public override string GetInfo()
        {
            return "紧急制动";
        }


        public override bool Initialize()
        {
            InitData();
            return true;
        }
        public override void paint(Graphics dcGs)
        {
            GetValue();
            OnPaint((dcGs));
            base.paint(dcGs);
        }
        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 9)
            {
                for (int i = 0; i < 9; i++)
                {
                    Valueb[i] = BoolList[UIObj.InBoolList[i]];
                }
            }

        }
        public void InitData()
        {
            for (int i = 0; i < 9; i++)
            {
                StatusRect[i] = new Rectangle(Recposition.X + 280, Recposition.Y + 25 * i + 60, 35, 20);
            }
        }
        public void OnPaint(Graphics g)
        {
            //绘制菜单标题
            Title.OnDraw(g);

            g.FillRectangle(BackBrush, Recposition);
            g.DrawRectangle(BlackPen, Recposition);
            for (int i = 0; i < 9; i++)
            {
                if (Valueb[i])
                {
                    g.FillRectangle(RedBrush, StatusRect[i]);
                }
                else
                {
                    g.FillRectangle(GreenBrush, StatusRect[i]);
                }
                g.DrawRectangle(BlackPen, StatusRect[i]);
                g.DrawString(StrInfo[i], Strfont, WhiteBrush, StatusRect[i].X + 90, StatusRect[i].Y + 3);
            }
            g.DrawString("紧急制动", new Font("Arial", 12), WhiteBrush, Recposition.X + 385, Recposition.Y + 6);
        }
    }


}
