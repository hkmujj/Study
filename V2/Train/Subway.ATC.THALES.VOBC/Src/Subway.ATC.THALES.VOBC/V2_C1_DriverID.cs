using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Subway.ATC.THALES.VOBC
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V2_C1_DriverID : baseClass
    {
        private Button _btn_Nr_1;
        private Button _btn_Nr_2;
        private Button _btn_Nr_3;
        private Button _btn_Nr_4;
        private Button _btn_Nr_5;
        private Button _btn_Nr_6;
        private Button _btn_Nr_7;
        private Button _btn_Nr_8;
        private Button _btn_Nr_9;
        private Button _btn_Nr_0;
        private Button _btn_Driver;
        private Button _btn_Cancel;
        private Button _btn_OK;

        private List<int> driverID = new List<int>();

        public override string GetInfo()
        {
            return "确认与列表按钮";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            _btn_Nr_1 = new Button(new Rectangle(80, 140, 60, 60), null, null, 1);
            _btn_Nr_1.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            _btn_Nr_2 = new Button(new Rectangle(160, 140, 60, 60), null, null, 2);
            _btn_Nr_2.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            _btn_Nr_3 = new Button(new Rectangle(240, 140, 60, 60), null, null, 3);
            _btn_Nr_3.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            _btn_Nr_4 = new Button(new Rectangle(80, 200, 60, 60), null, null, 4);
            _btn_Nr_4.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            _btn_Nr_5 = new Button(new Rectangle(160, 200, 60, 60), null, null, 5);
            _btn_Nr_5.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            _btn_Nr_6 = new Button(new Rectangle(240, 200, 60, 60), null, null, 6);
            _btn_Nr_6.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            _btn_Nr_7 = new Button(new Rectangle(80, 260, 60, 60), null, null, 7);
            _btn_Nr_7.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            _btn_Nr_8 = new Button(new Rectangle(160, 260, 60, 60), null, null, 8);
            _btn_Nr_8.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            _btn_Nr_9 = new Button(new Rectangle(240, 260, 60, 60), null, null, 9);
            _btn_Nr_9.MouseUpEvent += _btn_Nr_1_MouseUpEvent;
            _btn_Nr_0 = new Button(new Rectangle(80, 320, 60, 60), null, null, 0);
            _btn_Nr_0.MouseUpEvent += _btn_Nr_1_MouseUpEvent;

            _btn_Driver = new Button(new Rectangle(395, 200, 155, 60), null, null, 0);
            _btn_Cancel = new Button(new Rectangle(395, 260, 155, 60), null, null, 0);
            _btn_Cancel.MouseUpEvent += _btn_Cancel_MouseUpEvent;
            _btn_OK = new Button(new Rectangle(395, 320, 155, 60), null, null, 0);
            _btn_OK.MouseUpEvent += _btn_OK_MouseUpEvent;

            return true;
        }

        void _btn_OK_MouseUpEvent(int obj)
        {
            V1_C6_SystemInfo.DriverID = _driverID;
            append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, Convert.ToInt32(_driverID));
        }

        void _btn_Cancel_MouseUpEvent(int obj)
        {
            _driverID = string.Empty;
        }

        public override bool mouseUp(Point nPoint)
        {
            _btn_Nr_1.MouseUp(nPoint);
            _btn_Nr_2.MouseUp(nPoint);
            _btn_Nr_3.MouseUp(nPoint);
            _btn_Nr_4.MouseUp(nPoint);
            _btn_Nr_5.MouseUp(nPoint);
            _btn_Nr_6.MouseUp(nPoint);
            _btn_Nr_7.MouseUp(nPoint);
            _btn_Nr_8.MouseUp(nPoint);
            _btn_Nr_9.MouseUp(nPoint);
            _btn_Nr_0.MouseUp(nPoint);
            _btn_Cancel.MouseUp(nPoint);
            _btn_OK.MouseUp(nPoint);

            return true;
        }

        void _btn_Nr_1_MouseUpEvent(int obj)
        {
            if (_driverID.Length < 4)
            {
                _driverID += obj.ToString();
            }
        }

        private string _driverID = string.Empty;

        public override void paint(Graphics dcGs)
        {
            dcGs.FillRectangle(new SolidBrush(Color.FromArgb(51, 153, 102)), new Rectangle(4, 58, 791, 378));
            //dcGs.FillRectangle(new SolidBrush(Color.FromArgb(51, 153, 102)), new Rectangle(286, 547, 298, 45));

            dcGs.FillRectangle(new SolidBrush(Color.Black), new Rectangle(328, 66, 274, 52));
            dcGs.DrawString("司机", new Font("Arial", 28, FontStyle.Bold), new SolidBrush(Color.White), new PointF(328, 70));
            dcGs.DrawString(_driverID.Length >= 1 ? _driverID[0].ToString() : "-", new Font("Arial", 28), new SolidBrush(Color.FromArgb(0, 204, 255)), new PointF(480, 70));
            dcGs.DrawString(_driverID.Length >= 2 ? _driverID[1].ToString() : "-", new Font("Arial", 28), new SolidBrush(Color.FromArgb(0, 204, 255)), new PointF(510, 70));
            dcGs.DrawString(_driverID.Length >= 3 ? _driverID[2].ToString() : "-", new Font("Arial", 28), new SolidBrush(Color.FromArgb(0, 204, 255)), new PointF(540, 70));
            dcGs.DrawString(_driverID.Length >= 4 ? _driverID[3].ToString() : "-", new Font("Arial", 28), new SolidBrush(Color.FromArgb(0, 204, 255)), new PointF(570, 70));

            dcGs.FillRectangle(new SolidBrush(Color.FromArgb(253, 254, 255)), new Rectangle(80, 140, 60, 50));
            dcGs.DrawString("1", new Font("Arial", 28), new SolidBrush(Color.Black), new PointF(95, 145));

            dcGs.FillRectangle(new SolidBrush(Color.FromArgb(253, 254, 255)), new Rectangle(160, 140, 60, 50));
            dcGs.DrawString("2", new Font("Arial", 28), new SolidBrush(Color.Black), new PointF(175, 145));

            dcGs.FillRectangle(new SolidBrush(Color.FromArgb(253, 254, 255)), new Rectangle(240, 140, 60, 50));
            dcGs.DrawString("3", new Font("Arial", 28), new SolidBrush(Color.Black), new PointF(255, 145));

            dcGs.FillRectangle(new SolidBrush(Color.FromArgb(253, 254, 255)), new Rectangle(80, 200, 60, 50));
            dcGs.DrawString("4", new Font("Arial", 28), new SolidBrush(Color.Black), new PointF(95, 205));

            dcGs.FillRectangle(new SolidBrush(Color.FromArgb(253, 254, 255)), new Rectangle(160, 200, 60, 50));
            dcGs.DrawString("5", new Font("Arial", 28), new SolidBrush(Color.Black), new PointF(175, 205));

            dcGs.FillRectangle(new SolidBrush(Color.FromArgb(253, 254, 255)), new Rectangle(240, 200, 60, 50));
            dcGs.DrawString("6", new Font("Arial", 28), new SolidBrush(Color.Black), new PointF(255, 205));

            dcGs.FillRectangle(new SolidBrush(Color.FromArgb(253, 254, 255)), new Rectangle(80, 260, 60, 50));
            dcGs.DrawString("7", new Font("Arial", 28), new SolidBrush(Color.Black), new PointF(95, 265));

            dcGs.FillRectangle(new SolidBrush(Color.FromArgb(253, 254, 255)), new Rectangle(160, 260, 60, 50));
            dcGs.DrawString("8", new Font("Arial", 28), new SolidBrush(Color.Black), new PointF(175, 265));

            dcGs.FillRectangle(new SolidBrush(Color.FromArgb(253, 254, 255)), new Rectangle(240, 260, 60, 50));
            dcGs.DrawString("9", new Font("Arial", 28), new SolidBrush(Color.Black), new PointF(255, 265));

            dcGs.FillRectangle(new SolidBrush(Color.FromArgb(253, 254, 255)), new Rectangle(80, 320, 60, 50));
            dcGs.DrawString("0", new Font("Arial", 28), new SolidBrush(Color.Black), new PointF(95, 325));

            dcGs.FillRectangle(new SolidBrush(Color.FromArgb(253, 254, 255)), new Rectangle(395, 200, 155, 50));
            dcGs.DrawString("司机", new Font("Arial", 23), new SolidBrush(Color.Black), new PointF(425, 210));

            dcGs.FillRectangle(new SolidBrush(Color.FromArgb(253, 254, 255)), new Rectangle(395, 260, 155, 50));
            dcGs.DrawString("取消", new Font("Arial", 23), new SolidBrush(Color.Black), new PointF(425, 270));

            dcGs.FillRectangle(new SolidBrush(Color.FromArgb(253, 254, 255)), new Rectangle(395, 320, 155, 50));
            dcGs.DrawString("确认", new Font("Arial", 23), new SolidBrush(Color.Black), new PointF(425, 330));

            
        }
    }
}
