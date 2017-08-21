
using MMI.Facility.Interface;
using MMI.Facility.PublicUI.Interface;

namespace MMI.Facility.DataType.View.Form
{
    public interface IShareForm : IHelpForm
    {
        void SelectBool(int index, ProjectType projectType = ProjectType.Unkown, string appName= null);

        void SelectFloat(int index, ProjectType projectType = ProjectType.Unkown, string appName = null);
    }
}
