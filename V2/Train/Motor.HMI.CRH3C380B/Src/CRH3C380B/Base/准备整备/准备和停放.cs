using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.准备整备
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIPreparingStabling : CRH3C380BBase
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::

        //2
        public override string GetInfo()
        {
            return "DMI准备/停放";
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
            if (nParaA != 2)
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

        private void Draw(Graphics e)
        {
            PaintRectangles(e);
        }

        #endregion

        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::

        private void GetValue()
        {
            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }

            switch (DMIButton.BtnUpList[0])
            {
                case 0: //C键
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
                case 6: //列车编号/司机编号
                    break;
                case 8: //开始整备
                    break;
                case 10: //整备结束
                    break;
                case 15: //主页面
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
            }
        }

        private void PaintRectangles(Graphics e)
        {
            e.DrawString("准备/整备", FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));

            for (int i = 0; i < 10; i++)
            {
                e.DrawString(m_ContentStrs[i], FontsItems.FontC24B,
                    DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_RectsList[12 + i], FontsItems.TheAlignment(FontRelated.靠左));
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////
        }

        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        private void InitData()
        {
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIPreparingStabling, ref m_RectsList))
            {

            }
        }

        #endregion

        #region ::::::::::::::::::::::::::: init values :::::::::::::::::::::::::::

        private List<RectangleF> m_RectsList;

        private readonly string[] m_BtnStr =
        {
            "列车编号\n司机编号",
            "",
            "开始整备",
            "",
            "整备结束",
            "",
            "",
            "",
            "",
            "主页面"
        };

        private readonly string[] m_ContentStrs =
        {
            "1 输入列车编号和司机编号",
            "",
            "3 开始整备运行",
            "",
            "5 整备结束 = 准备开始",
            "",
            "",
            "",
            "",
            "0 主页面"
        };

        #endregion
    }
}