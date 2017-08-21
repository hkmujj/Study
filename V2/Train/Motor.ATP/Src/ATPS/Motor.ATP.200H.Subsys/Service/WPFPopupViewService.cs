using System;
using System.ComponentModel.Composition;
using CommonUtil.Util;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP._200H.Subsys.Constant;

namespace Motor.ATP._200H.Subsys.Service
{
    [Export]
    public class WPFPopupViewService : IPopupViewService
    {
        private readonly IRegionManager m_RegionManager;

        private readonly IEventAggregator m_EventAggregator;

        [ImportingConstructor]
        public WPFPopupViewService(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            m_RegionManager = regionManager;
            m_EventAggregator = eventAggregator;
            m_EventAggregator.GetEvent<ChangePopupViewEvent>()
                .Subscribe(s => m_RegionManager.RequestNavigate(s.RegionName, s.TargetUri), ThreadOption.UIThread, false);
        }

        public void Active(System.Windows.Forms.Control popupView)
        {
            AppLog.Debug("Can not active popupview by contorl, {0}", popupView.GetType());
        }

        public void ActiveView(PopViewParam popupView)
        {
            var tit = new Uri(PopupTitletViewNames.NullPopupTitleView, UriKind.Relative);
            var pu = new Uri(PopupContentViewNames.NullPopupView, UriKind.Relative);

            if (popupView != null)
            {
                if (popupView.PopViewName != null)
                {
                    pu =
                        new Uri(
                            popupView.PopViewName +
                            new UriQuery {{"ViewModelClassFullName", popupView.ViewModelType.FullName}},
                            UriKind.Relative);
                }
                if (popupView.Title != null)
                {
                    var uq = new UriQuery
                    {
                        {PopViewParam.TitleKey, popupView.Title},
                        {PopViewParam.PopViewTitleKey, popupView.PopViewTitle ?? string.Empty}
                    };
                    tit = new Uri(PopupTitletViewNames.NormalPopupTitleView + uq, UriKind.Relative);
                }
            }

            RequestNavigate(RegionNames.RegionPopupViewTitle, tit);

            RequestNavigate(RegionNames.RegionPopupViewContent, pu);
        }

        private void RequestNavigate(string reginName, Uri targetUri)
        {
            m_EventAggregator.GetEvent<ChangePopupViewEvent>().Publish(new ChangePopupViewEvent.EventArgs(reginName, targetUri));
        }
    }
}