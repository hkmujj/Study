using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;
using Motor.HMI.CRH3C380B.Resource.Images;
using Coordinate = Motor.HMI.CRH3C380B.Base.底层共用.Coordinate;

namespace Motor.HMI.CRH3C380B.Base.维修
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMITractiveEffortControllerSensorSetpoint : CRH3C380BBase
    {
        private List<RectangleF> m_RectsList;
        private List<CommonInnerControlBase> m_Collection;

        private readonly NeedChangeLength[] m_ChangingRect = new NeedChangeLength[2];

        private readonly string[] m_NameIndexs =
        {
            OutBoolKeys.Oub牵引手柄检测通道1,
            OutBoolKeys.Oub牵引手柄检测通道2,
        };

        private readonly string[] m_BtnStr =
        {
            "",
            "",
            "通道1\n断开",
            "",
            "通道2\n断开",
            "",
            "",
            "",
            "",
            "主页面"
        };

        //2
        public override string GetInfo()
        {
            return "DMI牵引手柄检测";
        }

        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2 && DMITitle.BtnContentList.Count != 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
                }

                if (GetInBoolValue(InBoolKeys.Inb15车_右侧门已禁用))
                {
                    DMITitle.BtnContentList[2].BtnStr = "通道1\n接通";
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub牵引手柄检测通道1), 1, 0);
                }

                if (GetInBoolValue(InBoolKeys.Inb15车_左侧门已禁用))
                {
                    DMITitle.BtnContentList[4].BtnStr = "通道1\n接通";
                    append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub牵引手柄检测通道2), 1, 0);
                }
            }
        }

        public override void Paint(Graphics g)
        {
            GetValue();

            Draw(g);
        }

        private void Draw(Graphics g)
        {
            PaintGroundImage(g);

            PaintState(g);

            m_Collection.ForEach(f => f.OnPaint(g));
        }

        private void GetValue()
        {
            ResponseBtnEvent();
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
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub牵引手柄检测通道1),
                            OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub牵引手柄检测通道1)] ? 0 : 1, 0);
                        DMITitle.BtnContentList[2].BtnStr = OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub牵引手柄检测通道1)]
                            ? "通道1\n接通"
                            : "通道1\n断开";
                        break;
                    case 10:
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub牵引手柄检测通道2),
                            OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub牵引手柄检测通道2)] ? 0 : 1, 0);
                        DMITitle.BtnContentList[4].BtnStr = OutBoolList[GetOutBoolIndex(OutBoolKeys.Oub牵引手柄检测通道2)]
                            ? "通道2\n接通"
                            : "通道2\n断开";
                        break;
                    case 15:
                        append_postCmd(CmdType.ChangePage, 11, 0, 0); //
                        break;
                }
            }
        }

        private void PaintState(Graphics g)
        {
            g.DrawString("维护; 牵引手柄检测", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));
        }

        private void PaintGroundImage(Graphics g)
        {
            g.DrawImage(MaintainImages.speedTest, m_RectsList[12]);
        }

        private void InitData()
        {
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMITractiveEffortControllerSensorSetpoint, ref m_RectsList))
            {

            }
            m_Collection = new List<CommonInnerControlBase>();
            for (int i = 0; i < 2; i++)
            {
                m_ChangingRect[i] = new NeedChangeLength(m_RectsList[15 + i], 100f, RectRiseDirection.上, false);
                m_Collection.Add(new GDIRectText
                {
                    Text = "",
                    OutLineRectangle = Rectangle.Round(m_RectsList[13 + i]),
                    NeedDarwOutline = true,
                    BackColorVisible = true,
                    BkColor = Color.Yellow,
                    Tag = m_NameIndexs[i],
                    RefreshAction = o =>
                    {
                        var item = (GDIRectText) o;
                        item.BackColorVisible = !GetOutBoolValue((string) item.Tag);
                    }
                });
            }
        }
    }
}