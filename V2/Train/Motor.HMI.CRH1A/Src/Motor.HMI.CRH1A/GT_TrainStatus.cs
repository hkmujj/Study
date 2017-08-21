using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Global;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;

namespace Motor.HMI.CRH1A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    class GT_TrainStatus : CRH1BaseClass
    {
        private GT_MenuTitle m_Title = new GT_MenuTitle("列车状态");//菜单标题
        private Rectangle m_Recposition = new Rectangle(0, 178, 790, 277);
        private CRH1AButton[] m_GButton = new CRH1AButton[2];
        private Pen m_Pen = new Pen(Color.FromArgb(210, 210, 210));
        private Rectangle[] m_NoRect = new Rectangle[22];//显示编号的小矩形框
        private Rectangle[] m_StrRect = new Rectangle[22];//显示状态的标题的矩形框
        private GDIRectText[] m_GText = new GDIRectText[22];//显示状态信息
        private SolidBrush m_Brush = new SolidBrush(Color.FromArgb(255, 255, 225));
        private SolidBrush m_Whitebrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        private SolidBrush m_Blackbrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        private Pen m_RectPen = new Pen(Color.FromArgb(55, 55, 55), 2);
        private Font m_Font = new Font("Arial", 11);
        private string[] m_Strtitle ={ "元件/系统", "紧急制动回路", "停放制动", "牵引封锁", "全部门关闭", "牵引力", "制动力", "全部主断路器", "接触网电流",
                                    "主风缸压力正常", "车轮防滑保护启动","元件/系统", "回送", "限速"," "," "," "," "," "," "," "," "};//暂时有12条 可以根据需要添加和删除

        #region 接口数据
        private bool[] m_BoolValue = new bool[8];
        private float m_TrainSatusTraction;
        private float m_TrainStatusBrake;
        private float m_TrainStatusJieChuWangDianLiu;
        private float m_TrainStatusXianShu;

        #endregion
        public override string GetInfo()
        {
            return "列车状态";
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
            if (UIObj.InBoolList.Count >= 8)
            {
                for (int index = 0; index < 8; index++)
                {
                    m_BoolValue[index] = BoolList[UIObj.InBoolList[index]];
                }
            }

            m_TrainSatusTraction = FloatList[UIObj.InFloatList[0]];
            m_TrainStatusBrake = FloatList[UIObj.InFloatList[1]];
            m_TrainStatusJieChuWangDianLiu = FloatList[UIObj.InFloatList[2]];
            m_TrainStatusXianShu = FloatList[UIObj.InFloatList[3]]*GlobalInfo.Instance.Crh1AConfig.AdaptConfig.LimitSpeedCoefficient;
            GlobalParam.Instance.TrainInfo.MaxSpeed = m_TrainStatusXianShu;

        }
        public void InitData()
        {   ///////////底部按钮初始化
            m_GButton[0] = new CRH1AButton();
            m_GButton[0].SetButtonRect(m_Recposition.X + 624, m_Recposition.Y + 335, 80, 50);
            m_GButton[0].SetButtonColor(192, 192, 192);
            m_GButton[0].SetButtonText("主菜单");
            m_GButton[1] = new CRH1AButton();
            m_GButton[1].SetButtonColor(192, 192, 192);
            m_GButton[1].SetButtonRect(m_Recposition.X + 710, m_Recposition.Y + 335, 80, 50);
            m_GButton[1].SetButtonText("运行");

            #region::::::::::::显示编号的小矩形框初始化:::::::::::

            for (int i = 0; i < 22; i++)
            {
                if (i < 11)
                {
                    m_NoRect[i] = new Rectangle(m_Recposition.X, m_Recposition.Y + 25 * i, 30, 25);
                }
                else
                {
                    m_NoRect[i] = new Rectangle(m_Recposition.X + 400, m_Recposition.Y + 25 * (i - 11), 30, 25);
                }
            }
            #endregion

            #region::::::::::::显示状态的标题的矩形框的初始化:::::::::::::

            for (int i = 0; i < 22; i++)
            {
                if (i < 11)
                {
                    m_StrRect[i] = new Rectangle(m_Recposition.X + 30, m_Recposition.Y + 25 * i, 250, 25);
                }
                else
                {
                    m_StrRect[i] = new Rectangle(m_Recposition.X + 430, m_Recposition.Y + 25 * (i - 11), 250, 25);
                }
            }
            #endregion

            #region ::;;;;;:::::::::显 示 状  态 信 息;::;;;::;;;;;::::::::

            for (int i = 0; i < 20; i++)
            {
                m_GText[i] = new GDIRectText();
                m_GText[i].SetBkColor(255, 255, 255);
                m_GText[i].SetTextColor(0, 0, 0);
                m_GText[i].SetTextStyle(11, FormatStyle.Center, false, "Arial");
                if (i < 10)
                {
                    m_GText[i].SetTextRect(m_Recposition.X + 280, m_Recposition.Y + (i + 1) * 25, 120, 25);
                }
                else
                {
                    m_GText[i].SetTextRect(m_Recposition.X + 680, m_Recposition.Y + (i - 9) * 25, 120, 25);
                }

            }
            m_GText[20] = new GDIRectText();
            m_GText[20].SetBkColor(255, 255, 225);
            m_GText[20].SetTextColor(0, 0, 0);
            m_GText[20].SetTextStyle(11, FormatStyle.Center, false, "Arial");
            m_GText[20].SetTextRect(m_Recposition.X + 280, m_Recposition.Y, 120, 25);
            m_GText[20].SetText("状态");

            m_GText[21] = new GDIRectText();
            m_GText[21].SetBkColor(255, 255, 225);
            m_GText[21].SetTextColor(0, 0, 0);
            m_GText[21].SetTextStyle(11, FormatStyle.Center, false, "Arial");
            m_GText[21].SetTextRect(m_Recposition.X + 680, m_Recposition.Y, 120, 25);
            m_GText[21].SetText("状态");

            #endregion
        }
        public void DrawOn(Graphics g)
        {

            //绘制菜单标题
            m_Title.OnDraw(g);
            //绘制底部按钮
            for (int i = 0; i < 2; i++)
            {
                m_GButton[i].OnDraw(g);
            }
            //绘制状态信息显示的矩形框
            g.DrawRectangle(m_RectPen, m_Recposition);

            //绘制编号框及其内容
            for (int i = 0; i < 22; i++)
            {
                g.FillRectangle(m_Brush, m_NoRect[i]);
                if (i > 0 && i < 11)
                    g.DrawString(i.ToString(), m_Font, m_Blackbrush, m_NoRect[i]);
                if (i > 11)
                {
                    g.DrawString((i - 1).ToString(), m_Font, m_Blackbrush, m_NoRect[i]);
                }
            }
            //绘制状态信息标题
            for (int i = 0; i < 22; i++)
            {
                if (i == 0 || i == 11)
                {
                    g.FillRectangle(m_Brush, m_StrRect[i]);
                    g.DrawString(m_Strtitle[i], m_Font, m_Blackbrush, m_StrRect[i]);
                }
                else
                {
                    g.FillRectangle(m_Whitebrush, m_StrRect[i]);
                    g.DrawString(m_Strtitle[i], m_Font, m_Blackbrush, m_StrRect[i]);
                }
            }

            //绘制状态信息
            StatusSetting();//更新状态
            for (int i = 0; i < 22; i++)
            {
                m_GText[i].OnDraw(g);
            }

            //绘制网格
            for (int i = 0; i <= 275; i += 25)
            {
                g.DrawLine(m_Pen, m_Recposition.X, m_Recposition.Y + i, m_Recposition.X + 800, m_Recposition.Y + i);
            }
            g.DrawLine(m_Pen, m_Recposition.X + 30, m_Recposition.Y, m_Recposition.X + 30, m_Recposition.Y + 275);
            g.DrawLine(m_Pen, m_Recposition.X + 280, m_Recposition.Y, m_Recposition.X + 280, m_Recposition.Y + 275);
            g.DrawLine(m_Pen, m_Recposition.X + 400, m_Recposition.Y, m_Recposition.X + 400, m_Recposition.Y + 275);
            g.DrawLine(m_Pen, m_Recposition.X + 430, m_Recposition.Y, m_Recposition.X + 430, m_Recposition.Y + 275);
            g.DrawLine(m_Pen, m_Recposition.X + 680, m_Recposition.Y, m_Recposition.X + 680, m_Recposition.Y + 275);


        }

        private void StatusSetting()
        {
            //紧急制动回路
            if (m_BoolValue[0])
            {
                m_GText[0].SetText("打开");
            }
            else
            {
                m_GText[0].SetText("关闭");
            }

            //停放制动
            if (m_BoolValue[1])
            {
                m_GText[1].SetText("开");
            }
            else
            {
                m_GText[1].SetText("关");
            }

            //牵引封锁
            if (m_BoolValue[2])
            {
                m_GText[2].SetText("开");
            }
            else
            {
                m_GText[2].SetText("关");
            }

            //全部门关闭
            if (m_BoolValue[3])
            {
                m_GText[3].SetText("有");
            }
            else
            {
                m_GText[3].SetText("无");
            }

            // 牵引力
            m_GText[4].SetText(m_TrainSatusTraction.ToString("F0") + "        %");

            //制动力
            m_GText[5].SetText(m_TrainStatusBrake.ToString("F0") + "        %");


            //全部主断路器
            if (m_BoolValue[4])
            {
                m_GText[6].SetText("打开");
            }
            else
            {
                m_GText[6].SetText("关闭");
            }

            //接触网电流
            m_GText[7].SetText(m_TrainStatusJieChuWangDianLiu.ToString() + "       A");

            //主风缸压力正常
            if (m_BoolValue[5])
            {
                m_GText[8].SetText("是");
            }
            else
            {
                m_GText[8].SetText("否");
            }

            //车轮防滑保护启动
            if (m_BoolValue[6])
            {
                m_GText[9].SetText("有");
            }
            else
            {
                m_GText[9].SetText("无");
            }

            //会送/救援
            if (m_BoolValue[7])
            {
                m_GText[10].SetText("有");
            }
            else
            {
                m_GText[10].SetText("无");
            }

            //限速
            //G_Text[11].SetText(TrainStatus_XianShu.ToString());
            //////////////////////////
            //-ycl-
            /////////////////////////
            m_GText[11].SetText(GlobalParam.Instance.TrainInfo.MaxSpeedText);
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
            for (int i = 0; i < 2; i++)
            {
                if (m_GButton[i].Contains(x, y))
                {

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
                    switch (i)
                    {
                        case 0:
                            OnPost(CmdType.ChangePage, 1, 0, 0);
                            break;
                        case 1:
                            OnPost(CmdType.ChangePage, 3, 0, 0);
                            break;


                    }
                    m_GButton[i].OnButtonUp();
                }
            }

        }




    }
}