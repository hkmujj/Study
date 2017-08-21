using CRH2MMI.Common.Config;
using CRH2MMI.Title;

namespace CRH2MMI.Fault.View
{
    class FaultViewTitleTitleShowStrategy : TitleShowStrategy
    {
        private readonly FaultInfoView m_FaultInfoView;

        public FaultViewTitleTitleShowStrategy(FaultInfoView faultInfoView)
        {
            m_FaultInfoView = faultInfoView;
        }


        public override TitleMenuType GetMenuType()
        {
            return m_FaultInfoView.CurrentFautlPage.GetType() == typeof(FaultDetailInfoPage) ? TitleMenuType.Retrun : TitleMenuType.Menu;
        }


        public override string GetTitleText(ViewConfig currentView)
        {
            return "故障信息";
        }

        public override bool FaultColorVisible()
        {
            return m_FaultInfoView.CurrentFautlPage.GetType() == typeof (FaultDetailInfoPage);
        }

        public override void TurnBackView(Title.Title title)
        {
            if (m_FaultInfoView.CurrentFautlPage.GetType() == typeof(FaultDetailInfoPage))
            {
                m_FaultInfoView.GetoFaultInfoPage();
            }
            else
            {
                GotoMenuView(title);
            }
        }
    }
}
