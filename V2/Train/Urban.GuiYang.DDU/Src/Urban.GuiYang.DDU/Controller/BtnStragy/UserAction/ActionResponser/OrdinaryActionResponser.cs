using System;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Urban.GuiYang.DDU.Extension;
using Urban.GuiYang.DDU.Model.BtnStragy;
using Urban.GuiYang.DDU.Resources.Keys;
using Urban.GuiYang.DDU.View.Contents.Contents;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Controller.BtnStragy.UserAction.ActionResponser
{
    public class OrdinaryActionResponser : NotificationObject, IBtnActionResponser
    {
        public OrdinaryActionResponser()
        {
            EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            Domain = ServiceLocator.Current.GetInstance<DomainViewModel>();
        }

        protected static string StateKey;
        protected static Type HelpView;
        protected static Type BypassBack;
        protected static bool IsContent;
        protected static Type FauleReturnView;

        public IEventAggregator EventAggregator { get; private set; }

        public IRegionManager RegionManager { get; private set; }

        public BtnItem Parent { get; set; }

        public DomainViewModel Domain { get; private set; }

        public virtual void UpdateState()
        {
        }

        /// <summary>
        /// 响应按键
        /// </summary>
        public virtual void ResponseClick()
        {
        }

        /// <summary>
        /// 导航内容，包含Train
        /// </summary>
        /// <param name="contentContentContentViewType"></param>
        protected void RequestNavigateToContentContent(Type contentContentContentViewType)
        {
            RequestNavigateToContentContent(StateKeys.Root, contentContentContentViewType);
        }

        /// <summary>
        /// 导航内容，包含Train
        /// </summary>
        /// <param name="stateKey"></param>
        /// <param name="contentContentContentViewType"></param>
        protected void RequestNavigateToContentContent(string stateKey, Type contentContentContentViewType)
        {
            SetLast(stateKey, contentContentContentViewType, false);
            Domain.Controller.NavigateTo(stateKey);
            RegionManager.RequestNavigateToContentContent(contentContentContentViewType);
        }

        /// <summary>
        /// 导航内容，不需要 Train
        /// </summary>
        /// <param name="contentViewType"></param>
        protected void RequestNavigateToContent(Type contentViewType)
        {
            RequestNavigateToContent(StateKeys.Root, contentViewType);
        }

        /// <summary>
        /// 导航内容，不需要 Train
        /// </summary>
        /// <param name="stateKey"></param>
        /// <param name="contentViewType"></param>
        protected void RequestNavigateToContent(string stateKey, Type contentViewType)
        {
            SetLast(stateKey, contentViewType, true);
            Domain.Controller.NavigateTo(stateKey);
            RegionManager.RequestNavigateToContent(contentViewType);
        }

        private void SetLast(string key, Type view, bool isContent)
        {
            StateKey = key;
            HelpView = view;
            IsContent = isContent;
            if (view != typeof(MainPageByPass1ContentView) && view != typeof(MainPageByPass2ContentView))
            {
                BypassBack = view;
            }

        }

        public void GoToLast()
        {
            if (IsContent)
            {
                RequestNavigateToContent(StateKey, BypassBack);
            }
            else
            {
                RequestNavigateToContentContent(StateKey, BypassBack);
            }
        }

    }
}