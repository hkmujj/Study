using System.Drawing;
using Motor.HMI.CRH1A.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;

namespace Motor.HMI.CRH1A
{
    /// <summary>
    ///  切换用户子菜单 此菜单的功能可以实现用户ID改变的功能 
    ///  可以从设置菜单进入该子菜单
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class GT_ChangeUser : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("设置");//菜单标题
        public Rectangle Recposition = new Rectangle(0, 168, 200, 283);
        public CRH1AButton[] GButton = new CRH1AButton[12];
        public Rectangle[] Rect = new Rectangle[3];
        public SolidBrush Rectbrush = new SolidBrush(Color.FromArgb(119, 135, 158));
        public Pen Rectpen = new Pen(Color.FromArgb(165, 177, 185), 3);
        public int Buttonweight = 60;
        public int Buttonheight = 60;
        public GDIRectText GText;//输入的ID显示框
        string m_Id;
        Font m_Strfont = new Font("Arial", 13);
        SolidBrush m_Whitebrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public override string GetInfo()
        {
            return "改变用户ID";
        }


        public override bool Initialize()
        {
            //3
            InitData();

            return true;
        }
        public override void paint(Graphics dcGs)
        {

            DrawOn(dcGs);
            base.paint(dcGs);
        }
        public void InitData()
        {
            for (int i = 0; i < 3; i++)
            {
                if (i < 2)
                    Rect[i] = new Rectangle(Recposition.X + (Recposition.Width + 5) * i + 5, Recposition.Y, Recposition.Width, Recposition.Height);
                else
                    Rect[i] = new Rectangle(Recposition.X + Recposition.Width * i + 15, Recposition.Y, Recposition.Width + 170, Recposition.Height);
            }
            GText = new GDIRectText();
            GText.SetBkColor(0, 0, 0);
            GText.SetTextColor(255, 255, 255);
            GText.SetTextStyle(12, FormatStyle.Center, true, "Arial");
            GText.Isdrawrectfrm = true;
            GText.SetTextRect(Rect[1].X + 30, Rect[1].Y + 120, 140, 40);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    GButton[i * 3 + j] = new CRH1AButton();
                    GButton[i * 3 + j].SetButtonColor(192, 192, 192);
                    if (i * 3 + j == 11)
                    {
                        GButton[i * 3 + j].SetButtonRect(Rect[2].X + j * (Buttonweight + 8) + 35, Rect[2].Y + i * (Buttonheight + 8) + 10, 2 * Buttonweight, Buttonheight);
                    }
                    else
                    {
                        GButton[i * 3 + j].SetButtonRect(Rect[2].X + j * (Buttonweight + 8) + 35, Rect[2].Y + i * (Buttonheight + 8) + 10, Buttonweight, Buttonheight);
                    }

                }
            }
            GButton[0].SetButtonText("7");
            GButton[1].SetButtonText("8");
            GButton[2].SetButtonText("9");
            GButton[3].SetButtonText("4");
            GButton[4].SetButtonText("5");
            GButton[6].SetButtonText("1");
            GButton[7].SetButtonText("2");
            GButton[8].SetButtonText("3");
            GButton[9].SetButtonText("清除");
            GButton[10].SetButtonText("0");
            GButton[11].SetButtonText("输 入");

        }
        public void DrawOn(Graphics g)
        {

            //绘制菜单标题
            Title.OnDraw(g);

            for (int i = 0; i < 3; i++)
            {
                g.FillRectangle(Rectbrush, Rect[i]);
                g.DrawRectangle(Rectpen, Rect[i]);
            }
            g.DrawString("请输入你的ID", m_Strfont, m_Whitebrush, Rect[0].X + 45, Rect[0].Y + 130);
            GText.OnDraw(g);
            for (int i = 0; i < 12; i++)
            {
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
        //鼠标响应事件
        public void OnButtonDown(int x, int y)
        {
            for (int i = 0; i < 12; i++)
            {
                if (GButton[i].OutLineRectangle.Contains(x, y))
                {
                    switch (i)
                    {
                        case 0:
                            m_Id += "7";
                            GText.SetText(m_Id);
                            break;
                        case 1:
                            m_Id += "8";
                            GText.SetText(m_Id);
                            break;
                        case 2:
                            m_Id += "9";
                            GText.SetText(m_Id);
                            break;
                        case 3:
                            m_Id += "4";
                            GText.SetText(m_Id);
                            break;
                        case 4:
                            m_Id += "5";
                            GText.SetText(m_Id);
                            break;
                        case 5:
                            m_Id += "6";
                            GText.SetText(m_Id);
                            break;
                        case 6:
                            m_Id += "1";
                            GText.SetText(m_Id);
                            break;
                        case 7:
                            m_Id += "2";
                            GText.SetText(m_Id);
                            break;
                        case 8:
                            m_Id += "3";
                            GText.SetText(m_Id);
                            break;
                        case 9:
                            m_Id = "";
                            GText.SetText(m_Id);
                            break;
                        case 10:
                            m_Id += "0";
                            GText.SetText(m_Id);
                            break;
                        case 11:

                            break;
                    }
                    GButton[i].OnButtonDown();
                }
            }
        }
        public void OnButtonUp(int x, int y)
        {
            for (int i = 0; i < 12; i++)
            {
                if (GButton[i].Contains(x, y))
                {
                    if (i == 11)
                    {
                        GButton[i].OnButtonUp();
                        OnPost(CmdType.ChangePage, 1, 0, 0);
                    }
                    else
                    {
                        GButton[i].OnButtonUp();
                    }

                }
            }

        }
    }
}