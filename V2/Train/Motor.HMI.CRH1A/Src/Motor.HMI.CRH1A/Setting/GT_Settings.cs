using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Drawing.Drawing2D;
using Motor.HMI.CRH1A.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;

namespace Motor.HMI.CRH1A
{
    /// <summary>
    ///   ���ò˵�����ҳ�� �Ӹ�ҳ����Խ����л��û�
    ///  ���ظ�λ�Ӳ˵�
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    class GT_Settings : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("����");//�˵�����
        public Rectangle Recposition = new Rectangle(0, 185, 70, 40);
        public Rectangle Rect;//�˵����Ĵ���ο�
        public SolidBrush Brush = new SolidBrush(Color.FromArgb(119, 136, 153));//�˵�������ˢ
        public Pen Pen = new Pen(Color.FromArgb(217, 223, 229), 3);
        public CRH1AButton[] GButton = new CRH1AButton[5];
        public GDIRectText GText;
        public int Weight = 100;
        public int Height = 60;
        public Image[] Image = new Image[2];
        public static float Valuef = 90;//��Ļ���ȳ�ʼֵ�ݶ�Ϊ50
        public override string GetInfo()
        {
            return "������ͼ";
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
            GetValue();
            DrawOn(dcGs);
            base.paint(dcGs);
        }
        public void GetValue()
        {
            if (UIObj.InFloatList.Count > 0)
            {
               // Valuef = FloatList[UIObj.InFloatList[0]];
            }

            GText.SetText(Valuef.ToString());
        }
        public void InitData()
        {
            Rect = new Rectangle(Recposition.X + 3, Recposition.Y, 785, 240);
            for (int i = 0; i < 5; i++)
            {
                GButton[i] = new CRH1AButton();
                GButton[i].SetButtonColor(192, 192, 192);
            }
            GButton[0].SetButtonRect(Recposition.X + 220, Recposition.Y + 50, Weight, Height);
            GButton[0].SetButtonText("�л��û�");

            GButton[1].SetButtonRect(Recposition.X + 350, Recposition.Y + 50, Weight, Height);
            GButton[1].SetButtonText("ע��");

            GButton[2].SetButtonRect(Recposition.X + 220, Recposition.Y + 130, Weight, Height);
            GButton[2].SetButtonText("���ظ�λ");

            GButton[3].SetButtonRect(Recposition.X + 500, Recposition.Y + 50, 60, Height);
            GButton[4].SetButtonRect(Recposition.X + 625, Recposition.Y + 50, 60, Height);

            GText = new GDIRectText();
            GText.SetBkColor(255, 255, 225);
            GText.Isdrawrectfrm = true;
            GText.SetLinePen(231, 231, 231, 3);
            GText.SetTextColor(0, 0, 0);
            GText.SetTextRect(Recposition.X + 565, Recposition.Y + 52, 55, 55);
            GText.SetTextStyle(12, FormatStyle.Center, true, "Arial");
        }
        public void DrawOn(Graphics g)
        {

            //���Ʋ˵�����
            Title.OnDraw(g);
            g.FillRectangle(Brush, Rect);

            g.DrawRectangle(Pen, Rect);
            for (int i = 0; i < 5; i++)
            {
                GButton[i].OnDraw(g);
            }
            g.DrawImage(Image[0], Recposition.X + 512, Recposition.Y + 65);
            g.DrawImage(Image[1], Recposition.X + 640, Recposition.Y + 63);
            GText.OnDraw(g);
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
            for (int i = 0; i < 5; i++)
            {
                if (GButton[i].OutLineRectangle.Contains(x,y))
                {

                    GButton[i].OnButtonDown();
                }
            }
        }

        public void OnButtonUp(int x, int y)
        {
            for (int i = 0; i < 5; i++)
            {
                if (GButton[i].OutLineRectangle.Contains(x, y))
                {
                    switch (i)
                    {
                        case 0:
                            OnPost(CmdType.ChangePage, 27, 0, 0);//�л��û�
                            break;
                        case 1:

                            break;
                        case 2:
                            OnPost(CmdType.ChangePage, 28, 0, 0);//���ظ�λ 
                            break;
                        case 3:
                            if (Valuef > 0)
                            {
                                Valuef-=10;
                              //  OnPost(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, Valuef);
                            }
                            break;
                        case 4:
                            if (Valuef < 100)  //�������ռ��Ϊ100��
                            {
                                Valuef+=10;
                               // OnPost(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, Valuef);
                            }
                            break;
                    }
                    GButton[i].OnButtonUp();
                }
            }
        }
    }
}
