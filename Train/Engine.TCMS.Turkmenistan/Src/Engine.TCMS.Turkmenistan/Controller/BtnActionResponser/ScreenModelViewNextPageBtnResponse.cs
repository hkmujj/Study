using System.ComponentModel.Composition;
using System.Linq;
using Engine.TCMS.Turkmenistan.Constant;
using Engine.TCMS.Turkmenistan.Event;
using Engine.TCMS.Turkmenistan.Model.BtnStragy;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.Controller.BtnActionResponser
{
    [Export]
    public class ScreenModelVCurrentiewNextPageBtnResponse : IBtnActionResponser
    {
        public BtnItem Parent { get; set; }
        public void UpdateState()
        {

        }

        public void ResponseClick()
        {
            var views = ServiceLocator.Current.GetInstance<IRegionManager>().Regions[RegionNames.ContentRunParam].ActiveViews;
            var s = views.FirstOrDefault().ToString();

            if (s.Equals(ViewNames.RunParamPage1))
            {
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>().Publish(new NavigatorToView.Args(ViewNames.RunParamPage2));
            }
            else if (s.Equals(ViewNames.RunParamPage2))
            {
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>().Publish(new NavigatorToView.Args(ViewNames.RunParamPage3));
            }
            else if (s.Equals(ViewNames.RunParamPage3))
            {
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>().Publish(new NavigatorToView.Args(ViewNames.RunParamPage1));
            }


        }
    }
    [Export]
    public class ScreenModelVCurrentiewLastPageBtnResponse : IBtnActionResponser
    {
        public BtnItem Parent { get; set; }
        public void UpdateState()
        {

        }

        public void ResponseClick()
        {
            var views = ServiceLocator.Current.GetInstance<IRegionManager>().Regions[RegionNames.ContentRunParam].ActiveViews;
            var s = views.FirstOrDefault().ToString();

            if (s.Equals(ViewNames.RunParamPage1))
            {
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>().Publish(new NavigatorToView.Args(ViewNames.RunParamPage3));
            }
            else if (s.Equals(ViewNames.RunParamPage2))
            {
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>().Publish(new NavigatorToView.Args(ViewNames.RunParamPage1));
            }
            else if (s.Equals(ViewNames.RunParamPage3))
            {
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>().Publish(new NavigatorToView.Args(ViewNames.RunParamPage2));
            }
        }
    }
    [Export]
    public class ScreenModelTwoScreenViewNextPageBtnResponse : IBtnActionResponser
    {
        public BtnItem Parent { get; set; }
        public void UpdateState()
        {

        }

        public void ResponseClick()
        {
            var views = ServiceLocator.Current.GetInstance<IRegionManager>().Regions[RegionNames.ContentRunParam].ActiveViews;
            var s = views.FirstOrDefault().ToString();

            if (s.Equals(ViewNames.RunParamPage4))
            {
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>().Publish(new NavigatorToView.Args(ViewNames.RunParamPage5));
            }
            else if (s.Equals(ViewNames.RunParamPage5))
            {
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>().Publish(new NavigatorToView.Args(ViewNames.RunParamPage6));
            }
            else if (s.Equals(ViewNames.RunParamPage6))
            {
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>().Publish(new NavigatorToView.Args(ViewNames.RunParamPage7));
            }
            else if (s.Equals(ViewNames.RunParamPage7))
            {
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>().Publish(new NavigatorToView.Args(ViewNames.RunParamPage4));
            }
        }
    }
    [Export]
    public class ScreenModelTwoScreenViewLastPageBtnResponse : IBtnActionResponser
    {
        public BtnItem Parent { get; set; }
        public void UpdateState()
        {

        }

        public void ResponseClick()
        {
            var views = ServiceLocator.Current.GetInstance<IRegionManager>().Regions[RegionNames.ContentRunParam].ActiveViews;
            var s = views.FirstOrDefault().ToString();

            if (s.Equals(ViewNames.RunParamPage4))
            {
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>().Publish(new NavigatorToView.Args(ViewNames.RunParamPage7));
            }
            else if (s.Equals(ViewNames.RunParamPage5))
            {
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>().Publish(new NavigatorToView.Args(ViewNames.RunParamPage4));
            }
            else if (s.Equals(ViewNames.RunParamPage6))
            {
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>().Publish(new NavigatorToView.Args(ViewNames.RunParamPage5));
            }
            else if (s.Equals(ViewNames.RunParamPage7))
            {
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>().Publish(new NavigatorToView.Args(ViewNames.RunParamPage6));
            }
        }
    }
}
