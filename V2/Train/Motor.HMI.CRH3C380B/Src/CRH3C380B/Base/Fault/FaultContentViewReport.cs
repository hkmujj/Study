using System.Collections.Generic;
using System.Drawing;
using Motor.HMI.CRH3C380B.Base.底层共用;

namespace Motor.HMI.CRH3C380B.Base.Fault
{
    public class FaultContentViewReport : FaultContentViewBase
    {
        private readonly string[] m_ButtomBtnContens;
        private readonly Font m_ContentFont;

        

        public override int BelongToView
        {
            get { return 41; }
        }

        public override FaultViewType ViewType
        {
            get { return FaultViewType.Report; }
        }

        // ReSharper disable once ConvertToAutoProperty
        public override string[] ButtomBtnContens
        {
            get { return m_ButtomBtnContens; }
        }

        // ReSharper disable once ConvertToAutoProperty
        public override Font ContentFont
        {
            get { return m_ContentFont; }
        }

        protected override string GetCurrentMsgContent()
        {
            return CurrentSelectedFaultInfo.MsgContent;
        }

        public FaultContentViewReport(DMIFault targetObj, IList<RectangleF> rectsList) : base(targetObj, rectsList)
        {
            m_ButtomBtnContens = new[] { "", "故障\n概况", "", "", "", "", "", "", "", "返回" };
            m_ContentFont = FontsItems.FontC11;
        }
    }
}