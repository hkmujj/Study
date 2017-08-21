using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;
using Motor.HMI.CRH3C380B.Resource.Images;

namespace Motor.HMI.CRH3C380B.Base.Restart
{
    /// <summary>
    /// 重启界面
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class RestartView : CRH3C380BBase
    {
        /// <summary>
        /// 占用司机室
        /// </summary>
        private List<string> m_UseDriverRoomIndexCollection;

        private GDIRectText m_TimeText;

        public override bool Initalize()
        {
            m_UseDriverRoomIndexCollection = new List<string>
            {
                InBoolKeys.Inb司机室00占用,
                InBoolKeys.Inb司机室07占用,
                InBoolKeys.Inb司机室08占用,
                InBoolKeys.Inb司机室15占用,
            };

            m_TimeText = new GDIRectText
            {
                OutLineRectangle = new Rectangle(620, 0, 150, 20),
                TextColor = Color.White,
                RefreshAction = o =>
                {
                    var txt = (GDIRectText) o;
                    txt.Text = CurrentTime.ToString("yy MM dd hh:mm:ss");
                }
            };

            return true;
        }

        public override void Paint(Graphics g)
        {
            m_TimeText.OnPaint(g);

            g.DrawImage(CommonImages.welcom, GlobalParam.Instance.FullScreenRectangle);

            if (m_UseDriverRoomIndexCollection.Any(GetInBoolValue))
            {
                append_postCmd(CmdType.ChangePage, 321, 0, 0);
            }
        }
    }
}