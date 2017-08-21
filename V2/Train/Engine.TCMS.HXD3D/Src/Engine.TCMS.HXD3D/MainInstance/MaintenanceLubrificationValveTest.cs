using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3D.MainInstance
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class MaintenanceLubrificationValveTest : HXD3BaseClass
    {
        // Fields
        private readonly Image[] m_BaseImage = new Image[2];

        private readonly SolidBrush[] m_Brushs1 = new SolidBrush[2];
        private readonly SolidBrush[] m_Brushs2 = new SolidBrush[2];
        private readonly List<Region> m_BtnArea = new List<Region>();
        private readonly bool[] m_BtnIsDown = new bool[2];
        private bool[] m_BValue;
        private bool m_HasPlan;
        private List<Image> m_ImgsList;
        private readonly List<ButtonState> m_LubrificationBtns = new List<ButtonState>();
        private bool[] m_OutbValue;
        private readonly List<RectangleF> m_RectsBtns = new List<RectangleF>();
        private readonly List<RectangleF> m_RectsImgs = new List<RectangleF>();
        private List<RectangleF> m_RectsList = new List<RectangleF>();
        private int[] m_SendID;
        private readonly string[] m_StrArr = { "YV43", "YV44" };

        // Methods
        public void Draw(Graphics e)
        {
            PaintGroundImage(e);
            PaintValue(e);
            PaintButtonsState(e);
        }

        public override string GetInfo()
        {
            return "轮缘润滑试验";
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
            int num;
            m_BValue = new bool[UIObj.InBoolList.Count];
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
            m_BaseImage[0] = m_ImgsList[2];
            m_BaseImage[1] = m_ImgsList[1];
            if (Coordinate.RectangleFLists(ViewIDName.MaintenanceLubrificationValveTest, ref m_RectsList))
            {
                for (num = 0; num < 2; num++)
                {
                    m_RectsBtns.Add(m_RectsList[num + 4]);
                }
                for (num = 0; num < 2; num++)
                {
                    m_RectsImgs.Add(m_RectsList[num + 6]);
                }
                for (num = 0; num < m_RectsBtns.Count; num++)
                {
                    m_BtnArea.Add(new Region(m_RectsBtns[num]));
                }
                InitTheButtons();
                m_HasPlan = true;
            }
            m_Brushs1[0] = SolidBrushsItems.BlackBrush;
            m_Brushs1[1] = SolidBrushsItems.WhiteBrush;
            m_Brushs2[0] = SolidBrushsItems.YellowBrush1;
            m_Brushs2[1] = SolidBrushsItems.BlackBrush;
        }

        private void InitTheButtons()
        {
            for (var i = 0; i < 2; i++)
            {
                m_LubrificationBtns.Add(new ButtonState(m_RectsBtns[i], m_BaseImage, m_RectsImgs[i], m_StrArr[i], true));
            }
        }

        public override bool mouseDown(Point nPoint)
        {
            var index = 0;
            while (index < 2)
            {
                if (m_BtnArea[index].IsVisible(nPoint))
                {
                    break;
                }
                index++;
            }
            switch (index)
            {
                case 0:
                case 1:
                    if (!m_LubrificationBtns[index].IsLocked)
                    {
                        m_LubrificationBtns[index].BtnStateChange(true);
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index], 1, 0f);
                        m_BtnIsDown[index] = true;
                        break;
                    }
                    m_LubrificationBtns[index].BtnRecover();
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index], 0, 0f);
                    m_BtnIsDown[index] = false;
                    break;
            }
            return true;
        }

        public override bool mouseUp(Point nPoint)
        {
            for (var i = 0; i < 2; i++)
            {
                if (m_BtnArea[i].IsVisible(nPoint))
                {
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
                m_LubrificationBtns[i].DrawTheBtn(e, m_BtnIsDown[i], m_BtnIsDown[i] ? m_Brushs2 : m_Brushs1);
            }
        }

        private void PaintGroundImage(Graphics e)
        {
            e.DrawImage(m_ImgsList[0], m_RectsList[0]);
        }

        private void PaintValue(Graphics e)
        {
            int num;
            for (num = 0; num < 2; num++)
            {
                if (m_BValue[num])
                {
                    e.DrawImage(m_ImgsList[3], m_RectsList[1 + num]);
                }
            }
            var ef = m_RectsList[3];
            ef = m_RectsList[3];
            ef = m_RectsList[3];
            ef = m_RectsList[3];
            e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
            for (num = 0; num < 2; num++)
            {
                e.DrawString("试验正在进行", FontsItems.Fonts_Regular(FontName.宋体, 14f, false), SolidBrushsItems.WhiteBrush, m_RectsList[3], FontsItems.TheAlignment(FontRelated.居中));
                if (!m_BtnIsDown[num])
                {
                    e.FillRectangle(new SolidBrush(Color.Blue), m_RectsList[6 + num]);
                }
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                for (var i = 0; i < 2; i++)
                {
                    if (!BoolList[UIObj.OutBoolList[i]])
                    {
                        m_LubrificationBtns[i].BtnRecover();
                    }
                }
            }
        }
    }
}