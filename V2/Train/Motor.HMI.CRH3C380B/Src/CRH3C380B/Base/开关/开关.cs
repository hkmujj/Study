using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.开关
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMISwitching : CRH3C380BBase
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::

        //2
        public override string GetInfo()
        {
            return "DMI开关";
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
                case 6: //网侧电流
                    append_postCmd(CmdType.ChangePage, 191, 0, 0);
                    break;
                case 8: //风扇
                    append_postCmd(CmdType.ChangePage, 193, 0, 0);
                    break;
                case 9: //牵引
                    append_postCmd(CmdType.ChangePage, 194, 0, 0);
                    break;
                case 10: //空调
                    append_postCmd(CmdType.ChangePage, 195, 0, 0);
                    break;
                case 11: //照明
                    append_postCmd(CmdType.ChangePage, 196, 0, 0);
                    break;
                case 12: //洗车运行
                    append_postCmd(CmdType.ChangePage, 197, 0, 0);
                    break;
                case 14: //前窗加热
                    append_postCmd(CmdType.ChangePage, 199, 0, 0);
                    break;
                case 15: //主页面
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    break;
            }
        }

        private void PaintRectangles(Graphics e)
        {
            e.DrawString("开关", FontsItems.FontC11,
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

            if (Coordinate.RectangleFLists(ViewIDName.DMISwitching, ref m_RectsList))
            {

            }
        }

        #endregion

        #region ::::::::::::::::::::::::::: init values :::::::::::::::::::::::::::

        private List<RectangleF> m_RectsList;

        private readonly string[] m_BtnStr =
        {
            "网侧电流限制",
            "",
            "风扇\n",
            "牵引\n",
            "空调\n",
            "照明\n",
            "洗车\n运行",
            "",
            "前窗\n加热",
            "主页面"
        };

        private readonly string[] m_ContentStrs =
        {
            "1 网侧电流限制",
            "",
            "3 风扇",
            "4 牵引",
            "5 空调",
            "6 照明",
            "7 洗车运行",
            "",
            "9 前窗加热",
            "0 主页面"
        };

        #endregion
    }
}