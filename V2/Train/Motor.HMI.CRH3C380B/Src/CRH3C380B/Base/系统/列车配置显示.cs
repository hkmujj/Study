using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource.Images;

namespace Motor.HMI.CRH3C380B.Base.系统
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMITrainConfigurationDisplay : CRH3C380BBase
    {
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

        //2
        public override string GetInfo()
        {
            return "DMI列车配置显示";
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
            PaintGroundImage(g);

            PaintRectangles(g);
        }

        private void GetValue()
        {
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 0:
                        append_postCmd(CmdType.ChangePage, 120, 0, 0);
                        break;
                    case 15:
                        append_postCmd(CmdType.ChangePage, 11, 0, 0);
                        break;
                }
            }

            for (int i = 0; i < m_FValue.Length; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }
        }

        private void PaintRectangles(Graphics e)
        {
            e.DrawString("系统; 列车配置显示", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));
        }

        private void PaintGroundImage(Graphics g)
        {
            g.DrawImage(DMITitle.MarshallMode ? SystemImages.TrainConfigpng : SystemImages.trainconfigbmp,
                m_RectsList[12]);
        }

        private void InitData()
        {
            m_FValue = new float[UIObj.InFloatList.Count];

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMITrainConfigurationDisplay, ref m_RectsList))
            {

            }
        }
    }
}