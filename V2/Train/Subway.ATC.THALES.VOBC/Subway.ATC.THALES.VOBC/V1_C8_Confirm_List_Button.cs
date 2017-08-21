using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Subway.ATC.THALES.VOBC
{
    public class Button
    {
        private Rectangle _rect;
        public Image _imageDown;
        public Image _imageUp;

        private bool _isSelected;

        public event Action<int> MouseUpEvent;

        private int _message;
        private int _currentView = 0;

        public Button(Rectangle rect, Image imageDown, Image imageUp, int message = 0)
        {
            _rect = rect;
            _imageDown = imageDown;
            _imageUp = imageUp;
            _message = message;
        }

        public void Paint(Graphics g)
        {
            if (_isSelected)
            {
                if (_imageDown != null)
                {
                    g.DrawImage(_imageDown, _rect);
                }
            }
            else
            {
                if (_imageUp != null)
                {
                    g.DrawImage(_imageUp, _rect);
                }
            }
        }

        public void MouseDown(Point p)
        {
            if (_rect.Contains(p))
            {
                _isSelected = true;
            }
        }

        public void MouseUp(Point p)
        {
            if (_rect.Contains(p))
            {
                _isSelected = false;


                if (MouseUpEvent != null)
                {
                    MouseUpEvent(_message);
                }

            }
        }
    }

    [GksDataType(DataType.isMMIObjectClass)]
    public class V1_C8_Confirm_List_Button : baseClass
    {
        private Button _btn_Confirm_Info;
        private Button _btn_Confirm_Fault;
        private Button _btn_List_Info;
        private Button _btn_List_Fault;
        private bool _isInfoShow = true;
        private bool _isFaultShow = true;
        private List<string> _infoCollection = new List<string>();
        private List<string> _faultCollection = new List<string>();
        private int _currentView = 0;

        public override string GetInfo()
        {
            return "确认与列表按钮";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            _btn_Confirm_Info = new Button(new Rectangle(15, 450, 95, 35), null, null);
            _btn_Confirm_Info.MouseUpEvent += _btn_ShowBox_MouseUpEvent;

            _btn_Confirm_Fault = new Button(new Rectangle(15, 500, 95, 35), null, null);
            _btn_Confirm_Fault.MouseUpEvent += _btn_Confirm_Fault_MouseUpEvent;

            _btn_List_Info = new Button(new Rectangle(565, 450, 95, 35), null, null);
            _btn_List_Info.MouseUpEvent += _btn_List_Info_MouseUpEvent;

            _btn_List_Fault = new Button(new Rectangle(565, 500, 95, 35), null, null);
            _btn_List_Fault.MouseUpEvent += _btn_List_Info_Fault_MouseUpEvent;

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                _currentView = nParaB;
            }
        }

        void _btn_List_Info_MouseUpEvent(int obj)
        {
            if(_currentView==104)
            {
                append_postCmd(CmdType.ChangePage, 107, 0, 0);
            }
        }

        void _btn_Confirm_Fault_MouseUpEvent(int obj)
        {
            if (VC_C0_GetValue.CurrentFalut != null)
            {
                VC_C0_GetValue.CurrentFalut.IsOK = true;
                VC_C0_GetValue.CurrentFaluts.Remove(VC_C0_GetValue.CurrentFalut);
            }
        }

        void _btn_List_Info_Fault_MouseUpEvent(int obj)
        {
            if (_currentView == 104)
            {
                append_postCmd(CmdType.ChangePage, 108, 0, 0);
            }
        }

        public override bool mouseUp(Point nPoint)
        {
            _btn_Confirm_Info.MouseUp(nPoint);
            _btn_Confirm_Fault.MouseUp(nPoint);
            _btn_List_Info.MouseUp(nPoint);
            _btn_List_Fault.MouseUp(nPoint);

            return true;
        }

        void _btn_ShowBox_MouseUpEvent(int obj)
        {
            if (VC_C0_GetValue.CurrentInfos != null)
            {
                VC_C0_GetValue.CurrentInfo.IsOK = true;
                VC_C0_GetValue.CurrentInfos.Remove(VC_C0_GetValue.CurrentInfo);
            }
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.DrawString("确认", new Font("Arial", 13, FontStyle.Bold), new SolidBrush(Color.Black), new PointF(32, 458));
            dcGs.DrawString("确认", new Font("Arial", 13, FontStyle.Bold), new SolidBrush(Color.Black), new PointF(32, 505));

            dcGs.DrawString("列表", new Font("Arial", 13, FontStyle.Bold), new SolidBrush(Color.Black), new PointF(581, 458));
            dcGs.DrawString("列表", new Font("Arial", 13, FontStyle.Bold), new SolidBrush(Color.Black), new PointF(581, 505));

            dcGs.DrawString("English", new Font("Arial", 13, FontStyle.Bold), new SolidBrush(Color.Black), new PointF(693, 458));


            string strFalut = VC_C0_GetValue.CurrentFalut == null ? "" : VC_C0_GetValue.CurrentFalut.Description;

            string strMessage = VC_C0_GetValue.CurrentInfo == null ? "" : VC_C0_GetValue.CurrentInfo.Description;

            if (_isInfoShow)
            {
                dcGs.DrawString(strMessage, new Font("Arial", 15, FontStyle.Bold), new SolidBrush(Color.White), new Rectangle(109,450,456,40),new StringFormat(){ Alignment= StringAlignment.Center, LineAlignment= StringAlignment.Center});
            }
            if (_isFaultShow)
            {
                dcGs.DrawString(strFalut, new Font("Arial", 15, FontStyle.Bold), new SolidBrush(Color.Red), new Rectangle(109, 496, 456, 40), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }

            
        }
    }
}
