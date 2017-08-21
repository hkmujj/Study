using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.黑屏
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIBeginScreen : CRH3C380BBase
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::

        //2
        public override string GetInfo()
        {
            return "DMI启动视图";
        }

        public override bool Initalize()
        {
            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIBlackScreen, ref m_RectsList))
            {

            }
            return true;
        }

        #endregion

        private int m_Count;
        private List<RectangleF> m_RectsList;

        public override void Paint(Graphics g)
        {
            if (CanChangePage())
            {
                append_postCmd(CmdType.ChangePage, DMITitle.MMIType == MMIType.右侧MMI屏 ? 21 : 11, 0, 0);
            }

            g.DrawString("欢迎来到\n车载计算机",
                FontsItems.FontC16B,
                SolidBrushsItems.WhiteBrush,
                m_RectsList[0],
                FontsItems.TheAlignment(FontRelated.居中));
        }

        private bool CanChangePage()
        {
            m_Count++;
            if (m_Count > 25)
            {
                m_Count = 0;
                return true;
            }

            if (GlobalParam.Instance.ProjectConfig.AccessControlConfig.CanSkipBeginViewByClickZeroButton)
            {
                if (DMIButton.BtnUpList.Any())
                {
                    m_Count = 0;
                    return true;
                }
            }

            return false;
        }
    }
}