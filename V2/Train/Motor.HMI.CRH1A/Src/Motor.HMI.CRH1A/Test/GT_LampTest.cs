using System.Drawing;
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
    /// 灯测试试验 此菜单的功能是当列车处于静止状态时对控制灯和警报进行测试
    /// 可以从菜单进入该子菜单
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class GT_LampTest : CRH1BaseClass
    {
        public Rectangle Recposition = new Rectangle(0, 170, 791, 280);
        public SolidBrush BackBrush = new SolidBrush(Color.FromArgb(119, 136, 152));
        public Pen BackPen = new Pen(Color.FromArgb(199, 215, 232), 3);
        public SolidBrush StrBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public Font Strfont = new Font("Arial", 11);
        public CRH1AButton[] GButton = new CRH1AButton[3];

        /// <summary>
        /// 测试完成标志
        /// </summary>
        private bool[] m_IsTestFinishFlag = new bool[3];

        /// <summary>
        /// 是否在进行本项实验 true表示进行实验 false 表示结束实验
        /// </summary>
        private bool m_IsJinBaoBeginOrEndTest = false;

        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "灯测试";
        }


        public override bool Initialize()
        {
            //3
            InitDate();

            return true;
        }


        public override void paint(Graphics g)
        {
            if (GlobalParam.Instance.TrainInfo.TrainState != TrainState.Stop)
            {
                OnPost(CmdType.ChangePage, (int)ViewConfig.MainView, 1, 0);
            }
            GetValue();
            RefreshButtonStatu();
            DrawOn(g);
        }
        public void GetValue()
        {
            if (UIObj.InBoolList.Count >= 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    m_IsTestFinishFlag[i] = BoolList[UIObj.InBoolList[i]];
                }
            }
        }
        public void InitDate()
        {
            for (int i = 0; i < 3; i++)
            {
                GButton[i] = new CRH1AButton();
                GButton[i].SetButtonRect(Recposition.X + 125 + i * 225, Recposition.Y + 130, 100, 60);
            }
            GButton[0].SetButtonText("灯全部亮");
            GButton[1].SetButtonText("无灯亮");
            GButton[2].SetButtonText("警报蜂鸣器");
        }

        public void DrawOn(Graphics e)
        {
            e.FillRectangle(BackBrush, Recposition);
            e.DrawRectangle(BackPen, Recposition);
            for (int i = 0; i < 3; i++)
            {
                //if (_isTestFinishFlag[i])
                //{
                //    G_Button[i].SetButtonColor(255, 255, 0);
                //}
                //else
                //{
                //    G_Button[i].SetButtonColor(192, 192, 192);
                //}
                GButton[i].OnDraw(e);
            }

        }

        private void RefreshButtonStatu()
        {
            //刷新灯测试按钮状态
            for (int index = 0; index < 3; index++)
            {
                if (m_IsTestFinishFlag[index])
                {
                    GButton[index].SetButtonColor(255, 255, 0);
                }
                else
                {
                    GButton[index].SetButtonColor(192, 192, 192);
                }
            }
        }
        #endregion#
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
                {
                    if (i < 2)
                    {
                        OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[i], 1, 0);
                    }
                    else
                    {
                        if (m_IsJinBaoBeginOrEndTest)//处于停止实验状态
                        {
                            OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[i], 1, 0);//发送开始实验标志
                            OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[i + 1], 0, 0);//发送开始实验标志
                            m_IsJinBaoBeginOrEndTest = false;
                        }
                        else//处于结束实验状态
                        {
                            OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[i], 0, 0);//发送结束实验标志
                            OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[i + 1], 1, 0);//发送结束实验标志
                            m_IsJinBaoBeginOrEndTest = true;
                        }
                    }
                    GButton[i].OnButtonDown();
                }
            }
        }

        public void OnButtonUp(int x, int y)
        {
            for (int i = 0; i < 3; i++)
            {
                if (GButton[i].Contains(x, y))
                {
                    if (i < 2)
                    {
                        OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[i], 0, 0);
                    }
                    GButton[i].OnButtonUp();
                }
            }
        }
    }
}