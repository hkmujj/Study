using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3D.MainInstance
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class AlertToTest : HXD3BaseClass
    {
        // Fields
        private readonly List<Rectangle> m_RecctLists = new List<Rectangle>();

        public static SolidBrush[] AltertBrush = new SolidBrush[3];
        private readonly Timer m_AlTimer = new Timer(1000.0);
        private readonly Image[] m_BaseImage = new Image[2];
        private readonly bool[] m_BtnIsDown = new bool[1];
        private readonly List<Region> m_BtnRegion = new List<Region>();
        private bool[] m_BValue;
        private float[] m_FValue;
        private bool m_HasPlan;
        private List<Image> m_ImgsList;
        private bool m_IsNeedFalsh;
        private List<RectangleF> m_RectsList;
        private static int m_TimeId;

        // Methods
        private void AltertTest(Graphics e)
        {
            if (ColoreId() >= 0)
            {
                e.FillRectangle(m_IsNeedFalsh ? AltertBrush[ColoreId()] : CommonRes.BlackBrush, m_RecctLists[0]);
                m_IsNeedFalsh = false;
            }
        }

        private int ColoreId()
        {
            if ((m_TimeId > 0) && (m_TimeId <= 10))
            {
                return 0;
            }
            if ((m_TimeId > 10) && (m_TimeId <= 20))
            {
                return 1;
            }
            if ((m_TimeId > 20) && (m_TimeId <= 30))
            {
                return 2;
            }
            if (m_TimeId > 30)
            {
                m_AlTimer.Close();
                m_BtnIsDown[0] = false;
                m_TimeId = 0;
                append_postCmd(CmdType.SetBoolValue, 0x9b1, 0, 1f);
            }
            return -1;
        }

        private void Draw(Graphics e)
        {
            AltertTest(e);
            PaintButtonsState(e);
            PaintValue(e);
        }

        public override string GetInfo()
        {
            return "无人警惕试验";
        }

        private void GetValue()
        {
            int num;
            for (num = 0; num < m_FValue.Length; num++)
            {
                m_FValue[num] = FloatList[UIObj.InFloatList[num]];
            }
            for (num = 0; num < m_BValue.Length; num++)
            {
                m_BValue[num] = BoolList[UIObj.InBoolList[num]];
            }
        }

        protected override  void Initalize()
        {
            InitData();
        }

        private void InitData()
        {
            m_FValue = new float[UIObj.InFloatList.Count];
            m_BValue = new bool[UIObj.InBoolList.Count];
            m_ImgsList = new List<Image>();
            for (var i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_ImgsList.Add(Image.FromFile(RecPath + @"\" + UIObj.ParaList[i]));
            }
            m_BaseImage[0] = m_ImgsList[1];
            m_BaseImage[1] = m_ImgsList[0];
            if (Coordinate.RectangleFLists(ViewIDName.LampTest, ref m_RectsList))
            {
                m_BtnRegion.Add(new Region(m_RectsList[1]));
                m_HasPlan = true;
            }
            m_RecctLists.Add(new Rectangle(0x14c, 0x17e, 0x24, 0x24));
            AltertBrush[0] = CommonRes.GreenBrush;
            AltertBrush[1] = CommonRes.YellowBrush;
            AltertBrush[2] = CommonRes.RedBrush;
        }

        public override bool mouseDown(Point nPoint)
        {
            if (m_BtnRegion[0].IsVisible(nPoint))
            {
                m_BtnIsDown[0] = true;
                append_postCmd(CmdType.SetBoolValue, 0x9b1, 1, 1f);
                Time();
                return true;
            }
            return false;
        }

        public override bool mouseUp(Point nPoint)
        {
            return m_BtnRegion[0].IsVisible(nPoint);
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            m_TimeId++;
            m_IsNeedFalsh = true;
        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            Draw(dcGs);
        }

        private void PaintButtonsState(Graphics e)
        {
            if (!m_BtnIsDown[0])
            {
                e.FillRectangle(new SolidBrush(Color.Blue), m_RectsList[2]);
                e.DrawString("确认", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[1], FontsItems.TheAlignment(FontRelated.居中));
                e.DrawImage(m_BtnIsDown[0] ? m_ImgsList[0] : m_ImgsList[1], m_RectsList[1]);
            }
        }

        private void PaintValue(Graphics e)
        {
            var ef = m_RectsList[0];
            ef = m_RectsList[0];
            ef = m_RectsList[0];
            ef = m_RectsList[0];
            e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
            var rectangle = m_RecctLists[0];
            rectangle = m_RecctLists[0];
            rectangle = m_RecctLists[0];
            rectangle = m_RecctLists[0];
            e.DrawRectangle(CommonRes.WhitePen, rectangle.X - 1, rectangle.Y - 1, rectangle.Width + 1, rectangle.Height + 1);
            e.DrawString("无人\n警惕", CommonRes.Font10, CommonRes.WhiteBrush, m_RecctLists[0], CommonRes.MFormat);
            if (m_BtnIsDown[0])
            {
                e.DrawString("试验正在进行", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[0], FontsItems.TheAlignment(FontRelated.居中));
            }
            else
            {
                e.DrawString("试验是否开始", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[0], FontsItems.TheAlignment(FontRelated.居中));
            }
        }

        private void Time()
        {
            m_AlTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            m_AlTimer.Interval = 1000.0;
            m_AlTimer.Enabled = true;
            m_AlTimer.Start();
        }
    }
}