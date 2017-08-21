using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ARTest : HMIBase
    {
        private FjButtonEx m_InputBtn;

        private ArTestView m_ArTest;

        protected List<CommonInnerControlBase> m_LineControlCollection;

        private void InputBtnMouseDown(FjButtonEx btnSender, Point pt)
        {
            ChangedPage((IranViewIndex) btnSender.BtnIndex);
        }

        public override string GetInfo()
        {
            return "ARTest";
        }

        protected override bool Initalize()
        {
            m_InputBtn = new FjButtonEx(30, GlobleParam.ResManager.GetString("String153"), GlobleRect.m_HlepRect);
            m_InputBtn.MouseDown += InputBtnMouseDown;

            m_LineControlCollection = new List<CommonInnerControlBase>();

            m_ArTest = new ArTestView(m_LineControlCollection, this);

            return true;
        }

        public override void paint(Graphics g)
        {
            m_InputBtn.OnDraw(g);

            m_LineControlCollection.ForEach(e => e.OnPaint(g));

        }

        public override bool mouseDown(Point point)
        {
            if (m_InputBtn.IsVisible(point))
            {
                m_InputBtn.OnMouseDown(point);
            }
            return true;
        }
    }
}