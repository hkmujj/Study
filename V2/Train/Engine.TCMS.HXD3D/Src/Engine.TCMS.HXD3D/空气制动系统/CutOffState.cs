using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.空气制动系统
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class CutOffState : HXD3BaseClass
    {
        // Fields
        private bool[] m_BValue;

        private bool m_HasPlan;
        private bool[] m_OutbValue;
        private List<RectangleF> m_RectsList = new List<RectangleF>();
        private int[] m_SendID;
        private readonly string[] m_StrAdc1 = { "制动缸1", "停车制动", "轮缘润滑1", "备用阀1", "制动缸2", "紧急制动", "轮缘润滑2", "备用阀2", "撒沙装置", "辅助风缸", "总风", "受电弓" };

        // Methods
        public void Draw(Graphics e)
        {
            PaintValue(e);
        }

        public override string GetInfo()
        {
            return "隔离阀状态";
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
            if (Coordinate.RectangleFLists(ViewIDName.CutOffState, ref m_RectsList))
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
            for (num = 0; num < 12; num++)
            {
                var ef = m_RectsList[num];
                ef = m_RectsList[num];
                ef = m_RectsList[num];
                ef = m_RectsList[num];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
            }
            for (num = 0; num < 12; num++)
            {
                e.FillRectangle(m_BValue[num] ? SolidBrushsItems.RedBrush1 : SolidBrushsItems.GreenBrush1, m_RectsList[12 + num]);
                if (m_BValue[num])
                {
                    e.DrawString(m_StrAdc1[num], FontsItems.Fonts_Regular(FontName.宋体, 16f, false), SolidBrushsItems.WhiteBrush, m_RectsList[num], FontsItems.TheAlignment(FontRelated.居中));
                }
                else
                {
                    e.DrawString(m_StrAdc1[num], FontsItems.Fonts_Regular(FontName.宋体, 16f, false), SolidBrushsItems.BlackBrush, m_RectsList[num], FontsItems.TheAlignment(FontRelated.居中));
                }
            }
        }
    }
}