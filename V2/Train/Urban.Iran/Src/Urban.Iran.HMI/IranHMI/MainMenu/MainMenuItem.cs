using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CommonUtil.Util;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI.MainMenu
{
    public enum MainMenuItem
    {
        [GotoView((IranViewIndex) 15)] [Description("Text0040")] [DisplayLocation(Horizontal = 0, Vertical = 0)] EventHistory,
        [GotoView((IranViewIndex) 16)] [Description("Text0041")] [DisplayLocation(Horizontal = 1, Vertical = 0)] Interlocks,
        [GotoView((IranViewIndex) 17)] [Description("Text0042")] [DisplayLocation(Horizontal = 2, Vertical = 0)] BypassOverview,
        [GotoView((IranViewIndex) 18)] [Description("Text0043")] [DisplayLocation(Horizontal = 3, Vertical = 0)] Settings,
        [GotoView((IranViewIndex) 19)] [Description("Text0044")] [DisplayLocation(Horizontal = 0, Vertical = 1)] EventOverview,
        [GotoView((IranViewIndex) 20)] [Description("Text0045")] [DisplayLocation(Horizontal = 1, Vertical = 1)] LoadWeight,
        [GotoView((IranViewIndex) 21)] [Description("Text0046")] [DisplayLocation(Horizontal = 2, Vertical = 1)] WSPTest,
        [GotoView((IranViewIndex) 26)] [Description("Text0047")] [DisplayLocation(Horizontal = 3, Vertical = 1)] OperationalData,
        [GotoView((IranViewIndex) 23)] [Description("Text0048")] [DisplayLocation(Horizontal = 0, Vertical = 2)] ODBSState,
        [GotoView((IranViewIndex) 24)] [Description("Text0049")] [DisplayLocation(Horizontal = 1, Vertical = 2)] CommOverview,
        [GotoView((IranViewIndex) 28)] [Description("Text0050")] [DisplayLocation(Horizontal = 2, Vertical = 2)] MaintTest,
        [GotoView((IranViewIndex) 48)] [Description("Text0051")] [DisplayLocation(Horizontal = 3, Vertical = 2)] Fire,
        [GotoView((IranViewIndex) 27)] [Description("Text0052")] [DisplayLocation(Horizontal = 0, Vertical = 3)] SoftwareVersion,
        [GotoView((IranViewIndex) 25)] [Description("Text0053")] [DisplayLocation(Horizontal = 1, Vertical = 3)] DoorParameters,
        [GotoView((IranViewIndex) 22)] [Description("Text0054")] [DisplayLocation(Horizontal = 2, Vertical = 3)] PISTest,
    }

    public static class MainMenuItemExtension
    {
        // ReSharper disable once InconsistentNaming
        private static readonly Dictionary<MainMenuItem, string> m_DescriptionDictionary =
            AllMainMenuItems
                .ToDictionary(kvp => kvp, kvp => EnumUtil.GetDescription(kvp).First());

        // ReSharper disable once InconsistentNaming
        private static readonly Dictionary<MainMenuItem, DisplayLocationAttribute> m_DisplayLocationDictionary = AllMainMenuItems
            .ToDictionary(kvp => kvp,
                kvp =>
                    (DisplayLocationAttribute)
                        kvp.GetType()
                            .GetFields()
                            .First(f => f.Name == kvp.ToString())
                            .GetCustomAttributes(typeof (DisplayLocationAttribute), false)
                            .First());

        private static List<MainMenuItem> m_AllMainMenuItems;
        private static Dictionary<MainMenuItem, GotoViewAttribute> m_GotoViewAttributeDictionary;

        public static List<MainMenuItem> AllMainMenuItems
        {
            get
            {
                return m_AllMainMenuItems ?? (m_AllMainMenuItems = Enum
                    .GetNames(typeof (MainMenuItem))
                    .Select(s => (MainMenuItem) Enum.Parse(typeof (MainMenuItem), s)).ToList());
            }
        }

        public static Dictionary<MainMenuItem, GotoViewAttribute> GotoViewAttributeDictionary
        {
            get
            {
                if (m_GotoViewAttributeDictionary == null)
                {
                    var fields = typeof (MainMenuItem).GetFields();
                    m_GotoViewAttributeDictionary = AllMainMenuItems.ToDictionary(kvp => kvp,
                        kvp => (GotoViewAttribute)
                            fields.First(f => f.Name == kvp.ToString())
                                .GetCustomAttributes(typeof (GotoViewAttribute), false)
                                .First());
                }
                return m_GotoViewAttributeDictionary;
            }
        }

        public static string GetDescription(this MainMenuItem mainMenuItem)
        {
            if (m_DescriptionDictionary.ContainsKey(mainMenuItem))
            {
                return m_DescriptionDictionary[mainMenuItem];
            }
            LogMgr.Error(string.Format("Can not found the description of MainMenuItem.{0}", mainMenuItem));
            return string.Empty;
        }

        public static DisplayLocationAttribute GetDisplayLocation(this MainMenuItem mainMenuItem)
        {
            if (m_DisplayLocationDictionary.ContainsKey(mainMenuItem))
            {
                return m_DisplayLocationDictionary[mainMenuItem];
            }

            LogMgr.Error(string.Format("Can not found the DisplayLocationAttribute of MainMenuItem.{0}", mainMenuItem));
            return null;
        }

        public static GotoViewAttribute GetGotoView(this MainMenuItem mainMenuItem)
        {
            if (GotoViewAttributeDictionary.ContainsKey(mainMenuItem))
            {
                return GotoViewAttributeDictionary[mainMenuItem];
            }

            LogMgr.Error(string.Format("Can not found the GotoViewAttribute of MainMenuItem.{0}", mainMenuItem));
            return null;
        }
    }
}