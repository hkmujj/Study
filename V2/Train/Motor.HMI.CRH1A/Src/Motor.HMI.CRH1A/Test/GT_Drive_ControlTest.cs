using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH1A.Alarm.Fault;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Common.Global;
using Motor.HMI.CRH1A.Common.Train;

namespace Motor.HMI.CRH1A.Test
{
    /// <summary>
    /// 驾驶控制试验 此菜单的功能是当列车处于静止状态时实施驱动控制
    /// 测试，可以从主菜单进入该菜单
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class GT_Drive_ControlTest : CRH1BaseClass
    {
        #region 测试信息
        /// <summary>
        /// 测试信息集合
        /// </summary>
        private readonly List<TestToolInfo> m_TestToolInfos = new List<TestToolInfo>();

        /// <summary>
        /// 显示测试提示信息
        /// </summary>
        private string m_ShowTestToolInfo = string.Empty;
        #endregion

        #region  私有字段

        readonly GT_MenuTitle m_Title = new GT_MenuTitle("手柄测试");//设置菜单标题
        public Rectangle m_Recposition = new Rectangle(0, 30, 800, 100);
        public Rectangle[] m_TrainRect = new Rectangle[8];//车的位置
        public int m_Weight = 90;//车的宽度
        public int m_Height = 70;//车的高度
        Rectangle m_Rect;//页脚区域
        readonly CRH1AButton[] m_GButton = new CRH1AButton[2];
        public Font m_Strfont = new Font("Arial", 9);
        public Font m_ToolInfofont = new Font("Arial", 11);
        public SolidBrush m_StrBrush = new SolidBrush(Color.FromArgb(135, 135, 135));
        public SolidBrush m_BackBrush = new SolidBrush(Color.FromArgb(119, 136, 153));
        private readonly SolidBrush m_GrayBrush = new SolidBrush(Color.Gray);
        public SolidBrush m_RedBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
        public SolidBrush m_GreenBrush = new SolidBrush(Color.FromArgb(0, 128, 0));
        public SolidBrush m_WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public SolidBrush m_BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        public Pen m_BackPen = new Pen(Color.FromArgb(86, 103, 121), 2);
        public Image[] m_Image = new Image[3];
        public bool[] m_Valueb = new bool[64];
        public Rectangle[] m_TestRectR = new Rectangle[16];
        public Rectangle[] m_TestRectL = new Rectangle[16];
        public string[] m_Text = new string[] { "手柄测试牵引位置6","手柄测试牵引位置5","手柄测试牵引位置4","手柄测试匀速位置","手柄测试牵引位置3",
                                                "手柄测试牵引位置2","手柄测试牵引位置1","手柄测试位置0","手柄测试制动位置1",
                                                "手柄测试制动位置2","手柄测试制动位置3","手柄测试制动位置4","手柄测试制动位置5","手柄测试制动位置6",
                                                "手柄测试制动位置7","手柄测试紧急制动位置" };
        #endregion

        #region 重载方法
        public override string GetInfo()
        {
            return "驾驶控制试验";
        }


        public override bool Initialize()
        {
            InitData();

            //////////////////加 载 图 片
            if (UIObj.ParaList.Count > 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    m_Image[i] = Image.FromFile(RecPath + "//" + UIObj.ParaList[i]);
                }

            }

            LoadDescriptionFile();

            return true;
        }

        private void LoadDescriptionFile()
        {
            var file = Path.Combine(RecPath, "..\\Config\\说明信息.txt");
            if (File.Exists(file))
            {
                var all = File.ReadAllLines(file, Encoding.Default);
                foreach (var s in all)
                {
                    if (s.Trim() != "")
                    {
                        string[] str = s.Split(';');
                        if (str.Length == 2 && int.Parse(str[0]) <= 736)
                        {
                            var toolInfo = new TestToolInfo
                            {
                                LogicalBit = Convert.ToInt32(str[0]),
                                ToolInfo = str[1].Replace('#', '\n')
                            };
                            m_TestToolInfos.Add(toolInfo);
                        }
                    }
                }
            }
        }

        public override void paint(Graphics g)
        {
            if (GlobalParam.Instance.TrainInfo.TrainState != TrainState.Stop)
            {
                OnPost(CmdType.ChangePage, (int)ViewConfig.MainView, 1, 0);
            }
            GetValue();
            RefreshShowInfo();
            DrawOn(g);
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

        #endregion

        public void InitData()
        {

            #region::::::::::::;;;;;;;;各 车 位 置 初 始 化;;;;;;;;;;;;;;;;;;;;;;;;;;;

            for (int i = 0; i < 8; i++)
            {
                m_TrainRect[i] = new Rectangle(m_Recposition.X + i * (m_Weight + 5) + 25, m_Recposition.Y + 140, m_Weight, m_Height);
            }
            #endregion

            #region ::::::::::::::::::::页 脚 信 息 区 域 位 置::::::::::::::::::::::::::::::::
            m_Rect = new Rectangle(m_Recposition.X, m_Recposition.Y + 340, 790, 80);

            #endregion

            #region ;;;;;;按 钮 初 始 化;;;;;;;;;;;;;;;;;;
            m_GButton[0] = new CRH1AButton();
            m_GButton[0].SetButtonColor(192, 192, 192);
            m_GButton[0].SetButtonRect(m_Recposition.X + 380, m_Recposition.Y + 485, 90, 50);
            m_GButton[0].SetButtonText("启动试验");

            m_GButton[1] = new CRH1AButton();
            m_GButton[1].SetButtonColor(192, 192, 192);
            m_GButton[1].SetButtonRect(m_Recposition.X + 720, m_Recposition.Y, 70, 60);


            #endregion

            #region ::::::::::::::::::::各 手 柄 状 态 位 置 初 始 化::::::::::::::::::::::::::::
            for (int i = 0; i < 16; i++)
            {
                m_TestRectL[i] = new Rectangle(m_Recposition.X + 250, m_Recposition.Y + 20 + 20 * i, 50, 15);
                m_TestRectR[i] = new Rectangle(m_Recposition.X + 315, m_Recposition.Y + 20 + 20 * i, 50, 15);
            }

            #endregion

        }

        public void DrawOn(Graphics g)
        {
            //添加菜单标题
            m_Title.OnDraw(g);
            //页脚区域绘制
            g.FillRectangle(m_BackBrush, m_Rect);
            g.DrawRectangle(m_BackPen, m_Rect);

            g.DrawImage(m_Image[0], m_Rect.X + 10, m_Rect.Y + 10);
            g.DrawString("(说明)", m_Strfont, m_WhiteBrush, m_Rect.X + 35, m_Rect.Y + 15);
            //绘制按钮
            for (int i = 0; i < 2; i++)
            {
                m_GButton[i].OnDraw(g);
            }

            g.DrawImage(GT_GalobalFaultManage.Instance.HasSuredActiveFault() ? m_Image[2] : m_Image[1], m_GButton[1].OutLineRectangle);
            //g.DrawImage(image[1],G_Button[1].RectPosition);

            //手柄测试状态显示
            //////////////////////////////////////
            //-ycl-
            //默认颜色为灰色，所以增加了一倍的接口
            /////////////////////////////////////
            for (int i = 0; i < 16; i++)
            {
                if (m_Valueb[i])
                {
                    g.FillRectangle(m_GreenBrush, m_TestRectL[i]);
                    g.FillRectangle(m_GreenBrush, m_TestRectR[i]);
                }
                else if (m_Valueb[i + 16])
                {
                    g.FillRectangle(m_RedBrush, m_TestRectL[i]);
                    g.FillRectangle(m_RedBrush, m_TestRectR[i]);
                }
                else
                {
                    g.FillRectangle(m_GrayBrush, m_TestRectL[i]);
                    g.FillRectangle(m_GrayBrush, m_TestRectR[i]);
                }

                g.DrawString(m_Text[i], m_Strfont, m_BlackBrush, m_TestRectR[i].X + 58, m_TestRectR[i].Y);
            }

            //绘制提示信息
            g.DrawString(m_ShowTestToolInfo, m_ToolInfofont, m_WhiteBrush, m_Rect.X + 100, m_Rect.Y + 30);

        }

        public void GetValue()
        {
            for (int i = 0; i < UIObj.InBoolList.Count; i++)
            {
                m_Valueb[i] = BoolList[UIObj.InBoolList[i]];
            }

        }

        public void OnButtonDown(int x, int y)
        {
            for (int i = 0; i < 2; i++)
            {
                if (m_GButton[i].Contains(x, y))
                {
                    if (i == 0)
                    {
                        OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[i], 1, 0);
                    }
                    m_GButton[i].OnButtonDown();
                }
            }

        }

        public void OnButtonUp(int x, int y)
        {
            for (int i = 0; i < 2; i++)
            {
                if (m_GButton[i].Contains(x, y))
                {
                    if (i == 0)
                    {
                        OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                    }
                    else
                    {
                        OnPost(CmdType.ChangePage, 23, 0, 0);
                    }
                    m_GButton[i].OnButtonUp();
                }
            }
        }

        /// <summary>
        /// 刷新消息提示
        /// </summary>
        private void RefreshShowInfo()
        {
            for (int index = m_TestToolInfos.Count - 1; index >= 0; index--)
            {
                if (BoolList[m_TestToolInfos[index].LogicalBit])
                {
                    m_ShowTestToolInfo = m_TestToolInfos[index].ToolInfo;
                    break;
                }

                if (index == 0)
                {
                    m_ShowTestToolInfo = string.Empty;
                }
            }
        }

    }
}