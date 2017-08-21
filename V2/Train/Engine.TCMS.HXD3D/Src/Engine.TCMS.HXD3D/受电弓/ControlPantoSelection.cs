using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.Title;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3D.受电弓
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class ControlPantoSelection : HXD3BaseClass
    {
        // Fields
        private readonly Image[] m_BaseImage = new Image[2];

        private readonly SolidBrush[] m_Brush1 = new SolidBrush[0];
        private readonly List<Region> m_BtnAreaShouD = new List<Region>();
        private readonly bool[] m_BtnIsDown = new bool[6];
        private bool[] m_BValue;
        private bool m_HasPlan;
        private List<Image> m_ImgsList;
        private bool[] m_OutbValue;
        private readonly List<RectangleF> m_RectsBtns = new List<RectangleF>();
        private readonly List<RectangleF> m_RectsImgs = new List<RectangleF>();
        private List<RectangleF> m_RectsList = new List<RectangleF>();
        private int[] m_SendID;
        private readonly List<ButtonState> m_ShouDSelectBtns = new List<ButtonState>();
        private int m_TheLastBtnId = -1;

        // Methods
        private void ChangeVIew(int index)
        {
            m_BtnIsDown[index] = false;
            if (index != -1)
            {
                m_TheLastBtnId = index;
                m_ShouDSelectBtns[index].BtnStateChange(true);
                for (var i = 0; i < 4; i++)
                {
                    append_postCmd(CmdType.SetBoolValue, m_SendID[i], 0, 0f);
                }
                if (index != 0)
                {
                    append_postCmd(CmdType.SetBoolValue, m_SendID[index], 1, 0f);
                }
            }
        }

        public void Draw(Graphics e)
        {
            PaintGroundImage(e);
            PaintValue(e);
            PaintButtonsState(e);
        }

        private List<Image> GetImgList(int id, int index)
        {
            var list = new List<Image>();
            for (var i = 0; i < index; i++)
            {
                list.Add(m_ImgsList[id + i]);
            }
            return list;
        }

        public override string GetInfo()
        {
            return "受电弓预选择";
        }

        private void GetshouD(Point nPoint, int index)
        {
            while (index < 4)
            {
                if (m_BtnAreaShouD[index].IsVisible(nPoint) && !m_ShouDSelectBtns[index].IsLocked)
                {
                    for (var i = 0; i < 4; i++)
                    {
                        m_ShouDSelectBtns[i].BtnRecover();
                    }
                    m_BtnIsDown[index] = true;
                    break;
                }
                index++;
            }
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

        private void GetValue(Point nPoint)
        {
            var index = 0;
            GetshouD(nPoint, index);
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
            m_BaseImage[0] = m_ImgsList[10];
            m_BaseImage[1] = m_ImgsList[9];
            
            {
                
            }
            if (Coordinate.RectangleFLists(ViewIDName.ControlPantoSelection, ref m_RectsList))
            {
                for (num = 0; num < 4; num++)
                {
                    m_RectsBtns.Add(m_RectsList[num]);
                }
                for (num = 0; num < 4; num++)
                {
                    m_RectsImgs.Add(m_RectsList[4 + num]);
                }
                for (num = 0; num < m_RectsBtns.Count; num++)
                {
                    m_BtnAreaShouD.Add(new Region(m_RectsBtns[num]));
                }
                InitTheButtons();
                m_HasPlan = true;
            }
        }

        private void InitTheButtons()
        {
            for (var i = 0; i < 4; i++)
            {
                m_ShouDSelectBtns.Add(new ButtonState(m_RectsBtns[i], m_BaseImage, m_RectsImgs[i], GetImgList(11 + i * 2, 2), true));
            }
            m_ShouDSelectBtns[0].BtnStateChange(true);
        }

        public override bool mouseDown(Point nPoint)
        {
            if (!m_BValue[10])
            {
                GetValue(nPoint);
            }
            return true;
        }

        public override bool mouseUp(Point nPoint)
        {
            if (!m_BValue[10])
            {
                for (var i = 0; i < 4; i++)
                {
                    if (!(!m_BtnAreaShouD[i].IsVisible(nPoint) || m_ShouDSelectBtns[i].IsLocked))
                    {
                        ChangeVIew(i);
                        break;
                    }
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
            for (var i = 0; i < 4; i++)
            {
                m_ShouDSelectBtns[i].DrawTheBtn(e, m_BtnIsDown[i], m_Brush1);
            }
        }

        private void PaintGroundImage(Graphics e)
        {
            e.DrawImage(m_ImgsList[0], m_RectsList[0x10]);
        }

        private void PaintValue(Graphics e)
        {
            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (m_BValue[j + i * 4])
                    {
                        e.DrawImage(m_ImgsList[1 + j + i * 4], m_RectsList[i + 12]);
                    }
                }
                if (m_BValue[8 + i])
                {
                    e.DrawImage(m_ImgsList[0x16], m_RectsList[i + 14]);
                }
            }
            e.DrawString(TopTitleScreen.TrainID1, FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush, m_RectsList[0x12], FontsItems.TheAlignment(FontRelated.居中));
        }
    }
}