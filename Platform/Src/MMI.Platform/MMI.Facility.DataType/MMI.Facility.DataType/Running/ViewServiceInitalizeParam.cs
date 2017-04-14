using System.Diagnostics;
using System.Windows.Forms;
using MMI.Facility.Interface.Data;

namespace MMI.Facility.DataType.Running
{
    public class ViewServiceInitalizeParam
    {
        [DebuggerStepThrough]
        public ViewServiceInitalizeParam(Form viewParent, IDataPackage dataPackage)
        {
            DataPackage = dataPackage;
            ViewParent = viewParent;
        }

        public Form ViewParent { private set; get; }

        public IDataPackage DataPackage { private set; get; }
    }
}