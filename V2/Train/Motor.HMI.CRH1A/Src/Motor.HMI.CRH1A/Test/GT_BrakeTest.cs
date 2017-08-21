using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using CommonUtil.Controls.Button;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Common.Global;
using Motor.HMI.CRH1A.Common.Train;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH1A.Test
{
    /// <summary>
    /// 制动控制试验 此菜单的功能是当列车处于静止状态时实施制动控制
    /// 测试，可以从主菜单进入该菜单
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    internal class GT_BrakeTest : CRH1BaseClass
    {
        #region 测试信息

        /// <summary>
        /// 测试信息集合
        /// </summary>
        private List<TestToolInfo> m_TestToolInfos = new List<TestToolInfo>();

        /// <summary>
        /// 显示测试提示信息
        /// </summary>
        private string m_ShowTestToolInfo = string.Empty;

        #endregion

        #region 私有字段

        private GT_MenuTitle m_Title = new GT_MenuTitle("制动试验"); //设置菜单标题
        public Rectangle Recposition = new Rectangle(0, 30, 800, 100);
        public Rectangle[] TrainRect = new Rectangle[8]; //车的位置
        public int Weight = 90; //车的宽度
        public int Height = 70; //车的高度
        private Rectangle m_Rect; //页脚区域
        private CRH1AButton m_GButton;

        public Font Strfont = new Font("Arial", 9);
        public Font Strfont1 = new Font("Arial", 11);
        public SolidBrush StrBrush = new SolidBrush(Color.FromArgb(135, 135, 135));
        public SolidBrush BackBrush = new SolidBrush(Color.FromArgb(119, 136, 153));
        public SolidBrush GrayBrush = new SolidBrush(Color.FromArgb(192, 192, 192));
        public SolidBrush GreenBrush = new SolidBrush(Color.FromArgb(0, 128, 0));
        public SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));

        public Pen BackPen = new Pen(Color.FromArgb(86, 103, 121), 2);
        public Pen GreenlPen = new Pen(Color.FromArgb(57, 149, 56), 2);
        public Pen RedPen = new Pen(Color.FromArgb(255, 0, 0), 2);
        public Image[] Image = new Image[10];
        public bool[] Valueb = new bool[37];

        public string[] StrText = new string[]
                                   {
                                       " ", "制控测试条件未达到---主控手柄不在'0'位", "制控测试条件未达到---主风缸压力低", "制控测试条件未达到---紧急制动实施", "制控测试条件未达到---停放制动未实施",
                                       "制控测试条件未达到---至少一辆车内的制动\n被隔离，必须从IDU启动制动测试。", "制控测试条件未达到---停放制动功能不足以\n实施制动测试，检查停放制动,如可能进行\n纠正，否则，实施手动制动测试",
                                       "将主控控制手柄至于7。", "将主控控制手柄至于0。", "将主控控制手柄至于8。(紧急制动)", "制动试验失败---制动不能正确缓解", "制动试验失败---制动不能正确施加", "制动试验已重复三次,均未成功---实施\n手工测试",
                                       "制动试验失败---列车未处于静止状态", "制动试验失败---制动测试时间用完,重新\n进行制动测试。", "制动试验失败---停放制动未实施。", "制动试验失败---主风缸压力低",
                                       "制动试验失败---安全回路未打开", "制动测试完成,至少有一辆车的制动被隔离。", "制动测试完成"
                                   };

        #endregion

        public override string GetInfo()
        {
            return "制动试验";
        }

        public override bool Initialize()
        {
            //3
            InitData();

            //////////////////加 载 图 片
            if (UIObj.ParaList.Count >= 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    Image[i] = System.Drawing.Image.FromFile(RecPath + "//" + UIObj.ParaList[i]);
                }
            }

            LoadDescriptionFile();

            return true;
        }

        public override void paint(Graphics g)
        {
            if (GlobalParam.Instance.TrainInfo.TrainState != TrainState.Stop)
            {
                OnPost(CmdType.ChangePage, (int) ViewConfig.MainView, 1, 0);
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
                        if (str.Length == 2 && int.Parse(str[0])>=766)
                        {
                            var toolInfo = new TestToolInfo();
                            toolInfo.LogicalBit = Convert.ToInt32(str[0]);
                            toolInfo.ToolInfo = str[1].Replace('#', '\n');
                            m_TestToolInfos.Add(toolInfo);
                        }
                    }
                }
            }
        }

        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 37)
            {
                for (int i = 0; i < 37; i++)
                {
                    Valueb[i] = BoolList[UIObj.InBoolList[i]];
                }
            }
        }

        public void InitData()
        {

            #region::::::::::::;;;;;;;;各 车 位 置 初 始 化;;;;;;;;;;;;;;;;;;;;;;;;;;;

            for (int i = 0; i < 8; i++)
            {
                TrainRect[i] = new Rectangle(Recposition.X + i * ( Weight + 5 ) + 25, Recposition.Y + 140, Weight, Height);
            }

            #endregion

            #region ::::::::::::::::::::页 脚 信 息 区 域 位 置::::::::::::::::::::::::::::::::

            m_Rect = new Rectangle(Recposition.X, Recposition.Y + 340, 790, 80);

            #endregion

            #region ;;;;;;按 钮 初 始 化;;;;;;;;;;;;;;;;;;

            m_GButton = new CRH1AButton();
            m_GButton.SetButtonColor(192, 192, 192);
            m_GButton.SetButtonRect(Recposition.X + 380, Recposition.Y + 485, 90, 50);
            m_GButton.SetButtonText("启动试验");

            #endregion
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


        public void DrawOn(Graphics g)
        {
            m_Title.OnDraw(g);

            //页脚区域绘制
            g.FillRectangle(BackBrush, m_Rect);
            g.DrawRectangle(BackPen, m_Rect);
            g.DrawImage(Image[0], m_Rect.X + 7, m_Rect.Y + 10);
            g.DrawString("(用法说明)", Strfont, WhiteBrush, m_Rect.X + 40, m_Rect.Y + 8);
            for (int i = 0; i < 20; i++)
            {
                if (Valueb[17 + i])
                {
                    //  g.DrawString(str_Text[i], strfont, white_Brush, rect.X + 30, rect.Y + 30);
                }
            }

            g.FillRectangle(GreenBrush, m_Rect.X + 690, m_Rect.Y + 5, 20, 20);
            g.DrawRectangle(GreenlPen, m_Rect.X + 690, m_Rect.Y + 5, 20, 20);
            g.DrawString("施加", Strfont, WhiteBrush, m_Rect.X + 720, m_Rect.Y + 7);

            g.FillRectangle(GrayBrush, m_Rect.X + 690, m_Rect.Y + 30, 20, 20);
            g.DrawRectangle(GreenlPen, m_Rect.X + 690, m_Rect.Y + 30, 20, 20);
            g.DrawString("释放", Strfont, WhiteBrush, m_Rect.X + 720, m_Rect.Y + 32);

            g.FillRectangle(GrayBrush, m_Rect.X + 690, m_Rect.Y + 55, 20, 20);
            g.DrawRectangle(RedPen, m_Rect.X + 690, m_Rect.Y + 55, 20, 20);
            g.DrawString("已切断或故障", Strfont, WhiteBrush, m_Rect.X + 710, m_Rect.Y + 57);

            //绘制提示信息
            g.DrawString(m_ShowTestToolInfo, Strfont1, WhiteBrush, m_Rect.X + 100, m_Rect.Y + 30);

            //绘制列车 及其编号
            if (!Valueb[0] && !Valueb[8]) //释放状态
            {
                g.DrawImage(Image[1], TrainRect[0]);
            }
            else if (Valueb[0] && !Valueb[8])
            {
                g.DrawImage(Image[2], TrainRect[0]); //施加
            }

            else if (Valueb[8])
            {
                g.DrawImage(Image[3], TrainRect[0]); //已切断或故障
            }
            else
            {

            }
            for (int i = 0; i < 6; i++)
            {
                if (!Valueb[i + 1] && !Valueb[i + 9])
                {
                    g.DrawImage(Image[4], TrainRect[i + 1]);
                }
                else if (Valueb[i + 1] && !Valueb[i + 9])
                {
                    g.DrawImage(Image[5], TrainRect[i + 1]);
                }
                else if (Valueb[i + 9])
                {
                    g.DrawImage(Image[6], TrainRect[i + 1]);
                }
                else
                {
                }
            }
            if (!Valueb[7] && !Valueb[15]) //释放状态
            {
                g.DrawImage(Image[7], TrainRect[7]);
            }
            else if (Valueb[7] && !Valueb[15])
            {
                g.DrawImage(Image[8], TrainRect[7]); //施加
            }

            else if (Valueb[15])
            {
                g.DrawImage(Image[9], TrainRect[7]); //已切断或故障
            }
            else
            {

            }

            for (int i = 0; i < 8; i++)
            {
                if (i == 7)
                {
                    g.DrawString("00", Strfont1, StrBrush, TrainRect[7].X + 30, TrainRect[7].Y - 15);
                }
                else
                {
                    g.DrawString("0" + ( i + 1 ).ToString(), Strfont1, StrBrush, TrainRect[i].X + 30, TrainRect[i].Y - 15);
                }

            }

            m_GButton.OnDraw(g);
        }

        /// <summary>
        /// 按钮点击事件
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void OnButtonDown(int x, int y)
        {
            if (m_GButton.Contains(x, y))
            {
                OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                m_GButton.OnButtonDown();
            }

        }

        public void OnButtonUp(int x, int y)
        {
            if (m_GButton.Contains(x, y))
            {
                OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                m_GButton.OnButtonUp();
            }
        }
    }
}
