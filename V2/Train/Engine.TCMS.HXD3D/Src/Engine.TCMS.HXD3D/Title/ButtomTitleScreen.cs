using System;
using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3D.HXD3D;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3D.Title
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ButtomTitleScreen : HXD3BaseClass
    {
        // Fields
        private bool m_HasPlan;

        private List<ButtonState> m_TitleButtonList = new List<ButtonState>();
        private readonly Image[] m_BaseImage = new Image[2];
        private readonly List<Region> m_BtnArea = new List<Region>();
        private readonly List<Region> m_BtnAreaTitle = new List<Region>();
        private bool[] m_BValue;
        private Region m_FaultArea;
        private float[] m_FValue;
        private List<Image> m_ImgsList;
        private List<RectangleF> m_RectsList;
        private readonly List<RectangleF> m_RectsTitleBtnList = new List<RectangleF>();
        private readonly List<RectangleF> m_RectsTitleImgList = new List<RectangleF>();
        private readonly string[] m_StrHead1Ch = { "", "主要\n数据", "机车\n设置", "运行\n条件", "维护\n模式", "", "", "", "", "", "故障\n查询" };
        private readonly SolidBrush[] m_TheBtnsBrush = new SolidBrush[5];
        private TopTitleScreen m_TopTitle = new TopTitleScreen();
        private readonly List<ButtonState> m_TitleButtonList1 = new List<ButtonState>();
        public static bool[] TitleBtnIsDown;
        private readonly string m_TrainID = "主界面  机车编号HXD3D0001";

        // Methods
        static ButtomTitleScreen()
        {
            var flagArray = new bool[11];
            flagArray[0] = true;
            TitleBtnIsDown = flagArray;
        }

        public void Draw(Graphics e)
        {
            PaintButtonsState(e);
            PaintValue(e);
        }

        public override string GetInfo()
        {
            return "标题视图";
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
            if (!m_BValue[0])
            {
                append_postCmd(CmdType.ChangePage, 0, 0, 0f);
            }
        }

        protected override  void Initalize()
        {
            InitData();
        }

        private void InitData()
        {
            m_BValue = new bool[UIObj.InBoolList.Count];
            m_FValue = new float[UIObj.InFloatList.Count];
            m_ImgsList = new List<Image>();
            foreach (var str in UIObj.ParaList)
            {
                m_ImgsList.Add(Image.FromFile(RecPath + @"\" + str));
            }

            m_BaseImage[0] = m_ImgsList[1];
            m_BaseImage[1] = m_ImgsList[2];


            if (Coordinate.RectangleFLists(ViewIDName.BottomTitleScreen, ref m_RectsList))
            {
                int num;
                for (num = 0; num < 11; num++)
                {
                    m_BtnArea.Add(new Region(m_RectsList[4 + num]));
                }
                for (num = 0; num < 11; num++)
                {
                    m_BtnAreaTitle.Add(m_BtnArea[num]);
                    m_RectsTitleBtnList.Add(m_RectsList[4 + num]);
                    m_RectsTitleImgList.Add(m_RectsList[0x10 + num]);
                }

                InitTheButtons();
                m_TheBtnsBrush[0] = SolidBrushsItems.YellowBrush1;
                m_TheBtnsBrush[1] = SolidBrushsItems.WhiteBrush;
                m_TheBtnsBrush[2] = new SolidBrush(Color.FromArgb(90, 90, 0));
                m_TheBtnsBrush[3] = new SolidBrush(Color.FromArgb(0x7d, 0x7d, 0));
                m_TheBtnsBrush[4] = new SolidBrush(Color.FromArgb(0xff, 0xff, 0x71));
            }
        }

        private void InitTheButtons()
        {
            m_TitleButtonList1.Add(new ButtonState(m_RectsTitleBtnList[0], new Image[] { m_ImgsList[1], m_ImgsList[0] }, m_RectsTitleImgList[0], m_StrHead1Ch[0], true));
            for (var i = 1; i < 11; i++)
            {
                m_TitleButtonList1.Add(new ButtonState1(m_RectsTitleBtnList[i], m_BaseImage, m_RectsTitleImgList[i], m_StrHead1Ch[i], true));
            }
            m_TitleButtonList = m_TitleButtonList1;
        }

        public override bool mouseDown(Point nPoint)
        {
            for (var i = 0; i < m_BtnAreaTitle.Count; i++)
            {
                if (m_BtnAreaTitle[i].IsVisible(nPoint) && !m_TitleButtonList[i].IsLocked)
                {
                    for (var j = 1; j < 11; j++)
                    {
                        m_TitleButtonList[j].BtnRecover();
                        TitleBtnIsDown[j] = false;
                    }
                    TitleBtnIsDown[i] = true;
                    break;
                }
            }
            return true;
        }

        public override bool mouseUp(Point nPoint)
        {
            var index = 0;
            while (index < m_BtnAreaTitle.Count)
            {
                if (!(!m_BtnAreaTitle[index].IsVisible(nPoint) || m_TitleButtonList[index].IsLocked))
                {
                    TitleBtnIsDown[index] = false;
                    break;
                }
                index++;
            }
            switch (index)
            {
                case 1:
                    append_postCmd(CmdType.ChangePage, 0x33, 0, 0f);
                    break;

                case 2:
                    append_postCmd(CmdType.ChangePage, 60, 0, 0f);
                    break;

                case 4:
                    append_postCmd(CmdType.ChangePage, 1, 0, 0f);
                    break;

                case 10:
                    append_postCmd(CmdType.ChangePage, 1, 0, 0f);
                    TitleBtnIsDown[index] = true;
                    break;
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
            for (var i = 0; i < 11; i++)
            {
                m_TitleButtonList[i].DrawTheBtn(e, TitleBtnIsDown[i], m_TheBtnsBrush);
            }
            var ef = m_RectsList[4];
            e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
        }

        private void PaintValue(Graphics e)
        {
            for (var i = 0; i < 4; i++)
            {
                var ef = m_RectsList[i];
                e.DrawRectangle(new Pen(Color.White), ef.X, ef.Y, ef.Width, ef.Height);
            }
            e.DrawString(TopTitleScreen.TrainID, FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush, m_RectsList[0], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(m_TrainID, FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush, m_RectsList[1], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(DateTime.Now.ToString("yyyy.MM.dd"), FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush, m_RectsList[2], FontsItems.TheAlignment(FontRelated.居中));
            e.DrawString(DateTime.Now.ToString("HH:mm:ss"), FontsItems.DefaultFont, SolidBrushsItems.WhiteBrush, m_RectsList[3], FontsItems.TheAlignment(FontRelated.居中));
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaB == 2)
            {
                if (m_BValue[1])
                {
                    append_postCmd(CmdType.ChangePage, 1, 0, 0f);
                }
                else if (m_BValue[2])
                {
                    append_postCmd(CmdType.ChangePage, 0x15, 0, 0f);
                }
            }
        }
    }
}