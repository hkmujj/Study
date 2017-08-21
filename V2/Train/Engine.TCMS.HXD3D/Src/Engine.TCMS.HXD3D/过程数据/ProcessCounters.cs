using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.过程数据
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class ProcessCounters : HXD3BaseClass
    {
        // Fields
        private readonly Image[] m_BaseImage = new Image[2];

        private readonly bool[] m_BtnIsDown = new bool[1];

        private readonly string[] m_BtnNames = {
        "主断路器", "受电弓1", "CI1K", "CI4K", "CI1AK", "CI4AK", "KM11", "KM13", "空压机1", "受电弓2", "CI2K", "CI5K", "CI2AK", "CI5AK", "KM12", "KM14",
        "空压机2", "CI3K", "CI6K", "CI3AK", "CI6AK", "KM20", "", ""
     };

        private readonly List<Region> m_BtnRegion = new List<Region>();
        private bool[] m_BValue;
        private bool m_HasPlan;
        private List<Image> m_ImgsList;
        private List<RectangleF> m_RectsList;

        // Methods
        private void Draw(Graphics e)
        {
            PaintButtonsState(e);
            PaintValue(e);
        }

        public override string GetInfo()
        {
            return "计数";
        }

        private void GetValue()
        {
            for (var i = 0; i < m_BValue.Length; i++)
            {
                m_BValue[i] = BoolList[UIObj.InBoolList[i]];
            }
        }

        protected override  void Initalize()
        {
            Instance = this;
            InitData();
        }

        private void InitData()
        {
            m_BValue = new bool[UIObj.InBoolList.Count];
            m_ImgsList = new List<Image>();
            for (var i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_ImgsList.Add(Image.FromFile(RecPath + @"\" + UIObj.ParaList[i]));
            }
            m_BaseImage[0] = m_ImgsList[1];
            m_BaseImage[1] = m_ImgsList[0];
            
            {
                
            }
            if (Coordinate.RectangleFLists(ViewIDName.ProcessCounters, ref m_RectsList))
            {
                m_BtnRegion.Add(new Region(m_RectsList[0x30]));
                m_HasPlan = true;
            }
        }

        public override bool mouseDown(Point nPoint)
        {
            if (m_BtnRegion[0].IsVisible(nPoint))
            {
                m_BtnIsDown[0] = true;
                return true;
            }
            return false;
        }

        public override bool mouseUp(Point nPoint)
        {
            if (m_BtnRegion[0].IsVisible(nPoint))
            {
                m_BtnIsDown[0] = false;
                return true;
            }
            return false;
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            Draw(dcGs);
        }

        private void PaintButtonsState(Graphics e)
        {
            e.DrawImage(m_BtnIsDown[0] ? m_ImgsList[0] : m_ImgsList[1], m_RectsList[0x30]);
            e.FillRectangle(new SolidBrush(Color.Blue), m_RectsList[0x31]);
            e.DrawString("设定", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[0x30], FontsItems.TheAlignment(FontRelated.居中));
        }

        private void PaintValue(Graphics e)
        {
            int num;
            for (num = 0; num < 0x18; num++)
            {
                e.DrawString(m_BtnNames[num], FontsItems.Fonts_Regular(FontName.宋体, 12f, false), SolidBrushsItems.WhiteBrush, m_RectsList[0x18 + num], FontsItems.TheAlignment(FontRelated.靠左));
            }
            for (num = 0; num < 20; num++)
            {
                e.DrawString("0次", FontsItems.Fonts_Regular(FontName.宋体, 12f, false), SolidBrushsItems.WhiteBrush, m_RectsList[2 + num], FontsItems.TheAlignment(FontRelated.居中));
            }
            e.DrawString(CbCount.ToString("0") + "次", FontsItems.Fonts_Regular(FontName.宋体, 12f, false), SolidBrushsItems.WhiteBrush, m_RectsList[0], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(ShouD1Count.ToString("0") + "次", FontsItems.Fonts_Regular(FontName.宋体, 12f, false), SolidBrushsItems.WhiteBrush, m_RectsList[1], FontsItems.TheAlignment(FontRelated.居中));
        }

        // Properties
        public int CbCount { get; set; }

        public static ProcessCounters Instance
        {
            get; private set;
        }

        public int ShouD1Count { get; set; }
    }
}