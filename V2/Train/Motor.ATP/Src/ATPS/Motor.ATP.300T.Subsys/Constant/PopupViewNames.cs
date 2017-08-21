using System.Linq;
using System.Text;
using Motor.ATP._300T.Subsys.WPFView.PopupViews.Contents;

namespace Motor.ATP._300T.Subsys.Constant
{
    public class PopupContentViewNames
    {
        public static readonly string EnsureEventView = typeof(EnsureEventView).FullName;
        public static readonly string EnsureDriverIdView = typeof(EnsureDriverIdView).FullName;
        public static readonly string EnsureTrainIdView = typeof(EnsureTrainIdView).FullName;
        public static readonly string InputDateTimeView = typeof(InputDateTimeView).FullName;
        public static readonly string InputDriverDataView = typeof(InputDriverDataView).FullName;
        public static readonly string InputDriverIdView = typeof(InputDriverIdView).FullName;
        public static readonly string InputDriverIdAndTrainIdView = typeof(InputDriverDataView).FullName;
        public static readonly string InputRBCView = typeof(InputRBCView).FullName;
        public static readonly string SelectTrainDataView = typeof(SelectTrainDataView).FullName;
        public static readonly string InputTrainIDView = typeof(InputTrainIDView).FullName;
        public static readonly string NullPopupView = typeof(NullPopupView).FullName;
        public static readonly string SelecCTCSView = typeof(SelecCTCSView).FullName;
        public static readonly string SelecDirectionView = typeof(SelecDirectionView).FullName;
        public static readonly string SetLightView = typeof(SetLightView).FullName;
        public static readonly string SetVolumeView = typeof(SetVolumeView).FullName;
        public static readonly string SelectControlModelView = typeof(SelectControlModelView).FullName;
        public static readonly string SelectBrakeTestView = typeof(SelectBrakeTestView).FullName;
        public static readonly string CheckDataContentPopView = typeof(CheckDataContentPopView).FullName;
        public static readonly string CheckSoftwareVersionView = typeof(CheckSoftwareVersionView).FullName;


        // ReSharper disable once UnusedMember.Local
        private static void GetNames()
        {
            var ass = typeof (PopupContentViewNames).Assembly;
            var tps = ass.GetTypes();
            var sb  = new StringBuilder();
            foreach (var t in tps.Where(w => w.FullName.Contains("Motor.ATP._300T.Subsys.WPFView.PopupViews.Contents")))
            {
                sb.AppendFormat("public static readonly string {0} = typeof({0}).FullName;\r\n", t.Name);
            }

            // ReSharper disable once UnusedVariable
            var s = sb.ToString();
        }

    }
}