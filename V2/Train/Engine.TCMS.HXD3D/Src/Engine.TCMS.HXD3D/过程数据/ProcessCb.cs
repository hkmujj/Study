using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.过程数据
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class ProcessCb : HXD3BaseClass
    {
        // Fields
        private bool[] m_BValue;

        private bool m_HasPlan;
        private bool[] m_OutbValue;
        private List<RectangleF> m_RectsList = new List<RectangleF>();
        private int[] m_SendID;
        private readonly string[] m_StrAdc1 = { "QA11", "QA13", "QA19", "QA21", "QA23", "QA25", "QA12", "QA14", "QA20", "QA22", "QA24" };
        private readonly string[] m_StrAdc2 = { "QF1", "QS1", "QS11", "KP62", "KP63", "SA75", "KC1", "QS2", "QS12", "KP64" };

        // Methods
        public void Draw(Graphics e)
        {
            PaintValue(e);
        }

        public override string GetInfo()
        {
            return "开关状态";
        }

        private void GetValue()
        {
            int num;
            for (num = 0; num < m_BValue.Length; num++)
            {
                m_BValue[num] = BoolList[UIObj.InBoolList[num]];
            }
            for (num = 0; num < m_OutbValue.Length; num++)
            {
                m_OutbValue[num] = BoolList[UIObj.OutBoolList[num]];
            }
        }

        protected override  void Initalize()
        {
            InitData();
        }

        private void InitData()
        {
            m_BValue = new bool[UIObj.InBoolList.Count];
            m_OutbValue = new bool[UIObj.OutBoolList.Count];
            m_SendID = new int[UIObj.OutBoolList.Count];
            for (var i = 0; i < m_SendID.Length; i++)
            {
                m_SendID[i] = UIObj.OutBoolList[i];
            }
            
            {
                
            }
            if (Coordinate.RectangleFLists(ViewIDName.ProcessCb, ref m_RectsList))
            {
                m_HasPlan = true;
            }
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            Draw(dcGs);
        }

        private void PaintValue(Graphics e)
        {
            int num;
            RectangleF ef;
            for (num = 0; num < 13; num++)
            {
                ef = m_RectsList[num];
                ef = m_RectsList[num];
                ef = m_RectsList[num];
                ef = m_RectsList[num];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
            }
            for (num = 0; num < 10; num++)
            {
                ef = m_RectsList[14 + num];
                ef = m_RectsList[14 + num];
                ef = m_RectsList[14 + num];
                ef = m_RectsList[14 + num];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
            }
            for (num = 0; num < 11; num++)
            {
                e.FillRectangle(m_BValue[num] ? SolidBrushsItems.GreenBrush1 : SolidBrushsItems.BlackBrush, m_RectsList[0x18 + num]);
                if (m_BValue[num])
                {
                    e.DrawString(m_StrAdc1[num], FontsItems.DefaultFont, SolidBrushsItems.BlackBrush, m_RectsList[2 + num], FontsItems.TheAlignment(FontRelated.居中));
                }
                else
                {
                    e.DrawString(m_StrAdc1[num], FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush, m_RectsList[2 + num], FontsItems.TheAlignment(FontRelated.居中));
                }
            }
            for (num = 0; num < 10; num++)
            {
                e.FillRectangle(m_BValue[11 + num] ? SolidBrushsItems.GreenBrush1 : SolidBrushsItems.BlackBrush, m_RectsList[0x24 + num]);
                if (m_BValue[11 + num])
                {
                    e.DrawString(m_StrAdc2[num], FontsItems.DefaultFont, SolidBrushsItems.BlackBrush, m_RectsList[14 + num], FontsItems.TheAlignment(FontRelated.居中));
                }
                else
                {
                    e.DrawString(m_StrAdc2[num], FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush, m_RectsList[14 + num], FontsItems.TheAlignment(FontRelated.居中));
                }
            }
        }
    }
}