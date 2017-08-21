using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource.Images;
using Coordinate = Motor.HMI.CRH3C380B.Base.底层共用.Coordinate;

namespace Motor.HMI.CRH3C380B.Base.维修
{
    [GksDataType(DataType.isMMIObjectClass)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class DMIV_SetpointSensorValue : CRH3C380BBase
    {
        private List<RectangleF> m_RectsList;

        private List<CommonInnerControlBase> m_Collection;

        private float[] m_FValue;

        private readonly NeedChangeLength[] m_ChangingRect = new NeedChangeLength[2];

        private readonly string[] m_BtnStr =
        {
            "",
            "",
            "通道1\n接通",
            "",
            "通道2\n接通",
            "",
            "",
            "",
            "",
            "主页面"
        };

        private readonly bool[] m_BlT = {false, false};

        //2
        public override string GetInfo()
        {
            return "DMI运行速度设定检测";
        }

        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA != 2 || DMITitle.BtnContentList.Count == 0)
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
            }
        }

        public override void Paint(Graphics g)
        {
            GetValue();
            Draw(g);
        }

        private void Draw(Graphics g)
        {
            //GetValue();
            PaintGroundImage(g);
            PaintState(g);
            m_Collection.ForEach(f => f.OnPaint(g));
        }

        private void GetValue()
        {
            ResponseBtnEvent();
            for (int i = 0; i < m_FValue.Length; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }
        }

        private void ResponseBtnEvent()
        {
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 0:
                        append_postCmd(CmdType.ChangePage, 110, 0, 0); //维护
                        break;
                    case 8:
                        m_BlT[0] = !m_BlT[0];
                        DMITitle.BtnContentList[2].BtnStr = m_BlT[0] ? "通道1\n断开" : "通道1\n接通";
                        break;
                    case 10:
                        m_BlT[1] = !m_BlT[1];
                        DMITitle.BtnContentList[4].BtnStr = m_BlT[1] ? "通道2\n断开" : "通道2\n接通";
                        break;
                    case 15:
                        append_postCmd(CmdType.ChangePage, 11, 0, 0); //
                        break;
                }
            }
        }

        private void PaintState(Graphics g)
        {
            g.DrawString("维护; 运行速度设定检测", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));
        }

        private void PaintGroundImage(Graphics g)
        {
            g.DrawImage(MaintainImages.speedTest, m_RectsList[12]);
        }

        private void InitData()
        {
            m_FValue = new float[UIObj.InFloatList.Count];

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DmivSetpointSensorValue, ref m_RectsList))
            {

            }
            m_Collection = new List<CommonInnerControlBase>();
            for (int i = 0; i < 2; i++)
            {
                m_ChangingRect[i] = new NeedChangeLength(m_RectsList[15 + i], 100f, RectRiseDirection.上, false);
                m_Collection.Add(new GDIRectText //TODO 通道 断开 连通
                {
                    NeedDarwOutline = true,
                    BkColor = Color.Yellow,
                    BackColorVisible = true,
                    Tag = i,
                    OutLineRectangle = Rectangle.Round(m_RectsList[13 + i]),
                    RefreshAction = o =>
                    {
                        var item = (GDIRectText) o;
                        var name = (int) item.Tag;
                        item.BackColorVisible = m_BlT[name];
                    }
                });
            }
        }
    }
}