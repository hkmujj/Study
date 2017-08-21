using Urban.ATC.CommonView.Model;

namespace Urban.ATC.CommonView.View
{
    public partial class LogSimens : TextBase
    {
        public LogSimens()
        {
            InitializeComponent();
            ChangeText("SIEMENS");
            ChangeTextColor(GDICommon.LogColor);
        }
    }
}