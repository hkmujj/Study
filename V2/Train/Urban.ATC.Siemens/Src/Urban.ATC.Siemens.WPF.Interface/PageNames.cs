using System.Collections.Generic;

namespace Urban.ATC.Siemens.WPF.Interface
{
    public static class PageNames
    {
        public const string DomainControl = "Urban.ATC.Siemens.WPF.Control.View.DomainControl";
        public const string GeneralScreen = "Urban.ATC.Siemens.WPF.Control.View.GeneralScreen";
        public const string SettingScreen = "Urban.ATC.Siemens.WPF.Control.View.SettingScreen";
        public const string ButtonRestScreen = "Urban.ATC.Siemens.WPF.Control.View.ButtonRestScreen";
        public const string StartView = "Urban.ATC.Siemens.WPF.Control.View.StartView";
        public const string BlackSrceenView = "Urban.ATC.Siemens.WPF.Control.View.BlackSrceenView";
        private static Dictionary<string, string[]> m_TitleName = new Dictionary<string, string[]>();

        static PageNames()
        {
            AddTitle("GeneralScreen", new[] { "信息", "Info" });
            AddTitle("SettingScreen", new[] { "设置", "Setup" });
            AddTitle("", new[] { "", "" });
            AddTitle("GeneralContent", new[] { "信息", "Info" });
            AddTitle("ButtonRestScreen", new[] { "按钮恢复", "  Button  \rReactivation" });
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