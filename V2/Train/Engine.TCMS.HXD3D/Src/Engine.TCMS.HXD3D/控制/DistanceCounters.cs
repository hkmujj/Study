using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.控制
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class DistanceCounters : HXD3BaseClass
    {
        // Fields
        private readonly Image[] m_BaseImage = new Image[2];

        private readonly SolidBrush[] m_Brushs1 = new SolidBrush[2];
        private readonly SolidBrush[] m_Brushs2 = new SolidBrush[5];
        private readonly List<Region> m_BtnArea = new List<Region>();
        private readonly bool[] m_BtnIsDown = new bool[2];
        private bool[] m_BValue;
        private readonly List<ButtonState> m_DistanceCounterBtns = new List<ButtonState>();
        private float[] m_FValue;
        private bool m_HasPlan;
        private List<Image> m_ImgsList;
        private bool[] m_OutbValue;
        private readonly List<RectangleF> m_RectsBtns = new List<RectangleF>();
        private readonly List<RectangleF> m_RectsImgs = new List<RectangleF>();
        private List<RectangleF> m_RectsList = new List<RectangleF>();
        private int[] m_SendID;

        // Methods
        private void Draw(Graphics e)
        {
            PaintButtonsState(e);
            PaintValue(e);
        }

        public override string GetInfo()
        {
            return "距离计数器";
        }

        private void GetValue()
        {
            int num;
            for (num = 0; num < m_BValue.Length; num++)
            {
                m_BValue[num] = BoolList[UIObj.InBoolList[num]];
            }
            for (num = 0; num < m_FValue.Length; num++)
            {
                m_FValue[num] = FloatList[UIObj.InFloatList[num]];
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
            int num;
            m_BValue = new bool[UIObj.InBoolList.Count];
            m_FValue = new float[UIObj.InFloatList.Count];
            m_OutbValue = new bool[UIObj.OutBoolList.Count];
            m_SendID = new int[UIObj.OutBoolList.Count];
            for (num = 0; num < m_SendID.Length; num++)
            {
                m_SendID[num] = UIObj.OutBoolList[num];
            }
            m_ImgsList = new List<Image>();
            for (num = 0; num < UIObj.ParaList.Count; num++)
            {
                m_ImgsList.Add(Image.FromFile(RecPath + @"\" + UIObj.ParaList[num]));
            }
            m_BaseImage[0] = m_ImgsList[0];
            m_BaseImage[1] = m_ImgsList[1];
            
            {
                
            }
            if (Coordinate.RectangleFLists(ViewIDName.DistanceCounters, ref m_RectsList))
            {
                for (num = 0; num < 2; num++)
                {
                    m_RectsBtns.Add(m_RectsList[4 + 2 * num]);
                }
                for (num = 0; num < 2; num++)
                {
                    m_RectsImgs.Add(m_RectsList[5 + 2 * num]);
                }
                for (num = 0; num < 2; num++)
                {
                    m_BtnArea.Add(new Region(m_RectsBtns[num]));
                }
                InitTheButtons();
                m_HasPlan = true;
            }
            m_Brushs1[0] = SolidBrushsItems.BlackBrush;
            m_Brushs1[1] = SolidBrushsItems.WhiteBrush;
            m_Brushs2[0] = SolidBrushsItems.BlackBrush;
            m_Brushs2[1] = SolidBrushsItems.WhiteBrush;
            m_Brushs2[2] = SolidBrushsItems.BlackBrush;
            m_Brushs2[3] = SolidBrushsItems.Grey1;
            m_Brushs2[4] = SolidBrushsItems.Grey2;
        }

        private void InitTheButtons()
        {
            m_DistanceCounterBtns.Add(new ButtonState(m_RectsBtns[0], m_BaseImage, m_RectsImgs[0], "复位", true));
            m_DistanceCounterBtns.Add(new ButtonState(m_RectsBtns[1], m_BaseImage, m_RectsImgs[1], "设定", true));
        }

        public override bool mouseDown(Point nPoint)
        {
            for (var i = 0; i < 2; i++)
            {
                if (!(!m_BtnArea[i].IsVisible(nPoint) || m_DistanceCounterBtns[i].IsLocked))
                {
                    m_BtnIsDown[i] = true;
                    break;
                }
            }
            return true;
        }

        public override bool mouseUp(Point nPoint)
        {
            for (var i = 0; i < 2; i++)
            {
                if (!(!m_BtnArea[i].IsVisible(nPoint) || m_DistanceCounterBtns[i].IsLocked))
                {
                    m_BtnIsDown[i] = false;
                    break;
                }
            }
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            Draw(dcGs);
        }

        private void PaintButtonsState(Graphics e)
        {
            for (var i = 0; i < 2; i++)
            {
                e.FillRectangle(new SolidBrush(Color.Blue), m_RectsList[5 + 2 * i]);
            }
            m_DistanceCounterBtns[0].DrawTheBtn(e, m_BtnIsDown[0], m_Brushs1);
            m_DistanceCounterBtns[1].DrawTheBtn(e, m_BtnIsDown[1], m_Brushs1);
        }

        private void PaintValue(Graphics e)
        {
            e.DrawString("往返路程", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.WhiteBrush, m_RectsList[0], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString("总路程", FontsItems.Fonts_Regular(FontName.宋体, 14f, true), SolidBrushsItems.WhiteBrush, m_RectsList[1], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString("0.0km", FontsItems.Fonts_Regular(FontName.宋体, 22f, true), SolidBrushsItems.YellowBrush1, m_RectsList[2], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString("0.0km", FontsItems.Fonts_Regular(FontName.宋体, 22f, true), SolidBrushsItems.YellowBrush1, m_RectsList[3], FontsItems.TheAlignment(FontRelated.居中));
            for (var i = 0; i < 2; i++)
            {
                var ef = m_RectsList[2 + i];
                ef = m_RectsList[2 + i];
                ef = m_RectsList[2 + i];
                ef = m_RectsList[2 + i];
                e.DrawRectangle(new Pen(Color.Green, 2f), ef.X, ef.Y, ef.Width, ef.Height);
            }
        }
    }
}