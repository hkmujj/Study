using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.ViewModel;
using MMITool.Addin.YdConfigCreater.Model.Condition.Detail;

namespace MMITool.Addin.YdConfigCreater.Model.Condition
{
    [Export]
    public class ConditionModel : NotificationObject
    {
        public DBParam DBParam { private set; get; }

        public RunParam RunParam { private set; get; }

        public SystemParam SystemParam { private set; get; }

        public ICommand CreateResultCommand { set; get; }

        public ConditionModel()
        {
            DBParam = new DBParam();
            RunParam = new RunParam();
            SystemParam = new SystemParam();
        }
    }
}