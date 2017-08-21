using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.维修
{
    public abstract class DMIVersionsCarBase : CRH3C380BBase
    {
        private List<RectangleF> m_RectsList;

        protected abstract string[] BtnContents { get; }

        protected abstract Image BackgoundImage { get; }

        protected abstract string TitleName { get; }

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
                DMITitle.BtnContentList[i].BtnStr = BtnContents[i];
            }
        }

        public override void Paint(Graphics g)
        {
            ResponseBtnEvent();

            PaintGroundImage(g);

            PaintRectangles(g);
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
                    case 6:
                        append_postCmd(CmdType.ChangePage, 1161, 0, 0);
                        break;
                    case 7:
                        append_postCmd(CmdType.ChangePage, 1162, 0, 0);
                        break;
                    case 8:
                        append_postCmd(CmdType.ChangePage, 1163, 0, 0);
                        break;
                    case 9:
                        append_postCmd(CmdType.ChangePage, 1164, 0, 0);
                        break;
                    case 10:
                        append_postCmd(CmdType.ChangePage, 1165, 0, 0);
                        break;
                    case 11:
                        append_postCmd(CmdType.ChangePage, 1166, 0, 0);
                        break;
                    case 12:
                        append_postCmd(CmdType.ChangePage, 1167, 0, 0);
                        break;
                    case 13:
                        append_postCmd(CmdType.ChangePage, 1168, 0, 0);
                        break;
                    case 14:
                        break;
                    case 15:
                        append_postCmd(CmdType.ChangePage, 11, 0, 0); //
                        break;
                }
            }
        }

        private void PaintRectangles(Graphics g)
        {
            g.DrawString(TitleName, FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[1], FontsItems.TheAlignment(FontRelated.靠左));
        }

        private void PaintGroundImage(Graphics g)
        {
            g.DrawImage(BackgoundImage, m_RectsList[12]);
        }

        private void InitData()
        {
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIVersionsCar, ref m_RectsList))
            {

            }
        }
    }
}