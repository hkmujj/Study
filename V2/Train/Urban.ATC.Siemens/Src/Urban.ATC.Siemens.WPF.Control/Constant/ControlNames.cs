using System.Collections.Generic;
using Urban.ATC.Siemens.WPF.Control.View;
using Urban.ATC.Siemens.WPF.Control.View.RegionD;

namespace Urban.ATC.Siemens.WPF.Control.Constant
{
    public class ControlNames
    {
        public static string BlackSrceenView = typeof(BlackSrceenView).FullName;
        public static string ButtonRestScreen = typeof(ButtonRestScreen).FullName;
        public static string DomainControl = typeof(DomainControl).FullName;
        public static string GeneralScreen = typeof(GeneralScreen).FullName;
        public static string SettingScreen = typeof(SettingScreen).FullName;
        public static string StartView = typeof(StartView).FullName;
        public static string Menu = typeof(Menu).FullName;
        public static string MenuButtonOpen = typeof(MenuButtonOpen).FullName;
        public static string GeneralContent = typeof(GeneralContent).FullName;
        public static string InputScreen = typeof(InputScreen).FullName;
        public static string ScreenSaverView = typeof (ScreenSaverView).FullName;
        private static Dictionary<string, string[]> m_TitleName = new Dictionary<string, string[]>();
        static ControlNames()
        {
            AddTitle(GeneralScreen, new[] { "信息", "Info" });
            AddTitle(SettingScreen, new[] { "设置", "Setup" });
            AddTitle("", new[] { "", "" });
            AddTitle(GeneralContent, new[] { "信息", "Info" });
            AddTitle(InputScreen, new[] { "按钮恢复", "  Button  \rReactivation" });
        }

        public static void AddTitle(string name, string[] names)
        {
            if (!m_TitleName.ContainsKey(name))
            {
                m_TitleName.Add(name, names);
            }
        }

        public static string[] GetChinaTitle(string name)
        {
            return m_TitleName.ContainsKey(name) ? m_TitleName[name] : m_TitleName[""];
        }
    }
}
