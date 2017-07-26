using System.Linq;
using System.Text;
using Motor.ATP._200C.Subsys.WPFView.PopupViews.Title;

namespace Motor.ATP._200C.Subsys.Constant
{
    public class PopupTitletViewNames
    {
        public static readonly string NormalPopupTitleView = typeof(NormalPopupTitleView).FullName;
        public static readonly string NullPopupTitleView = typeof(NullPopupTitleView).FullName;

        // ReSharper disable once UnusedMember.Local
        private static void GetNames()
        {
            var ass = typeof(PopupContentViewNames).Assembly;
            var tps = ass.GetTypes();
            var sb = new StringBuilder();
            foreach (var t in tps.Where(w => w.FullName.Contains("Motor.ATP._200C.Subsys.WPFView.PopupViews.Title")))
            {
                sb.AppendFormat("public static readonly string {0} = typeof({0}).FullName;\r\n", t.Name);
            }

            // ReSharper disable once UnusedVariable
            var s = sb.ToString();
        }
    }
}