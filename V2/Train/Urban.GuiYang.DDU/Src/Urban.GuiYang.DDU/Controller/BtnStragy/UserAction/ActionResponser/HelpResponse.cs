using System;
using System.ComponentModel.Composition;
using System.Linq;
using CommonUtil.Util;
using Microsoft.Practices.Prism.Regions;
using Urban.GuiYang.DDU.Config;
using Urban.GuiYang.DDU.Constant;
using Urban.GuiYang.DDU.View.Contents.Contents;

namespace Urban.GuiYang.DDU.Controller.BtnStragy.UserAction.ActionResponser
{
    [Export]
    public class HelpResponse : OrdinaryActionResponser
    {
        public void GoToHelp()
        {

            try
            {
                var help =
                    HelpView.GetCustomAttributes(typeof(HelpViewAttribute), false).FirstOrDefault() as HelpViewAttribute;
                if (help != null)
                {
                    RegionManager.RequestNavigate(RegionNames.HelpRegion, help.Help.FullName);
                }
            }
            catch (Exception e)
            {
                AppLog.Error(e.ToString());
            }
        }
    }
    [Export]
    public class BypassResponse : OrdinaryActionResponser
    {
        public void GoToBypass1()
        {

            try
            {
                RequestNavigateToContentContent(typeof(MainPageByPass1ContentView));
            }
            catch (Exception e)
            {
                AppLog.Error(e.ToString());
            }
        }
        public void GoToBypass2()
        {

            try
            {
                RequestNavigateToContentContent(typeof(MainPageByPass1ContentView));
            }
            catch (Exception e)
            {
                AppLog.Error(e.ToString());
            }
        }
    }
}

