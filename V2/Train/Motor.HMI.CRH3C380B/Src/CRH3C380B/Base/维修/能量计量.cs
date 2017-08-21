using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.维修
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIEnergyCounter : CRH3C380BBase
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::

        //2
        public override string GetInfo()
        {
            return "DMI能量计量";
        }

        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

        #endregion

        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::

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

            PaintGroundImage(g);
            PaintState(g);
        }


        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::

        private void GetValue()
        {
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 0:
                        append_postCmd(CmdType.ChangePage, 110, 0, 0); //维护
                        break;
                    case 15:
                        append_postCmd(CmdType.ChangePage, 11, 0, 0); //
                        break;
                }
            }

            for (int i = 0; i < m_FValue.Length; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }
        }

        private void PaintState(Graphics e)
        {
            e.DrawString("维护; 能量计量", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));
        }

        private void PaintGroundImage(Graphics e)
        {
            for (int i = 0; i < 11; i++)
            {
                e.DrawString(m_ContentStrs[i], FontsItems.FontC14B,
                    DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_RectsList[12 + i], FontsItems.TheAlignment(FontRelated.靠左));
            }
            for (int i = 0; i < 8; i++)
            {
                e.DrawString("0" + "kWh", FontsItems.FontC14B,
                    DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_RectsList[23 + i], FontsItems.TheAlignment(FontRelated.靠右));
            }

        }

        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        private void InitData()
        {
            m_FValue = new float[UIObj.InFloatList.Count];

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIEnergyCounter, ref m_RectsList))
            {

            }

        }

        #endregion

        #region ::::::::::::::::::::::::::: init values :::::::::::::::::::::::::::

        private List<RectangleF> m_RectsList;


        private float[] m_FValue;

        private readonly string[] m_BtnStr =
        {
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "主页面"
        };

        private readonly string[] m_ContentStrs =
        {
            "使用电量:",
            "牵引单元1:",
            "牵引单元2:",
            "牵引单元3:",
            "牵引单元4:",
            "",
            "再生电量:",
            "牵引单元1:",
            "牵引单元2:",
            "牵引单元3:",
            "牵引单元4:"
        };

        #endregion
    }
}