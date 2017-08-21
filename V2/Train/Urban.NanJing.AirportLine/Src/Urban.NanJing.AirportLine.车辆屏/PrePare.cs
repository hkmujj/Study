using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace Urban.NanJing.AirportLine.DDU
{
    [GksDataType(DataType.isMMIObjectClass)]
    class Prepare : baseClass
    {
        private PrepareData1 m_Jiechu;
        private PrepareData1 m_Nibian;
        private Image[] m_ImageArray;
        private PrepareData2 m_PrepareData2;
        private PrepareData3 m_PrepareData3;

        private int[] m_TemputureArray = { 23, 24, 25, 26, 27 };

        public override string GetInfo()
        {
            return "准备";
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                switch (Title.m_Model)
                {
                    case BoardCast.Model.Auto:
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2], 1, 0);
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 0, 0);
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                        break;
                    case BoardCast.Model.HalfAuto:
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2], 0, 0);
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 1, 0);
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                        break;
                    case BoardCast.Model.Manual:
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2], 0, 0);
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 0, 0);
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                        break;
                }
            }
            base.setRunValue(nParaA, nParaB, nParaC);
        }
        /// <summary>
        /// 当前温度
        /// </summary>
        /// <param name="message"></param>
        private void OnButton3UpHandler(int message)
        {
            for (int i = 0; i < 5; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    m_PrepareData2.Value = m_TemputureArray[i];
                }
            }
        }

        public override bool init(ref int nErrorObjectIndex)
        {

            m_ImageArray = new Image[UIObj.ParaList.Count];

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                using (FileStream fs = new FileStream(RecPath + "\\" + UIObj.ParaList[i], FileMode.Open))
                {
                    m_ImageArray[i] = Image.FromStream(fs);
                }
            }


            m_Jiechu = new PrepareData1(new Rectangle(Common.m_InitialPosX + 4, Common.m_InitialPosY + 300, 670, 42), "接触器", 3);
            m_Nibian = new PrepareData1(new Rectangle(Common.m_InitialPosX + 4, Common.m_InitialPosY + 350, 670, 55), "辅助逆变器", 2);

            m_PrepareData2 = new PrepareData2(new Rectangle(Common.m_InitialPosX + 4, Common.m_InitialPosY + 410, 525, 145));

            m_PrepareData2.m_Button1.m_ImageUp = m_ImageArray[7];
            m_PrepareData2.m_Button2.m_ImageUp = m_ImageArray[8];
            m_PrepareData2.m_Button3.m_ImageUp = m_ImageArray[9];
            m_PrepareData2.m_Button4.m_ImageUp = m_ImageArray[10];


            m_PrepareData2.m_Button1.m_ImageDown = m_ImageArray[11];
            m_PrepareData2.m_Button2.m_ImageDown = m_ImageArray[12];
            m_PrepareData2.m_Button3.m_ImageDown = m_ImageArray[13];
            m_PrepareData2.m_Button4.m_ImageDown = m_ImageArray[14];

            m_PrepareData2.m_UpButton.m_ImageUp = m_ImageArray[15];
            m_PrepareData2.m_DownButton.m_ImageUp = m_ImageArray[16];

            m_PrepareData2.m_Button3.MouseUpEvent += OnButton3UpHandler;


            m_PrepareData3 = new PrepareData3(new Rectangle(Common.m_InitialPosX + 535, Common.m_InitialPosY + 410, 185, 40));
            return true;
        }

        private void GetValue()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[0] + i * 3 + j])
                    {
                        if (i == 1)
                            m_Jiechu.m_StateList[i].ImagePicture = m_ImageArray[17 + j];
                        else
                            m_Jiechu.m_StateList[i].ImagePicture = m_ImageArray[j];
                    }
                }
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[UIObj.InBoolList[1] + i * 4 + j])
                    {
                        m_Nibian.m_StateList[i].ImagePicture = m_ImageArray[3 + j];
                    }
                }
            }
        }


        public override bool mouseDown(Point point)
        {
            m_PrepareData2.OnMouseDown(point);
            return true;
        }

        public override bool mouseUp(Point point)
        {
            m_PrepareData2.OnMouseUp(point);
            return true;
        }


        public override void paint(Graphics g)
        {
            GetValue();
            m_Jiechu.OnDraw(g);
            m_Nibian.OnDraw(g);
            m_PrepareData2.OnDraw(g);
            m_PrepareData3.OnDraw(g);
        }
    }

    public class PrepareData1
    {
        private RectText m_Rect;
        private string m_Label;
        public List<RectText> m_StateList = new List<RectText>();
        private Point m_LabelPoint;

        public PrepareData1(Rectangle rect, string label, int num)
        {
            m_Label = label;
            m_Rect = new RectText(rect, "", 13, 1, Common.m_WhiteColor, Common.m_BackGroundColor, Color.Blue, 1, true, null, true);

            m_LabelPoint = new Point(rect.X + 6, rect.Y + 10);

            int perlength = 0;
            if (num == 3)
            {
                perlength = 180;
            }
            else if (num == 2)
            {
                perlength = 360;
            }

            for (int i = 0; i < num; i++)
            {
                m_StateList.Add(new RectText(new Rectangle(Common.m_InitialPosX + Common.m_FirstTrainRect.X + perlength * i, rect.Y + 3, Common.m_FirstTrainRect.Width, rect.Height - 6), "", 13, 1, Common.m_WhiteColor, Common.m_BackGroundColor, Color.Black, 1, true));
            }
        }

        public void OnDraw(Graphics g)
        {
            m_Rect.OnDraw(g);
            g.DrawString(m_Label, Common.m_16Font, Common.m_BlueBrush, m_LabelPoint);

            foreach (var v in m_StateList)
            {
                v.OnDraw(g);
            }
        }
    }

    public class PrepareData2
    {
        private Rectangle m_RectFrame;

        public int Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
                m_Text.Text = m_Value.ToString() + "°";
                Title.m_Temperature.Text = Value.ToString() + "°";
            }
        }
        private int m_Value = 19;

        private RectText m_Text;

        public Button m_Button1;
        public Button m_Button2;
        public Button m_Button3;
        public Button m_Button4;

        private bool m_IsTempetureOn;

        public Button m_UpButton;
        public Button m_DownButton;


        public bool IsTextVisual
        {
            get
            {
                return m_IsTempetureOn;
            }
            set
            {
                m_IsTempetureOn = value;
            }
        }
        private bool m_IsTextVisual = false;

        private void OnTempetureOnHandler(int message)
        {
            m_IsTempetureOn = true;
        }

        private void OnTempetureOffHandler(int message)
        {
            m_IsTempetureOn = false;
        }

        private void OnTextVisualHandler(int message)
        {
            m_IsTextVisual = true;
        }

        private void OnTextUnVisualHandler(int message)
        {
            m_IsTextVisual = false;
        }



        public PrepareData2(Rectangle rect)
        {
            m_RectFrame = rect;
            m_Button1 = new Button(new Rectangle(m_RectFrame.X + 205, m_RectFrame.Y + 10, 150, 60), null, null);

            m_Button1.MouseUpEvent += OnTextVisualHandler;
            m_Button1.MouseUpEvent += OnTempetureOffHandler;

            m_Button2 = new Button(new Rectangle(m_RectFrame.X + 370, m_RectFrame.Y + 10, 150, 60), null, null);

            m_Button2.MouseUpEvent += OnTextUnVisualHandler;
            m_Button1.MouseUpEvent += OnTempetureOffHandler;

            m_Button3 = new Button(new Rectangle(m_RectFrame.X + 205, m_RectFrame.Y + 80, 150, 60), null, null);

            m_Button3.MouseUpEvent += OnTempetureOnHandler;
            m_Button3.MouseUpEvent += OnTextVisualHandler;

            m_Button4 = new Button(new Rectangle(m_RectFrame.X + 370, m_RectFrame.Y + 80, 150, 60), null, null);
            m_Button4.MouseUpEvent += OnTextUnVisualHandler;
            m_Button1.MouseUpEvent += OnTempetureOffHandler;

            m_UpButton = new Button(new Rectangle(m_RectFrame.X + 145, m_RectFrame.Y + 4, 50, 48), null, null);

            m_UpButton.MouseUpEvent += OnUpButtonHandler;


            m_DownButton = new Button(new Rectangle(m_RectFrame.X + 145, m_RectFrame.Y + 94, 50, 48), null, null);
            m_DownButton.MouseUpEvent += OnDownButtonHandler;


            m_Text = new RectText(new Rectangle(m_RectFrame.X + 140, m_RectFrame.Y + 60, 60, 30), "19°", 13, 1, Color.Black, Common.m_WhiteColor, Color.Black, 1, true, null, true);
        }

        private void OnDownButtonHandler(int message)
        {
            Value = Value - 1 < 19 ? 19 : Value - 1;

        }

        private void OnUpButtonHandler(int message)
        {
            Value = Value + 1 > 27 ? 27 : Value + 1;

        }

        public void OnDraw(Graphics g)
        {
            g.DrawRectangle(Common.m_BluePen, m_RectFrame);
            m_Text.OnDraw(g);
            m_Button1.OnDraw(g);
            m_Button2.OnDraw(g);
            m_Button3.OnDraw(g);
            m_Button4.OnDraw(g);

            if (m_IsTextVisual)
            {
                g.DrawString("客室空调温度", new Font("Arial", 13), new SolidBrush(Color.Blue), new Point(m_RectFrame.X + 5, m_RectFrame.Y + m_RectFrame.Height / 2 - 10));
            }

            if (m_IsTempetureOn)
            {
                m_UpButton.OnDraw(g);
                m_DownButton.OnDraw(g);
            }
        }

        public void OnMouseUp(Point p)
        {
            m_Button1.MouseUp(p);
            m_Button2.MouseUp(p);
            m_Button3.MouseUp(p);
            m_Button4.MouseUp(p);

            if (m_IsTempetureOn)
            {
                m_UpButton.MouseUp(p);
                m_DownButton.MouseUp(p);
            }
        }

        public void OnMouseDown(Point p)
        {
            m_Button1.MouseDown(p);
            m_Button2.MouseDown(p);
            m_Button3.MouseDown(p);
            m_Button4.MouseDown(p);

            if (m_IsTempetureOn)
            {
                m_UpButton.MouseDown(p);
                m_DownButton.MouseDown(p);
            }
        }


    }


    public class PrepareData3
    {
        private Rectangle m_RectFrame;

        private RectText m_Rect1;

        private RectText m_RectA1;
        private RectText m_RectA1Text;

        private RectText m_RectC1;
        private RectText m_RectC1Text;

        private RectText m_RectC2;
        private RectText m_RectC2Text;

        private RectText m_RectA2;
        private RectText m_RectA2Text;

        public PrepareData3(Rectangle rect)
        {
            m_RectFrame = rect;

            m_Rect1 = new RectText(new Rectangle(m_RectFrame.X, m_RectFrame.Y, 674 - m_RectFrame.X + Common.m_InitialPosX, 35), "自检状态", 13, 1, Color.Black, Common.m_BackGroundColor, Color.Blue, 1, true, null, true);

            m_RectA1 = new RectText(new Rectangle(m_RectFrame.X, m_RectFrame.Y + 35, 37, 25), "A1", 13, 1, Color.Black, Common.m_BackGroundColor, Color.Blue, 1, true, null, true);
            m_RectA1Text = new RectText(new Rectangle(m_RectFrame.X + 37, m_RectFrame.Y + 35, 637 - m_RectFrame.X + Common.m_InitialPosX, 25), "未实现", 13, 1, Color.Black, Common.m_BackGroundColor, Color.Blue, 1, true);

            m_RectC1 = new RectText(new Rectangle(m_RectFrame.X, m_RectFrame.Y + 60, 37, 25), "C1", 13, 1, Color.Black, Common.m_BackGroundColor, Color.Blue, 1, true, null, true);
            m_RectC1Text = new RectText(new Rectangle(m_RectFrame.X + 37, m_RectFrame.Y + 60, 637 - m_RectFrame.X + Common.m_InitialPosX, 25), "未实现", 13, 1, Color.Black, Common.m_BackGroundColor, Color.Blue, 1, true);

            m_RectC2 = new RectText(new Rectangle(m_RectFrame.X, m_RectFrame.Y + 85, 37, 25), "C2", 13, 1, Color.Black, Common.m_BackGroundColor, Color.Blue, 1, true, null, true);
            m_RectC2Text = new RectText(new Rectangle(m_RectFrame.X + 37, m_RectFrame.Y + 85, 637 - m_RectFrame.X + Common.m_InitialPosX, 25), "未实现", 13, 1, Color.Black, Common.m_BackGroundColor, Color.Blue, 1, true);

            m_RectA2 = new RectText(new Rectangle(m_RectFrame.X, m_RectFrame.Y + 110, 37, 25), "A2", 13, 1, Color.Black, Common.m_BackGroundColor, Color.Blue, 1, true, null, true);
            m_RectA2Text = new RectText(new Rectangle(m_RectFrame.X + 37, m_RectFrame.Y + 110, 637 - m_RectFrame.X + Common.m_InitialPosX, 25), "未实现", 13, 1, Color.Black, Common.m_BackGroundColor, Color.Blue, 1, true);
        }

        public void OnDraw(Graphics g)
        {
            m_Rect1.OnDraw(g);

            m_RectA1.OnDraw(g);
            m_RectA1Text.OnDraw(g);

            m_RectC1.OnDraw(g);
            m_RectC1Text.OnDraw(g);

            m_RectC2.OnDraw(g);
            m_RectC2Text.OnDraw(g);

            m_RectA2.OnDraw(g);
            m_RectA2Text.OnDraw(g);
        }
    }

}
