using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V2C1DriverID : baseClass
    {
        //private Button1 _btn_Nr_1;
        //private Button1 _btn_Nr_2;
        //private Button1 _btn_Nr_3;
        //private Button1 _btn_Nr_4;
        //private Button1 _btn_Nr_5;
        //private Button1 _btn_Nr_6;
        //private Button1 _btn_Nr_7;
        //private Button1 _btn_Nr_8;
        //private Button1 _btn_Nr_9;
        //private Button1 _btn_Nr_0;
        //private Button _btn_Driver;
        //private Button _btn_Cancel;
        //private Button _btn_OK;

        private List<Button> m_BtnNr = new List<Button>();
        private List<Int32> m_DriverID = new List<int>();
        private List<Image> m_Images = new List<Image>();

        public override string GetInfo()
        {
            return "确认与列表按钮";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(
               a =>
               {
                   using (FileStream fs = new FileStream(RecPath + "\\" + a, FileMode.Open))
                   {
                       m_Images.Add(Image.FromStream(fs));
                   }
               }
               );

            for (int i = 0; i < 10; i++)
            {
                Button btn = new Button(
                    ((i + 1) % 10).ToString(),
                    new Rectangle(80 + 80 * (i % 3), 140 + 60 * (i / 3), 60, 50),
                    (i + 1) % 10,
                    new ButtonStyle()
                    {
                        Background = m_Images[0],
                        DownImage = m_Images[1],
                        FontStyle = new FontStyleEs()
                        {
                            Font = Global.m_FontArial13B,
                            TextBrush = (SolidBrush)Brushes.Black,
                            StringFormat = Global.m_SfCc
                        }
                    });
                btn.ClickEvent += btn_ClickEvent;
                m_BtnNr.Add(btn);
            }

            //_btn_Nr_1 = new Button1(new Rectangle(80, 140, 60, 60), null, null, 1);
            //_btn_Nr_1.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            //_btn_Nr_2 = new Button1(new Rectangle(160, 140, 60, 60), null, null, 2);
            //_btn_Nr_2.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            //_btn_Nr_3 = new Button1(new Rectangle(240, 140, 60, 60), null, null, 3);
            //_btn_Nr_3.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            //_btn_Nr_4 = new Button1(new Rectangle(80, 200, 60, 60), null, null, 4);
            //_btn_Nr_4.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            //_btn_Nr_5 = new Button1(new Rectangle(160, 200, 60, 60), null, null, 5);
            //_btn_Nr_5.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            //_btn_Nr_6 = new Button1(new Rectangle(240, 200, 60, 60), null, null, 6);
            //_btn_Nr_6.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            //_btn_Nr_7 = new Button1(new Rectangle(80, 260, 60, 60), null, null, 7);
            //_btn_Nr_7.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            //_btn_Nr_8 = new Button1(new Rectangle(160, 260, 60, 60), null, null, 8);
            //_btn_Nr_8.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            //_btn_Nr_9 = new Button1(new Rectangle(240, 260, 60, 60), null, null, 9);
            //_btn_Nr_9.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            //_btn_Nr_0 = new Button1(new Rectangle(80, 320, 60, 60), null, null, 0);
            //_btn_Nr_0.MouseUpEvent += _btn_Nr_1_MouseUpEvent;

            Button btnDriver = new Button(
                "司机",
                new Rectangle(395, 200, 155, 50),
                10,
                new ButtonStyle()
                {
                    Background = m_Images[0],
                    DownImage = m_Images[1],
                    FontStyle = new FontStyleEs()
                    {
                        Font = Global.m_FontArial13B,
                        TextBrush = (SolidBrush)Brushes.Black,
                        StringFormat = Global.m_SfCc
                    }
                });
            btnDriver.ClickEvent += btn_ClickEvent;
            m_BtnNr.Add(btnDriver);

            Button btnCancel = new Button(
                "取消",
                new Rectangle(395, 260, 155, 50),
                11,
                new ButtonStyle()
                {
                    Background = m_Images[0],
                    DownImage = m_Images[1],
                    FontStyle = new FontStyleEs()
                    {
                        Font = Global.m_FontArial13B,
                        TextBrush = (SolidBrush)Brushes.Black,
                        StringFormat = Global.m_SfCc
                    }
                });
            btnCancel.ClickEvent += btn_ClickEvent;
            m_BtnNr.Add(btnCancel);
            Button btnOk = new Button(
                "确定",
                new Rectangle(395, 320, 155, 50),
                12,
                new ButtonStyle()
                {
                    Background = m_Images[0],
                    DownImage = m_Images[1],
                    FontStyle = new FontStyleEs()
                    {
                        Font = Global.m_FontArial13B,
                        TextBrush = (SolidBrush)Brushes.Black,
                        StringFormat = Global.m_SfCc
                    }
                });
            btnOk.ClickEvent += btn_ClickEvent;
            m_BtnNr.Add(btnOk);

            return true;
        }

        private void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 10:
                    break;
                case 11:
                    _driverID = String.Empty;
                    break;
                case 12:
                    V1C6SystemInfo.m_DriverID = _driverID;
                    break;
                default:
                    if (_driverID.Length < 4)
                        _driverID += e.Message.ToString();
                    break;
            }
        }

        public override bool mouseUp(Point point)
        {
            m_BtnNr.ForEach(a => a.MouseUp(point));

            return base.mouseUp(point);
        }

        public override bool mouseDown(Point point)
        {
            m_BtnNr.ForEach(a => a.MouseDown(point));

            return base.mouseUp(point);
        }

        void _btn_Nr_1_MouseUpEvent(int obj)
        {
            if (_driverID.Length < 4)
                _driverID += obj.ToString();
        }

        private String _driverID = String.Empty;
        private SolidBrush m_Sb1 = new SolidBrush(Color.FromArgb(0, 204, 255));
        private SolidBrush m_Sb2 = new SolidBrush(Color.FromArgb(51, 153, 102));

        public override void paint(Graphics g)
        {
            g.FillRectangle(m_Sb2, new Rectangle(4, 56, 791, 410));
            g.FillRectangle(m_Sb2, new Rectangle(286, 560, 298, 40));

            m_BtnNr.ForEach(a => a.Paint(g));
            g.FillRectangle(Brushes.Black, new Rectangle(328, 66, 274, 52));
            g.DrawString("司机", Global.m_FontArial28B, Brushes.White, new PointF(328, 70));
            g.DrawString(_driverID.Length >= 1 ? _driverID[0].ToString() : "-", Global.m_FontArial28, m_Sb1, new PointF(480, 70));
            g.DrawString(_driverID.Length >= 2 ? _driverID[1].ToString() : "-", Global.m_FontArial28, m_Sb1, new PointF(510, 70));
            g.DrawString(_driverID.Length >= 3 ? _driverID[2].ToString() : "-", Global.m_FontArial28, m_Sb1, new PointF(540, 70));
            g.DrawString(_driverID.Length >= 4 ? _driverID[3].ToString() : "-", Global.m_FontArial28, m_Sb1, new PointF(570, 70));

            //g.FillRectangle(Brushes.White, new Rectangle(80, 140, 60, 50));
            //g.DrawString("1", Global.Font_Arial_28, Brushes.Black, new PointF(95, 145));

            //g.FillRectangle(Brushes.White, new Rectangle(160, 140, 60, 50));
            //g.DrawString("2", Global.Font_Arial_28, Brushes.Black, new PointF(175, 145));

            //g.FillRectangle(Brushes.White, new Rectangle(240, 140, 60, 50));
            //g.DrawString("3", Global.Font_Arial_28, Brushes.Black, new PointF(255, 145));

            //g.FillRectangle(Brushes.White, new Rectangle(80, 200, 60, 50));
            //g.DrawString("4", Global.Font_Arial_28, Brushes.Black, new PointF(95, 205));

            //g.FillRectangle(Brushes.White, new Rectangle(160, 200, 60, 50));
            //g.DrawString("5", Global.Font_Arial_28, Brushes.Black, new PointF(175, 205));

            //g.FillRectangle(Brushes.White, new Rectangle(240, 200, 60, 50));
            //g.DrawString("6", Global.Font_Arial_28, Brushes.Black, new PointF(255, 205));

            //g.FillRectangle(Brushes.White, new Rectangle(80, 260, 60, 50));
            //g.DrawString("7", Global.Font_Arial_28, Brushes.Black, new PointF(95, 265));

            //g.FillRectangle(Brushes.White, new Rectangle(160, 260, 60, 50));
            //g.DrawString("8", Global.Font_Arial_28, Brushes.Black, new PointF(175, 265));

            //g.FillRectangle(Brushes.White, new Rectangle(240, 260, 60, 50));
            //g.DrawString("9", Global.Font_Arial_28, Brushes.Black, new PointF(255, 265));

            //g.FillRectangle(Brushes.White, new Rectangle(80, 320, 60, 50));
            //g.DrawString("0", Global.Font_Arial_28, Brushes.Black, new PointF(95, 325));

            //g.FillRectangle(Brushes.White, new Rectangle(395, 200, 155, 50));
            //g.DrawString("司机", Global.Font_Arial_23, Brushes.Black, new PointF(425, 210));

            //g.FillRectangle(Brushes.White, new Rectangle(395, 260, 155, 50));
            //g.DrawString("取消", Global.Font_Arial_23, Brushes.Black, new PointF(425, 270));

            //g.FillRectangle(Brushes.White, new Rectangle(395, 320, 155, 50));
            //g.DrawString("确认", Global.Font_Arial_23, Brushes.Black, new PointF(425, 330));

            base.paint(g);
        }
    }
}
