using System;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Login : HMIBase
    {
        private Rectangle m_EditRect;
        private Bitmap m_EditBmp;
        private string m_InputValue;

        private Rectangle m_StrRectagle;

        protected override bool Initalize()
        {
            m_InputValue = string.Empty;
            m_EditRect = new Rectangle(230, 232, 120, 38);
            m_EditBmp = new Bitmap(RecPath + "\\frame/txtEdit.jpg");
            m_StrRectagle = new Rectangle(270, 100, 250, 25);
            return true;
        }

        public override string GetInfo()
        {
            return "Login";
        }

        public override void paint(Graphics g)
        {
            var tempDisplayStr = string.Empty;
            if (KeyBoard.IsEnter)
            {
                ResponseEnter();
            }
            else if (KeyBoard.IsClr)
            {
                tempDisplayStr = string.Empty;
                m_InputValue = string.Empty;
                //FloatList[0] = 0;
                append_postCmd(CmdType.SetInFloatValue, 0, 0, 0);
            }
            else
            {
                m_InputValue = KeyBoard.PressedValue;
            }

            for (var index = 0; index < m_InputValue.Length; ++index)
            {
                tempDisplayStr += "●";
            }
            //g.DrawImage(m_EditBmp, m_EditRect);
            g.FillRectangle(GdiCommon.GrayWhiteBrush, m_EditRect);
            g.DrawString(tempDisplayStr, GdiCommon.Txt12Font, GdiCommon.WhiteBrush, m_EditRect, GdiCommon.LeftFormat);
            g.DrawString(GlobleParam.ResManagerText.GetString("Text0056"), GdiCommon.Txt12Font,
                GdiCommon.MediumGreyBrush, m_StrRectagle, GdiCommon.CenterFormat);
        }

        private void ResponseEnter()
        {
            KeyBoard.PressedValue = string.Empty;
            KeyBoard.IsClr = false;
            KeyBoard.IsEnter = false;

            if (GlobleParam.Instance.CurrentIranViewIndex == IranViewIndex.StartLogin)
            {
                if (m_InputValue == GlobleParam.Instance.DetailConfig.PasswordConfig.DriverPassword)
                {
                    ChangedPage(IranViewIndex.MainMenu);
                }
            }
            else
            {
                if (m_InputValue == GlobleParam.Instance.DetailConfig.PasswordConfig.ReconditionPassword)
                {
                    //ChangedPage((IranViewIndex)47);
                    ChangedPage(IranViewIndex.MainMenu);
                }
            }

            if (m_InputValue != string.Empty)
            {
                //FloatList[0] = Convert.ToUInt32(m_InputValue);
                append_postCmd(CmdType.SetInFloatValue, 0, 0, Convert.ToUInt32(m_InputValue));
            }
        }

        //public override bool mouseDown(Point point)
        //{
        //    if (DataPackage.RunningParam.StartModel == StartModel.Edit)
        //    {
        //        ChangedPage(IranViewIndex.MainMenu);
        //        return true;
        //    }
        //    return false;
        //}
    }
}
