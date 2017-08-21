using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Controls;

namespace Urban.Iran.HMI.Operation
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class OperationLegend : HMIBase
    {
        private List<CommonInnerControlBase> m_Collection;
        private List<Rectangle> m_Rec;
        private readonly string[] m_Str = { "TRB", "Wash", "ATO", "ATP", "Manual", "Towing","Towed", "No Skip/Slide", "No Em. brake", "No Traction block", "Ready to drive", "Skip/Slide", "Emergency brake", "Traction block", "Not Ready to drive", "Heavy Skip/Slide" };
        protected override bool Initalize()
        {
          
            InitRectangle();
            InitGDITwoLineRctList();
            return true;
        }

        private void InitGDITwoLineRctList()
        {
            var yellow = new[] {0, 1, 4, 5, 6, 11};
            var red = new[] {12, 13, 14, 15};
            m_Collection = new List<CommonInnerControlBase>();
            for (int i = 0; i < m_Rec.Count; i++)
            {
                m_Collection.Add(new GDITwoOutLineRec(2)
                {
                    Text = m_Str[i],
                    TextBrush = yellow.Contains(i) || red.Contains(i) ? GdiCommon.BlackBrush : GdiCommon.MediumGreyBrush,
                    BackColorVisible = yellow.Contains(i) || red.Contains(i),
                    BkColor = yellow.Contains(i) ? Color.Yellow : red.Contains(i) ? Color.Red : Color.White,
                    NeedDarwOutline = true,
                    Visible = true,
                    OutLineRectangle = m_Rec[i],
                    FillOutRectangle = true,
                    TextFormat = GdiCommon.CenterFormat,
                    OutLinePen = GdiCommon.MediumGreyPen
                });
            }
        }

        private void InitRectangle()
        {
            const int inval = 150;
            const int defaultY = 100;
            const int offset = 85;
            m_Rec = new List<Rectangle>();
            var point = new Point(40, 100);
            var size = new Size(95, 59);
            for (int i = 0; i < 4; i++)
            {
                m_Rec.Add(new Rectangle(point, size));
                point.Offset(0, offset);
            }
            point.Y = defaultY;
            point.Offset(inval, 0);
            for (int i = 0; i < 3; i++)
            {
                m_Rec.Add(new Rectangle(point, size));
                point.Offset(0, offset);
            }
            point.Y = defaultY;
            point.Offset(inval + 40, 0);
            for (int i = 0; i < 4; i++)
            {
                if (i == 3)
                {
                    m_Rec.Add(new Rectangle(point, size));
                }
                point.Offset(0, offset);
            }
            point.Y = defaultY;
            point.Offset(inval, 0);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    m_Rec.Add(new Rectangle(point, size));
                    point.Offset(0, offset);
                }
                point.Y = defaultY;
                point.Offset(inval, 0);
            }
        }

        public override void paint(Graphics dcGs)
        {
            m_Collection.ForEach(e=>e.OnDraw(dcGs));
        }
    }
}