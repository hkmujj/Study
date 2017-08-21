using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using Motor.HMI.CRH1A.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls.Button;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    internal class GT_MainMenuButton : CRH1BaseClass
    {
        private readonly GT_MenuTitle m_Title = new GT_MenuTitle("主菜单"); //设置菜单标题
        public Rectangle m_Recposition = new Rectangle(0, 178, 70, 40);
        public Rectangle[] m_Rect = new Rectangle[4];
        //public int level;//用户权限等级 0为司机 1为列车员 2为维护人员
        public CRH1AButton[] m_DiaButton = new CRH1AButton[3]; //诊断系统的菜单按钮
        public CRH1AButton[] m_InsButton = new CRH1AButton[5]; //仪器栏的菜单按钮
        public CRH1AButton[] m_MaiButton = new CRH1AButton[2]; //维修栏菜单按钮
        public CRH1AButton[] m_TestButton = new CRH1AButton[3]; //试验栏菜单按钮
        public CRH1AButton m_RunButton; //运行车站按钮
        public Pen m_Whitepen = new Pen(Color.FromArgb(255, 255, 255), 3);
        public SolidBrush m_Brush = new SolidBrush(Color.FromArgb(119, 136, 153));

        public Font m_Strfont = new Font("Arial", 12);
        public SolidBrush m_Whitebrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public float m_Valuef; //获取列车速度


        public override string GetInfo()
        {
            return "主菜单按钮栏";
        }


        public override bool Initialize()
        {
            //3
            InitData();

            return true;
        }


        public override void paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
        }

        public void GetValue()
        {
            if (UIObj.InFloatList.Count > 0)
            {
                m_Valuef = GlobalInfo.Instance.Crh1AConfig.AdaptConfig.CurrentSpeedCoefficient*FloatList[UIObj.InFloatList[0]];
                var isEnable = (Math.Abs(m_Valuef) <= float.Epsilon);
                if (GlobalInfo.Instance.ButtonEnable && !isEnable)
                {
                    foreach (var btn in m_TestButton)
                    {
                        btn.IsEnable = false;
                    }
                }
                else
                {
                    foreach (var btn in m_TestButton)
                    {
                        btn.IsEnable = true;
                    }
                }
            }
        }

        public void InitData()
        {
            //设置各项菜单栏的位置
            m_Rect[0] = new Rectangle(m_Recposition.X, m_Recposition.Y, 175, 275);
            m_Rect[1] = new Rectangle(m_Recposition.X + 180, m_Recposition.Y, 245, 275);
            m_Rect[2] = new Rectangle(m_Recposition.X + 435, m_Recposition.Y, 175, 275);
            m_Rect[3] = new Rectangle(m_Recposition.X + 615, m_Recposition.Y, 175, 275);

            //////////////////设置每项菜单栏的按钮栏位置/////////////////////////
            //初始化诊断系统项按钮
            for (int i = 0; i < 3; i++)
            {
                m_DiaButton[i] = new CRH1AButton();
                m_DiaButton[i].SetButtonRect(m_Rect[0].X + 45, m_Rect[0].Y + 60*i + 50, 85, 50);
                m_DiaButton[i].SetButtonColor(192, 192, 192);
            }
            m_DiaButton[0].SetButtonText("报警记录");
            m_DiaButton[1].SetButtonText("警报总况");
            m_DiaButton[2].SetButtonText("故障报告");

            //初始化仪器栏的按钮
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 2; j++)
                {
                    if ((i*2 + j) < 5)
                    {
                        m_InsButton[i*2 + j] = new CRH1AButton();
                        m_InsButton[i*2 + j].SetButtonRect(m_Rect[1].X + j*90 + 35, m_Rect[1].Y + i*60 + 50, 85, 50);
                        m_InsButton[i*2 + j].SetButtonColor(192, 192, 192);
                    }

                }
            m_InsButton[0].SetButtonText("系统");
            m_InsButton[1].SetButtonText("列车状态");
            m_InsButton[2].SetButtonText("启动");
            m_InsButton[3].SetButtonText("牵引/制动");
            m_InsButton[4].SetButtonText("互锁");
            //初始化维修栏
            m_MaiButton[0] = new CRH1AButton();
            m_MaiButton[0].SetButtonRect(m_Rect[2].X + 45, m_Rect[2].Y + 50, 85, 50);
            m_MaiButton[0].SetButtonColor(192, 192, 192);
            m_MaiButton[0].SetButtonText("设置");

            //初始化试验按钮栏
            for (int i = 0; i < 3; i++)
            {
                m_TestButton[i] = new CRH1AButton();
                m_TestButton[i].SetButtonColor(192, 192, 192);
                m_TestButton[i].SetButtonRect(m_Rect[3].X + 45, m_Rect[3].Y + 60*i + 50, 85, 50);
            }
            m_TestButton[0].SetButtonText("制动试验");
            m_TestButton[1].SetButtonText("手柄测试");
            m_TestButton[2].SetButtonText("灯测试");
            m_RunButton = new CRH1AButton();
            m_RunButton.SetButtonRect(m_Recposition.X + 705, m_Recposition.Y + 330, 85, 50);
            m_RunButton.SetButtonText("运行/车站");
            m_RunButton.SetButtonColor(192, 192, 192);

            GlobalInfo.Instance.ButtonStatusChange += () =>
            {
                foreach (var t in m_DiaButton)
                {
                    t.IsEnable = GlobalInfo.Instance.ButtonEnable;
                }
                foreach (var t in m_InsButton)
                {
                    t.IsEnable = GlobalInfo.Instance.ButtonEnable;
                }
                foreach (var t in m_MaiButton.Where(w => w is CRH1AButton))
                {
                    t.IsEnable = GlobalInfo.Instance.ButtonEnable;
                }
                foreach (var t in m_TestButton)
                {
                    t.IsEnable = GlobalInfo.Instance.ButtonEnable;
                }
                m_RunButton.IsEnable = GlobalInfo.Instance.ButtonEnable;
            };
        }

        public void DrawOn(Graphics g)
        {
            //绘制菜单标题
            m_Title.OnDraw(g);
            for (int i = 0; i < 4; i++)
            {
                g.FillRectangle(m_Brush, m_Rect[i]);
                g.DrawRectangle(m_Whitepen, m_Rect[i]);
            }
            for (int i = 0; i < 3; i++)
            {
                m_DiaButton[i].OnDraw(g);
            }
            for (int i = 0; i < 5; i++)
            {
                m_InsButton[i].OnDraw(g);
            }
            m_MaiButton[0].OnDraw(g);
            for (int i = 0; i < 3; i++)
            {
                m_TestButton[i].OnDraw(g);
            }
            g.DrawString("诊断系统", m_Strfont, m_Whitebrush, m_Rect[0].X + 40, m_Rect[0].Y + 15);
            g.DrawString("仪器", m_Strfont, m_Whitebrush, m_Rect[1].X + 120, m_Rect[0].Y + 15);
            g.DrawString("维修", m_Strfont, m_Whitebrush, m_Rect[2].X + 55, m_Rect[0].Y + 15);
            g.DrawString("试验", m_Strfont, m_Whitebrush, m_Rect[3].X + 55, m_Rect[0].Y + 15);
            m_RunButton.OnDraw(g);
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
            ///////诊断系统按钮响应事件 
            foreach (var button in m_DiaButton.Where(w => w.Contains(x, y) && w.IsEnable))
            {
                button.OnButtonDown();
            }
            /////////仪器栏按钮响应事件
            foreach (var button in m_InsButton.Where(w => w.Contains(x, y) && w.IsEnable))
            {
                button.OnButtonDown();
            }
            ///////////响应维修栏按钮
            foreach (var button in m_MaiButton.Where(w => w != null && w.Contains(x, y) && w.IsEnable))
            {
                button.OnButtonDown();
            }
            /////////响应试验栏按钮
            foreach (var button in m_TestButton.Where(w => w.Contains(x, y) && w.IsEnable))
            {
                button.OnButtonDown();
            }
            ////////响应运行车站按钮
            if (m_RunButton.Contains(x, y) && m_RunButton.IsEnable)
            {
                m_RunButton.OnButtonDown();
            }
        }

        public void OnButtonUp(int x, int y)
        {
            ///////诊断系统按钮响应事件 
            for (int i = 0; i < 3; i++)
                if (m_DiaButton[i].Contains(x, y) && m_DiaButton[i].IsEnable)
                {
                    if (i == 0)
                    {
                        OnPost(CmdType.ChangePage, 23, 0, 0);
                    }
                    else if (i == 1)
                    {
                        OnPost(CmdType.ChangePage, 25, 0, 0);
                    }
                    else
                    {
                        OnPost(CmdType.ChangePage, 24, 0, 0);
                    }
                    m_DiaButton[i].OnButtonUp();

                }
            /////////仪器栏按钮响应事件
            for (int i = 0; i < 5; i++)
            {
                if (m_InsButton[i].Contains(x, y) && m_InsButton[i].IsEnable)
                {
                    if (i == 0)
                    {
                        OnPost(CmdType.ChangePage, 4, 0, 0);
                    }
                    else if (i == 1)
                    {
                        OnPost(CmdType.ChangePage, 6, 0, 0);
                    }
                    else if (i == 2)
                    {
                        OnPost(CmdType.ChangePage, 18, 0, 0);
                    }
                    else if (i == 3)
                    {
                        OnPost(CmdType.ChangePage, 29, 0, 0);
                    }
                    else if (i == 4)
                    {
                        OnPost(CmdType.ChangePage, 19, 0, 0);
                    }
                    else
                    {

                    }
                    m_InsButton[i].OnButtonUp();

                }
            }
            ///////////响应维修栏按钮
            for (int i = 0; i < 1; i++)
            {

                if (m_MaiButton[i].Contains(x, y) && m_MaiButton[i].IsEnable)
                {
                    if (i == 0)
                    {
                        OnPost(CmdType.ChangePage, 26, 0, 0);
                    }
                    else
                    {

                    }
                    m_MaiButton[i].OnButtonUp();

                }

            }
            /////////响应试验栏按钮
            for (int i = 0; i < 3; i++)
            {

                if (m_TestButton[i].Contains(x, y)
                    && m_TestButton[i].IsEnable)
                {
                    if (i == 0)
                    {
                        OnPost(CmdType.ChangePage, 30, 0, 0); //制动试验
                    }
                    else if (i == 1)
                    {
                        OnPost(CmdType.ChangePage, 31, 0, 0); //驾驶控制测试
                    }
                    else
                    {
                        OnPost(CmdType.ChangePage, 32, 0, 0); //控制灯测试
                    }
                    m_TestButton[i].OnButtonUp();

                }

            }
            ////////响应运行车站按钮
            if (m_RunButton.Contains(x, y) && m_RunButton.IsEnable)
            {
                if (m_Valuef >= 3) //速度高于3km时跳转到运行界面
                    OnPost(CmdType.ChangePage, 3, 0, 0);
                else
                {
                    OnPost(CmdType.ChangePage, 5, 0, 0);
                }

                m_RunButton.OnButtonUp();
            }
        }
    }
}
